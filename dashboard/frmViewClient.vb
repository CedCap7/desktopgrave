Imports MySql.Data.MySqlClient

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
            sql = "SELECT CONCAT(d.LastName, ', ', d.FirstName, ' ', IFNULL(CONCAT(LEFT(d.MiddleName, 1), '.'), '')) AS FullName, " &
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


End Class