Imports MySql.Data.MySqlClient
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports System.IO

Public Class frmReservationsReg
    Private Sub frmReservationList_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        KeyPreview = True
        LoadReservations()
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        ' Call the LoadReservations method with the current search text.
        LoadReservations(txtSearch.Text.Trim())
    End Sub

    Sub LoadReservations(Optional searchText As String = "")
        Dim tempConnection As MySqlConnection = Nothing
        Try
            ' Create a new connection for this operation
            tempConnection = New MySqlConnection("server=srv594.hstgr.io; database=u976878483_cemetery; username=u976878483_doncarlos; password=d0Nc4los; port=3306")
            tempConnection.Open()

            Dim reservationData As New List(Of Dictionary(Of String, Object))

            ' Modify the SQL query to include a WHERE clause for the search text
            sql = "SELECT r.Reservation_ID, r.Client_ID, " &
              "CONCAT(COALESCE(c.FirstName, ''), ' ', " &
              "COALESCE(LEFT(c.MiddleName, 1), ''), IF(COALESCE(c.MiddleName, '') <> '', '. ', ''), " &
              "COALESCE(c.LastName, '')) AS FullName, " &
              "l.block, l.section, l.row, l.plot, " &
              "pr.Plot_ID, pr.Level, " &
              "r.Reservation_Date, " &
              "r.Quantity, " &
              "CASE r.Status " &
              "    WHEN 0 THEN 'Available' " &
              "    WHEN 1 THEN 'Used' " &
              "    ELSE 'Unknown' " &
              "END AS Status, " &
              "p.total_Amount, p.total_Paid, " &
              "CASE WHEN p.total_Paid >= p.total_Amount THEN 'Fully Paid' ELSE 'Partial' END AS Payment_Status, " &
              "CASE l.type " &
              "    WHEN 1 THEN 'Apartment' " &
              "    WHEN 2 THEN 'Family Lawn Lots' " &
              "    WHEN 3 THEN 'Bone Niche' " &
              "    WHEN 4 THEN 'Private' " &
              "END AS TypeName " &
              "FROM reservation r " &
              "LEFT JOIN client c ON r.Client_ID = c.Client_ID " &
              "JOIN plot_reservation pr ON r.Reservation_ID = pr.reservation_id " &
              "JOIN payment p ON r.Reservation_ID = p.Reservation_ID " &
              "JOIN location l ON pr.Plot_ID = l.id " &
              "WHERE CONCAT(COALESCE(c.FirstName, ''), ' ', COALESCE(LEFT(c.MiddleName, 1), ''), IF(COALESCE(c.MiddleName, '') <> '', '. ', ''), COALESCE(c.LastName, '')) LIKE @search " &
              "ORDER BY r.Reservation_Date DESC, r.Reservation_ID DESC"

            ' Add the search text parameter to the command
            Using cmd As New MySqlCommand(sql, tempConnection)
                cmd.Parameters.AddWithValue("@search", "%" & searchText & "%")
                Using dr As MySqlDataReader = cmd.ExecuteReader()
                    While dr.Read()
                        Dim row As New Dictionary(Of String, Object)
                        For i As Integer = 0 To dr.FieldCount - 1
                            row.Add(dr.GetName(i), dr.GetValue(i))
                        Next
                        reservationData.Add(row)
                    End While
                End Using
            End Using

            ReservationList.Items.Clear()

            ' Now process the data and update statuses
            For Each reservation In reservationData
                Dim newLine = ReservationList.Items.Add(reservation("Reservation_ID").ToString())  ' Display Reservation_ID
                newLine.Tag = reservation("Reservation_ID")
                newLine.SubItems.Add(reservation("FullName").ToString())  ' Full Name

                ' Include plot type and level in the plot location
                Dim plotLocation As String = String.Format("{0} - Block {1}, Section {2}, Row {3}, Plot {4}, Level {5}",
                reservation("TypeName"),
                reservation("block"),
                reservation("section"),
                reservation("row"),
                reservation("plot"),
                If(reservation("Level") Is DBNull.Value, "N/A", reservation("Level").ToString()))  ' Level
                newLine.SubItems.Add(plotLocation)  ' Reserved Plot

                ' Ensure Reservation_Date is not null
                Dim reservationDate As String = If(reservation("Reservation_Date") IsNot DBNull.Value,
                Convert.ToDateTime(reservation("Reservation_Date")).ToString("MMMM dd, yyyy"), "N/A")
                newLine.SubItems.Add(reservationDate)  ' Reservation Date

                ' Handle Quantity
                newLine.SubItems.Add(If(reservation("Quantity") Is DBNull.Value, "N/A", reservation("Quantity").ToString()))  ' Quantity

                ' Handle Status
                newLine.SubItems.Add(If(reservation("Status") Is DBNull.Value, "Unknown", reservation("Status").ToString()))  ' Reservation Status

                ' Payment Status
                Dim paymentStatus As String = If(reservation("Payment_Status") Is DBNull.Value, "N/A", reservation("Payment_Status").ToString())  ' Payment Status
                newLine.SubItems.Add(paymentStatus)
            Next

        Catch ex As Exception
            MessageBox.Show("Error loading reservations: " & ex.Message & vbCrLf & "Stack Trace: " & ex.StackTrace,
        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If tempConnection IsNot Nothing Then
                If tempConnection.State = ConnectionState.Open Then
                    tempConnection.Close()
                End If
                tempConnection.Dispose()
            End If
        End Try
    End Sub

    Private Sub btnAssign_Click(sender As Object, e As EventArgs) Handles btnAssign.Click
        If ReservationList.SelectedItems.Count > 0 Then
            Try
                ' Check if the selected reservation is already used
                Dim status As String = ReservationList.SelectedItems(0).SubItems(5).Text ' Status is in the 6th column (index 5)
                If status = "Used" Then
                    MessageBox.Show("Cannot assign this reservation as it is already marked as Used.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Return
                End If

                Dim reservationID As Integer = Convert.ToInt32(ReservationList.SelectedItems(0).Tag)
                Dim clientID As Integer = GetClientIDFromReservation(reservationID)
                Dim assignForm As New frmReservAssign(reservationID, clientID)
                assignForm.ShowDialog()
                LoadReservations() ' Refresh after assignment
            Catch ex As Exception
                MessageBox.Show("Error accessing reservation: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        Else
            MessageBox.Show("Please select a reservation to assign.")
        End If
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs)
        If ReservationList.SelectedItems.Count > 0 Then
            Dim selectedID As Integer = Convert.ToInt32(ReservationList.SelectedItems(0).Tag)

            Dim result As DialogResult = MessageBox.Show("Are you sure you want to delete this reservation?",
                                                   "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
            If result = DialogResult.Yes Then
                Try
                    dbconn()
                    If cn.State = ConnectionState.Open Then
                        cn.Close()
                    End If
                    cn.Open()
                    Using transaction As MySqlTransaction = cn.BeginTransaction()
                        Try
                            ' First, check if there are any deceased assigned to this reservation
                            Dim checkCmd As New MySqlCommand("SELECT COUNT(*) FROM deceased WHERE Plot_ID IN (SELECT Plot_ID FROM plot_reservation WHERE reservation_id = @ID)", cn, transaction)
                            checkCmd.Parameters.AddWithValue("@ID", selectedID)
                            Dim deceasedCount As Integer = Convert.ToInt32(checkCmd.ExecuteScalar())

                            If deceasedCount > 0 Then
                                MessageBox.Show("Cannot delete this reservation because there are deceased persons assigned to it.",
                                          "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                transaction.Rollback()
                                Return
                            End If

                            ' First, delete related records in plot_reservation table
                            Using cmd1 As New MySqlCommand("DELETE FROM plot_reservation WHERE reservation_id = @ID", cn, transaction)
                                cmd1.Parameters.AddWithValue("@ID", selectedID)
                                cmd1.ExecuteNonQuery()
                            End Using

                            ' Then, delete the reservation
                            Using cmd2 As New MySqlCommand("DELETE FROM Reservation WHERE Reservation_ID = @ID", cn, transaction)
                                cmd2.Parameters.AddWithValue("@ID", selectedID)
                                cmd2.ExecuteNonQuery()
                            End Using

                            transaction.Commit()
                            MessageBox.Show("Reservation deleted successfully.", "Success",
                                      MessageBoxButtons.OK, MessageBoxIcon.Information)
                            LoadReservations()

                        Catch ex As Exception
                            If transaction IsNot Nothing Then
                                transaction.Rollback()
                            End If
                            Throw
                        End Try
                    End Using

                Catch ex As Exception
                    MessageBox.Show("Error deleting reservation: " & ex.Message, "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error)
                Finally
                    If cn.State = ConnectionState.Open Then
                        cn.Close()
                    End If
                End Try
            End If
        Else
            MessageBox.Show("Please select a reservation to delete.", "Warning",
                       MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    Private Sub btnShowAll_Click(sender As Object, e As EventArgs) Handles btnShowAll.Click
        LoadReservations()
    End Sub

    Private Sub btnExportPDF_Click(sender As Object, e As EventArgs) Handles btnExportPDF.Click
        Try
            Dim saveDialog As New SaveFileDialog()
            saveDialog.Filter = "PDF files (*.pdf)|*.pdf"
            saveDialog.Title = "Save Reservation List"
            saveDialog.FileName = "ReservationList_" & DateTime.Now.ToString("yyyyMMdd") & ".pdf"

            If saveDialog.ShowDialog() = DialogResult.OK Then
                ' Initialize iTextSharp with proper encoding
                Dim baseFont As BaseFont = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED)
                Dim doc As New Document(PageSize.A4.Rotate(), 40, 40, 40, 40)
                Dim writer As PdfWriter = PdfWriter.GetInstance(doc, New FileStream(saveDialog.FileName, FileMode.Create))

                doc.Open()

                ' Add title
                Dim titleFont As New Font(baseFont, 16)
                Dim title As New Paragraph("Reservation List", titleFont)
                title.Alignment = Element.ALIGN_CENTER
                title.SpacingAfter = 20
                doc.Add(title)

                ' Create table with 7 columns
                Dim table As New PdfPTable(7)
                table.WidthPercentage = 100

                ' Set relative column widths
                Dim widths As Single() = {0.8F, 1.8F, 2.5F, 1.5F, 1.5F, 1.5F, 1.2F}
                table.SetWidths(widths)

                ' Add headers
                Dim headerFont As New Font(baseFont, 10, Font.Bold)
                Dim headers() As String = {"Client ID", "Client Name", "Plot Location", "Total Paid", "Total Amount", "Reservation Date", "Payment"}

                For Each header In headers
                    Dim cell As New PdfPCell(New Phrase(header, headerFont))
                    cell.HorizontalAlignment = Element.ALIGN_CENTER
                    cell.BackgroundColor = New BaseColor(240, 240, 240)
                    cell.Padding = 5
                    table.AddCell(cell)
                Next

                ' Add data
                Dim cellFont As New Font(baseFont, 9)
                For Each item As ListViewItem In ReservationList.Items
                    If item.SubItems.Count < 7 Then  ' Ensure there are enough subitems
                        MessageBox.Show("Error: Some rows in the list have missing data.", "Data Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        Exit Sub
                    End If

                    ' Only use first 7 columns
                    For i As Integer = 0 To 6
                        Dim cellText As String = If(i < item.SubItems.Count, item.SubItems(i).Text, "N/A")
                        Dim cell As New PdfPCell(New Phrase(cellText, cellFont))
                        cell.HorizontalAlignment = If(i = 1 OrElse i = 2, Element.ALIGN_LEFT, Element.ALIGN_CENTER)
                        cell.Padding = 5
                        table.AddCell(cell)
                    Next
                Next

                doc.Add(table)

                ' Add footer with date
                Dim footer As New Paragraph("Generated on: " & DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), cellFont)
                footer.Alignment = Element.ALIGN_RIGHT
                footer.SpacingBefore = 20
                doc.Add(footer)

                doc.Close()

                MessageBox.Show("PDF file has been created successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Process.Start(saveDialog.FileName)
            End If
        Catch ex As Exception
            MessageBox.Show("Error creating PDF: " & ex.Message & vbCrLf & "Stack Trace: " & ex.StackTrace, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Function GetClientIDFromReservation(reservationID As Integer) As Integer
        Dim clientID As Integer = -1 ' Default value if not found
        Dim sql As String = "SELECT Client_ID FROM reservation WHERE Reservation_ID = @ReservationID"

        Using conn As New MySqlConnection("server=srv594.hstgr.io; database=u976878483_cemetery; username=u976878483_doncarlos; password=d0Nc4los; port=3306")
            Using cmd As New MySqlCommand(sql, conn)
                cmd.Parameters.AddWithValue("@ReservationID", reservationID)
                conn.Open()
                Dim result = cmd.ExecuteScalar()
                If result IsNot Nothing Then
                    clientID = Convert.ToInt32(result)
                End If
            End Using
        End Using

        Return clientID
    End Function

    Private Sub ReservationList_DoubleClick(sender As Object, e As EventArgs) Handles ReservationList.DoubleClick
        If ReservationList.SelectedItems.Count > 0 Then
            Try
                ' Check if the selected reservation is already used
                Dim status As String = ReservationList.SelectedItems(0).SubItems(5).Text ' Status is in the 6th column (index 5)
                If status = "Used" Then
                    MessageBox.Show("Cannot assign this reservation as it is already marked as Used.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Return
                End If

                Dim reservationID As Integer = Convert.ToInt32(ReservationList.SelectedItems(0).Tag)
                Dim clientID As Integer = GetClientIDFromReservation(reservationID)

                If clientID = -1 Then
                    MessageBox.Show("Client ID not found for the selected reservation.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return
                End If

                Dim assignForm As New frmReservAssign(reservationID, clientID)
                assignForm.ShowDialog()
                LoadReservations() ' Refresh after assignment
            Catch ex As Exception
                MessageBox.Show("Error accessing reservation: " & ex.Message & vbCrLf & "Stack Trace: " & ex.StackTrace,
                              "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub
End Class