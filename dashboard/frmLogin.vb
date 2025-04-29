Public Class frmLogin
    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        Dim conn As New MySql.Data.MySqlClient.MySqlConnection("server=localhost; userid=root; password=root; database=dccms; port=3306")
        Dim sql As String = "SELECT * FROM user WHERE Username = @username AND Password = @password"

        Try

            conn.Open()

            Dim cmd As New MySql.Data.MySqlClient.MySqlCommand(sql, conn)
            cmd.Parameters.AddWithValue("@username", txtUser.Text)
            cmd.Parameters.AddWithValue("@password", txtPass.Text)

            Dim dr As MySql.Data.MySqlClient.MySqlDataReader = cmd.ExecuteReader()

            If dr.HasRows Then
                While dr.Read()

                    uid = dr("user_id")
                End While

                dr.Close()
                conn.Close()

                Me.Hide()
                dashboard.ShowDialog()

            Else
                MsgBox("Invalid login!")
            End If
        Catch ex As MySql.Data.MySqlClient.MySqlException

            MessageBox.Show("Database error: " & ex.Message)

        Finally

            If conn IsNot Nothing AndAlso conn.State = ConnectionState.Open Then
                conn.Close()

            End If

        End Try
    End Sub
End Class