Imports MySql.Data.MySqlClient
Imports Guna.UI2.WinForms

Public Class frmDeceasedDetails
    Private deceasedId As String
    Private cn As MySqlConnection

    Public Sub New(id As String)
        InitializeComponent()
        deceasedId = id
        InitializeConnection()
        LoadDeceasedDetails()
    End Sub

    Private Sub InitializeConnection()
        Try
            cn = New MySqlConnection("server=localhost; database=dccms; username=root; password=root; port=3306")
            dbconn()
        Catch ex As Exception
            MessageBox.Show("Error initializing database connection: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub LoadDeceasedDetails()
        Try
            If cn.State <> ConnectionState.Open Then cn.Open()

            Dim sql As String = "SELECT d.*, l.block, l.section, l.row, l.plot, 
                                       CASE l.type 
                                           WHEN 1 THEN 'Apartment' 
                                           WHEN 2 THEN 'Family Lawn Lots' 
                                           WHEN 3 THEN 'Bone Niche' 
                                           WHEN 4 THEN 'Private' 
                                       END AS PlotType
                                FROM deceased d
                                JOIN location l ON d.Plot_ID = l.id
                                WHERE d.Deceased_ID = @id"

            Using cmd As New MySqlCommand(sql, cn)
                cmd.Parameters.AddWithValue("@id", deceasedId)
                Using reader As MySqlDataReader = cmd.ExecuteReader()
                    If reader.Read() Then
                        ' Display the details in your form controls
                        lblName.Text = $"{reader("FirstName")} {reader("LastName")}"
                        lblIntermentDate.Text = Convert.ToDateTime(reader("Interment")).ToString("yyyy-MM-dd")
                        lblPlotLocation.Text = $"{reader("PlotType")} - Block {reader("block")}, Section {reader("section")}, Row {reader("row")}, Plot {reader("plot")}"
                        lblStatus.Text = reader("deceased_status").ToString()
                    End If
                End Using
            End Using

        Catch ex As Exception
            MessageBox.Show("Error loading deceased details: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If cn.State = ConnectionState.Open Then cn.Close()
        End Try
    End Sub
End Class