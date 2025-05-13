Imports MySql.Data.MySqlClient
Imports System.IO
Imports System.Diagnostics
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Public Class frmPaymentReg
    Private Sub frmPaymentReg_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadPaymentData()
    End Sub

    Private Sub LoadPaymentData(Optional filterText As String = "")
        Dim tempConnection As MySqlConnection = Nothing
        Try
            ' Create a new connection for this operation
            tempConnection = New MySqlConnection("server=srv594.hstgr.io; database=u976878483_cemetery; username=u976878483_doncarlos; password=d0Nc4los; port=3306")
            tempConnection.Open()

            ' Start building the SQL query
            Dim sql As String = "
        SELECT 
            c.Client_ID, 
            CONCAT(COALESCE(c.FirstName, ''), ' ', COALESCE(LEFT(c.MiddleName, 1), ''), '. ', COALESCE(c.LastName, '')) AS FullName,
            COUNT(r.Reservation_ID) AS ReservationCount,
            MAX(p.payment_date) AS LastPaymentDate,
            CASE 
                WHEN SUM(p.total_Paid) < SUM(p.total_Amount) THEN 'Partial' 
                ELSE 'Fully Paid' 
            END AS PaymentStatus
        FROM 
            client c
        LEFT JOIN 
            reservation r ON c.Client_ID = r.Client_ID
        LEFT JOIN 
            payment p ON r.Reservation_ID = p.Reservation_ID"

            ' Add the filter if there is any text entered in the search box
            If Not String.IsNullOrWhiteSpace(filterText) Then
                sql &= " WHERE CONCAT(COALESCE(c.FirstName, ''), ' ', COALESCE(LEFT(c.MiddleName, 1), ''), '. ', COALESCE(c.LastName, '')) LIKE @FilterText"
            End If

            ' Continue with the grouping and ordering part of the query
            sql &= "
        GROUP BY 
            c.Client_ID, FullName
        ORDER BY 
            MAX(p.payment_date) DESC, c.Client_ID"

            ' First get all the data
            Using cmd As New MySqlCommand(sql, tempConnection)
                ' Add the filter parameter if applicable
                If Not String.IsNullOrWhiteSpace(filterText) Then
                    cmd.Parameters.AddWithValue("@FilterText", "%" & filterText & "%")
                End If

                Using dr As MySqlDataReader = cmd.ExecuteReader()
                    PaymentList.Items.Clear() ' Clear existing items
                    While dr.Read()
                        Dim newLine = PaymentList.Items.Add(dr("Client_ID").ToString())  ' Display Client_ID
                        newLine.Tag = dr("Client_ID")  ' Store Client_ID in Tag
                        newLine.SubItems.Add(dr("FullName").ToString())  ' Full Name
                        newLine.SubItems.Add(dr("ReservationCount").ToString())  ' Count of Reservations

                        ' Set the Payment Status and LastPaymentDate
                        Dim paymentStatus As String
                        Dim lastPaymentDate As String

                        ' Check if ReservationCount is 0
                        If Convert.ToInt32(dr("ReservationCount")) = 0 Then
                            paymentStatus = "No Accountabilities"
                            lastPaymentDate = "N/A"
                        Else
                            paymentStatus = dr("PaymentStatus").ToString()
                            lastPaymentDate = If(Not IsDBNull(dr("LastPaymentDate")), Convert.ToDateTime(dr("LastPaymentDate")).ToString("MM/dd/yyyy"), "N/A")
                        End If

                        ' Add the payment status and payment date
                        newLine.SubItems.Add(paymentStatus)  ' Payment Status
                        newLine.SubItems.Add(lastPaymentDate)  ' Payment Date
                    End While
                End Using
            End Using

        Catch ex As Exception
            MessageBox.Show("Error loading payment data: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If tempConnection IsNot Nothing Then
                If tempConnection.State = ConnectionState.Open Then
                    tempConnection.Close()
                End If
                tempConnection.Dispose()
            End If
        End Try
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        ' Call LoadPaymentData with the current value in the search box
        LoadPaymentData(txtSearch.Text)
    End Sub

    ' Handle DoubleClick Event to Open frmViewPayment
    Private Sub PaymentList_DoubleClick(sender As Object, e As EventArgs) Handles PaymentList.DoubleClick
        If PaymentList.SelectedItems.Count > 0 Then
            Dim selectedClientID As String = PaymentList.SelectedItems(0).Text
            Dim viewPaymentForm As New frmViewPayment(selectedClientID)
            viewPaymentForm.ShowDialog()
        Else
            MessageBox.Show("Please select a client first.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    Private Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        ' Create a date filter form/dialog
        Dim dateFrom As Date = Nothing
        Dim dateTo As Date = Nothing

        ' Simple date range dialog (you may want to create a proper form for this)
        Using dateDialog As New Form()
            dateDialog.Text = "Select Date Range"
            dateDialog.Size = New Size(350, 200)
            dateDialog.FormBorderStyle = FormBorderStyle.FixedDialog
            dateDialog.StartPosition = FormStartPosition.CenterParent

            ' From date
            Dim lblFrom As New Label() With {.Text = "From Date:", .Location = New Point(20, 20), .AutoSize = True}
            Dim dtpFrom As New DateTimePicker() With {.Location = New Point(100, 20), .Format = DateTimePickerFormat.Short}

            ' To date
            Dim lblTo As New Label() With {.Text = "To Date:", .Location = New Point(20, 50), .AutoSize = True}
            Dim dtpTo As New DateTimePicker() With {.Location = New Point(100, 50), .Format = DateTimePickerFormat.Short}
            dtpTo.Value = DateTime.Now

            ' Buttons
            Dim btnOk As New Button() With {.Text = "OK", .Location = New Point(100, 100), .DialogResult = DialogResult.OK}
            Dim btnCancel As New Button() With {.Text = "Cancel", .Location = New Point(200, 100), .DialogResult = DialogResult.Cancel}

            dateDialog.Controls.AddRange(New Control() {lblFrom, dtpFrom, lblTo, dtpTo, btnOk, btnCancel})
            dateDialog.AcceptButton = btnOk
            dateDialog.CancelButton = btnCancel

            If dateDialog.ShowDialog() = DialogResult.OK Then
                dateFrom = dtpFrom.Value.Date
                dateTo = dtpTo.Value.Date.AddDays(1).AddSeconds(-1) ' End of the selected day
                ExportPaymentsToPdf(dateFrom, dateTo)
            End If
        End Using
    End Sub

    Private Sub ExportPaymentsToPdf(fromDate As Date, toDate As Date)
        Dim tempConnection As MySqlConnection = Nothing
        Try
            ' Ask user where to save the PDF
            Dim saveDialog As New SaveFileDialog()
            saveDialog.Filter = "PDF Files (*.pdf)|*.pdf"
            saveDialog.Title = "Save Payment Report"
            saveDialog.FileName = $"Payment_Report_{fromDate:yyyyMMdd}_to_{toDate:yyyyMMdd}.pdf"

            If saveDialog.ShowDialog() <> DialogResult.OK Then
                Return
            End If

            ' Get filtered payment data
            tempConnection = New MySqlConnection("server=srv594.hstgr.io; database=u976878483_cemetery; username=u976878483_doncarlos; password=d0Nc4los; port=3306")
            tempConnection.Open()

            Dim sql As String = "
                SELECT 
                    c.Client_ID, 
                    CONCAT(COALESCE(c.FirstName, ''), ' ', COALESCE(LEFT(c.MiddleName, 1), ''), '. ', COALESCE(c.LastName, '')) AS FullName,
                    r.Reservation_ID,
                    p.payment_date,
                    p.payment_status AS payment_type,
                    p.total_Paid,
                    p.total_Amount,
                    CASE 
                        WHEN p.total_Paid < p.total_Amount THEN 'Partial' 
                        ELSE 'Fully Paid' 
                    END AS PaymentStatus
                FROM 
                    client c
                INNER JOIN 
                    reservation r ON c.Client_ID = r.Client_ID
                INNER JOIN 
                    payment p ON r.Reservation_ID = p.Reservation_ID
                WHERE
                    p.payment_date BETWEEN @FromDate AND @ToDate
                ORDER BY 
                    p.payment_date, c.Client_ID"

            Dim dataTable As New DataTable()

            Using adapter As New MySqlDataAdapter(sql, tempConnection)
                adapter.SelectCommand.Parameters.AddWithValue("@FromDate", fromDate)
                adapter.SelectCommand.Parameters.AddWithValue("@ToDate", toDate)
                adapter.Fill(dataTable)
            End Using

            ' Generate PDF using iTextSharp (make sure to add the reference)
            ' You need to add iTextSharp via NuGet or reference the DLL
            Dim document As New iTextSharp.text.Document()
            Dim writer As iTextSharp.text.pdf.PdfWriter = iTextSharp.text.pdf.PdfWriter.GetInstance(document, New System.IO.FileStream(saveDialog.FileName, System.IO.FileMode.Create))

            ' Set document properties
            document.Open()
            document.AddTitle("Payment Report")
            document.AddAuthor("DCCMS System")
            document.AddCreationDate()

            ' Add header
            Dim headerFont As iTextSharp.text.Font = iTextSharp.text.FontFactory.GetFont(iTextSharp.text.FontFactory.HELVETICA_BOLD, 16)
            Dim titleParagraph As New iTextSharp.text.Paragraph("Payment Report", headerFont)
            titleParagraph.Alignment = iTextSharp.text.Element.ALIGN_CENTER
            document.Add(titleParagraph)

            ' Add date range info
            Dim infoFont As iTextSharp.text.Font = iTextSharp.text.FontFactory.GetFont(iTextSharp.text.FontFactory.HELVETICA, 12)
            Dim dateParagraph As New iTextSharp.text.Paragraph($"Date Range: {fromDate:MM/dd/yyyy} to {toDate:MM/dd/yyyy}", infoFont)
            dateParagraph.Alignment = iTextSharp.text.Element.ALIGN_CENTER
            document.Add(dateParagraph)
            document.Add(New iTextSharp.text.Paragraph(" ")) ' Blank line

            ' Create table
            Dim table As New iTextSharp.text.pdf.PdfPTable(6)
            table.WidthPercentage = 100

            ' Table headers
            Dim cellFont As iTextSharp.text.Font = iTextSharp.text.FontFactory.GetFont(iTextSharp.text.FontFactory.HELVETICA_BOLD, 10)
            Dim headers As String() = {"Client ID", "Name", "Reservation ID", "Payment Date", "Amount Paid", "Status"}

            For Each header In headers
                Dim cell As New iTextSharp.text.pdf.PdfPCell(New iTextSharp.text.Phrase(header, cellFont))
                cell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER
                cell.BackgroundColor = New iTextSharp.text.BaseColor(220, 220, 220)
                table.AddCell(cell)
            Next

            ' Add data rows
            Dim normalFont As iTextSharp.text.Font = iTextSharp.text.FontFactory.GetFont(iTextSharp.text.FontFactory.HELVETICA, 10)

            Dim totalPaid As Decimal = 0
            Dim totalDue As Decimal = 0

            For Each row As DataRow In dataTable.Rows
                table.AddCell(New iTextSharp.text.Phrase(row("Client_ID").ToString(), normalFont))
                table.AddCell(New iTextSharp.text.Phrase(row("FullName").ToString(), normalFont))
                table.AddCell(New iTextSharp.text.Phrase(row("Reservation_ID").ToString(), normalFont))

                ' Safely handle DBNull for payment_date, total_Paid, total_Amount, and PaymentStatus
                Dim paymentDateStr As String = If(IsDBNull(row("payment_date")), "N/A", Convert.ToDateTime(row("payment_date")).ToString("MM/dd/yyyy"))
                Dim totalPaidVal As Decimal = If(IsDBNull(row("total_Paid")), 0D, Convert.ToDecimal(row("total_Paid")))
                Dim totalAmountVal As Decimal = If(IsDBNull(row("total_Amount")), 0D, Convert.ToDecimal(row("total_Amount")))
                Dim paymentStatusStr As String = If(IsDBNull(row("PaymentStatus")), "N/A", row("PaymentStatus").ToString())

                table.AddCell(New iTextSharp.text.Phrase(paymentDateStr, normalFont))
                table.AddCell(New iTextSharp.text.Phrase(totalPaidVal.ToString("C"), normalFont))
                table.AddCell(New iTextSharp.text.Phrase(paymentStatusStr, normalFont))

                totalPaid += totalPaidVal
                totalDue += totalAmountVal
            Next

            document.Add(table)

            ' Add summary information
            document.Add(New iTextSharp.text.Paragraph(" ")) ' Blank line
            Dim summaryFont As iTextSharp.text.Font = iTextSharp.text.FontFactory.GetFont(iTextSharp.text.FontFactory.HELVETICA_BOLD, 12)
            document.Add(New iTextSharp.text.Paragraph($"Total Amount Paid: {totalPaid:C}", summaryFont))
            document.Add(New iTextSharp.text.Paragraph($"Total Amount Due: {totalDue:C}", summaryFont))
            document.Add(New iTextSharp.text.Paragraph($"Balance: {(totalDue - totalPaid):C}", summaryFont))

            ' Close document
            document.Close()
            writer.Close()

            MessageBox.Show($"Payment report has been saved to {saveDialog.FileName}", "Export Successful", MessageBoxButtons.OK, MessageBoxIcon.Information)

            ' Open the PDF file
            Process.Start(saveDialog.FileName)

        Catch ex As Exception
            MessageBox.Show("Error exporting to PDF: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If tempConnection IsNot Nothing Then
                If tempConnection.State = ConnectionState.Open Then
                    tempConnection.Close()
                End If
                tempConnection.Dispose()
            End If
        End Try
    End Sub

    Private Sub btnShowAll_Click(sender As Object, e As EventArgs) Handles btnShowAll.Click
        LoadPaymentData() ' Refresh the ListView with the latest data
    End Sub
End Class
