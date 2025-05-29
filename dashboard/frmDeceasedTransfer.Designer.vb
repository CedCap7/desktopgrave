<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDeceasedTransfer
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
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Guna2Panel1 = New Guna.UI2.WinForms.Guna2Panel()
        Me.LblCurrentPlot = New System.Windows.Forms.Label()
        Me.btnCancel = New Guna.UI2.WinForms.Guna2Button()
        Me.LblDeceased = New System.Windows.Forms.Label()
        Me.btnTransfer = New Guna.UI2.WinForms.Guna2Button()
        Me.cmbPlots = New Guna.UI2.WinForms.Guna2ComboBox()
        Me.Panel1.SuspendLayout()
        Me.Guna2Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(14, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(90, Byte), Integer))
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(525, 60)
        Me.Panel1.TabIndex = 3
        '
        'Label5
        '
        Me.Label5.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom), System.Windows.Forms.AnchorStyles)
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Roboto Condensed", 9.75!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.Control
        Me.Label5.Location = New System.Drawing.Point(14, 38)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(290, 17)
        Me.Label5.TabIndex = 13
        Me.Label5.Text = "Transfer the deceased to a purchased plot from a client."
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Roboto", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.Control
        Me.Label1.Location = New System.Drawing.Point(13, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(304, 29)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Deceased Relocation Form"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Guna2Panel1
        '
        Me.Guna2Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(14, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(90, Byte), Integer))
        Me.Guna2Panel1.Controls.Add(Me.LblCurrentPlot)
        Me.Guna2Panel1.Controls.Add(Me.btnCancel)
        Me.Guna2Panel1.Controls.Add(Me.LblDeceased)
        Me.Guna2Panel1.Controls.Add(Me.btnTransfer)
        Me.Guna2Panel1.Controls.Add(Me.cmbPlots)
        Me.Guna2Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Guna2Panel1.Location = New System.Drawing.Point(0, 60)
        Me.Guna2Panel1.Name = "Guna2Panel1"
        Me.Guna2Panel1.Size = New System.Drawing.Size(525, 232)
        Me.Guna2Panel1.TabIndex = 4
        '
        'LblCurrentPlot
        '
        Me.LblCurrentPlot.AutoSize = True
        Me.LblCurrentPlot.Font = New System.Drawing.Font("Roboto", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblCurrentPlot.ForeColor = System.Drawing.SystemColors.Control
        Me.LblCurrentPlot.Location = New System.Drawing.Point(14, 60)
        Me.LblCurrentPlot.Name = "LblCurrentPlot"
        Me.LblCurrentPlot.Size = New System.Drawing.Size(42, 20)
        Me.LblCurrentPlot.TabIndex = 21
        Me.LblCurrentPlot.Text = "Plot:"
        '
        'btnCancel
        '
        Me.btnCancel.BackColor = System.Drawing.Color.FromArgb(CType(CType(14, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(90, Byte), Integer))
        Me.btnCancel.BorderRadius = 17
        Me.btnCancel.BorderThickness = 1
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.FillColor = System.Drawing.Color.LimeGreen
        Me.btnCancel.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCancel.ForeColor = System.Drawing.Color.White
        Me.btnCancel.Location = New System.Drawing.Point(259, 184)
        Me.btnCancel.Margin = New System.Windows.Forms.Padding(2)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(167, 37)
        Me.btnCancel.TabIndex = 20
        Me.btnCancel.Text = "Cancel"
        '
        'LblDeceased
        '
        Me.LblDeceased.AutoSize = True
        Me.LblDeceased.Font = New System.Drawing.Font("Roboto", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblDeceased.ForeColor = System.Drawing.SystemColors.Control
        Me.LblDeceased.Location = New System.Drawing.Point(14, 30)
        Me.LblDeceased.Name = "LblDeceased"
        Me.LblDeceased.Size = New System.Drawing.Size(42, 20)
        Me.LblDeceased.TabIndex = 19
        Me.LblDeceased.Text = "Plot:"
        '
        'btnTransfer
        '
        Me.btnTransfer.BackColor = System.Drawing.Color.FromArgb(CType(CType(14, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(90, Byte), Integer))
        Me.btnTransfer.BorderRadius = 17
        Me.btnTransfer.BorderThickness = 1
        Me.btnTransfer.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnTransfer.FillColor = System.Drawing.Color.LimeGreen
        Me.btnTransfer.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnTransfer.ForeColor = System.Drawing.Color.White
        Me.btnTransfer.Location = New System.Drawing.Point(88, 184)
        Me.btnTransfer.Margin = New System.Windows.Forms.Padding(2)
        Me.btnTransfer.Name = "btnTransfer"
        Me.btnTransfer.Size = New System.Drawing.Size(167, 37)
        Me.btnTransfer.TabIndex = 16
        Me.btnTransfer.Text = "Transfer"
        '
        'cmbPlots
        '
        Me.cmbPlots.AccessibleDescription = ""
        Me.cmbPlots.AccessibleName = ""
        Me.cmbPlots.BackColor = System.Drawing.Color.Transparent
        Me.cmbPlots.BorderColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(171, Byte), Integer), CType(CType(189, Byte), Integer))
        Me.cmbPlots.BorderRadius = 5
        Me.cmbPlots.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cmbPlots.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbPlots.FocusedColor = System.Drawing.Color.Empty
        Me.cmbPlots.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbPlots.ForeColor = System.Drawing.Color.Black
        Me.cmbPlots.FormattingEnabled = True
        Me.cmbPlots.ItemHeight = 30
        Me.cmbPlots.Location = New System.Drawing.Point(11, 100)
        Me.cmbPlots.Margin = New System.Windows.Forms.Padding(2)
        Me.cmbPlots.Name = "cmbPlots"
        Me.cmbPlots.Size = New System.Drawing.Size(503, 36)
        Me.cmbPlots.TabIndex = 7
        Me.cmbPlots.Tag = ""
        '
        'frmDeceasedTransfer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(525, 292)
        Me.Controls.Add(Me.Guna2Panel1)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "frmDeceasedTransfer"
        Me.Text = "frmDeceasedTransfer"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Guna2Panel1.ResumeLayout(False)
        Me.Guna2Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents Label5 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Guna2Panel1 As Guna.UI2.WinForms.Guna2Panel
    Friend WithEvents LblCurrentPlot As Label
    Private WithEvents btnCancel As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents LblDeceased As Label
    Private WithEvents btnTransfer As Guna.UI2.WinForms.Guna2Button
    Private WithEvents cmbPlots As Guna.UI2.WinForms.Guna2ComboBox
End Class
