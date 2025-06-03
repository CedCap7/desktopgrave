Imports MySql.Data.MySqlClient
Imports System.IO
Imports System.Diagnostics

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
            LastPaymentDate DESC, c.Client_ID"


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
                ExportPaymentsReport(dateFrom, dateTo)
            End If
        End Using
    End Sub

    Private Sub ExportPaymentsReport(fromDate As Date, toDate As Date)
        Dim tempConnection As MySqlConnection = Nothing
        Try
            ' Ask user for export format
            Dim exportDialog As New Form()
            exportDialog.Text = "Select Export Format"
            exportDialog.Size = New Size(300, 150)
            exportDialog.FormBorderStyle = FormBorderStyle.FixedDialog
            exportDialog.StartPosition = FormStartPosition.CenterParent

            Dim lblFormat As New Label() With {.Text = "Choose export format:", .Location = New Point(20, 20), .AutoSize = True}
            Dim rbCSV As New RadioButton() With {.Text = "CSV (Excel compatible)", .Location = New Point(20, 50), .Checked = True}
            Dim rbHTML As New RadioButton() With {.Text = "HTML (Print to PDF)", .Location = New Point(20, 70)}

            Dim btnOk As New Button() With {.Text = "Export", .Location = New Point(100, 100), .DialogResult = DialogResult.OK}
            Dim btnCancel As New Button() With {.Text = "Cancel", .Location = New Point(180, 100), .DialogResult = DialogResult.Cancel}

            exportDialog.Controls.AddRange(New Control() {lblFormat, rbCSV, rbHTML, btnOk, btnCancel})

            If exportDialog.ShowDialog() <> DialogResult.OK Then
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

            If dataTable.Rows.Count = 0 Then
                MessageBox.Show("No payment data found for the selected date range.", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            ' Export based on selected format
            If rbCSV.Checked Then
                ExportToCSV(dataTable, fromDate, toDate)
            Else
                ExportToHTML(dataTable, fromDate, toDate)
            End If

        Catch ex As Exception
            MessageBox.Show("Error exporting payment report: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If tempConnection IsNot Nothing Then
                If tempConnection.State = ConnectionState.Open Then
                    tempConnection.Close()
                End If
                tempConnection.Dispose()
            End If
        End Try
    End Sub

    Private Sub ExportToCSV(dataTable As DataTable, fromDate As Date, toDate As Date)
        Try
            Dim saveDialog As New SaveFileDialog()
            saveDialog.Filter = "CSV Files (*.csv)|*.csv"
            saveDialog.Title = "Save Payment Report"
            saveDialog.FileName = $"Payment_Report_{fromDate:yyyyMMdd}_to_{toDate:yyyyMMdd}.csv"

            If saveDialog.ShowDialog() <> DialogResult.OK Then
                Return
            End If

            Using writer As New StreamWriter(saveDialog.FileName)
                ' Write header with date range info
                writer.WriteLine($"Payment Report - {fromDate:MMMM dd, yyyy} to {toDate:MMMM dd, yyyy}")
                writer.WriteLine($"Generated on: {DateTime.Now:MMMM dd, yyyy hh:mm tt}")
                writer.WriteLine() ' Empty line

                ' Write column headers
                writer.WriteLine("Client ID,Client Name,Reservation ID,Payment Date,Payment Type,Amount Paid,Total Amount,Payment Status")

                ' Write data
                For Each row As DataRow In dataTable.Rows
                    Dim values As New List(Of String)

                    values.Add(If(IsDBNull(row("Client_ID")), "", row("Client_ID").ToString()))
                    values.Add(If(IsDBNull(row("FullName")), "", row("FullName").ToString()))
                    values.Add(If(IsDBNull(row("Reservation_ID")), "", row("Reservation_ID").ToString()))

                    Dim paymentDate As String = If(IsDBNull(row("payment_date")), "N/A", Convert.ToDateTime(row("payment_date")).ToString("MM/dd/yyyy"))
                    values.Add(paymentDate)

                    values.Add(If(IsDBNull(row("payment_type")), "", row("payment_type").ToString()))

                    Dim totalPaid As String = If(IsDBNull(row("total_Paid")), "0", Convert.ToDecimal(row("total_Paid")).ToString("F2"))
                    values.Add(totalPaid)

                    Dim totalAmount As String = If(IsDBNull(row("total_Amount")), "0", Convert.ToDecimal(row("total_Amount")).ToString("F2"))
                    values.Add(totalAmount)

                    values.Add(If(IsDBNull(row("PaymentStatus")), "N/A", row("PaymentStatus").ToString()))

                    ' Escape values that contain commas
                    For i As Integer = 0 To values.Count - 1
                        If values(i).Contains(",") OrElse values(i).Contains("""") Then
                            values(i) = """" & values(i).Replace("""", """""") & """"
                        End If
                    Next

                    writer.WriteLine(String.Join(",", values))
                Next

                ' Write summary
                writer.WriteLine() ' Empty line
                Dim totalPayments As Decimal = 0
                For Each row As DataRow In dataTable.Rows
                    If Not IsDBNull(row("total_Paid")) Then
                        totalPayments += Convert.ToDecimal(row("total_Paid"))
                    End If
                Next
                writer.WriteLine($"Total Records: {dataTable.Rows.Count}")
                writer.WriteLine($"Total Payments: {totalPayments:F2}")
            End Using

            MessageBox.Show($"Payment report exported successfully to: {saveDialog.FileName}", "Export Complete", MessageBoxButtons.OK, MessageBoxIcon.Information)

            ' Add this line to log the export
            Module1.LogUserAction("Export Payment Report", "Exported Payment Report to CSV for date range: " & fromDate.ToString("MM/dd/yyyy") & " - " & toDate.ToString("MM/dd/yyyy"))

            Process.Start(saveDialog.FileName)

        Catch ex As Exception
            Throw New Exception("CSV export failed: " & ex.Message)
        End Try
    End Sub

    Private Sub ExportToHTML(dataTable As DataTable, fromDate As Date, toDate As Date)
        Try
            Dim saveDialog As New SaveFileDialog()
            saveDialog.Filter = "HTML Files (*.html)|*.html"
            saveDialog.Title = "Save Payment Report"
            saveDialog.FileName = $"Payment_Report_{fromDate:yyyyMMdd}_to_{toDate:yyyyMMdd}.html"

            If saveDialog.ShowDialog() <> DialogResult.OK Then
                Return
            End If

            Using writer As New StreamWriter(saveDialog.FileName)
                writer.WriteLine("<!DOCTYPE html>")
                writer.WriteLine("<html>")
                writer.WriteLine("<head>")
                writer.WriteLine("<title>Payment Report</title>")
                writer.WriteLine("<style>")
                writer.WriteLine("body { font-family: Arial, sans-serif; margin: 20px; }")
                writer.WriteLine("h1 { text-align: center; color: #333; margin-bottom: 10px; }")
                writer.WriteLine("h2 { text-align: center; color: #666; font-size: 14px; margin-bottom: 20px; }")
                writer.WriteLine("table { width: 100%; border-collapse: collapse; margin-top: 20px; }")
                writer.WriteLine("th, td { border: 1px solid #ddd; padding: 6px; text-align: left; font-size: 12px; }")
                writer.WriteLine("th { background-color: #f2f2f2; font-weight: bold; }")
                writer.WriteLine("tr:nth-child(even) { background-color: #f9f9f9; }")
                writer.WriteLine(".currency { text-align: right; }")
                writer.WriteLine(".summary { margin-top: 20px; font-weight: bold; }")
                writer.WriteLine("@media print { body { margin: 0; } }")
                writer.WriteLine("</style>")
                writer.WriteLine("</head>")
                writer.WriteLine("<body>")
                writer.WriteLine("<h1>Payment Report</h1>")
                writer.WriteLine($"<h2>{fromDate:MMMM dd, yyyy} to {toDate:MMMM dd, yyyy}</h2>")
                writer.WriteLine("<table>")

                ' Table headers
                writer.WriteLine("<tr>")
                writer.WriteLine("<th>Client ID</th>")
                writer.WriteLine("<th>Client Name</th>")
                writer.WriteLine("<th>Reservation ID</th>")
                writer.WriteLine("<th>Payment Date</th>")
                writer.WriteLine("<th>Payment Type</th>")
                writer.WriteLine("<th>Amount Paid</th>")
                writer.WriteLine("<th>Total Amount</th>")
                writer.WriteLine("<th>Status</th>")
                writer.WriteLine("</tr>")

                ' Data rows
                Dim totalPayments As Decimal = 0
                For Each row As DataRow In dataTable.Rows
                    writer.WriteLine("<tr>")

                    writer.WriteLine($"<td>{If(IsDBNull(row("Client_ID")), "", row("Client_ID").ToString())}</td>")
                    writer.WriteLine($"<td>{If(IsDBNull(row("FullName")), "", row("FullName").ToString())}</td>")
                    writer.WriteLine($"<td>{If(IsDBNull(row("Reservation_ID")), "", row("Reservation_ID").ToString())}</td>")

                    Dim paymentDate As String = If(IsDBNull(row("payment_date")), "N/A", Convert.ToDateTime(row("payment_date")).ToString("MM/dd/yyyy"))
                    writer.WriteLine($"<td>{paymentDate}</td>")

                    writer.WriteLine($"<td>{If(IsDBNull(row("payment_type")), "", row("payment_type").ToString())}</td>")

                    Dim totalPaid As Decimal = If(IsDBNull(row("total_Paid")), 0D, Convert.ToDecimal(row("total_Paid")))
                    writer.WriteLine($"<td class='currency'>{totalPaid:C}</td>")
                    totalPayments += totalPaid

                    Dim totalAmount As Decimal = If(IsDBNull(row("total_Amount")), 0D, Convert.ToDecimal(row("total_Amount")))
                    writer.WriteLine($"<td class='currency'>{totalAmount:C}</td>")

                    writer.WriteLine($"<td>{If(IsDBNull(row("PaymentStatus")), "N/A", row("PaymentStatus").ToString())}</td>")

                    writer.WriteLine("</tr>")
                Next

                writer.WriteLine("</table>")

                ' Summary
                writer.WriteLine("<div class='summary'>")
                writer.WriteLine($"<p>Total Records: {dataTable.Rows.Count}</p>")
                writer.WriteLine($"<p>Total Payments: {totalPayments:C}</p>")
                writer.WriteLine($"<p>Generated on: {DateTime.Now:MMMM dd, yyyy hh:mm tt}</p>")
                writer.WriteLine("</div>")

                writer.WriteLine("</body>")
                writer.WriteLine("</html>")
            End Using

            MessageBox.Show($"Payment report exported successfully! Open the HTML file in a browser and use Ctrl+P to print as PDF.{vbCrLf}File saved to: {saveDialog.FileName}", "Export Complete", MessageBoxButtons.OK, MessageBoxIcon.Information)

            ' Add this line to log the export
            Module1.LogUserAction("Export Payment Report", "Exported Payment Report to HTML for date range: " & fromDate.ToString("MM/dd/yyyy") & " - " & toDate.ToString("MM/dd/yyyy"))

            Process.Start(saveDialog.FileName)

        Catch ex As Exception
            Throw New Exception("HTML export failed: " & ex.Message)
        End Try
    End Sub


    Private Sub btnShowAll_Click(sender As Object, e As EventArgs) Handles btnShowAll.Click
        LoadPaymentData() ' Refresh the ListView with the latest data
    End Sub

    Private Sub btnView_Click(sender As Object, e As EventArgs) Handles btnView.Click
        If PaymentList.SelectedItems.Count > 0 Then
            Dim selectedClientID As String = PaymentList.SelectedItems(0).Text
            Dim viewPaymentForm As New frmViewPayment(selectedClientID)
            viewPaymentForm.ShowDialog()
        Else
            MessageBox.Show("Please select a client first.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub
End Class
