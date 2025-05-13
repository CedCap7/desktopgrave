Imports MySql.Data.MySqlClient

Public Class frmEditUser
    Private userId As Integer ' Store the selected user ID

    ' Constructor that accepts user ID
    Public Sub New(userId As Integer)
        InitializeComponent()
        Me.userId = userId
        LoadUserData(userId)
    End Sub

    Private Sub LoadUserData(userId As Integer)
        Try
            dbconn()
            If cn.State = ConnectionState.Closed Then cn.Open()

            sql = "SELECT * FROM user WHERE User_ID = @userId"
            Using cmd As New MySqlCommand(sql, cn)
                cmd.Parameters.AddWithValue("@userId", userId)
                Using dr As MySqlDataReader = cmd.ExecuteReader()
                    If dr.Read() Then
                        txtFirstName.Text = dr("F_name").ToString()
                        txtMiddleName.Text = If(dr("M_name") Is DBNull.Value, "", dr("M_name").ToString())
                        txtLastName.Text = dr("L_name").ToString()
                        txtEmail.Text = If(dr("Email") Is DBNull.Value, "", dr("Email").ToString())
                        txtMobile.Text = If(dr("Contact_info") Is DBNull.Value, "", dr("Contact_info").ToString())
                        txtUsername.Text = dr("Username").ToString()
                        txtPassword.Text = dr("Password").ToString()
                        ' Don't show password in clear text for security reasons
                    Else
                        MessageBox.Show("User not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Me.Close()
                    End If
                End Using
            End Using

        Catch ex As Exception
            MessageBox.Show("Error loading user data: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.Close()
        Finally
            If cn IsNot Nothing AndAlso cn.State = ConnectionState.Open Then
                cn.Close()
            End If
        End Try
    End Sub

    Private Sub btnConfirm_Click(sender As Object, e As EventArgs) Handles btnConfirm.Click
        ' Gather input from the form
        Dim firstName As String = txtFirstName.Text.Trim()
        Dim middleName As String = txtMiddleName.Text.Trim()
        Dim lastName As String = txtLastName.Text.Trim()
        Dim email As String = txtEmail.Text.Trim()
        Dim mobile As String = txtMobile.Text.Trim()
        Dim username As String = txtUsername.Text.Trim()
        Dim password As String = txtPassword.Text.Trim()

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

            ' Update user data
            sql = "UPDATE user SET " &
                  "F_name = @firstName, " &
                  "M_name = @middleName, " &
                  "L_name = @lastName, " &
                  "Username = @username, " &
                  "Password = @password, " &
                  "Contact_info = @contact, " &
                  "Email = @email " &
                  "WHERE User_ID = @userId"

            Using cmd As New MySqlCommand(sql, cn)
                ' Add parameters
                cmd.Parameters.AddWithValue("@firstName", firstName)
                cmd.Parameters.AddWithValue("@middleName", If(String.IsNullOrWhiteSpace(middleName), DBNull.Value, middleName))
                cmd.Parameters.AddWithValue("@lastName", lastName)
                cmd.Parameters.AddWithValue("@username", username)
                cmd.Parameters.AddWithValue("@password", password)
                cmd.Parameters.AddWithValue("@contact", If(String.IsNullOrWhiteSpace(mobile), DBNull.Value, mobile))
                cmd.Parameters.AddWithValue("@email", If(String.IsNullOrWhiteSpace(email), DBNull.Value, email))
                cmd.Parameters.AddWithValue("@userId", userId)

                ' Execute the update
                cmd.ExecuteNonQuery()

                ' Display success message
                MessageBox.Show("User information updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

                ' Close the form
                Me.Close()

            End Using

        Catch ex As Exception
            MessageBox.Show("Error updating user: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If cn IsNot Nothing AndAlso cn.State = ConnectionState.Open Then
                cn.Close()
            End If
        End Try
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub
End Class