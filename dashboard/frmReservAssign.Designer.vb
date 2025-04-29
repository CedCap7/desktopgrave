<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmReservAssign
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Guna2Panel1 = New Guna.UI2.WinForms.Guna2Panel()
        Me.btnCancel = New Guna.UI2.WinForms.Guna2Button()
        Me.LblPlotReserved = New System.Windows.Forms.Label()
        Me.LblClient = New System.Windows.Forms.Label()
        Me.btnAssign = New Guna.UI2.WinForms.Guna2Button()
        Me.cmbDeceased = New Guna.UI2.WinForms.Guna2ComboBox()
        Me.Panel1.SuspendLayout()
        Me.Guna2Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Roboto", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.Control
        Me.Label1.Location = New System.Drawing.Point(13, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(298, 29)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Deceased Allocation Form"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopCenter
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
        Me.Panel1.TabIndex = 1
        '
        'Label5
        '
        Me.Label5.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom), System.Windows.Forms.AnchorStyles)
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Roboto Condensed", 9.75!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.Control
        Me.Label5.Location = New System.Drawing.Point(14, 38)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(272, 17)
        Me.Label5.TabIndex = 13
        Me.Label5.Text = "Assign the deceased to a reserved plot from a client."
        '
        'Guna2Panel1
        '
        Me.Guna2Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(14, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(90, Byte), Integer))
        Me.Guna2Panel1.Controls.Add(Me.btnCancel)
        Me.Guna2Panel1.Controls.Add(Me.LblPlotReserved)
        Me.Guna2Panel1.Controls.Add(Me.LblClient)
        Me.Guna2Panel1.Controls.Add(Me.btnAssign)
        Me.Guna2Panel1.Controls.Add(Me.cmbDeceased)
        Me.Guna2Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Guna2Panel1.Location = New System.Drawing.Point(0, 60)
        Me.Guna2Panel1.Name = "Guna2Panel1"
        Me.Guna2Panel1.Size = New System.Drawing.Size(525, 232)
        Me.Guna2Panel1.TabIndex = 2
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
        'LblPlotReserved
        '
        Me.LblPlotReserved.AutoSize = True
        Me.LblPlotReserved.Font = New System.Drawing.Font("Roboto", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblPlotReserved.ForeColor = System.Drawing.SystemColors.Control
        Me.LblPlotReserved.Location = New System.Drawing.Point(14, 63)
        Me.LblPlotReserved.Name = "LblPlotReserved"
        Me.LblPlotReserved.Size = New System.Drawing.Size(114, 20)
        Me.LblPlotReserved.TabIndex = 19
        Me.LblPlotReserved.Text = "Reserved Plot:"
        '
        'LblClient
        '
        Me.LblClient.AutoSize = True
        Me.LblClient.Font = New System.Drawing.Font("Roboto", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblClient.ForeColor = System.Drawing.SystemColors.Control
        Me.LblClient.Location = New System.Drawing.Point(13, 33)
        Me.LblClient.Name = "LblClient"
        Me.LblClient.Size = New System.Drawing.Size(55, 20)
        Me.LblClient.TabIndex = 17
        Me.LblClient.Text = "Client:"
        '
        'btnAssign
        '
        Me.btnAssign.BackColor = System.Drawing.Color.FromArgb(CType(CType(14, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(90, Byte), Integer))
        Me.btnAssign.BorderRadius = 17
        Me.btnAssign.BorderThickness = 1
        Me.btnAssign.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnAssign.FillColor = System.Drawing.Color.LimeGreen
        Me.btnAssign.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAssign.ForeColor = System.Drawing.Color.White
        Me.btnAssign.Location = New System.Drawing.Point(88, 184)
        Me.btnAssign.Margin = New System.Windows.Forms.Padding(2)
        Me.btnAssign.Name = "btnAssign"
        Me.btnAssign.Size = New System.Drawing.Size(167, 37)
        Me.btnAssign.TabIndex = 16
        Me.btnAssign.Text = "Assign"
        '
        'cmbDeceased
        '
        Me.cmbDeceased.AccessibleDescription = ""
        Me.cmbDeceased.AccessibleName = ""
        Me.cmbDeceased.BackColor = System.Drawing.Color.Transparent
        Me.cmbDeceased.BorderColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(171, Byte), Integer), CType(CType(189, Byte), Integer))
        Me.cmbDeceased.BorderRadius = 5
        Me.cmbDeceased.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cmbDeceased.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbDeceased.FocusedColor = System.Drawing.Color.Empty
        Me.cmbDeceased.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbDeceased.ForeColor = System.Drawing.Color.Black
        Me.cmbDeceased.FormattingEnabled = True
        Me.cmbDeceased.ItemHeight = 30
        Me.cmbDeceased.Location = New System.Drawing.Point(11, 100)
        Me.cmbDeceased.Margin = New System.Windows.Forms.Padding(2)
        Me.cmbDeceased.Name = "cmbDeceased"
        Me.cmbDeceased.Size = New System.Drawing.Size(503, 36)
        Me.cmbDeceased.TabIndex = 7
        Me.cmbDeceased.Tag = ""
        '
        'frmReservAssign
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(23, Byte), Integer), CType(CType(107, Byte), Integer), CType(CType(135, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(525, 292)
        Me.Controls.Add(Me.Guna2Panel1)
        Me.Controls.Add(Me.Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "frmReservAssign"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "frmReservAssign"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Guna2Panel1.ResumeLayout(False)
        Me.Guna2Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Label1 As Label
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Guna2Panel1 As Guna.UI2.WinForms.Guna2Panel
    Private WithEvents cmbDeceased As Guna.UI2.WinForms.Guna2ComboBox
    Private WithEvents btnAssign As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents LblPlotReserved As Label
    Friend WithEvents LblClient As Label
    Friend WithEvents Label5 As Label
    Private WithEvents btnCancel As Guna.UI2.WinForms.Guna2Button
End Class
