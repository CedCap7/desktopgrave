Imports Microsoft.Web.WebView2.WinForms
Imports System.Runtime.InteropServices
Imports Newtonsoft.Json
Imports MySql.Data.MySqlClient
Imports Guna.UI2.WinForms
Imports System.Windows.Forms
Imports System.Linq
Imports System.Collections.Generic

Public Class frmPlotSelection
    Public Event PlotSelected(plotId As Integer, locationString As String, level As Integer)
    Private conn As New MySqlConnection("server=srv594.hstgr.io; database=u976878483_cemetery; username=u976878483_doncarlos; password=d0Nc4los; port=3306; Connection Timeout=30; Command Timeout=30;")
    Private selectedPlotType As String
    Private _selectedPlotId As Integer = -1
    Private _selectedLevel As Integer = 0
    Private _selectedLocation As String = ""
    Private _clientId As Integer = -1
    Private _remainingPlots As Integer = 0
    Private _parentForm As frmPlotPurchAndAssign = Nothing
    Private _selectedPlots As New Dictionary(Of Integer, Tuple(Of Integer, String))
    Private _layerName As String

    Public Sub CloseForm()
        Try
            Me.Hide()
            If Parent IsNot Nothing Then
                DirectCast(Parent, Panel).Controls.Clear()
            End If

            ' Clean up WebView2 resources
            If webViewPlotSelection IsNot Nothing Then
                RemoveHandler webViewPlotSelection.CoreWebView2.WebMessageReceived, AddressOf HandleWebMessage
                webViewPlotSelection.Dispose()
            End If

            ' Close database connection if open
            If conn IsNot Nothing AndAlso conn.State = ConnectionState.Open Then
                conn.Close()
            End If
        Catch ex As Exception
            Debug.WriteLine("Error during form close: " & ex.Message)
        End Try
    End Sub

    Public Sub New(layerName As String, Optional clientId As Integer = -1)
        InitializeComponent()
        _layerName = layerName
        selectedPlotType = layerName ' Set the selectedPlotType field
        _clientId = clientId
        _parentForm = DirectCast(Application.OpenForms("frmPlotPurchAndAssign"), frmPlotPurchAndAssign)
        Console.WriteLine($"frmPlotSelection initialized with layer: {layerName}, clientId: {clientId}") ' Debug log
    End Sub

    Private Async Sub frmPlotSelection_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            ' Initialize WebView2
            If webViewPlotSelection IsNot Nothing Then
                ' Remove any existing event handlers
                If webViewPlotSelection.CoreWebView2 IsNot Nothing Then
                    RemoveHandler webViewPlotSelection.CoreWebView2.WebMessageReceived, AddressOf HandleWebMessage
                End If

                ' Initialize WebView2
                Await webViewPlotSelection.EnsureCoreWebView2Async()

                ' Add message handler
                AddHandler webViewPlotSelection.CoreWebView2.WebMessageReceived, AddressOf HandleWebMessage

                ' Load the map after initialization
                LoadMap()
            Else
                MessageBox.Show("WebView2 control is not properly initialized.", "Initialization Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Catch ex As Exception
            MessageBox.Show("Error initializing WebView2: " & ex.Message)
        End Try
    End Sub

    Private Sub LoadMap()
        Try
            If webViewPlotSelection IsNot Nothing AndAlso webViewPlotSelection.CoreWebView2 IsNot Nothing Then
                ' Construct the URL with client ID for lawn lots and bone niches
                Dim url As String = $"https://libingan.test/map?type={_layerName}"
                If (_layerName = "lawnlots" OrElse _layerName = "boneniche") AndAlso _clientId > 0 Then
                    url &= $"&clientId={_clientId}"
                End If

                Console.WriteLine($"Loading map with URL: {url}") ' Debug log
                webViewPlotSelection.CoreWebView2.Navigate(url)
            End If
        Catch ex As Exception
            MessageBox.Show("Error loading map: " & ex.Message)
        End Try
    End Sub

    Private Sub HandleWebMessage(sender As Object, e As Microsoft.Web.WebView2.Core.CoreWebView2WebMessageReceivedEventArgs)
        Try
            Debug.WriteLine("Received web message") ' Debug log
            Dim message As String = e.WebMessageAsJson
            Debug.WriteLine($"Raw message: {message}") ' Debug log

            ' Clean up the incoming JSON string (remove escaped quotes and outer quotes)
            message = message.Replace("\""", """") ' Replace \" with "
            If message.StartsWith("""") AndAlso message.EndsWith("""") Then
                message = message.Substring(1, message.Length - 2)
            End If
            Debug.WriteLine($"Cleaned message: {message}") ' Debug log

            ' Deserialize the cleaned JSON message into a PlotData object
            Dim plotData As PlotData = Nothing
            Try
                plotData = Newtonsoft.Json.JsonConvert.DeserializeObject(Of PlotData)(message)
                Debug.WriteLine($"Deserialized plot data - ID: {plotData?.id}, Type: {plotData?.type}") ' Debug log
            Catch jsonEx As Exception
                Debug.WriteLine($"JSON deserialization error: {jsonEx.Message}") ' Debug log
                MessageBox.Show("Error processing plot data: Invalid format", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End Try

            ' Check if the deserialization was successful and the plot type is correct
            If plotData IsNot Nothing AndAlso IsCorrectPlotType(plotData.type) Then
                Debug.WriteLine($"Processing plot type: {plotData.type}") ' Debug log

                ' Check if plot is already selected before proceeding
                If _parentForm IsNot Nothing AndAlso _parentForm.SelectedPlots.ContainsKey(plotData.id) Then
                    MessageBox.Show("This plot has already been selected.", "Duplicate Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Return
                End If

                ' Now you can access the plot data properties directly
                Select Case plotData.type
                    Case 1 ' Apartment
                        HandleApartmentSelection(plotData)
                    Case 2 ' Family Lawn Lot
                        HandleLawnLotSelection(plotData)
                    Case 3 ' Bone Niche
                        HandleBoneNicheSelection(plotData)
                    Case 4 ' Private
                        HandlePrivateSelection(plotData)
                    Case Else
                        Debug.WriteLine($"Unknown plot type: {plotData.type}") ' Debug log
                        MessageBox.Show("Invalid plot type", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Select
            Else
                Debug.WriteLine($"Invalid plot data or incorrect plot type. Plot data: {If(plotData IsNot Nothing, plotData.type.ToString(), "null")}") ' Debug log
                MessageBox.Show("Invalid plot data or incorrect plot type", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If

        Catch ex As Exception
            Debug.WriteLine($"Error in HandleWebMessage: {ex.Message}") ' Debug log
            Debug.WriteLine($"Stack trace: {ex.StackTrace}") ' Debug log
            MessageBox.Show("Error processing plot selection: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Function IsCorrectPlotType(plotType As Integer) As Boolean
        If String.IsNullOrEmpty(selectedPlotType) Then
            Console.WriteLine("selectedPlotType is null or empty") ' Debug log
            Return False
        End If

        Dim expectedType As Integer

        Select Case selectedPlotType.ToLower()
            Case "apartment"
                expectedType = 1
            Case "lawnlots"
                expectedType = 2
            Case "boneniche"
                expectedType = 3
            Case "private"
                expectedType = 4
            Case Else
                Console.WriteLine($"Unknown plot type: {selectedPlotType}") ' Debug log
                expectedType = -1
        End Select

        Console.WriteLine($"Comparing plot type {plotType} with expected type {expectedType}") ' Debug log
        Return plotType = expectedType
    End Function

    Private Sub HandleApartmentSelection(plotData As PlotData)
        Try
            Using levelForm As New Form()
                levelForm.Text = "Select Apartment Level"
                levelForm.Size = New Size(300, 200)
                levelForm.StartPosition = FormStartPosition.CenterParent
                levelForm.FormBorderStyle = FormBorderStyle.FixedDialog
                levelForm.MaximizeBox = False
                levelForm.MinimizeBox = False

                Dim cmbLevel As New ComboBox()
                cmbLevel.DropDownStyle = ComboBoxStyle.DropDownList
                cmbLevel.Location = New Point(20, 20)
                cmbLevel.Size = New Size(240, 30)
                levelForm.Controls.Add(cmbLevel)

                ' Check available levels (1-4, one deceased per level)
                For level As Integer = 1 To 4
                    If Not IsLevelOccupied(plotData.id, level) Then
                        cmbLevel.Items.Add($"Level {level}")
                    End If
                Next

                If cmbLevel.Items.Count = 0 Then
                    MessageBox.Show("All levels in this apartment are occupied.", "Plot Full", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Return
                End If

                ShowLevelSelectionDialog(levelForm, cmbLevel, plotData)
            End Using
        Catch ex As Exception
            MessageBox.Show("Error handling apartment selection: " & ex.Message)
        End Try
    End Sub

    Private Sub HandleLawnLotSelection(plotData As PlotData)
        Try
            ' First check if plot is reserved and determine ownership status
            Using cmd As New MySqlCommand("
                SELECT 
                    pr.plot_id,
                    r.Reservation_Date,
                    p.Payment_Status,
                    p.total_Amount,
                    p.total_Paid,
                    r.Client_ID
                FROM plot_reservation pr 
                INNER JOIN reservation r ON pr.reservation_id = r.Reservation_ID 
                LEFT JOIN payment p ON r.Reservation_ID = p.Reservation_ID
                WHERE pr.plot_id = @plotId", conn)
                cmd.Parameters.AddWithValue("@plotId", plotData.id)
                If conn.State <> ConnectionState.Open Then conn.Open()

                Using reader As MySqlDataReader = cmd.ExecuteReader()
                    If reader.Read() Then
                        ' Plot is reserved
                        Dim reservationDate As DateTime = Convert.ToDateTime(reader("Reservation_Date"))
                        Dim paymentStatus As Integer = Convert.ToInt32(reader("Payment_Status"))
                        Dim totalAmount As Decimal = Convert.ToDecimal(reader("total_Amount"))
                        Dim totalPaid As Decimal = Convert.ToDecimal(reader("total_Paid"))
                        Dim reservedByClientId As Integer = Convert.ToInt32(reader("Client_ID"))

                        ' Check if plot is owned based on payment status or reservation date
                        Dim isOwned As Boolean = False

                        ' If fully paid (Payment_Status = 1), it's owned
                        If paymentStatus = 1 Then
                            isOwned = True
                            ' If not fully paid but 3 months have passed since reservation, it's owned
                        ElseIf paymentStatus = 0 Then
                            Dim threeMonthsLater As DateTime = reservationDate.AddMonths(3)
                            isOwned = DateTime.Now >= threeMonthsLater
                        End If

                        ' Check if the plot is reserved by the current client
                        If reservedByClientId = _clientId Then
                            MessageBox.Show("You have already reserved this plot.", "Plot Already Reserved", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Return
                        End If

                        If isOwned Then
                            MessageBox.Show("This plot is already owned.", "Plot Unavailable", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                            Return
                        Else
                            MessageBox.Show("This plot is already reserved.", "Plot Unavailable", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                            Return
                        End If
                    End If
                End Using
            End Using

            ' Then check if plot has deceased (permanent ownership)
            Using cmd As New MySqlCommand("SELECT COUNT(*) FROM deceased WHERE Plot_ID = @plotId", conn)
                cmd.Parameters.AddWithValue("@plotId", plotData.id)
                If conn.State <> ConnectionState.Open Then conn.Open()
                Dim hasDeceased As Boolean = Convert.ToInt32(cmd.ExecuteScalar()) > 0

                If hasDeceased Then
                    ' Check if it's owned by the selected client
                    Using cmd2 As New MySqlCommand("SELECT COUNT(*) FROM deceased WHERE Plot_ID = @plotId AND Client_ID = @clientId", conn)
                        cmd2.Parameters.AddWithValue("@plotId", plotData.id)
                        cmd2.Parameters.AddWithValue("@clientId", _clientId)
                        Dim isOwnedByClient As Boolean = Convert.ToInt32(cmd2.ExecuteScalar()) > 0

                        If isOwnedByClient Then
                            MessageBox.Show("You already own this plot.", "Plot Already Owned", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Else
                            MessageBox.Show("This plot is already owned by another client.", "Plot Unavailable", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        End If
                        Return
                    End Using
                End If

                ' If plot is not reserved, not owned, and has no deceased, proceed with selection
                Dim locationString As String = $"{GetLocationType(plotData.type)}, Block {plotData.block}, Section {plotData.section}, Row {plotData.row}, Plot {plotData.plot}"
                ValidateAndSelectPlot(plotData, locationString, 0) ' Level 0 for lawn lots

            End Using
        Catch ex As Exception
            MessageBox.Show("Error checking plot status: " & ex.Message)
        Finally
            If conn.State = ConnectionState.Open Then conn.Close()
        End Try
    End Sub

    Private Function EnsureConnection() As Boolean
        Try
            If conn Is Nothing Then
                conn = New MySqlConnection("server=srv594.hstgr.io; database=u976878483_cemetery; username=u976878483_doncarlos; password=d0Nc4los; port=3306; Connection Timeout=30; Command Timeout=30;")
            End If

            If conn.State <> ConnectionState.Open Then
                conn.Open()
            End If
            Return True
        Catch ex As Exception
            Debug.WriteLine($"Database connection error: {ex.Message}")
            MessageBox.Show("Unable to connect to the database. Please try again.", "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function

    Private Function IsLevelOccupied(plotId As Integer, level As Integer) As Boolean
        Try
            If Not EnsureConnection() Then Return True ' Assume occupied on connection error

            ' Check if the level is occupied by a deceased
            Using cmdDeceased As New MySqlCommand("SELECT COUNT(*) FROM deceased WHERE Plot_ID = @plotId AND Level = @level", conn)
                cmdDeceased.Parameters.AddWithValue("@plotId", plotId)
                cmdDeceased.Parameters.AddWithValue("@level", level)
                Dim deceasedCount As Integer = Convert.ToInt32(cmdDeceased.ExecuteScalar())
                If deceasedCount > 0 Then Return True ' Level is occupied by a deceased
            End Using

            ' Check if the level is reserved
            Using cmdReserved As New MySqlCommand("SELECT COUNT(*) FROM plot_reservation WHERE plot_id = @plotId AND level = @level", conn)
                cmdReserved.Parameters.AddWithValue("@plotId", plotId)
                cmdReserved.Parameters.AddWithValue("@level", level)
                Dim reservedCount As Integer = Convert.ToInt32(cmdReserved.ExecuteScalar())
                If reservedCount > 0 Then Return True ' Level is reserved
            End Using

            Return False ' Level is neither occupied nor reserved
        Catch ex As Exception
            Debug.WriteLine($"Error checking level occupancy: {ex.Message}")
            MessageBox.Show("Error checking level availability. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return True ' Assume occupied/reserved on error to prevent double booking
        End Try
    End Function

    Private Function GetLevelDeceasedCount(plotId As Integer, level As Integer) As Integer
        Try
            Using cmd As New MySqlCommand("SELECT COUNT(*) FROM deceased WHERE Plot_ID = @plotId AND Level = @level", conn)
                cmd.Parameters.AddWithValue("@plotId", plotId)
                cmd.Parameters.AddWithValue("@level", level)
                If conn.State <> ConnectionState.Open Then conn.Open()
                Return Convert.ToInt32(cmd.ExecuteScalar())
            End Using
        Catch ex As Exception
            MessageBox.Show("Error getting level count: " & ex.Message)
            Return 0
        Finally
            If conn.State = ConnectionState.Open Then conn.Close()
        End Try
    End Function

    Private Function GetLocationType(type As Integer) As String
        Select Case type
            Case 1
                Return "Apartment"
            Case 2
                Return "Family Lawn Lot"
            Case 3
                Return "Bone Niche"
            Case 4
                Return "Private"
            Case Else
                Return $"Type {type}"
        End Select
    End Function

    Private Sub HandleBoneNicheSelection(plotData As PlotData)
        Try
            If Not EnsureConnection() Then Return

            ' Show level selection dialog
            Using levelForm As New Form()
                levelForm.Text = "Select Bone Niche Level"
                levelForm.Size = New Size(300, 200)
                levelForm.StartPosition = FormStartPosition.CenterParent
                levelForm.FormBorderStyle = FormBorderStyle.FixedDialog
                levelForm.MaximizeBox = False
                levelForm.MinimizeBox = False

                Dim cmbLevel As New ComboBox()
                cmbLevel.DropDownStyle = ComboBoxStyle.DropDownList
                cmbLevel.Location = New Point(20, 20)
                cmbLevel.Size = New Size(240, 30)
                levelForm.Controls.Add(cmbLevel)

                ' Check available levels (1-3 for bone niche)
                For level As Integer = 1 To 3
                    If Not IsLevelOccupied(plotData.id, level) Then
                        cmbLevel.Items.Add($"Level {level}")
                    End If
                Next

                If cmbLevel.Items.Count = 0 Then
                    MessageBox.Show("All levels in this bone niche are occupied.", "Plot Full", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Return
                End If

                ShowLevelSelectionDialog(levelForm, cmbLevel, plotData)
            End Using
        Catch ex As Exception
            Debug.WriteLine($"Error in HandleBoneNicheSelection: {ex.Message}")
            MessageBox.Show("Error handling bone niche selection. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub HandlePrivateSelection(plotData As PlotData)
        Try
            Dim count = GetDeceasedCount(plotData.id)
            If count >= 4 Then
                MessageBox.Show("This private plot has reached its maximum capacity of 4 deceased.", "Plot Full", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            Dim locationString As String = $"{GetLocationType(plotData.type)}, Block {plotData.block}, Section {plotData.section}, Row {plotData.row}, Plot {plotData.plot}"
            Dim result = MessageBox.Show(
                $"Do you want to select this private plot? ({count}/4 occupied)" & vbCrLf & locationString,
                "Confirm Plot Selection",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question)

            If result = DialogResult.Yes Then
                ValidateAndSelectPlot(plotData, locationString, 0) ' Level 0 for private plots
                Me.Close()
            End If
        Catch ex As Exception
            MessageBox.Show("Error handling private plot selection: " & ex.Message)
        End Try
    End Sub

    Private Sub ShowLevelSelectionDialog(levelForm As Form, cmbLevel As ComboBox, plotData As PlotData)
        Dim btnOK As New Button()
        btnOK.Text = "OK"
        btnOK.DialogResult = DialogResult.OK
        btnOK.Location = New Point(100, 100)
        levelForm.Controls.Add(btnOK)

        If levelForm.ShowDialog() = DialogResult.OK AndAlso cmbLevel.SelectedItem IsNot Nothing Then
            Dim selectedLevel As Integer = Convert.ToInt32(cmbLevel.SelectedItem.ToString().Split(" "c)(1))
            Dim locationString As String = $"{GetLocationType(plotData.type)}, Block {plotData.block}, Section {plotData.section}, Row {plotData.row}, Plot {plotData.plot}, Level {selectedLevel}"

            ValidateAndSelectPlot(plotData, locationString, selectedLevel)
        End If
    End Sub

    Private Function GetDeceasedCount(plotId As Integer) As Integer
        Try
            Using cmd As New MySqlCommand("SELECT COUNT(*) FROM deceased WHERE Plot_ID = @plotId", conn)
                cmd.Parameters.AddWithValue("@plotId", plotId)
                If conn.State <> ConnectionState.Open Then conn.Open()
                Return Convert.ToInt32(cmd.ExecuteScalar())
            End Using
        Catch ex As Exception
            MessageBox.Show("Error getting deceased count: " & ex.Message)
            Return 0
        Finally
            If conn.State = ConnectionState.Open Then conn.Close()
        End Try
    End Function

    Private Sub ValidateAndSelectPlot(plotData As PlotData, locationString As String, selectedLevel As Integer)
        Try
            If Not EnsureConnection() Then Return

            ' Check if plot is reserved
            Using cmd As New MySqlCommand("
                SELECT 
                    pr.plot_id,
                    r.Reservation_Date,
                    p.Payment_Status,
                    r.Client_ID
                FROM plot_reservation pr 
                INNER JOIN reservation r ON pr.reservation_id = r.Reservation_ID 
                LEFT JOIN payment p ON r.Reservation_ID = p.Reservation_ID
                WHERE pr.plot_id = @plotId", conn)
                cmd.Parameters.AddWithValue("@plotId", plotData.id)

                Using reader As MySqlDataReader = cmd.ExecuteReader()
                    If reader.Read() Then
                        ' Plot is reserved
                        Dim reservationDate As DateTime = Convert.ToDateTime(reader("Reservation_Date"))
                        Dim paymentStatus As Integer = Convert.ToInt32(reader("Payment_Status"))
                        Dim reservedByClientId As Integer = Convert.ToInt32(reader("Client_ID"))

                        ' Check if plot is owned based on payment status or reservation date
                        Dim isOwned As Boolean = False

                        ' If fully paid (Payment_Status = 1), it's owned
                        If paymentStatus = 1 Then
                            isOwned = True
                        ' If not fully paid but 3 months have passed since reservation, it's owned
                        ElseIf paymentStatus = 0 Then
                            Dim threeMonthsLater As DateTime = reservationDate.AddMonths(3)
                            isOwned = DateTime.Now >= threeMonthsLater
                        End If

                        ' Check if the plot is reserved by the current client
                        If reservedByClientId = _clientId Then
                            MessageBox.Show("You have already reserved this plot.", "Plot Already Reserved", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Return
                        End If

                        If isOwned Then
                            MessageBox.Show("This plot is already owned.", "Plot Unavailable", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                            Return
                        Else
                            MessageBox.Show("This plot is already reserved.", "Plot Unavailable", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                            Return
                        End If
                    End If
                End Using
            End Using

            ' If this is not the first plot selection, validate adjacency
            If _parentForm IsNot Nothing AndAlso _parentForm.SelectedPlots.Count > 0 Then
                ' Check if the new plot is adjacent to any of the previously selected plots
                Dim isAdjacentToAny As Boolean = False
                Dim isInSameBlockSectionRow As Boolean = False

                For Each selectedPlot In _parentForm.SelectedPlots
                    ' Get the selected plot's details
                    Using cmd3 As New MySqlCommand("SELECT block, section, row, plot FROM location WHERE id = @plotId", conn)
                        cmd3.Parameters.AddWithValue("@plotId", selectedPlot.Key)
                        Using reader As MySqlDataReader = cmd3.ExecuteReader()
                            If reader.Read() Then
                                ' Check if plots are in the same block, section, and row
                                If reader("block").ToString() = plotData.block AndAlso
                                   reader("section").ToString() = plotData.section AndAlso
                                   reader("row").ToString() = plotData.row Then
                                    isInSameBlockSectionRow = True

                                    ' Check if plots are adjacent
                                    Dim selectedPlotNumber As Integer = Convert.ToInt32(reader("plot"))
                                    Dim currentPlotNumber As Integer = Convert.ToInt32(plotData.plot)

                                    If Math.Abs(selectedPlotNumber - currentPlotNumber) = 1 Then
                                        isAdjacentToAny = True
                                        Exit For ' Found an adjacent plot, no need to check others
                                    End If
                                End If
                            End If
                        End Using
                    End Using
                Next

                ' If not in the same block/section/row, show error
                If Not isInSameBlockSectionRow Then
                    MessageBox.Show("Selected plot must be in the same block, section, and row as the previous selections.", "Invalid Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Return
                End If

                ' If not adjacent to any selected plot, show error
                If Not isAdjacentToAny Then
                    MessageBox.Show("Selected plot must be adjacent to one of the previously selected plots.", "Invalid Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Return
                End If
            End If

            ' Raise the event to notify parent form
            RaiseEvent PlotSelected(plotData.id, locationString, selectedLevel)

        Catch ex As Exception
            Debug.WriteLine($"Error in ValidateAndSelectPlot: {ex.Message}")
            MessageBox.Show("Error in plot selection. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub frmPlotSelection_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Try
            ' Clean up WebView2 resources
            If webViewPlotSelection IsNot Nothing Then
                RemoveHandler webViewPlotSelection.CoreWebView2.WebMessageReceived, AddressOf HandleWebMessage
            End If

            ' Close database connection if open
            If conn IsNot Nothing AndAlso conn.State = ConnectionState.Open Then
                conn.Close()
            End If
        Catch ex As Exception
            Debug.WriteLine("Error during form closing: " & ex.Message)
        End Try
    End Sub
End Class

Public Class PlotData
    Public Property id As Integer
    Public Property type As Integer
    Public Property block As String
    Public Property section As String
    Public Property row As String
    Public Property plot As String
    Public Property level As Integer
End Class