Imports MySql.Data.MySqlClient

Module Module1
    Public cn As New MySqlConnection("server=srv594.hstgr.io; database=u976878483_cemetery; username=u976878483_doncarlos; password=d0Nc4los; port=3306")
    Public dr As MySqlDataReader
    Public cmd As MySqlCommand

    Public uid As Integer
    Public itemid As Integer
    Public supid As Integer
    Public sql As String
    Public newLine As ListViewItem

    Public imgpath As String
    Public arrImage() As Byte

    Public Sub dbconn()
        Try
            If cn.State = ConnectionState.Closed Then
                cn.Open()
            End If
        Catch ex As Exception
            MessageBox.Show("Error connecting to database: " & ex.Message)
        End Try
    End Sub
End Module