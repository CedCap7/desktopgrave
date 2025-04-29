Imports MySql.Data.MySqlClient
Imports Guna.UI2.WinForms
Imports System.Data

Public Class frmDashboard
    ' Ensure connection is initialized globally
    Private cn As MySqlConnection
    Private sql As String



    Private Sub frmHome_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        InitializeConnection() ' Properly initialize connection
        UpdateCounts()
        LoadNotifications()
        SetupNotificationColumns() ' Initialize columns once

    End Sub

    Private Sub InitializeConnection()
        Try
            ' Replace this with your actual connection string or method
            cn = New MySqlConnection("server=localhost; database=dccms; username=root; password=root; port=3306")
            ' Or call your existing dbconn() method if it properly sets up cn
            dbconn()
        Catch ex As Exception
            MessageBox.Show("Error initializing database connection: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub SetupNotificationColumns()
        ' Set up columns once
        If dgvNotification.Columns.Count = 0 Then
            dgvNotification.Columns.Add("ID", "ID")
            dgvNotification.Columns.Add("Name", "Name")
            dgvNotification.Columns.Add("IntermentDate", "Interment Date")
            dgvNotification.Columns.Add("ExpirationDate", "Expiration Date")
            dgvNotification.Columns.Add("Location", "Plot Location")
            dgvNotification.Columns.Add("Deceased_Status", "Status")


        End If
    End Sub

    Private Sub UpdateCounts()
        Try
            If cn.State <> ConnectionState.Open Then cn.Open()

            ' Count clients
            Using cmd As New MySqlCommand("SELECT COUNT(*) FROM client", cn)
                lblTotalClients.Text = Format(Convert.ToInt32(cmd.ExecuteScalar()), "#,##0")
            End Using

            ' Count deceased
            Using cmd As New MySqlCommand("SELECT COUNT(*) FROM deceased", cn)
                lblTotalDeceased.Text = Format(Convert.ToInt32(cmd.ExecuteScalar()), "#,##0")
            End Using

            ' Count all available spaces
            sql = "SELECT SUM(CASE 
                     WHEN l.type = 1 THEN (4 - COALESCE(occupied.count, 0)) 
                     WHEN l.type = 2 THEN (1 - COALESCE(occupied.count, 0)) 
                     WHEN l.type = 3 THEN (30 - COALESCE(occupied.count, 0)) 
                     WHEN l.type = 4 THEN (4 - COALESCE(occupied.count, 0)) 
                     ELSE 0 END) AS available 
                   FROM location l 
                   LEFT JOIN (SELECT Plot_ID, COUNT(*) AS count FROM deceased GROUP BY Plot_ID) occupied 
                   ON l.id = occupied.Plot_ID"

            Using cmd As New MySqlCommand(sql, cn)
                lblAvailable.Text = Format(Convert.ToInt32(cmd.ExecuteScalar()), "#,##0")
            End Using

            ' Update for each type
            UpdateSpaceInfo(1, lblApartment, pbApartment)
            UpdateSpaceInfo(2, lblLawnlots, pblawnlots)
            UpdateSpaceInfo(3, lblBoneNiche, pbboneniche)
            UpdateSpaceInfo(4, lblPrivate, pbprivate)

        Catch ex As Exception
            MessageBox.Show("Error updating counts: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If cn.State = ConnectionState.Open Then cn.Close()
        End Try
    End Sub

    Private Sub UpdateSpaceInfo(type As Integer, label As Label, progressBar As Guna2CircleProgressBar)
        Dim capacityPerPlot As Integer = If(type = 1 Or type = 4, 4, If(type = 2, 1, 30))

        ' Use parameterized query to avoid SQL injection
        sql = "SELECT 
                (SELECT COUNT(*) FROM location WHERE type = @type) * @capacity AS total_spaces, 
                (SELECT COUNT(*) FROM deceased d 
                 INNER JOIN location l ON d.Plot_ID = l.id 
                 WHERE l.type = @type) AS used_spaces"

        Try
            If cn.State <> ConnectionState.Open Then cn.Open()

            Using cmd As New MySqlCommand(sql, cn)
                cmd.Parameters.AddWithValue("@type", type)
                cmd.Parameters.AddWithValue("@capacity", capacityPerPlot)

                Using reader As MySqlDataReader = cmd.ExecuteReader()
                    If reader.Read() Then
                        Dim totalSpaces As Integer = Convert.ToInt32(reader("total_spaces"))
                        Dim usedSpaces As Integer = Convert.ToInt32(reader("used_spaces"))
                        Dim availableSpaces As Integer = totalSpaces - usedSpaces

                        label.Text = $"{Format(availableSpaces, "#,##0")} out of {Format(totalSpaces, "#,##0")}"

                        ' Progress calculation
                        Dim percentage As Integer = If(totalSpaces > 0, CInt((usedSpaces / totalSpaces) * 100), 0)

                        ' Configure Guna2CircleProgressBar
                        With progressBar
                            .Minimum = 0
                            .Maximum = 100
                            .Value = percentage

                            ' Set colors based on percentage
                            Select Case percentage
                                Case >= 90
                                    .ProgressColor = Color.FromArgb(255, 82, 96) ' Red
                                Case >= 70
                                    .ProgressColor = Color.FromArgb(255, 152, 0) ' Orange
                                Case Else
                                    .ProgressColor = Color.FromArgb(80, 200, 120) ' Green
                            End Select
                        End With
                    End If
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error updating space info: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub LoadNotifications()
        Try
            If cn.State <> ConnectionState.Open Then cn.Open()

            sql = "SELECT d.Deceased_ID, d.FirstName, d.LastName, d.Interment, 
                          DATE_ADD(d.Interment, INTERVAL 8 YEAR) AS ExpirationDate, 
                          d.deceased_status,
                          CASE l.type 
                              WHEN 1 THEN 'Apartment' 
                              WHEN 2 THEN 'Family Lawn Lots' 
                              WHEN 3 THEN 'Bone Niche' 
                              WHEN 4 THEN 'Private' 
                          END AS PlotType, 
                          l.block, l.section, l.row, l.plot, 
                          CONCAT(
                              CASE l.type 
                                  WHEN 1 THEN 'Apartment' 
                                  WHEN 2 THEN 'Family Lawn Lots' 
                                  WHEN 3 THEN 'Bone Niche' 
                                  WHEN 4 THEN 'Private' 
                              END, ' - Block ', l.block, ', Section ', l.section, 
                              ', Row ', l.row, ', Plot ', l.plot
                          ) AS Location 
                   FROM deceased d 
                   JOIN location l ON d.Plot_ID = l.id 
                   WHERE DATE_ADD(d.Interment, INTERVAL 8 YEAR) <= CURDATE()"

            Using cmd As New MySqlCommand(sql, cn)
                Using reader As MySqlDataReader = cmd.ExecuteReader()
                    dgvNotification.Rows.Clear()

                    While reader.Read()

                        Dim rowIndex As Integer = dgvNotification.Rows.Add(
                            reader("Deceased_ID").ToString(),
                            reader("FirstName").ToString() & " " & reader("LastName").ToString(),
                            Convert.ToDateTime(reader("Interment")).ToString("yyyy-MM-dd"),
                            Convert.ToDateTime(reader("ExpirationDate")).ToString("yyyy-MM-dd"),
                            reader("Location").ToString(),
                            reader("deceased_status").ToString()
                        )


                    End While
                End Using
            End Using

            dgvNotification.AutoResizeColumns()

        Catch ex As Exception
            MessageBox.Show("Error loading notifications: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If cn.State = ConnectionState.Open Then cn.Close()
        End Try
    End Sub

    ' Refresh Data
    Private Sub RefreshCounts_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        UpdateCounts()
        LoadNotifications()
    End Sub

    ' Auto-refresh using timer
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles tmDashboard.Tick
        UpdateCounts()
    End Sub

End Class
