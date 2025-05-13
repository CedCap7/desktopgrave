Imports MySql.Data.MySqlClient

Public Class frmAddUser

    Private Sub btnConfirm_Click(sender As Object, e As EventArgs) Handles btnConfirm.Click
        ' Gather input from the form
        Dim firstName As String = txtFirstName.Text.Trim()
        Dim middleName As String = txtMiddleName.Text.Trim()
        Dim lastName As String = txtLastName.Text.Trim()
        Dim email As String = txtEmail.Text.Trim()
        Dim mobile As String = txtMobile.Text.Trim()
        Dim username As String = txtUsername.Text.Trim()
        Dim password As String = txtPassword.Text.Trim()
        Dim userTypeID As Integer = 2 ' As specified in the requirements

        ' Validate required fields
        If String.IsNullOrWhiteSpace(firstName) Then
            MessageBox.Show("Please enter a first name.", "Missing Required Field", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If String.IsNullOrWhiteSpace(lastName) Then
            MessageBox.Show("Please enter a last name.", "Missing Required Field", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If String.IsNullOrWhiteSpace(username) Then
            MessageBox.Show("Please enter a username.", "Missing Required Field", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If String.IsNullOrWhiteSpace(password) Then
            MessageBox.Show("Please enter a password.", "Missing Required Field", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' Validate mobile number format (optional but if provided must be valid)
        If Not String.IsNullOrWhiteSpace(mobile) Then
            If mobile.Length <> 11 OrElse Not mobile.StartsWith("09") Then
                MessageBox.Show("Please enter a valid Philippine mobile number starting with '09' and exactly 11 digits.", "Invalid Mobile Number", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If
        End If

        Try
            ' Open database connection
            dbconn()
            If cn.State = ConnectionState.Closed Then
                cn.Open()
            End If

            ' Insert into user table
            sql = "INSERT INTO user (F_name, M_name, L_name, Username, Password, Contact_info, Email, User_TypeID) " &
                  "VALUES (@firstName, @middleName, @lastName, @username, @password, @contact, @email, @userTypeID)"

            Using cmd As New MySqlCommand(sql, cn)
                ' Add parameters
                cmd.Parameters.AddWithValue("@firstName", firstName)
                cmd.Parameters.AddWithValue("@middleName", If(String.IsNullOrWhiteSpace(middleName), DBNull.Value, middleName))
                cmd.Parameters.AddWithValue("@lastName", lastName)
                cmd.Parameters.AddWithValue("@username", username)
                cmd.Parameters.AddWithValue("@password", password) ' Note: In production, you should hash the password
                cmd.Parameters.AddWithValue("@contact", If(String.IsNullOrWhiteSpace(mobile), DBNull.Value, mobile))
                cmd.Parameters.AddWithValue("@email", If(String.IsNullOrWhiteSpace(email), DBNull.Value, email))
                cmd.Parameters.AddWithValue("@userTypeID", userTypeID)

                ' Execute the query
                cmd.ExecuteNonQuery()

                ' Display success message
                MessageBox.Show("User added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

                ' Clear form fields
                ClearForm()

            End Using

        Catch ex As Exception
            MessageBox.Show("Error adding user: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If cn IsNot Nothing AndAlso cn.State = ConnectionState.Open Then
                cn.Close()
            End If
        End Try
    End Sub

    Private Sub ClearForm()
        txtFirstName.Clear()
        txtMiddleName.Clear()
        txtLastName.Clear()
        txtEmail.Clear()
        txtMobile.Clear()
        txtUsername.Clear()
        txtPassword.Clear()
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

End Class