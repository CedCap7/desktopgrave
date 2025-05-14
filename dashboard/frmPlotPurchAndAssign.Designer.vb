<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPlotPurchAndAssign
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPlotPurchAndAssign))
        Me.panel1 = New System.Windows.Forms.Panel()
        Me.subSidePanel = New Guna.UI2.WinForms.Guna2Panel()
        Me.Guna2Panel4 = New Guna.UI2.WinForms.Guna2Panel()
        Me.Guna2ShadowPanel2 = New Guna.UI2.WinForms.Guna2ShadowPanel()
        Me.lblPaidAmount = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtPackage = New System.Windows.Forms.Label()
        Me.txtGraveType = New System.Windows.Forms.Label()
        Me.txtTotal = New System.Windows.Forms.Label()
        Me.lblQuantity = New System.Windows.Forms.Label()
        Me.currentQuantity = New System.Windows.Forms.NumericUpDown()
        Me.txtPrice = New System.Windows.Forms.Label()
        Me.Guna2ShadowPanel1 = New Guna.UI2.WinForms.Guna2ShadowPanel()
        Me.txtPaidAmount = New Guna.UI2.WinForms.Guna2TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.lstClientSuggestions = New System.Windows.Forms.ListBox()
        Me.txtClientSearch = New Guna.UI2.WinForms.Guna2TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.GraveType = New Guna.UI2.WinForms.Guna2ComboBox()
        Me.btnConfirm = New Guna.UI2.WinForms.Guna2Button()
        Me.btnSelectPlot = New Guna.UI2.WinForms.Guna2Button()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.lblPlotLocation = New System.Windows.Forms.Label()
        Me.label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cmbDeceased = New Guna.UI2.WinForms.Guna2ComboBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Guna2Panel2 = New Guna.UI2.WinForms.Guna2Panel()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.panel1.SuspendLayout()
        Me.Guna2Panel4.SuspendLayout()
        Me.Guna2ShadowPanel2.SuspendLayout()
        CType(Me.currentQuantity, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Guna2ShadowPanel1.SuspendLayout()
        Me.Guna2Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'panel1
        '
        Me.panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(23, Byte), Integer), CType(CType(107, Byte), Integer), CType(CType(135, Byte), Integer))
        Me.panel1.Controls.Add(Me.subSidePanel)
        Me.panel1.Controls.Add(Me.Guna2Panel4)
        Me.panel1.Controls.Add(Me.Label3)
        Me.panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.panel1.Location = New System.Drawing.Point(0, 71)
        Me.panel1.Margin = New System.Windows.Forms.Padding(2)
        Me.panel1.Name = "panel1"
        Me.panel1.Size = New System.Drawing.Size(1220, 678)
        Me.panel1.TabIndex = 5
        '
        'subSidePanel
        '
        Me.subSidePanel.AutoSize = True
        Me.subSidePanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(14, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(90, Byte), Integer))
        Me.subSidePanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.subSidePanel.Location = New System.Drawing.Point(400, 0)
        Me.subSidePanel.Name = "subSidePanel"
        Me.subSidePanel.Size = New System.Drawing.Size(820, 678)
        Me.subSidePanel.TabIndex = 61
        '
        'Guna2Panel4
        '
        Me.Guna2Panel4.Controls.Add(Me.Guna2ShadowPanel2)
        Me.Guna2Panel4.Controls.Add(Me.Guna2ShadowPanel1)
        Me.Guna2Panel4.Dock = System.Windows.Forms.DockStyle.Left
        Me.Guna2Panel4.Location = New System.Drawing.Point(0, 0)
        Me.Guna2Panel4.Name = "Guna2Panel4"
        Me.Guna2Panel4.Size = New System.Drawing.Size(400, 678)
        Me.Guna2Panel4.TabIndex = 30
        '
        'Guna2ShadowPanel2
        '
        Me.Guna2ShadowPanel2.BackColor = System.Drawing.Color.Transparent
        Me.Guna2ShadowPanel2.Controls.Add(Me.lblPaidAmount)
        Me.Guna2ShadowPanel2.Controls.Add(Me.Label10)
        Me.Guna2ShadowPanel2.Controls.Add(Me.Label4)
        Me.Guna2ShadowPanel2.Controls.Add(Me.txtPackage)
        Me.Guna2ShadowPanel2.Controls.Add(Me.txtGraveType)
        Me.Guna2ShadowPanel2.Controls.Add(Me.txtTotal)
        Me.Guna2ShadowPanel2.Controls.Add(Me.lblQuantity)
        Me.Guna2ShadowPanel2.Controls.Add(Me.currentQuantity)
        Me.Guna2ShadowPanel2.Controls.Add(Me.txtPrice)
        Me.Guna2ShadowPanel2.FillColor = System.Drawing.Color.FromArgb(CType(CType(14, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(90, Byte), Integer))
        Me.Guna2ShadowPanel2.Location = New System.Drawing.Point(18, 472)
        Me.Guna2ShadowPanel2.Name = "Guna2ShadowPanel2"
        Me.Guna2ShadowPanel2.Radius = 10
        Me.Guna2ShadowPanel2.ShadowColor = System.Drawing.Color.Black
        Me.Guna2ShadowPanel2.Size = New System.Drawing.Size(365, 203)
        Me.Guna2ShadowPanel2.TabIndex = 0
        '
        'lblPaidAmount
        '
        Me.lblPaidAmount.AutoSize = True
        Me.lblPaidAmount.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPaidAmount.ForeColor = System.Drawing.Color.White
        Me.lblPaidAmount.Location = New System.Drawing.Point(15, 89)
        Me.lblPaidAmount.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblPaidAmount.Name = "lblPaidAmount"
        Me.lblPaidAmount.Size = New System.Drawing.Size(92, 17)
        Me.lblPaidAmount.TabIndex = 27
        Me.lblPaidAmount.Text = "Paid Amount:"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 21.75!, System.Drawing.FontStyle.Bold)
        Me.Label10.ForeColor = System.Drawing.Color.White
        Me.Label10.Location = New System.Drawing.Point(12, 130)
        Me.Label10.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(103, 33)
        Me.Label10.TabIndex = 26
        Me.Label10.Text = "Total: "
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.White
        Me.Label4.Location = New System.Drawing.Point(120, 12)
        Me.Label4.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(136, 31)
        Me.Label4.TabIndex = 25
        Me.Label4.Text = "Summary"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'txtPackage
        '
        Me.txtPackage.AutoSize = True
        Me.txtPackage.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPackage.ForeColor = System.Drawing.Color.White
        Me.txtPackage.Location = New System.Drawing.Point(15, 55)
        Me.txtPackage.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.txtPackage.Name = "txtPackage"
        Me.txtPackage.Size = New System.Drawing.Size(67, 17)
        Me.txtPackage.TabIndex = 18
        Me.txtPackage.Text = "Package:"
        '
        'txtGraveType
        '
        Me.txtGraveType.AutoSize = True
        Me.txtGraveType.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGraveType.ForeColor = System.Drawing.Color.White
        Me.txtGraveType.Location = New System.Drawing.Point(15, 38)
        Me.txtGraveType.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.txtGraveType.Name = "txtGraveType"
        Me.txtGraveType.Size = New System.Drawing.Size(91, 17)
        Me.txtGraveType.TabIndex = 17
        Me.txtGraveType.Text = "Grave Type: "
        '
        'txtTotal
        '
        Me.txtTotal.AutoSize = True
        Me.txtTotal.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold)
        Me.txtTotal.ForeColor = System.Drawing.Color.White
        Me.txtTotal.Location = New System.Drawing.Point(13, 163)
        Me.txtTotal.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.txtTotal.Name = "txtTotal"
        Me.txtTotal.Size = New System.Drawing.Size(79, 25)
        Me.txtTotal.TabIndex = 20
        Me.txtTotal.Text = "Total: "
        '
        'lblQuantity
        '
        Me.lblQuantity.AutoSize = True
        Me.lblQuantity.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblQuantity.ForeColor = System.Drawing.Color.White
        Me.lblQuantity.Location = New System.Drawing.Point(15, 106)
        Me.lblQuantity.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblQuantity.Name = "lblQuantity"
        Me.lblQuantity.Size = New System.Drawing.Size(65, 17)
        Me.lblQuantity.TabIndex = 21
        Me.lblQuantity.Text = "Quantity:"
        '
        'currentQuantity
        '
        Me.currentQuantity.Location = New System.Drawing.Point(85, 107)
        Me.currentQuantity.Name = "currentQuantity"
        Me.currentQuantity.Size = New System.Drawing.Size(46, 20)
        Me.currentQuantity.TabIndex = 22
        Me.currentQuantity.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.currentQuantity.Visible = False
        '
        'txtPrice
        '
        Me.txtPrice.AutoSize = True
        Me.txtPrice.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPrice.ForeColor = System.Drawing.Color.White
        Me.txtPrice.Location = New System.Drawing.Point(15, 72)
        Me.txtPrice.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.txtPrice.Name = "txtPrice"
        Me.txtPrice.Size = New System.Drawing.Size(44, 17)
        Me.txtPrice.TabIndex = 19
        Me.txtPrice.Text = "Price:"
        '
        'Guna2ShadowPanel1
        '
        Me.Guna2ShadowPanel1.BackColor = System.Drawing.Color.Transparent
        Me.Guna2ShadowPanel1.Controls.Add(Me.txtPaidAmount)
        Me.Guna2ShadowPanel1.Controls.Add(Me.Label11)
        Me.Guna2ShadowPanel1.Controls.Add(Me.lstClientSuggestions)
        Me.Guna2ShadowPanel1.Controls.Add(Me.txtClientSearch)
        Me.Guna2ShadowPanel1.Controls.Add(Me.Label9)
        Me.Guna2ShadowPanel1.Controls.Add(Me.GraveType)
        Me.Guna2ShadowPanel1.Controls.Add(Me.btnConfirm)
        Me.Guna2ShadowPanel1.Controls.Add(Me.btnSelectPlot)
        Me.Guna2ShadowPanel1.Controls.Add(Me.Label8)
        Me.Guna2ShadowPanel1.Controls.Add(Me.lblPlotLocation)
        Me.Guna2ShadowPanel1.Controls.Add(Me.label1)
        Me.Guna2ShadowPanel1.Controls.Add(Me.Label2)
        Me.Guna2ShadowPanel1.Controls.Add(Me.cmbDeceased)
        Me.Guna2ShadowPanel1.Controls.Add(Me.Label7)
        Me.Guna2ShadowPanel1.FillColor = System.Drawing.Color.FromArgb(CType(CType(14, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(90, Byte), Integer))
        Me.Guna2ShadowPanel1.Location = New System.Drawing.Point(18, 33)
        Me.Guna2ShadowPanel1.Name = "Guna2ShadowPanel1"
        Me.Guna2ShadowPanel1.Radius = 10
        Me.Guna2ShadowPanel1.ShadowColor = System.Drawing.Color.Black
        Me.Guna2ShadowPanel1.Size = New System.Drawing.Size(365, 433)
        Me.Guna2ShadowPanel1.TabIndex = 0
        '
        'txtPaidAmount
        '
        Me.txtPaidAmount.Animated = True
        Me.txtPaidAmount.BorderColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(171, Byte), Integer), CType(CType(189, Byte), Integer))
        Me.txtPaidAmount.BorderRadius = 5
        Me.txtPaidAmount.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPaidAmount.DefaultText = ""
        Me.txtPaidAmount.DisabledState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.txtPaidAmount.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer))
        Me.txtPaidAmount.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.txtPaidAmount.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.txtPaidAmount.FocusedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtPaidAmount.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.txtPaidAmount.ForeColor = System.Drawing.Color.Black
        Me.txtPaidAmount.HoverState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtPaidAmount.Location = New System.Drawing.Point(18, 257)
        Me.txtPaidAmount.Margin = New System.Windows.Forms.Padding(4)
        Me.txtPaidAmount.Name = "txtPaidAmount"
        Me.txtPaidAmount.PlaceholderForeColor = System.Drawing.Color.Gray
        Me.txtPaidAmount.PlaceholderText = "Input Amount"
        Me.txtPaidAmount.SelectedText = ""
        Me.txtPaidAmount.Size = New System.Drawing.Size(334, 36)
        Me.txtPaidAmount.TabIndex = 65
        Me.txtPaidAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.Color.Transparent
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.Color.White
        Me.Label11.Location = New System.Drawing.Point(15, 236)
        Me.Label11.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(88, 17)
        Me.Label11.TabIndex = 64
        Me.Label11.Text = "Amount Paid"
        '
        'lstClientSuggestions
        '
        Me.lstClientSuggestions.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstClientSuggestions.FormattingEnabled = True
        Me.lstClientSuggestions.ItemHeight = 20
        Me.lstClientSuggestions.Location = New System.Drawing.Point(18, 117)
        Me.lstClientSuggestions.Name = "lstClientSuggestions"
        Me.lstClientSuggestions.Size = New System.Drawing.Size(334, 64)
        Me.lstClientSuggestions.TabIndex = 63
        Me.lstClientSuggestions.Visible = False
        '
        'txtClientSearch
        '
        Me.txtClientSearch.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtClientSearch.AutoSize = True
        Me.txtClientSearch.BorderColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(171, Byte), Integer), CType(CType(189, Byte), Integer))
        Me.txtClientSearch.BorderRadius = 5
        Me.txtClientSearch.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtClientSearch.DefaultText = ""
        Me.txtClientSearch.DisabledState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.txtClientSearch.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer))
        Me.txtClientSearch.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.txtClientSearch.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.txtClientSearch.FocusedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtClientSearch.Font = New System.Drawing.Font("Segoe UI", 11.25!)
        Me.txtClientSearch.ForeColor = System.Drawing.Color.Black
        Me.txtClientSearch.HoverState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtClientSearch.Location = New System.Drawing.Point(18, 80)
        Me.txtClientSearch.Margin = New System.Windows.Forms.Padding(4)
        Me.txtClientSearch.Name = "txtClientSearch"
        Me.txtClientSearch.PlaceholderForeColor = System.Drawing.Color.Gray
        Me.txtClientSearch.PlaceholderText = "Select client or type to search"
        Me.txtClientSearch.SelectedText = ""
        Me.txtClientSearch.Size = New System.Drawing.Size(334, 37)
        Me.txtClientSearch.TabIndex = 62
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Roboto", 21.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.White
        Me.Label9.Location = New System.Drawing.Point(72, 11)
        Me.Label9.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(224, 36)
        Me.Label9.TabIndex = 28
        Me.Label9.Text = "Grave Purchase"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'GraveType
        '
        Me.GraveType.AccessibleDescription = ""
        Me.GraveType.AccessibleName = ""
        Me.GraveType.BackColor = System.Drawing.Color.Transparent
        Me.GraveType.BorderColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(171, Byte), Integer), CType(CType(189, Byte), Integer))
        Me.GraveType.BorderRadius = 5
        Me.GraveType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.GraveType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.GraveType.FocusedColor = System.Drawing.Color.Empty
        Me.GraveType.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GraveType.ForeColor = System.Drawing.Color.Black
        Me.GraveType.FormattingEnabled = True
        Me.GraveType.ItemHeight = 30
        Me.GraveType.Location = New System.Drawing.Point(18, 139)
        Me.GraveType.Margin = New System.Windows.Forms.Padding(2)
        Me.GraveType.Name = "GraveType"
        Me.GraveType.Size = New System.Drawing.Size(334, 36)
        Me.GraveType.TabIndex = 6
        Me.GraveType.Tag = ""
        '
        'btnConfirm
        '
        Me.btnConfirm.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.btnConfirm.AutoRoundedCorners = True
        Me.btnConfirm.BackColor = System.Drawing.Color.FromArgb(CType(CType(14, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(90, Byte), Integer))
        Me.btnConfirm.BorderRadius = 17
        Me.btnConfirm.BorderThickness = 1
        Me.btnConfirm.FillColor = System.Drawing.Color.LimeGreen
        Me.btnConfirm.Font = New System.Drawing.Font("Roboto", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnConfirm.ForeColor = System.Drawing.Color.White
        Me.btnConfirm.Location = New System.Drawing.Point(68, 376)
        Me.btnConfirm.Margin = New System.Windows.Forms.Padding(2)
        Me.btnConfirm.Name = "btnConfirm"
        Me.btnConfirm.Size = New System.Drawing.Size(238, 37)
        Me.btnConfirm.TabIndex = 2
        Me.btnConfirm.Text = "Confirm"
        '
        'btnSelectPlot
        '
        Me.btnSelectPlot.Animated = True
        Me.btnSelectPlot.BackColor = System.Drawing.Color.FromArgb(CType(CType(14, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(90, Byte), Integer))
        Me.btnSelectPlot.BorderRadius = 5
        Me.btnSelectPlot.BorderThickness = 1
        Me.btnSelectPlot.FillColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(171, Byte), Integer), CType(CType(189, Byte), Integer))
        Me.btnSelectPlot.Font = New System.Drawing.Font("Roboto", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSelectPlot.ForeColor = System.Drawing.SystemColors.Window
        Me.btnSelectPlot.HoverState.BorderColor = System.Drawing.SystemColors.Control
        Me.btnSelectPlot.HoverState.FillColor = System.Drawing.Color.Black
        Me.btnSelectPlot.Image = Global.dashboard.My.Resources.Resources.map
        Me.btnSelectPlot.Location = New System.Drawing.Point(18, 299)
        Me.btnSelectPlot.Margin = New System.Windows.Forms.Padding(2)
        Me.btnSelectPlot.Name = "btnSelectPlot"
        Me.btnSelectPlot.Size = New System.Drawing.Size(334, 36)
        Me.btnSelectPlot.TabIndex = 23
        Me.btnSelectPlot.Text = "Select Plot"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.White
        Me.Label8.Location = New System.Drawing.Point(15, 177)
        Me.Label8.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(72, 17)
        Me.Label8.TabIndex = 27
        Me.Label8.Text = "Deceased"
        '
        'lblPlotLocation
        '
        Me.lblPlotLocation.AutoSize = True
        Me.lblPlotLocation.Font = New System.Drawing.Font("Roboto", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPlotLocation.ForeColor = System.Drawing.Color.White
        Me.lblPlotLocation.Location = New System.Drawing.Point(15, 355)
        Me.lblPlotLocation.Name = "lblPlotLocation"
        Me.lblPlotLocation.Size = New System.Drawing.Size(53, 19)
        Me.lblPlotLocation.TabIndex = 24
        Me.lblPlotLocation.Text = "Label7"
        Me.lblPlotLocation.Visible = False
        '
        'label1
        '
        Me.label1.AutoSize = True
        Me.label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label1.ForeColor = System.Drawing.Color.White
        Me.label1.Location = New System.Drawing.Point(15, 120)
        Me.label1.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.label1.Name = "label1"
        Me.label1.Size = New System.Drawing.Size(291, 17)
        Me.label1.TabIndex = 5
        Me.label1.Text = "Burial Type (Apartment, Private, Maoseleum)"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.White
        Me.Label2.Location = New System.Drawing.Point(15, 59)
        Me.Label2.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(84, 17)
        Me.Label2.TabIndex = 15
        Me.Label2.Text = "Client Name"
        '
        'cmbDeceased
        '
        Me.cmbDeceased.BackColor = System.Drawing.Color.Transparent
        Me.cmbDeceased.BorderColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(171, Byte), Integer), CType(CType(189, Byte), Integer))
        Me.cmbDeceased.BorderRadius = 5
        Me.cmbDeceased.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cmbDeceased.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbDeceased.FocusedColor = System.Drawing.Color.Empty
        Me.cmbDeceased.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.cmbDeceased.ForeColor = System.Drawing.Color.Black
        Me.cmbDeceased.FormattingEnabled = True
        Me.cmbDeceased.ItemHeight = 30
        Me.cmbDeceased.Location = New System.Drawing.Point(18, 196)
        Me.cmbDeceased.Margin = New System.Windows.Forms.Padding(2)
        Me.cmbDeceased.Name = "cmbDeceased"
        Me.cmbDeceased.Size = New System.Drawing.Size(334, 36)
        Me.cmbDeceased.TabIndex = 26
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Roboto", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.White
        Me.Label7.Location = New System.Drawing.Point(15, 337)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(109, 20)
        Me.Label7.TabIndex = 25
        Me.Label7.Text = "Selected Plot:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.White
        Me.Label3.Location = New System.Drawing.Point(85, 92)
        Me.Label3.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(0, 17)
        Me.Label3.TabIndex = 16
        '
        'Guna2Panel2
        '
        Me.Guna2Panel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(23, Byte), Integer), CType(CType(107, Byte), Integer), CType(CType(135, Byte), Integer))
        Me.Guna2Panel2.Controls.Add(Me.Label5)
        Me.Guna2Panel2.Controls.Add(Me.Label6)
        Me.Guna2Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Guna2Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Guna2Panel2.Name = "Guna2Panel2"
        Me.Guna2Panel2.Size = New System.Drawing.Size(1220, 71)
        Me.Guna2Panel2.TabIndex = 57
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Roboto", 11.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.Control
        Me.Label5.Location = New System.Drawing.Point(15, 43)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(333, 19)
        Me.Label5.TabIndex = 1
        Me.Label5.Text = "Select Grave Type, Plot, and Package Information"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Roboto", 24.0!, System.Drawing.FontStyle.Bold)
        Me.Label6.ForeColor = System.Drawing.SystemColors.Control
        Me.Label6.Location = New System.Drawing.Point(12, 4)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(635, 39)
        Me.Label6.TabIndex = 0
        Me.Label6.Text = "Plot Purchase and Burial Assignment Form"
        '
        'frmPlotPurchAndAssign
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSize = True
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(23, Byte), Integer), CType(CType(107, Byte), Integer), CType(CType(135, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1220, 749)
        Me.Controls.Add(Me.panel1)
        Me.Controls.Add(Me.Guna2Panel2)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(2)
        Me.Name = "frmPlotPurchAndAssign"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Plot Purchase and Assignment Form"
        Me.panel1.ResumeLayout(False)
        Me.panel1.PerformLayout()
        Me.Guna2Panel4.ResumeLayout(False)
        Me.Guna2ShadowPanel2.ResumeLayout(False)
        Me.Guna2ShadowPanel2.PerformLayout()
        CType(Me.currentQuantity, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Guna2ShadowPanel1.ResumeLayout(False)
        Me.Guna2ShadowPanel1.PerformLayout()
        Me.Guna2Panel2.ResumeLayout(False)
        Me.Guna2Panel2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents panel1 As Panel
    Private WithEvents GraveType As Guna.UI2.WinForms.Guna2ComboBox
    Private WithEvents label1 As Label
    Private WithEvents btnConfirm As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents Guna2Panel2 As Guna.UI2.WinForms.Guna2Panel
    Friend WithEvents Label5 As Label
    Friend WithEvents Label6 As Label
    Private WithEvents Label2 As Label
    Private WithEvents Label3 As Label
    Private WithEvents txtPrice As Label
    Private WithEvents txtPackage As Label
    Private WithEvents txtGraveType As Label
    Private WithEvents txtTotal As Label
    Private WithEvents lblQuantity As Label
    Friend WithEvents currentQuantity As NumericUpDown
    Private WithEvents btnSelectPlot As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents lblPlotLocation As Label
    Friend WithEvents Label7 As Label
    Private WithEvents Label8 As Label
    Private WithEvents cmbDeceased As Guna.UI2.WinForms.Guna2ComboBox
    Friend WithEvents Guna2Panel4 As Guna.UI2.WinForms.Guna2Panel
    Friend WithEvents subSidePanel As Guna.UI2.WinForms.Guna2Panel
    Friend WithEvents Guna2ShadowPanel1 As Guna.UI2.WinForms.Guna2ShadowPanel
    Friend WithEvents Guna2ShadowPanel2 As Guna.UI2.WinForms.Guna2ShadowPanel
    Private WithEvents Label9 As Label
    Private WithEvents Label4 As Label
    Private WithEvents Label10 As Label
    Friend WithEvents lstClientSuggestions As ListBox
    Private WithEvents txtClientSearch As Guna.UI2.WinForms.Guna2TextBox
    Private WithEvents txtPaidAmount As Guna.UI2.WinForms.Guna2TextBox
    Private WithEvents Label11 As Label
    Private WithEvents lblPaidAmount As Label
End Class
