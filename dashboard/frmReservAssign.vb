Imports MySql.Data.MySqlClient

Public Class frmReservAssign
    Private ReservationID As Integer
    Private ClientID As Integer

    ' Constructor
    Public Sub New(reservationId As Integer, clientId As Integer)
        InitializeComponent()
        Me.ReservationID = reservationId
        Me.ClientID = clientId
        Module1.dbconn() ' Initialize the database connection
    End Sub

    Private Sub frmReservAssign_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadReservationDetails()
        LoadDeceasedList()
    End Sub

    Private Sub LoadReservationDetails()
        Try
            If Module1.cn.State = ConnectionState.Open Then
                Module1.cn.Close()
            End If
            Module1.cn.Open()

            ' Updated SQL query to get client name and plot details from plot_reservation
            Dim sql As String = "SELECT " &
            "CONCAT(c.FirstName, ' ', COALESCE(LEFT(c.MiddleName, 1), ''), '. ', c.LastName) AS ClientName, " &
            "pr.Plot_ID, pr.Level, l.type, l.block, l.section, l.row, l.plot " &
            "FROM plot_reservation pr " &  ' Selecting from plot_reservation table
            "JOIN reservation r ON pr.Reservation_ID = r.Reservation_ID " &  ' Join on Reservation_ID
            "JOIN client c ON r.Client_ID = c.Client_ID " &  ' Join on Client_ID from reservation
            "JOIN location l ON pr.Plot_ID = l.id " &  ' Join on Plot_ID
            "WHERE pr.Reservation_ID = @ReservationID"

            Using cmd As New MySqlCommand(sql, Module1.cn)
                cmd.Parameters.AddWithValue("@ReservationID", ReservationID)
                Using dr As MySqlDataReader = cmd.ExecuteReader()
                    If dr.Read() Then
                        ' Display client name
                        LblClient.Text = "Client Name: " & dr("ClientName").ToString()

                        ' Get plot details
                        Dim plotId As Integer = If(dr("Plot_ID") Is DBNull.Value, 0, Convert.ToInt32(dr("Plot_ID")))
                        Dim level As Integer = If(dr("Level") Is DBNull.Value, 0, Convert.ToInt32(dr("Level")))
                        Dim plotTypeValue As Integer = Convert.ToInt32(dr("type")) ' Get the numeric type value
                        Dim block As String = dr("block").ToString()
                        Dim section As String = dr("section").ToString()
                        Dim row As String = dr("row").ToString()
                        Dim plot As String = dr("plot").ToString()

                        ' Determine the plot type based on the numeric value
                        Dim plotType As String
                        Select Case plotTypeValue
                            Case 1
                                plotType = "Apartment"
                            Case 2
                                plotType = "Family Lawn Lots"
                            Case 3
                                plotType = "Bone Niche"
                            Case 4
                                plotType = "Private"
                            Case Else
                                plotType = "Unknown Type" ' Fallback for unexpected values
                        End Select

                        ' Format and display plot location
                        Dim plotLocation As String = String.Format("{0} - Block {1}, Section {2}, Row {3}, Plot {4}{5}",
                        plotType,
                        block,
                        section,
                        row,
                        plot,
                        If(level > 0, ", Level " & level.ToString(), "")) ' Append level if greater than 0

                        LblPlotReserved.Text = "Reserved Plot: " & plotLocation
                    Else
                        MessageBox.Show("No reservation details found.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                End Using
            End Using

        Catch ex As Exception
            MessageBox.Show("Error loading reservation details: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Module1.cn.State = ConnectionState.Open Then
                Module1.cn.Close()
            End If
        End Try
    End Sub

    Private Sub LoadDeceasedList()
        Try
            If Module1.cn.State = ConnectionState.Open Then
                Module1.cn.Close()
            End If
            Module1.cn.Open()

            ' Create DataTable for ComboBox
            Dim dt As New DataTable()
            dt.Columns.Add("Deceased_ID", GetType(Integer))
            dt.Columns.Add("FullName", GetType(String))

            ' Add default "Select Deceased" row
            dt.Rows.Add(-1, "Select Deceased")

            ' Get deceased associated with the client who do not have an assigned plot
            Dim sql As String = "SELECT Deceased_ID, " &
                          "CONCAT(COALESCE(FirstName, ''), " &
                          "IF(MiddleName IS NULL, '', CONCAT(' ', LEFT(MiddleName, 1), '.')), " &
                          "IF(LastName IS NULL, '', CONCAT(' ', LastName))) AS FullName " &
                          "FROM deceased " &
                          "WHERE Client_ID = @ClientID AND Plot_ID IS NULL " &  ' Ensure no assigned plot
                          "AND TRIM(CONCAT(COALESCE(FirstName, ''), " &
                          "COALESCE(MiddleName, ''), " &
                          "COALESCE(LastName, ''))) != ''"

            Using cmd As New MySqlCommand(sql, Module1.cn)
                cmd.Parameters.AddWithValue("@ClientID", ClientID)
                Using dr As MySqlDataReader = cmd.ExecuteReader()
                    While dr.Read()
                        Dim fullName As String = dr("FullName").ToString().Trim()
                        If Not String.IsNullOrEmpty(fullName) Then
                            dt.Rows.Add(dr("Deceased_ID"), fullName)
                        End If
                    End While
                End Using
            End Using

            ' Bind to ComboBox
            cmbDeceased.DataSource = dt
            cmbDeceased.DisplayMember = "FullName"
            cmbDeceased.ValueMember = "Deceased_ID"

        Catch ex As Exception
            MessageBox.Show("Error loading deceased list: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Module1.cn.State = ConnectionState.Open Then
                Module1.cn.Close()
            End If
        End Try
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Try
            ' Just close the form
            Me.Close()
        Catch ex As Exception
            MessageBox.Show("Error closing form: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub frmReservAssign_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Try
            ' Clear data bindings
            If cmbDeceased.DataSource IsNot Nothing Then
                cmbDeceased.DataSource = Nothing
            End If

            ' Clear labels
            LblClient.Text = ""
            LblPlotReserved.Text = ""

            ' Clean up connection
            If Module1.cn IsNot Nothing Then
                If Module1.cn.State = ConnectionState.Open Then
                    Module1.cn.Close()
                End If
            End If

        Catch ex As Exception
            Debug.WriteLine("Error during form cleanup: " & ex.Message)
        End Try
    End Sub

    Private Sub btnAssign_Click(sender As Object, e As EventArgs) Handles btnAssign.Click
        ' Validate deceased selection
        If cmbDeceased.SelectedValue Is Nothing OrElse
       Not TypeOf cmbDeceased.SelectedValue Is Integer OrElse
       Convert.ToInt32(cmbDeceased.SelectedValue) = -1 Then
            MessageBox.Show("Please select a deceased person to assign.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Try
            If Module1.cn.State = ConnectionState.Open Then
                Module1.cn.Close()
            End If
            Module1.cn.Open()

            Using transaction As MySqlTransaction = Module1.cn.BeginTransaction()
                Try
                    ' Get Plot_ID, Level, and plot type from plot_reservation and location
                    Dim plotId As Integer
                    Dim level As Integer
                    Dim plotType As Integer
                    Dim block As String = ""
                    Dim section As String = ""
                    Dim row As String = ""
                    Dim plot As String = ""
                    Dim sql As String = "SELECT pr.Plot_ID, pr.Level, l.type, l.block, l.section, l.row, l.plot " &
                                    "FROM plot_reservation pr " &
                                    "JOIN location l ON pr.Plot_ID = l.id " &
                                    "WHERE pr.Reservation_ID = @ReservationID"

                    Using cmd As New MySqlCommand(sql, Module1.cn, transaction)
                        cmd.Parameters.AddWithValue("@ReservationID", ReservationID)
                        Using dr As MySqlDataReader = cmd.ExecuteReader()
                            If dr.Read() Then
                                plotId = Convert.ToInt32(dr("Plot_ID"))
                                level = If(dr("Level") Is DBNull.Value, 0, Convert.ToInt32(dr("Level")))
                                plotType = Convert.ToInt32(dr("type"))
                                block = dr("block").ToString()
                                section = dr("section").ToString()
                                row = dr("row").ToString()
                                plot = dr("plot").ToString()
                            Else
                                MessageBox.Show("No plot reservation found for this reservation ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                Return
                            End If
                        End Using
                    End Using

                    ' For Family Lawn Lots (type 2), check if all adjacent plots are fully paid
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

                        Using cmd As New MySqlCommand(sql, Module1.cn, transaction)
                            cmd.Parameters.AddWithValue("@Block", block)
                            cmd.Parameters.AddWithValue("@Section", section)
                            cmd.Parameters.AddWithValue("@Row", row)
                            cmd.Parameters.AddWithValue("@ClientID", ClientID)
                            Using dr As MySqlDataReader = cmd.ExecuteReader()
                                Dim adjacentPlots As New List(Of String)
                                While dr.Read()
                                    Dim plotNumber As String = dr("plot").ToString()
                                    Dim fullyPaidCount As Integer = Convert.ToInt32(dr("fully_paid_count"))
                                    Dim totalPayments As Integer = Convert.ToInt32(dr("total_payments"))
                                    
                                    ' If any plot is not fully paid, add it to the list
                                    If fullyPaidCount < totalPayments Then
                                        adjacentPlots.Add(plotNumber)
                                    End If
                                End While

                                ' If there are any unpaid adjacent plots, show error and return
                                If adjacentPlots.Count > 0 Then
                                    MessageBox.Show("Cannot assign deceased. The following adjacent plots are not fully paid: " & 
                                                  String.Join(", ", adjacentPlots), 
                                                  "Payment Required", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                    Return
                                End If
                            End Using
                        End Using
                    End If

                    ' Update deceased record with Plot_ID and Level
                    sql = "UPDATE deceased SET Plot_ID = @PlotID, Level = @Level WHERE Deceased_ID = @DeceasedID"

                    Using cmd As New MySqlCommand(sql, Module1.cn, transaction)
                        cmd.Parameters.AddWithValue("@PlotID", plotId)
                        cmd.Parameters.AddWithValue("@Level", level)
                        cmd.Parameters.AddWithValue("@DeceasedID", Convert.ToInt32(cmbDeceased.SelectedValue))
                        cmd.ExecuteNonQuery()
                    End Using

                    ' Only update reservation status if it's not a Family Lawn Lot (type 2)
                    If plotType <> 2 Then
                        sql = "UPDATE reservation SET Status = 1 WHERE Reservation_ID = @ReservationID"
                        Using cmd As New MySqlCommand(sql, Module1.cn, transaction)
                            cmd.Parameters.AddWithValue("@ReservationID", ReservationID)
                            cmd.ExecuteNonQuery()
                        End Using
                    End If

                    transaction.Commit()
                    MessageBox.Show("Deceased successfully assigned to plot!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

                    ' Refresh the deceased list to remove the assigned deceased
                    LoadDeceasedList()

                    ' Close the form after successful assignment
                    Me.Close()

                Catch ex As Exception
                    transaction.Rollback()
                    MessageBox.Show("Error during assignment: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End Using

        Catch ex As Exception
            MessageBox.Show("Error assigning deceased to plot: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Module1.cn.State = ConnectionState.Open Then
                Module1.cn.Close()
            End If
        End Try
    End Sub
End Class