Imports Guna.UI2.WinForms
Imports MySql.Data.MySqlClient
Imports Newtonsoft.Json
Imports System.Globalization  ' Add this for CultureInfo and NumberStyles

Public Class frmPlotReservation
    Private _plotSelectionForm As frmPlotSelection = Nothing
    Private conn As New MySqlConnection("server=localhost; user=root; password=root; database=dccms")
    Private _selectedPlots As New List(Of PlotSelection)
    Private _currentPlotCount As Integer = 0

    Public ClientID As Integer
    Public ClientFirstName As String
    Public ClientMobile As String
    Public ClientEmail As String
    Public ClientGender As String

    Private mainDashboard As dashboard ' Reference to the main dashboard


    Public Class ClientItem
        Public Property ClientID As Integer
        Public Property FullName As String

        ' Override the ToString method to display FullName in the ListBox
        Public Overrides Function ToString() As String
            Return FullName
        End Function
    End Class

    ' Constructor to pass the main dashboard form
    Public Sub New(dashboardForm As dashboard)
        InitializeComponent()
        mainDashboard = dashboardForm
    End Sub

    ' Create a class to store plot information
    Private Class PlotSelection
        Public Property PlotId As Integer
        Public Property Level As Integer
        Public Property LocationString As String
    End Class

    ' Event handler to filter client suggestions based on user input in txtClientSearch
    Private Sub txtClientSearch_TextChanged(sender As Object, e As EventArgs) Handles txtClientSearch.TextChanged
        Try
            If String.IsNullOrEmpty(txtClientSearch.Text) Then
                lstClientSuggestions.Visible = False
                Return
            End If

            ' Set the query to search for clients based on the text in txtClientSearch
            Dim query As String = "SELECT Client_ID, CONCAT(FirstName, ' ', LastName) AS FullName FROM client " &
                              "WHERE CONCAT(FirstName, ' ', LastName) LIKE @searchText"
            Using cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@searchText", "%" & txtClientSearch.Text & "%")
                conn.Open()
                Dim adapter As New MySqlDataAdapter(cmd)
                Dim dt As New DataTable()
                adapter.Fill(dt)

                ' Clear any previous items in the listbox
                lstClientSuggestions.Items.Clear()

                ' Add new client suggestions to the listbox
                For Each row As DataRow In dt.Rows
                    ' Create a ClientItem object and set FullName and ClientID
                    Dim client As New ClientItem With {
                    .ClientID = Convert.ToInt32(row("Client_ID")),
                    .FullName = row("FullName").ToString()
                }

                    ' Add the ClientItem to the ListBox
                    lstClientSuggestions.Items.Add(client)
                Next

                ' Show the listbox only if there are results
                lstClientSuggestions.Visible = dt.Rows.Count > 0
            End Using
        Catch ex As Exception
            MessageBox.Show("Error loading client suggestions: " & ex.Message)
        Finally
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
        End Try
    End Sub

    Private Sub lstClientSuggestions_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstClientSuggestions.SelectedIndexChanged
        If lstClientSuggestions.SelectedIndex <> -1 Then
            ' Retrieve the selected ClientItem object from the ListBox
            Dim selectedClient As ClientItem = CType(lstClientSuggestions.SelectedItem, ClientItem)

            ' Assign the Client_ID to the ClientID variable
            ClientID = selectedClient.ClientID

            ' Set the textbox value to the selected full name
            txtClientSearch.Text = selectedClient.FullName

            ' Hide the suggestion list after selection
            lstClientSuggestions.Visible = False
        End If
    End Sub


    Private Sub frmPaymentReserv_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadPackages()

        ' Initialize quantity controls
        lblQuantity.Visible = False
        currentQuantity.Visible = False
        currentQuantity.Value = 1
        currentQuantity.Minimum = 1
        currentQuantity.Maximum = 100

        ' Initialize paid amount controls
        txtPaidAmount.Text = ""
        lblPaidAmount.Text = "₱0.00"
    End Sub

    Private Sub txtPaidAmount_TextChanged(sender As Object, e As EventArgs) Handles txtPaidAmount.TextChanged
        Try
            ' Remove existing commas and non-numeric characters
            Dim cleanText = txtPaidAmount.Text.Replace(",", "").Trim()

            ' If the text is empty, clear the label and exit
            If String.IsNullOrEmpty(cleanText) Then
                lblPaidAmount.Text = "₱0.00"
                Return
            End If

            ' Try to parse the clean text as decimal
            If Decimal.TryParse(cleanText, NumberStyles.Number, CultureInfo.InvariantCulture, result:=Decimal.Zero) Then
                Dim amount As Decimal = Decimal.Parse(cleanText)
                ' Format with commas and two decimal places
                txtPaidAmount.Text = Format(amount, "#,##0")
                lblPaidAmount.Text = $"₱{amount:N2}"

                ' Move cursor to end of text
                txtPaidAmount.SelectionStart = txtPaidAmount.Text.Length
            End If
        Catch ex As Exception
            MessageBox.Show("Please enter a valid amount", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtPaidAmount.Text = ""
            lblPaidAmount.Text = "₱0.00"
        End Try
    End Sub

    Private Sub GraveType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles GraveType.SelectedIndexChanged
        Try
            If GraveType.SelectedValue IsNot Nothing AndAlso
               TypeOf GraveType.SelectedValue Is Integer AndAlso
               Convert.ToInt32(GraveType.SelectedValue) <> 0 Then

                Dim selectedPackageId As Integer = Convert.ToInt32(DirectCast(GraveType.SelectedItem, DataRowView)("p_id"))
                LoadPackageDetails(selectedPackageId)

                ' Handle quantity visibility based on package type
                Dim description As String = DirectCast(GraveType.SelectedItem, DataRowView)("description").ToString().Trim().ToLower()

                If description = "family lawn lot" Then
                    lblQuantity.Visible = True
                    currentQuantity.Visible = True
                    currentQuantity.Value = 1
                Else
                    lblQuantity.Visible = False
                    currentQuantity.Visible = False
                    currentQuantity.Value = 1
                End If

                ' Reset plot selections when package type changes
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

    Private Sub currentQuantity_ValueChanged(sender As Object, e As EventArgs) Handles currentQuantity.ValueChanged
        UpdateTotalAmount()
        ResetPlotSelections()
        If currentQuantity.Value > 1 Then
            MessageBox.Show($"Please select {currentQuantity.Value} plots", "Plot Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
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

                txtTotal.Text = $" ₱{totalAmount:N2}"
            End If
        Catch ex As Exception
            MessageBox.Show("Error updating total amount: " & ex.Message)
        End Try
    End Sub

    Private Sub LoadPackages()
        Try
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If

            Dim query As String = "SELECT p_id, package_name, description, price FROM package"

            Using cmd As New MySqlCommand(query, conn)
                conn.Open()
                Dim adapter As New MySqlDataAdapter(cmd)
                Dim dt As New DataTable()
                adapter.Fill(dt)

                ' Add default row
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
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
        End Try
    End Sub

    Private Sub LoadPackageDetails(p_id As Integer)
        Try
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If

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
            conn.Close()
        End Try
    End Sub

    Private Sub btnSelectPlot_Click(sender As Object, e As EventArgs) Handles btnSelectPlot.Click
        If GraveType.SelectedValue Is Nothing OrElse
           (TypeOf GraveType.SelectedValue Is Integer AndAlso Convert.ToInt32(GraveType.SelectedValue) = 0) Then
            MessageBox.Show("Please select a package first", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If _currentPlotCount >= currentQuantity.Value Then
            MessageBox.Show("You have already selected the maximum number of plots.", "Maximum Plots Selected", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        ' Get the selected package type
        Dim selectedPackageType As String = ""
        Select Case DirectCast(GraveType.SelectedItem, DataRowView)("description").ToString().Trim().ToLower()
            Case "private"
                selectedPackageType = "private"
            Case "family lawn lot"
                selectedPackageType = "lawnlots"
            Case "bone niche"
                selectedPackageType = "boneniche"
            Case "apartment"
                selectedPackageType = "apartment"
            Case Else
                MessageBox.Show("Invalid grave type selected", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
        End Select

        ' Dispose of previous form if it exists
        If _plotSelectionForm IsNot Nothing Then
            RemoveHandler _plotSelectionForm.PlotSelected, AddressOf OnPlotSelected
            _plotSelectionForm.Dispose()
            _plotSelectionForm = Nothing
        End If

        ' Create new form instance
        _plotSelectionForm = New frmPlotSelection(selectedPackageType)
        AddHandler _plotSelectionForm.PlotSelected, AddressOf OnPlotSelected

        ' Configure the form to be displayed in the panel
        With _plotSelectionForm
            .TopLevel = False
            .FormBorderStyle = FormBorderStyle.None
            .Dock = DockStyle.Fill
        End With

        ' Clear the panel and add the new form
        subSidePanel.Controls.Clear()
        subSidePanel.Controls.Add(_plotSelectionForm)
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

        ' Check if client is selected
        If ClientID = 0 Then
            Dim result = MessageBox.Show("No client is selected. Do you want to proceed with the reservation without a client?",
                       "Confirm Reservation",
                       MessageBoxButtons.YesNo,
                       MessageBoxIcon.Question)
            If result = DialogResult.No Then
                Return
            End If
        End If

        ' Declare necessary variables
        Dim totalAmount As Decimal = 0
        Dim paidAmount As Decimal = 0
        Dim paymentStatus As Integer = 0

        Try
            conn.Open()
            Using transaction As MySqlTransaction = conn.BeginTransaction()
                Try
                    ' Get the package price from the package table (using the selected package from GraveType)
                    Dim selectedPackageId As Integer = Convert.ToInt32(DirectCast(GraveType.SelectedItem, DataRowView)("p_id"))
                    Dim packagePrice As Decimal = 0

                    ' Retrieve the price for the selected package
                    Dim query As String = "SELECT price FROM package WHERE p_id = @p_id"
                    Using cmd As New MySqlCommand(query, conn, transaction)
                        cmd.Parameters.AddWithValue("@p_id", selectedPackageId)
                        packagePrice = Convert.ToDecimal(cmd.ExecuteScalar())
                    End Using

                    ' Calculate the total amount based on quantity and package price
                    totalAmount = packagePrice * currentQuantity.Value

                    ' Get the paid amount from the input (txtPaidAmount)
                    If Not String.IsNullOrEmpty(txtPaidAmount.Text) Then
                        paidAmount = Decimal.Parse(txtPaidAmount.Text.Replace(",", ""))
                    End If

                    ' Determine payment status
                    paymentStatus = If(paidAmount >= totalAmount, 1, 0)

                    ' Insert reservation first to get the Reservation_ID
                    Dim reservationQuery As String = "INSERT INTO reservation (Client_ID, p_id, Reservation_Date, Status, Quantity) " &
                                                   "VALUES (@Client_ID, @p_id, @Reservation_Date, '0', @currentQuantity); " &
                                                   "SELECT LAST_INSERT_ID();"

                    Dim reservationId As Integer
                    Using cmd As New MySqlCommand(reservationQuery, conn, transaction)
                        cmd.Parameters.AddWithValue("@Client_ID", ClientID)
                        ' Insert the PlotId from the selected plot as p_id in the reservation table
                        cmd.Parameters.AddWithValue("@p_id", _selectedPlots(0).PlotId)  ' Use the first selected plot's PlotId
                        cmd.Parameters.AddWithValue("@Reservation_Date", DateTime.Now)
                        cmd.Parameters.AddWithValue("@currentQuantity", currentQuantity.Value)
                        reservationId = Convert.ToInt32(cmd.ExecuteScalar())
                    End Using

                    ' Now insert into the payment table with the Reservation_ID and Client_ID
                    Dim paymentQuery As String = "INSERT INTO payment (Reservation_ID, Client_ID, total_Amount, total_Paid, Payment_Status) " &
                                              "VALUES (@Reservation_ID, @Client_ID, @total_Amount, @total_Paid, @Payment_Status);"

                    Using cmdPayment As New MySqlCommand(paymentQuery, conn, transaction)
                        cmdPayment.Parameters.AddWithValue("@Reservation_ID", reservationId)
                        cmdPayment.Parameters.AddWithValue("@Client_ID", ClientID)
                        cmdPayment.Parameters.AddWithValue("@total_Amount", totalAmount)
                        cmdPayment.Parameters.AddWithValue("@total_Paid", paidAmount)
                        cmdPayment.Parameters.AddWithValue("@Payment_Status", paymentStatus)
                        cmdPayment.ExecuteNonQuery()
                    End Using

                    ' Insert plot reservations
                    Dim plotReservationQuery As String = "INSERT INTO plot_reservation (reservation_id, plot_id, level) " &
                                                    "VALUES (@reservation_id, @plot_id, @level)"

                    For Each plot In _selectedPlots
                        ' Insert plot reservation
                        Using cmd As New MySqlCommand(plotReservationQuery, conn, transaction)
                            cmd.Parameters.AddWithValue("@reservation_id", reservationId)
                            cmd.Parameters.AddWithValue("@plot_id", plot.PlotId)  ' Use Plot_ID from selected plot
                            Dim plotLevel As Integer = If(NeedsLevel(selectedPackageId), plot.Level, 0)
                            cmd.Parameters.AddWithValue("@level", plotLevel)
                            cmd.ExecuteNonQuery()
                        End Using
                    Next

                    transaction.Commit()
                    MessageBox.Show("Reservation saved successfully!" & vbCrLf &
                          $"Total Amount: ₱{totalAmount:N2}" & vbCrLf &
                          $"Amount Paid: ₱{paidAmount:N2}" & vbCrLf &
                          $"Payment Status: {If(paymentStatus = 1, "Paid", "Partial")}",
                          "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

                    Me.Close()

                Catch ex As Exception
                    transaction.Rollback()
                    Throw
                End Try
            End Using
        Catch ex As Exception
            MessageBox.Show("Error adding Reservation: " & ex.Message & vbCrLf & "Stack Trace: " & ex.StackTrace)
        Finally
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
        End Try
    End Sub


    ' Helper function to determine if a plot type needs levels
    Private Function NeedsLevel(packageType As Integer) As Boolean
        Return packageType = 1 OrElse packageType = 3 ' Apartment or Bone Niche
    End Function

    Private Function ValidateSelections() As Boolean
        ' Check if a package is selected
        If GraveType.SelectedValue Is Nothing OrElse Convert.ToInt32(GraveType.SelectedValue) = 0 Then
            MessageBox.Show("Please select a package first", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End If

        ' Check if a client is selected by verifying the ClientID
        If ClientID = 0 Then
            MessageBox.Show("Please select a client or choose 'No Client'", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End If

        Return True
    End Function


    Private Sub BtnPrintReceipt_Click(sender As Object, e As EventArgs) Handles BtnPrintReceipt.Click
        Try
            Dim receiptContent As New System.Text.StringBuilder()
            receiptContent.AppendLine("=== RESERVATION RECEIPT ===")
            receiptContent.AppendLine($"Date: {DateTime.Now}")
            receiptContent.AppendLine($"Client: {txtClientSearch.Text}")
            receiptContent.AppendLine($"{txtPackage.Text}")
            receiptContent.AppendLine($"Quantity: {currentQuantity.Value}")
            receiptContent.AppendLine($"{txtPrice.Text}")
            receiptContent.AppendLine($"Total Amount: {txtTotal.Text}")
            receiptContent.AppendLine($"Amount Paid: {lblPaidAmount.Text}")
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
End Class