Imports MySql.Data.MySqlClient

Module Module1
    Public cn As New MySqlConnection
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
        'cn.ConnectionString = "server=172.16.11.195; database=razonado; username=razonado; password=123; port=3306"
        cn.ConnectionString = "server=localhost; database=dccms; username=root; password=root; port=3306"
    End Sub

End Module