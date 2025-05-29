Imports MySql.Data.MySqlClient

Public Class frmLogs
    Private Sub frmLogs_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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
                  "ORDER BY l.timestamp DESC"

            cmd = New MySqlCommand(sql, cn)
            dr = cmd.ExecuteReader

            ListView1.Items.Clear()
            While dr.Read
                newLine = New ListViewItem
                newLine.Text = dr("timestamp").ToString
                newLine.SubItems.Add(dr("Username").ToString)
                newLine.SubItems.Add(dr("action").ToString)
                newLine.SubItems.Add(dr("details").ToString)
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

    Private Sub ListView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListView1.SelectedIndexChanged

    End Sub
End Class