Imports MySql.Data.MySqlClient
Imports System.IO
Imports Microsoft.Office.Interop.Excel
Imports System.Runtime.InteropServices
Imports System.Diagnostics

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

        ' Assign the context menu to the ListView
        DeceasedList.ContextMenuStrip = contextMenu

    End Sub

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

        sql = "SELECT d.Deceased_ID, d.LastName, d.FirstName, d.MiddleName, d.DateOfBirth, d.DateOfDeath, d.deceased_status, " &
          "l.Type, l.Block, l.Section, l.Row, l.Plot " &
          "FROM deceased d " &
          "JOIN location l ON d.Plot_ID = l.id " &
          whereClause & " ORDER BY d.LastName ASC"

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

                ' Format DateOfBirth
                Dim dob As String = If(dr("DateOfBirth") IsNot DBNull.Value, Convert.ToDateTime(dr("DateOfBirth")).ToString("MMMM dd, yyyy"), "N/A")
                newLine.SubItems.Add(dob)

                ' Format DateOfDeath
                Dim dod As String = If(dr("DateOfDeath") IsNot DBNull.Value, Convert.ToDateTime(dr("DateOfDeath")).ToString("MMMM dd, yyyy"), "N/A")
                newLine.SubItems.Add(dod)

                ' Convert type integer to string
                Dim typeString As String = ""
                Select Case Convert.ToInt32(dr("Type"))
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

                ' Get deceased status
                Dim status As String = If(dr("deceased_status") IsNot DBNull.Value, dr("deceased_status").ToString(), "N/A")
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

            Dim dob As String = If(dr("DateOfBirth") IsNot DBNull.Value, Convert.ToDateTime(dr("DateOfBirth")).ToString("MMMM dd, yyyy"), "N/A")
            newLine.SubItems.Add(dob)

            Dim dod As String = If(dr("DateOfDeath") IsNot DBNull.Value, Convert.ToDateTime(dr("DateOfDeath")).ToString("MMMM dd, yyyy"), "N/A")
            newLine.SubItems.Add(dod)

            Dim typeString As String = ""
            Select Case Convert.ToInt32(dr("Type"))
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
            ' Start Excel and open the workbook
            excelApp = New Application()
            workbooks = excelApp.Workbooks
            workbook = workbooks.Open(filePath)
            worksheet = DirectCast(workbook.Worksheets(1), Worksheet)

            ' Get the used range
            Dim range As Range = worksheet.UsedRange
            Dim rowCount As Integer = range.Rows.Count

            ' Check if there's at least one data row (plus header)
            If rowCount <= 1 Then
                MessageBox.Show("No data found in the Excel file.", "Empty File", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            ' Begin database transaction
            dbconn()
            cn.Open()
            Dim transaction As MySqlTransaction = cn.BeginTransaction()

            Try
                ' Process each row starting from row 2 (assuming row 1 is header)
                For rowIndex As Integer = 2 To rowCount
                    Try
                        ' Read values based on the row structure
                        Dim deceasedId As String = GetCellValue(worksheet.Cells(rowIndex, 1))
                        Dim lastName As String = GetCellValue(worksheet.Cells(rowIndex, 2))
                        Dim firstName As String = GetCellValue(worksheet.Cells(rowIndex, 3))
                        Dim middleName As String = GetCellValue(worksheet.Cells(rowIndex, 4))
                        Dim ext As String = GetCellValue(worksheet.Cells(rowIndex, 5))
                        Dim dateOfBirthStr As String = GetCellValue(worksheet.Cells(rowIndex, 6))
                        Dim dateOfDeathStr As String = GetCellValue(worksheet.Cells(rowIndex, 7))
                        Dim intermentStr As String = GetCellValue(worksheet.Cells(rowIndex, 8))
                        Dim gender As String = GetCellValue(worksheet.Cells(rowIndex, 9))
                        Dim nationality As String = GetCellValue(worksheet.Cells(rowIndex, 10))
                        Dim religion As String = GetCellValue(worksheet.Cells(rowIndex, 11))
                        Dim beneficiary1 As String = GetCellValue(worksheet.Cells(rowIndex, 12))
                        Dim beneficiary2 As String = GetCellValue(worksheet.Cells(rowIndex, 13))
                        Dim relationship As String = GetCellValue(worksheet.Cells(rowIndex, 14))
                        Dim plotId As String = GetCellValue(worksheet.Cells(rowIndex, 15))

                        ' Debugging output
                        Debug.WriteLine($"Row {rowIndex}: Deceased ID: {deceasedId}, First Name: {firstName}, Last Name: {lastName}, Date of Birth: {dateOfBirthStr}, Date of Death: {dateOfDeathStr}")

                        ' Parse dates
                        Dim dateOfBirth As Date
                        Dim dateOfDeath As Date

                        If Not Date.TryParse(dateOfBirthStr, dateOfBirth) Then
                            errorMessages.Add($"Row {rowIndex}: Invalid Date of Birth format: {dateOfBirthStr}")
                            errorCount += 1
                            Continue For
                        End If

                        If Not Date.TryParse(dateOfDeathStr, dateOfDeath) Then
                            errorMessages.Add($"Row {rowIndex}: Invalid Date of Death format: {dateOfDeathStr}")
                            errorCount += 1
                            Continue For
                        End If

                        Dim intermentDate As Date
                        If Not Date.TryParse(intermentStr, intermentDate) Then
                            errorMessages.Add($"Row {rowIndex}: Invalid Interment date format: {intermentStr}")
                            errorCount += 1
                            Continue For
                        End If

                        ' Insert into database with all fields
                        sql = "INSERT INTO deceased (Deceased_ID, FirstName, MiddleName, LastName, DateOfBirth, DateOfDeath, Plot_ID, " &
                              "Ext, Interment, Gender, Nationality, Religion, Beneficiary1, Beneficiary2, Relationship) " &
                              "VALUES (@DeceasedID, @FirstName, @MiddleName, @LastName, @DateOfBirth, @DateOfDeath, @PlotID, " &
                              "@Ext, @Interment, @Gender, @Nationality, @Religion, @Beneficiary1, @Beneficiary2, @Relationship)"

                        Using cmd As New MySqlCommand(sql, cn, transaction)
                            ' Add essential parameters
                            cmd.Parameters.AddWithValue("@DeceasedID", If(String.IsNullOrWhiteSpace(deceasedId) OrElse Not IsNumeric(deceasedId), DBNull.Value, Convert.ToInt32(deceasedId)))
                            cmd.Parameters.AddWithValue("@FirstName", firstName)
                            cmd.Parameters.AddWithValue("@LastName", lastName)
                            cmd.Parameters.AddWithValue("@MiddleName", If(String.IsNullOrWhiteSpace(middleName), DBNull.Value, middleName))
                            cmd.Parameters.AddWithValue("@DateOfBirth", dateOfBirth)
                            cmd.Parameters.AddWithValue("@DateOfDeath", dateOfDeath)
                            cmd.Parameters.AddWithValue("@PlotID", If(String.IsNullOrWhiteSpace(plotId) OrElse Not IsNumeric(plotId), DBNull.Value, Convert.ToInt32(plotId)))
                            cmd.Parameters.AddWithValue("@Ext", If(String.IsNullOrWhiteSpace(ext), DBNull.Value, ext))
                            cmd.Parameters.AddWithValue("@Interment", intermentDate)
                            cmd.Parameters.AddWithValue("@Gender", If(String.IsNullOrWhiteSpace(gender), DBNull.Value, gender))
                            cmd.Parameters.AddWithValue("@Nationality", If(String.IsNullOrWhiteSpace(nationality), DBNull.Value, nationality))
                            cmd.Parameters.AddWithValue("@Religion", If(String.IsNullOrWhiteSpace(religion), DBNull.Value, religion))
                            cmd.Parameters.AddWithValue("@Beneficiary1", If(String.IsNullOrWhiteSpace(beneficiary1), DBNull.Value, beneficiary1))
                            cmd.Parameters.AddWithValue("@Beneficiary2", If(String.IsNullOrWhiteSpace(beneficiary2), DBNull.Value, beneficiary2))
                            cmd.Parameters.AddWithValue("@Relationship", If(String.IsNullOrWhiteSpace(relationship), DBNull.Value, relationship))

                            cmd.ExecuteNonQuery()
                            successCount += 1
                        End Using
                    Catch ex As Exception
                        errorMessages.Add($"Row {rowIndex}: {ex.Message}")
                        errorCount += 1
                    End Try
                Next

                transaction.Commit()
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

        Finally
            ' Clean up Excel objects
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

            ' Close database connection
            If cn IsNot Nothing AndAlso cn.State = ConnectionState.Open Then
                cn.Close()
            End If

            ' Force garbage collection
            GC.Collect()
            GC.WaitForPendingFinalizers()
        End Try
    End Sub

    Private Function GetCellValue(cell As Range) As String
        If cell IsNot Nothing AndAlso cell.Value IsNot Nothing Then
            Return cell.Value.ToString().Trim()
        End If
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
End Class