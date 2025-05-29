Imports MySql.Data.MySqlClient
Imports System.Data

Public Class frmDeceasedAssign
    Private ClientID As Integer
    Private SelectedPlotID As Integer
    Private SelectedPlotLevel As Integer
    Private SelectedPlotType As Integer ' Store the numeric type
    Private SelectedPlotBlock As String
    Private SelectedPlotSection As String
    Private SelectedPlotRow As String
    Private SelectedPlotPlot As String

    ' Constructor to initialize with client ID and selected plot details
    Public Sub New(clientId As Integer, plotId As Integer, plotLevel As Integer, plotType As Integer, plotBlock As String, plotSection As String, plotRow As String, plotPlot As String)
        InitializeComponent()
        Me.ClientID = clientId
        Me.SelectedPlotID = plotId
        Me.SelectedPlotLevel = plotLevel
        Me.SelectedPlotType = plotType
        Me.SelectedPlotBlock = plotBlock
        Me.SelectedPlotSection = plotSection
        Me.SelectedPlotRow = plotRow
        Me.SelectedPlotPlot = plotPlot

        ' Assuming Module1.dbconn() is your database connection setup
        Module1.dbconn()
    End Sub

    Private Sub frmDeceasedAssign_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Display the selected plot details
        DisplaySelectedPlotDetails()

        ' Display the occupancy status of the selected plot
        DisplayOccupancy()

        ' Load the list of deceased for the client
        LoadDeceasedList()
    End Sub

    ' Method to load all deceased of the client into cmbDeceased
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
            dt.Rows.Add(-1, "-- Select Deceased --")

            ' Get all deceased associated with the client
            Dim sql As String = "SELECT Deceased_ID, " &
                          "CONCAT(COALESCE(FirstName, ''), " &
                          "IF(MiddleName IS NULL, '', CONCAT(' ', LEFT(MiddleName, 1), '.')), " &
                          "IF(LastName IS NULL, '', CONCAT(' ', LastName))) AS FullName " &
                          "FROM deceased " &
                          "WHERE Client_ID = @ClientID " &
                          "AND TRIM(CONCAT(COALESCE(FirstName, ''), " &
                          "COALESCE(MiddleName, ''), " &
                          "COALESCE(LastName, ''))) != '' " &
                          "ORDER BY LastName, FirstName"

            Using cmd As New MySqlCommand(sql, Module1.cn)
                cmd.Parameters.AddWithValue("@ClientID", ClientID)
                Using dr As MySqlDataReader = cmd.ExecuteReader()
                    While dr.Read()
                        Dim deceasedId As Integer = dr.GetInt32(dr.GetOrdinal("Deceased_ID"))
                        Dim fullName As String = dr("FullName").ToString().Trim()
                        If Not String.IsNullOrEmpty(fullName) Then
                            dt.Rows.Add(deceasedId, fullName)
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
            If Module1.cn IsNot Nothing AndAlso Module1.cn.State = ConnectionState.Open Then
                Module1.cn.Close()
            End If
        End Try
    End Sub

    ' Method to display the selected plot details in LblPlot
    Private Sub DisplaySelectedPlotDetails()
        Dim plotTypeString As String
        Select Case SelectedPlotType
            Case 1
                plotTypeString = "Apartment"
            Case 2
                plotTypeString = "Family Lawn Lots"
            Case 3
                plotTypeString = "Bone Niche"
            Case 4
                plotTypeString = "Private"
            Case Else
                plotTypeString = "Unknown Type"
        End Select

        Dim plotLocation As String = $"{plotTypeString} - Block {SelectedPlotBlock}, Section {SelectedPlotSection}, Row {SelectedPlotRow}, Plot {SelectedPlotPlot}"

        ' Add level information if applicable
        If SelectedPlotType = 1 OrElse SelectedPlotType = 3 Then
            plotLocation &= $", Level {SelectedPlotLevel}"
        End If

        LblPlot.Text = $"{plotLocation}"
    End Sub

    ' Method to display the occupancy status of the selected plot
    Private Sub DisplayOccupancy()
        Try
            If Module1.cn.State = ConnectionState.Open Then
                Module1.cn.Close()
            End If
            Module1.cn.Open()

            Dim deceasedCount As Integer = 0
            Dim sql As String

            ' Query to count deceased based on plot type and level
            If SelectedPlotType = 1 OrElse SelectedPlotType = 3 Then ' Apartment or Bone Niche (consider level)
                sql = "SELECT COUNT(*) FROM deceased WHERE Plot_ID = @PlotID AND Level = @Level"
            Else ' Family Lawn Lots or Private (only consider Plot_ID)
                sql = "SELECT COUNT(*) FROM deceased WHERE Plot_ID = @PlotID"
            End If

            Using cmd As New MySqlCommand(sql, Module1.cn)
                cmd.Parameters.AddWithValue("@PlotID", SelectedPlotID)
                If SelectedPlotType = 1 OrElse SelectedPlotType = 3 Then
                    cmd.Parameters.AddWithValue("@Level", SelectedPlotLevel)
                End If

                deceasedCount = Convert.ToInt32(cmd.ExecuteScalar())
            End Using

            ' Determine and display the occupancy status
            Dim occupancyText As String
            Select Case SelectedPlotType
                Case 1 ' Apartment (max 1 per level based on previous info)
                    If deceasedCount = 0 Then
                        occupancyText = "Not yet occupied"
                    Else
                        occupancyText = $"Occupied: {deceasedCount}/1"
                    End If
                Case 3 ' Bone Niche (max 10 per level)
                    If deceasedCount = 0 Then
                        occupancyText = "Not yet occupied"
                    ElseIf deceasedCount < 10 Then
                        occupancyText = $"Occupied: {deceasedCount}/10"
                    Else
                        occupancyText = "Full"
                    End If
                Case 4 ' Private (max 4)
                    If deceasedCount = 0 Then
                        occupancyText = "Not yet occupied"
                    ElseIf deceasedCount < 4 Then
                        occupancyText = $"Occupied: {deceasedCount}/4"
                    Else
                        occupancyText = "Full"
                    End If
                Case Else ' Other types (Family Lawn Lots or Unknown)
                    If deceasedCount = 0 Then
                        occupancyText = "Not yet occupied"
                    Else
                        occupancyText = $"Occupied: {deceasedCount}"
                    End If
            End Select

            LblOccupied.Text = occupancyText

        Catch ex As Exception
            MessageBox.Show("Error loading occupancy details: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Module1.cn IsNot Nothing AndAlso Module1.cn.State = ConnectionState.Open Then
                Module1.cn.Close()
            End If
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

        Dim selectedDeceasedID As Integer = Convert.ToInt32(cmbDeceased.SelectedValue)

        ' Determine the level to assign (0 if not Apartment or Bone Niche)
        Dim levelToAssign As Integer = 0
        If SelectedPlotType = 1 OrElse SelectedPlotType = 3 Then
            levelToAssign = SelectedPlotLevel
        End If

        Try
            If Module1.cn.State = ConnectionState.Open Then
                Module1.cn.Close()
            End If
            Module1.cn.Open()

            ' Update deceased record with Plot_ID and Level
            Dim sql As String = "UPDATE deceased SET Plot_ID = @PlotID, Level = @Level WHERE Deceased_ID = @DeceasedID"

            Using cmd As New MySqlCommand(sql, Module1.cn)
                cmd.Parameters.AddWithValue("@PlotID", SelectedPlotID)
                cmd.Parameters.AddWithValue("@Level", levelToAssign)
                cmd.Parameters.AddWithValue("@DeceasedID", selectedDeceasedID)
                cmd.ExecuteNonQuery()
            End Using

            MessageBox.Show("Deceased successfully assigned to plot!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

            ' Close the form after successful assignment
            Me.Close()

        Catch ex As Exception
            MessageBox.Show("Error assigning deceased to plot: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Module1.cn IsNot Nothing AndAlso Module1.cn.State = ConnectionState.Open Then
                Module1.cn.Close()
            End If
        End Try
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        ' Implementation for canceling
        Try
            Me.Close()
        Catch ex As Exception
            MessageBox.Show("Error closing form: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' Add other event handlers as needed
End Class