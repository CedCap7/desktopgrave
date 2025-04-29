<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDeceasedReg
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Guna2Elipse1 = New Guna.UI2.WinForms.Guna2Elipse(Me.components)
        Me.Guna2Panel2 = New Guna.UI2.WinForms.Guna2Panel()
        Me.btnImportExcel = New Guna.UI2.WinForms.Guna2Button()
        Me.btnRefresh = New Guna.UI2.WinForms.Guna2Button()
        Me.btnDelete = New Guna.UI2.WinForms.Guna2Button()
        Me.btnEdit = New Guna.UI2.WinForms.Guna2Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.DeceasedList = New System.Windows.Forms.ListView()
        Me.number = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.fullname = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.birth = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.death = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.loc = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.status = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Guna2Panel1 = New Guna.UI2.WinForms.Guna2Panel()
        Me.chkbxBoneNiche = New Guna.UI2.WinForms.Guna2CheckBox()
        Me.chkbxPrivate = New Guna.UI2.WinForms.Guna2CheckBox()
        Me.chkbxLawnLots = New Guna.UI2.WinForms.Guna2CheckBox()
        Me.chkbxApartment = New Guna.UI2.WinForms.Guna2CheckBox()
        Me.txtSearch = New Guna.UI2.WinForms.Guna2TextBox()
        Me.contextMenu = New Guna.UI2.WinForms.Guna2ContextMenuStrip()
        Me.RemainingToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RelocationToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RenewalToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PendingToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Guna2Panel2.SuspendLayout()
        Me.Guna2Panel1.SuspendLayout()
        Me.contextMenu.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Century Gothic", 21.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.Control
        Me.Label1.Location = New System.Drawing.Point(15, 11)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(354, 44)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Deceased Registry"
        '
        'Guna2Elipse1
        '
        Me.Guna2Elipse1.BorderRadius = 20
        Me.Guna2Elipse1.TargetControl = Me
        '
        'Guna2Panel2
        '
        Me.Guna2Panel2.AutoSize = True
        Me.Guna2Panel2.Controls.Add(Me.btnImportExcel)
        Me.Guna2Panel2.Controls.Add(Me.btnRefresh)
        Me.Guna2Panel2.Controls.Add(Me.btnDelete)
        Me.Guna2Panel2.Controls.Add(Me.btnEdit)
        Me.Guna2Panel2.Controls.Add(Me.Label2)
        Me.Guna2Panel2.Controls.Add(Me.Label1)
        Me.Guna2Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Guna2Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Guna2Panel2.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Guna2Panel2.Name = "Guna2Panel2"
        Me.Guna2Panel2.Size = New System.Drawing.Size(1405, 100)
        Me.Guna2Panel2.TabIndex = 3
        '
        'btnImportExcel
        '
        Me.btnImportExcel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnImportExcel.Animated = True
        Me.btnImportExcel.FillColor = System.Drawing.Color.Green
        Me.btnImportExcel.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.btnImportExcel.ForeColor = System.Drawing.Color.White
        Me.btnImportExcel.Image = Global.dashboard.My.Resources.Resources.Excel_Icon
        Me.btnImportExcel.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.btnImportExcel.ImageOffset = New System.Drawing.Point(-7, 0)
        Me.btnImportExcel.Location = New System.Drawing.Point(955, 60)
        Me.btnImportExcel.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnImportExcel.Name = "btnImportExcel"
        Me.btnImportExcel.Size = New System.Drawing.Size(131, 31)
        Me.btnImportExcel.TabIndex = 8
        Me.btnImportExcel.Text = "Import Excel"
        Me.btnImportExcel.TextOffset = New System.Drawing.Point(10, 0)
        '
        'btnRefresh
        '
        Me.btnRefresh.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnRefresh.Animated = True
        Me.btnRefresh.FillColor = System.Drawing.Color.DarkGoldenrod
        Me.btnRefresh.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.btnRefresh.ForeColor = System.Drawing.Color.White
        Me.btnRefresh.Location = New System.Drawing.Point(1094, 60)
        Me.btnRefresh.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(94, 31)
        Me.btnRefresh.TabIndex = 7
        Me.btnRefresh.Text = "Refresh"
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Animated = True
        Me.btnDelete.FillColor = System.Drawing.Color.Red
        Me.btnDelete.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.btnDelete.ForeColor = System.Drawing.Color.White
        Me.btnDelete.Location = New System.Drawing.Point(1296, 60)
        Me.btnDelete.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(94, 31)
        Me.btnDelete.TabIndex = 3
        Me.btnDelete.Text = "Delete"
        '
        'btnEdit
        '
        Me.btnEdit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnEdit.Animated = True
        Me.btnEdit.FillColor = System.Drawing.Color.Blue
        Me.btnEdit.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.btnEdit.ForeColor = System.Drawing.Color.White
        Me.btnEdit.Location = New System.Drawing.Point(1195, 60)
        Me.btnEdit.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnEdit.Name = "btnEdit"
        Me.btnEdit.Size = New System.Drawing.Size(94, 31)
        Me.btnEdit.TabIndex = 2
        Me.btnEdit.Text = "Edit"
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Gill Sans MT", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.Control
        Me.Label2.Location = New System.Drawing.Point(19, 65)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(237, 27)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Search for Registered Graves"
        '
        'DeceasedList
        '
        Me.DeceasedList.AllowColumnReorder = True
        Me.DeceasedList.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DeceasedList.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.DeceasedList.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.DeceasedList.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.number, Me.fullname, Me.birth, Me.death, Me.loc, Me.status})
        Me.DeceasedList.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DeceasedList.ForeColor = System.Drawing.SystemColors.Window
        Me.DeceasedList.FullRowSelect = True
        Me.DeceasedList.GridLines = True
        Me.DeceasedList.HideSelection = False
        Me.DeceasedList.Location = New System.Drawing.Point(15, 61)
        Me.DeceasedList.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.DeceasedList.Name = "DeceasedList"
        Me.DeceasedList.ShowItemToolTips = True
        Me.DeceasedList.Size = New System.Drawing.Size(1375, 366)
        Me.DeceasedList.TabIndex = 1
        Me.DeceasedList.TileSize = New System.Drawing.Size(10, 10)
        Me.DeceasedList.UseCompatibleStateImageBehavior = False
        Me.DeceasedList.View = System.Windows.Forms.View.Details
        '
        'number
        '
        Me.number.Text = "No."
        Me.number.Width = 40
        '
        'fullname
        '
        Me.fullname.Text = "Full Name (Last Name, First Name MI. Ext."
        Me.fullname.Width = 305
        '
        'birth
        '
        Me.birth.Text = "Birthdate"
        Me.birth.Width = 170
        '
        'death
        '
        Me.death.Text = "Deathdate"
        Me.death.Width = 170
        '
        'loc
        '
        Me.loc.Text = "Location"
        Me.loc.Width = 400
        '
        'status
        '
        Me.status.Text = "Status"
        Me.status.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.status.Width = 150
        '
        'Guna2Panel1
        '
        Me.Guna2Panel1.AutoSize = True
        Me.Guna2Panel1.Controls.Add(Me.chkbxBoneNiche)
        Me.Guna2Panel1.Controls.Add(Me.chkbxPrivate)
        Me.Guna2Panel1.Controls.Add(Me.chkbxLawnLots)
        Me.Guna2Panel1.Controls.Add(Me.chkbxApartment)
        Me.Guna2Panel1.Controls.Add(Me.txtSearch)
        Me.Guna2Panel1.Controls.Add(Me.DeceasedList)
        Me.Guna2Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Guna2Panel1.Location = New System.Drawing.Point(0, 100)
        Me.Guna2Panel1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Guna2Panel1.Name = "Guna2Panel1"
        Me.Guna2Panel1.Size = New System.Drawing.Size(1405, 445)
        Me.Guna2Panel1.TabIndex = 2
        '
        'chkbxBoneNiche
        '
        Me.chkbxBoneNiche.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkbxBoneNiche.AutoSize = True
        Me.chkbxBoneNiche.CheckedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.chkbxBoneNiche.CheckedState.BorderRadius = 0
        Me.chkbxBoneNiche.CheckedState.BorderThickness = 0
        Me.chkbxBoneNiche.CheckedState.FillColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.chkbxBoneNiche.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkbxBoneNiche.ForeColor = System.Drawing.SystemColors.Control
        Me.chkbxBoneNiche.Location = New System.Drawing.Point(1018, 24)
        Me.chkbxBoneNiche.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.chkbxBoneNiche.Name = "chkbxBoneNiche"
        Me.chkbxBoneNiche.Size = New System.Drawing.Size(145, 29)
        Me.chkbxBoneNiche.TabIndex = 8
        Me.chkbxBoneNiche.Text = "Bone Niche"
        Me.chkbxBoneNiche.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(125, Byte), Integer), CType(CType(137, Byte), Integer), CType(CType(149, Byte), Integer))
        Me.chkbxBoneNiche.UncheckedState.BorderRadius = 0
        Me.chkbxBoneNiche.UncheckedState.BorderThickness = 0
        Me.chkbxBoneNiche.UncheckedState.FillColor = System.Drawing.Color.FromArgb(CType(CType(125, Byte), Integer), CType(CType(137, Byte), Integer), CType(CType(149, Byte), Integer))
        '
        'chkbxPrivate
        '
        Me.chkbxPrivate.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkbxPrivate.AutoSize = True
        Me.chkbxPrivate.CheckedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.chkbxPrivate.CheckedState.BorderRadius = 0
        Me.chkbxPrivate.CheckedState.BorderThickness = 0
        Me.chkbxPrivate.CheckedState.FillColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.chkbxPrivate.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkbxPrivate.ForeColor = System.Drawing.SystemColors.Control
        Me.chkbxPrivate.Location = New System.Drawing.Point(1161, 24)
        Me.chkbxPrivate.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.chkbxPrivate.Name = "chkbxPrivate"
        Me.chkbxPrivate.Size = New System.Drawing.Size(101, 29)
        Me.chkbxPrivate.TabIndex = 7
        Me.chkbxPrivate.Text = "Private"
        Me.chkbxPrivate.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(125, Byte), Integer), CType(CType(137, Byte), Integer), CType(CType(149, Byte), Integer))
        Me.chkbxPrivate.UncheckedState.BorderRadius = 0
        Me.chkbxPrivate.UncheckedState.BorderThickness = 0
        Me.chkbxPrivate.UncheckedState.FillColor = System.Drawing.Color.FromArgb(CType(CType(125, Byte), Integer), CType(CType(137, Byte), Integer), CType(CType(149, Byte), Integer))
        '
        'chkbxLawnLots
        '
        Me.chkbxLawnLots.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkbxLawnLots.AutoSize = True
        Me.chkbxLawnLots.CheckedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.chkbxLawnLots.CheckedState.BorderRadius = 0
        Me.chkbxLawnLots.CheckedState.BorderThickness = 0
        Me.chkbxLawnLots.CheckedState.FillColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.chkbxLawnLots.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkbxLawnLots.ForeColor = System.Drawing.SystemColors.Control
        Me.chkbxLawnLots.Location = New System.Drawing.Point(1258, 24)
        Me.chkbxLawnLots.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.chkbxLawnLots.Name = "chkbxLawnLots"
        Me.chkbxLawnLots.Size = New System.Drawing.Size(132, 29)
        Me.chkbxLawnLots.TabIndex = 6
        Me.chkbxLawnLots.Text = "Lawn Lots"
        Me.chkbxLawnLots.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(125, Byte), Integer), CType(CType(137, Byte), Integer), CType(CType(149, Byte), Integer))
        Me.chkbxLawnLots.UncheckedState.BorderRadius = 0
        Me.chkbxLawnLots.UncheckedState.BorderThickness = 0
        Me.chkbxLawnLots.UncheckedState.FillColor = System.Drawing.Color.FromArgb(CType(CType(125, Byte), Integer), CType(CType(137, Byte), Integer), CType(CType(149, Byte), Integer))
        '
        'chkbxApartment
        '
        Me.chkbxApartment.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkbxApartment.AutoSize = True
        Me.chkbxApartment.CheckedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.chkbxApartment.CheckedState.BorderRadius = 0
        Me.chkbxApartment.CheckedState.BorderThickness = 0
        Me.chkbxApartment.CheckedState.FillColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.chkbxApartment.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkbxApartment.ForeColor = System.Drawing.SystemColors.Control
        Me.chkbxApartment.Location = New System.Drawing.Point(893, 24)
        Me.chkbxApartment.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.chkbxApartment.Name = "chkbxApartment"
        Me.chkbxApartment.Size = New System.Drawing.Size(133, 29)
        Me.chkbxApartment.TabIndex = 5
        Me.chkbxApartment.Text = "Apartment"
        Me.chkbxApartment.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(125, Byte), Integer), CType(CType(137, Byte), Integer), CType(CType(149, Byte), Integer))
        Me.chkbxApartment.UncheckedState.BorderRadius = 0
        Me.chkbxApartment.UncheckedState.BorderThickness = 0
        Me.chkbxApartment.UncheckedState.FillColor = System.Drawing.Color.FromArgb(CType(CType(125, Byte), Integer), CType(CType(137, Byte), Integer), CType(CType(149, Byte), Integer))
        '
        'txtSearch
        '
        Me.txtSearch.Animated = True
        Me.txtSearch.AutoRoundedCorners = True
        Me.txtSearch.BorderColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(171, Byte), Integer), CType(CType(189, Byte), Integer))
        Me.txtSearch.BorderRadius = 21
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
        Me.txtSearch.IconRight = Global.dashboard.My.Resources.Resources.search1
        Me.txtSearch.IconRightOffset = New System.Drawing.Point(8, 0)
        Me.txtSearch.IconRightSize = New System.Drawing.Size(25, 25)
        Me.txtSearch.Location = New System.Drawing.Point(15, 9)
        Me.txtSearch.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.PlaceholderForeColor = System.Drawing.Color.Gray
        Me.txtSearch.PlaceholderText = "Search a name"
        Me.txtSearch.SelectedText = ""
        Me.txtSearch.Size = New System.Drawing.Size(365, 44)
        Me.txtSearch.TabIndex = 4
        '
        'contextMenu
        '
        Me.contextMenu.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.contextMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.RemainingToolStripMenuItem, Me.RelocationToolStripMenuItem, Me.RenewalToolStripMenuItem, Me.PendingToolStripMenuItem})
        Me.contextMenu.Name = "contextMenu"
        Me.contextMenu.RenderStyle.ArrowColor = System.Drawing.Color.FromArgb(CType(CType(151, Byte), Integer), CType(CType(143, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.contextMenu.RenderStyle.BorderColor = System.Drawing.Color.Gainsboro
        Me.contextMenu.RenderStyle.ColorTable = Nothing
        Me.contextMenu.RenderStyle.RoundedEdges = True
        Me.contextMenu.RenderStyle.SelectionArrowColor = System.Drawing.Color.White
        Me.contextMenu.RenderStyle.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(100, Byte), Integer), CType(CType(88, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.contextMenu.RenderStyle.SelectionForeColor = System.Drawing.Color.White
        Me.contextMenu.RenderStyle.SeparatorColor = System.Drawing.Color.Gainsboro
        Me.contextMenu.RenderStyle.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault
        Me.contextMenu.Size = New System.Drawing.Size(150, 100)
        '
        'RemainingToolStripMenuItem
        '
        Me.RemainingToolStripMenuItem.Name = "RemainingToolStripMenuItem"
        Me.RemainingToolStripMenuItem.Size = New System.Drawing.Size(149, 24)
        Me.RemainingToolStripMenuItem.Text = "Remaining"
        '
        'RelocationToolStripMenuItem
        '
        Me.RelocationToolStripMenuItem.Name = "RelocationToolStripMenuItem"
        Me.RelocationToolStripMenuItem.Size = New System.Drawing.Size(149, 24)
        Me.RelocationToolStripMenuItem.Text = "Relocation"
        '
        'RenewalToolStripMenuItem
        '
        Me.RenewalToolStripMenuItem.Name = "RenewalToolStripMenuItem"
        Me.RenewalToolStripMenuItem.Size = New System.Drawing.Size(149, 24)
        Me.RenewalToolStripMenuItem.Text = "Renewal"
        '
        'PendingToolStripMenuItem
        '
        Me.PendingToolStripMenuItem.Name = "PendingToolStripMenuItem"
        Me.PendingToolStripMenuItem.Size = New System.Drawing.Size(149, 24)
        Me.PendingToolStripMenuItem.Text = "Pending"
        '
        'frmDeceasedReg
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(120.0!, 120.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoSize = True
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(14, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(90, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1405, 545)
        Me.Controls.Add(Me.Guna2Panel1)
        Me.Controls.Add(Me.Guna2Panel2)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Name = "frmDeceasedReg"
        Me.RightToLeftLayout = True
        Me.Text = "frmDeceasedReg"
        Me.Guna2Panel2.ResumeLayout(False)
        Me.Guna2Panel2.PerformLayout()
        Me.Guna2Panel1.ResumeLayout(False)
        Me.Guna2Panel1.PerformLayout()
        Me.contextMenu.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents Guna2Elipse1 As Guna.UI2.WinForms.Guna2Elipse
    Friend WithEvents Guna2Panel2 As Guna.UI2.WinForms.Guna2Panel
    Friend WithEvents Guna2Panel1 As Guna.UI2.WinForms.Guna2Panel
    Friend WithEvents DeceasedList As ListView
    Friend WithEvents fullname As ColumnHeader
    Friend WithEvents birth As ColumnHeader
    Friend WithEvents death As ColumnHeader
    Friend WithEvents loc As ColumnHeader
    Friend WithEvents Label2 As Label
    Friend WithEvents btnDelete As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents btnEdit As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents btnRefresh As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents txtSearch As Guna.UI2.WinForms.Guna2TextBox
    Friend WithEvents btnImportExcel As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents chkbxBoneNiche As Guna.UI2.WinForms.Guna2CheckBox
    Friend WithEvents chkbxPrivate As Guna.UI2.WinForms.Guna2CheckBox
    Friend WithEvents chkbxLawnLots As Guna.UI2.WinForms.Guna2CheckBox
    Friend WithEvents chkbxApartment As Guna.UI2.WinForms.Guna2CheckBox
    Friend WithEvents status As ColumnHeader
    Friend WithEvents contextMenu As Guna.UI2.WinForms.Guna2ContextMenuStrip
    Friend WithEvents RemainingToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents RelocationToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents RenewalToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents PendingToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents number As ColumnHeader
End Class
