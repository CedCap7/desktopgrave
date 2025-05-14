Imports MySql.Data.MySqlClient

Public Class frmUpdatePackage
    Private packageId As Integer
    Private parentForm As frmPackages

    Public Sub New(p_id As Integer, parent As frmPackages)
        InitializeComponent()
        packageId = p_id
        parentForm = parent
        LoadPackageDetails()
    End Sub

    Private Sub LoadPackageDetails()
        Try
            dbconn()
            If cn.State = ConnectionState.Closed Then cn.Open()
            Dim sql As String = "SELECT package_name, price, description FROM package WHERE p_id = @id"
            Dim cmd As New MySqlCommand(sql, cn)
            cmd.Parameters.AddWithValue("@id", packageId)
            Dim dr As MySqlDataReader = cmd.ExecuteReader()
            If dr.Read() Then
                lblPackagePrice.Text = dr("package_name").ToString()
                txtPrice.Text = dr("price").ToString()
                lblDescription.Text = "Description: " & dr("description").ToString()
                Me.Text = "Update Price - " & dr("package_name").ToString()
            End If
            dr.Close()
            cn.Close()
        Catch ex As Exception
            MessageBox.Show("Error loading package details: " & ex.Message)
        End Try
    End Sub

    Private Sub btnConfirm_Click(sender As Object, e As EventArgs)
        Try
            Dim newPrice As Decimal
            If Not Decimal.TryParse(txtPrice.Text, newPrice) Then
                MessageBox.Show("Please enter a valid price.")
                Return
            End If
            dbconn()
            If cn.State = ConnectionState.Closed Then cn.Open()
            Dim sql As String = "UPDATE package SET price = @price WHERE p_id = @id"
            Dim cmd As New MySqlCommand(sql, cn)
            cmd.Parameters.AddWithValue("@price", newPrice)
            cmd.Parameters.AddWithValue("@id", packageId)
            cmd.ExecuteNonQuery()
            cn.Close()
            MessageBox.Show("Price updated successfully.")
            parentForm.RefreshPackages()
            Me.Close()
        Catch ex As Exception
            MessageBox.Show("Error updating price: " & ex.Message)
        End Try
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs)
        Me.Close()
    End Sub
End Class