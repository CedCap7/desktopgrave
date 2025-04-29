Imports MySql.Data.MySqlClient

Public Class frmDeceased
    Private isLoading As Boolean = True
    Private mainDashboard As dashboard
    Public ClientFirstName As String
    Public ClientMobile As String
    Public ClientEmail As String
    Public ClientGender As String

    Private clientSuggestions As DataTable
    Private selectedClientId As Integer = -1

    Public Sub New(dashboardForm As dashboard)
        InitializeComponent()
        mainDashboard = dashboardForm
    End Sub

    Private Sub frmDeceased_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        isLoading = True
        PopulateDateCombos()
        SetupClientSearchBox() ' Setup the client search box with suggestions
        isLoading = False
    End Sub

    ' --- COMBOBOX DATE VALIDATION HELPERS ---
    Private Function TryGetDateFromCombo(monthBox As ComboBox, dayBox As ComboBox, yearBox As ComboBox, ByRef resultDate As Date) As Boolean
        If monthBox.SelectedIndex = -1 OrElse dayBox.SelectedIndex = -1 OrElse yearBox.SelectedIndex = -1 Then
            Return False
        End If

        Dim dateString As String = $"{monthBox.SelectedItem} {dayBox.SelectedItem}, {yearBox.SelectedItem}"
        Return Date.TryParse(dateString, resultDate)
    End Function

    Private Sub ValidateDates()
        Dim birthDate As Date
        Dim deathDate As Date
        Dim intermentDate As Date

        ' Date of Birth must be valid
        If Not TryGetDateFromCombo(cmbBirthMonth, cmbBirthDay, cmbBirthYear, birthDate) Then
            MessageBox.Show("Please select a valid Date of Birth.", "Invalid Date", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        ' Date of Death (if selected)
        If TryGetDateFromCombo(cmbDeathMonth, cmbDeathDay, cmbDeathYear, deathDate) Then
            If deathDate < birthDate Then
                MessageBox.Show("Date of Death cannot be before Date of Birth.", "Invalid Date", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

            ' Interment Date (if selected)
            If TryGetDateFromCombo(cmbIntermentMonth, cmbIntermentDay, cmbIntermentYear, intermentDate) Then
                If intermentDate < deathDate Then
                    MessageBox.Show("Interment date cannot be before Date of Death.", "Invalid Date", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Exit Sub
                End If
            End If
        End If
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

        ' Populate Days
        For i = 1 To 31
            cmbBirthDay.Items.Add(i)
            cmbDeathDay.Items.Add(i)
            cmbIntermentDay.Items.Add(i)
        Next

        ' Populate Years
        For i = Date.Now.Year To 1900 Step -1
            cmbBirthYear.Items.Add(i)
            cmbDeathYear.Items.Add(i)
            cmbIntermentYear.Items.Add(i)
        Next
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


    ' --- CLIENT SEARCH LIVE SUGGESTIONS ---
    Private Sub txtClientSearch_TextChanged(sender As Object, e As EventArgs) Handles txtClientSearch.TextChanged
        If txtClientSearch.Text.Length < 1 Then
            lstClientSuggestions.Visible = False
            Return
        End If

        Dim input As String = txtClientSearch.Text.Trim()

        Try
            dbconn()
            cn.Open()
            sql = "SELECT Client_ID, " &
      "CONCAT(FirstName, ' ', " &
      "IF(MiddleName IS NULL OR MiddleName = '', '', CONCAT(LEFT(MiddleName,1), '. ')), " &
      "LastName) AS FullName " &
      "FROM client " &
      "WHERE CONCAT(FirstName, ' ', IF(MiddleName IS NULL OR MiddleName = '', '', CONCAT(LEFT(MiddleName,1), '. ')), LastName) LIKE @search " &
      "ORDER BY Client_ID DESC"

            Dim cmd As New MySqlCommand(sql, cn)
            cmd.Parameters.AddWithValue("@search", "%" & input & "%")

            Dim adapter As New MySqlDataAdapter(cmd)
            clientSuggestions = New DataTable()
            adapter.Fill(clientSuggestions)

            lstClientSuggestions.Items.Clear()

            For Each row As DataRow In clientSuggestions.Rows
                lstClientSuggestions.Items.Add(row("FullName").ToString())
            Next

            lstClientSuggestions.Visible = clientSuggestions.Rows.Count > 0

        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        Finally
            cn.Close()
        End Try
    End Sub

    ' --- CLIENT SUGGESTION SELECTION ---
    Private Sub lstClientSuggestions_Click(sender As Object, e As EventArgs) Handles lstClientSuggestions.Click
        Dim index As Integer = lstClientSuggestions.SelectedIndex

        If index >= 0 AndAlso index < clientSuggestions.Rows.Count Then
            txtClientSearch.Text = lstClientSuggestions.SelectedItem.ToString()
            selectedClientId = CInt(clientSuggestions.Rows(index)("Client_ID"))
            lstClientSuggestions.Visible = False
        End If
    End Sub


    ' --- BUTTON: ADD DECEASED ---
    Private Sub btnAddDeceased_Click(sender As Object, e As EventArgs) Handles btnAddDeceased.Click
        ValidateDates()

        If selectedClientId = -1 Then
            MessageBox.Show("Please select a valid client from the suggestions.")
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

        ' Deceased Date Validation
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

        Try
            ' Prepare Deceased record data
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
            Dim ClientId As Integer = selectedClientId

            dbconn()
            cn.Open()

            ' Begin transaction for deceased and beneficiaries insertion
            Using transaction As MySqlTransaction = cn.BeginTransaction()

                ' Insert Deceased record into the 'deceased' table
                sql = "INSERT INTO deceased (FirstName, MiddleName, LastName, Ext, DateOfBirth, DateofDeath, " &
                  "Gender, Religion, Interment, Relationship, Client_ID) " &
                  "VALUES (@FirstName, @MiddleName, @LastName, @Ext, @DateOfBirth, @DateOfDeath, " &
                  "@Gender, @Religion, @Interment, @Relationship, @ClientId)"

                Using cmd As New MySqlCommand(sql, cn, transaction)
                    cmd.Parameters.AddWithValue("@FirstName", FirstName)
                    cmd.Parameters.AddWithValue("@MiddleName", If(String.IsNullOrWhiteSpace(MiddleName), DBNull.Value, MiddleName))
                    cmd.Parameters.AddWithValue("@LastName", LastName)
                    cmd.Parameters.AddWithValue("@Ext", If(String.IsNullOrWhiteSpace(Ext), DBNull.Value, Ext))
                    cmd.Parameters.AddWithValue("@DateOfBirth", birthDate)
                    cmd.Parameters.AddWithValue("@DateOfDeath", deathDate)
                    cmd.Parameters.AddWithValue("@Gender", Gender)
                    cmd.Parameters.AddWithValue("@Religion", If(String.IsNullOrWhiteSpace(Religion), DBNull.Value, Religion))
                    cmd.Parameters.AddWithValue("@Interment", If(hasIntermentDate, intermentDate, DBNull.Value))
                    cmd.Parameters.AddWithValue("@Relationship", If(String.IsNullOrWhiteSpace(Relationship), DBNull.Value, Relationship))
                    cmd.Parameters.AddWithValue("@ClientId", ClientId)

                    cmd.ExecuteNonQuery()
                End Using

                ' Get the last inserted ID for the Deceased record (if AUTO_INCREMENT is used)
                Dim deceasedID As Integer = CInt(New MySqlCommand("SELECT LAST_INSERT_ID()", cn).ExecuteScalar())

                ' Now, insert beneficiaries data into the 'beneficiaries' table (if any beneficiaries are provided)
                If Not String.IsNullOrWhiteSpace(Beneficiary1) Then
                    InsertBeneficiary(deceasedID, ClientId, Beneficiary1, Contact1, 1, transaction)
                End If

                If Not String.IsNullOrWhiteSpace(Beneficiary2) Then
                    InsertBeneficiary(deceasedID, ClientId, Beneficiary2, Contact2, 2, transaction)
                End If

                transaction.Commit()

                MessageBox.Show("Deceased record and beneficiaries added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                ClearForm()

            End Using

        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            cn.Close()
        End Try
    End Sub



    ' --- INSERT BENEFICIARY FUNCTION ---
    Private Sub InsertBeneficiary(deceasedID As Integer, clientId As Integer, fullName As String, contact As String, order As Integer, transaction As MySqlTransaction)
        sql = "INSERT INTO beneficiaries (Deceased_ID, Client_ID, FullName, Contact, Date_Created, `Order`) " &
          "VALUES (@DeceasedID, @ClientId, @FullName, @Contact, NOW(), @Order)"

        Using cmd As New MySqlCommand(sql, cn, transaction)
            cmd.Parameters.AddWithValue("@DeceasedID", deceasedID)   ' Deceased ID
            cmd.Parameters.AddWithValue("@ClientId", clientId)
            cmd.Parameters.AddWithValue("@FullName", fullName)
            cmd.Parameters.AddWithValue("@Contact", If(String.IsNullOrWhiteSpace(contact), DBNull.Value, contact))
            cmd.Parameters.AddWithValue("@Order", order)
            cmd.ExecuteNonQuery()
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

        txtClientSearch.Clear()
    End Sub

    Private Sub btnGoBack_Click(sender As Object, e As EventArgs) Handles btnGoBack.Click
        Dim formInstance As New frmForms(mainDashboard)
        mainDashboard.subForm(formInstance)
    End Sub


End Class
