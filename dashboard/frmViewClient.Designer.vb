<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmViewClient
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmViewClient))
        Me.Guna2ShadowPanel1 = New Guna.UI2.WinForms.Guna2ShadowPanel()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.pbClient = New Guna.UI2.WinForms.Guna2CirclePictureBox()
        Me.LblAddress = New System.Windows.Forms.Label()
        Me.LblEmail = New System.Windows.Forms.Label()
        Me.LblMobile = New System.Windows.Forms.Label()
        Me.LblClient = New System.Windows.Forms.Label()
        Me.Guna2ShadowPanel2 = New Guna.UI2.WinForms.Guna2ShadowPanel()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.btnTransfer = New Guna.UI2.WinForms.Guna2Button()
        Me.ListView1 = New System.Windows.Forms.ListView()
        Me.associatedDeceased = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.plotLocation = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Guna2ShadowPanel3 = New Guna.UI2.WinForms.Guna2ShadowPanel()
        Me.btnAddDeceased = New Guna.UI2.WinForms.Guna2Button()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.PlotsList = New System.Windows.Forms.ListView()
        Me.ColumnHeader2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Guna2ShadowPanel1.SuspendLayout()
        CType(Me.pbClient, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Guna2ShadowPanel2.SuspendLayout()
        Me.Guna2ShadowPanel3.SuspendLayout()
        Me.SuspendLayout()
        '
        'Guna2ShadowPanel1
        '
        Me.Guna2ShadowPanel1.BackColor = System.Drawing.Color.Transparent
        Me.Guna2ShadowPanel1.Controls.Add(Me.Label4)
        Me.Guna2ShadowPanel1.Controls.Add(Me.Label3)
        Me.Guna2ShadowPanel1.Controls.Add(Me.Label2)
        Me.Guna2ShadowPanel1.Controls.Add(Me.Label1)
        Me.Guna2ShadowPanel1.Controls.Add(Me.pbClient)
        Me.Guna2ShadowPanel1.Controls.Add(Me.LblAddress)
        Me.Guna2ShadowPanel1.Controls.Add(Me.LblEmail)
        Me.Guna2ShadowPanel1.Controls.Add(Me.LblMobile)
        Me.Guna2ShadowPanel1.Controls.Add(Me.LblClient)
        Me.Guna2ShadowPanel1.FillColor = System.Drawing.Color.FromArgb(CType(CType(14, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(90, Byte), Integer))
        Me.Guna2ShadowPanel1.Location = New System.Drawing.Point(493, 60)
        Me.Guna2ShadowPanel1.Name = "Guna2ShadowPanel1"
        Me.Guna2ShadowPanel1.Radius = 10
        Me.Guna2ShadowPanel1.ShadowColor = System.Drawing.Color.Black
        Me.Guna2ShadowPanel1.ShadowDepth = 150
        Me.Guna2ShadowPanel1.ShadowShift = 7
        Me.Guna2ShadowPanel1.Size = New System.Drawing.Size(406, 426)
        Me.Guna2ShadowPanel1.TabIndex = 7
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Roboto", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.Control
        Me.Label4.Location = New System.Drawing.Point(20, 322)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(87, 24)
        Me.Label4.TabIndex = 10
        Me.Label4.Text = "Address:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Roboto", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.Control
        Me.Label3.Location = New System.Drawing.Point(20, 278)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(62, 24)
        Me.Label3.TabIndex = 9
        Me.Label3.Text = "Email:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Roboto", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.Control
        Me.Label2.Location = New System.Drawing.Point(20, 234)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(110, 24)
        Me.Label2.TabIndex = 8
        Me.Label2.Text = "Contact No:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Roboto", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.Control
        Me.Label1.Location = New System.Drawing.Point(20, 190)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(64, 24)
        Me.Label1.TabIndex = 7
        Me.Label1.Text = "Client:"
        '
        'pbClient
        '
        Me.pbClient.Image = Global.dashboard.My.Resources.Resources.profile
        Me.pbClient.ImageRotate = 0!
        Me.pbClient.InitialImage = Global.dashboard.My.Resources.Resources.profile
        Me.pbClient.Location = New System.Drawing.Point(130, 22)
        Me.pbClient.Name = "pbClient"
        Me.pbClient.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle
        Me.pbClient.Size = New System.Drawing.Size(175, 175)
        Me.pbClient.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pbClient.TabIndex = 6
        Me.pbClient.TabStop = False
        Me.pbClient.UseTransparentBackground = True
        '
        'LblAddress
        '
        Me.LblAddress.AutoSize = True
        Me.LblAddress.Font = New System.Drawing.Font("Roboto", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblAddress.ForeColor = System.Drawing.SystemColors.Control
        Me.LblAddress.Location = New System.Drawing.Point(20, 346)
        Me.LblAddress.Name = "LblAddress"
        Me.LblAddress.Size = New System.Drawing.Size(70, 20)
        Me.LblAddress.TabIndex = 5
        Me.LblAddress.Text = "Address:"
        '
        'LblEmail
        '
        Me.LblEmail.AutoSize = True
        Me.LblEmail.Font = New System.Drawing.Font("Roboto", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblEmail.ForeColor = System.Drawing.SystemColors.Control
        Me.LblEmail.Location = New System.Drawing.Point(20, 302)
        Me.LblEmail.Name = "LblEmail"
        Me.LblEmail.Size = New System.Drawing.Size(53, 20)
        Me.LblEmail.TabIndex = 4
        Me.LblEmail.Text = "Email:"
        '
        'LblMobile
        '
        Me.LblMobile.AutoSize = True
        Me.LblMobile.Font = New System.Drawing.Font("Roboto", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblMobile.ForeColor = System.Drawing.SystemColors.Control
        Me.LblMobile.Location = New System.Drawing.Point(20, 258)
        Me.LblMobile.Name = "LblMobile"
        Me.LblMobile.Size = New System.Drawing.Size(92, 20)
        Me.LblMobile.TabIndex = 3
        Me.LblMobile.Text = "Contact No:"
        '
        'LblClient
        '
        Me.LblClient.AutoSize = True
        Me.LblClient.Font = New System.Drawing.Font("Roboto", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblClient.ForeColor = System.Drawing.SystemColors.Control
        Me.LblClient.Location = New System.Drawing.Point(20, 214)
        Me.LblClient.Name = "LblClient"
        Me.LblClient.Size = New System.Drawing.Size(53, 20)
        Me.LblClient.TabIndex = 2
        Me.LblClient.Text = "Client:"
        '
        'Guna2ShadowPanel2
        '
        Me.Guna2ShadowPanel2.BackColor = System.Drawing.Color.Transparent
        Me.Guna2ShadowPanel2.Controls.Add(Me.Label7)
        Me.Guna2ShadowPanel2.Controls.Add(Me.btnTransfer)
        Me.Guna2ShadowPanel2.Controls.Add(Me.ListView1)
        Me.Guna2ShadowPanel2.FillColor = System.Drawing.Color.FromArgb(CType(CType(14, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(90, Byte), Integer))
        Me.Guna2ShadowPanel2.Location = New System.Drawing.Point(12, 492)
        Me.Guna2ShadowPanel2.Name = "Guna2ShadowPanel2"
        Me.Guna2ShadowPanel2.Radius = 10
        Me.Guna2ShadowPanel2.ShadowColor = System.Drawing.Color.Black
        Me.Guna2ShadowPanel2.ShadowDepth = 150
        Me.Guna2ShadowPanel2.ShadowShift = 7
        Me.Guna2ShadowPanel2.Size = New System.Drawing.Size(886, 245)
        Me.Guna2ShadowPanel2.TabIndex = 8
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Roboto", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.SystemColors.Control
        Me.Label7.Location = New System.Drawing.Point(22, 18)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(247, 29)
        Me.Label7.TabIndex = 15
        Me.Label7.Text = "Associated Deceased"
        '
        'btnTransfer
        '
        Me.btnTransfer.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.btnTransfer.Animated = True
        Me.btnTransfer.FillColor = System.Drawing.Color.DarkGoldenrod
        Me.btnTransfer.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnTransfer.ForeColor = System.Drawing.Color.White
        Me.btnTransfer.Location = New System.Drawing.Point(778, 22)
        Me.btnTransfer.Name = "btnTransfer"
        Me.btnTransfer.Size = New System.Drawing.Size(75, 25)
        Me.btnTransfer.TabIndex = 14
        Me.btnTransfer.Text = "Transfer"
        '
        'ListView1
        '
        Me.ListView1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ListView1.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.ListView1.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.associatedDeceased, Me.plotLocation})
        Me.ListView1.Font = New System.Drawing.Font("Roboto", 11.25!)
        Me.ListView1.ForeColor = System.Drawing.SystemColors.Control
        Me.ListView1.FullRowSelect = True
        Me.ListView1.GridLines = True
        Me.ListView1.HideSelection = False
        Me.ListView1.Location = New System.Drawing.Point(16, 63)
        Me.ListView1.Name = "ListView1"
        Me.ListView1.Size = New System.Drawing.Size(854, 165)
        Me.ListView1.TabIndex = 0
        Me.ListView1.UseCompatibleStateImageBehavior = False
        Me.ListView1.View = System.Windows.Forms.View.Details
        '
        'associatedDeceased
        '
        Me.associatedDeceased.Text = "Name"
        Me.associatedDeceased.Width = 300
        '
        'plotLocation
        '
        Me.plotLocation.Text = "Plot Location"
        Me.plotLocation.Width = 500
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Roboto", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.SystemColors.Control
        Me.Label11.Location = New System.Drawing.Point(12, 9)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(208, 29)
        Me.Label11.TabIndex = 10
        Me.Label11.Text = "Client Information"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Roboto Condensed", 9.75!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.Control
        Me.Label5.Location = New System.Drawing.Point(14, 38)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(278, 17)
        Me.Label5.TabIndex = 12
        Me.Label5.Text = "Information of the Client and its Associated Deceased"
        '
        'Guna2ShadowPanel3
        '
        Me.Guna2ShadowPanel3.BackColor = System.Drawing.Color.Transparent
        Me.Guna2ShadowPanel3.Controls.Add(Me.btnAddDeceased)
        Me.Guna2ShadowPanel3.Controls.Add(Me.Label6)
        Me.Guna2ShadowPanel3.Controls.Add(Me.PlotsList)
        Me.Guna2ShadowPanel3.FillColor = System.Drawing.Color.FromArgb(CType(CType(14, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(90, Byte), Integer))
        Me.Guna2ShadowPanel3.Location = New System.Drawing.Point(12, 60)
        Me.Guna2ShadowPanel3.Name = "Guna2ShadowPanel3"
        Me.Guna2ShadowPanel3.Radius = 10
        Me.Guna2ShadowPanel3.ShadowColor = System.Drawing.Color.Black
        Me.Guna2ShadowPanel3.ShadowDepth = 150
        Me.Guna2ShadowPanel3.ShadowShift = 7
        Me.Guna2ShadowPanel3.Size = New System.Drawing.Size(475, 426)
        Me.Guna2ShadowPanel3.TabIndex = 9
        '
        'btnAddDeceased
        '
        Me.btnAddDeceased.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.btnAddDeceased.Animated = True
        Me.btnAddDeceased.FillColor = System.Drawing.Color.DarkGoldenrod
        Me.btnAddDeceased.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddDeceased.ForeColor = System.Drawing.Color.White
        Me.btnAddDeceased.Location = New System.Drawing.Point(372, 16)
        Me.btnAddDeceased.Name = "btnAddDeceased"
        Me.btnAddDeceased.Size = New System.Drawing.Size(75, 25)
        Me.btnAddDeceased.TabIndex = 12
        Me.btnAddDeceased.Text = "Assign"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Roboto", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.SystemColors.Control
        Me.Label6.Location = New System.Drawing.Point(22, 16)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(149, 29)
        Me.Label6.TabIndex = 11
        Me.Label6.Text = "Owned Plots"
        '
        'PlotsList
        '
        Me.PlotsList.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PlotsList.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.PlotsList.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader2})
        Me.PlotsList.Font = New System.Drawing.Font("Roboto", 11.25!)
        Me.PlotsList.ForeColor = System.Drawing.SystemColors.Control
        Me.PlotsList.FullRowSelect = True
        Me.PlotsList.GridLines = True
        Me.PlotsList.HideSelection = False
        Me.PlotsList.Location = New System.Drawing.Point(16, 57)
        Me.PlotsList.Name = "PlotsList"
        Me.PlotsList.Size = New System.Drawing.Size(443, 352)
        Me.PlotsList.TabIndex = 0
        Me.PlotsList.UseCompatibleStateImageBehavior = False
        Me.PlotsList.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "Plot Location"
        Me.ColumnHeader2.Width = 800
        '
        'frmViewClient
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(23, Byte), Integer), CType(CType(107, Byte), Integer), CType(CType(135, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(910, 749)
        Me.Controls.Add(Me.Guna2ShadowPanel3)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.Guna2ShadowPanel2)
        Me.Controls.Add(Me.Guna2ShadowPanel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmViewClient"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "View Client"
        Me.Guna2ShadowPanel1.ResumeLayout(False)
        Me.Guna2ShadowPanel1.PerformLayout()
        CType(Me.pbClient, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Guna2ShadowPanel2.ResumeLayout(False)
        Me.Guna2ShadowPanel2.PerformLayout()
        Me.Guna2ShadowPanel3.ResumeLayout(False)
        Me.Guna2ShadowPanel3.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Guna2ShadowPanel1 As Guna.UI2.WinForms.Guna2ShadowPanel
    Friend WithEvents LblAddress As Label
    Friend WithEvents LblMobile As Label
    Friend WithEvents LblClient As Label
    Friend WithEvents Guna2ShadowPanel2 As Guna.UI2.WinForms.Guna2ShadowPanel
    Friend WithEvents ListView1 As ListView
    Friend WithEvents associatedDeceased As ColumnHeader
    Friend WithEvents plotLocation As ColumnHeader
    Friend WithEvents Label11 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents pbClient As Guna.UI2.WinForms.Guna2CirclePictureBox
    Friend WithEvents Label4 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents LblEmail As Label
    Friend WithEvents Guna2ShadowPanel3 As Guna.UI2.WinForms.Guna2ShadowPanel
    Friend WithEvents Label6 As Label
    Friend WithEvents PlotsList As ListView
    Friend WithEvents ColumnHeader2 As ColumnHeader
    Friend WithEvents Label7 As Label
    Friend WithEvents btnTransfer As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents btnAddDeceased As Guna.UI2.WinForms.Guna2Button
End Class
