Imports MySql.Data.MySqlClient

Public Class frmClientReg
    Private Sub ClientList_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        KeyPreview = True
        LoadClients()
    End Sub

    ' Load clients with optional search functionality
    Sub LoadClients(Optional searchText As String = "")
        dbconn()
        cn.Open()

        sql = "SELECT Client_ID, LastName, FirstName, MiddleName, Date_Registered, status FROM client " &
      "WHERE LastName LIKE @search OR FirstName LIKE @search OR MiddleName LIKE @search " &
      "ORDER BY LastName ASC"


        cmd = New MySqlCommand(sql, cn)
        cmd.Parameters.AddWithValue("@search", "%" & searchText & "%")
        dr = cmd.ExecuteReader

        ClientList.Items.Clear()

        While dr.Read
            newLine = ClientList.Items.Add(dr("Client_ID").ToString())

            ' Full Name
            newLine.SubItems.Add(dr("LastName") & ", " & dr("FirstName") & " " & dr("MiddleName"))

            ' Date Registered
            Dim dateReg As String = If(IsDBNull(dr("Date_Registered")), "", CDate(dr("Date_Registered")).ToString("yyyy-MM-dd"))
            newLine.SubItems.Add(dateReg)

            ' Status
            Dim statusText As String = If(dr("status") = 1, "Active", "Inactive")
            newLine.SubItems.Add(statusText)
        End While


        cmd.Dispose()
        dr.Close()
        cn.Close()
    End Sub

    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        If ClientList.SelectedItems.Count = 0 Then
            MessageBox.Show("Please select a client to edit.", "Edit Client", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim selectedID As Integer = CInt(ClientList.SelectedItems(0).SubItems(0).Text)
        Dim updateForm As New frmUpdateClient(selectedID)
        updateForm.ShowDialog()

        ' Reload clients list after editing
        LoadClients()
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        If ClientList.SelectedItems.Count = 0 Then
            MessageBox.Show("Please select a client to delete.", "Delete Client", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim selectedID As Integer = CInt(ClientList.SelectedItems(0).SubItems(0).Text)
        Dim clientName As String = ClientList.SelectedItems(0).SubItems(1).Text

        Dim confirmDelete As DialogResult = MessageBox.Show("Are you sure you want to delete client: " & clientName & "?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If confirmDelete = DialogResult.Yes Then
            Try
                dbconn()
                cn.Open()

                sql = "DELETE FROM client WHERE Client_ID = @ID"
                cmd = New MySqlCommand(sql, cn)
                cmd.Parameters.AddWithValue("@ID", selectedID)

                cmd.ExecuteNonQuery()
                cn.Close()

                MessageBox.Show("Client deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

                ' Refresh client list
                LoadClients()

            Catch ex As MySqlException
                MessageBox.Show("Database error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Catch ex As Exception
                MessageBox.Show("An unexpected error occurred: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                If cn IsNot Nothing AndAlso cn.State = ConnectionState.Open Then
                    cn.Close()
                End If
            End Try
        End If
    End Sub

    Private Sub ClientList_DoubleClick(sender As Object, e As EventArgs) Handles ClientList.DoubleClick
        If ClientList.SelectedItems.Count = 0 Then
            MessageBox.Show("Please select a client.", "View Client", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim selectedID As Integer = CInt(ClientList.SelectedItems(0).SubItems(0).Text)

        ' Pass the selected client ID to frmViewClient
        Dim viewClientForm As New frmViewClient(selectedID)
        viewClientForm.ShowDialog()
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        LoadClients()
    End Sub

    ' Search functionality added
    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        LoadClients(txtSearch.Text.Trim()) ' Search as user types
    End Sub
End Class
