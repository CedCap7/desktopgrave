<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class dashboard
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(dashboard))
        Me.Guna2Panel1 = New Guna.UI2.WinForms.Guna2Panel()
        Me.Guna2Elipse1 = New Guna.UI2.WinForms.Guna2Elipse(Me.components)
        Me.Guna2Panel3 = New Guna.UI2.WinForms.Guna2Panel()
        Me.Guna2ControlBox3 = New Guna.UI2.WinForms.Guna2ControlBox()
        Me.Guna2ControlBox2 = New Guna.UI2.WinForms.Guna2ControlBox()
        Me.Guna2ControlBox1 = New Guna.UI2.WinForms.Guna2ControlBox()
        Me.subPanel = New Guna.UI2.WinForms.Guna2Panel()
        Me.btnPackages = New Guna.UI2.WinForms.Guna2Button()
        Me.btnUserManagement = New Guna.UI2.WinForms.Guna2Button()
        Me.paymentbtn = New Guna.UI2.WinForms.Guna2Button()
        Me.btnDashboard = New Guna.UI2.WinForms.Guna2Button()
        Me.clientregistrybtn = New Guna.UI2.WinForms.Guna2Button()
        Me.Guna2Button1 = New Guna.UI2.WinForms.Guna2Button()
        Me.payreservbtn = New Guna.UI2.WinForms.Guna2Button()
        Me.mapbtn = New Guna.UI2.WinForms.Guna2Button()
        Me.registrybtn = New Guna.UI2.WinForms.Guna2Button()
        Me.formsbtn = New Guna.UI2.WinForms.Guna2Button()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Guna2Panel1.SuspendLayout()
        Me.Guna2Panel3.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Guna2Panel1
        '
        Me.Guna2Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(8, Byte), Integer), CType(CType(37, Byte), Integer), CType(CType(56, Byte), Integer))
        Me.Guna2Panel1.Controls.Add(Me.btnPackages)
        Me.Guna2Panel1.Controls.Add(Me.btnUserManagement)
        Me.Guna2Panel1.Controls.Add(Me.paymentbtn)
        Me.Guna2Panel1.Controls.Add(Me.btnDashboard)
        Me.Guna2Panel1.Controls.Add(Me.clientregistrybtn)
        Me.Guna2Panel1.Controls.Add(Me.Guna2Button1)
        Me.Guna2Panel1.Controls.Add(Me.payreservbtn)
        Me.Guna2Panel1.Controls.Add(Me.mapbtn)
        Me.Guna2Panel1.Controls.Add(Me.registrybtn)
        Me.Guna2Panel1.Controls.Add(Me.formsbtn)
        Me.Guna2Panel1.Dock = System.Windows.Forms.DockStyle.Left
        Me.Guna2Panel1.Location = New System.Drawing.Point(0, 51)
        Me.Guna2Panel1.Name = "Guna2Panel1"
        Me.Guna2Panel1.ShadowDecoration.BorderRadius = 20
        Me.Guna2Panel1.ShadowDecoration.Enabled = True
        Me.Guna2Panel1.ShadowDecoration.Shadow = New System.Windows.Forms.Padding(10)
        Me.Guna2Panel1.Size = New System.Drawing.Size(178, 730)
        Me.Guna2Panel1.TabIndex = 0
        '
        'Guna2Elipse1
        '
        Me.Guna2Elipse1.BorderRadius = 10
        '
        'Guna2Panel3
        '
        Me.Guna2Panel3.BackColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(171, Byte), Integer), CType(CType(189, Byte), Integer))
        Me.Guna2Panel3.Controls.Add(Me.PictureBox1)
        Me.Guna2Panel3.Controls.Add(Me.Guna2ControlBox3)
        Me.Guna2Panel3.Controls.Add(Me.Guna2ControlBox2)
        Me.Guna2Panel3.Controls.Add(Me.Guna2ControlBox1)
        Me.Guna2Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Guna2Panel3.Location = New System.Drawing.Point(0, 0)
        Me.Guna2Panel3.Name = "Guna2Panel3"
        Me.Guna2Panel3.ShadowDecoration.BorderRadius = 10
        Me.Guna2Panel3.ShadowDecoration.Enabled = True
        Me.Guna2Panel3.ShadowDecoration.Shadow = New System.Windows.Forms.Padding(0, 0, 0, 5)
        Me.Guna2Panel3.Size = New System.Drawing.Size(870, 51)
        Me.Guna2Panel3.TabIndex = 5
        '
        'Guna2ControlBox3
        '
        Me.Guna2ControlBox3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Guna2ControlBox3.Animated = True
        Me.Guna2ControlBox3.ControlBoxType = Guna.UI2.WinForms.Enums.ControlBoxType.MinimizeBox
        Me.Guna2ControlBox3.FillColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(171, Byte), Integer), CType(CType(189, Byte), Integer))
        Me.Guna2ControlBox3.IconColor = System.Drawing.Color.White
        Me.Guna2ControlBox3.Location = New System.Drawing.Point(762, 0)
        Me.Guna2ControlBox3.Name = "Guna2ControlBox3"
        Me.Guna2ControlBox3.Size = New System.Drawing.Size(36, 23)
        Me.Guna2ControlBox3.TabIndex = 3
        '
        'Guna2ControlBox2
        '
        Me.Guna2ControlBox2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Guna2ControlBox2.Animated = True
        Me.Guna2ControlBox2.ControlBoxType = Guna.UI2.WinForms.Enums.ControlBoxType.MaximizeBox
        Me.Guna2ControlBox2.FillColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(171, Byte), Integer), CType(CType(189, Byte), Integer))
        Me.Guna2ControlBox2.IconColor = System.Drawing.Color.White
        Me.Guna2ControlBox2.Location = New System.Drawing.Point(798, 0)
        Me.Guna2ControlBox2.Name = "Guna2ControlBox2"
        Me.Guna2ControlBox2.Size = New System.Drawing.Size(36, 23)
        Me.Guna2ControlBox2.TabIndex = 2
        '
        'Guna2ControlBox1
        '
        Me.Guna2ControlBox1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Guna2ControlBox1.Animated = True
        Me.Guna2ControlBox1.FillColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(171, Byte), Integer), CType(CType(189, Byte), Integer))
        Me.Guna2ControlBox1.HoverState.FillColor = System.Drawing.Color.Red
        Me.Guna2ControlBox1.IconColor = System.Drawing.Color.White
        Me.Guna2ControlBox1.Location = New System.Drawing.Point(834, 0)
        Me.Guna2ControlBox1.Name = "Guna2ControlBox1"
        Me.Guna2ControlBox1.Size = New System.Drawing.Size(36, 23)
        Me.Guna2ControlBox1.TabIndex = 1
        '
        'subPanel
        '
        Me.subPanel.AutoRoundedCorners = True
        Me.subPanel.AutoSize = True
        Me.subPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(2, Byte), Integer), CType(CType(15, Byte), Integer), CType(CType(26, Byte), Integer))
        Me.subPanel.BorderRadius = 345
        Me.subPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.subPanel.Location = New System.Drawing.Point(178, 51)
        Me.subPanel.Name = "subPanel"
        Me.subPanel.Padding = New System.Windows.Forms.Padding(5)
        Me.subPanel.Size = New System.Drawing.Size(692, 730)
        Me.subPanel.TabIndex = 6
        '
        'btnPackages
        '
        Me.btnPackages.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.btnPackages.Animated = True
        Me.btnPackages.CheckedState.BorderColor = System.Drawing.Color.White
        Me.btnPackages.CheckedState.CustomBorderColor = System.Drawing.Color.White
        Me.btnPackages.CheckedState.FillColor = System.Drawing.Color.Black
        Me.btnPackages.CustomBorderColor = System.Drawing.Color.FromArgb(CType(CType(8, Byte), Integer), CType(CType(37, Byte), Integer), CType(CType(56, Byte), Integer))
        Me.btnPackages.CustomBorderThickness = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.btnPackages.FillColor = System.Drawing.Color.FromArgb(CType(CType(8, Byte), Integer), CType(CType(37, Byte), Integer), CType(CType(56, Byte), Integer))
        Me.btnPackages.Font = New System.Drawing.Font("Roboto Condensed", 12.0!)
        Me.btnPackages.ForeColor = System.Drawing.Color.White
        Me.btnPackages.HoverState.CustomBorderColor = System.Drawing.Color.White
        Me.btnPackages.HoverState.FillColor = System.Drawing.Color.FromArgb(CType(CType(2, Byte), Integer), CType(CType(15, Byte), Integer), CType(CType(26, Byte), Integer))
        Me.btnPackages.Image = Global.dashboard.My.Resources.Resources.packages
        Me.btnPackages.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.btnPackages.ImageSize = New System.Drawing.Size(28, 28)
        Me.btnPackages.IndicateFocus = True
        Me.btnPackages.Location = New System.Drawing.Point(0, 518)
        Me.btnPackages.Name = "btnPackages"
        Me.btnPackages.Size = New System.Drawing.Size(178, 60)
        Me.btnPackages.TabIndex = 9
        Me.btnPackages.Text = "Packages"
        Me.btnPackages.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        '
        'btnUserManagement
        '
        Me.btnUserManagement.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.btnUserManagement.Animated = True
        Me.btnUserManagement.CheckedState.BorderColor = System.Drawing.Color.White
        Me.btnUserManagement.CheckedState.CustomBorderColor = System.Drawing.Color.White
        Me.btnUserManagement.CheckedState.FillColor = System.Drawing.Color.Black
        Me.btnUserManagement.CustomBorderColor = System.Drawing.Color.FromArgb(CType(CType(8, Byte), Integer), CType(CType(37, Byte), Integer), CType(CType(56, Byte), Integer))
        Me.btnUserManagement.CustomBorderThickness = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.btnUserManagement.FillColor = System.Drawing.Color.FromArgb(CType(CType(8, Byte), Integer), CType(CType(37, Byte), Integer), CType(CType(56, Byte), Integer))
        Me.btnUserManagement.Font = New System.Drawing.Font("Roboto Condensed", 12.0!)
        Me.btnUserManagement.ForeColor = System.Drawing.Color.White
        Me.btnUserManagement.HoverState.CustomBorderColor = System.Drawing.Color.White
        Me.btnUserManagement.HoverState.FillColor = System.Drawing.Color.FromArgb(CType(CType(2, Byte), Integer), CType(CType(15, Byte), Integer), CType(CType(26, Byte), Integer))
        Me.btnUserManagement.Image = Global.dashboard.My.Resources.Resources.user
        Me.btnUserManagement.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.btnUserManagement.ImageSize = New System.Drawing.Size(28, 28)
        Me.btnUserManagement.IndicateFocus = True
        Me.btnUserManagement.Location = New System.Drawing.Point(0, 458)
        Me.btnUserManagement.Name = "btnUserManagement"
        Me.btnUserManagement.Size = New System.Drawing.Size(178, 60)
        Me.btnUserManagement.TabIndex = 8
        Me.btnUserManagement.Text = "User Management"
        Me.btnUserManagement.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        '
        'paymentbtn
        '
        Me.paymentbtn.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.paymentbtn.Animated = True
        Me.paymentbtn.CustomBorderColor = System.Drawing.Color.FromArgb(CType(CType(8, Byte), Integer), CType(CType(37, Byte), Integer), CType(CType(56, Byte), Integer))
        Me.paymentbtn.CustomBorderThickness = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.paymentbtn.FillColor = System.Drawing.Color.FromArgb(CType(CType(8, Byte), Integer), CType(CType(37, Byte), Integer), CType(CType(56, Byte), Integer))
        Me.paymentbtn.Font = New System.Drawing.Font("Roboto Condensed", 12.0!)
        Me.paymentbtn.ForeColor = System.Drawing.Color.White
        Me.paymentbtn.HoverState.CustomBorderColor = System.Drawing.Color.White
        Me.paymentbtn.HoverState.FillColor = System.Drawing.Color.FromArgb(CType(CType(2, Byte), Integer), CType(CType(15, Byte), Integer), CType(CType(26, Byte), Integer))
        Me.paymentbtn.Image = Global.dashboard.My.Resources.Resources.reservation1
        Me.paymentbtn.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.paymentbtn.ImageSize = New System.Drawing.Size(28, 28)
        Me.paymentbtn.IndicateFocus = True
        Me.paymentbtn.Location = New System.Drawing.Point(0, 230)
        Me.paymentbtn.Name = "paymentbtn"
        Me.paymentbtn.Size = New System.Drawing.Size(178, 54)
        Me.paymentbtn.TabIndex = 7
        Me.paymentbtn.Text = "Payment"
        Me.paymentbtn.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        '
        'btnDashboard
        '
        Me.btnDashboard.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.btnDashboard.Animated = True
        Me.btnDashboard.CustomBorderColor = System.Drawing.Color.FromArgb(CType(CType(8, Byte), Integer), CType(CType(37, Byte), Integer), CType(CType(56, Byte), Integer))
        Me.btnDashboard.CustomBorderThickness = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.btnDashboard.FillColor = System.Drawing.Color.FromArgb(CType(CType(8, Byte), Integer), CType(CType(37, Byte), Integer), CType(CType(56, Byte), Integer))
        Me.btnDashboard.Font = New System.Drawing.Font("Roboto Condensed", 12.0!)
        Me.btnDashboard.ForeColor = System.Drawing.Color.White
        Me.btnDashboard.HoverState.CustomBorderColor = System.Drawing.Color.White
        Me.btnDashboard.HoverState.FillColor = System.Drawing.Color.FromArgb(CType(CType(2, Byte), Integer), CType(CType(15, Byte), Integer), CType(CType(26, Byte), Integer))
        Me.btnDashboard.Image = Global.dashboard.My.Resources.Resources.home1
        Me.btnDashboard.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.btnDashboard.ImageSize = New System.Drawing.Size(28, 28)
        Me.btnDashboard.IndicateFocus = True
        Me.btnDashboard.Location = New System.Drawing.Point(0, 69)
        Me.btnDashboard.Name = "btnDashboard"
        Me.btnDashboard.Size = New System.Drawing.Size(178, 54)
        Me.btnDashboard.TabIndex = 0
        Me.btnDashboard.Text = "Dashboard"
        Me.btnDashboard.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        '
        'clientregistrybtn
        '
        Me.clientregistrybtn.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.clientregistrybtn.Animated = True
        Me.clientregistrybtn.CheckedState.BorderColor = System.Drawing.Color.White
        Me.clientregistrybtn.CheckedState.CustomBorderColor = System.Drawing.Color.White
        Me.clientregistrybtn.CheckedState.FillColor = System.Drawing.Color.Black
        Me.clientregistrybtn.CustomBorderColor = System.Drawing.Color.FromArgb(CType(CType(8, Byte), Integer), CType(CType(37, Byte), Integer), CType(CType(56, Byte), Integer))
        Me.clientregistrybtn.CustomBorderThickness = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.clientregistrybtn.FillColor = System.Drawing.Color.FromArgb(CType(CType(8, Byte), Integer), CType(CType(37, Byte), Integer), CType(CType(56, Byte), Integer))
        Me.clientregistrybtn.Font = New System.Drawing.Font("Roboto Condensed", 12.0!)
        Me.clientregistrybtn.ForeColor = System.Drawing.Color.White
        Me.clientregistrybtn.HoverState.CustomBorderColor = System.Drawing.Color.White
        Me.clientregistrybtn.HoverState.FillColor = System.Drawing.Color.FromArgb(CType(CType(2, Byte), Integer), CType(CType(15, Byte), Integer), CType(CType(26, Byte), Integer))
        Me.clientregistrybtn.Image = Global.dashboard.My.Resources.Resources.client1
        Me.clientregistrybtn.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.clientregistrybtn.ImageSize = New System.Drawing.Size(28, 28)
        Me.clientregistrybtn.IndicateFocus = True
        Me.clientregistrybtn.Location = New System.Drawing.Point(0, 398)
        Me.clientregistrybtn.Name = "clientregistrybtn"
        Me.clientregistrybtn.Size = New System.Drawing.Size(178, 60)
        Me.clientregistrybtn.TabIndex = 5
        Me.clientregistrybtn.Text = "Client Registry"
        Me.clientregistrybtn.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        '
        'Guna2Button1
        '
        Me.Guna2Button1.Animated = True
        Me.Guna2Button1.CheckedState.BorderColor = System.Drawing.Color.White
        Me.Guna2Button1.CheckedState.CustomBorderColor = System.Drawing.Color.White
        Me.Guna2Button1.CheckedState.FillColor = System.Drawing.Color.Black
        Me.Guna2Button1.CustomBorderColor = System.Drawing.Color.FromArgb(CType(CType(8, Byte), Integer), CType(CType(37, Byte), Integer), CType(CType(56, Byte), Integer))
        Me.Guna2Button1.CustomBorderThickness = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.Guna2Button1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Guna2Button1.FillColor = System.Drawing.Color.FromArgb(CType(CType(8, Byte), Integer), CType(CType(37, Byte), Integer), CType(CType(56, Byte), Integer))
        Me.Guna2Button1.Font = New System.Drawing.Font("Roboto Condensed", 11.25!, System.Drawing.FontStyle.Bold)
        Me.Guna2Button1.ForeColor = System.Drawing.Color.White
        Me.Guna2Button1.HoverState.CustomBorderColor = System.Drawing.Color.White
        Me.Guna2Button1.HoverState.FillColor = System.Drawing.Color.FromArgb(CType(CType(2, Byte), Integer), CType(CType(15, Byte), Integer), CType(CType(26, Byte), Integer))
        Me.Guna2Button1.Image = Global.dashboard.My.Resources.Resources.signout1
        Me.Guna2Button1.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.Guna2Button1.ImageSize = New System.Drawing.Size(28, 28)
        Me.Guna2Button1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Guna2Button1.Location = New System.Drawing.Point(0, 690)
        Me.Guna2Button1.Name = "Guna2Button1"
        Me.Guna2Button1.Size = New System.Drawing.Size(178, 40)
        Me.Guna2Button1.TabIndex = 6
        Me.Guna2Button1.Text = "Sign Out"
        '
        'payreservbtn
        '
        Me.payreservbtn.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.payreservbtn.Animated = True
        Me.payreservbtn.CustomBorderColor = System.Drawing.Color.FromArgb(CType(CType(8, Byte), Integer), CType(CType(37, Byte), Integer), CType(CType(56, Byte), Integer))
        Me.payreservbtn.CustomBorderThickness = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.payreservbtn.FillColor = System.Drawing.Color.FromArgb(CType(CType(8, Byte), Integer), CType(CType(37, Byte), Integer), CType(CType(56, Byte), Integer))
        Me.payreservbtn.Font = New System.Drawing.Font("Roboto Condensed", 12.0!)
        Me.payreservbtn.ForeColor = System.Drawing.Color.White
        Me.payreservbtn.HoverState.CustomBorderColor = System.Drawing.Color.White
        Me.payreservbtn.HoverState.FillColor = System.Drawing.Color.FromArgb(CType(CType(2, Byte), Integer), CType(CType(15, Byte), Integer), CType(CType(26, Byte), Integer))
        Me.payreservbtn.Image = Global.dashboard.My.Resources.Resources.reservation3
        Me.payreservbtn.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.payreservbtn.ImageSize = New System.Drawing.Size(28, 28)
        Me.payreservbtn.IndicateFocus = True
        Me.payreservbtn.Location = New System.Drawing.Point(0, 177)
        Me.payreservbtn.Name = "payreservbtn"
        Me.payreservbtn.Size = New System.Drawing.Size(178, 53)
        Me.payreservbtn.TabIndex = 2
        Me.payreservbtn.Text = "Grave Purchases"
        Me.payreservbtn.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        '
        'mapbtn
        '
        Me.mapbtn.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.mapbtn.Animated = True
        Me.mapbtn.CustomBorderColor = System.Drawing.Color.FromArgb(CType(CType(8, Byte), Integer), CType(CType(37, Byte), Integer), CType(CType(56, Byte), Integer))
        Me.mapbtn.CustomBorderThickness = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.mapbtn.FillColor = System.Drawing.Color.FromArgb(CType(CType(8, Byte), Integer), CType(CType(37, Byte), Integer), CType(CType(56, Byte), Integer))
        Me.mapbtn.Font = New System.Drawing.Font("Roboto Condensed", 12.0!)
        Me.mapbtn.ForeColor = System.Drawing.Color.White
        Me.mapbtn.HoverState.CustomBorderColor = System.Drawing.Color.White
        Me.mapbtn.HoverState.FillColor = System.Drawing.Color.FromArgb(CType(CType(2, Byte), Integer), CType(CType(15, Byte), Integer), CType(CType(26, Byte), Integer))
        Me.mapbtn.Image = Global.dashboard.My.Resources.Resources.map
        Me.mapbtn.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.mapbtn.ImageSize = New System.Drawing.Size(28, 28)
        Me.mapbtn.IndicateFocus = True
        Me.mapbtn.Location = New System.Drawing.Point(0, 284)
        Me.mapbtn.Name = "mapbtn"
        Me.mapbtn.Size = New System.Drawing.Size(178, 54)
        Me.mapbtn.TabIndex = 3
        Me.mapbtn.Text = "Cemetery Map"
        Me.mapbtn.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        '
        'registrybtn
        '
        Me.registrybtn.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.registrybtn.Animated = True
        Me.registrybtn.CheckedState.BorderColor = System.Drawing.Color.White
        Me.registrybtn.CheckedState.CustomBorderColor = System.Drawing.Color.White
        Me.registrybtn.CheckedState.FillColor = System.Drawing.Color.Black
        Me.registrybtn.CustomBorderColor = System.Drawing.Color.FromArgb(CType(CType(8, Byte), Integer), CType(CType(37, Byte), Integer), CType(CType(56, Byte), Integer))
        Me.registrybtn.CustomBorderThickness = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.registrybtn.FillColor = System.Drawing.Color.FromArgb(CType(CType(8, Byte), Integer), CType(CType(37, Byte), Integer), CType(CType(56, Byte), Integer))
        Me.registrybtn.Font = New System.Drawing.Font("Roboto Condensed", 12.0!)
        Me.registrybtn.ForeColor = System.Drawing.Color.White
        Me.registrybtn.HoverState.CustomBorderColor = System.Drawing.Color.White
        Me.registrybtn.HoverState.FillColor = System.Drawing.Color.FromArgb(CType(CType(2, Byte), Integer), CType(CType(15, Byte), Integer), CType(CType(26, Byte), Integer))
        Me.registrybtn.Image = Global.dashboard.My.Resources.Resources.registry
        Me.registrybtn.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.registrybtn.ImageSize = New System.Drawing.Size(28, 28)
        Me.registrybtn.IndicateFocus = True
        Me.registrybtn.Location = New System.Drawing.Point(0, 338)
        Me.registrybtn.Name = "registrybtn"
        Me.registrybtn.Size = New System.Drawing.Size(178, 60)
        Me.registrybtn.TabIndex = 4
        Me.registrybtn.Text = "Deceased Registry"
        Me.registrybtn.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        '
        'formsbtn
        '
        Me.formsbtn.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.formsbtn.Animated = True
        Me.formsbtn.CustomBorderColor = System.Drawing.Color.FromArgb(CType(CType(8, Byte), Integer), CType(CType(37, Byte), Integer), CType(CType(56, Byte), Integer))
        Me.formsbtn.CustomBorderThickness = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.formsbtn.FillColor = System.Drawing.Color.FromArgb(CType(CType(8, Byte), Integer), CType(CType(37, Byte), Integer), CType(CType(56, Byte), Integer))
        Me.formsbtn.Font = New System.Drawing.Font("Roboto Condensed", 12.0!)
        Me.formsbtn.ForeColor = System.Drawing.Color.White
        Me.formsbtn.HoverState.CustomBorderColor = System.Drawing.Color.White
        Me.formsbtn.HoverState.FillColor = System.Drawing.Color.FromArgb(CType(CType(2, Byte), Integer), CType(CType(15, Byte), Integer), CType(CType(26, Byte), Integer))
        Me.formsbtn.Image = Global.dashboard.My.Resources.Resources.documents
        Me.formsbtn.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.formsbtn.ImageSize = New System.Drawing.Size(28, 28)
        Me.formsbtn.IndicateFocus = True
        Me.formsbtn.Location = New System.Drawing.Point(0, 123)
        Me.formsbtn.Name = "formsbtn"
        Me.formsbtn.Size = New System.Drawing.Size(178, 54)
        Me.formsbtn.TabIndex = 1
        Me.formsbtn.Text = "Forms"
        Me.formsbtn.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(12, 0)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(213, 51)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox1.TabIndex = 4
        Me.PictureBox1.TabStop = False
        '
        'dashboard
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.ClientSize = New System.Drawing.Size(870, 781)
        Me.Controls.Add(Me.subPanel)
        Me.Controls.Add(Me.Guna2Panel1)
        Me.Controls.Add(Me.Guna2Panel3)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "dashboard"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "DCCMS"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.Guna2Panel1.ResumeLayout(False)
        Me.Guna2Panel3.ResumeLayout(False)
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Guna2Panel1 As Guna.UI2.WinForms.Guna2Panel
    Friend WithEvents formsbtn As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents Guna2Elipse1 As Guna.UI2.WinForms.Guna2Elipse
    Friend WithEvents payreservbtn As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents mapbtn As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents registrybtn As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents Guna2Panel3 As Guna.UI2.WinForms.Guna2Panel
    Friend WithEvents subPanel As Guna.UI2.WinForms.Guna2Panel
    Friend WithEvents Guna2Button1 As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents clientregistrybtn As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents btnDashboard As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents Guna2ControlBox1 As Guna.UI2.WinForms.Guna2ControlBox
    Friend WithEvents Guna2ControlBox3 As Guna.UI2.WinForms.Guna2ControlBox
    Friend WithEvents Guna2ControlBox2 As Guna.UI2.WinForms.Guna2ControlBox
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents paymentbtn As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents btnUserManagement As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents btnPackages As Guna.UI2.WinForms.Guna2Button
End Class
