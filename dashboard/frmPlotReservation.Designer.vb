<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPlotReservation
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
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.panel1 = New System.Windows.Forms.Panel()
        Me.Guna2ShadowPanel2 = New Guna.UI2.WinForms.Guna2ShadowPanel()
        Me.lblPaidAmount = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtGraveType = New System.Windows.Forms.Label()
        Me.currentQuantity = New System.Windows.Forms.NumericUpDown()
        Me.BtnPrintReceipt = New Guna.UI2.WinForms.Guna2Button()
        Me.txtPackage = New System.Windows.Forms.Label()
        Me.lblQuantity = New System.Windows.Forms.Label()
        Me.txtPrice = New System.Windows.Forms.Label()
        Me.txtTotal = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Guna2ShadowPanel1 = New Guna.UI2.WinForms.Guna2ShadowPanel()
        Me.lstClientSuggestions = New System.Windows.Forms.ListBox()
        Me.txtClientSearch = New Guna.UI2.WinForms.Guna2TextBox()
        Me.txtPaidAmount = New Guna.UI2.WinForms.Guna2TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.label1 = New System.Windows.Forms.Label()
        Me.btnConfirm = New Guna.UI2.WinForms.Guna2Button()
        Me.lblPlotLocation = New System.Windows.Forms.Label()
        Me.GraveType = New Guna.UI2.WinForms.Guna2ComboBox()
        Me.btnSelectPlot = New Guna.UI2.WinForms.Guna2Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.subSidePanel = New Guna.UI2.WinForms.Guna2Panel()
        Me.Guna2Panel2.SuspendLayout()
        Me.panel1.SuspendLayout()
        Me.Guna2ShadowPanel2.SuspendLayout()
        CType(Me.currentQuantity, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Guna2ShadowPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Guna2Panel2
        '
        Me.Guna2Panel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(23, Byte), Integer), CType(CType(107, Byte), Integer), CType(CType(135, Byte), Integer))
        Me.Guna2Panel2.Controls.Add(Me.Label5)
        Me.Guna2Panel2.Controls.Add(Me.Label6)
        Me.Guna2Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Guna2Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Guna2Panel2.Name = "Guna2Panel2"
        Me.Guna2Panel2.Size = New System.Drawing.Size(1215, 69)
        Me.Guna2Panel2.TabIndex = 58
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Gill Sans MT", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.Control
        Me.Label5.Location = New System.Drawing.Point(15, 41)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(249, 18)
        Me.Label5.TabIndex = 1
        Me.Label5.Text = "Select Grave Type, Plot, and Package Information"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Century Gothic", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.SystemColors.Control
        Me.Label6.Location = New System.Drawing.Point(12, 9)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(292, 32)
        Me.Label6.TabIndex = 0
        Me.Label6.Text = "Plot Reservation Form"
        '
        'panel1
        '
        Me.panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(23, Byte), Integer), CType(CType(107, Byte), Integer), CType(CType(135, Byte), Integer))
        Me.panel1.Controls.Add(Me.Guna2ShadowPanel2)
        Me.panel1.Controls.Add(Me.Label3)
        Me.panel1.Controls.Add(Me.Guna2ShadowPanel1)
        Me.panel1.Dock = System.Windows.Forms.DockStyle.Left
        Me.panel1.Location = New System.Drawing.Point(0, 69)
        Me.panel1.Margin = New System.Windows.Forms.Padding(2)
        Me.panel1.Name = "panel1"
        Me.panel1.Size = New System.Drawing.Size(372, 680)
        Me.panel1.TabIndex = 59
        '
        'Guna2ShadowPanel2
        '
        Me.Guna2ShadowPanel2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Guna2ShadowPanel2.BackColor = System.Drawing.Color.Transparent
        Me.Guna2ShadowPanel2.Controls.Add(Me.lblPaidAmount)
        Me.Guna2ShadowPanel2.Controls.Add(Me.Label8)
        Me.Guna2ShadowPanel2.Controls.Add(Me.Label4)
        Me.Guna2ShadowPanel2.Controls.Add(Me.txtGraveType)
        Me.Guna2ShadowPanel2.Controls.Add(Me.currentQuantity)
        Me.Guna2ShadowPanel2.Controls.Add(Me.BtnPrintReceipt)
        Me.Guna2ShadowPanel2.Controls.Add(Me.txtPackage)
        Me.Guna2ShadowPanel2.Controls.Add(Me.lblQuantity)
        Me.Guna2ShadowPanel2.Controls.Add(Me.txtPrice)
        Me.Guna2ShadowPanel2.Controls.Add(Me.txtTotal)
        Me.Guna2ShadowPanel2.FillColor = System.Drawing.Color.FromArgb(CType(CType(14, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(90, Byte), Integer))
        Me.Guna2ShadowPanel2.Location = New System.Drawing.Point(18, 419)
        Me.Guna2ShadowPanel2.Margin = New System.Windows.Forms.Padding(5, 30, 5, 20)
        Me.Guna2ShadowPanel2.Name = "Guna2ShadowPanel2"
        Me.Guna2ShadowPanel2.Radius = 10
        Me.Guna2ShadowPanel2.ShadowColor = System.Drawing.Color.Black
        Me.Guna2ShadowPanel2.Size = New System.Drawing.Size(332, 261)
        Me.Guna2ShadowPanel2.TabIndex = 27
        '
        'lblPaidAmount
        '
        Me.lblPaidAmount.AutoSize = True
        Me.lblPaidAmount.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPaidAmount.ForeColor = System.Drawing.Color.White
        Me.lblPaidAmount.Location = New System.Drawing.Point(14, 112)
        Me.lblPaidAmount.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblPaidAmount.Name = "lblPaidAmount"
        Me.lblPaidAmount.Size = New System.Drawing.Size(92, 17)
        Me.lblPaidAmount.TabIndex = 25
        Me.lblPaidAmount.Text = "Paid Amount:"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.White
        Me.Label8.Location = New System.Drawing.Point(97, 18)
        Me.Label8.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(136, 31)
        Me.Label8.TabIndex = 24
        Me.Label8.Text = "Summary"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 21.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.White
        Me.Label4.Location = New System.Drawing.Point(11, 153)
        Me.Label4.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(103, 33)
        Me.Label4.TabIndex = 23
        Me.Label4.Text = "Total: "
        '
        'txtGraveType
        '
        Me.txtGraveType.AutoSize = True
        Me.txtGraveType.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGraveType.ForeColor = System.Drawing.Color.White
        Me.txtGraveType.Location = New System.Drawing.Point(14, 61)
        Me.txtGraveType.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.txtGraveType.Name = "txtGraveType"
        Me.txtGraveType.Size = New System.Drawing.Size(91, 17)
        Me.txtGraveType.TabIndex = 17
        Me.txtGraveType.Text = "Grave Type: "
        '
        'currentQuantity
        '
        Me.currentQuantity.Location = New System.Drawing.Point(84, 130)
        Me.currentQuantity.Name = "currentQuantity"
        Me.currentQuantity.Size = New System.Drawing.Size(46, 20)
        Me.currentQuantity.TabIndex = 22
        Me.currentQuantity.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.currentQuantity.Visible = False
        '
        'BtnPrintReceipt
        '
        Me.BtnPrintReceipt.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnPrintReceipt.AutoRoundedCorners = True
        Me.BtnPrintReceipt.BackColor = System.Drawing.Color.Transparent
        Me.BtnPrintReceipt.BorderRadius = 17
        Me.BtnPrintReceipt.BorderThickness = 1
        Me.BtnPrintReceipt.FillColor = System.Drawing.Color.LimeGreen
        Me.BtnPrintReceipt.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold)
        Me.BtnPrintReceipt.ForeColor = System.Drawing.SystemColors.Window
        Me.BtnPrintReceipt.Location = New System.Drawing.Point(36, 214)
        Me.BtnPrintReceipt.Margin = New System.Windows.Forms.Padding(2, 2, 2, 10)
        Me.BtnPrintReceipt.Name = "BtnPrintReceipt"
        Me.BtnPrintReceipt.Size = New System.Drawing.Size(258, 37)
        Me.BtnPrintReceipt.TabIndex = 3
        Me.BtnPrintReceipt.Text = "Print Receipt"
        '
        'txtPackage
        '
        Me.txtPackage.AutoSize = True
        Me.txtPackage.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPackage.ForeColor = System.Drawing.Color.White
        Me.txtPackage.Location = New System.Drawing.Point(14, 78)
        Me.txtPackage.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.txtPackage.Name = "txtPackage"
        Me.txtPackage.Size = New System.Drawing.Size(67, 17)
        Me.txtPackage.TabIndex = 18
        Me.txtPackage.Text = "Package:"
        '
        'lblQuantity
        '
        Me.lblQuantity.AutoSize = True
        Me.lblQuantity.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblQuantity.ForeColor = System.Drawing.Color.White
        Me.lblQuantity.Location = New System.Drawing.Point(14, 129)
        Me.lblQuantity.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblQuantity.Name = "lblQuantity"
        Me.lblQuantity.Size = New System.Drawing.Size(65, 17)
        Me.lblQuantity.TabIndex = 21
        Me.lblQuantity.Text = "Quantity:"
        '
        'txtPrice
        '
        Me.txtPrice.AutoSize = True
        Me.txtPrice.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPrice.ForeColor = System.Drawing.Color.White
        Me.txtPrice.Location = New System.Drawing.Point(14, 95)
        Me.txtPrice.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.txtPrice.Name = "txtPrice"
        Me.txtPrice.Size = New System.Drawing.Size(44, 17)
        Me.txtPrice.TabIndex = 19
        Me.txtPrice.Text = "Price:"
        '
        'txtTotal
        '
        Me.txtTotal.AutoSize = True
        Me.txtTotal.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotal.ForeColor = System.Drawing.Color.White
        Me.txtTotal.Location = New System.Drawing.Point(12, 186)
        Me.txtTotal.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.txtTotal.Name = "txtTotal"
        Me.txtTotal.Size = New System.Drawing.Size(79, 25)
        Me.txtTotal.TabIndex = 20
        Me.txtTotal.Text = "Total: "
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
        'Guna2ShadowPanel1
        '
        Me.Guna2ShadowPanel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Guna2ShadowPanel1.BackColor = System.Drawing.Color.Transparent
        Me.Guna2ShadowPanel1.Controls.Add(Me.lstClientSuggestions)
        Me.Guna2ShadowPanel1.Controls.Add(Me.txtClientSearch)
        Me.Guna2ShadowPanel1.Controls.Add(Me.txtPaidAmount)
        Me.Guna2ShadowPanel1.Controls.Add(Me.Label11)
        Me.Guna2ShadowPanel1.Controls.Add(Me.Label10)
        Me.Guna2ShadowPanel1.Controls.Add(Me.Label9)
        Me.Guna2ShadowPanel1.Controls.Add(Me.Label7)
        Me.Guna2ShadowPanel1.Controls.Add(Me.label1)
        Me.Guna2ShadowPanel1.Controls.Add(Me.btnConfirm)
        Me.Guna2ShadowPanel1.Controls.Add(Me.lblPlotLocation)
        Me.Guna2ShadowPanel1.Controls.Add(Me.GraveType)
        Me.Guna2ShadowPanel1.Controls.Add(Me.btnSelectPlot)
        Me.Guna2ShadowPanel1.Controls.Add(Me.Label2)
        Me.Guna2ShadowPanel1.FillColor = System.Drawing.Color.FromArgb(CType(CType(14, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(90, Byte), Integer))
        Me.Guna2ShadowPanel1.Location = New System.Drawing.Point(18, 33)
        Me.Guna2ShadowPanel1.Margin = New System.Windows.Forms.Padding(5, 30, 5, 20)
        Me.Guna2ShadowPanel1.Name = "Guna2ShadowPanel1"
        Me.Guna2ShadowPanel1.Radius = 10
        Me.Guna2ShadowPanel1.ShadowColor = System.Drawing.Color.Black
        Me.Guna2ShadowPanel1.Size = New System.Drawing.Size(332, 384)
        Me.Guna2ShadowPanel1.TabIndex = 26
        '
        'lstClientSuggestions
        '
        Me.lstClientSuggestions.Font = New System.Drawing.Font("Roboto", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstClientSuggestions.FormattingEnabled = True
        Me.lstClientSuggestions.ItemHeight = 20
        Me.lstClientSuggestions.Location = New System.Drawing.Point(14, 112)
        Me.lstClientSuggestions.Name = "lstClientSuggestions"
        Me.lstClientSuggestions.Size = New System.Drawing.Size(306, 64)
        Me.lstClientSuggestions.TabIndex = 65
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
        Me.txtClientSearch.Location = New System.Drawing.Point(14, 75)
        Me.txtClientSearch.Margin = New System.Windows.Forms.Padding(4)
        Me.txtClientSearch.Name = "txtClientSearch"
        Me.txtClientSearch.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.txtClientSearch.PlaceholderForeColor = System.Drawing.Color.Gray
        Me.txtClientSearch.PlaceholderText = "Select client or type to search"
        Me.txtClientSearch.SelectedText = ""
        Me.txtClientSearch.Size = New System.Drawing.Size(305, 37)
        Me.txtClientSearch.TabIndex = 64
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
        Me.txtPaidAmount.Location = New System.Drawing.Point(14, 187)
        Me.txtPaidAmount.Margin = New System.Windows.Forms.Padding(4)
        Me.txtPaidAmount.Name = "txtPaidAmount"
        Me.txtPaidAmount.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.txtPaidAmount.PlaceholderForeColor = System.Drawing.Color.Gray
        Me.txtPaidAmount.PlaceholderText = "Input Amount"
        Me.txtPaidAmount.SelectedText = ""
        Me.txtPaidAmount.Size = New System.Drawing.Size(305, 36)
        Me.txtPaidAmount.TabIndex = 28
        Me.txtPaidAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.Color.Transparent
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.Color.White
        Me.Label11.Location = New System.Drawing.Point(11, 168)
        Me.Label11.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(88, 17)
        Me.Label11.TabIndex = 27
        Me.Label11.Text = "Amount Paid"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.White
        Me.Label10.Location = New System.Drawing.Point(11, 225)
        Me.Label10.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(94, 17)
        Me.Label10.TabIndex = 26
        Me.Label10.Text = "Plot Selection"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 21.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.White
        Me.Label9.Location = New System.Drawing.Point(78, 12)
        Me.Label9.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(182, 33)
        Me.Label9.TabIndex = 25
        Me.Label9.Text = "Reservation"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.White
        Me.Label7.Location = New System.Drawing.Point(11, 287)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(113, 18)
        Me.Label7.TabIndex = 25
        Me.Label7.Text = "Selected Plot:"
        '
        'label1
        '
        Me.label1.AutoSize = True
        Me.label1.BackColor = System.Drawing.Color.Transparent
        Me.label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label1.ForeColor = System.Drawing.Color.White
        Me.label1.Location = New System.Drawing.Point(14, 111)
        Me.label1.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.label1.Name = "label1"
        Me.label1.Size = New System.Drawing.Size(291, 17)
        Me.label1.TabIndex = 5
        Me.label1.Text = "Burial Type (Apartment, Private, Maoseleum)"
        '
        'btnConfirm
        '
        Me.btnConfirm.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnConfirm.AutoRoundedCorners = True
        Me.btnConfirm.BackColor = System.Drawing.Color.Transparent
        Me.btnConfirm.BorderRadius = 17
        Me.btnConfirm.BorderThickness = 1
        Me.btnConfirm.FillColor = System.Drawing.Color.LimeGreen
        Me.btnConfirm.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold)
        Me.btnConfirm.ForeColor = System.Drawing.Color.White
        Me.btnConfirm.Location = New System.Drawing.Point(36, 337)
        Me.btnConfirm.Margin = New System.Windows.Forms.Padding(2, 2, 2, 10)
        Me.btnConfirm.Name = "btnConfirm"
        Me.btnConfirm.Size = New System.Drawing.Size(258, 37)
        Me.btnConfirm.TabIndex = 2
        Me.btnConfirm.Text = "Confirm"
        '
        'lblPlotLocation
        '
        Me.lblPlotLocation.AutoSize = True
        Me.lblPlotLocation.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPlotLocation.ForeColor = System.Drawing.Color.White
        Me.lblPlotLocation.Location = New System.Drawing.Point(11, 305)
        Me.lblPlotLocation.Name = "lblPlotLocation"
        Me.lblPlotLocation.Size = New System.Drawing.Size(49, 16)
        Me.lblPlotLocation.TabIndex = 24
        Me.lblPlotLocation.Text = "Label7"
        Me.lblPlotLocation.Visible = False
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
        Me.GraveType.Location = New System.Drawing.Point(14, 130)
        Me.GraveType.Margin = New System.Windows.Forms.Padding(2)
        Me.GraveType.Name = "GraveType"
        Me.GraveType.Size = New System.Drawing.Size(305, 36)
        Me.GraveType.TabIndex = 6
        Me.GraveType.Tag = ""
        '
        'btnSelectPlot
        '
        Me.btnSelectPlot.Animated = True
        Me.btnSelectPlot.BackColor = System.Drawing.Color.FromArgb(CType(CType(14, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(90, Byte), Integer))
        Me.btnSelectPlot.BorderRadius = 5
        Me.btnSelectPlot.BorderThickness = 1
        Me.btnSelectPlot.FillColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(171, Byte), Integer), CType(CType(189, Byte), Integer))
        Me.btnSelectPlot.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold)
        Me.btnSelectPlot.ForeColor = System.Drawing.SystemColors.Window
        Me.btnSelectPlot.HoverState.BorderColor = System.Drawing.SystemColors.Control
        Me.btnSelectPlot.HoverState.FillColor = System.Drawing.Color.Black
        Me.btnSelectPlot.Image = Global.dashboard.My.Resources.Resources.map
        Me.btnSelectPlot.Location = New System.Drawing.Point(14, 244)
        Me.btnSelectPlot.Margin = New System.Windows.Forms.Padding(2)
        Me.btnSelectPlot.Name = "btnSelectPlot"
        Me.btnSelectPlot.Size = New System.Drawing.Size(305, 36)
        Me.btnSelectPlot.TabIndex = 23
        Me.btnSelectPlot.Text = "Select Plot"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.White
        Me.Label2.Location = New System.Drawing.Point(14, 54)
        Me.Label2.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(84, 17)
        Me.Label2.TabIndex = 15
        Me.Label2.Text = "Client Name"
        '
        'subSidePanel
        '
        Me.subSidePanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(14, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(90, Byte), Integer))
        Me.subSidePanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.subSidePanel.Location = New System.Drawing.Point(372, 69)
        Me.subSidePanel.Name = "subSidePanel"
        Me.subSidePanel.Size = New System.Drawing.Size(843, 680)
        Me.subSidePanel.TabIndex = 60
        '
        'frmPlotReservation
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1215, 749)
        Me.Controls.Add(Me.subSidePanel)
        Me.Controls.Add(Me.panel1)
        Me.Controls.Add(Me.Guna2Panel2)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "frmPlotReservation"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "frmPlotReservation"
        Me.Guna2Panel2.ResumeLayout(False)
        Me.Guna2Panel2.PerformLayout()
        Me.panel1.ResumeLayout(False)
        Me.panel1.PerformLayout()
        Me.Guna2ShadowPanel2.ResumeLayout(False)
        Me.Guna2ShadowPanel2.PerformLayout()
        CType(Me.currentQuantity, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Guna2ShadowPanel1.ResumeLayout(False)
        Me.Guna2ShadowPanel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Guna2Panel2 As Guna.UI2.WinForms.Guna2Panel
    Friend WithEvents Label5 As Label
    Friend WithEvents Label6 As Label
    Private WithEvents panel1 As Panel
    Friend WithEvents Label7 As Label
    Friend WithEvents lblPlotLocation As Label
    Private WithEvents btnSelectPlot As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents currentQuantity As NumericUpDown
    Private WithEvents lblQuantity As Label
    Private WithEvents txtTotal As Label
    Private WithEvents txtPrice As Label
    Private WithEvents txtPackage As Label
    Private WithEvents txtGraveType As Label
    Private WithEvents Label3 As Label
    Private WithEvents Label2 As Label
    Private WithEvents GraveType As Guna.UI2.WinForms.Guna2ComboBox
    Private WithEvents label1 As Label
    Private WithEvents BtnPrintReceipt As Guna.UI2.WinForms.Guna2Button
    Private WithEvents btnConfirm As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents Guna2ShadowPanel1 As Guna.UI2.WinForms.Guna2ShadowPanel
    Friend WithEvents Guna2ShadowPanel2 As Guna.UI2.WinForms.Guna2ShadowPanel
    Private WithEvents Label4 As Label
    Friend WithEvents subSidePanel As Guna.UI2.WinForms.Guna2Panel
    Private WithEvents Label8 As Label
    Private WithEvents Label9 As Label
    Private WithEvents Label10 As Label
    Private WithEvents Label11 As Label
    Private WithEvents txtPaidAmount As Guna.UI2.WinForms.Guna2TextBox
    Private WithEvents lblPaidAmount As Label
    Friend WithEvents lstClientSuggestions As ListBox
    Private WithEvents txtClientSearch As Guna.UI2.WinForms.Guna2TextBox
End Class
