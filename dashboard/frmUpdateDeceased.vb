Imports MySql.Data.MySqlClient

Public Class frmUpdateDeceased
    Private deceasedID As Integer
    Private clientSuggestions As DataTable
    Private selectedClientId As Integer = -1

    Public Sub New(selectedID As Integer)
        InitializeComponent()
        deceasedID = selectedID
    End Sub

    Private Sub frmUpdateDeceased_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        PopulateDateCombos()
        SetupClientSearchBox() ' Setup the client search box with suggestions
        LoadDeceasedData() ' <-- ONLY call LoadDeceasedData AFTER populating combos!

        ' Initially hide the list of client suggestions
        lstClientSuggestions.Visible = False
    End Sub

    ' --- COMBOBOX POPULATION ---
    Private Sub PopulateDateCombos()
        ' Add blank option first
        cmbBirthMonth.Items.Add("") : cmbDeathMonth.Items.Add("") : cmbIntermentMonth.Items.Add("")
        cmbBirthDay.Items.Add("") : cmbDeathDay.Items.Add("") : cmbIntermentDay.Items.Add("")
        cmbBirthYear.Items.Add("") : cmbDeathYear.Items.Add("") : cmbIntermentYear.Items.Add("")

        ' Populate Months
        Dim months = System.Globalization.DateTimeFormatInfo.InvariantInfo.MonthNames
        For i = 0 To 11
            If months(i) <> "" Then
                cmbBirthMonth.Items.Add(months(i))
                cmbDeathMonth.Items.Add(months(i))
                cmbIntermentMonth.Items.Add(months(i))
            End If
        Next

        ' Populate Days (always string)
        For i = 1 To 31
            cmbBirthDay.Items.Add(i.ToString())
            cmbDeathDay.Items.Add(i.ToString())
            cmbIntermentDay.Items.Add(i.ToString())
        Next

        ' Populate Years (always string)
        For i = Date.Now.Year To 1900 Step -1
            cmbBirthYear.Items.Add(i.ToString())
            cmbDeathYear.Items.Add(i.ToString())
            cmbIntermentYear.Items.Add(i.ToString())
        Next
    End Sub

    Private Sub txtClientSearch_TextChanged(sender As Object, e As EventArgs) Handles txtClientSearch.TextChanged
        ' Only show the suggestions if the user typed more than 1 character
        If txtClientSearch.Text.Length < 2 Then
            lstClientSuggestions.Visible = False
            Return
        End If

        Dim input As String = txtClientSearch.Text.Trim()

        Try
            ' Ensure the connection is closed before opening it
            If cn.State = ConnectionState.Open Then
                cn.Close()  ' Close the connection if already open
            End If

            dbconn()
            cn.Open()

            ' SQL query to search for client based on full name
            sql = "SELECT Client_ID, " &
              "CONCAT(FirstName, ' ', " &
              "IF(MiddleName IS NULL OR MiddleName = '', '', CONCAT(LEFT(MiddleName,1), '. ')), " &
              "LastName) AS FullName " &
              "FROM client WHERE CONCAT(FirstName, ' ', IF(MiddleName IS NULL OR MiddleName = '', '', CONCAT(LEFT(MiddleName,1), '. ')), LastName) LIKE @search ORDER BY Client_ID DESC"

            Dim cmd As New MySqlCommand(sql, cn)
            cmd.Parameters.AddWithValue("@search", "%" & input & "%")

            Dim adapter As New MySqlDataAdapter(cmd)
            clientSuggestions = New DataTable()
            adapter.Fill(clientSuggestions)

            lstClientSuggestions.Items.Clear()

            For Each row As DataRow In clientSuggestions.Rows
                ' Format the full name with middle name as initial and period, if available
                Dim fullName As String = row("FullName").ToString()
                lstClientSuggestions.Items.Add(fullName)
            Next

            ' Show the suggestions only if there are results
            lstClientSuggestions.Visible = clientSuggestions.Rows.Count > 0

        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        Finally
            If cn.State = ConnectionState.Open Then
                cn.Close()  ' Always close the connection after use
            End If
        End Try
    End Sub

    Private Sub lstClientSuggestions_Click(sender As Object, e As EventArgs) Handles lstClientSuggestions.Click
        ' Ensure a valid index is selected
        If lstClientSuggestions.SelectedIndex >= 0 Then
            Dim selectedIndex As Integer = lstClientSuggestions.SelectedIndex

            ' Ensure the index is within the valid range of clientSuggestions
            If selectedIndex >= 0 AndAlso selectedIndex < clientSuggestions.Rows.Count Then
                ' Get the FullName and Client_ID from clientSuggestions
                txtClientSearch.Text = lstClientSuggestions.SelectedItem.ToString()
                selectedClientId = CInt(clientSuggestions.Rows(selectedIndex)("Client_ID"))

                ' Hide the suggestions after selection
                lstClientSuggestions.Visible = False
            End If
        End If
    End Sub

    ' --- CLIENT SEARCH BOX SETUP ---
    Private Sub SetupClientSearchBox()
        ' Just configure existing controls
        txtClientSearch.PlaceholderText = "Search Client..."
        txtClientSearch.BringToFront()

        lstClientSuggestions.Width = txtClientSearch.Width
        lstClientSuggestions.Height = 100
        lstClientSuggestions.Top = txtClientSearch.Bottom
        lstClientSuggestions.Left = txtClientSearch.Left
        lstClientSuggestions.Visible = False
    End Sub


    ' --- COMBOBOX DATE VALIDATION HELPERS ---
    Private Function TryGetDateFromCombo(monthBox As ComboBox, dayBox As ComboBox, yearBox As ComboBox, ByRef resultDate As Date) As Boolean
        If monthBox.SelectedIndex = -1 OrElse dayBox.SelectedIndex = -1 OrElse yearBox.SelectedIndex = -1 Then
            Return False
        End If

        Dim dateString As String = $"{monthBox.SelectedItem} {dayBox.SelectedItem}, {yearBox.SelectedItem}"
        Return Date.TryParse(dateString, resultDate)
    End Function

    ' --- LOAD DECEASED DATA ---
    Private Sub LoadDeceasedData()
        Try
            ' First, ensure the connection is open
            If cn.State = ConnectionState.Closed Then
                dbconn()  ' Ensure the database connection is properly initialized
                cn.Open() ' Open the connection
            End If

            ' Query to fetch deceased data, excluding client info initially
            sql = "SELECT d.Deceased_ID, d.Client_ID, " &
              "d.FirstName AS DeceasedFirstName, d.MiddleName AS DeceasedMiddleName, " &
              "d.LastName AS DeceasedLastName, d.DateOfBirth, d.DateOfDeath, d.Interment, d.Gender, d.Ext, d.Religion, " &
              "b1.FullName AS Beneficiary1FullName, b1.Contact AS Beneficiary1Contact, " &
              "b2.FullName AS Beneficiary2FullName, b2.Contact AS Beneficiary2Contact " &
              "FROM deceased d " &
              "LEFT JOIN beneficiaries b1 ON b1.Deceased_ID = d.Deceased_ID AND b1.Order = 1 " &
              "LEFT JOIN beneficiaries b2 ON b2.Deceased_ID = d.Deceased_ID AND b2.Order = 2 " &
              "WHERE d.Deceased_ID = @DeceasedID"

            ' Prepare command and add parameters
            Dim cmd As New MySqlCommand(sql, cn)
            cmd.Parameters.AddWithValue("@DeceasedID", deceasedID)

            ' Execute the query for deceased data
            Dim reader As MySqlDataReader = cmd.ExecuteReader()

            ' Check if we got rows back from the query
            If reader.HasRows Then
                reader.Read()  ' Move to the first row

                ' Store Client_ID for later use
                Dim storedClientId As Object = reader("Client_ID")

                ' Populate the form with deceased data
                txtDeceasedFirstName.Text = If(IsDBNull(reader("DeceasedFirstName")), "", reader("DeceasedFirstName").ToString())
                txtDeceasedMiddleName.Text = If(IsDBNull(reader("DeceasedMiddleName")), "", reader("DeceasedMiddleName").ToString())
                txtDeceasedLastName.Text = If(IsDBNull(reader("DeceasedLastName")), "", reader("DeceasedLastName").ToString())
                txtDeceasedExt.Text = If(IsDBNull(reader("Ext")), "", reader("Ext").ToString())
                txtReligion.Text = If(IsDBNull(reader("Religion")), "", reader("Religion").ToString())

                ' Handle Date of Birth
                If Not IsDBNull(reader("DateOfBirth")) Then
                    Dim birthDate As Date = CDate(reader("DateOfBirth"))
                    cmbBirthMonth.SelectedItem = birthDate.ToString("MMMM")
                    cmbBirthDay.SelectedItem = birthDate.Day.ToString()
                    cmbBirthYear.SelectedItem = birthDate.Year.ToString()
                End If

                ' Handle Date of Death
                If Not IsDBNull(reader("DateOfDeath")) Then
                    Dim deathDate As Date = CDate(reader("DateOfDeath"))
                    cmbDeathMonth.SelectedItem = deathDate.ToString("MMMM")
                    cmbDeathDay.SelectedItem = deathDate.Day.ToString()
                    cmbDeathYear.SelectedItem = deathDate.Year.ToString()
                End If

                ' Handle Interment Date
                If Not IsDBNull(reader("Interment")) Then
                    Dim intermentDate As Date = CDate(reader("Interment"))
                    cmbIntermentMonth.SelectedItem = intermentDate.ToString("MMMM")
                    cmbIntermentDay.SelectedItem = intermentDate.Day.ToString()
                    cmbIntermentYear.SelectedItem = intermentDate.Year.ToString()
                End If

                ' Handle Gender
                If reader("Gender").ToString() = "Male" Then
                    chkMaleDeceased.Checked = True
                    chkFemaleDeceased.Checked = False
                ElseIf reader("Gender").ToString() = "Female" Then
                    chkFemaleDeceased.Checked = True
                    chkMaleDeceased.Checked = False
                End If

                ' Handle Beneficiaries - Store separately
                txtBeneficiary1.Text = If(IsDBNull(reader("Beneficiary1FullName")), "", reader("Beneficiary1FullName").ToString())
                txtBeneficiaryContact1.Text = If(IsDBNull(reader("Beneficiary1Contact")), "", reader("Beneficiary1Contact").ToString())

                txtBeneficiary2.Text = If(IsDBNull(reader("Beneficiary2FullName")), "", reader("Beneficiary2FullName").ToString())
                txtBeneficiaryContact2.Text = If(IsDBNull(reader("Beneficiary2Contact")), "", reader("Beneficiary2Contact").ToString())

                ' Close the reader before making another query
                reader.Close()

                ' Now handle the client information
                If Not IsDBNull(storedClientId) AndAlso storedClientId IsNot Nothing Then
                    ' Use ExecuteScalar to fetch the client's full name
                    Dim clientCmd As New MySqlCommand("SELECT CONCAT(IFNULL(FirstName, ''), ' ', IFNULL(LEFT(MiddleName,1), ''), '. ', IFNULL(LastName, '')) AS FullName FROM client WHERE Client_ID = @ClientID", cn)
                    clientCmd.Parameters.AddWithValue("@ClientID", storedClientId)

                    ' Execute the query for client data
                    Dim clientFullName As Object = clientCmd.ExecuteScalar()

                    ' If clientFullName is not null, set the text; otherwise, set to "Unknown Client"
                    txtClientSearch.Text = If(clientFullName IsNot Nothing AndAlso Not IsDBNull(clientFullName), clientFullName.ToString(), "Unknown Client")
                Else
                    txtClientSearch.Text = "Unknown Client"
                End If

            Else
                ' If no rows found, handle accordingly
                MessageBox.Show("No data found for the specified Deceased ID.", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

        Catch ex As MySqlException
            ' Display MySQL specific errors
            MessageBox.Show("MySQL Error: " & ex.Message & vbCrLf & "Error Code: " & ex.ErrorCode.ToString())
        Catch ex As Exception
            ' General error handling
            MessageBox.Show("General Error: " & ex.Message)
        Finally
            ' Ensure connection is closed if it's still open
            If cn.State = ConnectionState.Open Then
                cn.Close()
            End If
        End Try
    End Sub




    ' --- HELPER FUNCTIONS ---
    Private Function FindDayIndex(cmb As ComboBox, dayValue As Integer) As Integer
        For i As Integer = 0 To cmb.Items.Count - 1
            If cmb.Items(i).ToString() = dayValue.ToString() Then
                Return i
            End If
        Next
        Return -1
    End Function

    Private Function FindYearIndex(cmb As ComboBox, yearValue As Integer) As Integer
        For i As Integer = 0 To cmb.Items.Count - 1
            If cmb.Items(i).ToString() = yearValue.ToString() Then
                Return i
            End If
        Next
        Return -1
    End Function


    Private Function ValidateDates() As Boolean
        Dim birthDate As Date
        Dim deathDate As Date
        Dim intermentDate As Date

        ' Date of Birth must be valid
        If Not TryGetDateFromCombo(cmbBirthMonth, cmbBirthDay, cmbBirthYear, birthDate) Then
            MessageBox.Show("Please select a valid Date of Birth.", "Invalid Date", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End If

        ' Date of Death (if selected)
        If TryGetDateFromCombo(cmbDeathMonth, cmbDeathDay, cmbDeathYear, deathDate) Then
            If deathDate < birthDate Then
                MessageBox.Show("Date of Death cannot be before Date of Birth.", "Invalid Date", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return False
            End If

            ' Interment Date (if selected)
            If TryGetDateFromCombo(cmbIntermentMonth, cmbIntermentDay, cmbIntermentYear, intermentDate) Then
                If intermentDate < deathDate Then
                    MessageBox.Show("Interment date cannot be before Date of Death.", "Invalid Date", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Return False
                End If
            End If
        End If

        Return True
    End Function


    ' --- BUTTON: UPDATE DECEASED ---
    Private Sub btnUpdateDeceased_Click(sender As Object, e As EventArgs) Handles btnUpdateDeceased.Click
        ' Validate the input fields before proceeding
        If Not ValidateDates() Then
            Return
        End If

        If String.IsNullOrWhiteSpace(txtDeceasedFirstName.Text.Trim()) Then
            MessageBox.Show("Please enter First Name.", "Required Field", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If String.IsNullOrWhiteSpace(txtDeceasedLastName.Text.Trim()) Then
            MessageBox.Show("Please enter Last Name.", "Required Field", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If Not chkMaleDeceased.Checked AndAlso Not chkFemaleDeceased.Checked Then
            MessageBox.Show("Please select Gender.", "Required Field", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim birthDate, deathDate, intermentDate As Date
        If Not TryGetDateFromCombo(cmbBirthMonth, cmbBirthDay, cmbBirthYear, birthDate) Then
            MessageBox.Show("Please select a valid Date of Birth.", "Required Field", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If Not TryGetDateFromCombo(cmbDeathMonth, cmbDeathDay, cmbDeathYear, deathDate) Then
            MessageBox.Show("Please select a valid Date of Death.", "Required Field", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' Optional: Interment date
        Dim hasIntermentDate As Boolean = TryGetDateFromCombo(cmbIntermentMonth, cmbIntermentDay, cmbIntermentYear, intermentDate)

        Dim transaction As MySqlTransaction = Nothing

        Try
            ' Prepare the values to update the database
            Dim FirstName As String = txtDeceasedFirstName.Text.Trim()
            Dim MiddleName As String = txtDeceasedMiddleName.Text.Trim()
            Dim LastName As String = txtDeceasedLastName.Text.Trim()
            Dim Ext As String = txtDeceasedExt.Text.Trim()
            Dim Gender As String = If(chkMaleDeceased.Checked, "Male", "Female")
            Dim Religion As String = txtReligion.Text.Trim()
            Dim Beneficiary1 As String = txtBeneficiary1.Text.Trim()
            Dim Beneficiary2 As String = txtBeneficiary2.Text.Trim()
            Dim Contact1 As String = txtBeneficiaryContact1.Text.Trim()
            Dim Contact2 As String = txtBeneficiaryContact2.Text.Trim()
            Dim Relationship As String = txtRelationship.Text.Trim()

            dbconn()
            cn.Open()
            transaction = cn.BeginTransaction()

            ' Update the Deceased record
            sql = "UPDATE deceased SET " &
                  "FirstName = @FirstName, " &
                  "MiddleName = @MiddleName, " &
                  "LastName = @LastName, " &
                  "Ext = @Ext, " &
                  "DateOfBirth = @DateOfBirth, " &
                  "DateOfDeath = @DateOfDeath, " &
                  "Gender = @Gender, " &
                  "Religion = @Religion, " &
                  "Interment = @Interment, " &
                  "Client_ID = @ClientID " &
                  "WHERE Deceased_ID = @ID"

            Using cmd As New MySqlCommand(sql, cn, transaction)
                cmd.Parameters.AddWithValue("@ID", deceasedID)
                cmd.Parameters.AddWithValue("@FirstName", FirstName)
                cmd.Parameters.AddWithValue("@MiddleName", If(String.IsNullOrWhiteSpace(MiddleName), DBNull.Value, MiddleName))
                cmd.Parameters.AddWithValue("@LastName", LastName)
                cmd.Parameters.AddWithValue("@Ext", If(String.IsNullOrWhiteSpace(Ext), DBNull.Value, Ext))
                cmd.Parameters.AddWithValue("@DateOfBirth", birthDate)
                cmd.Parameters.AddWithValue("@DateOfDeath", deathDate)
                cmd.Parameters.AddWithValue("@Gender", Gender)
                cmd.Parameters.AddWithValue("@Religion", If(String.IsNullOrWhiteSpace(Religion), DBNull.Value, Religion))
                cmd.Parameters.AddWithValue("@Interment", If(hasIntermentDate, intermentDate, DBNull.Value))
                cmd.Parameters.AddWithValue("@ClientID", If(selectedClientId = -1, DBNull.Value, selectedClientId))

                cmd.ExecuteNonQuery()
                LogUserAction("Update Deceased", "Updated deceased ID: " & deceasedID)
            End Using

            ' Update Beneficiaries if any changes
            If Not String.IsNullOrWhiteSpace(Beneficiary1) Then
                UpdateBeneficiary(deceasedID, Beneficiary1, Contact1, 1, transaction)
            End If

            If Not String.IsNullOrWhiteSpace(Beneficiary2) Then
                UpdateBeneficiary(deceasedID, Beneficiary2, Contact2, 2, transaction)
            End If

            transaction.Commit()
            MessageBox.Show("Deceased record updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

            ' Close the form
            Me.Close()

        Catch ex As Exception
            If transaction IsNot Nothing Then
                Try
                    transaction.Rollback()
                Catch rollbackEx As Exception
                    MessageBox.Show("Rollback Error: " & rollbackEx.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End If
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If cn.State = ConnectionState.Open Then
                cn.Close()
            End If
        End Try
    End Sub

    ' --- UPDATE BENEFICIARY FUNCTION ---
    Private Sub UpdateBeneficiary(deceasedID As Integer, fullName As String, contact As String, order As Integer, transaction As MySqlTransaction)
        ' First check if the beneficiary exists
        sql = "SELECT id FROM beneficiaries WHERE Deceased_ID = @DeceasedID AND `Order` = @Order"

        Using checkCmd As New MySqlCommand(sql, cn, transaction)
            checkCmd.Parameters.AddWithValue("@DeceasedID", deceasedID)
            checkCmd.Parameters.AddWithValue("@Order", order)

            Dim existingId As Object = checkCmd.ExecuteScalar()

            If existingId IsNot Nothing AndAlso Not IsDBNull(existingId) Then
                ' Update existing record
                sql = "UPDATE beneficiaries SET " &
                     "FullName = @FullName, " &
                     "Contact = @Contact, " &
                     "Client_ID = @ClientID, " &
                     "status = 1 " &
                     "WHERE id = @ID"

                Using cmd As New MySqlCommand(sql, cn, transaction)
                    cmd.Parameters.AddWithValue("@ID", existingId)
                    cmd.Parameters.AddWithValue("@FullName", fullName)
                    cmd.Parameters.AddWithValue("@Contact", If(String.IsNullOrWhiteSpace(contact), DBNull.Value, contact))
                    cmd.Parameters.AddWithValue("@ClientID", If(selectedClientId = -1, DBNull.Value, selectedClientId))
                    cmd.ExecuteNonQuery()
                End Using
            Else
                ' Insert new record
                sql = "INSERT INTO beneficiaries (" &
                      "Deceased_ID, Client_ID, FullName, date_created, `Order`, Contact, status" &
                      ") VALUES (" &
                      "@DeceasedID, @ClientID, @FullName, NOW(), @Order, @Contact, 1)"

                Using cmd As New MySqlCommand(sql, cn, transaction)
                    cmd.Parameters.AddWithValue("@DeceasedID", deceasedID)
                    cmd.Parameters.AddWithValue("@ClientID", If(selectedClientId = -1, DBNull.Value, selectedClientId))
                    cmd.Parameters.AddWithValue("@FullName", fullName)
                    cmd.Parameters.AddWithValue("@Order", order)
                    cmd.Parameters.AddWithValue("@Contact", If(String.IsNullOrWhiteSpace(contact), DBNull.Value, contact))
                    cmd.ExecuteNonQuery()
                End Using
            End If
        End Using
    End Sub

    ' --- CLEAR FORM ---
    Private Sub ClearForm()
        txtDeceasedFirstName.Clear()
        txtDeceasedMiddleName.Clear()
        txtDeceasedLastName.Clear()
        txtDeceasedExt.Clear()
        txtReligion.Clear()
        txtBeneficiary1.Clear()
        txtBeneficiary2.Clear()
        txtRelationship.Clear()

        chkMaleDeceased.Checked = False
        chkFemaleDeceased.Checked = False

        cmbBirthMonth.SelectedIndex = -1
        cmbBirthDay.SelectedIndex = -1
        cmbBirthYear.SelectedIndex = -1

        cmbDeathMonth.SelectedIndex = -1
        cmbDeathDay.SelectedIndex = -1
        cmbDeathYear.SelectedIndex = -1

        cmbIntermentMonth.SelectedIndex = -1
        cmbIntermentDay.SelectedIndex = -1
        cmbIntermentYear.SelectedIndex = -1
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        ' Close form and go back to the previous screen
        Me.Close()
    End Sub
End Class
