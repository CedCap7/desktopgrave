<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class dashboard
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
        Me.Guna2Panel1 = New Guna.UI2.WinForms.Guna2Panel()
        Me.formsbtn = New Guna.UI2.WinForms.Guna2Button()
        Me.Guna2Elipse1 = New Guna.UI2.WinForms.Guna2Elipse(Me.components)
        Me.registrybtn = New Guna.UI2.WinForms.Guna2Button()
        Me.mapbtn = New Guna.UI2.WinForms.Guna2Button()
        Me.payreservbtn = New Guna.UI2.WinForms.Guna2Button()
        Me.Guna2Panel2 = New Guna.UI2.WinForms.Guna2Panel()
        Me.Guna2Panel3 = New Guna.UI2.WinForms.Guna2Panel()
        Me.Guna2Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Guna2Panel1
        '
        Me.Guna2Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(14, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(90, Byte), Integer))
        Me.Guna2Panel1.Controls.Add(Me.Guna2Panel2)
        Me.Guna2Panel1.Controls.Add(Me.payreservbtn)
        Me.Guna2Panel1.Controls.Add(Me.mapbtn)
        Me.Guna2Panel1.Controls.Add(Me.registrybtn)
        Me.Guna2Panel1.Controls.Add(Me.formsbtn)
        Me.Guna2Panel1.Dock = System.Windows.Forms.DockStyle.Left
        Me.Guna2Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Guna2Panel1.Name = "Guna2Panel1"
        Me.Guna2Panel1.ShadowDecoration.Parent = Me.Guna2Panel1
        Me.Guna2Panel1.Size = New System.Drawing.Size(178, 520)
        Me.Guna2Panel1.TabIndex = 0
        '
        'formsbtn
        '
        Me.formsbtn.CheckedState.Parent = Me.formsbtn
        Me.formsbtn.CustomBorderColor = System.Drawing.Color.FromArgb(CType(CType(14, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(90, Byte), Integer))
        Me.formsbtn.CustomBorderThickness = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.formsbtn.CustomImages.Parent = Me.formsbtn
        Me.formsbtn.FillColor = System.Drawing.Color.FromArgb(CType(CType(14, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(90, Byte), Integer))
        Me.formsbtn.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.formsbtn.ForeColor = System.Drawing.Color.White
        Me.formsbtn.HoverState.CustomBorderColor = System.Drawing.Color.White
        Me.formsbtn.HoverState.FillColor = System.Drawing.Color.FromArgb(CType(CType(2, Byte), Integer), CType(CType(15, Byte), Integer), CType(CType(26, Byte), Integer))
        Me.formsbtn.HoverState.Parent = Me.formsbtn
        Me.formsbtn.Image = Global.dashboard.My.Resources.Resources.documents
        Me.formsbtn.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.formsbtn.ImageSize = New System.Drawing.Size(28, 28)
        Me.formsbtn.Location = New System.Drawing.Point(0, 66)
        Me.formsbtn.Name = "formsbtn"
        Me.formsbtn.ShadowDecoration.Parent = Me.formsbtn
        Me.formsbtn.Size = New System.Drawing.Size(178, 40)
        Me.formsbtn.TabIndex = 0
        Me.formsbtn.Text = "Forms"
        Me.formsbtn.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        '
        'Guna2Elipse1
        '
        Me.Guna2Elipse1.TargetControl = Me
        '
        'registrybtn
        '
        Me.registrybtn.CheckedState.Parent = Me.registrybtn
        Me.registrybtn.CustomBorderColor = System.Drawing.Color.FromArgb(CType(CType(14, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(90, Byte), Integer))
        Me.registrybtn.CustomBorderThickness = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.registrybtn.CustomImages.Parent = Me.registrybtn
        Me.registrybtn.FillColor = System.Drawing.Color.FromArgb(CType(CType(14, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(90, Byte), Integer))
        Me.registrybtn.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.registrybtn.ForeColor = System.Drawing.Color.White
        Me.registrybtn.HoverState.CustomBorderColor = System.Drawing.Color.White
        Me.registrybtn.HoverState.FillColor = System.Drawing.Color.FromArgb(CType(CType(2, Byte), Integer), CType(CType(15, Byte), Integer), CType(CType(26, Byte), Integer))
        Me.registrybtn.HoverState.Parent = Me.registrybtn
        Me.registrybtn.Image = Global.dashboard.My.Resources.Resources.registry
        Me.registrybtn.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.registrybtn.ImageSize = New System.Drawing.Size(28, 28)
        Me.registrybtn.Location = New System.Drawing.Point(0, 186)
        Me.registrybtn.Name = "registrybtn"
        Me.registrybtn.ShadowDecoration.Parent = Me.registrybtn
        Me.registrybtn.Size = New System.Drawing.Size(178, 40)
        Me.registrybtn.TabIndex = 1
        Me.registrybtn.Text = "Deceased Registry"
        Me.registrybtn.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        '
        'mapbtn
        '
        Me.mapbtn.CheckedState.Parent = Me.mapbtn
        Me.mapbtn.CustomBorderColor = System.Drawing.Color.FromArgb(CType(CType(14, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(90, Byte), Integer))
        Me.mapbtn.CustomBorderThickness = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.mapbtn.CustomImages.Parent = Me.mapbtn
        Me.mapbtn.FillColor = System.Drawing.Color.FromArgb(CType(CType(14, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(90, Byte), Integer))
        Me.mapbtn.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.mapbtn.ForeColor = System.Drawing.Color.White
        Me.mapbtn.HoverState.CustomBorderColor = System.Drawing.Color.White
        Me.mapbtn.HoverState.FillColor = System.Drawing.Color.FromArgb(CType(CType(2, Byte), Integer), CType(CType(15, Byte), Integer), CType(CType(26, Byte), Integer))
        Me.mapbtn.HoverState.Parent = Me.mapbtn
        Me.mapbtn.Image = Global.dashboard.My.Resources.Resources.map
        Me.mapbtn.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.mapbtn.ImageSize = New System.Drawing.Size(28, 28)
        Me.mapbtn.Location = New System.Drawing.Point(0, 146)
        Me.mapbtn.Name = "mapbtn"
        Me.mapbtn.ShadowDecoration.Parent = Me.mapbtn
        Me.mapbtn.Size = New System.Drawing.Size(178, 40)
        Me.mapbtn.TabIndex = 2
        Me.mapbtn.Text = "Cemetery Map"
        Me.mapbtn.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        '
        'payreservbtn
        '
        Me.payreservbtn.CheckedState.Parent = Me.payreservbtn
        Me.payreservbtn.CustomBorderColor = System.Drawing.Color.FromArgb(CType(CType(14, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(90, Byte), Integer))
        Me.payreservbtn.CustomBorderThickness = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.payreservbtn.CustomImages.Parent = Me.payreservbtn
        Me.payreservbtn.FillColor = System.Drawing.Color.FromArgb(CType(CType(14, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(90, Byte), Integer))
        Me.payreservbtn.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.payreservbtn.ForeColor = System.Drawing.Color.White
        Me.payreservbtn.HoverState.CustomBorderColor = System.Drawing.Color.White
        Me.payreservbtn.HoverState.FillColor = System.Drawing.Color.FromArgb(CType(CType(2, Byte), Integer), CType(CType(15, Byte), Integer), CType(CType(26, Byte), Integer))
        Me.payreservbtn.HoverState.Parent = Me.payreservbtn
        Me.payreservbtn.Image = Global.dashboard.My.Resources.Resources.reservation1
        Me.payreservbtn.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.payreservbtn.ImageSize = New System.Drawing.Size(28, 28)
        Me.payreservbtn.Location = New System.Drawing.Point(0, 106)
        Me.payreservbtn.Name = "payreservbtn"
        Me.payreservbtn.ShadowDecoration.Parent = Me.payreservbtn
        Me.payreservbtn.Size = New System.Drawing.Size(178, 40)
        Me.payreservbtn.TabIndex = 3
        Me.payreservbtn.Text = "Payment/Reservation"
        Me.payreservbtn.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        '
        'Guna2Panel2
        '
        Me.Guna2Panel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(171, Byte), Integer), CType(CType(189, Byte), Integer))
        Me.Guna2Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Guna2Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Guna2Panel2.Name = "Guna2Panel2"
        Me.Guna2Panel2.ShadowDecoration.BorderRadius = 10
        Me.Guna2Panel2.ShadowDecoration.Enabled = True
        Me.Guna2Panel2.ShadowDecoration.Parent = Me.Guna2Panel2
        Me.Guna2Panel2.ShadowDecoration.Shadow = New System.Windows.Forms.Padding(0, 0, 0, 5)
        Me.Guna2Panel2.Size = New System.Drawing.Size(178, 51)
        Me.Guna2Panel2.TabIndex = 4
        '
        'Guna2Panel3
        '
        Me.Guna2Panel3.BackColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(171, Byte), Integer), CType(CType(189, Byte), Integer))
        Me.Guna2Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Guna2Panel3.Location = New System.Drawing.Point(178, 0)
        Me.Guna2Panel3.Name = "Guna2Panel3"
        Me.Guna2Panel3.ShadowDecoration.BorderRadius = 10
        Me.Guna2Panel3.ShadowDecoration.Enabled = True
        Me.Guna2Panel3.ShadowDecoration.Parent = Me.Guna2Panel3
        Me.Guna2Panel3.ShadowDecoration.Shadow = New System.Windows.Forms.Padding(0, 0, 0, 5)
        Me.Guna2Panel3.Size = New System.Drawing.Size(692, 51)
        Me.Guna2Panel3.TabIndex = 5
        '
        'dashboard
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(870, 520)
        Me.Controls.Add(Me.Guna2Panel3)
        Me.Controls.Add(Me.Guna2Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "dashboard"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Dashboard"
        Me.Guna2Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Guna2Panel1 As Guna.UI2.WinForms.Guna2Panel
    Friend WithEvents formsbtn As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents Guna2Elipse1 As Guna.UI2.WinForms.Guna2Elipse
    Friend WithEvents payreservbtn As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents mapbtn As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents registrybtn As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents Guna2Panel2 As Guna.UI2.WinForms.Guna2Panel
    Friend WithEvents Guna2Panel3 As Guna.UI2.WinForms.Guna2Panel
End Class
