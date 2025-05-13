Imports MySql.Data.MySqlClient
Imports System.Data
Imports System.Windows.Forms

Public Class frmUserManagement
    Dim connStr As String = "server=srv594.hstgr.io; database=u976878483_cemetery; username=u976878483_doncarlos; password=d0Nc4los; port=3306;"
    Dim conn As New MySqlConnection(connStr)


    Private Sub LoadUsers()
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If

            Dim query As String = "
SELECT 
    u.user_id, 
    CONCAT(u.F_name, ' ', u.M_name, ' ', u.L_name) AS NameOfUser, 
    u.Username, 
    u.Password, 
    u.email, 
    u.Contact_info, 
    CASE 
        WHEN u.user_typeID = 1 THEN 'admin'
        WHEN u.user_typeID = 2 THEN 'user'
        ELSE 'unknown'
    END AS UserType
FROM 
    user u"
            Dim cmd As New MySqlCommand(query, conn)
            Dim reader As MySqlDataReader = cmd.ExecuteReader()

            UserList.Items.Clear()
            UserList.Columns.Clear()
            UserList.View = View.Details
            UserList.Columns.Add("ID")
            UserList.Columns.Add("Full Name")
            UserList.Columns.Add("Username")
            UserList.Columns.Add("Password")
            UserList.Columns.Add("Email")
            UserList.Columns.Add("Contact")
            UserList.Columns.Add("User Type")

            While reader.Read()
                Dim item As New ListViewItem(reader("user_id").ToString())
                item.SubItems.Add(reader("NameOfUser").ToString())
                item.SubItems.Add(reader("Username").ToString())
                item.SubItems.Add(reader("Password").ToString())
                item.SubItems.Add(reader("email").ToString())
                item.SubItems.Add(reader("Contact_info").ToString())
                item.SubItems.Add(reader("UserType").ToString())
                UserList.Items.Add(item)
            End While

            reader.Close()
            UserList.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize)
        Catch ex As Exception
            MessageBox.Show("Error loading users: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
        End Try
    End Sub

    Private Sub frmUserManagement_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadUsers()
    End Sub


    Private Sub btnAddUser_Click(sender As Object, e As EventArgs) Handles btnAddUser.Click
        Dim addUserForm As New frmAddUser()
        If addUserForm.ShowDialog() = DialogResult.OK Then
            LoadUsers()
        End If

    End Sub

    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        If UserList.SelectedItems.Count > 0 Then
            Dim userId As Integer = CInt(UserList.SelectedItems(0).SubItems(0).Text) ' Get user ID from first column
            Dim editUserForm As New frmEditUser(userId)
            If editUserForm.ShowDialog() = DialogResult.OK Then
                LoadUsers() ' Refresh the list after successful edit
            End If
        Else
            MessageBox.Show("Please select a user to edit.", "No User Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    Private Sub btnShowAll_Click(sender As Object, e As EventArgs) Handles btnShowAll.Click
        LoadUsers()
    End Sub
End Class