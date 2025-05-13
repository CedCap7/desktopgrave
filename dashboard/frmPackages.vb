Imports MySql.Data.MySqlClient

Public Class frmPackages
    Private packageIds As New List(Of Integer)()

    Private Sub frmPackages_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadPackages()
    End Sub

    Private Sub LoadPackages()
        Try
            dbconn()
            If cn.State = ConnectionState.Closed Then cn.Open()
            Dim sql As String = "SELECT p_id, package_name, price, description FROM package ORDER BY p_id ASC"
            Dim cmd As New MySqlCommand(sql, cn)
            Dim dr As MySqlDataReader = cmd.ExecuteReader()

            Dim priceLabels = New Label() {lblPrice1, lblPrice3, lblPrice2, lblPrice4}
            Dim descLabels = New Label() {lblDesc1, lblDesc3, lblDesc2, lblDesc4}
            packageIds.Clear()
            Dim i As Integer = 0
            While dr.Read() AndAlso i < 4
                priceLabels(i).Text = "Price: ₱" & FormatNumber(dr("price"), 2)
                descLabels(i).Text = "Description: " & dr("description").ToString()
                packageIds.Add(Convert.ToInt32(dr("p_id")))
                i += 1
            End While
            dr.Close()
            cn.Close()
        Catch ex As Exception
            MessageBox.Show("Error loading packages: " & ex.Message)
        End Try
    End Sub

    Private Sub Guna2Button1_Click(sender As Object, e As EventArgs) Handles Guna2Button1.Click
        OpenUpdateForm(0)
    End Sub
    Private Sub Guna2Button2_Click(sender As Object, e As EventArgs) Handles Guna2Button2.Click
        OpenUpdateForm(1)
    End Sub
    Private Sub Guna2Button3_Click(sender As Object, e As EventArgs) Handles Guna2Button3.Click
        OpenUpdateForm(2)
    End Sub
    Private Sub Guna2Button4_Click(sender As Object, e As EventArgs) Handles Guna2Button4.Click
        OpenUpdateForm(3)
    End Sub

    Private Sub OpenUpdateForm(index As Integer)
        If index < 0 OrElse index >= packageIds.Count Then Return
        Dim updateForm As New frmUpdatePackage(packageIds(index), Me)
        updateForm.ShowDialog()
    End Sub

    ' Public method to refresh after update
    Public Sub RefreshPackages()
        LoadPackages()
    End Sub
End Class