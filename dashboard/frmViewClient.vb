Imports MySql.Data.MySqlClient
Imports System.Data

Public Class frmViewClient
    Private clientID As Integer
    Private WithEvents AssociatedDeceasedList As ListView

    ' Constructor to initialize with selected client ID
    Public Sub New(selectedClientID As Integer)
        InitializeComponent()
        clientID = selectedClientID
        AssociatedDeceasedList = ListView1
    End Sub

    Private Sub frmViewClient_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadClientDetails()
        LoadDeceased()
        LoadClientPlots()
    End Sub

    ' Load client details
    Private Sub LoadClientDetails()
        Try
            dbconn()
            cn.Open()

            sql = "SELECT FirstName, MiddleName, LastName, Mobile, Email, Address FROM client WHERE Client_ID = @ClientID"
            cmd = New MySqlCommand(sql, cn)
            cmd.Parameters.AddWithValue("@ClientID", clientID)
            dr = cmd.ExecuteReader()

            If dr.Read() Then

                Dim fullName As String = $"{dr("LastName")}, {dr("FirstName")} {dr("MiddleName")}"
                LblClient.Text = fullName.Trim()

                LblMobile.Text = If(dr.IsDBNull(dr.GetOrdinal("Mobile")), "", dr("Mobile").ToString())
                LblEmail.Text = If(dr.IsDBNull(dr.GetOrdinal("Email")), "", dr("Email").ToString())
                LblAddress.Text = If(dr.IsDBNull(dr.GetOrdinal("Address")), "", dr("Address").ToString())
            End If

            dr.Close()
            cn.Close()
        Catch ex As MySqlException
            MessageBox.Show("Database error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            MessageBox.Show("An unexpected error occurred: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If cn IsNot Nothing AndAlso cn.State = ConnectionState.Open Then
                cn.Close()
            End If
        End Try
    End Sub

    Private Sub LoadDeceased()
        Try
            dbconn()
            cn.Open()

            ' SQL query to join deceased and location tables
            sql = "SELECT d.Deceased_ID, CONCAT(d.LastName, ', ', d.FirstName, ' ', IFNULL(CONCAT(LEFT(d.MiddleName, 1), '.'), '')) AS FullName, " &
                  "CONCAT(CASE l.type " &
                  "         WHEN 1 THEN 'Apartment' " &
                  "         WHEN 2 THEN 'Family Lawn Lots' " &
                  "         WHEN 3 THEN 'Bone Niche' " &
                  "         WHEN 4 THEN 'Private' " &
                  "         ELSE 'Unknown' " &
                  "       END, ', Block ', IFNULL(l.block, 'N/A'), ', Section ', IFNULL(l.section, 'N/A'), ', Row ', IFNULL(l.row, 'N/A'), ', Plot ', IFNULL(l.plot, 'N/A')) AS PlotLocation " &
                  "FROM deceased d " &
                  "LEFT JOIN location l ON d.Plot_ID = l.id " &
                  "WHERE d.Client_ID = @ClientID"

            cmd = New MySqlCommand(sql, cn)
            cmd.Parameters.AddWithValue("@ClientID", clientID)
            dr = cmd.ExecuteReader()

            ' Clear the list before populating
            AssociatedDeceasedList.Items.Clear()

            While dr.Read()
                Dim newLine = AssociatedDeceasedList.Items.Add(dr("FullName").ToString())
                newLine.Tag = dr("Deceased_ID")  ' Store the Deceased_ID in the Tag property
                newLine.SubItems.Add(dr("PlotLocation").ToString())
            End While

            dr.Close()
            cn.Close()
        Catch ex As MySqlException
            MessageBox.Show("Database error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            MessageBox.Show("An unexpected error occurred: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If cn IsNot Nothing AndAlso cn.State = ConnectionState.Open Then
                cn.Close()
            End If
        End Try
    End Sub

    Private Sub LoadClientPlots()
        Try
            dbconn()
            cn.Open()

            ' SQL query to get all plots associated with client payments from plot_reservation table
            ' Select individual fields for passing to frmDeceasedAssign
            sql = "SELECT l.id, pr.level, l.type, l.block, l.section, l.row, l.plot, " & ' Select individual fields' &
                  "CONCAT(" &
                  "    CASE l.type " &
                  "        WHEN 1 THEN 'Apartment' " &
                  "        WHEN 2 THEN 'Family Lawn Lots' " &
                  "        WHEN 3 THEN 'Bone Niche' " &
                  "        WHEN 4 THEN 'Private' " &
                  "        ELSE 'Unknown' " &
                  "    END, " &
                  "    ' - Block ', IFNULL(l.block, 'N/A'), " &
                  "    ', Section ', IFNULL(l.section, 'N/A'), " &
                  "    ', Row ', IFNULL(l.row, 'N/A'), " &
                  "    ', Plot ', IFNULL(l.plot, 'N/A'), " &
                  "    CASE " &
                  "        WHEN l.type IN (1, 3) THEN CONCAT(' - Level ', IFNULL(pr.level, 'N/A')) " &
                  "        ELSE '' " &
                  "    END" &
                  ") AS PlotInfo " &
                  "FROM location l " &
                  "JOIN plot_reservation pr ON l.id = pr.plot_id " & ' Join location to plot_reservation' &
                  "JOIN reservation r ON pr.reservation_id = r.Reservation_ID " & ' Join plot_reservation to reservation' &
                  "JOIN payment p ON r.Reservation_ID = p.Reservation_ID " & ' Join reservation to payment' &
                  "WHERE r.Client_ID = @ClientID AND p.Payment_Status = 1 " & ' Filter by client and payment status' &
                  "GROUP BY l.id, pr.level " & ' Include pr.level in GROUP BY' &
                  "ORDER BY l.type, l.block, l.section, l.row, l.plot"

            cmd = New MySqlCommand(sql, cn)
            cmd.Parameters.AddWithValue("@ClientID", clientID)
            dr = cmd.ExecuteReader()

            ' Clear the list before populating
            PlotsList.Items.Clear()

            While dr.Read()
                Dim plotInfoItem As New ListViewItem(dr("PlotInfo").ToString())
                plotInfoItem.Tag = If(dr("id") Is DBNull.Value, -1, Convert.ToInt32(dr("id"))) ' Store Plot ID in Tag
                plotInfoItem.SubItems.Add(If(dr("level") Is DBNull.Value, 0, Convert.ToInt32(dr("level"))).ToString()) ' Store Level
                plotInfoItem.SubItems.Add(If(dr("type") Is DBNull.Value, 0, Convert.ToInt32(dr("type"))).ToString()) ' Store Plot Type
                plotInfoItem.SubItems.Add(If(dr("block") Is DBNull.Value, "", dr("block").ToString())) ' Store Block
                plotInfoItem.SubItems.Add(If(dr("section") Is DBNull.Value, "", dr("section").ToString())) ' Store Section
                plotInfoItem.SubItems.Add(If(dr("row") Is DBNull.Value, "", dr("row").ToString())) ' Store Row
                plotInfoItem.SubItems.Add(If(dr("plot") Is DBNull.Value, "", dr("plot").ToString())) ' Store Plot

                PlotsList.Items.Add(plotInfoItem)
            End While

            dr.Close()
            cn.Close()
        Catch ex As MySqlException
            MessageBox.Show("Database error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            MessageBox.Show("An unexpected error occurred: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If cn IsNot Nothing AndAlso cn.State = ConnectionState.Open Then
                cn.Close()
            End If
        End Try
    End Sub

    Private Sub btnAddDeceased_Click(sender As Object, e As EventArgs) Handles btnAddDeceased.Click
        OpenDeceasedAssignForm()
    End Sub

    Private Sub PlotsList_DoubleClick(sender As Object, e As EventArgs) Handles PlotsList.DoubleClick
        OpenDeceasedAssignForm()
    End Sub

    Private Sub OpenDeceasedAssignForm()
        ' Check if a plot is selected
        If PlotsList.SelectedItems.Count = 0 Then
            MessageBox.Show("Please select a plot to assign a deceased person.", "Select Plot", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        ' Get the selected plot item
        Dim selectedPlotItem As ListViewItem = PlotsList.SelectedItems(0)

        ' Retrieve plot details from the ListViewItem's Tag and SubItems
        Dim plotId As Integer = Convert.ToInt32(selectedPlotItem.Tag)
        Dim plotLevel As Integer = Convert.ToInt32(selectedPlotItem.SubItems(1).Text) ' Level is at index 1
        Dim plotType As Integer = Convert.ToInt32(selectedPlotItem.SubItems(2).Text) ' Plot Type is at index 2
        Dim plotBlock As String = selectedPlotItem.SubItems(3).Text ' Block is at index 3
        Dim plotSection As String = selectedPlotItem.SubItems(4).Text ' Section is at index 4
        Dim plotRow As String = selectedPlotItem.SubItems(5).Text ' Row is at index 5
        Dim plotPlot As String = selectedPlotItem.SubItems(6).Text ' Plot is at index 6

        ' Open the frmDeceasedAssign form and pass the necessary details
        Dim deceasedAssignForm As New frmDeceasedAssign(clientID, plotId, plotLevel, plotType, plotBlock, plotSection, plotRow, plotPlot)
        deceasedAssignForm.ShowDialog()

        ' Optionally, refresh the plot list after assignment if needed
        LoadClientPlots() ' Refresh the plot list after frmDeceasedAssign is closed
    End Sub

    ' Event handler for right-clicking on AssociatedDeceasedList to show context menu
    Private Sub AssociatedDeceasedList_MouseDown(sender As Object, e As MouseEventArgs) Handles AssociatedDeceasedList.MouseDown
        If e.Button = MouseButtons.Right Then
            ' Select the item under the mouse cursor
            Dim hitTest As ListViewHitTestInfo = AssociatedDeceasedList.HitTest(e.X, e.Y)
            If hitTest.Item IsNot Nothing Then
                hitTest.Item.Selected = True
            End If
        End If
    End Sub

    Private Sub AssociatedDeceasedList_DoubleClick(sender As Object, e As EventArgs) Handles AssociatedDeceasedList.DoubleClick
        OpenDeceasedTransferForm()
    End Sub

    Private Sub btnTransfer_Click(sender As Object, e As EventArgs) Handles btnTransfer.Click
        OpenDeceasedTransferForm()
    End Sub

    Private Sub OpenDeceasedTransferForm()
        ' Check if a deceased is selected
        If AssociatedDeceasedList.SelectedItems.Count = 0 Then
            MessageBox.Show("Please select a deceased person to transfer.", "Select Deceased", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        ' Get the selected deceased item
        Dim selectedDeceasedItem As ListViewItem = AssociatedDeceasedList.SelectedItems(0)
        Dim deceasedID As Integer = Convert.ToInt32(selectedDeceasedItem.Tag)

        ' Open the transfer form
        Dim transferForm As New frmDeceasedTransfer(clientID, deceasedID)
        If transferForm.ShowDialog() = DialogResult.OK Then
            ' Refresh the deceased list after transfer
            LoadDeceased()
        End If
    End Sub

End Class