<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmUserManagement
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Guna2Panel2 = New Guna.UI2.WinForms.Guna2Panel()
        Me.btnEdit = New Guna.UI2.WinForms.Guna2Button()
        Me.btnAddUser = New Guna.UI2.WinForms.Guna2Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.UserList = New System.Windows.Forms.ListView()
        Me.id = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.user_type_id = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Fullname = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Username = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Password = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.email = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Contact = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Guna2Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'Guna2Panel2
        '
        Me.Guna2Panel2.AutoSize = True
        Me.Guna2Panel2.Controls.Add(Me.btnEdit)
        Me.Guna2Panel2.Controls.Add(Me.btnAddUser)
        Me.Guna2Panel2.Controls.Add(Me.Label2)
        Me.Guna2Panel2.Controls.Add(Me.Label1)
        Me.Guna2Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Guna2Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Guna2Panel2.Margin = New System.Windows.Forms.Padding(4)
        Me.Guna2Panel2.Name = "Guna2Panel2"
        Me.Guna2Panel2.Size = New System.Drawing.Size(1371, 100)
        Me.Guna2Panel2.TabIndex = 6
        '
        'btnEdit
        '
        Me.btnEdit.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.btnEdit.Animated = True
        Me.btnEdit.FillColor = System.Drawing.Color.Blue
        Me.btnEdit.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnEdit.ForeColor = System.Drawing.Color.White
        Me.btnEdit.Location = New System.Drawing.Point(1064, 54)
        Me.btnEdit.Margin = New System.Windows.Forms.Padding(4)
        Me.btnEdit.Name = "btnEdit"
        Me.btnEdit.Size = New System.Drawing.Size(117, 31)
        Me.btnEdit.TabIndex = 8
        Me.btnEdit.Text = "Edit "
        '
        'btnAddUser
        '
        Me.btnAddUser.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.btnAddUser.Animated = True
        Me.btnAddUser.FillColor = System.Drawing.Color.Green
        Me.btnAddUser.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddUser.ForeColor = System.Drawing.Color.White
        Me.btnAddUser.Location = New System.Drawing.Point(924, 54)
        Me.btnAddUser.Margin = New System.Windows.Forms.Padding(4)
        Me.btnAddUser.Name = "btnAddUser"
        Me.btnAddUser.Size = New System.Drawing.Size(117, 31)
        Me.btnAddUser.TabIndex = 2
        Me.btnAddUser.Text = "Add User"
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(14, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(90, Byte), Integer))
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.Control
        Me.Label2.Location = New System.Drawing.Point(20, 54)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(378, 24)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "List of users that can manage the application"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 22.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.Control
        Me.Label1.Location = New System.Drawing.Point(16, 11)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(348, 44)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "User Management"
        '
        'UserList
        '
        Me.UserList.AllowColumnReorder = True
        Me.UserList.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.UserList.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.UserList.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.UserList.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.id, Me.user_type_id, Me.Fullname, Me.Username, Me.Password, Me.email, Me.Contact})
        Me.UserList.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UserList.ForeColor = System.Drawing.SystemColors.Window
        Me.UserList.FullRowSelect = True
        Me.UserList.GridLines = True
        Me.UserList.HideSelection = False
        Me.UserList.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.UserList.Location = New System.Drawing.Point(13, 122)
        Me.UserList.Margin = New System.Windows.Forms.Padding(4)
        Me.UserList.Name = "UserList"
        Me.UserList.ShowItemToolTips = True
        Me.UserList.Size = New System.Drawing.Size(1358, 461)
        Me.UserList.TabIndex = 8
        Me.UserList.TileSize = New System.Drawing.Size(10, 10)
        Me.UserList.UseCompatibleStateImageBehavior = False
        Me.UserList.View = System.Windows.Forms.View.Details
        '
        'id
        '
        Me.id.Text = "ID"
        Me.id.Width = 150
        '
        'user_type_id
        '
        Me.user_type_id.DisplayIndex = 6
        Me.user_type_id.Text = "User type"
        Me.user_type_id.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.user_type_id.Width = 250
        '
        'Fullname
        '
        Me.Fullname.DisplayIndex = 1
        Me.Fullname.Text = "Name of User"
        Me.Fullname.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.Fullname.Width = 300
        '
        'Username
        '
        Me.Username.DisplayIndex = 2
        Me.Username.Text = "Username"
        Me.Username.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.Username.Width = 300
        '
        'Password
        '
        Me.Password.DisplayIndex = 3
        Me.Password.Text = "Password"
        Me.Password.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.Password.Width = 300
        '
        'email
        '
        Me.email.DisplayIndex = 4
        Me.email.Text = "Email"
        Me.email.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.email.Width = 300
        '
        'Contact
        '
        Me.Contact.DisplayIndex = 5
        Me.Contact.Text = "Contact No"
        Me.Contact.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.Contact.Width = 300
        '
        'frmUserManagement
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(14, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(90, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1371, 596)
        Me.Controls.Add(Me.UserList)
        Me.Controls.Add(Me.Guna2Panel2)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "frmUserManagement"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "frmUserManagement"
        Me.Guna2Panel2.ResumeLayout(False)
        Me.Guna2Panel2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Guna2Panel2 As Guna.UI2.WinForms.Guna2Panel
    Friend WithEvents btnEdit As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents btnAddUser As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents UserList As ListView
    Friend WithEvents id As ColumnHeader
    Friend WithEvents Fullname As ColumnHeader
    Friend WithEvents Username As ColumnHeader
    Friend WithEvents Password As ColumnHeader
    Friend WithEvents email As ColumnHeader
    Friend WithEvents Contact As ColumnHeader
    Friend WithEvents user_type_id As ColumnHeader
End Class
