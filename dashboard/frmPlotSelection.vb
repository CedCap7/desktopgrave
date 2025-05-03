Imports Microsoft.Web.WebView2.WinForms
Imports System.Runtime.InteropServices
Imports Newtonsoft.Json
Imports MySql.Data.MySqlClient

Public Class frmPlotSelection
    Public Event PlotSelected(plotId As Integer, locationString As String, level As Integer)
    Private conn As New MySqlConnection("server=localhost; user=root; password=root; database=dccms")
    Private selectedPlotType As String

    Public Sub CloseForm()
        Me.Hide()
        If Parent IsNot Nothing Then
            DirectCast(Parent, Panel).Controls.Clear()
        End If
    End Sub

    Public Sub New(plotType As String)
        InitializeComponent()
        selectedPlotType = plotType
    End Sub

    Private Async Sub frmPlotSelection_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Await InitializeAsync()
        Catch ex As Exception
            MessageBox.Show("Error initializing WebView2: " & ex.Message)
        End Try
    End Sub

    Private Async Function InitializeAsync() As Task
        Try
            Await webViewPlotSelection.EnsureCoreWebView2Async()
            AddHandler webViewPlotSelection.CoreWebView2.WebMessageReceived, AddressOf HandleWebMessage
            webViewPlotSelection.CoreWebView2.Navigate($"https://doncarloscemetery.io/map?type={selectedPlotType}")
        Catch ex As Exception
            MessageBox.Show("Error initializing WebView2: " & ex.Message)
        End Try
    End Function

    Private Sub HandleWebMessage(sender As Object, e As Microsoft.Web.WebView2.Core.CoreWebView2WebMessageReceivedEventArgs)
        Try
            Dim plotDataJson As String = e.WebMessageAsJson
            plotDataJson = plotDataJson.Replace("\""", """").Trim(""""c)
            Dim plotData = JsonConvert.DeserializeObject(Of PlotData)(plotDataJson)

            If Not IsCorrectPlotType(plotData.type) Then
                Return
            End If

            Select Case plotData.type
                Case 1 ' Apartment
                    HandleApartmentSelection(plotData)
                Case 2 ' Family Lawn Lot
                    HandleLawnLotSelection(plotData)
                Case 3 ' Bone Niche
                    HandleBoneNicheSelection(plotData)
                Case 4 ' Private
                    HandlePrivateSelection(plotData)
            End Select

        Catch ex As Exception
            MessageBox.Show("Error processing plot data: " & ex.Message)
        End Try
    End Sub

    Private Function IsCorrectPlotType(plotType As Integer) As Boolean
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
                expectedType = -1
        End Select

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
        ' Family Lawn Lots have unlimited capacity
        Dim locationString As String = $"{GetLocationType(plotData.type)}, Block {plotData.block}, Section {plotData.section}, Row {plotData.row}, Plot {plotData.plot}"

        Dim result = MessageBox.Show(
            "Do you want to select this lawn lot?" & vbCrLf & locationString,
            "Confirm Plot Selection",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question)

        If result = DialogResult.Yes Then
            RaiseEvent PlotSelected(plotData.id, locationString, 0) ' Level 0 for lawn lots
            Me.Close()
        End If
    End Sub

    Private Sub HandleBoneNicheSelection(plotData As PlotData)
        Try
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

                ' Check available levels (1-3, max 10 deceased per level)
                For level As Integer = 1 To 3
                    Dim count = GetLevelDeceasedCount(plotData.id, level)
                    If count < 10 Then
                        cmbLevel.Items.Add($"Level {level} ({count}/10 occupied)")
                    End If
                Next

                If cmbLevel.Items.Count = 0 Then
                    MessageBox.Show("All levels in this bone niche are full.", "Plot Full", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Return
                End If

                ShowLevelSelectionDialog(levelForm, cmbLevel, plotData)
            End Using
        Catch ex As Exception
            MessageBox.Show("Error handling bone niche selection: " & ex.Message)
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
                RaiseEvent PlotSelected(plotData.id, locationString, 0) ' Level 0 for private plots
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

            RaiseEvent PlotSelected(plotData.id, locationString, selectedLevel)
            Me.Close()
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

    Private Function IsLevelOccupied(plotId As Integer, level As Integer) As Boolean
        Try
            Using cmd As New MySqlCommand("SELECT COUNT(*) FROM deceased WHERE Plot_ID = @plotId AND Level = @level", conn)
                cmd.Parameters.AddWithValue("@plotId", plotId)
                cmd.Parameters.AddWithValue("@level", level)
                If conn.State <> ConnectionState.Open Then conn.Open()
                Return Convert.ToInt32(cmd.ExecuteScalar()) > 0
            End Using
        Catch ex As Exception
            MessageBox.Show("Error checking level occupancy: " & ex.Message)
            Return True
        Finally
            If conn.State = ConnectionState.Open Then conn.Close()
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