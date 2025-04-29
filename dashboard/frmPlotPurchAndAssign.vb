Imports Guna.UI2.WinForms
Imports MySql.Data.MySqlClient
Imports Newtonsoft.Json
Imports System.Globalization


Public Class frmPlotPurchAndAssign
    Private _plotSelectionForm As frmPlotSelection = Nothing
    Private conn As New MySqlConnection("server=localhost; user=root; password=root; database=dccms")
    Private _selectedPlots As New List(Of PlotSelection)
    Private _currentPlotCount As Integer = 0
    Private clientSuggestions As DataTable
    Private selectedClientId As Integer = -1

    Private Class PlotSelection
        Public Property PlotId As Integer
        Public Property Level As Integer
        Public Property LocationString As String
    End Class

    Private Sub frmPlotPurchAndAssign_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadPackages()
        SetupClientSearchBox()
        lblQuantity.Visible = False
        txtPaidAmount.Text = ""
        lblPaidAmount.Text = "₱0.00"
        currentQuantity.Visible = False
        currentQuantity.Value = 1
        currentQuantity.Minimum = 1
        currentQuantity.Maximum = 100
    End Sub

    Private Sub SetupClientSearchBox()
        txtClientSearch.PlaceholderText = "Search Client..."
        lstClientSuggestions.Width = txtClientSearch.Width
        lstClientSuggestions.Height = 100
        lstClientSuggestions.Top = txtClientSearch.Bottom
        lstClientSuggestions.Left = txtClientSearch.Left
        lstClientSuggestions.Visible = False
    End Sub

    Private Sub txtPaidAmount_TextChanged(sender As Object, e As EventArgs) Handles txtPaidAmount.TextChanged
        Try
            Dim cleanText = txtPaidAmount.Text.Replace(",", "").Trim()

            If String.IsNullOrEmpty(cleanText) Then
                lblPaidAmount.Text = "₱0.00"
                Return
            End If

            Dim amount As Decimal
            If Decimal.TryParse(cleanText, NumberStyles.Number, CultureInfo.InvariantCulture, amount) Then
                txtPaidAmount.Text = Format(amount, "#,##0")
                lblPaidAmount.Text = $"₱{amount:N2}"
                txtPaidAmount.SelectionStart = txtPaidAmount.Text.Length
            End If
        Catch ex As Exception
            MessageBox.Show("Please enter a valid amount", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtPaidAmount.Text = ""
            lblPaidAmount.Text = "₱0.00"
        End Try
    End Sub

    Private Sub txtClientSearch_TextChanged(sender As Object, e As EventArgs) Handles txtClientSearch.TextChanged
        If txtClientSearch.Text.Length < 2 Then
            lstClientSuggestions.Visible = False
            Return
        End If

        Dim input As String = txtClientSearch.Text.Trim()
        Try
            ' Modified query to format the Middle Name as Initial with a period, if available
            Dim query As String = "SELECT Client_ID, " &
                              "CONCAT(FirstName, ' ', " &
                              "IF(MiddleName IS NULL OR MiddleName = '', '', CONCAT(LEFT(MiddleName, 1), '. ')), " &
                              "LastName) AS FullName " &
                              "FROM client WHERE CONCAT(FirstName, ' ', IF(MiddleName IS NULL OR MiddleName = '', '', CONCAT(LEFT(MiddleName, 1), '. ')), LastName) LIKE @search " &
                              "ORDER BY Client_ID DESC"

            Using cmd As New MySqlCommand(query, conn)
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
            Dim query As String = "SELECT p_id, package_name, description, price FROM package"
            Using cmd As New MySqlCommand(query, conn)
                conn.Open()
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
        Catch ex As Exception
            MessageBox.Show("Error loading packages: " & ex.Message)
        Finally
            conn.Close()
        End Try
    End Sub

    Private Sub LoadDeceasedList()
        Try
            If selectedClientId <= 0 Then
                cmbDeceased.DataSource = Nothing
                Return
            End If

            Dim query As String = "SELECT Deceased_ID, CONCAT(FirstName, ' ', LastName) AS FullName FROM deceased WHERE Client_ID = @ClientId"
            Using cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@ClientId", selectedClientId)
                Dim adapter As New MySqlDataAdapter(cmd)
                Dim dt As New DataTable()
                adapter.Fill(dt)

                Dim defaultRow As DataRow = dt.NewRow()
                defaultRow("Deceased_ID") = 0
                defaultRow("FullName") = "Select a Deceased"
                dt.Rows.InsertAt(defaultRow, 0)

                cmbDeceased.DataSource = dt
                cmbDeceased.DisplayMember = "FullName"
                cmbDeceased.ValueMember = "Deceased_ID"
                cmbDeceased.SelectedIndex = 0
            End Using
        Catch ex As Exception
            MessageBox.Show("Error loading deceased list: " & ex.Message)
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

        ' Dispose any old instance
        If _plotSelectionForm IsNot Nothing Then
            RemoveHandler _plotSelectionForm.PlotSelected, AddressOf OnPlotSelected
            _plotSelectionForm.Dispose()
        End If

        ' Create new one
        _plotSelectionForm = New frmPlotSelection(selectedPackageType)
        AddHandler _plotSelectionForm.PlotSelected, AddressOf OnPlotSelected

        ' **Embed it in your subSidePanel** (instead of ShowDialog)
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
            If _selectedPlots.Any(Function(p) p.PlotId = plotId) Then
                MessageBox.Show("This plot has already been selected.", "Duplicate Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            ' Add new plot selection
            _selectedPlots.Add(New PlotSelection With {
            .PlotId = plotId,
            .Level = level,
            .LocationString = locationString
        })
            _currentPlotCount += 1

            ' Update the plot location display
            UpdatePlotLocationsDisplay()

            ' Close plot selection if we've reached the quantity
            If _currentPlotCount >= currentQuantity.Value Then
                MessageBox.Show("All plots have been selected.", "Plot Selection Complete", MessageBoxButtons.OK, MessageBoxIcon.Information)
                If _plotSelectionForm IsNot Nothing Then
                    _plotSelectionForm.Close()
                End If
            End If
        Catch ex As Exception
            MessageBox.Show("Error in plot selection: " & ex.Message)
        End Try
    End Sub

    Private Sub UpdatePlotLocationsDisplay()
        Dim locations As New System.Text.StringBuilder()
        For i As Integer = 0 To _selectedPlots.Count - 1
            locations.AppendLine($"Plot {i + 1}: {_selectedPlots(i).LocationString}")
        Next
        lblPlotLocation.Text = locations.ToString()
        lblPlotLocation.Visible = True
    End Sub

    Private Sub SaveReservation()
        If Not ValidateSelections() Then Return

        If _selectedPlots.Count = 0 Then
            MessageBox.Show("Please select at least one plot", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If _selectedPlots.Count < currentQuantity.Value Then
            MessageBox.Show($"Please select {currentQuantity.Value} plots before proceeding", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' Get selected deceased ID
        Dim deceasedId As Integer = 0
        If cmbDeceased.SelectedItem IsNot Nothing Then
            Dim selectedRow As DataRowView = DirectCast(cmbDeceased.SelectedItem, DataRowView)
            deceasedId = Convert.ToInt32(selectedRow("Deceased_ID"))
        End If

        ' Check if client is selected
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

        Try
            conn.Open()
            Using transaction As MySqlTransaction = conn.BeginTransaction()
                Try
                    ' Package and total amount calculation
                    Dim selectedRow As DataRowView = DirectCast(GraveType.SelectedItem, DataRowView)
                    Dim packageType As Integer = Convert.ToInt32(selectedRow("p_id"))
                    Dim packagePrice As Decimal = Convert.ToDecimal(selectedRow("price"))
                    Dim totalAmount As Decimal = packagePrice * currentQuantity.Value

                    ' Insert into reservation table with PlotId (p_id from selected plot)
                    ' Create the MySqlCommand with parameters
                    Dim reservationQuery As String = "INSERT INTO reservation (Client_ID, p_id, Reservation_Date, Status, Quantity) " &
                                 "VALUES (@Client_ID, @p_id, @Reservation_Date, '1', @currentQuantity)"

                    Dim reservationId As Integer
                    Using cmd As New MySqlCommand(reservationQuery & "; SELECT LAST_INSERT_ID();", conn, transaction)
                        cmd.Parameters.AddWithValue("@Client_ID", clientId)
                        cmd.Parameters.AddWithValue("@p_id", _selectedPlots(0).PlotId) ' Correctly insert the PlotId from the selected plot
                        cmd.Parameters.AddWithValue("@total_Amount", totalAmount)
                        cmd.Parameters.AddWithValue("@Reservation_Date", DateTime.Now)
                        cmd.Parameters.AddWithValue("@currentQuantity", currentQuantity.Value)

                        ' Log the SQL and parameters for debugging
                        Console.WriteLine("Executing SQL: " & cmd.CommandText)
                        For Each param As MySqlParameter In cmd.Parameters
                            Console.WriteLine($"Parameter: {param.ParameterName}, Value: {param.Value}")
                        Next

                        Try
                            reservationId = Convert.ToInt32(cmd.ExecuteScalar())
                        Catch ex As MySqlException
                            MessageBox.Show("SQL Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        End Try
                    End Using


                    ' Insert into payment table
                    Dim paidAmount As Decimal = 0
                    If Not String.IsNullOrEmpty(txtPaidAmount.Text) Then
                        paidAmount = Decimal.Parse(txtPaidAmount.Text.Replace(",", ""))
                    End If

                    Dim paymentQuery As String = "INSERT INTO payment (Client_ID, total_paid, reservation_id, total_amount, payment_date, payment_status) " &
                                             "VALUES (@Client_ID, @total_paid, @reservation_id, @total_amount, @payment_date, 0)"
                    Using cmd As New MySqlCommand(paymentQuery, conn, transaction)
                        cmd.Parameters.AddWithValue("@reservation_id", reservationId)
                        cmd.Parameters.AddWithValue("@total_amount", totalAmount)
                        cmd.Parameters.AddWithValue("@total_paid", paidAmount)
                        cmd.Parameters.AddWithValue("@payment_date", DateTime.Now)

                        ' Log the SQL command and parameters for debugging
                        Console.WriteLine("Executing SQL: " & cmd.CommandText)
                        For Each param As MySqlParameter In cmd.Parameters
                            Console.WriteLine($"Parameter: {param.ParameterName}, Value: {param.Value}")
                        Next

                        cmd.ExecuteNonQuery()
                    End Using

                    ' Insert into transaction table with Type_ID from location
                    Dim locationType As Integer = GetLocationTypeId(_selectedPlots(0).PlotId) ' Get the Type_ID from location based on selected PlotId
                    Dim transactionQuery As String = "INSERT INTO transaction (Date, Amount, Client_ID, Type_ID, Deceased_ID) " &
                                 "VALUES (@Date, @Amount, @Client_ID, @Type_ID, @Deceased_ID)"

                    Using cmd As New MySqlCommand(transactionQuery, conn, transaction)
                        cmd.Parameters.AddWithValue("@Date", DateTime.Now)
                        cmd.Parameters.AddWithValue("@Amount", paidAmount)
                        cmd.Parameters.AddWithValue("@Client_ID", clientId)
                        cmd.Parameters.AddWithValue("@Type_ID", locationType)
                        cmd.Parameters.AddWithValue("@Deceased_ID", deceasedId)
                        cmd.ExecuteNonQuery()
                    End Using

                    ' Insert plot reservations and update deceased if selected
                    Dim plotReservationQuery As String = "INSERT INTO plot_reservation (reservation_id, plot_id, level) VALUES (@reservation_id, @plot_id, @level)"
                    For Each plot In _selectedPlots
                        ' Insert plot reservation
                        Using cmd As New MySqlCommand(plotReservationQuery, conn, transaction)
                            cmd.Parameters.AddWithValue("@reservation_id", reservationId)
                            cmd.Parameters.AddWithValue("@plot_id", plot.PlotId) ' Use PlotId from selected plot
                            cmd.Parameters.AddWithValue("@level", If(packageType = 2 OrElse packageType = 4, 0, plot.Level))
                            cmd.ExecuteNonQuery()
                        End Using

                        ' Update deceased record if one is selected
                        If deceasedId > 0 Then
                            Dim deceasedQuery As String = "UPDATE deceased SET Plot_ID = @PlotId, Level = @Level WHERE Deceased_ID = @DeceasedId"
                            Using cmd As New MySqlCommand(deceasedQuery, conn, transaction)
                                cmd.Parameters.AddWithValue("@PlotId", plot.PlotId)
                                cmd.Parameters.AddWithValue("@Level", If(packageType = 2 OrElse packageType = 4, 0, plot.Level))
                                cmd.Parameters.AddWithValue("@DeceasedId", deceasedId)
                                cmd.ExecuteNonQuery()
                            End Using
                        End If
                    Next

                    transaction.Commit()
                    MessageBox.Show("Reservation and payment saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Me.Close()
                Catch ex As Exception
                    transaction.Rollback()
                    Throw
                End Try
            End Using
        Catch ex As Exception
            MessageBox.Show("Error adding Reservation and Payment: " & ex.Message & vbCrLf & "Stack Trace: " & ex.StackTrace)
        Finally
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
        End Try
    End Sub

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

    Private Sub BtnPrintReceipt_Click(sender As Object, e As EventArgs) Handles guna2Button1.Click
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
            For i As Integer = 0 To _selectedPlots.Count - 1
                receiptContent.AppendLine($"Plot {i + 1}: {_selectedPlots(i).LocationString}")
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
                _plotSelectionForm.Dispose()
                _plotSelectionForm = Nothing
            End If
        Catch ex As Exception
            ' Log error if needed
        End Try
    End Sub

    Private Sub ResetPlotSelections()
        _selectedPlots.Clear()
        _currentPlotCount = 0
        lblPlotLocation.Text = ""
        lblPlotLocation.Visible = False

        ' Clean up plot selection form
        If _plotSelectionForm IsNot Nothing Then
            RemoveHandler _plotSelectionForm.PlotSelected, AddressOf OnPlotSelected
            _plotSelectionForm.Dispose()
            _plotSelectionForm = Nothing
        End If
    End Sub

    Private Function GetDeceasedCount(plotId As Integer) As Integer
        Try
            Using cmd As New MySqlCommand("SELECT COUNT(*) FROM deceased WHERE Plot_ID = @plotId", conn)
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
            Using cmd As New MySqlCommand("SELECT COUNT(*) FROM deceased WHERE Plot_ID = @plotId AND Level = @level AND Level > 0", conn)
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
            Using cmd As New MySqlCommand("SELECT COUNT(*) FROM deceased WHERE Plot_ID = @plotId AND Level = @level AND Level > 0", conn)
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

                ' Show quantity only for family lawn lot
                If description = "family lawn lot" Then
                    lblQuantity.Visible = True
                    currentQuantity.Visible = True
                    currentQuantity.Value = 1
                Else
                    lblQuantity.Visible = False
                    currentQuantity.Visible = False
                    currentQuantity.Value = 1
                End If

                LoadPackageDetails(selectedPackageId)
                ResetPlotSelections()
                UpdateTotalAmount()
            Else
                txtGraveType.Text = "Grave Type:"
                txtPackage.Text = "Package:"
                txtPrice.Text = "Price:"
                txtTotal.Text = "Total:"
                lblQuantity.Visible = False
                currentQuantity.Visible = False
                currentQuantity.Value = 1
                ResetPlotSelections()
            End If
        Catch ex As Exception
            MessageBox.Show("Error loading package details: " & ex.Message)
        End Try
    End Sub

    Private Sub LoadPackageDetails(p_id As Integer)
        Try
            If conn.State = ConnectionState.Open Then conn.Close()

            Dim query As String = "SELECT * FROM package WHERE p_id = @p_id"
            Using cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@p_id", p_id)
                conn.Open()
                Using reader As MySqlDataReader = cmd.ExecuteReader()
                    If reader.Read() Then
                        Dim packagePrice As Decimal = Convert.ToDecimal(reader("price"))
                        Dim quantity As Integer = Convert.ToInt32(currentQuantity.Value)
                        Dim totalAmount As Decimal = packagePrice * quantity

                        txtGraveType.Text = $"Grave Type: {reader("description").ToString()}"
                        txtPackage.Text = $"Package Type: {reader("description")}"
                        txtPrice.Text = $"Price: ₱{packagePrice:N2}"
                        txtTotal.Text = $"₱{totalAmount:N2}"
                    End If
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error loading package details: " & ex.Message)
        Finally
            If conn.State = ConnectionState.Open Then conn.Close()
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

                txtTotal.Text = $"₱{totalAmount:N2}"
            End If
        Catch ex As Exception
            MessageBox.Show("Error updating total amount: " & ex.Message)
        End Try
    End Sub

    Private Function GetLocationTypeId(plotId As Integer) As Integer
        Try
            ' Correctly query for the location type using PlotId
            Dim query As String = "SELECT type FROM location WHERE id = @LocationId"
            Using cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@LocationId", plotId) ' Correctly pass the PlotId here
                conn.Open()
                Dim result As Object = cmd.ExecuteScalar()

                ' Ensure a result is returned
                If result IsNot Nothing AndAlso IsNumeric(result) Then
                    Return Convert.ToInt32(result)
                Else
                    Return 0 ' Default value if no valid Type_ID is found
                End If
            End Using
        Catch ex As Exception
            MessageBox.Show("Error fetching location type: " & ex.Message)
            Return 0 ' Return a default value in case of error
        Finally
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
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