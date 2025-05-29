Imports MySql.Data.MySqlClient
Imports System.Data

Public Class frmDeceasedTransfer
    Private ClientID As Integer
    Private DeceasedID As Integer
    Private CurrentPlotID As Integer
    Private CurrentPlotLevel As Integer

    ' Constructor
    Public Sub New(clientId As Integer, deceasedId As Integer)
        InitializeComponent()
        Me.ClientID = clientId
        Me.DeceasedID = deceasedId

        ' Assuming Module1.dbconn() is your database connection setup
        Module1.dbconn()
    End Sub

    Private Sub frmDeceasedTransfer_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Load deceased details
        LoadDeceasedDetails()

        ' Load current plot details
        LoadCurrentPlotDetails()

        ' Load available plots for transfer
        LoadAvailablePlots()
    End Sub

    ' Method to load deceased person's name
    Private Sub LoadDeceasedDetails()
        Try
            If Module1.cn.State = ConnectionState.Open Then
                Module1.cn.Close()
            End If
            Module1.cn.Open()

            Dim sql As String = "SELECT CONCAT(COALESCE(FirstName, ''), " &
                          "IF(MiddleName IS NULL, '', CONCAT(' ', LEFT(MiddleName, 1), '.')), " &
                          "IF(LastName IS NULL, '', CONCAT(' ', LastName))) AS FullName " &
                          "FROM deceased " &
                          "WHERE Deceased_ID = @DeceasedID"

            Using cmd As New MySqlCommand(sql, Module1.cn)
                cmd.Parameters.AddWithValue("@DeceasedID", DeceasedID)
                Dim deceasedName As Object = cmd.ExecuteScalar()

                If deceasedName IsNot DBNull.Value AndAlso deceasedName IsNot Nothing Then
                    LblDeceased.Text = $"Deceased: {deceasedName.ToString().Trim()}"
                Else
                    LblDeceased.Text = "Deceased: Not Found"
                End If
            End Using

        Catch ex As Exception
            MessageBox.Show("Error loading deceased details: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Module1.cn IsNot Nothing AndAlso Module1.cn.State = ConnectionState.Open Then
                Module1.cn.Close()
            End If
        End Try
    End Sub

    ' Method to load the deceased person's current plot location
    Private Sub LoadCurrentPlotDetails()
        Try
            If Module1.cn.State = ConnectionState.Open Then
                Module1.cn.Close()
            End If
            Module1.cn.Open()

            ' SQL query to get current plot details for the deceased
            Dim sql As String = "SELECT l.id, l.type, l.block, l.section, l.row, l.plot, d.Level " &
                                "FROM deceased d " &
                                "JOIN location l ON d.Plot_ID = l.id " &
                                "WHERE d.Deceased_ID = @DeceasedID"

            Using cmd As New MySqlCommand(sql, Module1.cn)
                cmd.Parameters.AddWithValue("@DeceasedID", DeceasedID)
                Using dr As MySqlDataReader = cmd.ExecuteReader()
                    If dr.Read() Then
                        ' Store current plot details
                        CurrentPlotID = dr.GetInt32(dr.GetOrdinal("id"))
                        CurrentPlotLevel = If(dr("Level") Is DBNull.Value, 0, Convert.ToInt32(dr("Level")))
                        Dim plotType As Integer = Convert.ToInt32(dr("type"))
                        Dim block As String = dr("block").ToString()
                        Dim section As String = dr("section").ToString()
                        Dim row As String = dr("row").ToString()
                        Dim plot As String = dr("plot").ToString()

                        ' Format and display current plot location
                        Dim plotTypeString As String
                        Select Case plotType
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

                        Dim plotLocation As String = $"{plotTypeString} - Block {block}, Section {section}, Row {row}, Plot {plot}"

                        ' Add level information if applicable
                        If plotType = 1 OrElse plotType = 3 Then
                             plotLocation &= $", Level {CurrentPlotLevel}"
                        End If

                        LblCurrentPlot.Text = $"Current Plot: {plotLocation}"
                    Else
                        LblCurrentPlot.Text = "Current Plot: Not Assigned"
                    End If
                End Using
            End Using

        Catch ex As Exception
            MessageBox.Show("Error loading current plot details: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Module1.cn IsNot Nothing AndAlso Module1.cn.State = ConnectionState.Open Then
                Module1.cn.Close()
            End If
        End Try
    End Sub

    ' Method to load available plots for transfer
    Private Sub LoadAvailablePlots()
        Try
            If Module1.cn.State = ConnectionState.Open Then
                Module1.cn.Close()
            End If
            Module1.cn.Open()

            ' Create DataTable for ComboBox
            Dim dt As New DataTable()
            dt.Columns.Add("PlotValue", GetType(String))
            dt.Columns.Add("PlotDisplay", GetType(String))

            ' Add default "Select New Plot" row
            dt.Rows.Add("-1,0,0", "-- Select New Plot --") ' Value will be "PlotID,Level,PlotType"

            ' SQL query to find potential available plots for transfer for the client:
            ' 1. Join location to plot_reservation, reservation, and payment to find client's purchased plots.
            ' 2. Exclude the deceased person's current plot.
            ' 3. We will check payment status and occupancy in VB.Net.
            Dim sql As String = "SELECT l.id, pr.level, l.type, l.block, l.section, l.row, l.plot, " & _
                                "CASE WHEN p.total_Paid >= p.total_Amount THEN 'Fully Paid' ELSE 'Partial' END AS Payment_Status, " & _
                                "COUNT(d_count.Deceased_ID) as OccupiedCount " & _
                                "FROM location l " & _
                                "JOIN plot_reservation pr ON l.id = pr.plot_id " & _
                                "JOIN reservation r ON pr.reservation_id = r.Reservation_ID " & _
                                "JOIN payment p ON r.Reservation_ID = p.Reservation_ID " & _
                                "LEFT JOIN deceased d_count ON l.id = d_count.Plot_ID AND (l.type NOT IN (1, 3) OR pr.level = d_count.Level) " & _
                                "WHERE r.Client_ID = @ClientID " & _
                                "AND l.id <> @CurrentPlotID " & _
                                "GROUP BY l.id, pr.level, l.type, l.block, l.section, l.row, l.plot, p.total_Paid, p.total_Amount " & _
                                "ORDER BY l.type, l.block, l.section, l.row, l.plot, pr.level"

            Dim plotData As New List(Of Dictionary(Of String, Object))

            Using cmd As New MySqlCommand(sql, Module1.cn)
                cmd.Parameters.AddWithValue("@ClientID", ClientID)
                cmd.Parameters.AddWithValue("@CurrentPlotID", CurrentPlotID)

                Using dr As MySqlDataReader = cmd.ExecuteReader()
                    While dr.Read()
                        Dim row As New Dictionary(Of String, Object)
                        For i As Integer = 0 To dr.FieldCount - 1
                            row.Add(dr.GetName(i), dr.GetValue(i))
                        Next
                        plotData.Add(row)
                    End While
                End Using
            End Using

            Module1.cn.Close()

            ' Process fetched data to apply Family Lawn Lots payment validation and occupancy check
            Dim familyLawnLotsGroups As New Dictionary(Of String, List(Of Dictionary(Of String, Object)))
            Dim availablePlots As New List(Of Dictionary(Of String, Object))

            For Each plot In plotData
                Dim plotType As Integer = Convert.ToInt32(plot("type"))

                ' Check general payment status (must be fully paid)
                If plot("Payment_Status").ToString() = "Fully Paid" Then

                    ' Check occupancy based on plot type
                    Dim occupiedCount As Integer = Convert.ToInt32(plot("OccupiedCount"))
                    Dim isFull As Boolean = False
                    Select Case plotType
                        Case 1 ' Apartment (max 1 per level)
                            If occupiedCount >= 1 Then isFull = True
                        Case 3 ' Bone Niche (max 10 per level)
                            If occupiedCount >= 10 Then isFull = True
                        Case 4 ' Private (max 4)
                            If occupiedCount >= 4 Then isFull = True
                        Case Else ' Other types (Family Lawn Lots or Unknown) - Only check if already occupied by ONE deceased if there's a limit, otherwise, it's not full based on count.
                            ' Based on clarification, Family Lawn Lots have no fixed limit, so they are never 'full' based on occupiedCount.
                            isFull = False ' Family Lawn Lots are not limited by count
                    End Select

                    ' If the plot is not full, process it
                    If Not isFull Then

                        ' If it's a Family Lawn Lot, group it by block, section, and row
                        If plotType = 2 Then
                            Dim block As String = plot("block").ToString()
                            Dim section As String = plot("section").ToString()
                            Dim row As String = plot("row").ToString()
                            Dim key As String = $"{block}-{section}-{row}"

                            If Not familyLawnLotsGroups.ContainsKey(key) Then
                                familyLawnLotsGroups.Add(key, New List(Of Dictionary(Of String, Object)))
                            End If
                            familyLawnLotsGroups(key).Add(plot)
                        Else
                            ' If not a Family Lawn Lot and fully paid and not full, add it to the available plots list
                            availablePlots.Add(plot)
                        End If
                    End If
                End If ' End of check for fully paid
            Next

            ' Now process the Family Lawn Lots groups
            For Each group In familyLawnLotsGroups.Values
                Dim allFullyPaidAndAvailable As Boolean = True
                For Each plot In group
                    ' This check is technically redundant due to the initial filter, but kept for clarity
                    If plot("Payment_Status").ToString() <> "Fully Paid" Then
                        allFullyPaidAndAvailable = False
                        Exit For
                    End If
                    ' For Family Lawn Lots, we don't check individual plot occupancy as they are unlimited.
                    ' The availability check is based on the entire group being fully paid.
                Next

                ' If all plots in the group are fully paid, add them to the available plots list
                If allFullyPaidAndAvailable Then
                    For Each plot In group
                        availablePlots.Add(plot)
                    Next
                End If
            Next

            ' Sort available plots
            availablePlots.Sort(Function(p1, p2) As Integer
                Dim typeCompare = Convert.ToInt32(p1("type")).CompareTo(Convert.ToInt32(p2("type")))
                If typeCompare <> 0 Then Return typeCompare

                Dim blockCompare = String.Compare(p1("block").ToString(), p2("block").ToString())
                If blockCompare <> 0 Then Return blockCompare

                Dim sectionCompare = String.Compare(p1("section").ToString(), p2("section").ToString())
                If sectionCompare <> 0 Then Return sectionCompare

                Dim rowCompare = String.Compare(p1("row").ToString(), p2("row").ToString())
                If rowCompare <> 0 Then Return rowCompare

                ' Numerically compare plot numbers
                Dim plotCompare = Comparer(Of Integer).Default.Compare(Convert.ToInt32(p1("plot")), Convert.ToInt32(p2("plot")))
                If plotCompare <> 0 Then Return plotCompare

                Dim levelCompare = Convert.ToInt32(p1("level")).CompareTo(Convert.ToInt32(p2("level")))
                Return levelCompare
            End Function)

            ' Populate the ComboBox DataTable from the filtered and sorted list
            For Each plot In availablePlots
                Dim plotId As Integer = Convert.ToInt32(plot("id"))
                Dim level As Integer = If(plot("level") Is DBNull.Value, 0, Convert.ToInt32(plot("level")))
                Dim plotType As Integer = Convert.ToInt32(plot("type"))
                Dim block As String = plot("block").ToString()
                Dim section As String = plot("section").ToString()
                Dim row As String = plot("row").ToString()
                Dim plotPlot As String = plot("plot").ToString()

                ' Format the display string
                Dim plotTypeString As String
                Select Case plotType
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

                Dim plotDisplay As String = $"{plotTypeString} - Block {block}, Section {section}, Row {row}, Plot {plotPlot}"

                ' Add level information if applicable
                If plotType = 1 OrElse plotType = 3 Then
                    plotDisplay &= $", Level {level}"
                End If

                ' Store value as "PlotID,Level,PlotType"
                Dim plotValue As String = $"{plotId},{level},{plotType}"

                dt.Rows.Add(plotValue, plotDisplay)
            Next

            ' Bind to ComboBox
            cmbPlots.DataSource = dt
            cmbPlots.DisplayMember = "PlotDisplay"
            cmbPlots.ValueMember = "PlotValue"

        Catch ex As Exception
            MessageBox.Show("Error loading available plots: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Module1.cn IsNot Nothing AndAlso Module1.cn.State = ConnectionState.Open Then
                Module1.cn.Close()
            End If
        End Try
    End Sub

    Private Sub btnTransfer_Click(sender As Object, e As EventArgs) Handles btnTransfer.Click
        ' Validate that a new plot is selected
        If cmbPlots.SelectedValue Is Nothing OrElse cmbPlots.SelectedValue.ToString() = "-1,0,0" Then
            MessageBox.Show("Please select a new plot for transfer.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' Get the selected plot details from the ComboBox value
        Dim selectedPlotValue As String = cmbPlots.SelectedValue.ToString()
        Dim plotDetails() As String = selectedPlotValue.Split(","c)

        Dim newPlotID As Integer = Convert.ToInt32(plotDetails(0))
        Dim newPlotLevel As Integer = Convert.ToInt32(plotDetails(1))
        Dim newPlotType As Integer = Convert.ToInt32(plotDetails(2))

        ' Determine the level to assign to the deceased record
        Dim levelToAssign As Integer = 0
        If newPlotType = 1 OrElse newPlotType = 3 Then ' Apartment or Bone Niche
            levelToAssign = newPlotLevel
        End If

        Try
            If Module1.cn.State = ConnectionState.Open Then
                Module1.cn.Close()
            End If
            Module1.cn.Open()

            ' Update deceased record with new Plot_ID and Level
            Dim sql As String = "UPDATE deceased SET Plot_ID = @NewPlotID, Level = @LevelToAssign WHERE Deceased_ID = @DeceasedID"

            Using cmd As New MySqlCommand(sql, Module1.cn)
                cmd.Parameters.AddWithValue("@NewPlotID", newPlotID)
                cmd.Parameters.AddWithValue("@LevelToAssign", levelToAssign)
                cmd.Parameters.AddWithValue("@DeceasedID", DeceasedID)
                cmd.ExecuteNonQuery()
            End Using

            MessageBox.Show("Deceased successfully transferred!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

            ' Indicate success and close the form
            Me.DialogResult = DialogResult.OK
            Me.Close()

        Catch ex As Exception
            MessageBox.Show("Error transferring deceased: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
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

End Class