<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAccApproval
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
        Me.components = New System.ComponentModel.Container()
        Me.Guna2Panel2 = New Guna.UI2.WinForms.Guna2Panel()
        Me.btnReject = New Guna.UI2.WinForms.Guna2Button()
        Me.btnShowAll = New Guna.UI2.WinForms.Guna2Button()
        Me.btnDelete = New Guna.UI2.WinForms.Guna2Button()
        Me.btnApproval = New Guna.UI2.WinForms.Guna2Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Guna2Panel1 = New Guna.UI2.WinForms.Guna2Panel()
        Me.txtSearch = New Guna.UI2.WinForms.Guna2TextBox()
        Me.ApprovalList = New System.Windows.Forms.ListView()
        Me.id = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.applicant = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.gender = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.mobile = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.email = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.address = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.dateRegistered = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.status = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.Guna2Panel2.SuspendLayout()
        Me.Guna2Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Guna2Panel2
        '
        Me.Guna2Panel2.AutoSize = True
        Me.Guna2Panel2.Controls.Add(Me.btnReject)
        Me.Guna2Panel2.Controls.Add(Me.btnShowAll)
        Me.Guna2Panel2.Controls.Add(Me.btnDelete)
        Me.Guna2Panel2.Controls.Add(Me.btnApproval)
        Me.Guna2Panel2.Controls.Add(Me.Label2)
        Me.Guna2Panel2.Controls.Add(Me.Label1)
        Me.Guna2Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Guna2Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Guna2Panel2.Name = "Guna2Panel2"
        Me.Guna2Panel2.Size = New System.Drawing.Size(1295, 81)
        Me.Guna2Panel2.TabIndex = 5
        '
        'btnReject
        '
        Me.btnReject.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.btnReject.Animated = True
        Me.btnReject.FillColor = System.Drawing.Color.Crimson
        Me.btnReject.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReject.ForeColor = System.Drawing.Color.White
        Me.btnReject.Location = New System.Drawing.Point(1114, 45)
        Me.btnReject.Name = "btnReject"
        Me.btnReject.Size = New System.Drawing.Size(88, 25)
        Me.btnReject.TabIndex = 8
        Me.btnReject.Text = "Reject"
        '
        'btnShowAll
        '
        Me.btnShowAll.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.btnShowAll.Animated = True
        Me.btnShowAll.FillColor = System.Drawing.Color.DarkGoldenrod
        Me.btnShowAll.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnShowAll.ForeColor = System.Drawing.Color.White
        Me.btnShowAll.Location = New System.Drawing.Point(939, 45)
        Me.btnShowAll.Name = "btnShowAll"
        Me.btnShowAll.Size = New System.Drawing.Size(75, 25)
        Me.btnShowAll.TabIndex = 6
        Me.btnShowAll.Text = "Refresh"
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.btnDelete.Animated = True
        Me.btnDelete.FillColor = System.Drawing.Color.Red
        Me.btnDelete.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.ForeColor = System.Drawing.Color.White
        Me.btnDelete.Location = New System.Drawing.Point(1208, 45)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(75, 25)
        Me.btnDelete.TabIndex = 3
        Me.btnDelete.Text = "Delete"
        '
        'btnApproval
        '
        Me.btnApproval.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.btnApproval.Animated = True
        Me.btnApproval.FillColor = System.Drawing.Color.Blue
        Me.btnApproval.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnApproval.ForeColor = System.Drawing.Color.White
        Me.btnApproval.Location = New System.Drawing.Point(1020, 45)
        Me.btnApproval.Name = "btnApproval"
        Me.btnApproval.Size = New System.Drawing.Size(88, 25)
        Me.btnApproval.TabIndex = 2
        Me.btnApproval.Text = "Approve"
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Gill Sans MT", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.Control
        Me.Label2.Location = New System.Drawing.Point(15, 44)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(261, 21)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "List of clients registered thru the website"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Century Gothic", 21.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.Control
        Me.Label1.Location = New System.Drawing.Point(12, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(323, 36)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Account Approval List"
        '
        'Guna2Panel1
        '
        Me.Guna2Panel1.AutoSize = True
        Me.Guna2Panel1.Controls.Add(Me.txtSearch)
        Me.Guna2Panel1.Controls.Add(Me.ApprovalList)
        Me.Guna2Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Guna2Panel1.Location = New System.Drawing.Point(0, 81)
        Me.Guna2Panel1.Name = "Guna2Panel1"
        Me.Guna2Panel1.ShadowDecoration.Enabled = True
        Me.Guna2Panel1.Size = New System.Drawing.Size(1295, 403)
        Me.Guna2Panel1.TabIndex = 6
        '
        'txtSearch
        '
        Me.txtSearch.Animated = True
        Me.txtSearch.AutoRoundedCorners = True
        Me.txtSearch.BorderColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(171, Byte), Integer), CType(CType(189, Byte), Integer))
        Me.txtSearch.BorderRadius = 16
        Me.txtSearch.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSearch.DefaultText = ""
        Me.txtSearch.DisabledState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.txtSearch.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer))
        Me.txtSearch.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.txtSearch.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.txtSearch.FocusedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtSearch.Font = New System.Drawing.Font("Segoe UI", 11.25!)
        Me.txtSearch.ForeColor = System.Drawing.Color.Black
        Me.txtSearch.HoverState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtSearch.IconRight = Global.dashboard.My.Resources.Resources.search
        Me.txtSearch.IconRightOffset = New System.Drawing.Point(8, 0)
        Me.txtSearch.IconRightSize = New System.Drawing.Size(25, 25)
        Me.txtSearch.Location = New System.Drawing.Point(12, 7)
        Me.txtSearch.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.txtSearch.PlaceholderForeColor = System.Drawing.Color.Gray
        Me.txtSearch.PlaceholderText = "Search a name"
        Me.txtSearch.SelectedText = ""
        Me.txtSearch.Size = New System.Drawing.Size(292, 35)
        Me.txtSearch.TabIndex = 2
        '
        'ApprovalList
        '
        Me.ApprovalList.AllowColumnReorder = True
        Me.ApprovalList.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ApprovalList.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.ApprovalList.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.ApprovalList.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.id, Me.applicant, Me.gender, Me.mobile, Me.email, Me.address, Me.dateRegistered, Me.status})
        Me.ApprovalList.Font = New System.Drawing.Font("Roboto", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ApprovalList.ForeColor = System.Drawing.SystemColors.Window
        Me.ApprovalList.FullRowSelect = True
        Me.ApprovalList.GridLines = True
        Me.ApprovalList.HideSelection = False
        Me.ApprovalList.Location = New System.Drawing.Point(12, 49)
        Me.ApprovalList.Name = "ApprovalList"
        Me.ApprovalList.ShowItemToolTips = True
        Me.ApprovalList.Size = New System.Drawing.Size(1271, 349)
        Me.ApprovalList.TabIndex = 1
        Me.ApprovalList.TileSize = New System.Drawing.Size(10, 10)
        Me.ApprovalList.UseCompatibleStateImageBehavior = False
        Me.ApprovalList.View = System.Windows.Forms.View.Details
        '
        'id
        '
        Me.id.Text = "ID"
        Me.id.Width = 50
        '
        'applicant
        '
        Me.applicant.Text = "Name of Applicant"
        Me.applicant.Width = 270
        '
        'gender
        '
        Me.gender.Text = "Gender"
        Me.gender.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.gender.Width = 100
        '
        'mobile
        '
        Me.mobile.Text = "Contact"
        Me.mobile.Width = 150
        '
        'email
        '
        Me.email.Text = "Email"
        Me.email.Width = 200
        '
        'address
        '
        Me.address.Text = "Address"
        Me.address.Width = 250
        '
        'dateRegistered
        '
        Me.dateRegistered.Text = "Date Reserved"
        Me.dateRegistered.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.dateRegistered.Width = 150
        '
        'status
        '
        Me.status.Text = "Status"
        Me.status.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.status.Width = 100
        '
        'ToolTip1
        '
        Me.ToolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info
        '
        'frmAccApproval
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(14, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(90, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1295, 484)
        Me.Controls.Add(Me.Guna2Panel1)
        Me.Controls.Add(Me.Guna2Panel2)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "frmAccApproval"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "frmAccApproval"
        Me.Guna2Panel2.ResumeLayout(False)
        Me.Guna2Panel2.PerformLayout()
        Me.Guna2Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Guna2Panel2 As Guna.UI2.WinForms.Guna2Panel
    Friend WithEvents btnShowAll As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents btnDelete As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents btnApproval As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Guna2Panel1 As Guna.UI2.WinForms.Guna2Panel
    Friend WithEvents txtSearch As Guna.UI2.WinForms.Guna2TextBox
    Friend WithEvents ApprovalList As ListView
    Friend WithEvents id As ColumnHeader
    Friend WithEvents applicant As ColumnHeader
    Friend WithEvents gender As ColumnHeader
    Friend WithEvents mobile As ColumnHeader
    Friend WithEvents email As ColumnHeader
    Friend WithEvents address As ColumnHeader
    Friend WithEvents dateRegistered As ColumnHeader
    Friend WithEvents status As ColumnHeader
    Friend WithEvents btnReject As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents ToolTip1 As ToolTip
End Class
