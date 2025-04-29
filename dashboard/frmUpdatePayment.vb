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
    End Sub

    Private Sub btnConfirm_Click(sender As Object, e As EventArgs) Handles btnConfirm.Click
        Dim paidAmount As Decimal
        If Not Decimal.TryParse(txtPaidAmount.Text, paidAmount) Then
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
            Using tempConnection As New MySqlConnection("server=localhost; user=root; password=root; database=dccms")
                tempConnection.Open()

                ' Generate new Transaction_ID manually
                Dim getMaxIdCmd As New MySqlCommand("SELECT IFNULL(MAX(Transaction_ID), 0) + 1 FROM `transaction`", tempConnection)
                Dim newTransactionID As Integer = Convert.ToInt32(getMaxIdCmd.ExecuteScalar())

                ' Get Type_ID from the location table using reservation.p_id
                Dim getTypeCmd As New MySqlCommand("
                SELECT l.type FROM Reservation r 
                INNER JOIN Location l ON r.p_id = l.id 
                WHERE r.Reservation_ID = @ResID", tempConnection)
                getTypeCmd.Parameters.AddWithValue("@ResID", reservationID)
                Dim typeID As Integer = Convert.ToInt32(getTypeCmd.ExecuteScalar())

                ' Update the total_Paid and Payment_Date in the payment table
                Dim updatePaymentCmd As New MySqlCommand("
                UPDATE Payment 
                SET total_Paid = total_Paid + @Amount, Payment_Date = @Date 
                WHERE Reservation_ID = @ReservationID", tempConnection)
                updatePaymentCmd.Parameters.AddWithValue("@Amount", paidAmount)
                updatePaymentCmd.Parameters.AddWithValue("@Date", DateTime.Now)
                updatePaymentCmd.Parameters.AddWithValue("@ReservationID", reservationID)
                updatePaymentCmd.ExecuteNonQuery()

                ' Insert transaction
                Dim sql As String = "
                INSERT INTO `transaction` (Transaction_ID, Date, Amount, Client_ID, Type_ID, Deceased_ID)
                VALUES (
                    @TransactionID,
                    @Date,
                    @Amount,
                    (SELECT Client_ID FROM Reservation WHERE Reservation_ID = @ReservationID),
                    @TypeID,
                    (SELECT Deceased_ID FROM Reservation WHERE Reservation_ID = @ReservationID)
                )"

                Using cmd As New MySqlCommand(sql, tempConnection)
                    cmd.Parameters.AddWithValue("@TransactionID", newTransactionID)
                    cmd.Parameters.AddWithValue("@Date", DateTime.Now)
                    cmd.Parameters.AddWithValue("@Amount", paidAmount)
                    cmd.Parameters.AddWithValue("@ReservationID", reservationID)
                    cmd.Parameters.AddWithValue("@TypeID", typeID)
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
End Class
