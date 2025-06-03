Imports MySql.Data.MySqlClient
Imports System.IO
Imports iTextSharp.text
Imports iTextSharp.text.pdf

Public Class frmReservationsReg
    Private Sub frmReservationList_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        KeyPreview = True
        LoadReservations()
    End Sub

    Public Enum XFontStyle
        Regular = 0
        Bold = 1
        Italic = 2
        BoldItalic = 3
    End Enum

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

    Private Function CheckFamilyLawnLotsPayment(reservationID As Integer, clientID As Integer) As Boolean
        Try
            dbconn()
            If cn.State = ConnectionState.Open Then
                cn.Close()
            End If
            cn.Open()

            ' Get plot details for the selected reservation
            Dim block As String = ""
            Dim section As String = ""
            Dim row As String = ""
            Dim plotType As Integer = -1
            Dim currentPlot As String = ""

            Dim sql As String = "SELECT l.block, l.section, l.row, l.type, l.plot " &
                              "FROM plot_reservation pr " &
                              "JOIN location l ON pr.Plot_ID = l.id " &
                              "WHERE pr.Reservation_ID = @ReservationID"

            Using cmd As New MySqlCommand(sql, cn)
                cmd.Parameters.AddWithValue("@ReservationID", reservationID)
                Using dr As MySqlDataReader = cmd.ExecuteReader()
                    If dr.Read() Then
                        ' Read necessary data from the first reader
                        block = dr("block").ToString()
                        section = dr("section").ToString()
                        row = dr("row").ToString()
                        plotType = Convert.ToInt32(dr("type"))
                        currentPlot = dr("plot").ToString()
                    Else
                        ' No plot reservation found, return true as this validation doesn't apply
                        Return True
                    End If
                End Using ' Close and dispose of the first data reader
            End Using ' Dispose of the first command

            ' Only proceed with validation if it's a Family Lawn Lot (type 2)
            If plotType = 2 Then
                ' Get all adjacent plots in the same block, section, and row that belong to the same client
                sql = "SELECT l.id, l.plot, " &
                      "(SELECT COUNT(*) FROM payment p " &
                      "JOIN plot_reservation pr ON p.Reservation_ID = pr.Reservation_ID " &
                      "WHERE pr.Plot_ID = l.id AND p.payment_status = 1) as fully_paid_count, " &
                      "(SELECT COUNT(*) FROM payment p " &
                      "JOIN plot_reservation pr ON p.Reservation_ID = pr.Reservation_ID " &
                      "WHERE pr.Plot_ID = l.id) as total_payments " &
                      "FROM location l " &
                      "JOIN plot_reservation pr ON l.id = pr.Plot_ID " &
                      "JOIN reservation r ON pr.Reservation_ID = r.Reservation_ID " &
                      "WHERE l.type = 2 " &
                      "AND l.block = @Block " &
                      "AND l.section = @Section " &
                      "AND l.row = @Row " &
                      "AND r.Client_ID = @ClientID " &
                      "ORDER BY CAST(l.plot AS UNSIGNED)"

                Using cmd2 As New MySqlCommand(sql, cn)
                    cmd2.Parameters.AddWithValue("@Block", block)
                    cmd2.Parameters.AddWithValue("@Section", section)
                    cmd2.Parameters.AddWithValue("@Row", row)
                    cmd2.Parameters.AddWithValue("@ClientID", clientID)
                    Using dr2 As MySqlDataReader = cmd2.ExecuteReader()
                        Dim adjacentPlots As New List(Of String)
                        While dr2.Read()
                            Dim plotNumber As String = dr2("plot").ToString()
                            Dim fullyPaidCount As Integer = Convert.ToInt32(dr2("fully_paid_count"))
                            Dim totalPayments As Integer = Convert.ToInt32(dr2("total_payments"))
                            
                            ' If any plot is not fully paid, add it to the list
                            If fullyPaidCount < totalPayments Then
                                adjacentPlots.Add(plotNumber)
                            End If
                        End While

                        ' If there are any unpaid adjacent plots, show error and return false
                        If adjacentPlots.Count > 0 Then
                            MessageBox.Show("Cannot open assignment form. This is a Family Lawn Lot (Plot " & currentPlot & ") " & _
                                          "and the following adjacent plots belonging to the same client are not fully paid: " & _
                                          String.Join(", ", adjacentPlots) & vbCrLf & vbCrLf & _
                                          "All adjacent plots must be fully paid before assigning any deceased.", 
                                          "Payment Required", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                            Return False
                        End If
                    End Using ' Close and dispose of the second data reader
                End Using ' Dispose of the second command
            End If

            Return True
        Catch ex As Exception
            MessageBox.Show("Error checking Family Lawn Lots payment: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        Finally
            If cn.State = ConnectionState.Open Then
                cn.Close()
            End If
        End Try
    End Function

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

                ' Check Family Lawn Lots payment status before opening the form
                If Not CheckFamilyLawnLotsPayment(reservationID, clientID) Then
                    Return
                End If

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
        ExportToCSV() ' Much more reliable than PDF libraries
    End Sub

    Private Sub ExportToCSV()
        Try
            Dim saveDialog As New SaveFileDialog()
            saveDialog.Filter = "CSV files (*.csv)|*.csv"
            saveDialog.FileName = "ReservationList_" & DateTime.Now.ToString("yyyyMMdd") & ".csv"

            If saveDialog.ShowDialog() = DialogResult.OK Then
                Using writer As New StreamWriter(saveDialog.FileName)
                    writer.WriteLine("Reservation ID,Client Name,Plot Location,Reservation Date,Quantity,Status,Payment Status")

                    For Each item As ListViewItem In ReservationList.Items
                        Dim values As New List(Of String)
                        For i As Integer = 0 To Math.Min(6, item.SubItems.Count - 1)
                            values.Add(item.SubItems(i).Text)
                        Next
                        writer.WriteLine(String.Join(",", values))
                    Next
                End Using

                MessageBox.Show("Export completed successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Process.Start(saveDialog.FileName)
            End If
        Catch ex As Exception
            MessageBox.Show("Export failed: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
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

                ' Check payment status
                Dim paymentStatus As String = ReservationList.SelectedItems(0).SubItems(6).Text ' Payment Status is in the 7th column (index 6)
                If paymentStatus = "Partial" Then
                    MessageBox.Show("Cannot assign this reservation as it has partial payment. Please ensure full payment is made first.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Return
                End If

                Dim reservationID As Integer = Convert.ToInt32(ReservationList.SelectedItems(0).Tag)
                Dim clientID As Integer = GetClientIDFromReservation(reservationID)

                If clientID = -1 Then
                    MessageBox.Show("Client ID not found for the selected reservation.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return
                End If

                ' Check Family Lawn Lots payment status before opening the form
                If Not CheckFamilyLawnLotsPayment(reservationID, clientID) Then
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

    Private Sub Guna2Panel2_Paint(sender As Object, e As PaintEventArgs) Handles Guna2Panel2.Paint

    End Sub
End Class