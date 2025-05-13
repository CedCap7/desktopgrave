Imports MySql.Data.MySqlClient

Public Class frmClientReg
    Private Sub ClientList_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        KeyPreview = True

        ' Initialize context menu
        ClientList.ContextMenuStrip = contextMenu

        LoadClients()
    End Sub

    ' Load clients with optional search functionality
    Sub LoadClients(Optional searchText As String = "")
        Try
            dbconn()
            cn.Open()

            sql = "SELECT c.Client_ID, c.LastName, c.FirstName, c.MiddleName, c.Date_Registered, c.Status " &
                  "FROM client c " &
                  "WHERE c.LastName LIKE @search OR c.FirstName LIKE @search OR c.MiddleName LIKE @search " &
                  "ORDER BY c.LastName ASC"

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

                ' Status based on Status column
                Dim statusText As String = If(Convert.ToInt32(dr("Status")) = 1, "Active", "Inactive")
                newLine.SubItems.Add(statusText)
            End While

            cmd.Dispose()
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

    Private Sub ClientList_MouseDown(sender As Object, e As MouseEventArgs) Handles ClientList.MouseDown
        If e.Button = MouseButtons.Right Then
            ' Select the item under the mouse cursor
            Dim hitTest As ListViewHitTestInfo = ClientList.HitTest(e.X, e.Y)
            If hitTest.Item IsNot Nothing Then
                ClientList.SelectedItems.Clear()
                hitTest.Item.Selected = True
            End If
        End If
    End Sub

    Private Sub InactiveToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles InactiveToolStripMenuItem.Click
        SetClientInactive()
    End Sub

    Private Sub ActiveToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ActiveToolStripMenuItem.Click
        SetClientActive()
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        LoadClients()
    End Sub

    ' Search functionality added
    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        LoadClients(txtSearch.Text.Trim()) ' Search as user types
    End Sub

    ' Add a new method to handle setting client status to active
    Private Sub SetClientActive()
        If ClientList.SelectedItems.Count = 0 Then
            MessageBox.Show("Please select a client to set as active.", "Set Active", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim selectedID As Integer = CInt(ClientList.SelectedItems(0).SubItems(0).Text)
        Dim clientName As String = ClientList.SelectedItems(0).SubItems(1).Text

        Dim confirmActive As DialogResult = MessageBox.Show($"Are you sure you want to set {clientName} as active?", "Confirm Active", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If confirmActive = DialogResult.Yes Then
            Try
                dbconn()
                cn.Open()

                ' Update the client status to 1
                sql = "UPDATE client SET Status = 1 WHERE Client_ID = @ID"
                cmd = New MySqlCommand(sql, cn)
                cmd.Parameters.AddWithValue("@ID", selectedID)

                cmd.ExecuteNonQuery()
                cn.Close()

                MessageBox.Show("Client status updated to active.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

                ' Refresh client list to show updated status
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

    ' Add a new method to handle setting client status to inactive
    Private Sub SetClientInactive()
        If ClientList.SelectedItems.Count = 0 Then
            MessageBox.Show("Please select a client to set as inactive.", "Set Inactive", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim selectedID As Integer = CInt(ClientList.SelectedItems(0).SubItems(0).Text)
        Dim clientName As String = ClientList.SelectedItems(0).SubItems(1).Text

        Dim confirmInactive As DialogResult = MessageBox.Show($"Are you sure you want to set {clientName} as inactive?", "Confirm Inactive", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If confirmInactive = DialogResult.Yes Then
            Try
                dbconn()
                cn.Open()

                ' Update the client status to 0
                sql = "UPDATE client SET Status = 0 WHERE Client_ID = @ID"
                cmd = New MySqlCommand(sql, cn)
                cmd.Parameters.AddWithValue("@ID", selectedID)

                cmd.ExecuteNonQuery()
                cn.Close()

                MessageBox.Show("Client status updated to inactive.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

                ' Refresh client list to show updated status
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

End Class
