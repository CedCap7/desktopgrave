Imports MySql.Data.MySqlClient
Public Class frmUpdateClient
    Private client_ID As Integer

    Public Sub New(selectedID As Integer)
        InitializeComponent()
        client_ID = selectedID
        LoadClientData()
    End Sub

    Private Sub LoadClientData()
        Try
            dbconn()
            cn.Open()

            Dim sql As String = "SELECT * FROM client WHERE Client_ID = @ID"
            Dim cmd As New MySqlCommand(sql, cn)
            cmd.Parameters.AddWithValue("@ID", client_ID)

            Dim dr As MySqlDataReader = cmd.ExecuteReader()

            If dr.Read() Then
                txtFirstName.Text = dr("FirstName").ToString()
                txtLastName.Text = dr("LastName").ToString()
                txtMiddleName.Text = dr("MiddleName").ToString()
                txtMobile.Text = dr("Mobile").ToString()
                txtAddress.Text = dr("Address").ToString()
                txtEmail.Text = dr("Email").ToString()
                txtExt.Text = dr("Ext").ToString()
                If dr("Gender").ToString() = "Male" Then
                    chkMale.Checked = True
                Else
                    chkFemale.Checked = True
                End If
            End If

            dr.Close()
            cn.Close()

        Catch ex As MySqlException
            MessageBox.Show("Database error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If cn IsNot Nothing AndAlso cn.State = ConnectionState.Open Then
                cn.Close()
            End If
        End Try
    End Sub

    Private Sub btnUpdateClient_Click(sender As Object, e As EventArgs) Handles btnUpdateClient.Click
        Try
            dbconn()
            cn.Open()

            Dim sql As String = "UPDATE client SET " &
                            "LastName = @LastName, " &
                            "FirstName = @FirstName, " &
                            "MiddleName = @MiddleName, " &
                            "Ext = @Ext, " &
                            "Mobile = @Mobile, " &
                            "Email = @Email, " &
                            "Address = @Address, " &
                            "Gender = @Gender " &
                            "WHERE Client_ID = @ClientID"

            Dim cmd As New MySqlCommand(sql, cn)
            cmd.Parameters.AddWithValue("@LastName", txtLastName.Text)
            cmd.Parameters.AddWithValue("@FirstName", txtFirstName.Text)
            cmd.Parameters.AddWithValue("@MiddleName", txtMiddleName.Text)
            cmd.Parameters.AddWithValue("@Ext", txtExt.Text)
            cmd.Parameters.AddWithValue("@Mobile", txtMobile.Text)
            cmd.Parameters.AddWithValue("@Email", txtEmail.Text)
            cmd.Parameters.AddWithValue("@Address", txtAddress.Text)
            cmd.Parameters.AddWithValue("@Gender", If(chkMale.Checked, "Male", "Female"))
            cmd.Parameters.AddWithValue("@ClientID", client_ID) ' 

            cmd.ExecuteNonQuery()
            MessageBox.Show("Client information updated successfully.")
            cn.Close()
            Me.Close()
        Catch ex As Exception
            MessageBox.Show("Database error: " & ex.Message)
        End Try
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub chkMale_CheckedChanged(sender As Object, e As EventArgs)
        If chkMale.Checked Then chkFemale.Checked = False
    End Sub

    Private Sub chkFemale_CheckedChanged(sender As Object, e As EventArgs)
        If chkFemale.Checked Then chkMale.Checked = False
    End Sub
End Class