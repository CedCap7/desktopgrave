Imports Guna.UI2.WinForms
Imports MySql.Data.MySqlClient
Imports Newtonsoft.Json
Imports System.Globalization
Imports System.Linq
Imports System.Collections.Generic
Imports System.Diagnostics

Public Class frmPlotPurchAndAssign
    Private _plotSelectionForm As frmPlotSelection = Nothing
    Private _selectedPlotsDict As New Dictionary(Of Integer, (Level As Integer, LocationString As String))
    Private _currentPlotCount As Integer = 0
    Private clientSuggestions As DataTable
    Private selectedClientId As Integer = -1
    Private _parentForm As frmPlotPurchAndAssign = Nothing

    ' Add public property to access selected plots
    Public ReadOnly Property SelectedPlots As Dictionary(Of Integer, (Level As Integer, LocationString As String))
        Get
            Return _selectedPlotsDict
        End Get
    End Property

    Private Sub frmPlotPurchAndAssign_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Module1.dbconn() ' Initialize the database connection
        LoadPackages()
        SetupClientSearchBox()
        lblQuantity.Visible = False
        currentQuantity.Visible = False
        currentQuantity.Value = 1
        currentQuantity.Minimum = 1
        currentQuantity.Maximum = 100
        txtTotal.Visible = False
    End Sub

    Private Sub SetupClientSearchBox()
        txtClientSearch.PlaceholderText = "Search Client..."
        lstClientSuggestions.Width = txtClientSearch.Width
        lstClientSuggestions.Height = 100
        lstClientSuggestions.Top = txtClientSearch.Bottom
        lstClientSuggestions.Left = txtClientSearch.Left
        lstClientSuggestions.Visible = False
    End Sub


    Private Sub txtClientSearch_TextChanged(sender As Object, e As EventArgs) Handles txtClientSearch.TextChanged
        If txtClientSearch.Text.Length < 2 Then
            lstClientSuggestions.Visible = False
            Return
        End If

        Dim input As String = txtClientSearch.Text.Trim()
        Dim connectionString As String = "server=srv594.hstgr.io; database=u976878483_cemetery; username=u976878483_doncarlos; password=d0Nc4los; port=3306"

        Try
            Dim query As String = "SELECT Client_ID, " &
                              "CONCAT(FirstName, ' ', " &
                              "IF(MiddleName IS NULL OR MiddleName = '', '', CONCAT(LEFT(MiddleName, 1), '. ')), " &
                              "LastName) AS FullName " &
                              "FROM client WHERE CONCAT(FirstName, ' ', IF(MiddleName IS NULL OR MiddleName = '', '', CONCAT(LEFT(MiddleName, 1), '. ')), LastName) LIKE @search " &
                              "ORDER BY Client_ID DESC"

            Using cn As New MySqlConnection(connectionString)
                cn.Open()
                Using cmd As New MySqlCommand(query, cn)
                    cmd.Parameters.AddWithValue("@search", "%" & input & "%")
                    Dim adapter As New MySqlDataAdapter(cmd)
                    clientSuggestions = New DataTable()
                    adapter.Fill(clientSuggestions)

                    lstClientSuggestions.Items.Clear()
                    For Each row As DataRow In clientSuggestions.Rows
                        lstClientSuggestions.Items.Add(row("FullName").ToString())
                    Next
                    lstClientSuggestions.Visible = clientSuggestions.Rows.Count > 0
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error loading client suggestions: " & ex.Message)
        End Try
    End Sub


    Private Sub lstClientSuggestions_Click(sender As Object, e As EventArgs) Handles lstClientSuggestions.Click
        Dim selectedName As String = lstClientSuggestions.SelectedItem?.ToString()
        If String.IsNullOrEmpty(selectedName) Then Return

        ' Search the DataTable for the matching row
        Dim matchingRows() As DataRow = clientSuggestions.Select($"FullName = '{selectedName.Replace("'", "''")}'")

        If matchingRows.Length > 0 Then
            txtClientSearch.Text = selectedName
            selectedClientId = Convert.ToInt32(matchingRows(0)("Client_ID"))
            lstClientSuggestions.Visible = False
            LoadDeceasedList()
        Else
            MessageBox.Show("Selected client not found in the list.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    Private Sub LoadPackages()
        Try
            Dim connectionString As String = "server=srv594.hstgr.io; database=u976878483_cemetery; username=u976878483_doncarlos; password=d0Nc4los; port=3306"
            Using cn As New MySqlConnection(connectionString)
                cn.Open()
                Dim query As String = "SELECT p_id, package_name, description, price FROM package"
                Using cmd As New MySqlCommand(query, cn)
                    Dim adapter As New MySqlDataAdapter(cmd)
                    Dim dt As New DataTable()
                    adapter.Fill(dt)

                    Dim defaultRow As DataRow = dt.NewRow()
                    defaultRow("p_id") = 0
                    defaultRow("package_name") = "Select a Package"
                    defaultRow("description") = ""
                    defaultRow("price") = 0
                    dt.Rows.InsertAt(defaultRow, 0)

                    GraveType.DataSource = dt
                    GraveType.DisplayMember = "package_name"
                    GraveType.ValueMember = "p_id"
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error loading packages: " & ex.Message)
        End Try
    End Sub


    Private Sub LoadDeceasedList()
        Try
            If selectedClientId <= 0 Then
                cmbDeceased.DataSource = Nothing
                Return
            End If

            Dim query As String = "SELECT Deceased_ID, CONCAT(FirstName, ' ', LastName) AS FullName FROM deceased WHERE Client_ID = @ClientId AND Plot_ID IS NULL"
            Using cmd As New MySqlCommand(query, Module1.cn)
                cmd.Parameters.AddWithValue("@ClientId", selectedClientId)
                Dim adapter As New MySqlDataAdapter(cmd)
                If Module1.cn.State <> ConnectionState.Open Then
                    Module1.cn.Open() ' Open the connection only if it's not already open
                End If
                Dim dt As New DataTable()
                adapter.Fill(dt)

                Dim defaultRow As DataRow = dt.NewRow()
                defaultRow("Deceased_ID") = 0
                defaultRow("FullName") = "Select a Deceased"
                dt.Rows.InsertAt(defaultRow, 0)

                cmbDeceased.DataSource = dt
                cmbDeceased.DisplayMember = "FullName"
                cmbDeceased.ValueMember = "Deceased_ID"
                cmbDeceased.SelectedIndex = 0 ' Set the default selected index to the first item
            End Using
        Catch ex As Exception
            MessageBox.Show("Error loading deceased list: " & ex.Message)
        Finally
            If Module1.cn.State = ConnectionState.Open Then
                Module1.cn.Close() ' Ensure the connection is closed
            End If
        End Try
    End Sub

    Private Sub btnSelectPlot_Click(sender As Object, e As EventArgs) Handles btnSelectPlot.Click
        If GraveType.SelectedValue Is Nothing OrElse Convert.ToInt32(GraveType.SelectedValue) = 0 Then
            MessageBox.Show("Please select a package first", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If _currentPlotCount >= currentQuantity.Value Then
            MessageBox.Show("You have already selected the maximum number of plots.", "Maximum Plots Selected", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        ' Figure out which layer name to pass in...
        Dim selectedPackageType As String = ""
        Select Case DirectCast(GraveType.SelectedItem, DataRowView)("description").ToString().Trim().ToLower()
            Case "private" : selectedPackageType = "private"
            Case "family lawn lot" : selectedPackageType = "lawnlots"
            Case "bone niche" : selectedPackageType = "boneniche"
            Case "apartment" : selectedPackageType = "apartment"
            Case Else
                MessageBox.Show("Invalid grave type selected", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
        End Select

        Console.WriteLine($"Selected package type: {selectedPackageType}") ' Debug log

        ' Clean up existing form if it exists
        If _plotSelectionForm IsNot Nothing Then
            RemoveHandler _plotSelectionForm.PlotSelected, AddressOf OnPlotSelected
            _plotSelectionForm.Close()
            _plotSelectionForm.Dispose()
            _plotSelectionForm = Nothing
        End If

        ' Create new form
        If selectedPackageType = "lawnlots" OrElse selectedPackageType = "boneniche" Then
            _plotSelectionForm = New frmPlotSelection(selectedPackageType, selectedClientId)
        Else
            _plotSelectionForm = New frmPlotSelection(selectedPackageType)
        End If
        AddHandler _plotSelectionForm.PlotSelected, AddressOf OnPlotSelected

        ' Embed it in your subSidePanel
        With subSidePanel
            .Controls.Clear()
            _plotSelectionForm.TopLevel = False
            _plotSelectionForm.FormBorderStyle = FormBorderStyle.None
            _plotSelectionForm.Dock = DockStyle.Fill
            .Controls.Add(_plotSelectionForm)
        End With

        _plotSelectionForm.Show()
    End Sub

    Private Sub OnPlotSelected(plotId As Integer, locationString As String, level As Integer)
        Try
            If _currentPlotCount >= currentQuantity.Value Then
                MessageBox.Show("You have already selected the maximum number of plots.", "Maximum Plots Selected", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            ' Check if this plot is already selected
            If _selectedPlotsDict.ContainsKey(plotId) Then
                MessageBox.Show("This plot has already been selected.", "Duplicate Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            ' Add new plot selection
            _selectedPlotsDict.Add(plotId, (level, locationString))
            _currentPlotCount += 1

            ' Update the plot location display
            UpdatePlotLocationsDisplay()

            ' If this is not the first plot selection, validate adjacency and block
            If _parentForm IsNot Nothing AndAlso _parentForm.SelectedPlots.Count > 0 Then
                ' Get the last selected plot instead of the first one
                Dim lastPlot = _parentForm.SelectedPlots.Last()

                ' Get the last selected plot's details
                Using cmd3 As New MySqlCommand("SELECT block, section, row, plot FROM location WHERE id = @plotId", Module1.cn)
                    cmd3.Parameters.AddWithValue("@plotId", lastPlot.Key)
                    Using reader As MySqlDataReader = cmd3.ExecuteReader()
                        If reader.Read() Then
                            ' Check if plots are in the same block
                            If reader("block").ToString() <> locationString.Split(", ")(0) Then
                                MessageBox.Show("Selected plot must be in the same block as the previous selection.", "Invalid Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                Return
                            End If

                            ' Check if plots are in the same section
                            If reader("section").ToString() <> locationString.Split(", ")(1) Then
                                MessageBox.Show("Selected plot must be in the same section as the previous selection.", "Invalid Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                Return
                            End If

                            ' Check if plots are in the same row
                            If reader("row").ToString() <> locationString.Split(", ")(2) Then
                                MessageBox.Show("Selected plot must be in the same row as the previous selection.", "Invalid Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                Return
                            End If

                            ' Check if plots are adjacent
                            Dim lastPlotNumber As Integer = Convert.ToInt32(reader("plot"))
                            Dim currentPlotNumber As Integer = Convert.ToInt32(locationString.Split(", ")(3))

                            If Math.Abs(lastPlotNumber - currentPlotNumber) <> 1 Then
                                MessageBox.Show("Selected plot must be adjacent to the previous selection.", "Invalid Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                Return
                            End If
                        End If
                    End Using
                End Using
            End If

            ' Only close the form when we've reached the desired quantity
            If _currentPlotCount >= currentQuantity.Value Then
                If _plotSelectionForm IsNot Nothing Then
                    RemoveHandler _plotSelectionForm.PlotSelected, AddressOf OnPlotSelected
                    _plotSelectionForm.Close()
                    _plotSelectionForm.Dispose()
                    _plotSelectionForm = Nothing
                End If
            End If
        Catch ex As Exception
            MessageBox.Show("Error in plot selection: " & ex.Message)
        End Try
    End Sub

    Private Sub UpdatePlotLocationsDisplay()
        SelectedPlotsList.Items.Clear()
        Dim index As Integer = 1
        For Each plot In _selectedPlotsDict
            Dim item As New ListViewItem($"Plot {index}: {plot.Value.LocationString}")
            SelectedPlotsList.Items.Add(item)
            index += 1
        Next
        SelectedPlotsList.Visible = True
    End Sub

    Private Sub SaveReservation()
        If Not ValidateSelections() Then Return

        If _selectedPlotsDict.Count = 0 Then
            MessageBox.Show("Please select at least one plot", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' Check for duplicate plot IDs
        If _selectedPlotsDict.Count <> _selectedPlotsDict.Distinct().Count() Then
            MessageBox.Show("Duplicate plot selections detected. Please select different plots.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim deceasedId As Integer? = Nothing
        Dim status As Integer = 0 ' Default status to 0 (no deceased selected)

        ' Check the selected index of cmbDeceased
        If cmbDeceased.SelectedIndex = 0 Then
            status = 0 ' No deceased selected
        ElseIf cmbDeceased.SelectedItem IsNot Nothing Then
            Dim selectedRow As DataRowView = DirectCast(cmbDeceased.SelectedItem, DataRowView)
            deceasedId = Convert.ToInt32(selectedRow("Deceased_ID"))
            status = 1 ' Set status to 1 if a deceased is selected
        End If

        Dim clientId As Integer = selectedClientId
        If clientId <= 0 Then
            Dim result = MessageBox.Show("No client is selected. Do you want to proceed with the reservation without a client?",
             "Confirm Reservation",
             MessageBoxButtons.YesNo,
             MessageBoxIcon.Question)
            If result = DialogResult.No Then
                Return
            End If
        End If

        Dim transaction As MySqlTransaction = Nothing
        Try
            ' Ensure the connection is open before starting the transaction
            If Module1.cn Is Nothing Then
                MessageBox.Show("Database connection is not initialized.", "Connection Error")
                Return
            End If

            If Module1.cn.State <> ConnectionState.Open Then
                Module1.cn.Open()
            End If

            transaction = Module1.cn.BeginTransaction()
            Try
                ' Declare and initialize selectedRow here
                Dim selectedRow As DataRowView = DirectCast(GraveType.SelectedItem, DataRowView)
                Dim packageType As Integer = Convert.ToInt32(selectedRow("p_id"))
                Dim packagePrice As Decimal = Convert.ToDecimal(selectedRow("price"))
                Dim totalAmount As Decimal = packagePrice * currentQuantity.Value

                ' For Family Lawn Lots, create one reservation for all adjacent plots
                If packageType = 2 Then ' Family Lawn Lot
                    ' Create separate reservations for each plot
                    For Each plot In _selectedPlotsDict
                        ' Check if this plot is already reserved
                        Dim checkQuery As String = "SELECT COUNT(*) FROM plot_reservation WHERE plot_id = @plot_id"
                        Using checkCmd As New MySqlCommand(checkQuery, Module1.cn, transaction)
                            checkCmd.Parameters.AddWithValue("@plot_id", plot.Key)
                            Dim count As Integer = Convert.ToInt32(checkCmd.ExecuteScalar())
                            If count > 0 Then
                                Throw New Exception($"Plot {plot.Key} is already reserved.")
                            End If
                        End Using

                        ' Insert into reservation table for each plot
                        Dim reservationQuery As String = "INSERT INTO reservation (Client_ID, p_id, Reservation_Date, Status, Quantity) " &
                         "VALUES (@Client_ID, @p_id, @Reservation_Date, @Status, @Quantity)"

                        Dim reservationId As Integer
                        Using cmd As New MySqlCommand(reservationQuery & "; SELECT LAST_INSERT_ID();", Module1.cn, transaction)
                            cmd.Parameters.AddWithValue("@Client_ID", clientId)
                            cmd.Parameters.AddWithValue("@p_id", plot.Key) ' Use the plot ID from location table
                            cmd.Parameters.AddWithValue("@Reservation_Date", DateTime.Now)
                            cmd.Parameters.AddWithValue("@Status", status)
                            cmd.Parameters.AddWithValue("@Quantity", 1) ' Each plot gets quantity 1

                            reservationId = Convert.ToInt32(cmd.ExecuteScalar())
                        End Using

                        ' Insert plot reservation
                        Dim plotReservationQuery As String = "INSERT INTO plot_reservation (reservation_id, plot_id, level) VALUES (@reservation_id, @plot_id, @level)"
                        Using cmd As New MySqlCommand(plotReservationQuery, Module1.cn, transaction)
                            cmd.Parameters.AddWithValue("@reservation_id", reservationId)
                            cmd.Parameters.AddWithValue("@plot_id", plot.Key)
                            cmd.Parameters.AddWithValue("@level", 0) ' Level is always 0 for lawn lots
                            cmd.ExecuteNonQuery()
                        End Using

                        ' Update deceased record if selected (only for the first plot)
                        If deceasedId.HasValue AndAlso plot.Key = _selectedPlotsDict.First().Key Then
                            Dim updateDeceasedQuery As String = "UPDATE deceased SET Plot_ID = @PlotID, Level = @Level WHERE Deceased_ID = @DeceasedID"
                            Using cmd As New MySqlCommand(updateDeceasedQuery, Module1.cn, transaction)
                                cmd.Parameters.AddWithValue("@PlotID", plot.Key)
                                cmd.Parameters.AddWithValue("@Level", 0)
                                cmd.Parameters.AddWithValue("@DeceasedID", deceasedId.Value)
                                cmd.ExecuteNonQuery()
                            End Using
                        End If

                        ' Record the payment for each plot
                        Dim paymentQuery As String = "INSERT INTO payment (Reservation_ID, total_Amount, total_Paid, Payment_Date, Payment_Status) " &
                            "VALUES (@ReservationID, @TotalAmount, @TotalPaid, @PaymentDate, @PaymentStatus)"
                        Using cmd As New MySqlCommand(paymentQuery, Module1.cn, transaction)
                            cmd.Parameters.AddWithValue("@ReservationID", reservationId)
                            cmd.Parameters.AddWithValue("@TotalAmount", packagePrice)
                            cmd.Parameters.AddWithValue("@TotalPaid", 0)
                            cmd.Parameters.AddWithValue("@PaymentDate", DateTime.Now)
                            cmd.Parameters.AddWithValue("@PaymentStatus", 0)
                            cmd.ExecuteNonQuery()
                        End Using
                    Next

                Else
                    ' For other plot types (Apartment and Bone Niche), create separate reservations
                    For Each plot In _selectedPlotsDict
                        ' Check if this plot at this level is already reserved
                        Dim checkQuery As String = "SELECT COUNT(*) FROM plot_reservation pr " &
                                                 "INNER JOIN reservation r ON pr.reservation_id = r.Reservation_ID " &
                                                 "WHERE pr.plot_id = @plot_id AND pr.level = @level"
                        Debug.WriteLine($"Checking reservation for PlotID: {plot.Key}, Level: {plot.Value.Level}") ' Debug log
                        Using checkCmd As New MySqlCommand(checkQuery, Module1.cn, transaction)
                            checkCmd.Parameters.AddWithValue("@plot_id", plot.Key)
                            checkCmd.Parameters.AddWithValue("@level", plot.Value.Level)
                            Dim count As Integer = Convert.ToInt32(checkCmd.ExecuteScalar())
                            If count > 0 Then
                                Throw New Exception($"Plot {plot.Key} at level {plot.Value.Level} is already reserved.")
                            End If
                        End Using

                        ' Insert into reservation table (one reservation per selected plot/level)
                        Dim reservationQuery As String = "INSERT INTO reservation (Client_ID, p_id, Reservation_Date, Status, Quantity) " &
                         "VALUES (@Client_ID, @p_id, @Reservation_Date, @Status, '1')" ' Quantity is 1 for each individual reservation

                        Dim reservationId As Integer
                        Using cmd As New MySqlCommand(reservationQuery & "; SELECT LAST_INSERT_ID();", Module1.cn, transaction)
                            cmd.Parameters.AddWithValue("@Client_ID", clientId)
                            cmd.Parameters.AddWithValue("@p_id", plot.Key)
                            cmd.Parameters.AddWithValue("@Reservation_Date", DateTime.Now)
                            cmd.Parameters.AddWithValue("@Status", status)

                            reservationId = Convert.ToInt32(cmd.ExecuteScalar())
                        End Using

                        ' Insert plot reservation
                        Dim plotReservationQuery As String = "INSERT INTO plot_reservation (reservation_id, plot_id, level) VALUES (@reservation_id, @plot_id, @level)"
                        Using cmd As New MySqlCommand(plotReservationQuery, Module1.cn, transaction)
                            cmd.Parameters.AddWithValue("@reservation_id", reservationId)
                            cmd.Parameters.AddWithValue("@plot_id", plot.Key)
                            cmd.Parameters.AddWithValue("@level", plot.Value.Level)
                            cmd.ExecuteNonQuery()
                        End Using

                        ' Update deceased record with Plot_ID and Level if a deceased person is selected
                        ' Only update for the first plot if multiple plots are selected
                        If deceasedId.HasValue AndAlso plot.Key = _selectedPlotsDict.First().Key Then
                            Dim updateDeceasedQuery As String = "UPDATE deceased SET Plot_ID = @PlotID, Level = @Level WHERE Deceased_ID = @DeceasedID"
                            Using cmd As New MySqlCommand(updateDeceasedQuery, Module1.cn, transaction)
                                cmd.Parameters.AddWithValue("@PlotID", plot.Key)
                                cmd.Parameters.AddWithValue("@Level", plot.Value.Level)
                                cmd.Parameters.AddWithValue("@DeceasedID", deceasedId.Value)
                                cmd.ExecuteNonQuery()
                            End Using
                        End If

                        ' Record the payment for each plot
                        Dim paymentQuery As String = "INSERT INTO payment (Reservation_ID, total_Amount, total_Paid, Payment_Date, Payment_Status) " &
                            "VALUES (@ReservationID, @TotalAmount, @TotalPaid, @PaymentDate, @PaymentStatus)"
                        Using cmd As New MySqlCommand(paymentQuery, Module1.cn, transaction)
                            cmd.Parameters.AddWithValue("@ReservationID", reservationId)
                            cmd.Parameters.AddWithValue("@TotalAmount", packagePrice)
                            cmd.Parameters.AddWithValue("@TotalPaid", 0)
                            cmd.Parameters.AddWithValue("@PaymentDate", DateTime.Now)
                            cmd.Parameters.AddWithValue("@PaymentStatus", 0)
                            cmd.ExecuteNonQuery()
                        End Using
                    Next
                End If

                transaction.Commit()
                MessageBox.Show("Reservation saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Me.Close()
            Catch ex As Exception
                If transaction IsNot Nothing Then
                    transaction.Rollback()
                End If
                Throw ' Re-throw the exception to be caught by the outer catch block
            End Try
        Catch ex As Exception
            MessageBox.Show("Error adding Reservation: " & ex.Message)
        Finally
            ' Clean up resources
            If transaction IsNot Nothing Then
                transaction.Dispose()
            End If
            If Module1.cn IsNot Nothing Then
                If Module1.cn.State = ConnectionState.Open Then
                    Module1.cn.Close()
                End If
            End If
        End Try
    End Sub

    Private Function GenerateUniqueTransactionId() As String
        Dim random As New Random()
        Dim transactionId As String
        Dim exists As Boolean = True

        Dim query As String = "SELECT COUNT(*) FROM transaction WHERE Transaction_ID = @TransactionId"
        Dim connectionString As String = "server=srv594.hstgr.io; database=u976878483_cemetery; username=u976878483_doncarlos; password=d0Nc4los; port=3306" ' adjust if needed

        Using cn As New MySqlConnection(connectionString)
            cn.Open()

            While exists
                transactionId = random.Next(100000, 999999).ToString() ' 6-digit random number

                Using cmd As New MySqlCommand(query, cn)
                    cmd.Parameters.AddWithValue("@TransactionId", transactionId)
                    Dim count As Integer = Convert.ToInt32(cmd.ExecuteScalar())
                    exists = (count > 0)
                End Using
            End While
        End Using

        Return transactionId
    End Function


    Private Function ValidateSelections() As Boolean
        If GraveType.SelectedValue Is Nothing OrElse Convert.ToInt32(GraveType.SelectedValue) = 0 Then
            MessageBox.Show("Please select a package first", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End If

        ' Check if user has made a client selection (either a client or "No Client")
        If selectedClientId = -1 Then
            MessageBox.Show("Please select a client or choose 'No Client'", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End If

        Return True
    End Function

    Private Sub BtnPrintReceipt_Click(sender As Object, e As EventArgs)
        Try
            Dim receiptContent As New System.Text.StringBuilder()
            receiptContent.AppendLine("=== RESERVATION RECEIPT ===")
            receiptContent.AppendLine($"Date: {DateTime.Now}")
            receiptContent.AppendLine($"Client: {txtClientSearch.Text}")
            receiptContent.AppendLine($"{txtPackage.Text}")
            receiptContent.AppendLine($"Quantity: {currentQuantity.Value}")
            receiptContent.AppendLine($"{txtPrice.Text}")
            receiptContent.AppendLine($"{txtTotal.Text}")
            receiptContent.AppendLine("Selected Plots:")
            For Each plot In _selectedPlotsDict
                receiptContent.AppendLine($"Plot {_selectedPlotsDict.Count - _selectedPlotsDict.Count + 1}: {plot.Value.LocationString}")
            Next
            receiptContent.AppendLine("==========================")

            MessageBox.Show(receiptContent.ToString(), "Receipt Preview")
        Catch ex As Exception
            MessageBox.Show("Error printing receipt: " & ex.Message)
        End Try
    End Sub

    Private Sub btnConfirm_Click(sender As Object, e As EventArgs) Handles btnConfirm.Click
        SaveReservation()
    End Sub

    Private Sub cmbDeceased_Click(sender As Object, e As EventArgs) Handles cmbDeceased.Click
        ' Check if a selection has been made (either client or "No Client")
        If selectedClientId = -1 Then
            MessageBox.Show("Please select a client first or choose 'No Client'", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            cmbDeceased.DataSource = Nothing
            Return
        End If

        LoadDeceasedList()
    End Sub

    Private Sub frmPaymentReserv_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Try
            ' Clean up plot selection form
            If _plotSelectionForm IsNot Nothing Then
                RemoveHandler _plotSelectionForm.PlotSelected, AddressOf OnPlotSelected
                _plotSelectionForm.Close()
                _plotSelectionForm.Dispose()
                _plotSelectionForm = Nothing
            End If

            ' Clear any remaining controls in subSidePanel
            If subSidePanel IsNot Nothing Then
                subSidePanel.Controls.Clear()
            End If

            ' Clear any remaining data
            _selectedPlotsDict.Clear()
            If clientSuggestions IsNot Nothing Then
                clientSuggestions.Clear()
            End If

            ' Close database connection if open - DO NOT DISPOSE SHARED CONNECTION HERE
            If Module1.cn IsNot Nothing AndAlso Module1.cn.State = ConnectionState.Open Then
                Module1.cn.Close()
            End If
        Catch ex As Exception
            ' Log error if needed
            Debug.WriteLine("Error during form closing: " & ex.Message)
        End Try
    End Sub

    Private Sub ClearPlotSelections()
        ' Clears the selected plots dictionary and the list display
        _selectedPlotsDict.Clear()
        _currentPlotCount = 0
        If SelectedPlotsList IsNot Nothing Then
            SelectedPlotsList.Items.Clear()
            ' Keep the list visible, even when empty
        End If
    End Sub

    Private Sub ClearPackageDetails()
        ' Clears the package detail text boxes
        If txtGraveType IsNot Nothing Then txtGraveType.Text = "Grave Type:"
        If txtPackage IsNot Nothing Then txtPackage.Text = "Package Type:"
        If txtPrice IsNot Nothing Then txtPrice.Text = "Price:"
        If txtTotal IsNot Nothing Then
            txtTotal.Text = "Total: ₱0.00"
            txtTotal.Visible = False
        End If
    End Sub

    Private Sub PopulateDeceasedComboBox()
        ' Populates the deceased combo box based on the selected client
        LoadDeceasedList()
    End Sub

    Private Sub ResetPlotSelections()
        ' Resets the plot selections and clears the display
        ClearPlotSelections()
        ' Optionally, reset quantity to 1 if applicable
        If currentQuantity IsNot Nothing Then currentQuantity.Value = 1
        If lblQuantity IsNot Nothing Then lblQuantity.Visible = False
        If currentQuantity IsNot Nothing Then currentQuantity.Visible = False
    End Sub

    Private Function GetDeceasedCount(plotId As Integer) As Integer
        Try
            Using cmd As New MySqlCommand("SELECT COUNT(*) FROM deceased WHERE Plot_ID = @plotId", Module1.cn)
                cmd.Parameters.AddWithValue("@plotId", plotId)
                Return Convert.ToInt32(cmd.ExecuteScalar())
            End Using
        Catch ex As Exception
            MessageBox.Show("Error getting deceased count: " & ex.Message)
            Return 4
        End Try
    End Function

    Private Function GetLevelDeceasedCount(plotId As Integer, level As Integer) As Integer
        Try
            Using cmd As New MySqlCommand("SELECT COUNT(*) FROM deceased WHERE Plot_ID = @plotId AND Level = @level AND Level > 0", Module1.cn)
                cmd.Parameters.AddWithValue("@plotId", plotId)
                cmd.Parameters.AddWithValue("@level", level)
                Return Convert.ToInt32(cmd.ExecuteScalar())
            End Using
        Catch ex As Exception
            MessageBox.Show("Error getting level count: " & ex.Message)
            Return 10
        End Try
    End Function

    Private Function IsLevelOccupied(plotId As Integer, level As Integer) As Boolean
        Try
            Using cmd As New MySqlCommand("SELECT COUNT(*) FROM deceased WHERE Plot_ID = @plotId AND Level = @level AND Level > 0", Module1.cn)
                cmd.Parameters.AddWithValue("@plotId", plotId)
                cmd.Parameters.AddWithValue("@level", level)
                Return Convert.ToInt32(cmd.ExecuteScalar()) > 0
            End Using
        Catch ex As Exception
            MessageBox.Show("Error checking level occupancy: " & ex.Message)
            Return True
        End Try
    End Function

    Private Sub GraveType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles GraveType.SelectedIndexChanged
        Try
            If GraveType.SelectedValue IsNot Nothing AndAlso
           TypeOf GraveType.SelectedValue Is Integer AndAlso
           Convert.ToInt32(GraveType.SelectedValue) <> 0 Then

                Dim selectedPackageId As Integer = Convert.ToInt32(DirectCast(GraveType.SelectedItem, DataRowView)("p_id"))
                Dim description As String = DirectCast(GraveType.SelectedItem, DataRowView)("description").ToString().Trim().ToLower()

                ' *** DEBUG: Output selected package info ***
                Debug.WriteLine($"Selected Package ID: {selectedPackageId}, Description: {description}")
                ' ******************************************

                ' Show quantity only for family lawn lot (p_id = 2)
                If selectedPackageId = 2 Then
                    lblQuantity.Visible = True
                    currentQuantity.Visible = True
                    currentQuantity.Value = 1
                    cmbDeceased.Visible = False
                    Label8.Visible = False
                ElseIf description = "apartment" OrElse description = "bone niche" Then
                    lblQuantity.Visible = False
                    currentQuantity.Visible = False
                    currentQuantity.Value = 1
                    cmbDeceased.Visible = True
                    Label8.Visible = True
                Else
                    lblQuantity.Visible = False
                    currentQuantity.Visible = False
                    currentQuantity.Value = 1
                    cmbDeceased.Visible = False
                    Label8.Visible = False
                End If

                LoadPackageDetails(selectedPackageId)
                ResetPlotSelections()
                UpdateTotalAmount()
            Else
                txtGraveType.Text = "Grave Type:"
                txtPackage.Text = "Package:"
                txtPrice.Text = "Price:"
                txtTotal.Text = "Total: ₱0.00"
                txtTotal.Visible = False
                lblQuantity.Visible = False
                currentQuantity.Visible = False
                currentQuantity.Value = 1
                cmbDeceased.Visible = False
                Label8.Visible = False
                ResetPlotSelections()
            End If
        Catch ex As Exception
            MessageBox.Show("Error loading package details: " & ex.Message)
        End Try
    End Sub

    Private Sub LoadPackageDetails(p_id As Integer)
        Try
            If Module1.cn.State = ConnectionState.Open Then Module1.cn.Close()

            Dim query As String = "SELECT * FROM package WHERE p_id = @p_id"
            Using cmd As New MySqlCommand(query, Module1.cn)
                cmd.Parameters.AddWithValue("@p_id", p_id)
                If Module1.cn.State <> ConnectionState.Open Then
                    Module1.cn.Open() ' Open the connection only if it's not already open
                End If
                Using reader As MySqlDataReader = cmd.ExecuteReader()
                    If reader.Read() Then
                        Dim packagePrice As Decimal = Convert.ToDecimal(reader("price"))
                        Dim quantity As Integer = Convert.ToInt32(currentQuantity.Value)
                        Dim totalAmount As Decimal = packagePrice * quantity

                        txtGraveType.Text = $"Grave Type: {reader("description").ToString()}"
                        txtPackage.Text = $"Package Type: {reader("description")}"
                        txtPrice.Text = $"Price: ₱{packagePrice:N2}"
                        txtTotal.Text = $"Total: ₱{totalAmount:N2}"
                        txtTotal.Visible = True
                    End If
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error loading package details: " & ex.Message)
        Finally
            If Module1.cn.State = ConnectionState.Open Then
                Module1.cn.Close() ' Ensure the connection is closed
            End If
        End Try
    End Sub

    Private Sub UpdateTotalAmount()
        Try
            If GraveType.SelectedValue IsNot Nothing AndAlso
           TypeOf GraveType.SelectedValue Is Integer AndAlso
           Convert.ToInt32(GraveType.SelectedValue) <> 0 Then

                Dim selectedRow As DataRowView = DirectCast(GraveType.SelectedItem, DataRowView)
                Dim packagePrice As Decimal = Convert.ToDecimal(selectedRow("price"))
                Dim quantity As Integer = Convert.ToInt32(currentQuantity.Value)
                Dim totalAmount As Decimal = packagePrice * quantity

                txtTotal.Text = $"Total: ₱{totalAmount:N2}"
                txtTotal.Visible = True
            Else
                txtTotal.Text = "Total: ₱0.00"
                txtTotal.Visible = False
            End If
        Catch ex As Exception
            MessageBox.Show("Error updating total amount: " & ex.Message)
            txtTotal.Text = "Total: ₱0.00"
            txtTotal.Visible = False
        End Try
    End Sub

    Private Function GetLocationTypeId(plotId As Integer) As Integer
        Try
            Dim connectionString As String = "server=srv594.hstgr.io; database=u976878483_cemetery; username=u976878483_doncarlos; password=d0Nc4los; port=3306"
            Using cn As New MySqlConnection(connectionString)
                cn.Open()
                Dim query As String = "SELECT type FROM location WHERE id = @LocationId"
                Using cmd As New MySqlCommand(query, cn)
                    cmd.Parameters.AddWithValue("@LocationId", plotId)
                    Dim result As Object = cmd.ExecuteScalar()
                    If result IsNot Nothing AndAlso IsNumeric(result) Then
                        Return Convert.ToInt32(result)
                    Else
                        Return 0 ' Default value if no valid Type_ID is found
                    End If
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error fetching location type: " & ex.Message)
            Return 0
        End Try
    End Function


    Private Sub currentQuantity_ValueChanged(sender As Object, e As EventArgs) Handles currentQuantity.ValueChanged
        UpdateTotalAmount()

        ' Optional: Notify user when quantity > 1
        If currentQuantity.Value > 1 Then
            MessageBox.Show($"Please select {currentQuantity.Value} plots", "Plot Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

End Class