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

            ' SQL query to find available plots:
            ' 1. Join location to plot_reservation, reservation, and payment to find client's purchased plots.
            ' 2. Exclude the deceased person's current plot.
            ' 3. Check occupancy based on plot type and level.
            Dim sql As String = "SELECT l.id, pr.level, l.type, l.block, l.section, l.row, l.plot " &
                                "FROM location l " &
                                "JOIN plot_reservation pr ON l.id = pr.plot_id " &
                                "JOIN reservation r ON pr.reservation_id = r.Reservation_ID " &
                                "JOIN payment p ON r.Reservation_ID = p.Reservation_ID " &
                                "LEFT JOIN deceased d_count ON l.id = d_count.Plot_ID AND (l.type NOT IN (1, 3) OR pr.level = d_count.Level) " &
                                "WHERE r.Client_ID = @ClientID " &
                                "AND p.Payment_Status = 1 " &
                                "AND l.id <> @CurrentPlotID " &
                                "GROUP BY l.id, pr.level, l.type, l.block, l.section, l.row, l.plot " &
                                "HAVING " &
                                "    (l.type = 4 AND COUNT(d_count.Deceased_ID) < 4) OR " & ' Private: max 4 deceased' &
                                "    (l.type = 1 AND COUNT(d_count.Deceased_ID) < 1) OR " & ' Apartment: max 1 per level' &
                                "    (l.type = 3 AND COUNT(d_count.Deceased_ID) < 10) OR " & ' Bone Niche: max 10 per level' &
                                "    l.type NOT IN (1, 3, 4) " &
                                "ORDER BY l.type, l.block, l.section, l.row, l.plot, pr.level"

            Using cmd As New MySqlCommand(sql, Module1.cn)
                cmd.Parameters.AddWithValue("@ClientID", ClientID)
                cmd.Parameters.AddWithValue("@CurrentPlotID", CurrentPlotID)

                Using dr As MySqlDataReader = cmd.ExecuteReader()
                    While dr.Read()
                        Dim plotId As Integer = dr.GetInt32(dr.GetOrdinal("id"))
                        Dim level As Integer = If(dr("level") Is DBNull.Value, 0, Convert.ToInt32(dr("level")))
                        Dim plotType As Integer = Convert.ToInt32(dr("type"))
                        Dim block As String = dr("block").ToString()
                        Dim section As String = dr("section").ToString()
                        Dim row As String = dr("row").ToString()
                        Dim plot As String = dr("plot").ToString()

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

                         Dim plotDisplay As String = $"{plotTypeString} - Block {block}, Section {section}, Row {row}, Plot {plot}"

                        ' Add level information if applicable
                        If plotType = 1 OrElse plotType = 3 Then
                             plotDisplay &= $", Level {level}"
                        End If

                        ' Store value as "PlotID,Level,PlotType"
                        Dim plotValue As String = $"{plotId},{level},{plotType}"

                        dt.Rows.Add(plotValue, plotDisplay)
                    End While
                End Using
            End Using

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