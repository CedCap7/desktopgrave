Imports MySql.Data.MySqlClient
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports System.IO

Public Class frmReservationsReg
    Private Sub frmReservationList_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        DateTimePicker1 = New DateTimePicker()
        DateTimePicker1.Format = DateTimePickerFormat.Custom
        DateTimePicker1.CustomFormat = "MMMM dd, yyyy" ' Set custom format
        DateTimePicker1.Location = New Point(50, 50)
        Me.Controls.Add(DateTimePicker1)

        KeyPreview = True
        LoadReservations()
    End Sub

    Sub LoadReservations()
        Dim tempConnection As MySqlConnection = Nothing
        Try
            ' Create a new connection for this operation
            tempConnection = New MySqlConnection("server=localhost; user=root; password=root; database=dccms")
            tempConnection.Open()

            Dim reservationData As New List(Of Dictionary(Of String, Object))

            sql = "SELECT r.Reservation_ID, r.Client_ID, " &
              "CONCAT(COALESCE(c.FirstName, ''), ' ', " &
              "COALESCE(LEFT(c.MiddleName, 1), ''), '. ', " &
              "COALESCE(c.LastName, '')) AS FullName, " &
              "l.block, l.section, l.row, l.plot, " &
              "pr.Plot_ID, pr.Level, " &  ' Include Plot_ID and Level from plot_reservation table
              "r.Reservation_Date, " &
              "r.Quantity, " &  ' Move Quantity before Status
              "CASE r.Status " &  ' Convert Status from integer to string
              "    WHEN 0 THEN 'Available' " &
              "    WHEN 1 THEN 'Used' " &
              "    ELSE 'Unknown' " &  ' Fallback for unexpected values
              "END AS Status, " &  ' Determine reservation status
              "p.total_Amount, p.total_Paid, " &  ' Include total amount and total paid
              "CASE WHEN p.total_Paid >= p.total_Amount THEN 'Fully Paid' ELSE 'Partial' END AS Payment_Status, " &  ' Determine payment status
              "CASE l.type " &  ' Determine plot type
              "    WHEN 1 THEN 'Apartment' " &
              "    WHEN 2 THEN 'Family Lawn Lots' " &
              "    WHEN 3 THEN 'Bone Niche' " &
              "    WHEN 4 THEN 'Private' " &
              "END AS TypeName " &
              "FROM Reservation r " &
              "LEFT JOIN Client c ON r.Client_ID = c.Client_ID " &
              "JOIN plot_reservation pr ON r.Reservation_ID = pr.reservation_id " &  ' Join with plot_reservation table
              "JOIN payment p ON r.Reservation_ID = p.Reservation_ID " &  ' Join with payment table
              "JOIN location l ON pr.Plot_ID = l.id " &  ' Join with location to get plot details from plot_reservation
              "ORDER BY r.Reservation_Date DESC, r.Reservation_ID DESC"

            ' First get all the data
            Using cmd As New MySqlCommand(sql, tempConnection)
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
                newLine.SubItems.Add(reservation("FullName").ToString())  ' Client Name

                ' Include plot type and level in the plot location
                Dim plotLocation As String = String.Format("{0} - Block {1}, Section {2}, Row {3}, Plot {4}, Plot ID {5}, Level {6}",
                reservation("TypeName"),
                reservation("block"),
                reservation("section"),
                reservation("row"),
                reservation("plot"),
                If(reservation("Plot_ID") Is DBNull.Value, "N/A", reservation("Plot_ID").ToString()),  ' Plot ID
                If(reservation("Level") Is DBNull.Value, "N/A", reservation("Level").ToString()))  ' Level
                newLine.SubItems.Add(plotLocation)  ' Reserved Plot

                Dim reservationDate As String = If(reservation("Reservation_Date") IsNot DBNull.Value,
                Convert.ToDateTime(reservation("Reservation_Date")).ToString("MMMM dd, yyyy"), "N/A")
                newLine.SubItems.Add(reservationDate)  ' Reservation Date

                newLine.SubItems.Add(reservation("Quantity").ToString())  ' Quantity
                newLine.SubItems.Add(reservation("Status").ToString())  ' Reservation Status

                ' Payment Status
                Dim paymentStatus As String = reservation("Payment_Status").ToString()  ' Payment Status
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
                Dim reservationID As Integer = Convert.ToInt32(ReservationList.SelectedItems(0).Tag)
                Dim clientID As Integer = GetClientIDFromReservation(reservationID) ' Implement this method to fetch ClientID
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

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
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

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        LoadReservationsBySearchAndDate()
    End Sub

    Private Sub LoadReservationsBySearchAndDate()
        Try
            dbconn()
            If cn.State = ConnectionState.Open Then
                cn.Close()
            End If
            cn.Open()

            Dim selectedDate As String = DateTimePicker1.Value.ToString("yyyy-MM-dd")
            Dim reservationData As New List(Of Dictionary(Of String, Object))

            sql = "SELECT r.Reservation_ID, r.Client_ID, " &
              "CONCAT(COALESCE(c.FirstName, ''), ' ', " &
              "COALESCE(LEFT(c.MiddleName, 1), ''), '. ', " &
              "COALESCE(c.LastName, '')) AS FullName, " &
              "p.total_Amount, p.total_Paid, " &
              "r.Reservation_Date, r.Status, r.Quantity, " &
              "CASE l.type " &
              "    WHEN 1 THEN 'Apartment' " &
              "    WHEN 2 THEN 'Family Lawn Lots' " &
              "    WHEN 3 THEN 'Bone Niche' " &
              "    WHEN 4 THEN 'Private' " &
              "END AS TypeName, " &
              "l.block, l.section, l.row, l.plot, " &
              "pr.Plot_ID, pr.Level, " &
              "(SELECT COUNT(*) FROM deceased d WHERE d.Plot_ID = pr.Plot_ID) as deceased_count " &
              "FROM Reservation r " &
              "LEFT JOIN Client c ON r.Client_ID = c.Client_ID " &
              "JOIN plot_reservation pr ON r.Reservation_ID = pr.reservation_id " &
              "JOIN payment p ON r.Reservation_ID = p.Reservation_ID " &
              "JOIN location l ON pr.Plot_ID = l.id " &
              "WHERE CONCAT(c.FirstName, ' ', LEFT(c.MiddleName, 1), '. ', c.LastName) LIKE @search " &
              "AND DATE(r.Reservation_Date) = @selectedDate " &
              "ORDER BY r.Reservation_Date DESC, r.Reservation_ID DESC"

            Using cmd As New MySqlCommand(sql, cn)
                cmd.Parameters.AddWithValue("@search", "%" & txtSearch.Text & "%")
                cmd.Parameters.AddWithValue("@selectedDate", selectedDate)
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
            For Each reservation In reservationData
                Dim newLine = ReservationList.Items.Add(reservation("Client_ID").ToString())
                newLine.Tag = reservation("Reservation_ID")  ' Store Reservation_ID in Tag
                newLine.SubItems.Add(reservation("FullName").ToString())

                Dim plotLocation As String = String.Format("{0}, Block {1}, Section {2}, Row {3}, Plot {4}, Plot ID {5}, Level {6}",
            reservation("TypeName"),
            reservation("block"),
            reservation("section"),
            reservation("row"),
            reservation("plot"),
            If(reservation("Plot_ID") Is DBNull.Value, "N/A", reservation("Plot_ID").ToString()),  ' Plot ID
            If(reservation("Level") Is DBNull.Value, "N/A", reservation("Level").ToString()))  ' Level
                newLine.SubItems.Add(plotLocation)

                Dim totalPaid As Decimal = If(reservation("total_Paid") Is DBNull.Value, 0D, Convert.ToDecimal(reservation("total_Paid")))
                newLine.SubItems.Add(Format(totalPaid, "#,##0.00"))

                Dim totalAmount As Decimal = Convert.ToDecimal(reservation("total_Amount"))
                newLine.SubItems.Add(Format(totalAmount, "#,##0.00"))

                Dim reservationDate As String = If(reservation("Reservation_Date") IsNot DBNull.Value,
            Convert.ToDateTime(reservation("Reservation_Date")).ToString("MMMM dd, yyyy"), "N/A")
                newLine.SubItems.Add(reservationDate)

                newLine.SubItems.Add(reservation("Quantity").ToString())

                Dim deceasedCount As Integer = Convert.ToInt32(reservation("deceased_count"))
                Dim usageStatus As String = If(deceasedCount > 0, "Used", "Available")
                newLine.SubItems.Add(usageStatus)

                Dim paymentStatus As String = If(totalPaid >= totalAmount, "Paid", "Partial")
                newLine.SubItems.Add(paymentStatus)
            Next

        Catch ex As Exception
            MessageBox.Show("Error searching reservations: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If cn.State = ConnectionState.Open Then
                cn.Close()
            End If
        End Try
    End Sub

    Private Sub DateTimePicker1_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker1.ValueChanged
        LoadReservationsBySearchAndDate()
    End Sub
    Sub LoadReservationsByDate()
        Try
            dbconn()
            If cn.State = ConnectionState.Open Then
                cn.Close()
            End If
            cn.Open()

            Dim selectedDate As String = DateTimePicker1.Value.ToString("yyyy-MM-dd")
            Dim reservationData As New List(Of Dictionary(Of String, Object))

            sql = "SELECT r.Reservation_ID, r.Client_ID, " &
              "CONCAT(COALESCE(c.FirstName, ''), ' ', " &
              "COALESCE(LEFT(c.MiddleName, 1), ''), '. ', " &
              "COALESCE(c.LastName, '')) AS FullName, " &
              "p.total_Amount, p.total_Paid, " &  ' Changed from r. to p.
              "r.Reservation_Date, r.Status, r.Quantity, " &
              "CASE l.type " &
              "    WHEN 1 THEN 'Apartment' " &
              "    WHEN 2 THEN 'Family Lawn Lots' " &
              "    WHEN 3 THEN 'Bone Niche' " &
              "    WHEN 4 THEN 'Private' " &
              "END AS TypeName, " &
              "l.block, l.section, l.row, l.plot, " &
              "(SELECT COUNT(*) FROM deceased d WHERE d.Plot_ID = p.Plot_ID) as deceased_count " &  ' Use p.Plot_ID here
              "FROM Reservation r " &
              "LEFT JOIN Client c ON r.Client_ID = c.Client_ID " &
              "JOIN location l ON p.Plot_ID = l.id " &  ' Use p.Plot_ID here
              "JOIN payment p ON r.Reservation_ID = p.Reservation_ID " &  ' Added JOIN with payment table
              "WHERE DATE(r.Reservation_Date) = @selectedDate " &
              "ORDER BY r.Reservation_Date DESC, r.Reservation_ID DESC"

            Using cmd As New MySqlCommand(sql, cn)
                cmd.Parameters.AddWithValue("@selectedDate", selectedDate)
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
            For Each reservation In reservationData
                Dim newLine = ReservationList.Items.Add(reservation("Client_ID").ToString())
                newLine.Tag = reservation("Reservation_ID")  ' Store Reservation_ID in Tag
                newLine.SubItems.Add(reservation("FullName").ToString())

                Dim plotLocation As String = String.Format("{0}, Block {1}, Section {2}, Row {3}, Plot {4}",
                reservation("TypeName"),
                reservation("block"),
                reservation("section"),
                reservation("row"),
                reservation("plot"))
                newLine.SubItems.Add(plotLocation)

                Dim totalPaid As Decimal = If(reservation("total_Paid") Is DBNull.Value, 0D, Convert.ToDecimal(reservation("total_Paid")))
                newLine.SubItems.Add(Format(totalPaid, "#,##0.00"))

                Dim totalAmount As Decimal = Convert.ToDecimal(reservation("total_Amount"))
                newLine.SubItems.Add(Format(totalAmount, "#,##0.00"))

                Dim reservationDate As String = If(reservation("Reservation_Date") IsNot DBNull.Value,
                Convert.ToDateTime(reservation("Reservation_Date")).ToString("MMMM dd, yyyy"), "N/A")
                newLine.SubItems.Add(reservationDate)

                newLine.SubItems.Add(reservation("Quantity").ToString())

                Dim deceasedCount As Integer = Convert.ToInt32(reservation("deceased_count"))
                Dim usageStatus As String = If(deceasedCount > 0, "Used", "Available")
                newLine.SubItems.Add(usageStatus)

                Dim paymentStatus As String = If(totalPaid >= totalAmount, "Paid", "Partial")
                newLine.SubItems.Add(paymentStatus)
            Next

        Catch ex As Exception
            MessageBox.Show("Error loading reservations by date: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If cn.State = ConnectionState.Open Then
                cn.Close()
            End If
        End Try
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
                Dim doc As New Document(PageSize.A4.Rotate(), 40, 40, 40, 40)
                Dim writer As PdfWriter = PdfWriter.GetInstance(doc, New FileStream(saveDialog.FileName, FileMode.Create))

                doc.Open()

                ' Add title
                Dim titleFont As New Font(BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1252, BaseFont.EMBEDDED), 16)
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
                Dim headerFont As New Font(BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1252, BaseFont.EMBEDDED), 10)
                Dim headers() As String = {"Client ID", "Client Name", "Plot Location", "Total Paid", "Total Amount", "Reservation Date", "Payment"}

                For Each header In headers
                    Dim cell As New PdfPCell(New Phrase(header, headerFont))
                    cell.HorizontalAlignment = Element.ALIGN_CENTER
                    cell.BackgroundColor = BaseColor.LIGHT_GRAY
                    cell.Padding = 5
                    table.AddCell(cell)
                Next

                ' Add data
                Dim cellFont As New Font(BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.EMBEDDED), 9)
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
                Dim footer As New Paragraph("Generated on: " & DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
                footer.Alignment = Element.ALIGN_RIGHT
                footer.SpacingBefore = 20
                doc.Add(footer)

                doc.Close()

                MessageBox.Show("PDF file has been created successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Process.Start(saveDialog.FileName)
            End If
        Catch ex As Exception
            MessageBox.Show("Error creating PDF: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Function GetClientIDFromReservation(reservationID As Integer) As Integer
        Dim clientID As Integer = -1 ' Default value if not found
        Dim sql As String = "SELECT Client_ID FROM reservation WHERE Reservation_ID = @ReservationID"

        Using conn As New MySqlConnection("server=localhost; user=root; password=root; database=dccms")
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
                Dim reservationID As Integer = Convert.ToInt32(ReservationList.SelectedItems(0).Tag)
                Dim clientID As Integer = GetClientIDFromReservation(reservationID) ' Fetch ClientID

                If clientID = -1 Then
                    MessageBox.Show("Client ID not found for the selected reservation.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return
                End If

                Dim assignForm As New frmReservAssign(reservationID, clientID) ' Pass both parameters
                assignForm.ShowDialog()
                LoadReservations() ' Refresh after assignment
            Catch ex As Exception
                MessageBox.Show("Error accessing reservation: " & ex.Message & vbCrLf & "Stack Trace: " & ex.StackTrace,
                              "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub
End Class