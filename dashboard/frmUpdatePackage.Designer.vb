<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmUpdatePackage
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
        Me.lblPackagePrice = New System.Windows.Forms.Label()
        Me.lblDescription = New System.Windows.Forms.Label()
        Me.txtPrice = New Guna.UI2.WinForms.Guna2TextBox()
        Me.btnCancel = New Guna.UI2.WinForms.Guna2Button()
        Me.btnConfirm = New Guna.UI2.WinForms.Guna2Button()
        Me.SuspendLayout()
        '
        'lblPackagePrice
        '
        Me.lblPackagePrice.Font = New System.Drawing.Font("Roboto", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPackagePrice.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.lblPackagePrice.Location = New System.Drawing.Point(11, 9)
        Me.lblPackagePrice.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblPackagePrice.Name = "lblPackagePrice"
        Me.lblPackagePrice.Size = New System.Drawing.Size(255, 24)
        Me.lblPackagePrice.TabIndex = 0
        Me.lblPackagePrice.Text = "Package Name"
        '
        'lblDescription
        '
        Me.lblDescription.Font = New System.Drawing.Font("Roboto", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDescription.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.lblDescription.Location = New System.Drawing.Point(22, 98)
        Me.lblDescription.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblDescription.Name = "lblDescription"
        Me.lblDescription.Size = New System.Drawing.Size(255, 49)
        Me.lblDescription.TabIndex = 2
        Me.lblDescription.Text = "Description:"
        '
        'txtPrice
        '
        Me.txtPrice.Animated = True
        Me.txtPrice.BorderColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(171, Byte), Integer), CType(CType(189, Byte), Integer))
        Me.txtPrice.BorderRadius = 5
        Me.txtPrice.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPrice.DefaultText = ""
        Me.txtPrice.DisabledState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.txtPrice.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer))
        Me.txtPrice.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.txtPrice.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.txtPrice.FocusedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtPrice.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPrice.ForeColor = System.Drawing.Color.Black
        Me.txtPrice.HoverState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtPrice.Location = New System.Drawing.Point(26, 53)
        Me.txtPrice.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtPrice.Name = "txtPrice"
        Me.txtPrice.PlaceholderForeColor = System.Drawing.Color.Gray
        Me.txtPrice.PlaceholderText = "Input Amount"
        Me.txtPrice.SelectedText = ""
        Me.txtPrice.Size = New System.Drawing.Size(251, 40)
        Me.txtPrice.TabIndex = 30
        Me.txtPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCancel.AutoRoundedCorners = True
        Me.btnCancel.BackColor = System.Drawing.Color.Transparent
        Me.btnCancel.BorderRadius = 17
        Me.btnCancel.BorderThickness = 1
        Me.btnCancel.FillColor = System.Drawing.Color.Brown
        Me.btnCancel.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold)
        Me.btnCancel.ForeColor = System.Drawing.Color.White
        Me.btnCancel.Location = New System.Drawing.Point(2, 167)
        Me.btnCancel.Margin = New System.Windows.Forms.Padding(2, 2, 2, 10)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(146, 37)
        Me.btnCancel.TabIndex = 33
        Me.btnCancel.Text = "Cancel"
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
        Me.btnConfirm.Location = New System.Drawing.Point(152, 167)
        Me.btnConfirm.Margin = New System.Windows.Forms.Padding(2, 2, 2, 10)
        Me.btnConfirm.Name = "btnConfirm"
        Me.btnConfirm.Size = New System.Drawing.Size(146, 37)
        Me.btnConfirm.TabIndex = 32
        Me.btnConfirm.Text = "Confirm"
        '
        'frmUpdatePackage
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(23, Byte), Integer), CType(CType(107, Byte), Integer), CType(CType(135, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(300, 211)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnConfirm)
        Me.Controls.Add(Me.txtPrice)
        Me.Controls.Add(Me.lblDescription)
        Me.Controls.Add(Me.lblPackagePrice)
        Me.ForeColor = System.Drawing.Color.White
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmUpdatePackage"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Update Package Price"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents lblPackagePrice As System.Windows.Forms.Label
    Friend WithEvents lblDescription As System.Windows.Forms.Label
    Private WithEvents txtPrice As Guna.UI2.WinForms.Guna2TextBox
    Private WithEvents btnCancel As Guna.UI2.WinForms.Guna2Button
    Private WithEvents btnConfirm As Guna.UI2.WinForms.Guna2Button
End Class
