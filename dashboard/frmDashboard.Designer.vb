﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmDashboard
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.Guna2Elipse1 = New Guna.UI2.WinForms.Guna2Elipse(Me.components)
        Me.Guna2Panel2 = New Guna.UI2.WinForms.Guna2Panel()
        Me.btnRefresh = New Guna.UI2.WinForms.Guna2Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.Guna2Panel3 = New Guna.UI2.WinForms.Guna2Panel()
        Me.TableLayoutPanel3 = New System.Windows.Forms.TableLayoutPanel()
        Me.pbApartment = New Guna.UI2.WinForms.Guna2CircleProgressBar()
        Me.lblApartment = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Guna2Panel4 = New Guna.UI2.WinForms.Guna2Panel()
        Me.TableLayoutPanel4 = New System.Windows.Forms.TableLayoutPanel()
        Me.pblawnlots = New Guna.UI2.WinForms.Guna2CircleProgressBar()
        Me.lblLawnlots = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Guna2Panel5 = New Guna.UI2.WinForms.Guna2Panel()
        Me.TableLayoutPanel5 = New System.Windows.Forms.TableLayoutPanel()
        Me.pbboneniche = New Guna.UI2.WinForms.Guna2CircleProgressBar()
        Me.lblBoneNiche = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Guna2Panel6 = New Guna.UI2.WinForms.Guna2Panel()
        Me.TableLayoutPanel6 = New System.Windows.Forms.TableLayoutPanel()
        Me.pbprivate = New Guna.UI2.WinForms.Guna2CircleProgressBar()
        Me.lblPrivate = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.dgvNotification = New Guna.UI2.WinForms.Guna2DataGridView()
        Me.id = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.fullName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.interment = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.expiry = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.plotLocation = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.status = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.daysExpired = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.Guna2Panel9 = New Guna.UI2.WinForms.Guna2Panel()
        Me.lblAvailable = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Guna2Panel8 = New Guna.UI2.WinForms.Guna2Panel()
        Me.lblTotalDeceased = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Guna2Panel7 = New Guna.UI2.WinForms.Guna2Panel()
        Me.lblTotalClients = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.tmDashboard = New System.Windows.Forms.Timer(Me.components)
        Me.Label4 = New System.Windows.Forms.Label()
        Me.pnlNotifications = New Guna.UI2.WinForms.Guna2CustomGradientPanel()
        Me.Guna2Panel2.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.Guna2Panel3.SuspendLayout()
        Me.TableLayoutPanel3.SuspendLayout()
        Me.Guna2Panel4.SuspendLayout()
        Me.TableLayoutPanel4.SuspendLayout()
        Me.Guna2Panel5.SuspendLayout()
        Me.TableLayoutPanel5.SuspendLayout()
        Me.Guna2Panel6.SuspendLayout()
        Me.TableLayoutPanel6.SuspendLayout()
        CType(Me.dgvNotification, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.Guna2Panel9.SuspendLayout()
        Me.Guna2Panel8.SuspendLayout()
        Me.Guna2Panel7.SuspendLayout()
        Me.SuspendLayout()
        '
        'Guna2Elipse1
        '
        Me.Guna2Elipse1.BorderRadius = 20
        Me.Guna2Elipse1.TargetControl = Me
        '
        'Guna2Panel2
        '
        Me.Guna2Panel2.Controls.Add(Me.btnRefresh)
        Me.Guna2Panel2.Controls.Add(Me.Label2)
        Me.Guna2Panel2.Controls.Add(Me.Label1)
        Me.Guna2Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Guna2Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Guna2Panel2.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Guna2Panel2.Name = "Guna2Panel2"
        Me.Guna2Panel2.Size = New System.Drawing.Size(1387, 98)
        Me.Guna2Panel2.TabIndex = 5
        '
        'btnRefresh
        '
        Me.btnRefresh.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnRefresh.Animated = True
        Me.btnRefresh.FillColor = System.Drawing.Color.Blue
        Me.btnRefresh.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.btnRefresh.ForeColor = System.Drawing.Color.White
        Me.btnRefresh.Location = New System.Drawing.Point(1271, 59)
        Me.btnRefresh.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(100, 31)
        Me.btnRefresh.TabIndex = 4
        Me.btnRefresh.Text = "Refresh"
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Italic)
        Me.Label2.ForeColor = System.Drawing.SystemColors.Control
        Me.Label2.Location = New System.Drawing.Point(20, 59)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(762, 24)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Displays key cemetery data, including burials, client records, and burial expiry " &
    "notifications."
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Roboto", 24.0!, System.Drawing.FontStyle.Bold)
        Me.Label1.ForeColor = System.Drawing.SystemColors.Control
        Me.Label1.Location = New System.Drawing.Point(16, 11)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(218, 53)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Dashboard"
        '
        'Label3
        '
        Me.Label3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!)
        Me.Label3.ForeColor = System.Drawing.SystemColors.Control
        Me.Label3.Location = New System.Drawing.Point(19, 116)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(265, 36)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "Statistics Overview"
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.BackColor = System.Drawing.Color.Transparent
        Me.TableLayoutPanel1.ColumnCount = 9
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 0.952381!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 23.80952!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 0.952381!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 23.80952!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 0.952381!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 23.80952!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 0.952381!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 23.80952!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 0.952381!))
        Me.TableLayoutPanel1.Controls.Add(Me.Guna2Panel3, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Guna2Panel4, 3, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Guna2Panel5, 5, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Guna2Panel6, 7, 0)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 310)
        Me.TableLayoutPanel1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(1387, 260)
        Me.TableLayoutPanel1.TabIndex = 6
        '
        'Guna2Panel3
        '
        Me.Guna2Panel3.BackColor = System.Drawing.Color.FromArgb(CType(CType(44, Byte), Integer), CType(CType(95, Byte), Integer), CType(CType(120, Byte), Integer))
        Me.Guna2Panel3.Controls.Add(Me.TableLayoutPanel3)
        Me.Guna2Panel3.Controls.Add(Me.lblApartment)
        Me.Guna2Panel3.Controls.Add(Me.Label5)
        Me.Guna2Panel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Guna2Panel3.Location = New System.Drawing.Point(17, 4)
        Me.Guna2Panel3.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Guna2Panel3.Name = "Guna2Panel3"
        Me.Guna2Panel3.ShadowDecoration.Depth = 5
        Me.Guna2Panel3.ShadowDecoration.Enabled = True
        Me.Guna2Panel3.ShadowDecoration.Shadow = New System.Windows.Forms.Padding(10)
        Me.Guna2Panel3.Size = New System.Drawing.Size(322, 252)
        Me.Guna2Panel3.TabIndex = 1
        '
        'TableLayoutPanel3
        '
        Me.TableLayoutPanel3.ColumnCount = 3
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel3.Controls.Add(Me.pbApartment, 1, 0)
        Me.TableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.TableLayoutPanel3.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel3.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.TableLayoutPanel3.Name = "TableLayoutPanel3"
        Me.TableLayoutPanel3.RowCount = 1
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel3.Size = New System.Drawing.Size(322, 176)
        Me.TableLayoutPanel3.TabIndex = 9
        '
        'pbApartment
        '
        Me.pbApartment.Animated = True
        Me.pbApartment.AnimationSpeed = 0!
        Me.pbApartment.BackColor = System.Drawing.Color.Transparent
        Me.pbApartment.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pbApartment.FillColor = System.Drawing.Color.FromArgb(CType(CType(200, Byte), Integer), CType(CType(213, Byte), Integer), CType(CType(218, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.pbApartment.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.pbApartment.ForeColor = System.Drawing.Color.White
        Me.pbApartment.Location = New System.Drawing.Point(84, 4)
        Me.pbApartment.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.pbApartment.Minimum = 0
        Me.pbApartment.Name = "pbApartment"
        Me.pbApartment.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle
        Me.pbApartment.ShowText = True
        Me.pbApartment.Size = New System.Drawing.Size(168, 168)
        Me.pbApartment.TabIndex = 8
        Me.pbApartment.Text = "Guna"
        '
        'lblApartment
        '
        Me.lblApartment.AutoSize = True
        Me.lblApartment.Font = New System.Drawing.Font("Roboto", 11.25!)
        Me.lblApartment.ForeColor = System.Drawing.SystemColors.Control
        Me.lblApartment.Location = New System.Drawing.Point(27, 210)
        Me.lblApartment.Margin = New System.Windows.Forms.Padding(27, 0, 4, 18)
        Me.lblApartment.Name = "lblApartment"
        Me.lblApartment.Size = New System.Drawing.Size(80, 25)
        Me.lblApartment.TabIndex = 2
        Me.lblApartment.Text = "Label10"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Roboto", 14.25!)
        Me.Label5.ForeColor = System.Drawing.SystemColors.Control
        Me.Label5.Location = New System.Drawing.Point(27, 170)
        Me.Label5.Margin = New System.Windows.Forms.Padding(27, 0, 4, 49)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(136, 32)
        Me.Label5.TabIndex = 1
        Me.Label5.Text = "Apartment:"
        '
        'Guna2Panel4
        '
        Me.Guna2Panel4.BackColor = System.Drawing.Color.FromArgb(CType(CType(44, Byte), Integer), CType(CType(95, Byte), Integer), CType(CType(120, Byte), Integer))
        Me.Guna2Panel4.Controls.Add(Me.TableLayoutPanel4)
        Me.Guna2Panel4.Controls.Add(Me.lblLawnlots)
        Me.Guna2Panel4.Controls.Add(Me.Label6)
        Me.Guna2Panel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Guna2Panel4.Location = New System.Drawing.Point(360, 4)
        Me.Guna2Panel4.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Guna2Panel4.Name = "Guna2Panel4"
        Me.Guna2Panel4.ShadowDecoration.Depth = 5
        Me.Guna2Panel4.ShadowDecoration.Enabled = True
        Me.Guna2Panel4.ShadowDecoration.Shadow = New System.Windows.Forms.Padding(10)
        Me.Guna2Panel4.Size = New System.Drawing.Size(322, 252)
        Me.Guna2Panel4.TabIndex = 2
        '
        'TableLayoutPanel4
        '
        Me.TableLayoutPanel4.ColumnCount = 3
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel4.Controls.Add(Me.pblawnlots, 1, 0)
        Me.TableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Top
        Me.TableLayoutPanel4.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel4.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.TableLayoutPanel4.Name = "TableLayoutPanel4"
        Me.TableLayoutPanel4.RowCount = 1
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel4.Size = New System.Drawing.Size(322, 176)
        Me.TableLayoutPanel4.TabIndex = 10
        '
        'pblawnlots
        '
        Me.pblawnlots.Animated = True
        Me.pblawnlots.AnimationSpeed = 0!
        Me.pblawnlots.BackColor = System.Drawing.Color.Transparent
        Me.pblawnlots.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pblawnlots.FillColor = System.Drawing.Color.FromArgb(CType(CType(200, Byte), Integer), CType(CType(213, Byte), Integer), CType(CType(218, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.pblawnlots.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.pblawnlots.ForeColor = System.Drawing.Color.White
        Me.pblawnlots.Location = New System.Drawing.Point(84, 4)
        Me.pblawnlots.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.pblawnlots.Minimum = 0
        Me.pblawnlots.Name = "pblawnlots"
        Me.pblawnlots.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle
        Me.pblawnlots.ShowText = True
        Me.pblawnlots.Size = New System.Drawing.Size(168, 168)
        Me.pblawnlots.TabIndex = 8
        Me.pblawnlots.Text = "Guna2CircleProgressBar3"
        '
        'lblLawnlots
        '
        Me.lblLawnlots.AutoSize = True
        Me.lblLawnlots.Font = New System.Drawing.Font("Roboto", 11.25!)
        Me.lblLawnlots.ForeColor = System.Drawing.SystemColors.Control
        Me.lblLawnlots.Location = New System.Drawing.Point(27, 210)
        Me.lblLawnlots.Margin = New System.Windows.Forms.Padding(27, 0, 4, 18)
        Me.lblLawnlots.Name = "lblLawnlots"
        Me.lblLawnlots.Size = New System.Drawing.Size(80, 25)
        Me.lblLawnlots.TabIndex = 3
        Me.lblLawnlots.Text = "Label11"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Roboto", 14.25!)
        Me.Label6.ForeColor = System.Drawing.SystemColors.Control
        Me.Label6.Location = New System.Drawing.Point(27, 170)
        Me.Label6.Margin = New System.Windows.Forms.Padding(27, 0, 4, 49)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(211, 32)
        Me.Label6.TabIndex = 2
        Me.Label6.Text = "Family Lawn Lots:"
        '
        'Guna2Panel5
        '
        Me.Guna2Panel5.BackColor = System.Drawing.Color.FromArgb(CType(CType(44, Byte), Integer), CType(CType(95, Byte), Integer), CType(CType(120, Byte), Integer))
        Me.Guna2Panel5.Controls.Add(Me.TableLayoutPanel5)
        Me.Guna2Panel5.Controls.Add(Me.lblBoneNiche)
        Me.Guna2Panel5.Controls.Add(Me.Label7)
        Me.Guna2Panel5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Guna2Panel5.Location = New System.Drawing.Point(703, 4)
        Me.Guna2Panel5.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Guna2Panel5.Name = "Guna2Panel5"
        Me.Guna2Panel5.ShadowDecoration.Depth = 5
        Me.Guna2Panel5.ShadowDecoration.Enabled = True
        Me.Guna2Panel5.ShadowDecoration.Shadow = New System.Windows.Forms.Padding(10)
        Me.Guna2Panel5.Size = New System.Drawing.Size(322, 252)
        Me.Guna2Panel5.TabIndex = 3
        '
        'TableLayoutPanel5
        '
        Me.TableLayoutPanel5.ColumnCount = 3
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel5.Controls.Add(Me.pbboneniche, 1, 0)
        Me.TableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Top
        Me.TableLayoutPanel5.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel5.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.TableLayoutPanel5.Name = "TableLayoutPanel5"
        Me.TableLayoutPanel5.RowCount = 1
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel5.Size = New System.Drawing.Size(322, 176)
        Me.TableLayoutPanel5.TabIndex = 10
        '
        'pbboneniche
        '
        Me.pbboneniche.Animated = True
        Me.pbboneniche.AnimationSpeed = 0!
        Me.pbboneniche.BackColor = System.Drawing.Color.Transparent
        Me.pbboneniche.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pbboneniche.FillColor = System.Drawing.Color.FromArgb(CType(CType(200, Byte), Integer), CType(CType(213, Byte), Integer), CType(CType(218, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.pbboneniche.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.pbboneniche.ForeColor = System.Drawing.Color.White
        Me.pbboneniche.Location = New System.Drawing.Point(84, 4)
        Me.pbboneniche.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.pbboneniche.Minimum = 0
        Me.pbboneniche.Name = "pbboneniche"
        Me.pbboneniche.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle
        Me.pbboneniche.ShowText = True
        Me.pbboneniche.Size = New System.Drawing.Size(168, 168)
        Me.pbboneniche.TabIndex = 8
        Me.pbboneniche.Text = "Guna2CircleProgressBar4"
        '
        'lblBoneNiche
        '
        Me.lblBoneNiche.AutoSize = True
        Me.lblBoneNiche.Font = New System.Drawing.Font("Roboto", 11.25!)
        Me.lblBoneNiche.ForeColor = System.Drawing.SystemColors.Control
        Me.lblBoneNiche.Location = New System.Drawing.Point(27, 210)
        Me.lblBoneNiche.Margin = New System.Windows.Forms.Padding(27, 0, 4, 18)
        Me.lblBoneNiche.Name = "lblBoneNiche"
        Me.lblBoneNiche.Size = New System.Drawing.Size(80, 25)
        Me.lblBoneNiche.TabIndex = 4
        Me.lblBoneNiche.Text = "Label12"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Roboto", 14.25!)
        Me.Label7.ForeColor = System.Drawing.SystemColors.Control
        Me.Label7.Location = New System.Drawing.Point(27, 170)
        Me.Label7.Margin = New System.Windows.Forms.Padding(27, 0, 4, 49)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(146, 32)
        Me.Label7.TabIndex = 3
        Me.Label7.Text = "Bone Niche:"
        '
        'Guna2Panel6
        '
        Me.Guna2Panel6.BackColor = System.Drawing.Color.FromArgb(CType(CType(44, Byte), Integer), CType(CType(95, Byte), Integer), CType(CType(120, Byte), Integer))
        Me.Guna2Panel6.Controls.Add(Me.TableLayoutPanel6)
        Me.Guna2Panel6.Controls.Add(Me.lblPrivate)
        Me.Guna2Panel6.Controls.Add(Me.Label8)
        Me.Guna2Panel6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Guna2Panel6.Location = New System.Drawing.Point(1046, 4)
        Me.Guna2Panel6.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Guna2Panel6.Name = "Guna2Panel6"
        Me.Guna2Panel6.ShadowDecoration.Depth = 5
        Me.Guna2Panel6.ShadowDecoration.Enabled = True
        Me.Guna2Panel6.ShadowDecoration.Shadow = New System.Windows.Forms.Padding(10)
        Me.Guna2Panel6.Size = New System.Drawing.Size(322, 252)
        Me.Guna2Panel6.TabIndex = 4
        '
        'TableLayoutPanel6
        '
        Me.TableLayoutPanel6.ColumnCount = 3
        Me.TableLayoutPanel6.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel6.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel6.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel6.Controls.Add(Me.pbprivate, 1, 0)
        Me.TableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Top
        Me.TableLayoutPanel6.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel6.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.TableLayoutPanel6.Name = "TableLayoutPanel6"
        Me.TableLayoutPanel6.RowCount = 1
        Me.TableLayoutPanel6.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel6.Size = New System.Drawing.Size(322, 176)
        Me.TableLayoutPanel6.TabIndex = 10
        '
        'pbprivate
        '
        Me.pbprivate.Animated = True
        Me.pbprivate.AnimationSpeed = 0!
        Me.pbprivate.BackColor = System.Drawing.Color.Transparent
        Me.pbprivate.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pbprivate.FillColor = System.Drawing.Color.FromArgb(CType(CType(200, Byte), Integer), CType(CType(213, Byte), Integer), CType(CType(218, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.pbprivate.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.pbprivate.ForeColor = System.Drawing.Color.White
        Me.pbprivate.Location = New System.Drawing.Point(84, 4)
        Me.pbprivate.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.pbprivate.Minimum = 0
        Me.pbprivate.Name = "pbprivate"
        Me.pbprivate.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle
        Me.pbprivate.ShowText = True
        Me.pbprivate.Size = New System.Drawing.Size(168, 168)
        Me.pbprivate.TabIndex = 8
        Me.pbprivate.Text = "Guna2CircleProgressBar5"
        '
        'lblPrivate
        '
        Me.lblPrivate.AutoSize = True
        Me.lblPrivate.Font = New System.Drawing.Font("Roboto", 11.25!)
        Me.lblPrivate.ForeColor = System.Drawing.SystemColors.Control
        Me.lblPrivate.Location = New System.Drawing.Point(27, 210)
        Me.lblPrivate.Margin = New System.Windows.Forms.Padding(27, 0, 4, 18)
        Me.lblPrivate.Name = "lblPrivate"
        Me.lblPrivate.Size = New System.Drawing.Size(80, 25)
        Me.lblPrivate.TabIndex = 5
        Me.lblPrivate.Text = "Label13"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Roboto", 14.25!)
        Me.Label8.ForeColor = System.Drawing.SystemColors.Control
        Me.Label8.Location = New System.Drawing.Point(27, 170)
        Me.Label8.Margin = New System.Windows.Forms.Padding(27, 0, 4, 49)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(96, 32)
        Me.Label8.TabIndex = 4
        Me.Label8.Text = "Private:"
        '
        'dgvNotification
        '
        Me.dgvNotification.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.White
        Me.dgvNotification.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvNotification.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(CType(CType(100, Byte), Integer), CType(CType(88, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.Color.White
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvNotification.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.dgvNotification.ColumnHeadersHeight = 15
        Me.dgvNotification.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing
        Me.dgvNotification.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.id, Me.fullName, Me.interment, Me.expiry, Me.plotLocation, Me.status, Me.daysExpired})
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.Color.White
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(71, Byte), Integer), CType(CType(69, Byte), Integer), CType(CType(94, Byte), Integer))
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(71, Byte), Integer), CType(CType(69, Byte), Integer), CType(CType(94, Byte), Integer))
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvNotification.DefaultCellStyle = DataGridViewCellStyle3
        Me.dgvNotification.GridColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.dgvNotification.Location = New System.Drawing.Point(19, 748)
        Me.dgvNotification.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.dgvNotification.Name = "dgvNotification"
        Me.dgvNotification.ReadOnly = True
        Me.dgvNotification.RowHeadersVisible = False
        Me.dgvNotification.RowHeadersWidth = 51
        Me.dgvNotification.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.dgvNotification.Size = New System.Drawing.Size(827, 182)
        Me.dgvNotification.TabIndex = 7
        Me.dgvNotification.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White
        Me.dgvNotification.ThemeStyle.AlternatingRowsStyle.Font = Nothing
        Me.dgvNotification.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty
        Me.dgvNotification.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty
        Me.dgvNotification.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty
        Me.dgvNotification.ThemeStyle.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.dgvNotification.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.dgvNotification.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(CType(CType(100, Byte), Integer), CType(CType(88, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.dgvNotification.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        Me.dgvNotification.ThemeStyle.HeaderStyle.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgvNotification.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White
        Me.dgvNotification.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing
        Me.dgvNotification.ThemeStyle.HeaderStyle.Height = 15
        Me.dgvNotification.ThemeStyle.ReadOnly = True
        Me.dgvNotification.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White
        Me.dgvNotification.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal
        Me.dgvNotification.ThemeStyle.RowsStyle.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgvNotification.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(71, Byte), Integer), CType(CType(69, Byte), Integer), CType(CType(94, Byte), Integer))
        Me.dgvNotification.ThemeStyle.RowsStyle.Height = 22
        Me.dgvNotification.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.dgvNotification.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(71, Byte), Integer), CType(CType(69, Byte), Integer), CType(CType(94, Byte), Integer))
        '
        'id
        '
        Me.id.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.id.HeaderText = "ID"
        Me.id.MinimumWidth = 6
        Me.id.Name = "id"
        Me.id.ReadOnly = True
        Me.id.Width = 30
        '
        'fullName
        '
        Me.fullName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.fullName.HeaderText = "Full Name"
        Me.fullName.MinimumWidth = 6
        Me.fullName.Name = "fullName"
        Me.fullName.ReadOnly = True
        Me.fullName.Width = 200
        '
        'interment
        '
        Me.interment.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.interment.HeaderText = "Interment"
        Me.interment.MinimumWidth = 6
        Me.interment.Name = "interment"
        Me.interment.ReadOnly = True
        Me.interment.Width = 120
        '
        'expiry
        '
        Me.expiry.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.expiry.HeaderText = "Expiring Date"
        Me.expiry.MinimumWidth = 6
        Me.expiry.Name = "expiry"
        Me.expiry.ReadOnly = True
        Me.expiry.Width = 120
        '
        'plotLocation
        '
        Me.plotLocation.HeaderText = "Plot Location"
        Me.plotLocation.MinimumWidth = 6
        Me.plotLocation.Name = "plotLocation"
        Me.plotLocation.ReadOnly = True
        '
        'status
        '
        Me.status.HeaderText = "Status"
        Me.status.MinimumWidth = 6
        Me.status.Name = "status"
        Me.status.ReadOnly = True
        '
        'daysExpired
        '
        Me.daysExpired.HeaderText = "Days Expired"
        Me.daysExpired.MinimumWidth = 6
        Me.daysExpired.Name = "daysExpired"
        Me.daysExpired.ReadOnly = True
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel2.ColumnCount = 7
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 0.8064516!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 32.25806!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 0.8064516!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 32.25806!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 0.8064516!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 32.25806!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 0.8064516!))
        Me.TableLayoutPanel2.Controls.Add(Me.Guna2Panel9, 5, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.Guna2Panel8, 3, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.Guna2Panel7, 1, 0)
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(0, 155)
        Me.TableLayoutPanel2.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 1
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(1387, 148)
        Me.TableLayoutPanel2.TabIndex = 8
        '
        'Guna2Panel9
        '
        Me.Guna2Panel9.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Guna2Panel9.BackColor = System.Drawing.Color.FromArgb(CType(CType(44, Byte), Integer), CType(CType(95, Byte), Integer), CType(CType(120, Byte), Integer))
        Me.Guna2Panel9.Controls.Add(Me.lblAvailable)
        Me.Guna2Panel9.Controls.Add(Me.Label11)
        Me.Guna2Panel9.Location = New System.Drawing.Point(931, 4)
        Me.Guna2Panel9.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Guna2Panel9.Name = "Guna2Panel9"
        Me.Guna2Panel9.Size = New System.Drawing.Size(439, 140)
        Me.Guna2Panel9.TabIndex = 2
        '
        'lblAvailable
        '
        Me.lblAvailable.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblAvailable.AutoSize = True
        Me.lblAvailable.Font = New System.Drawing.Font("Microsoft Sans Serif", 36.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAvailable.ForeColor = System.Drawing.SystemColors.Control
        Me.lblAvailable.Location = New System.Drawing.Point(27, 25)
        Me.lblAvailable.Margin = New System.Windows.Forms.Padding(27, 25, 40, 18)
        Me.lblAvailable.Name = "lblAvailable"
        Me.lblAvailable.Size = New System.Drawing.Size(63, 69)
        Me.lblAvailable.TabIndex = 11
        Me.lblAvailable.Text = "0"
        Me.lblAvailable.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label11
        '
        Me.Label11.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.Color.Transparent
        Me.Label11.Font = New System.Drawing.Font("Roboto", 14.25!, System.Drawing.FontStyle.Bold)
        Me.Label11.ForeColor = System.Drawing.SystemColors.Control
        Me.Label11.Location = New System.Drawing.Point(27, 92)
        Me.Label11.Margin = New System.Windows.Forms.Padding(27, 0, 27, 18)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(175, 32)
        Me.Label11.TabIndex = 1
        Me.Label11.Text = "Available Plots"
        '
        'Guna2Panel8
        '
        Me.Guna2Panel8.BackColor = System.Drawing.Color.FromArgb(CType(CType(44, Byte), Integer), CType(CType(95, Byte), Integer), CType(CType(120, Byte), Integer))
        Me.Guna2Panel8.Controls.Add(Me.lblTotalDeceased)
        Me.Guna2Panel8.Controls.Add(Me.Label10)
        Me.Guna2Panel8.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Guna2Panel8.Location = New System.Drawing.Point(473, 4)
        Me.Guna2Panel8.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Guna2Panel8.Name = "Guna2Panel8"
        Me.Guna2Panel8.Size = New System.Drawing.Size(439, 140)
        Me.Guna2Panel8.TabIndex = 2
        '
        'lblTotalDeceased
        '
        Me.lblTotalDeceased.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblTotalDeceased.AutoSize = True
        Me.lblTotalDeceased.Font = New System.Drawing.Font("Microsoft Sans Serif", 36.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotalDeceased.ForeColor = System.Drawing.SystemColors.Control
        Me.lblTotalDeceased.Location = New System.Drawing.Point(27, 25)
        Me.lblTotalDeceased.Margin = New System.Windows.Forms.Padding(27, 25, 40, 18)
        Me.lblTotalDeceased.Name = "lblTotalDeceased"
        Me.lblTotalDeceased.Size = New System.Drawing.Size(63, 69)
        Me.lblTotalDeceased.TabIndex = 11
        Me.lblTotalDeceased.Text = "0"
        Me.lblTotalDeceased.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label10
        '
        Me.Label10.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Font = New System.Drawing.Font("Roboto", 14.25!, System.Drawing.FontStyle.Bold)
        Me.Label10.ForeColor = System.Drawing.SystemColors.Control
        Me.Label10.Location = New System.Drawing.Point(27, 92)
        Me.Label10.Margin = New System.Windows.Forms.Padding(27, 0, 27, 18)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(181, 32)
        Me.Label10.TabIndex = 1
        Me.Label10.Text = "Total Deceased"
        '
        'Guna2Panel7
        '
        Me.Guna2Panel7.BackColor = System.Drawing.Color.FromArgb(CType(CType(44, Byte), Integer), CType(CType(95, Byte), Integer), CType(CType(120, Byte), Integer))
        Me.Guna2Panel7.Controls.Add(Me.lblTotalClients)
        Me.Guna2Panel7.Controls.Add(Me.Label9)
        Me.Guna2Panel7.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Guna2Panel7.Location = New System.Drawing.Point(15, 4)
        Me.Guna2Panel7.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Guna2Panel7.Name = "Guna2Panel7"
        Me.Guna2Panel7.Size = New System.Drawing.Size(439, 140)
        Me.Guna2Panel7.TabIndex = 0
        '
        'lblTotalClients
        '
        Me.lblTotalClients.AutoSize = True
        Me.lblTotalClients.Font = New System.Drawing.Font("Microsoft Sans Serif", 36.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotalClients.ForeColor = System.Drawing.SystemColors.Control
        Me.lblTotalClients.Location = New System.Drawing.Point(27, 25)
        Me.lblTotalClients.Margin = New System.Windows.Forms.Padding(27, 25, 40, 18)
        Me.lblTotalClients.Name = "lblTotalClients"
        Me.lblTotalClients.Size = New System.Drawing.Size(63, 69)
        Me.lblTotalClients.TabIndex = 10
        Me.lblTotalClients.Text = "0"
        Me.lblTotalClients.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label9
        '
        Me.Label9.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Font = New System.Drawing.Font("Roboto", 14.25!, System.Drawing.FontStyle.Bold)
        Me.Label9.ForeColor = System.Drawing.SystemColors.Control
        Me.Label9.Location = New System.Drawing.Point(24, 92)
        Me.Label9.Margin = New System.Windows.Forms.Padding(27, 0, 27, 18)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(149, 32)
        Me.Label9.TabIndex = 1
        Me.Label9.Text = "Total Clients"
        '
        'tmDashboard
        '
        Me.tmDashboard.Interval = 600
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 24.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.Red
        Me.Label4.Location = New System.Drawing.Point(17, 587)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(609, 46)
        Me.Label4.TabIndex = 5
        Me.Label4.Text = "GRAVE EXPIRATION NOTICE!"
        '
        'pnlNotifications
        '
        Me.pnlNotifications.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.pnlNotifications.Location = New System.Drawing.Point(19, 636)
        Me.pnlNotifications.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.pnlNotifications.Name = "pnlNotifications"
        Me.pnlNotifications.Size = New System.Drawing.Size(827, 106)
        Me.pnlNotifications.TabIndex = 9
        '
        'frmDashboard
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(14, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(90, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1387, 788)
        Me.Controls.Add(Me.pnlNotifications)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.TableLayoutPanel2)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Guna2Panel2)
        Me.Controls.Add(Me.dgvNotification)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Name = "frmDashboard"
        Me.Text = "frmHome"
        Me.Guna2Panel2.ResumeLayout(False)
        Me.Guna2Panel2.PerformLayout()
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.Guna2Panel3.ResumeLayout(False)
        Me.Guna2Panel3.PerformLayout()
        Me.TableLayoutPanel3.ResumeLayout(False)
        Me.Guna2Panel4.ResumeLayout(False)
        Me.Guna2Panel4.PerformLayout()
        Me.TableLayoutPanel4.ResumeLayout(False)
        Me.Guna2Panel5.ResumeLayout(False)
        Me.Guna2Panel5.PerformLayout()
        Me.TableLayoutPanel5.ResumeLayout(False)
        Me.Guna2Panel6.ResumeLayout(False)
        Me.Guna2Panel6.PerformLayout()
        Me.TableLayoutPanel6.ResumeLayout(False)
        CType(Me.dgvNotification, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.Guna2Panel9.ResumeLayout(False)
        Me.Guna2Panel9.PerformLayout()
        Me.Guna2Panel8.ResumeLayout(False)
        Me.Guna2Panel8.PerformLayout()
        Me.Guna2Panel7.ResumeLayout(False)
        Me.Guna2Panel7.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Guna2Elipse1 As Guna.UI2.WinForms.Guna2Elipse
    Friend WithEvents Guna2Panel2 As Guna.UI2.WinForms.Guna2Panel
    Friend WithEvents btnRefresh As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents Guna2Panel3 As Guna.UI2.WinForms.Guna2Panel
    Friend WithEvents Guna2Panel4 As Guna.UI2.WinForms.Guna2Panel
    Friend WithEvents Guna2Panel5 As Guna.UI2.WinForms.Guna2Panel
    Friend WithEvents Guna2Panel6 As Guna.UI2.WinForms.Guna2Panel
    Friend WithEvents Label5 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents lblApartment As Label
    Friend WithEvents lblLawnlots As Label
    Friend WithEvents lblBoneNiche As Label
    Friend WithEvents lblPrivate As Label
    Friend WithEvents pbApartment As Guna.UI2.WinForms.Guna2CircleProgressBar
    Friend WithEvents pblawnlots As Guna.UI2.WinForms.Guna2CircleProgressBar
    Friend WithEvents pbboneniche As Guna.UI2.WinForms.Guna2CircleProgressBar
    Friend WithEvents pbprivate As Guna.UI2.WinForms.Guna2CircleProgressBar
    Friend WithEvents dgvNotification As Guna.UI2.WinForms.Guna2DataGridView
    Friend WithEvents TableLayoutPanel2 As TableLayoutPanel
    Friend WithEvents Guna2Panel7 As Guna.UI2.WinForms.Guna2Panel
    Friend WithEvents Label9 As Label
    Friend WithEvents Guna2Panel9 As Guna.UI2.WinForms.Guna2Panel
    Friend WithEvents lblAvailable As Label
    Friend WithEvents Label11 As Label
    Friend WithEvents Guna2Panel8 As Guna.UI2.WinForms.Guna2Panel
    Friend WithEvents lblTotalDeceased As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents lblTotalClients As Label
    Friend WithEvents tmDashboard As Timer
    Friend WithEvents Label4 As Label
    Friend WithEvents pnlNotifications As Guna.UI2.WinForms.Guna2CustomGradientPanel
    Friend WithEvents cmdFilt As Guna.UI2.WinForms.Guna2ComboBox
    Friend WithEvents id As DataGridViewTextBoxColumn
    Friend WithEvents fullName As DataGridViewTextBoxColumn
    Friend WithEvents interment As DataGridViewTextBoxColumn
    Friend WithEvents expiry As DataGridViewTextBoxColumn
    Friend WithEvents plotLocation As DataGridViewTextBoxColumn
    Friend WithEvents status As DataGridViewTextBoxColumn
    Friend WithEvents daysExpired As DataGridViewTextBoxColumn
    Friend WithEvents TableLayoutPanel3 As TableLayoutPanel
    Friend WithEvents TableLayoutPanel4 As TableLayoutPanel
    Friend WithEvents TableLayoutPanel5 As TableLayoutPanel
    Friend WithEvents TableLayoutPanel6 As TableLayoutPanel
End Class
