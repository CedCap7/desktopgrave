Imports MySql.Data.MySqlClient
Imports System.IO

Public Class frmLogs
    Private Sub frmLogs_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Set up ListView columns
        ListView1.View = View.Details
        ListView1.FullRowSelect = True
        ListView1.GridLines = True
        
        ' Add columns if they don't exist
        If ListView1.Columns.Count = 0 Then
            ListView1.Columns.Add("Date & Time", 150)
            ListView1.Columns.Add("Username", 100)
            ListView1.Columns.Add("Action", 100)
            ListView1.Columns.Add("Details", 300)
        End If

        ' Set up control properties
        Label1.Text = "From:"
        Label2.Text = "To:"
        btnSearch.Text = "Search"
        btnExport.Text = "Export to CSV"
        btnRefresh.Text = "Refresh"
        
        txtDetails.Multiline = True
        txtDetails.ReadOnly = True
        txtDetails.ScrollBars = ScrollBars.Vertical
        
        ' Initialize date pickers
        dtpFrom.Value = DateTime.Now.AddDays(-7) ' Default to last 7 days
        dtpTo.Value = DateTime.Now
        
        LoadLogs()
    End Sub

    Private Sub LoadLogs()
        Try
            If cn.State = ConnectionState.Closed Then
                cn.Open()
            End If

            sql = "SELECT l.timestamp, u.Username, l.action, l.details " &
                  "FROM user_logs l " &
                  "JOIN user u ON l.user_id = u.user_id " &
                  "WHERE l.timestamp BETWEEN @fromDate AND @toDate " &
                  "ORDER BY l.timestamp DESC"

            cmd = New MySqlCommand(sql, cn)
            cmd.Parameters.AddWithValue("@fromDate", dtpFrom.Value.ToString("yyyy-MM-dd 00:00:00"))
            cmd.Parameters.AddWithValue("@toDate", dtpTo.Value.ToString("yyyy-MM-dd 23:59:59"))
            
            dr = cmd.ExecuteReader

            ListView1.Items.Clear()
            While dr.Read
                newLine = New ListViewItem
                ' Format the timestamp to be more readable
                Dim timestamp As DateTime = Convert.ToDateTime(dr("timestamp"))
                newLine.Text = timestamp.ToString("MM/dd/yyyy hh:mm:ss tt")
                newLine.SubItems.Add(dr("Username").ToString)
                newLine.SubItems.Add(dr("action").ToString)
                newLine.SubItems.Add(dr("details").ToString)
                
                ' Color code based on action type
                Select Case dr("action").ToString.ToLower
                    Case "login"
                        newLine.BackColor = Color.LightGreen
                    Case "update", "edit user", "edit deceased"
                        newLine.BackColor = Color.LightBlue
                    Case "system update"
                        newLine.BackColor = Color.LightYellow
                    Case "delete user"
                        newLine.BackColor = Color.LightCoral
                    Case "add user", "add client", "add deceased"
                        newLine.BackColor = Color.LightCyan
                    Case "import", "export"
                         newLine.BackColor = Color.LightGoldenrodYellow
                    Case Else ' Default color for actions not explicitly handled
                         newLine.BackColor = Color.White
                End Select
                
                ListView1.Items.Add(newLine)
            End While
            dr.Close()

        Catch ex As Exception
            MessageBox.Show("Error loading logs: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        LoadLogs()
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        LoadLogs()
    End Sub

    Private Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        Try
            Dim saveFileDialog As New SaveFileDialog()
            saveFileDialog.Filter = "CSV files (*.csv)|*.csv"
            saveFileDialog.Title = "Export Logs"
            saveFileDialog.FileName = "SystemLogs_" & DateTime.Now.ToString("yyyyMMdd_HHmmss")

            If saveFileDialog.ShowDialog() = DialogResult.OK Then
                Using writer As New StreamWriter(saveFileDialog.FileName)
                    ' Write header
                    writer.WriteLine("Date & Time,Username,Action,Details")
                    
                    ' Write data
                    For Each item As ListViewItem In ListView1.Items
                        writer.WriteLine(String.Format("{0},{1},{2},{3}",
                            item.Text,
                            item.SubItems(1).Text,
                            item.SubItems(2).Text,
                            item.SubItems(3).Text))
                    Next
                End Using
                MessageBox.Show("Logs exported successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Catch ex As Exception
            MessageBox.Show("Error exporting logs: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ListView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListView1.SelectedIndexChanged
        If ListView1.SelectedItems.Count > 0 Then
            Dim selectedItem As ListViewItem = ListView1.SelectedItems(0)
            txtDetails.Text = String.Format("Date & Time: {0}" & vbCrLf &
                                          "Username: {1}" & vbCrLf &
                                          "Action: {2}" & vbCrLf &
                                          "Details: {3}",
                                          selectedItem.Text,
                                          selectedItem.SubItems(1).Text,
                                          selectedItem.SubItems(2).Text,
                                          selectedItem.SubItems(3).Text)
        End If
    End Sub
End Class