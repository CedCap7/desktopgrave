<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmUpdatePayment
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmUpdatePayment))
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtPaidAmount = New Guna.UI2.WinForms.Guna2TextBox()
        Me.btnConfirm = New Guna.UI2.WinForms.Guna2Button()
        Me.btnCancel = New Guna.UI2.WinForms.Guna2Button()
        Me.lblReservationID = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lblRemainingBalance = New System.Windows.Forms.Label()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(367, 74)
        Me.Panel1.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Roboto", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.Control
        Me.Label1.Location = New System.Drawing.Point(81, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(192, 29)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Update Payment"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopCenter
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
        Me.txtPaidAmount.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPaidAmount.ForeColor = System.Drawing.Color.Black
        Me.txtPaidAmount.HoverState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtPaidAmount.Location = New System.Drawing.Point(13, 216)
        Me.txtPaidAmount.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtPaidAmount.Name = "txtPaidAmount"
        Me.txtPaidAmount.PlaceholderForeColor = System.Drawing.Color.Gray
        Me.txtPaidAmount.PlaceholderText = "Input Amount"
        Me.txtPaidAmount.SelectedText = ""
        Me.txtPaidAmount.Size = New System.Drawing.Size(341, 41)
        Me.txtPaidAmount.TabIndex = 29
        Me.txtPaidAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
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
        Me.btnConfirm.Location = New System.Drawing.Point(34, 264)
        Me.btnConfirm.Margin = New System.Windows.Forms.Padding(2, 2, 2, 10)
        Me.btnConfirm.Name = "btnConfirm"
        Me.btnConfirm.Size = New System.Drawing.Size(146, 37)
        Me.btnConfirm.TabIndex = 30
        Me.btnConfirm.Text = "Confirm"
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCancel.AutoRoundedCorners = True
        Me.btnCancel.BackColor = System.Drawing.Color.Transparent
        Me.btnCancel.BorderRadius = 17
        Me.btnCancel.BorderThickness = 1
        Me.btnCancel.FillColor = System.Drawing.Color.LimeGreen
        Me.btnCancel.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold)
        Me.btnCancel.ForeColor = System.Drawing.Color.White
        Me.btnCancel.Location = New System.Drawing.Point(184, 264)
        Me.btnCancel.Margin = New System.Windows.Forms.Padding(2, 2, 2, 10)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(146, 37)
        Me.btnCancel.TabIndex = 31
        Me.btnCancel.Text = "Cancel"
        '
        'lblReservationID
        '
        Me.lblReservationID.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom), System.Windows.Forms.AnchorStyles)
        Me.lblReservationID.AutoSize = True
        Me.lblReservationID.Font = New System.Drawing.Font("Roboto Condensed", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblReservationID.ForeColor = System.Drawing.SystemColors.Control
        Me.lblReservationID.Location = New System.Drawing.Point(9, 118)
        Me.lblReservationID.Name = "lblReservationID"
        Me.lblReservationID.Size = New System.Drawing.Size(104, 20)
        Me.lblReservationID.TabIndex = 1
        Me.lblReservationID.Text = "Reservation ID:"
        Me.lblReservationID.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Roboto", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.Control
        Me.Label2.Location = New System.Drawing.Point(9, 94)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(140, 24)
        Me.Label2.TabIndex = 32
        Me.Label2.Text = "Reservation ID:"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label3
        '
        Me.Label3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom), System.Windows.Forms.AnchorStyles)
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Roboto", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.Control
        Me.Label3.Location = New System.Drawing.Point(9, 151)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(179, 24)
        Me.Label3.TabIndex = 33
        Me.Label3.Text = "Remaining Balance:"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblRemainingBalance
        '
        Me.lblRemainingBalance.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom), System.Windows.Forms.AnchorStyles)
        Me.lblRemainingBalance.AutoSize = True
        Me.lblRemainingBalance.Font = New System.Drawing.Font("Roboto Condensed", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRemainingBalance.ForeColor = System.Drawing.SystemColors.Control
        Me.lblRemainingBalance.Location = New System.Drawing.Point(9, 175)
        Me.lblRemainingBalance.Name = "lblRemainingBalance"
        Me.lblRemainingBalance.Size = New System.Drawing.Size(135, 20)
        Me.lblRemainingBalance.TabIndex = 34
        Me.lblRemainingBalance.Text = "Remaining Balance:"
        Me.lblRemainingBalance.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'frmUpdatePayment
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(23, Byte), Integer), CType(CType(107, Byte), Integer), CType(CType(135, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(367, 311)
        Me.Controls.Add(Me.lblRemainingBalance)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.lblReservationID)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnConfirm)
        Me.Controls.Add(Me.txtPaidAmount)
        Me.Controls.Add(Me.Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmUpdatePayment"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Update Payment"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Label1 As Label
    Private WithEvents txtPaidAmount As Guna.UI2.WinForms.Guna2TextBox
    Friend WithEvents lblReservationID As Label
    Private WithEvents btnCancel As Guna.UI2.WinForms.Guna2Button
    Private WithEvents btnConfirm As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents lblRemainingBalance As Label
End Class
