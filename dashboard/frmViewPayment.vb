Imports MySql.Data.MySqlClient
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports System.Data
Imports System.Diagnostics
Imports System.IO

Public Class frmViewPayment
    Private clientID As String
    Private Const CONNECTION_STRING As String = "server=srv594.hstgr.io; database=u976878483_cemetery; username=u976878483_doncarlos; password=d0Nc4los; port=3306"

    ' Constructor to accept Client_ID
    Public Sub New(selectedClientID As String)
        InitializeComponent()
        clientID = selectedClientID
    End Sub

    Private Sub frmViewPayment_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            LoadClientDetails()
            LoadClientPaymentData()
            LoadPaymentHistory()
        Catch ex As Exception
            MessageBox.Show($"Error loading form data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Sub LoadClientDetails()
        Using tempConnection As New MySqlConnection(CONNECTION_STRING)
            Try
                tempConnection.Open()

                ' SQL Query to get Client Name and Total Reservations
                Dim sql As String = "
                    SELECT 
                        CONCAT(COALESCE(c.FirstName, ''), 
                               CASE WHEN c.MiddleName IS NOT NULL AND c.MiddleName != '' 
                                    THEN CONCAT(' ', LEFT(c.MiddleName, 1), '. ') 
                                    ELSE ' ' END, 
                               COALESCE(c.LastName, '')) AS FullName,
                        COALESCE(COUNT(DISTINCT r.Reservation_ID), 0) AS TotalReservations,
                        COALESCE(SUM(p.total_Amount), 0) AS OverallAmount,
                        COALESCE(SUM(p.total_Paid), 0) AS TotalPaid
                    FROM 
                        client c
                    LEFT JOIN 
                        reservation r ON c.Client_ID = r.Client_ID
                    LEFT JOIN 
                        payment p ON r.Reservation_ID = p.Reservation_ID
                    WHERE 
                        c.Client_ID = @ClientID
                    GROUP BY 
                        c.Client_ID, c.FirstName, c.MiddleName, c.LastName"

                Using cmd As New MySqlCommand(sql, tempConnection)
                    cmd.Parameters.AddWithValue("@ClientID", clientID)

                    Using dr As MySqlDataReader = cmd.ExecuteReader()
                        If dr.Read() Then
                            ' Display Name
                            lblName.Text = $"Name: {dr("FullName")}"

                            ' Display Total Reserved Plots
                            lblTotalReserved.Text = $"Total Reserved Plots: {dr("TotalReservations")}"

                            ' Display Overall Amount
                            Dim overallAmount As Decimal = Convert.ToDecimal(dr("OverallAmount"))
                            lblOverallAmount.Text = $"Overall Amount: ₱{overallAmount:N2}"

                            ' Display Overall Balance
                            Dim totalPaid As Decimal = Convert.ToDecimal(dr("TotalPaid"))
                            Dim balance As Decimal = overallAmount - totalPaid
                            lblOverallBal.Text = $"Remaining Balance: ₱{balance:N2}"
                        Else
                            SetDefaultLabels()
                        End If
                    End Using
                End Using
            Catch ex As MySqlException
                MessageBox.Show($"Database error loading client details: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Catch ex As Exception
                MessageBox.Show($"Error loading client details: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Using
    End Sub

    Private Sub SetDefaultLabels()
        lblName.Text = "Name: Not Found"
        lblTotalReserved.Text = "Total Reserved Plots: 0"
        lblOverallAmount.Text = "Overall Amount: ₱0.00"
        lblOverallBal.Text = "Remaining Balance: ₱0.00"
    End Sub

    Public Sub LoadClientPaymentData()
        Using tempConnection As New MySqlConnection(CONNECTION_STRING)
            Try
                tempConnection.Open()

                Dim sql As String = "
                    SELECT 
                        r.Reservation_ID,
                        r.Reservation_Date,
                        CONCAT(
                            CASE 
                                WHEN l.type = 1 THEN 'Apartment'
                                WHEN l.type = 2 THEN 'Family Lawn Lot'
                                WHEN l.type = 3 THEN 'Bone Niche'
                                WHEN l.type = 4 THEN 'Private'
                                ELSE 'Unknown' 
                            END, 
                            ' - Block ', COALESCE(l.block, 'N/A'), 
                            ', Section ', COALESCE(l.section, 'N/A'), 
                            ', Row ', COALESCE(l.row, 'N/A'), 
                            ', Plot ', COALESCE(l.plot, 'N/A'), 
                            ', Plot ID ', COALESCE(l.id, 'N/A')
                        ) AS ReservedPlot,
                        COALESCE(SUM(p.total_Paid), 0) AS TotalPaid,
                        COALESCE(SUM(p.total_Amount), 0) AS TotalAmount,
                        COALESCE(SUM(p.total_Amount) - SUM(p.total_Paid), 0) AS Balance,
                        CASE 
                            WHEN COALESCE(SUM(p.total_Paid), 0) >= COALESCE(SUM(p.total_Amount), 0) THEN 'Fully Paid' 
                            ELSE 'Partial' 
                        END AS PaymentStatus
                    FROM 
                        reservation r
                    LEFT JOIN 
                        location l ON r.p_id = l.id
                    LEFT JOIN 
                        payment p ON r.Reservation_ID = p.Reservation_ID
                    WHERE 
                        r.Client_ID = @ClientID
                    GROUP BY 
                        r.Reservation_ID, r.Reservation_Date, l.type, l.block, l.section, l.row, l.plot, l.id
                    ORDER BY 
                        r.Reservation_Date DESC"

                Using cmd As New MySqlCommand(sql, tempConnection)
                    cmd.Parameters.AddWithValue("@ClientID", clientID)

                    Using dr As MySqlDataReader = cmd.ExecuteReader()
                        ReservAccount.Items.Clear()

                        While dr.Read()
                            Dim reservationID As String = dr("Reservation_ID").ToString()
                            Dim reservationDate As DateTime = Convert.ToDateTime(dr("Reservation_Date"))
                            Dim reservedPlot As String = dr("ReservedPlot").ToString()
                            Dim totalPaid As Decimal = Convert.ToDecimal(dr("TotalPaid"))
                            Dim totalAmount As Decimal = Convert.ToDecimal(dr("TotalAmount"))
                            Dim balance As Decimal = Convert.ToDecimal(dr("Balance"))
                            Dim paymentStatus As String = dr("PaymentStatus").ToString()

                            ' Add to ListView
                            Dim newItem As ListViewItem = ReservAccount.Items.Add(reservationID)
                            newItem.SubItems.Add(reservedPlot)
                            newItem.SubItems.Add($"₱{totalPaid:N2}")
                            newItem.SubItems.Add($"₱{totalAmount:N2}")
                            newItem.SubItems.Add($"₱{balance:N2}")
                            newItem.SubItems.Add(paymentStatus)
                            newItem.SubItems.Add(reservationDate.ToString("yyyy-MM-dd HH:mm:ss"))
                        End While
                    End Using
                End Using
            Catch ex As MySqlException
                MessageBox.Show($"Database error loading reservation data: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Catch ex As Exception
                MessageBox.Show($"Error loading reservation data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Using
    End Sub

    Public Sub LoadPaymentHistory()
        Using tempConnection As New MySqlConnection(CONNECTION_STRING)
            Try
                tempConnection.Open()

                Dim sql As String = "
                    SELECT 
                        t.Transaction_ID,
                        COALESCE(t.Amount, 0) AS Amount,
                        t.Date,
                        COALESCE(t.official_receipt, 'N/A') AS official_receipt
                    FROM 
                        `transaction` t
                    WHERE 
                        t.Client_ID = @ClientID
                    ORDER BY t.Date DESC"

                Using cmd As New MySqlCommand(sql, tempConnection)
                    cmd.Parameters.AddWithValue("@ClientID", clientID)

                    Using dr As MySqlDataReader = cmd.ExecuteReader()
                        lstPaymentHistory.Items.Clear()

                        While dr.Read()
                            Dim officialReceipt As String = dr("official_receipt").ToString()
                            Dim amount As Decimal = Convert.ToDecimal(dr("Amount"))
                            Dim dateValue As DateTime = Convert.ToDateTime(dr("Date"))

                            Dim item As ListViewItem = lstPaymentHistory.Items.Add(officialReceipt)
                            item.SubItems.Add($"₱{amount:N2}")
                            item.SubItems.Add(dateValue.ToString("yyyy-MM-dd HH:mm:ss"))
                        End While
                    End Using
                End Using
            Catch ex As MySqlException
                MessageBox.Show($"Database error loading payment history: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Catch ex As Exception
                MessageBox.Show($"Error loading payment history: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Using
    End Sub

    ' Handle DoubleClick event on the ListView
    Private Sub ReservAccount_DoubleClick(sender As Object, e As EventArgs) Handles ReservAccount.DoubleClick
        HandlePaymentUpdate()
    End Sub

    Private Sub btnPayment_Click(sender As Object, e As EventArgs) Handles btnPayment.Click
        HandlePaymentUpdate()
    End Sub

    Private Sub HandlePaymentUpdate()
        If ReservAccount.SelectedItems.Count = 0 Then
            MessageBox.Show("Please select a reservation from the list.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Try
            Dim selectedItem As ListViewItem = ReservAccount.SelectedItems(0)
            Dim selectedReservationID As String = selectedItem.SubItems(0).Text
            Dim selectedRemainingBalanceStr As String = selectedItem.SubItems(4).Text.Replace("₱", "").Replace("$", "").Replace(",", "").Trim()

            If Not Decimal.TryParse(selectedRemainingBalanceStr, Nothing) Then
                MessageBox.Show("Invalid Remaining Balance data.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If

            Dim selectedRemainingBalance As Decimal = Decimal.Parse(selectedRemainingBalanceStr)
            Dim updateForm As New frmUpdatePayment(selectedReservationID, selectedRemainingBalance, Me)
            updateForm.ShowDialog()
        Catch ex As Exception
            MessageBox.Show($"Error opening payment update form: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnShowAll_Click(sender As Object, e As EventArgs) Handles btnShowAll.Click
        Try
            LoadPaymentHistory()
            LoadClientPaymentData()
            LoadClientDetails()
        Catch ex As Exception
            MessageBox.Show($"Error refreshing data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        Try
            Dim saveDialog As New SaveFileDialog()
            saveDialog.Filter = "PDF files (*.pdf)|*.pdf"
            saveDialog.Title = "Export Client Payment Details"
            saveDialog.FileName = $"ClientPayment_{clientID}_{DateTime.Now:yyyyMMdd}.pdf"

            If saveDialog.ShowDialog() = DialogResult.OK Then
                ExportToPDF(saveDialog.FileName)
                MessageBox.Show("Payment details exported successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

                ' Try to open the file, but don't fail if it can't be opened
                Try
                    Process.Start(saveDialog.FileName)
                Catch
                    ' Ignore if file can't be opened
                End Try
            End If
        Catch ex As Exception
            MessageBox.Show($"Error exporting payment details: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ExportToPDF(filePath As String)
        Try
            ' Create a new PDF document
            Dim document As New Document(PageSize.A4, 50, 50, 50, 50)

            ' Create a file stream
            Using stream As New FileStream(filePath, FileMode.Create)
                ' Create PdfWriter instance
                Dim writer As PdfWriter = PdfWriter.GetInstance(document, stream)

                ' Open the document
                document.Open()

                ' Define fonts
                Dim titleFont As Font = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16, BaseColor.BLACK)
                Dim headerFont As Font = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12, BaseColor.BLACK)
                Dim normalFont As Font = FontFactory.GetFont(FontFactory.HELVETICA, 10, BaseColor.BLACK)
                Dim smallFont As Font = FontFactory.GetFont(FontFactory.HELVETICA, 8, BaseColor.BLACK)

                ' Add title
                Dim title As New Paragraph("Client Payment Details", titleFont)
                title.Alignment = Element.ALIGN_CENTER
                title.SpacingAfter = 20
                document.Add(title)

                ' Add client information section
                Dim clientInfoHeader As New Paragraph("Client Information", headerFont)
                clientInfoHeader.SpacingBefore = 10
                clientInfoHeader.SpacingAfter = 10
                document.Add(clientInfoHeader)

                ' Client details
                document.Add(New Paragraph(lblName.Text, normalFont))
                document.Add(New Paragraph(lblTotalReserved.Text, normalFont))
                document.Add(New Paragraph(lblOverallAmount.Text, normalFont))
                document.Add(New Paragraph(lblOverallBal.Text, normalFont))

                ' Add space before next section
                document.Add(New Paragraph(" "))

                ' Payment Account Details section
                Dim paymentAccountHeader As New Paragraph("Payment Account Details", headerFont)
                paymentAccountHeader.SpacingBefore = 15
                paymentAccountHeader.SpacingAfter = 10
                document.Add(paymentAccountHeader)

                ' Create table for payment accounts
                If ReservAccount.Items.Count > 0 Then
                    Dim paymentTable As New PdfPTable(7)
                    paymentTable.WidthPercentage = 100
                    paymentTable.SetWidths(New Single() {1.0F, 3.5F, 1.5F, 1.5F, 1.5F, 1.2F, 2.0F})

                    ' Add headers
                    Dim headers As String() = {"ID", "Plot", "Paid", "Amount", "Balance", "Status", "Date"}
                    For Each header As String In headers
                        Dim cell As New PdfPCell(New Phrase(header, headerFont))
                        cell.BackgroundColor = BaseColor.LIGHT_GRAY
                        cell.HorizontalAlignment = Element.ALIGN_CENTER
                        cell.Padding = 5
                        paymentTable.AddCell(cell)
                    Next

                    ' Add data rows
                    For Each item As ListViewItem In ReservAccount.Items
                        For i As Integer = 0 To Math.Min(item.SubItems.Count - 1, 6)
                            Dim cellText As String = item.SubItems(i).Text
                            ' Truncate long text for plot column
                            If i = 1 AndAlso cellText.Length > 30 Then
                                cellText = cellText.Substring(0, 27) + "..."
                            End If

                            Dim cell As New PdfPCell(New Phrase(cellText, smallFont))
                            cell.HorizontalAlignment = If(i = 0 OrElse i >= 2 AndAlso i <= 4, Element.ALIGN_RIGHT, Element.ALIGN_LEFT)
                            cell.Padding = 3
                            paymentTable.AddCell(cell)
                        Next
                    Next

                    document.Add(paymentTable)
                Else
                    document.Add(New Paragraph("No payment account data available.", normalFont))
                End If

                ' Add space before next section
                document.Add(New Paragraph(" "))

                ' Payment History section
                Dim paymentHistoryHeader As New Paragraph("Payment History", headerFont)
                paymentHistoryHeader.SpacingBefore = 15
                paymentHistoryHeader.SpacingAfter = 10
                document.Add(paymentHistoryHeader)

                ' Create table for payment history
                If lstPaymentHistory.Items.Count > 0 Then
                    Dim historyTable As New PdfPTable(3)
                    historyTable.WidthPercentage = 100
                    historyTable.SetWidths(New Single() {2.0F, 1.5F, 2.0F})

                    ' Add headers
                    Dim historyHeaders As String() = {"Receipt #", "Amount", "Date"}
                    For Each header As String In historyHeaders
                        Dim cell As New PdfPCell(New Phrase(header, headerFont))
                        cell.BackgroundColor = BaseColor.LIGHT_GRAY
                        cell.HorizontalAlignment = Element.ALIGN_CENTER
                        cell.Padding = 5
                        historyTable.AddCell(cell)
                    Next

                    ' Add data rows
                    For Each item As ListViewItem In lstPaymentHistory.Items
                        For i As Integer = 0 To Math.Min(item.SubItems.Count - 1, 2)
                            Dim cell As New PdfPCell(New Phrase(item.SubItems(i).Text, normalFont))
                            cell.HorizontalAlignment = If(i = 1, Element.ALIGN_RIGHT, Element.ALIGN_LEFT)
                            cell.Padding = 3
                            historyTable.AddCell(cell)
                        Next
                    Next

                    document.Add(historyTable)
                Else
                    document.Add(New Paragraph("No payment history available.", normalFont))
                End If

                ' Add footer with generation date
                document.Add(New Paragraph(" "))
                Dim footer As New Paragraph($"Generated on: {DateTime.Now:yyyy-MM-dd HH:mm:ss}", smallFont)
                footer.Alignment = Element.ALIGN_RIGHT
                document.Add(footer)

                ' Close the document
                document.Close()
            End Using

        Catch ex As Exception
            Throw New Exception($"Error creating PDF: {ex.Message}", ex)
        End Try
    End Sub
End Class