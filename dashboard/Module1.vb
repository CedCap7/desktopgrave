Imports MySql.Data.MySqlClient

Module Module1
    Public cn As New MySqlConnection
    Public dr As MySqlDataReader
    Public cmd As MySqlCommand

    Public uid As Integer
    Public itemid As Integer
    Public supid As Integer
    Public sql As String
    Public newLine As ListViewItem

    Public imgpath As String
    Public arrImage() As Byte

    Public Sub dbconn()
        If cn.State = ConnectionState.Open Then
            cn.Close()
        End If
        cn.ConnectionString = "server=srv594.hstgr.io; database=u976878483_cemetery; username=u976878483_doncarlos; password=d0Nc4los; port=3306"
    End Sub

    Public Sub LogUserAction(ByVal action As String, ByVal details As String)
        If uid = 0 Then
            MessageBox.Show("User is not logged in. Cannot log action.", "Logging Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If
        Try
            If cn.State = ConnectionState.Closed Then
                cn.Open()
            End If

            sql = "INSERT INTO user_logs (user_id, action, details, timestamp) VALUES (@user_id, @action, @details, @timestamp)"
            cmd = New MySqlCommand(sql, cn)
            cmd.Parameters.AddWithValue("@user_id", uid)
            cmd.Parameters.AddWithValue("@action", action)
            cmd.Parameters.AddWithValue("@details", details)
            cmd.Parameters.AddWithValue("@timestamp", DateTime.Now)
            cmd.ExecuteNonQuery()

        Catch ex As Exception
            MessageBox.Show("Error logging action: " & ex.Message, "Logging Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
End Module