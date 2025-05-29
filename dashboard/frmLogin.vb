Public Class frmLogin
    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        Dim conn As New MySql.Data.MySqlClient.MySqlConnection("server=srv594.hstgr.io; database=u976878483_cemetery; username=u976878483_doncarlos; password=d0Nc4los; port=3306")
        Dim sql As String = "SELECT * FROM user WHERE Username = @username AND Password = @password"

        Try

            conn.Open()

            Dim cmd As New MySql.Data.MySqlClient.MySqlCommand(sql, conn)
            
            cmd.Parameters.AddWithValue("@username", txtUser.Text)
            cmd.Parameters.AddWithValue("@password", txtPass.Text)
            cmd.Parameters.AddWithValue("@user_id", uid)

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

    Private Sub Guna2CustomGradientPanel1_Paint(sender As Object, e As PaintEventArgs) Handles Guna2CustomGradientPanel1.Paint

    End Sub

    Private Sub txtUser_TextChanged(sender As Object, e As EventArgs) Handles txtUser.TextChanged

    End Sub
End Class