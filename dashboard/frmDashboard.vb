Imports MySql.Data.MySqlClient
Imports Guna.UI2.WinForms
Imports System.Data
Imports System.Net
Imports System.Net.Mail

Public Class frmDashboard

    Private notificationCount As Integer = 0

    ' Add new controls
    Private WithEvents btnMarkAsRead As New Guna2Button()
    Private WithEvents cmbFilter As New Guna2ComboBox()
    Private WithEvents lblNotificationCount As New Label()

    Private Sub frmHome_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        InitializeConnection()
        SetupNotificationControls()
        UpdateCounts()
        LoadNotifications()
        SetupNotificationColumns()
    End Sub


    Private Sub SetupNotificationControls()
        ' Setup Notification Counter Label
        With lblNotificationCount
            .Text = "0 Notifications"
            .Font = New Font("Segoe UI", 12, FontStyle.Bold)
            .ForeColor = Color.FromArgb(255, 82, 96)
            .Location = New Point(10, 10)
            .AutoSize = True
        End With
        pnlNotifications.Controls.Add(lblNotificationCount)

        ' Setup Filter ComboBox
        With cmbFilter
            .Items.Add("All Notifications")
            .Items.Add("Recently Expired")
            .Items.Add("Expiring Soon")
            .SelectedIndex = 0
            .Location = New Point(150, 10)
            .Size = New Size(150, 30)
        End With
        pnlNotifications.Controls.Add(cmbFilter)

        ' Setup Mark as Read Button
        With btnMarkAsRead
            .Text = "Mark as Read"
            .Location = New Point(310, 10)
            .Size = New Size(120, 30)
            .Enabled = False
        End With
        pnlNotifications.Controls.Add(btnMarkAsRead)
    End Sub

    Private Sub SetupNotificationColumns()
        If dgvNotification.Columns.Count = 0 Then
            dgvNotification.Columns.Add("ID", "ID")
            dgvNotification.Columns.Add("Name", "Name")
            dgvNotification.Columns.Add("IntermentDate", "Interment Date")
            dgvNotification.Columns.Add("ExpirationDate", "Expiration Date")
            dgvNotification.Columns.Add("Location", "Plot Location")
            dgvNotification.Columns.Add("Deceased_Status", "Status")
            dgvNotification.Columns.Add("DaysExpired", "Days Expired")
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

    Private Sub InitializeConnection()
        Try
            ' Replace this with your actual connection string or method
            dbconn()
            ' IMPORTANT: If using custom dbconn() method, make sure it doesn't create a new connection object
            ' that overwrites the global cn variable
            ' dbconn()  ' Comment this out if it's causing issues
        Catch ex As Exception
            MessageBox.Show("Error initializing database connection: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub LoadNotifications()
        Try
            If cn.State <> ConnectionState.Open Then cn.Open()

            sql = "SELECT d.Deceased_ID, d.FirstName, d.LastName, d.Interment, 
                      DATE_ADD(d.Interment, INTERVAL 8 YEAR) AS ExpirationDate, 
                      d.deceased_status,
                      DATEDIFF(CURDATE(), DATE_ADD(d.Interment, INTERVAL 8 YEAR)) AS DaysExpired,
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
                      ) AS Location,
                      c.Email AS ClientEmail,
                      CONCAT(c.FirstName, ' ', c.LastName) AS ClientName,
                      d.last_notification_date
               FROM deceased d 
               JOIN location l ON d.Plot_ID = l.id 
               JOIN client c ON d.Client_ID = c.Client_ID
               WHERE DATE_ADD(d.Interment, INTERVAL 8 YEAR) <= CURDATE()
               AND LOWER(d.deceased_status) NOT IN (LOWER('Remaining'), LOWER('Relocated'), LOWER('Renewal'), LOWER('Pending'))"

            ' Apply filter based on combobox selection
            Select Case cmbFilter.SelectedIndex
                Case 1 ' Recently Expired
                    sql &= " AND DATEDIFF(CURDATE(), DATE_ADD(d.Interment, INTERVAL 8 YEAR)) <= 30"
                Case 2 ' Expiring Soon
                    sql &= " AND DATEDIFF(CURDATE(), DATE_ADD(d.Interment, INTERVAL 8 YEAR)) > 30"
            End Select

            sql &= " ORDER BY DaysExpired DESC"

            Dim emailsToSend As New List(Of Tuple(Of String, String, String, String, String, Integer))

            Using cmd As New MySqlCommand(sql, cn)
                Using reader As MySqlDataReader = cmd.ExecuteReader()
                    dgvNotification.Rows.Clear()
                    notificationCount = 0

                    While reader.Read()
                        Dim rowIndex As Integer = dgvNotification.Rows.Add(
                            reader("Deceased_ID").ToString(),
                            reader("FirstName").ToString() & " " & reader("LastName").ToString(),
                            Convert.ToDateTime(reader("Interment")).ToString("yyyy-MM-dd"),
                            Convert.ToDateTime(reader("ExpirationDate")).ToString("yyyy-MM-dd"),
                            reader("Location").ToString(),
                            reader("deceased_status").ToString(),
                            reader("DaysExpired").ToString()
                        )
                        notificationCount += 1

                        ' Collect info for email notification if the deceased status is expired and no notification was sent in the last 7 days
                        If reader("deceased_status").ToString().ToLower() = "expired" Then
                            Dim lastNotificationDate As DateTime? = If(reader("last_notification_date") IsNot DBNull.Value, 
                                                                     Convert.ToDateTime(reader("last_notification_date")), 
                                                                     Nothing)
                            If Not lastNotificationDate.HasValue OrElse 
                               DateDiff(DateInterval.Day, lastNotificationDate.Value, DateTime.Now) > 7 Then
                                Dim clientEmail As String = reader("ClientEmail").ToString()
                                Dim clientName As String = reader("ClientName").ToString()
                                Dim deceasedName As String = reader("FirstName").ToString() & " " & reader("LastName").ToString()
                                Dim location As String = reader("Location").ToString()
                                Dim expirationDate As String = Convert.ToDateTime(reader("ExpirationDate")).ToString("MMMM dd, yyyy")
                                Dim deceasedId As Integer = Convert.ToInt32(reader("Deceased_ID"))
                                emailsToSend.Add(Tuple.Create(clientEmail, clientName, deceasedName, location, expirationDate, deceasedId))
                            End If
                        End If
                    End While
                End Using ' DataReader closed here
            End Using

            ' Now send emails and update DB (connection is free)
            For Each emailInfo In emailsToSend
                SendExpirationEmail(emailInfo.Item1, emailInfo.Item2, emailInfo.Item3, emailInfo.Item4, emailInfo.Item5, emailInfo.Item6)
            Next

            ' Update notification count
            lblNotificationCount.Text = $"{notificationCount} Notification(s)"
            btnMarkAsRead.Enabled = notificationCount > 0

            dgvNotification.AutoResizeColumns()

        Catch ex As Exception
            MessageBox.Show("Error loading notifications: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If cn.State = ConnectionState.Open Then cn.Close()
        End Try
    End Sub

    Private Sub SendExpirationEmail(toEmail As String, clientName As String, deceasedName As String, location As String, expirationDate As String, deceasedId As Integer)
        Try
            ' Set up the SMTP client
            Dim smtpClient As New SmtpClient("smtp.gmail.com")
            smtpClient.Port = 587
            smtpClient.Credentials = New NetworkCredential("cedrickcap7@gmail.com", "empj kkji wjhl sfij")
            smtpClient.EnableSsl = True

            ' Create the email message
            Dim mailMessage As New MailMessage()
            mailMessage.From = New MailAddress("cedrickcap7@gmail.com")
            mailMessage.To.Add(toEmail)
            mailMessage.Subject = "Grave Space Expiration Notice"
            mailMessage.Body = String.Format("Dear {0},{1}{1}" &
                                           "This is to inform you that the grave space for {2} at {3} has expired on {4}.{1}{1}" &
                                           "Please visit the municipal office to discuss renewal options or make necessary arrangements.{1}{1}" &
                                           "Best regards,{1}Your Cemetery Management Team",
                                           clientName, Environment.NewLine, deceasedName, location, expirationDate)

            ' Send the email
            smtpClient.Send(mailMessage)

            ' Update the last notification date in the database
            If cn.State <> ConnectionState.Open Then cn.Open()
            sql = "UPDATE deceased SET last_notification_date = NOW() WHERE Deceased_ID = @id"
            Using cmd As New MySqlCommand(sql, cn)
                cmd.Parameters.AddWithValue("@id", deceasedId)
                cmd.ExecuteNonQuery()
            End Using

        Catch ex As Exception
            MessageBox.Show("Error sending expiration email: " & ex.Message)
        End Try
    End Sub

    Private Sub btnMarkAsRead_Click(sender As Object, e As EventArgs) Handles btnMarkAsRead.Click
        If dgvNotification.SelectedRows.Count > 0 Then
            Dim transaction As MySqlTransaction = Nothing
            Try
                ' Make sure to use the same connection object throughout
                If cn.State <> ConnectionState.Open Then cn.Open()
                transaction = cn.BeginTransaction()

                For Each row As DataGridViewRow In dgvNotification.SelectedRows
                    Dim deceasedId As String = row.Cells("ID").Value.ToString()
                    sql = "UPDATE deceased SET deceased_status = 'Remaining' WHERE Deceased_ID = @id"

                    Using cmd As New MySqlCommand(sql, cn, transaction)
                        cmd.Parameters.AddWithValue("@id", deceasedId)
                        Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

                        If rowsAffected = 0 Then
                            MessageBox.Show($"Failed to update status for deceased ID: {deceasedId}", "Update Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        End If
                    End Using
                Next

                ' Commit the transaction
                transaction.Commit()
                MessageBox.Show("Selected notifications have been marked as read.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

                ' Force connection to close before reloading notifications
                If cn.State = ConnectionState.Open Then cn.Close()

                ' Reload notifications to refresh the UI
                LoadNotifications()

            Catch ex As Exception
                ' Rollback the transaction if there was an error
                If transaction IsNot Nothing Then
                    transaction.Rollback()
                End If
                MessageBox.Show("Error marking notifications as read: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                ' Always close the connection
                If cn.State = ConnectionState.Open Then cn.Close()
            End Try
        Else
            MessageBox.Show("Please select at least one notification to mark as read.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub



    Private Sub dgvNotification_SelectionChanged(sender As Object, e As EventArgs) Handles dgvNotification.SelectionChanged
        btnMarkAsRead.Enabled = dgvNotification.SelectedRows.Count > 0

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

    Private Sub dgvNotification_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvNotification.CellContentClick

    End Sub

    Private Sub Label4_Click(sender As Object, e As EventArgs) Handles Label4.Click

    End Sub

    Private Sub Guna2Button1_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub pnlNotifications_Paint(sender As Object, e As PaintEventArgs) Handles pnlNotifications.Paint

    End Sub
End Class
