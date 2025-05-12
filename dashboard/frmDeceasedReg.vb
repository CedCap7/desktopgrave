Imports MySql.Data.MySqlClient
Imports System.IO
Imports Microsoft.Office.Interop.Excel
Imports System.Runtime.InteropServices
Imports System.Diagnostics
Imports System.Globalization

Public Class frmDeceasedReg

    Private Sub frmDeceasedReg_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        KeyPreview = True
        LoadUsers()

        ' Create the context menu for status updates
        Dim contextMenu As New ContextMenuStrip()
        contextMenu.Items.Add("Remaining", Nothing, Sub() UpdateStatus("Remaining"))
        contextMenu.Items.Add("Relocated", Nothing, Sub() UpdateStatus("Relocated"))
        contextMenu.Items.Add("Renewal", Nothing, Sub() UpdateStatus("Renewal"))
        contextMenu.Items.Add("Pending", Nothing, Sub() UpdateStatus("Pending"))
        contextMenu.Items.Add("Expired", Nothing, Sub() UpdateStatus("Expired"))

        ' Assign the context menu to the ListView
        DeceasedList.ContextMenuStrip = contextMenu

    End Sub

    Private Function GetDeceasedStatus(deceasedId As Integer, typeValue As Integer, dateOfDeath As Date?, intermentDate As Date?) As String
        Try
            ' Check if deceased has location and client
            Dim hasLocationAndClient As Boolean = False
            Dim sql As String = "SELECT COUNT(*) FROM deceased d " &
                              "WHERE d.Deceased_ID = @DeceasedID " &
                              "AND d.Plot_ID IS NOT NULL " &
                              "AND d.Client_ID IS NOT NULL"

            If cn IsNot Nothing AndAlso cn.State = ConnectionState.Open Then
                cn.Close()
            End If

            dbconn()
            cn.Open()

            Using cmd As New MySqlCommand(sql, cn)
                cmd.Parameters.AddWithValue("@DeceasedID", deceasedId)
                Dim count As Integer = Convert.ToInt32(cmd.ExecuteScalar())
                hasLocationAndClient = (count > 0)
            End Using

            ' If no location or client, status is Pending
            If Not hasLocationAndClient Then
                Return "Pending"
            End If

            ' For apartment type, check for expired status
            If typeValue = 1 Then
                Dim baseDate As Date? = Nothing
                If dateOfDeath.HasValue AndAlso intermentDate.HasValue Then
                    baseDate = If(dateOfDeath > intermentDate, dateOfDeath, intermentDate)
                ElseIf dateOfDeath.HasValue Then
                    baseDate = dateOfDeath
                ElseIf intermentDate.HasValue Then
                    baseDate = intermentDate
                End If

                If baseDate.HasValue Then
                    Dim expiryDate = baseDate.Value.AddYears(8)
                    If Date.Now >= expiryDate Then
                        Return "Expired"
                    End If
                End If
            End If

            ' Default status if location and client exist
            Return "Remaining"

        Catch ex As Exception
            MessageBox.Show("Error checking deceased status: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return "Pending"
        Finally
            If cn IsNot Nothing AndAlso cn.State = ConnectionState.Open Then
                cn.Close()
            End If
        End Try
    End Function

    Sub LoadUsers(Optional filterText As String = "")
        ' Ensure the connection is closed before using it
        If cn IsNot Nothing AndAlso cn.State = ConnectionState.Open Then
            cn.Close()
        End If

        dbconn()  ' Now open the connection with the correct connection string
        cn.Open()  ' Open the connection explicitly after setting it

        Dim filters As New List(Of String)

        If chkbxBoneNiche.Checked Then filters.Add("l.Type = 3")
        If chkbxPrivate.Checked Then filters.Add("l.Type = 4")
        If chkbxApartment.Checked Then filters.Add("l.Type = 1")
        If chkbxLawnLots.Checked Then filters.Add("l.Type = 2")

        Dim whereClause As String = ""
        If filters.Count > 0 Then
            whereClause = "WHERE " & String.Join(" OR ", filters)
        End If

        sql = "SELECT d.Deceased_ID, d.LastName, d.FirstName, d.MiddleName, d.DateOfDeath, d.deceased_status, " &
          "l.Type, l.Block, l.Section, l.Row, l.Plot, d.Interment, d.Plot_ID, d.Client_ID " &
          "FROM deceased d " &
          "LEFT JOIN location l ON d.Plot_ID = l.id " &
          whereClause & " ORDER BY d.DateOfDeath DESC, d.LastName ASC"

        cmd = New MySqlCommand(sql, cn)
        dr = cmd.ExecuteReader

        DeceasedList.Items.Clear()

        Dim index As Integer = 1  ' Start numbering from 1

        While dr.Read
            Dim fullName As String = (dr("LastName") & ", " & dr("FirstName") & ", " & dr("MiddleName")).ToLower()

            If filterText = "" OrElse fullName.Contains(filterText.ToLower()) Then
                ' Add index as the first column instead of Deceased_ID
                Dim newLine = DeceasedList.Items.Add(index.ToString()) ' This will be the numbered list starting from 1
                newLine.Tag = dr("Deceased_ID")  ' Store Deceased_ID in the Tag property
                newLine.SubItems.Add(dr("LastName") & ", " & dr("FirstName") & ", " & dr("MiddleName"))

                ' Convert type integer to string
                Dim typeValue As Integer = 0
                If dr("Type") IsNot DBNull.Value AndAlso Integer.TryParse(dr("Type").ToString(), typeValue) Then
                    ' typeValue is set
                Else
                    typeValue = 0 ' or any default value for unknown
                End If

                Dim typeString As String = ""
                Select Case typeValue
                    Case 1
                        typeString = "Apartment"
                    Case 2
                        typeString = "Family Lawn Lots"
                    Case 3
                        typeString = "Bone Niche"
                    Case 4
                        typeString = "Private"
                    Case Else
                        typeString = "Unknown"
                End Select

                ' Format location string
                Dim locationString As String = $"{typeString}, Block {dr("Block")}, Section {dr("Section")}, Row {dr("Row")}, Plot {dr("Plot")}"
                newLine.SubItems.Add(locationString)

                ' Get status logic inline (no DB call)
                Dim plotId As Object = dr("Plot_ID")
                Dim clientId As Object = dr("Client_ID")
                Dim hasLocationAndClient As Boolean = (plotId IsNot DBNull.Value AndAlso clientId IsNot DBNull.Value)

                ' Get dates for status check
                Dim dateOfDeath As Date? = Nothing
                Dim intermentDate As Date? = Nothing
                If dr("DateOfDeath") IsNot DBNull.Value Then
                    Dim tmpDate As Date
                    If Date.TryParse(dr("DateOfDeath").ToString(), tmpDate) Then
                        dateOfDeath = tmpDate
                    End If
                End If
                If dr("Interment") IsNot DBNull.Value Then
                    Dim tmpDate As Date
                    If Date.TryParse(dr("Interment").ToString(), tmpDate) Then
                        intermentDate = tmpDate
                    End If
                End If

                Dim status As String = If(dr("deceased_status") IsNot DBNull.Value, dr("deceased_status").ToString(), "N/A")
                If Not hasLocationAndClient Then
                    status = "Pending"
                ElseIf typeValue = 1 Then
                    Dim baseDate As Date? = Nothing
                    If dateOfDeath.HasValue AndAlso intermentDate.HasValue Then
                        baseDate = If(dateOfDeath > intermentDate, dateOfDeath, intermentDate)
                    ElseIf dateOfDeath.HasValue Then
                        baseDate = dateOfDeath
                    ElseIf intermentDate.HasValue Then
                        baseDate = intermentDate
                    End If
                    If baseDate.HasValue AndAlso Date.Now >= baseDate.Value.AddYears(8) _
                        AndAlso status <> "Relocated" AndAlso status <> "Renewal" Then
                        status = "Expired"
                    End If
                End If
                newLine.SubItems.Add(status)  ' Display deceased status

                ' Increment index for the next item
                index += 1
            End If
        End While

        dr.Close()
        cn.Close()  ' Close the connection when done
    End Sub


    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        LoadUsers(txtSearch.Text)
    End Sub

    Private Sub chkbxBoneNiche_CheckedChanged(sender As Object, e As EventArgs) Handles chkbxBoneNiche.CheckedChanged
        LoadUsers(txtSearch.Text)
    End Sub

    Private Sub chkbxPrivate_CheckedChanged(sender As Object, e As EventArgs) Handles chkbxPrivate.CheckedChanged
        LoadUsers(txtSearch.Text)
    End Sub

    Private Sub chkbxApartment_CheckedChanged(sender As Object, e As EventArgs) Handles chkbxApartment.CheckedChanged
        LoadUsers(txtSearch.Text)
    End Sub

    Private Sub chkbxLawnLots_CheckedChanged(sender As Object, e As EventArgs) Handles chkbxLawnLots.CheckedChanged
        LoadUsers(txtSearch.Text)
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        txtSearch.Clear()
        chkbxBoneNiche.Checked = False
        chkbxPrivate.Checked = False
        chkbxApartment.Checked = False
        chkbxLawnLots.Checked = False
        LoadUsers()
    End Sub

    Private Sub DeceasedList_MouseDown(sender As Object, e As MouseEventArgs) Handles DeceasedList.MouseDown
        ' Check if the user right-clicked (MouseButtons.Right) on the ListView
        If e.Button = MouseButtons.Right Then
            ' Check if the click is on an item (i.e., not just on empty space)
            Dim hit = DeceasedList.HitTest(e.Location)
            If hit.Item IsNot Nothing AndAlso hit.Item.SubItems.Count > 5 Then
                ' Show the context menu at the click location
                DeceasedList.ContextMenuStrip.Show(DeceasedList, e.Location)
            End If
        End If
    End Sub

    Private Sub UpdateStatus(newStatus As String)
        Try
            ' Get the selected item
            If DeceasedList.SelectedItems.Count = 0 Then
                MessageBox.Show("Please select a deceased record to update the status.", "Select Record", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            ' Retrieve the Deceased_ID from the Tag property of the selected item
            Dim selectedID As Integer = Convert.ToInt32(DeceasedList.SelectedItems(0).Tag)

            ' Close connection if it's open
            If cn IsNot Nothing AndAlso cn.State = ConnectionState.Open Then
                cn.Close()
            End If

            dbconn()
            cn.Open()

            Dim updateSql As String = "UPDATE deceased SET deceased_status = @status WHERE Deceased_ID = @id"
            Using cmd As New MySqlCommand(updateSql, cn)
                cmd.Parameters.AddWithValue("@status", newStatus)
                cmd.Parameters.AddWithValue("@id", selectedID)
                cmd.ExecuteNonQuery()
            End Using

            MessageBox.Show("Status updated successfully to: " & newStatus, "Update Successful", MessageBoxButtons.OK, MessageBoxIcon.Information)

            ' Refresh the list to show the updated status
            LoadUsers()
        Catch ex As Exception
            MessageBox.Show("Error updating status: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If cn.State = ConnectionState.Open Then cn.Close()
        End Try
    End Sub

    Private Sub btnImportExcel_Click(sender As Object, e As EventArgs) Handles btnImportExcel.Click
        Try
            ' Show file dialog to select Excel file
            Dim openFileDialog As New OpenFileDialog()
            openFileDialog.Filter = "Excel Files|*.xlsx;*.xls"
            openFileDialog.Title = "Select Excel File"

            If openFileDialog.ShowDialog() = DialogResult.OK Then
                Dim filePath As String = openFileDialog.FileName
                ImportExcelData(filePath)
            End If
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Excel Import Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ApplyTypeFilter()
        dbconn()
        cn.Open()

        Dim filters As New List(Of String)

        If chkbxBoneNiche.Checked Then
            filters.Add("l.Type = 3")
        End If
        If chkbxPrivate.Checked Then
            filters.Add("l.Type = 4")
        End If
        If chkbxApartment.Checked Then
            filters.Add("l.Type = 1")
        End If
        If chkbxLawnLots.Checked Then
            filters.Add("l.Type = 2")
        End If

        Dim whereClause As String = ""
        If filters.Count > 0 Then
            whereClause = "WHERE " & String.Join(" OR ", filters)
        End If

        sql = "SELECT d.Deceased_ID, d.LastName, d.FirstName, d.MiddleName, d.DateOfBirth, d.DateOfDeath, " &
          "l.Type, l.Block, l.Section, l.Row, l.Plot " &
          "FROM deceased d " &
          "JOIN location l ON d.Plot_ID = l.id " &
          whereClause &
          " ORDER BY d.LastName ASC"

        cmd = New MySqlCommand(sql, cn)
        dr = cmd.ExecuteReader

        DeceasedList.Items.Clear()

        While dr.Read
            Dim newLine = DeceasedList.Items.Add(dr("Deceased_ID"))
            newLine.SubItems.Add(dr("LastName") & ", " & dr("FirstName") & ", " & dr("MiddleName"))

            Dim typeValue As Integer = 0
            If dr("Type") IsNot DBNull.Value AndAlso Integer.TryParse(dr("Type").ToString(), typeValue) Then
                ' typeValue is set
            Else
                typeValue = 0 ' or any default value for unknown
            End If

            Dim typeString As String = ""
            Select Case typeValue
                Case 1 : typeString = "Apartment"
                Case 2 : typeString = "Family Lawn Lots"
                Case 3 : typeString = "Bone Niche"
                Case 4 : typeString = "Private"
                Case Else : typeString = "Unknown"
            End Select

            Dim locationString As String = $"{typeString}, Block {dr("Block")}, Section {dr("Section")}, Row {dr("Row")}, Plot {dr("Plot")}"
            newLine.SubItems.Add(locationString)
        End While

        dr.Close()
        cn.Close()
    End Sub

    Private Sub ImportExcelData(filePath As String)
        Dim excelApp As Application = Nothing
        Dim workbooks As Workbooks = Nothing
        Dim workbook As Workbook = Nothing
        Dim worksheet As Worksheet = Nothing

        Dim successCount As Integer = 0
        Dim errorCount As Integer = 0
        Dim errorMessages As New List(Of String)

        Try
            excelApp = New Application()
            excelApp.Visible = False
            excelApp.DisplayAlerts = False
            workbooks = excelApp.Workbooks
            workbook = workbooks.Open(filePath)
            worksheet = DirectCast(workbook.Worksheets(1), Worksheet)
            Dim range As Range = worksheet.UsedRange
            Dim rowCount As Integer = range.Rows.Count

            If rowCount <= 1 Then
                MessageBox.Show("No data found in the Excel file.", "Empty File", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            dbconn()
            cn.Open()
            Dim transaction As MySqlTransaction = cn.BeginTransaction()

            Try
                For rowIndex As Integer = 2 To rowCount ' Start from row 2 (assuming row 1 has headers)
                    Try
                        ' 1. Read client info
                        Dim clientFirstName As String = GetCellValue(worksheet.Cells(rowIndex, 1))
                        Dim clientMiddleName As String = GetCellValue(worksheet.Cells(rowIndex, 2))
                        Dim clientLastName As String = GetCellValue(worksheet.Cells(rowIndex, 3))
                        Dim clientExt As String = GetCellValue(worksheet.Cells(rowIndex, 4))
                        Dim clientGender As String = GetCellValue(worksheet.Cells(rowIndex, 5))
                        Dim clientMobile As String = GetCellValue(worksheet.Cells(rowIndex, 6))
                        Dim clientRelationship As String = GetCellValue(worksheet.Cells(rowIndex, 7))
                        Dim clientAddress As String = GetCellValue(worksheet.Cells(rowIndex, 8))

                        ' Skip row if essential client information is missing
                        If String.IsNullOrWhiteSpace(clientFirstName) OrElse String.IsNullOrWhiteSpace(clientLastName) Then
                            errorMessages.Add($"Row {rowIndex}: Missing essential client information (First Name and Last Name required)")
                            errorCount += 1
                            Continue For
                        End If

                        ' 2. Check if client exists
                        Dim clientId As Integer = -1
                        Dim sqlCheckClient As String = "SELECT Client_ID FROM client WHERE FirstName=@FirstName AND LastName=@LastName"
                        If Not String.IsNullOrWhiteSpace(clientMobile) Then
                            sqlCheckClient &= " AND Mobile=@Mobile"
                        End If
                        sqlCheckClient &= " LIMIT 1"

                        Using cmdCheck As New MySqlCommand(sqlCheckClient, cn, transaction)
                            cmdCheck.Parameters.AddWithValue("@FirstName", clientFirstName)
                            cmdCheck.Parameters.AddWithValue("@LastName", clientLastName)
                            If Not String.IsNullOrWhiteSpace(clientMobile) Then
                                cmdCheck.Parameters.AddWithValue("@Mobile", clientMobile)
                            End If

                            Dim result = cmdCheck.ExecuteScalar()
                            If result IsNot Nothing Then
                                clientId = Convert.ToInt32(result)
                            Else
                                ' Insert client
                                Dim sqlInsertClient As String = "INSERT INTO client (FirstName, MiddleName, LastName, Ext, Gender, Mobile, Relationship_to_Deceased, Address) VALUES (@FirstName, @MiddleName, @LastName, @Ext, @Gender, @Mobile, @Relationship, @Address)"
                                Using cmdInsert As New MySqlCommand(sqlInsertClient, cn, transaction)
                                    cmdInsert.Parameters.AddWithValue("@FirstName", clientFirstName)
                                    cmdInsert.Parameters.AddWithValue("@MiddleName", If(String.IsNullOrWhiteSpace(clientMiddleName), DBNull.Value, clientMiddleName))
                                    cmdInsert.Parameters.AddWithValue("@LastName", clientLastName)
                                    cmdInsert.Parameters.AddWithValue("@Ext", If(String.IsNullOrWhiteSpace(clientExt), DBNull.Value, clientExt))
                                    cmdInsert.Parameters.AddWithValue("@Gender", clientGender)
                                    cmdInsert.Parameters.AddWithValue("@Mobile", If(String.IsNullOrWhiteSpace(clientMobile), DBNull.Value, clientMobile))
                                    cmdInsert.Parameters.AddWithValue("@Relationship", If(String.IsNullOrWhiteSpace(clientRelationship), DBNull.Value, clientRelationship))
                                    cmdInsert.Parameters.AddWithValue("@Address", If(String.IsNullOrWhiteSpace(clientAddress), DBNull.Value, clientAddress))
                                    cmdInsert.ExecuteNonQuery()
                                    clientId = Convert.ToInt32(cmdInsert.LastInsertedId)
                                End Using
                            End If
                        End Using

                        ' 3. Read deceased info
                        Dim deceasedFirstName As String = GetCellValue(worksheet.Cells(rowIndex, 9))
                        Dim deceasedMiddleName As String = GetCellValue(worksheet.Cells(rowIndex, 10))
                        Dim deceasedLastName As String = GetCellValue(worksheet.Cells(rowIndex, 11))
                        Dim deceasedExt As String = GetCellValue(worksheet.Cells(rowIndex, 12))
                        Dim dateOfBirthStr As String = GetCellValue(worksheet.Cells(rowIndex, 13))
                        Dim dateOfDeathStr As String = GetCellValue(worksheet.Cells(rowIndex, 14))
                        Dim intermentStr As String = GetCellValue(worksheet.Cells(rowIndex, 20))
                        Dim deceasedGender As String = GetCellValue(worksheet.Cells(rowIndex, 21))
                        Dim deceasedNationality As String = GetCellValue(worksheet.Cells(rowIndex, 22))
                        Dim deceasedReligion As String = GetCellValue(worksheet.Cells(rowIndex, 23))

                        ' Skip row if essential deceased information is missing
                        If String.IsNullOrWhiteSpace(deceasedFirstName) OrElse String.IsNullOrWhiteSpace(deceasedLastName) Then
                            errorMessages.Add($"Row {rowIndex}: Missing essential deceased information (First Name and Last Name required)")
                            errorCount += 1
                            Continue For
                        End If

                        ' Acceptable date formats
                        Dim dateFormats() As String = {
                        "yyyy-MM-dd", "dd/MM/yyyy", "MM/dd/yyyy", "M/d/yyyy", "d/M/yyyy",
                        "yyyy/MM/dd", "dd-MM-yyyy", "MM-dd-yyyy", "d-M-yyyy", "M-d-yyyy",
                        "dd/MM/yyyy HH:mm:ss tt", "dd/MM/yyyy h:mm:ss tt", "dd/MM/yyyy HH:mm:ss", "dd/MM/yyyy h:mm:ss"
                    }

                        ' Date of Birth handling
                        Dim dateOfBirth As Date
                        Dim dobIsValid As Boolean = False

                        If Not String.IsNullOrWhiteSpace(dateOfBirthStr) Then
                            ' Try to extract just the date part if there's time information
                            Dim datePart As String = dateOfBirthStr.Split(" "c)(0)
                            dobIsValid = Date.TryParseExact(datePart, dateFormats, CultureInfo.InvariantCulture, DateTimeStyles.None, dateOfBirth)
                            If Not dobIsValid Then
                                dobIsValid = Date.TryParse(datePart, CultureInfo.InvariantCulture, DateTimeStyles.None, dateOfBirth)
                            End If

                            If Not dobIsValid Then
                                errorMessages.Add($"Row {rowIndex}: Invalid Date of Birth format: {dateOfBirthStr}")
                                errorCount += 1
                                Continue For
                            End If
                        Else
                            ' If no DOB provided, can either skip or use a default
                            errorMessages.Add($"Row {rowIndex}: Missing Date of Birth")
                            errorCount += 1
                            Continue For
                        End If

                        ' Date of Death handling
                        Dim dateOfDeath As Date
                        Dim dodIsValid As Boolean = False

                        If Not String.IsNullOrWhiteSpace(dateOfDeathStr) Then
                            ' Try to extract just the date part if there's time information
                            Dim datePart As String = dateOfDeathStr.Split(" "c)(0)
                            dodIsValid = Date.TryParseExact(datePart, dateFormats, CultureInfo.InvariantCulture, DateTimeStyles.None, dateOfDeath)
                            If Not dodIsValid Then
                                dodIsValid = Date.TryParse(datePart, CultureInfo.InvariantCulture, DateTimeStyles.None, dateOfDeath)
                            End If

                            If Not dodIsValid Then
                                errorMessages.Add($"Row {rowIndex}: Invalid Date of Death format: {dateOfDeathStr}")
                                errorCount += 1
                                Continue For
                            End If
                        Else
                            ' If no DOD provided, can either skip or use a default
                            errorMessages.Add($"Row {rowIndex}: Missing Date of Death")
                            errorCount += 1
                            Continue For
                        End If

                        ' Interment date handling (optional)
                        Dim intermentDate As Date
                        Dim intermentIsValid As Boolean = False

                        If Not String.IsNullOrWhiteSpace(intermentStr) Then
                            ' Try to extract just the date part if there's time information
                            Dim datePart As String = intermentStr.Split(" "c)(0)
                            intermentIsValid = Date.TryParseExact(datePart, dateFormats, CultureInfo.InvariantCulture, DateTimeStyles.None, intermentDate)
                            If Not intermentIsValid Then
                                intermentIsValid = Date.TryParse(datePart, CultureInfo.InvariantCulture, DateTimeStyles.None, intermentDate)
                            End If

                            If Not intermentIsValid Then
                                ' Warning only - not critical
                                errorMessages.Add($"Row {rowIndex}: Warning - Invalid Interment Date format: {intermentStr}, it will be ignored")
                            End If
                        End If

                        ' Read location info from Excel
                        Dim block As String = GetCellValue(worksheet.Cells(rowIndex, 15))
                        Dim section As String = GetCellValue(worksheet.Cells(rowIndex, 16))
                        Dim row As String = GetCellValue(worksheet.Cells(rowIndex, 17))
                        Dim plot As String = GetCellValue(worksheet.Cells(rowIndex, 18))
                        Dim type As String = GetCellValue(worksheet.Cells(rowIndex, 19))

                        ' Convert type to integer if needed (if stored as string in Excel)
                        Dim typeNumber As Integer
                        If Not Integer.TryParse(type, typeNumber) Then
                            ' Try to convert type string to number
                            Select Case type.Trim().ToLower()
                                Case "apartment"
                                    typeNumber = 1
                                Case "family lawn lots", "lawn lots", "lawn"
                                    typeNumber = 2
                                Case "bone niche", "niche", "bone"
                                    typeNumber = 3
                                Case "private"
                                    typeNumber = 4
                                Case Else
                                    ' Default type or error
                                    errorMessages.Add($"Row {rowIndex}: Unknown location type: {type}, using default")
                                    typeNumber = 1 ' Default to apartment or whatever makes sense as default
                            End Select
                        End If

                        ' Convert location fields to integers
                        Dim blockInt, sectionInt, rowInt, plotInt As Integer
                        If Not Integer.TryParse(block, blockInt) OrElse
                           Not Integer.TryParse(section, sectionInt) OrElse
                           Not Integer.TryParse(row, rowInt) OrElse
                           Not Integer.TryParse(plot, plotInt) Then
                            errorMessages.Add($"Row {rowIndex}: Invalid number in location fields (Block, Section, Row, Plot must be numbers)")
                            errorCount += 1
                            Continue For
                        End If

                        ' Check essential location info
                        If String.IsNullOrWhiteSpace(block) OrElse String.IsNullOrWhiteSpace(section) Then
                            errorMessages.Add($"Row {rowIndex}: Missing essential location information (Block and Section required)")
                            errorCount += 1
                            Continue For
                        End If

                        ' 1. Check if location exists
                        Dim plotId As Integer = -1
                        Dim sqlCheckLocation As String = "SELECT id FROM location WHERE block=@block AND section=@section AND row=@row AND plot=@plot AND type=@type LIMIT 1"
                        Using cmdCheckLoc As New MySqlCommand(sqlCheckLocation, cn, transaction)
                            cmdCheckLoc.Parameters.AddWithValue("@block", blockInt)
                            cmdCheckLoc.Parameters.AddWithValue("@section", sectionInt)
                            cmdCheckLoc.Parameters.AddWithValue("@row", rowInt)
                            cmdCheckLoc.Parameters.AddWithValue("@plot", plotInt)
                            cmdCheckLoc.Parameters.AddWithValue("@type", typeNumber)
                            Dim resultLoc = cmdCheckLoc.ExecuteScalar()
                            If resultLoc IsNot Nothing Then
                                plotId = Convert.ToInt32(resultLoc)
                                ' Additional validation to ensure plotId is greater than 0
                                If plotId <= 0 Then
                                    errorMessages.Add($"Row {rowIndex}: Invalid location ID retrieved from database (ID: {plotId})")
                                    errorCount += 1
                                    Continue For
                                End If
                            Else
                                ' 2. Insert location if not exists
                                Dim sqlInsertLoc As String = "INSERT INTO location (block, section, row, plot, type) VALUES (@block, @section, @row, @plot, @type)"
                                Using cmdInsertLoc As New MySqlCommand(sqlInsertLoc, cn, transaction)
                                    cmdInsertLoc.Parameters.AddWithValue("@block", blockInt)
                                    cmdInsertLoc.Parameters.AddWithValue("@section", sectionInt)
                                    cmdInsertLoc.Parameters.AddWithValue("@row", rowInt)
                                    cmdInsertLoc.Parameters.AddWithValue("@plot", plotInt)
                                    cmdInsertLoc.Parameters.AddWithValue("@type", typeNumber)
                                    cmdInsertLoc.ExecuteNonQuery()
                                    plotId = Convert.ToInt32(cmdInsertLoc.LastInsertedId)

                                    ' Additional validation after insert
                                    If plotId <= 0 Then
                                        errorMessages.Add($"Row {rowIndex}: Failed to create valid location ID (ID: {plotId})")
                                        errorCount += 1
                                        Continue For
                                    End If
                                End Using
                            End If
                        End Using

                        ' 4. Insert deceased
                        Dim deceasedStatus As String = "Pending"
                        If plotId > 0 AndAlso clientId > 0 Then
                            deceasedStatus = "Remaining"
                        End If

                        ' Ensure clientId is valid before proceeding
                        If clientId <= 0 Then
                            errorMessages.Add($"Row {rowIndex}: Invalid or missing client information (Client_ID is {clientId})")
                            errorCount += 1
                            Continue For
                        End If

                        ' Ensure plotId is valid before proceeding
                        If plotId <= 0 Then
                            errorMessages.Add($"Row {rowIndex}: Invalid or missing plot information (Plot_ID is {plotId})")
                            errorCount += 1
                            Continue For
                        End If

                        ' Check if deceased with same details already exists to prevent duplicates
                        Dim deceasedExists As Boolean = False
                        Dim deceasedId As Integer = -1
                        Dim sqlCheckDeceased As String = "SELECT Deceased_ID FROM deceased WHERE FirstName=@FirstName AND LastName=@LastName AND DateOfDeath=@DateOfDeath LIMIT 1"
                        Using cmdCheckDeceased As New MySqlCommand(sqlCheckDeceased, cn, transaction)
                            cmdCheckDeceased.Parameters.AddWithValue("@FirstName", deceasedFirstName)
                            cmdCheckDeceased.Parameters.AddWithValue("@LastName", deceasedLastName)
                            cmdCheckDeceased.Parameters.AddWithValue("@DateOfDeath", dateOfDeath)
                            Dim resultDeceased = cmdCheckDeceased.ExecuteScalar()
                            If resultDeceased IsNot Nothing Then
                                deceasedExists = True
                                deceasedId = Convert.ToInt32(resultDeceased)
                                errorMessages.Add($"Row {rowIndex}: Warning - Deceased person already exists in database, updating existing record")
                            End If
                        End Using

                        If deceasedExists Then
                            ' Update existing record
                            Dim sqlUpdateDeceased As String = "UPDATE deceased SET " &
                                                      "MiddleName=@MiddleName, Ext=@Ext, " &
                                                      "DateOfBirth=@DateOfBirth, Plot_ID=@PlotID, " &
                                                      "Interment=@Interment, Gender=@Gender, " &
                                                      "Nationality=@Nationality, Religion=@Religion, " &
                                                      "Client_ID=@ClientID, deceased_status=@DeceasedStatus " &
                                                      "WHERE Deceased_ID=@DeceasedID"
                            Using cmdUpdateDeceased As New MySqlCommand(sqlUpdateDeceased, cn, transaction)
                                cmdUpdateDeceased.Parameters.AddWithValue("@MiddleName", If(String.IsNullOrWhiteSpace(deceasedMiddleName), DBNull.Value, deceasedMiddleName))
                                cmdUpdateDeceased.Parameters.AddWithValue("@Ext", If(String.IsNullOrWhiteSpace(deceasedExt), DBNull.Value, deceasedExt))
                                cmdUpdateDeceased.Parameters.AddWithValue("@DateOfBirth", dateOfBirth)
                                cmdUpdateDeceased.Parameters.AddWithValue("@PlotID", plotId)
                                cmdUpdateDeceased.Parameters.AddWithValue("@Interment", If(intermentIsValid, intermentDate, DBNull.Value))
                                cmdUpdateDeceased.Parameters.AddWithValue("@Gender", deceasedGender)
                                cmdUpdateDeceased.Parameters.AddWithValue("@Nationality", If(String.IsNullOrWhiteSpace(deceasedNationality), DBNull.Value, deceasedNationality))
                                cmdUpdateDeceased.Parameters.AddWithValue("@Religion", If(String.IsNullOrWhiteSpace(deceasedReligion), DBNull.Value, deceasedReligion))
                                cmdUpdateDeceased.Parameters.AddWithValue("@ClientID", clientId)
                                cmdUpdateDeceased.Parameters.AddWithValue("@DeceasedStatus", deceasedStatus)
                                cmdUpdateDeceased.Parameters.AddWithValue("@DeceasedID", deceasedId)
                                cmdUpdateDeceased.ExecuteNonQuery()
                            End Using
                        Else
                            ' Insert new deceased record
                            Dim sqlInsertDeceased As String = "INSERT INTO deceased (FirstName, MiddleName, LastName, Ext, DateOfBirth, DateOfDeath, Plot_ID, Interment, Gender, Nationality, Religion, Client_ID, deceased_status) " &
                                                      "VALUES (@FirstName, @MiddleName, @LastName, @Ext, @DateOfBirth, @DateOfDeath, @PlotID, @Interment, @Gender, @Nationality, @Religion, @ClientID, @DeceasedStatus)"
                            Using cmdInsertDeceased As New MySqlCommand(sqlInsertDeceased, cn, transaction)
                                cmdInsertDeceased.Parameters.AddWithValue("@FirstName", deceasedFirstName)
                                cmdInsertDeceased.Parameters.AddWithValue("@MiddleName", If(String.IsNullOrWhiteSpace(deceasedMiddleName), DBNull.Value, deceasedMiddleName))
                                cmdInsertDeceased.Parameters.AddWithValue("@LastName", deceasedLastName)
                                cmdInsertDeceased.Parameters.AddWithValue("@Ext", If(String.IsNullOrWhiteSpace(deceasedExt), DBNull.Value, deceasedExt))
                                cmdInsertDeceased.Parameters.AddWithValue("@DateOfBirth", dateOfBirth)
                                cmdInsertDeceased.Parameters.AddWithValue("@DateOfDeath", dateOfDeath)
                                cmdInsertDeceased.Parameters.AddWithValue("@PlotID", plotId)
                                cmdInsertDeceased.Parameters.AddWithValue("@Interment", If(intermentIsValid, intermentDate, DBNull.Value))
                                cmdInsertDeceased.Parameters.AddWithValue("@Gender", deceasedGender)
                                cmdInsertDeceased.Parameters.AddWithValue("@Nationality", If(String.IsNullOrWhiteSpace(deceasedNationality), DBNull.Value, deceasedNationality))
                                cmdInsertDeceased.Parameters.AddWithValue("@Religion", If(String.IsNullOrWhiteSpace(deceasedReligion), DBNull.Value, deceasedReligion))
                                cmdInsertDeceased.Parameters.AddWithValue("@ClientID", clientId)
                                cmdInsertDeceased.Parameters.AddWithValue("@DeceasedStatus", deceasedStatus)
                                cmdInsertDeceased.ExecuteNonQuery()
                                deceasedId = Convert.ToInt32(cmdInsertDeceased.LastInsertedId)
                            End Using
                        End If

                        ' 5. Insert beneficiaries
                        Dim beneficiary1Name As String = GetCellValue(worksheet.Cells(rowIndex, 24))
                        Dim beneficiary1Contact As String = GetCellValue(worksheet.Cells(rowIndex, 25))
                        Dim beneficiary2Name As String = GetCellValue(worksheet.Cells(rowIndex, 26))
                        Dim beneficiary2Contact As String = GetCellValue(worksheet.Cells(rowIndex, 27))
                        Dim relationship As String = GetCellValue(worksheet.Cells(rowIndex, 28))

                        If Not String.IsNullOrWhiteSpace(beneficiary1Name) Then
                            ' Check if beneficiary already exists
                            Dim sqlCheckBenef As String = "SELECT id FROM beneficiaries WHERE Client_ID = @ClientID AND FullName = @FullName AND `Order` = @Order LIMIT 1"
                            Dim benefExists As Boolean = False
                            Using cmdCheckBenef As New MySqlCommand(sqlCheckBenef, cn, transaction)
                                cmdCheckBenef.Parameters.AddWithValue("@ClientID", clientId)
                                cmdCheckBenef.Parameters.AddWithValue("@FullName", beneficiary1Name)
                                cmdCheckBenef.Parameters.AddWithValue("@Order", 1)
                                benefExists = cmdCheckBenef.ExecuteScalar() IsNot Nothing
                            End Using

                            If Not benefExists Then
                                Dim sqlInsertBeneficiary As String = "INSERT INTO beneficiaries (Client_ID, FullName, `Order`, Contact, status) VALUES (@ClientID, @FullName, @Order, @Contact, @Status)"
                                Using cmdB As New MySqlCommand(sqlInsertBeneficiary, cn, transaction)
                                    cmdB.Parameters.AddWithValue("@ClientID", clientId)
                                    cmdB.Parameters.AddWithValue("@FullName", beneficiary1Name)
                                    cmdB.Parameters.AddWithValue("@Order", 1)
                                    cmdB.Parameters.AddWithValue("@Contact", If(String.IsNullOrWhiteSpace(beneficiary1Contact), DBNull.Value, beneficiary1Contact))
                                    cmdB.Parameters.AddWithValue("@Status", DBNull.Value)
                                    cmdB.ExecuteNonQuery()
                                End Using
                            End If
                        End If

                        If Not String.IsNullOrWhiteSpace(beneficiary2Name) Then
                            ' Check if beneficiary already exists
                            Dim sqlCheckBenef As String = "SELECT id FROM beneficiaries WHERE Client_ID = @ClientID AND FullName = @FullName AND `Order` = @Order LIMIT 1"
                            Dim benefExists As Boolean = False
                            Using cmdCheckBenef As New MySqlCommand(sqlCheckBenef, cn, transaction)
                                cmdCheckBenef.Parameters.AddWithValue("@ClientID", clientId)
                                cmdCheckBenef.Parameters.AddWithValue("@FullName", beneficiary2Name)
                                cmdCheckBenef.Parameters.AddWithValue("@Order", 2)
                                benefExists = cmdCheckBenef.ExecuteScalar() IsNot Nothing
                            End Using

                            If Not benefExists Then
                                Dim sqlInsertBeneficiary As String = "INSERT INTO beneficiaries (Client_ID, FullName, `Order`, Contact, status) VALUES (@ClientID, @FullName, @Order, @Contact, @Status)"
                                Using cmdB As New MySqlCommand(sqlInsertBeneficiary, cn, transaction)
                                    cmdB.Parameters.AddWithValue("@ClientID", clientId)
                                    cmdB.Parameters.AddWithValue("@FullName", beneficiary2Name)
                                    cmdB.Parameters.AddWithValue("@Order", 2)
                                    cmdB.Parameters.AddWithValue("@Contact", If(String.IsNullOrWhiteSpace(beneficiary2Contact), DBNull.Value, beneficiary2Contact))
                                    cmdB.Parameters.AddWithValue("@Status", DBNull.Value)
                                    cmdB.ExecuteNonQuery()
                                End Using
                            End If
                        End If

                        successCount += 1
                    Catch ex As Exception
                        errorMessages.Add($"Row {rowIndex}: {ex.Message}")
                        errorCount += 1
                    End Try
                Next

                transaction.Commit()
                LoadUsers() ' Refresh the list to show new imported records

                If errorMessages.Count > 0 Then
                    Dim errorDetails As String = String.Join(Environment.NewLine, errorMessages.Take(5))
                    If errorMessages.Count > 5 Then
                        errorDetails += Environment.NewLine + $"...and {errorMessages.Count - 5} more errors"
                    End If
                    MessageBox.Show($"Import complete. {successCount} records imported successfully. {errorCount} records had errors.{Environment.NewLine}{Environment.NewLine}First few errors:{Environment.NewLine}{errorDetails}",
                            "Import Results", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Else
                    MessageBox.Show($"Import complete. {successCount} records imported successfully.",
                            "Import Results", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If

            Catch ex As Exception
                transaction.Rollback()
                MessageBox.Show($"Transaction error: {ex.Message}", "Import Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try

        Catch ex As Exception
            MessageBox.Show($"Excel processing error: {ex.Message}", "Import Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            ' Clean up Excel objects to prevent memory leaks
            If worksheet IsNot Nothing Then Marshal.ReleaseComObject(worksheet)
            If workbook IsNot Nothing Then
                workbook.Close(False)
                Marshal.ReleaseComObject(workbook)
            End If
            If workbooks IsNot Nothing Then Marshal.ReleaseComObject(workbooks)
            If excelApp IsNot Nothing Then
                excelApp.Quit()
                Marshal.ReleaseComObject(excelApp)
            End If
            If cn IsNot Nothing AndAlso cn.State = ConnectionState.Open Then
                cn.Close()
            End If
            GC.Collect()
            GC.WaitForPendingFinalizers()
        End Try
    End Sub

    Private Function GetCellValue(cell As Range) As String
        Try
            If cell IsNot Nothing AndAlso cell.Value IsNot Nothing Then
                Return cell.Value.ToString().Trim()
            End If
        Catch ex As Exception
            ' Handle any unexpected exceptions when reading cell values
            Return String.Empty
        End Try
        Return String.Empty
    End Function

    Private Sub DeceasedList_DoubleClick(sender As Object, e As EventArgs) Handles DeceasedList.DoubleClick
        If DeceasedList.SelectedItems.Count = 0 Then
            MessageBox.Show("Please select a deceased record.", "View Deceased", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' Retrieve the Deceased_ID from the Tag property of the selected item
        Dim selectedID As Integer = Convert.ToInt32(DeceasedList.SelectedItems(0).Tag)

        ' Open the update form and pass the Deceased_ID to it
        Dim viewDeceasedForm As New frmViewDeceased()
        viewDeceasedForm.LoadDeceasedInfo(selectedID)
        viewDeceasedForm.ShowDialog()
    End Sub

    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        ' Ensure a record is selected
        If DeceasedList.SelectedItems.Count > 0 Then
            ' Retrieve the Deceased_ID from the Tag property of the selected item
            Dim selectedID As Integer = Convert.ToInt32(DeceasedList.SelectedItems(0).Tag)

            ' Open the update form and pass the Deceased_ID to it
            Dim updateForm As New frmUpdateDeceased(selectedID)
            updateForm.ShowDialog()

            ' After updating, refresh the list to reflect the changes
            LoadUsers()
        Else
            MessageBox.Show("Please select a record to edit.")
        End If
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        If DeceasedList.SelectedItems.Count > 0 Then
            Dim selectedID As Integer = Convert.ToInt32(DeceasedList.SelectedItems(0).Text)
            Dim result As DialogResult = MessageBox.Show("Are you sure you want to delete this record?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
            If result = DialogResult.Yes Then
                dbconn()
                cn.Open()

                Dim sql As String = "DELETE FROM deceased WHERE Deceased_ID = @ID"
                Dim cmd As New MySqlCommand(sql, cn)
                cmd.Parameters.AddWithValue("@ID", selectedID)
                cmd.ExecuteNonQuery()

                cn.Close()
                MessageBox.Show("Record deleted successfully.")
                LoadUsers()
            End If
        Else
            MessageBox.Show("Please select a record to delete.")
        End If
    End Sub

    Private Sub Guna2Panel2_Paint(sender As Object, e As PaintEventArgs) Handles Guna2Panel2.Paint

    End Sub

    Private Sub Guna2Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Guna2Panel1.Paint

    End Sub

    Private Sub DeceasedList_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DeceasedList.SelectedIndexChanged

    End Sub
End Class