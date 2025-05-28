Imports MySql.Data.MySqlClient

Public Class frmViewPayment
    Private clientID As String

    ' Constructor to accept Client_ID
    Public Sub New(selectedClientID As String)
        InitializeComponent()
        clientID = selectedClientID
    End Sub

    Private Sub frmViewPayment_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadClientDetails()
        LoadClientPaymentData()
        LoadPaymentHistory()
    End Sub


    Public Sub LoadClientDetails()
        Dim tempConnection As MySqlConnection = Nothing
        Try
            ' Create a new connection
            tempConnection = New MySqlConnection("server=srv594.hstgr.io; database=u976878483_cemetery; username=u976878483_doncarlos; password=d0Nc4los; port=3306")
            tempConnection.Open()

            ' SQL Query to get Client Name and Total Reservations
            Dim sql As String = "
                SELECT 
                    CONCAT(COALESCE(c.FirstName, ''), ' ', COALESCE(LEFT(c.MiddleName, 1), ''), '. ', COALESCE(c.LastName, '')) AS FullName,
                    COUNT(r.Reservation_ID) AS TotalReservations,
                    SUM(p.total_Amount) AS OverallAmount,
                    SUM(p.total_Paid) AS TotalPaid
                FROM 
                    client c
                LEFT JOIN 
                    reservation r ON c.Client_ID = r.Client_ID
                LEFT JOIN 
                    payment p ON r.Reservation_ID = p.Reservation_ID
                WHERE 
                    c.Client_ID = @ClientID
                GROUP BY 
                    c.Client_ID, FullName;"

            ' Execute Query
            Using cmd As New MySqlCommand(sql, tempConnection)
                cmd.Parameters.AddWithValue("@ClientID", clientID)
                Using dr As MySqlDataReader = cmd.ExecuteReader()
                    If dr.Read() Then
                        ' Display Name
                        lblName.Text = "Name: " & dr("FullName").ToString()

                        ' Display Total Reserved Plots
                        lblTotalReserved.Text = "Total Reserved Plots: " & dr("TotalReservations").ToString()

                        ' Display Overall Amount
                        Dim overallAmount As Decimal = If(IsDBNull(dr("OverallAmount")), 0, Convert.ToDecimal(dr("OverallAmount")))
                        lblOverallAmount.Text = "Overall Amount: ₱" & overallAmount.ToString("N2")

                        ' Display Overall Balance
                        Dim totalPaid As Decimal = If(IsDBNull(dr("TotalPaid")), 0, Convert.ToDecimal(dr("TotalPaid")))
                        Dim balance As Decimal = overallAmount - totalPaid
                        lblOverallBal.Text = "Remaining Balance: ₱" & balance.ToString("N2")
                    Else
                        lblName.Text = "Name: Not Found"
                        lblTotalReserved.Text = "Total Reserved Plots: 0"
                        lblOverallAmount.Text = "Overall Amount: ₱0.00"
                        lblOverallBal.Text = "Overall Balance: ₱0.00"
                    End If
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error loading client details: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            ' Close and Dispose Connection
            If tempConnection IsNot Nothing Then
                If tempConnection.State = ConnectionState.Open Then
                    tempConnection.Close()
                End If
                tempConnection.Dispose()
            End If
        End Try
    End Sub

    Public Sub LoadClientPaymentData()
        Try
            Using tempConnection As New MySqlConnection("server=srv594.hstgr.io; database=u976878483_cemetery; username=u976878483_doncarlos; password=d0Nc4los; port=3306")
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
                        ' - Block ', l.block, ', Section ', l.section, ', Row ', l.row, ', Plot ', l.plot, ', Plot ID ', COALESCE(l.id, 'N/A')
                    ) AS ReservedPlot,
                    COALESCE(SUM(p.total_Paid), 0) AS TotalPaid,
                    COALESCE(SUM(p.total_Amount), 0) AS TotalAmount,
                    COALESCE(SUM(p.total_Amount) - SUM(p.total_Paid), 0) AS Balance,
                    CASE 
                        WHEN SUM(p.total_Paid) >= SUM(p.total_Amount) THEN 'Fully Paid' 
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
                    r.Reservation_ID, r.Reservation_Date, ReservedPlot
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

                            ' Handle NULL values using COALESCE in SQL, but also check in VB.NET
                            Dim totalPaid As Decimal = If(IsDBNull(dr("TotalPaid")), 0, Convert.ToDecimal(dr("TotalPaid")))
                            Dim totalAmount As Decimal = If(IsDBNull(dr("TotalAmount")), 0, Convert.ToDecimal(dr("TotalAmount")))
                            Dim balance As Decimal = If(IsDBNull(dr("Balance")), 0, Convert.ToDecimal(dr("Balance")))
                            Dim paymentStatus As String = dr("PaymentStatus").ToString()

                            ' Add to ListView
                            Dim newItem As ListViewItem = ReservAccount.Items.Add(reservationID)
                            newItem.SubItems.Add(reservedPlot)
                            newItem.SubItems.Add("₱" & totalPaid.ToString("N2"))
                            newItem.SubItems.Add("₱" & totalAmount.ToString("N2"))
                            newItem.SubItems.Add("₱" & balance.ToString("N2"))
                            newItem.SubItems.Add(paymentStatus)
                            newItem.SubItems.Add(reservationDate.ToString("yyyy-MM-dd HH:mm:ss"))
                        End While
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error loading reservation data: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' Handle DoubleClick event on the ListView
    Private Sub ReservAccount_DoubleClick(sender As Object, e As EventArgs) Handles ReservAccount.DoubleClick
        If ReservAccount.SelectedItems.Count > 0 Then
            Dim selectedItem As ListViewItem = ReservAccount.SelectedItems(0)
            Dim selectedReservationID As String = selectedItem.SubItems(0).Text
            Dim selectedRemainingBalanceStr As String = selectedItem.SubItems(4).Text.Replace("₱", "").Replace("$", "").Replace(",", "").Trim()

            Dim selectedRemainingBalance As Decimal
            If Decimal.TryParse(selectedRemainingBalanceStr, selectedRemainingBalance) Then
                Dim updateForm As New frmUpdatePayment(selectedReservationID, selectedRemainingBalance, Me)
                updateForm.ShowDialog()
            Else
                MessageBox.Show("Invalid Remaining Balance data.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End If
    End Sub

    Public Sub LoadPaymentHistory()
        Try
            Using tempConnection As New MySqlConnection("server=srv594.hstgr.io; database=u976878483_cemetery; username=u976878483_doncarlos; password=d0Nc4los; port=3306")
                tempConnection.Open()

                Dim sql As String = "
                SELECT 
                    Transaction_ID,
                    Amount,
                    Date
                FROM 
                    `transaction`
                WHERE 
                    Client_ID = @ClientID
                ORDER BY Date DESC;"

                Using cmd As New MySqlCommand(sql, tempConnection)
                    cmd.Parameters.AddWithValue("@ClientID", clientID)

                    Using dr As MySqlDataReader = cmd.ExecuteReader()
                        lstPaymentHistory.Items.Clear()

                        While dr.Read()
                            Dim transactionID As String = dr("Transaction_ID").ToString()
                            Dim amount As Decimal = If(IsDBNull(dr("Amount")), 0, Convert.ToDecimal(dr("Amount")))
                            Dim dateValue As Date = If(IsDBNull(dr("Date")), Nothing, Convert.ToDateTime(dr("Date")))

                            Dim item As ListViewItem = lstPaymentHistory.Items.Add(transactionID)
                            item.SubItems.Add("₱" & amount.ToString("N2"))
                            item.SubItems.Add(dateValue.ToString("yyyy-MM-dd HH:mm:ss"))
                        End While
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error loading payment history: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnPayment_Click(sender As Object, e As EventArgs) Handles btnPayment.Click
        ' Check if an item is selected in the ListView
        If ReservAccount.SelectedItems.Count > 0 Then
            ' Get the selected item
            Dim selectedItem As ListViewItem = ReservAccount.SelectedItems(0)
            Dim selectedReservationID As String = selectedItem.SubItems(0).Text
            Dim selectedRemainingBalanceStr As String = selectedItem.SubItems(4).Text.Replace("₱", "").Replace("$", "").Replace(",", "").Trim()

            Dim selectedRemainingBalance As Decimal
            If Decimal.TryParse(selectedRemainingBalanceStr, selectedRemainingBalance) Then
                Dim updateForm As New frmUpdatePayment(selectedReservationID, selectedRemainingBalance, Me)
                updateForm.ShowDialog()
            Else
                MessageBox.Show("Invalid Remaining Balance data.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Else
            MessageBox.Show("Please select a reservation from the list.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    Private Sub btnShowAll_Click(sender As Object, e As EventArgs) Handles btnShowAll.Click
        LoadPaymentHistory()
        LoadClientPaymentData()
        LoadClientDetails()
    End Sub

End Class
