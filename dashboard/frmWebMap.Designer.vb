<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmWebMap
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
        Me.Guna2Elipse1 = New Guna.UI2.WinForms.Guna2Elipse(Me.components)
        Me.WebView21 = New Microsoft.Web.WebView2.WinForms.WebView2()
        Me.btnRefreshMap = New Guna.UI2.WinForms.Guna2Button()
        CType(Me.WebView21, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Guna2Elipse1
        '
        Me.Guna2Elipse1.TargetControl = Me
        '
        'WebView21
        '
        Me.WebView21.AllowExternalDrop = True
        Me.WebView21.CreationProperties = Nothing
        Me.WebView21.DefaultBackgroundColor = System.Drawing.Color.White
        Me.WebView21.Dock = System.Windows.Forms.DockStyle.Fill
        Me.WebView21.Location = New System.Drawing.Point(0, 0)
        Me.WebView21.Name = "WebView21"
        Me.WebView21.Size = New System.Drawing.Size(660, 397)
        Me.WebView21.TabIndex = 60
        Me.WebView21.ZoomFactor = 1.0R
        '
        'btnRefreshMap
        '
        Me.btnRefreshMap.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnRefreshMap.Animated = True
        Me.btnRefreshMap.BackColor = System.Drawing.Color.Transparent
        Me.btnRefreshMap.BorderRadius = 5
        Me.btnRefreshMap.BorderThickness = 1
        Me.btnRefreshMap.DisabledState.BorderColor = System.Drawing.Color.DarkGray
        Me.btnRefreshMap.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray
        Me.btnRefreshMap.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer))
        Me.btnRefreshMap.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer))
        Me.btnRefreshMap.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.btnRefreshMap.ForeColor = System.Drawing.Color.FromArgb(CType(CType(2, Byte), Integer), CType(CType(15, Byte), Integer), CType(CType(26, Byte), Integer))
        Me.btnRefreshMap.HoverState.BorderColor = System.Drawing.Color.White
        Me.btnRefreshMap.HoverState.FillColor = System.Drawing.Color.FromArgb(CType(CType(8, Byte), Integer), CType(CType(37, Byte), Integer), CType(CType(56, Byte), Integer))
        Me.btnRefreshMap.HoverState.ForeColor = System.Drawing.SystemColors.Control
        Me.btnRefreshMap.HoverState.Image = Global.dashboard.My.Resources.Resources.refresh
        Me.btnRefreshMap.Image = Global.dashboard.My.Resources.Resources.refreshhover
        Me.btnRefreshMap.Location = New System.Drawing.Point(541, 12)
        Me.btnRefreshMap.Name = "btnRefreshMap"
        Me.btnRefreshMap.Size = New System.Drawing.Size(107, 35)
        Me.btnRefreshMap.TabIndex = 61
        Me.btnRefreshMap.Text = "Refresh"
        Me.btnRefreshMap.UseTransparentBackground = True
        '
        'frmWebMap
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(14, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(90, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(660, 397)
        Me.Controls.Add(Me.btnRefreshMap)
        Me.Controls.Add(Me.WebView21)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "frmWebMap"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "frmWebMap"
        CType(Me.WebView21, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Guna2Elipse1 As Guna.UI2.WinForms.Guna2Elipse
    Friend WithEvents WebView21 As Microsoft.Web.WebView2.WinForms.WebView2
    Friend WithEvents btnRefreshMap As Guna.UI2.WinForms.Guna2Button
End Class
