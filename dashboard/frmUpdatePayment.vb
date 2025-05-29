Imports MySql.Data.MySqlClient

Public Class frmUpdatePayment
    Private reservationID As String
    Private remainingBalance As Decimal
    Public ParentForm As frmViewPayment ' To allow refreshing parent

    Public Sub New(reservationID As String, remainingBalance As Decimal, Optional parent As frmViewPayment = Nothing)
        InitializeComponent()
        Me.reservationID = reservationID
        Me.remainingBalance = remainingBalance
        Me.ParentForm = parent
    End Sub

    Private Sub frmUpdatePayment_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        lblReservationID.Text = reservationID
        lblRemainingBalance.Text = "₱" & remainingBalance.ToString("N2")
        
        ' Set up the txtPaidAmount to only accept numbers
        AddHandler txtPaidAmount.KeyPress, AddressOf TxtPaidAmount_KeyPress
    End Sub

    Private Sub TxtPaidAmount_KeyPress(sender As Object, e As KeyPressEventArgs)
        ' Only allow numbers and backspace
        If Not Char.IsDigit(e.KeyChar) AndAlso e.KeyChar <> ChrW(Keys.Back) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtPaidAmount_TextChanged(sender As Object, e As EventArgs) Handles txtPaidAmount.TextChanged
        ' Remove any non-digit characters
        Dim digitsOnly As String = New String(txtPaidAmount.Text.Where(Function(c) Char.IsDigit(c)).ToArray())
        
        ' Format the number with commas
        If Not String.IsNullOrEmpty(digitsOnly) Then
            Dim number As Long
            If Long.TryParse(digitsOnly, number) Then
                txtPaidAmount.Text = number.ToString("N0")
                txtPaidAmount.SelectionStart = txtPaidAmount.Text.Length
            End If
        End If
    End Sub

    Private Sub btnConfirm_Click(sender As Object, e As EventArgs) Handles btnConfirm.Click
        ' Validate official receipt number
        If String.IsNullOrWhiteSpace(txtOR.Text) Then
            MessageBox.Show("Please enter the Official Receipt number.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' Get the numeric value without commas
        Dim paidAmountStr As String = New String(txtPaidAmount.Text.Where(Function(c) Char.IsDigit(c)).ToArray())
        Dim paidAmount As Decimal
        If Not Decimal.TryParse(paidAmountStr, paidAmount) Then
            MessageBox.Show("Please enter a valid number for the paid amount.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        If paidAmount <= 0 Then
            MessageBox.Show("The paid amount must be greater than zero.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        If paidAmount > remainingBalance Then
            MessageBox.Show("Amount exceeds remaining balance. Please enter a valid amount.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        ' Save the payment transaction
        If SaveTransaction(paidAmount) Then
            MessageBox.Show("Payment recorded successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

            ' Refresh parent form's data
            ParentForm?.LoadClientPaymentData()
            ParentForm?.LoadClientDetails()
            ParentForm?.LoadPaymentHistory()

            ' Close this form
            Me.Dispose()
            Me.Close()
        End If
    End Sub

    Private Function SaveTransaction(paidAmount As Decimal) As Boolean
        Try
            Using tempConnection As New MySqlConnection("server=srv594.hstgr.io; database=u976878483_cemetery; username=u976878483_doncarlos; password=d0Nc4los; port=3306")
                tempConnection.Open()

                ' Generate new Transaction_ID manually
                Dim getMaxIdCmd As New MySqlCommand("SELECT IFNULL(MAX(Transaction_ID), 0) + 1 FROM `transaction`", tempConnection)
                Dim newTransactionID As Integer = Convert.ToInt32(getMaxIdCmd.ExecuteScalar())

                ' Get Type_ID from the location table using reservation.p_id
                Dim getTypeCmd As New MySqlCommand("
                SELECT l.type FROM reservation r 
                INNER JOIN location l ON r.p_id = l.id 
                WHERE r.Reservation_ID = @ResID", tempConnection)
                getTypeCmd.Parameters.AddWithValue("@ResID", reservationID)
                Dim typeID As Integer = Convert.ToInt32(getTypeCmd.ExecuteScalar())

                ' Update the total_Paid, Payment_Date, and Payment_Status in the payment table
                Dim updatePaymentCmd As New MySqlCommand("
                UPDATE payment 
                SET total_Paid = total_Paid + @Amount, 
                    Payment_Date = @Date,
                    Payment_Status = CASE 
                        WHEN (total_Paid + @Amount) >= total_Amount THEN 1 
                        ELSE Payment_Status 
                    END
                WHERE Reservation_ID = @ReservationID", tempConnection)
                updatePaymentCmd.Parameters.AddWithValue("@Amount", paidAmount)
                updatePaymentCmd.Parameters.AddWithValue("@Date", DateTime.Now)
                updatePaymentCmd.Parameters.AddWithValue("@ReservationID", reservationID)
                updatePaymentCmd.ExecuteNonQuery()
                LogUserAction("Update Payment", "Updated payment for reservation: " & reservationID & ", new amount: " & paidAmount)

                ' Insert transaction
                Dim sql As String = "
                INSERT INTO `transaction` (Transaction_ID, Date, Amount, Client_ID, Type_ID, official_receipt)
                VALUES (
                    @TransactionID,
                    @Date,
                    @Amount,
                    (SELECT Client_ID FROM reservation WHERE Reservation_ID = @ReservationID),
                    @TypeID,
                    @OfficialReceipt
                )"

                Using cmd As New MySqlCommand(sql, tempConnection)
                    cmd.Parameters.AddWithValue("@TransactionID", newTransactionID)
                    cmd.Parameters.AddWithValue("@Date", DateTime.Now)
                    cmd.Parameters.AddWithValue("@Amount", paidAmount)
                    cmd.Parameters.AddWithValue("@ReservationID", reservationID)
                    cmd.Parameters.AddWithValue("@TypeID", typeID)
                    cmd.Parameters.AddWithValue("@OfficialReceipt", txtOR.Text.Trim())
                    cmd.ExecuteNonQuery()
                End Using
            End Using

            Return True

        Catch ex As Exception
            MessageBox.Show("Error recording the payment: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.Dispose()
        Me.Close()
    End Sub

    Public Property SelectedReservationID As String
        Get
            Return reservationID
        End Get
        Set(value As String)
            reservationID = value
        End Set
    End Property

End Class
