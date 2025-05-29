Imports MySql.Data.MySqlClient

Public Class frmApplication

    Private mainDashboard As dashboard ' Reference to the main dashboard

    ' Constructor to pass the main dashboard form
    Public Sub New(dashboardForm As dashboard)
        InitializeComponent()
        mainDashboard = dashboardForm
    End Sub

    Private Sub frmApplication_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Initialize auto-complete for search textbox
        InitializeSearchAutoComplete()
    End Sub

    Private Sub InitializeSearchAutoComplete()
        Try
            dbconn()
            cn.Open()

            ' Get all clients for auto-complete
            Dim sql As String = "SELECT Client_ID, CONCAT(LastName, ', ', FirstName, ' ', IFNULL(MiddleName,'')) as FullName FROM client"
            Using cmd As New MySqlCommand(sql, cn)
                Using dr As MySqlDataReader = cmd.ExecuteReader()
                    ' Create auto-complete collection
                    Dim autoComplete As New AutoCompleteStringCollection()

                    While dr.Read()
                        ' Add both ID and name to suggestions
                        autoComplete.Add(dr("Client_ID").ToString())
                        autoComplete.Add(dr("FullName").ToString())
                    End While

                    ' Set up auto-complete on the TextBox
                    txtSearch.AutoCompleteMode = AutoCompleteMode.SuggestAppend
                    txtSearch.AutoCompleteSource = AutoCompleteSource.CustomSource
                    txtSearch.AutoCompleteCustomSource = autoComplete
                End Using
            End Using

        Catch ex As Exception
            MessageBox.Show("Error initializing search suggestions: " & ex.Message)
        Finally
            If cn.State = ConnectionState.Open Then
                cn.Close()
            End If
        End Try
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        If Not String.IsNullOrWhiteSpace(txtSearch.Text) Then
            Try
                dbconn()
                cn.Open()

                ' Search as user types
                Dim searchText As String = txtSearch.Text.Trim()
                Dim sql As String = "SELECT * FROM client WHERE LastName LIKE @search OR FirstName LIKE @search OR Client_ID LIKE @search OR CONCAT(LastName, ', ', FirstName, ' ', IFNULL(MiddleName,'')) LIKE @search"

                Using cmd As New MySqlCommand(sql, cn)
                    cmd.Parameters.AddWithValue("@search", "%" & searchText & "%")
                    Using dr As MySqlDataReader = cmd.ExecuteReader()
                        If dr.Read() Then
                            ' Populate form fields with retrieved data
                            txtFirstName.Text = dr("FirstName").ToString()
                            txtMiddleName.Text = dr("MiddleName").ToString()
                            txtLastName.Text = dr("LastName").ToString()
                            txtExt.Text = dr("Ext").ToString()
                            txtAddress.Text = dr("Address").ToString()
                            txtMobile.Text = dr("Mobile").ToString()
                            txtEmail.Text = dr("Email").ToString()

                            ' Set gender checkbox
                            Dim gender As String = dr("Gender").ToString()
                            chkMale.Checked = (gender.ToLower() = "male")
                            chkFemale.Checked = (gender.ToLower() = "female")
                        End If
                    End Using
                End Using

            Catch ex As Exception
                ' Silently handle errors during typing
            Finally
                If cn.State = ConnectionState.Open Then
                    cn.Close()
                End If
            End Try
        End If
    End Sub

    ' Add search functionality
    Private Sub btnSearch_Click(sender As Object, e As EventArgs)
        Try
            dbconn()
            cn.Open()

            ' Search by name or ID
            Dim searchText As String = txtSearch.Text.Trim()
            Dim sql As String = "SELECT * FROM client WHERE LastName LIKE @search OR FirstName LIKE @search OR Client_ID LIKE @search"

            Using cmd As New MySqlCommand(sql, cn)
                cmd.Parameters.AddWithValue("@search", "%" & searchText & "%")
                Using dr As MySqlDataReader = cmd.ExecuteReader()
                    If dr.Read() Then
                        ' Populate form fields with retrieved data
                        txtFirstName.Text = dr("FirstName").ToString()
                        txtMiddleName.Text = dr("MiddleName").ToString()
                        txtLastName.Text = dr("LastName").ToString()
                        txtExt.Text = dr("Ext").ToString()
                        txtAddress.Text = dr("Address").ToString()
                        txtMobile.Text = dr("Mobile").ToString()
                        txtEmail.Text = dr("Email").ToString()

                        ' Set gender checkbox
                        Dim gender As String = dr("Gender").ToString()
                        chkMale.Checked = (gender.ToLower() = "male")
                        chkFemale.Checked = (gender.ToLower() = "female")

                        MessageBox.Show("Applicant information retrieved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Else
                        MessageBox.Show("No matching applicant found.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                End Using
            End Using

        Catch ex As Exception
            MessageBox.Show("Error retrieving applicant information: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If cn.State = ConnectionState.Open Then
                cn.Close()
            End If
        End Try
    End Sub

    Private Sub btnConfirm_Click(sender As Object, e As EventArgs) Handles btnConfirm.Click
        ' Gather input from the form
        Dim firstName As String = txtFirstName.Text
        Dim middleName As String = txtMiddleName.Text
        Dim lastName As String = txtLastName.Text
        Dim ext As String = txtExt.Text
        Dim address As String = txtAddress.Text
        Dim mobile As String = txtMobile.Text
        Dim email As String = txtEmail.Text
        Dim gender As String = If(chkMale.Checked, "Male", If(chkFemale.Checked, "Female", "Not Specified"))

        ' List to store validation errors
        Dim validationErrors As New List(Of String)

        ' Open connection first
        Try
            ' Ensure we have a connection
            If cn Is Nothing Then
                dbconn()
            End If

            If cn.State = ConnectionState.Closed Then
                cn.Open()
            End If

            ' Validate required fields
            If String.IsNullOrWhiteSpace(address) Then
                validationErrors.Add("• Address is required")
            End If

            If String.IsNullOrWhiteSpace(mobile) Then
                validationErrors.Add("• Mobile number is required")
            End If

            If Not chkMale.Checked AndAlso Not chkFemale.Checked Then
                validationErrors.Add("• Please select a gender")
            End If

            ' Validate mobile number format if provided
            If Not String.IsNullOrWhiteSpace(mobile) Then
                ' Clean the mobile number
                Dim cleanedMobile = mobile.Replace(" ", "").Replace("-", "")

                ' Check if mobile number is valid
                If cleanedMobile.Length <> 11 OrElse Not cleanedMobile.StartsWith("09") Then
                    validationErrors.Add("• Mobile number must start with '09' and be exactly 11 digits")
                Else
                    ' Check for duplicate mobile number
                    sql = "SELECT COUNT(*) FROM client WHERE Mobile = @mobile"
                    Using cmd As New MySqlCommand(sql, cn)
                        cmd.Parameters.AddWithValue("@mobile", cleanedMobile)
                        Dim mobileCount As Integer = CInt(cmd.ExecuteScalar())
                        If mobileCount > 0 Then
                            validationErrors.Add("• This mobile number is already registered")
                        End If
                    End Using
                    ' Use the cleaned mobile number for insertion
                    mobile = cleanedMobile
                End If
            End If

            ' Check for duplicate email if provided
            If Not String.IsNullOrWhiteSpace(email) Then
                sql = "SELECT COUNT(*) FROM client WHERE Email = @email"
                Using cmd As New MySqlCommand(sql, cn)
                    cmd.Parameters.AddWithValue("@email", email)
                    Dim emailCount As Integer = CInt(cmd.ExecuteScalar())
                    If emailCount > 0 Then
                        validationErrors.Add("• This email address is already registered")
                    End If
                End Using
            End If

            ' Check for duplicate names
            sql = "SELECT COUNT(*) FROM client WHERE FirstName = @firstName AND LastName = @lastName AND (MiddleName = @middleName OR (MiddleName IS NULL AND @middleName IS NULL))"
            Using cmd As New MySqlCommand(sql, cn)
                cmd.Parameters.AddWithValue("@firstName", firstName)
                cmd.Parameters.AddWithValue("@lastName", lastName)
                cmd.Parameters.AddWithValue("@middleName", If(String.IsNullOrWhiteSpace(middleName), DBNull.Value, middleName))
                Dim nameCount As Integer = CInt(cmd.ExecuteScalar())
                If nameCount > 0 Then
                    validationErrors.Add("• A client with this name already exists")
                End If
            End Using

            ' If there are any validation errors, show them all at once
            If validationErrors.Count > 0 Then
                Dim errorMessage As String = "Please correct the following:" & vbCrLf & vbCrLf & String.Join(vbCrLf, validationErrors)
                MessageBox.Show(errorMessage, "Validation Errors", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

        Catch ex As Exception
            MessageBox.Show("Error validating input: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        Finally
            ' Close connection after validation
            If cn IsNot Nothing AndAlso cn.State = ConnectionState.Open Then
                cn.Close()
            End If
        End Try

        ' If validation passed, proceed with insertion
        Try
            ' Reopen connection for insertion
            If cn Is Nothing Then
                dbconn()
            End If

            If cn.State = ConnectionState.Closed Then
                cn.Open()
            End If

            Dim clientId As Integer = 0 ' Variable to store the inserted Client_ID

            sql = "INSERT INTO client (FirstName, MiddleName, LastName, Ext, Address, Mobile, Gender, Email) VALUES (@firstName, @middleName, @lastName, @ext, @address, @mobile, @gender, @Email)"
            Using cmd As New MySqlCommand(sql, cn)
                ' Add parameters
                cmd.Parameters.AddWithValue("@firstName", firstName)
                cmd.Parameters.AddWithValue("@middleName", middleName)
                cmd.Parameters.AddWithValue("@lastName", lastName)
                cmd.Parameters.AddWithValue("@ext", ext)
                cmd.Parameters.AddWithValue("@address", address)
                cmd.Parameters.AddWithValue("@mobile", mobile)
                cmd.Parameters.AddWithValue("@gender", gender)
                cmd.Parameters.AddWithValue("@Email", email)

                ' Execute the query
                cmd.ExecuteNonQuery()
                LogUserAction("Add Client", "Added client: " & firstName & " " & lastName)

                ' Retrieve the last inserted Client_ID
                clientId = CInt(cmd.LastInsertedId)
            End Using

            ' Set the status to 1 in the client table or any other table based on the logic you want
            ' Assuming you need to set status to 1 in the client table
            Dim statusUpdateQuery As String = "UPDATE client SET Status = 1 WHERE Client_ID = @ClientID"
            Using cmdStatus As New MySqlCommand(statusUpdateQuery, cn)
                cmdStatus.Parameters.AddWithValue("@ClientID", clientId)
                cmdStatus.ExecuteNonQuery()
            End Using

            ' Display a confirmation message after saving the data
            MessageBox.Show("Client information saved successfully!", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As Exception
            MessageBox.Show("Error adding client: " & ex.Message)
            Exit Sub ' Exit to prevent opening the next form if an error occurs
        Finally
            If cn IsNot Nothing AndAlso cn.State = ConnectionState.Open Then
                cn.Close()
            End If
        End Try

        ' Close and dispose of the current form without opening another form
        Me.Close()
        Me.Dispose()
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        ' Simply close and dispose the form without any record or action
        Me.Close()
        Me.Dispose()
    End Sub

    ' Checkbox toggle logic
    Private Sub chkMale_CheckedChanged(sender As Object, e As EventArgs) Handles chkMale.CheckedChanged
        If chkMale.Checked Then chkFemale.Checked = False
    End Sub

    Private Sub chkFemale_CheckedChanged(sender As Object, e As EventArgs) Handles chkFemale.CheckedChanged
        If chkFemale.Checked Then chkMale.Checked = False
    End Sub
End Class
