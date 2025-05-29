<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmViewPayment
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmViewPayment))
        Me.Guna2Panel2 = New Guna.UI2.WinForms.Guna2Panel()
        Me.btnPayment = New Guna.UI2.WinForms.Guna2Button()
        Me.btnShowAll = New Guna.UI2.WinForms.Guna2Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Guna2ShadowPanel1 = New Guna.UI2.WinForms.Guna2ShadowPanel()
        Me.lblTotalReserved = New System.Windows.Forms.Label()
        Me.lblOverallAmount = New System.Windows.Forms.Label()
        Me.lblOverallBal = New System.Windows.Forms.Label()
        Me.lblName = New System.Windows.Forms.Label()
        Me.Guna2ShadowPanel2 = New Guna.UI2.WinForms.Guna2ShadowPanel()
        Me.ReservAccount = New System.Windows.Forms.ListView()
        Me.id = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.plot = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.total_Paid = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.total_Amount = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.balance = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.payment_status = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.datepurchased = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Guna2ShadowPanel3 = New Guna.UI2.WinForms.Guna2ShadowPanel()
        Me.lstPaymentHistory = New System.Windows.Forms.ListView()
        Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader3 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Label5 = New System.Windows.Forms.Label()
        Me.btnExport = New Guna.UI2.WinForms.Guna2Button()
        Me.Guna2Panel2.SuspendLayout()
        Me.Guna2ShadowPanel1.SuspendLayout()
        Me.Guna2ShadowPanel2.SuspendLayout()
        Me.Guna2ShadowPanel3.SuspendLayout()
        Me.SuspendLayout()
        '
        'Guna2Panel2
        '
        Me.Guna2Panel2.AutoSize = True
        Me.Guna2Panel2.Controls.Add(Me.btnExport)
        Me.Guna2Panel2.Controls.Add(Me.btnPayment)
        Me.Guna2Panel2.Controls.Add(Me.btnShowAll)
        Me.Guna2Panel2.Controls.Add(Me.Label2)
        Me.Guna2Panel2.Controls.Add(Me.Label1)
        Me.Guna2Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Guna2Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Guna2Panel2.Name = "Guna2Panel2"
        Me.Guna2Panel2.Size = New System.Drawing.Size(1366, 81)
        Me.Guna2Panel2.TabIndex = 5
        '
        'btnPayment
        '
        Me.btnPayment.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.btnPayment.Animated = True
        Me.btnPayment.FillColor = System.Drawing.Color.LimeGreen
        Me.btnPayment.Font = New System.Drawing.Font("Segoe UI", 9.75!)
        Me.btnPayment.ForeColor = System.Drawing.Color.White
        Me.btnPayment.Location = New System.Drawing.Point(1279, 44)
        Me.btnPayment.Name = "btnPayment"
        Me.btnPayment.Size = New System.Drawing.Size(75, 25)
        Me.btnPayment.TabIndex = 7
        Me.btnPayment.Text = "Pay"
        '
        'btnShowAll
        '
        Me.btnShowAll.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.btnShowAll.Animated = True
        Me.btnShowAll.FillColor = System.Drawing.Color.DarkGoldenrod
        Me.btnShowAll.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnShowAll.ForeColor = System.Drawing.Color.White
        Me.btnShowAll.Location = New System.Drawing.Point(1198, 44)
        Me.btnShowAll.Name = "btnShowAll"
        Me.btnShowAll.Size = New System.Drawing.Size(75, 25)
        Me.btnShowAll.TabIndex = 6
        Me.btnShowAll.Text = "Refresh"
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Roboto Condensed", 11.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.Control
        Me.Label2.Location = New System.Drawing.Point(16, 48)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(172, 19)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "List of client accountabilities"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Roboto", 24.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.Control
        Me.Label1.Location = New System.Drawing.Point(13, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(412, 39)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Client Payment Information"
        '
        'Guna2ShadowPanel1
        '
        Me.Guna2ShadowPanel1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Guna2ShadowPanel1.BackColor = System.Drawing.Color.Transparent
        Me.Guna2ShadowPanel1.Controls.Add(Me.lblTotalReserved)
        Me.Guna2ShadowPanel1.Controls.Add(Me.lblOverallAmount)
        Me.Guna2ShadowPanel1.Controls.Add(Me.lblOverallBal)
        Me.Guna2ShadowPanel1.Controls.Add(Me.lblName)
        Me.Guna2ShadowPanel1.FillColor = System.Drawing.Color.FromArgb(CType(CType(14, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(90, Byte), Integer))
        Me.Guna2ShadowPanel1.Location = New System.Drawing.Point(0, 123)
        Me.Guna2ShadowPanel1.Name = "Guna2ShadowPanel1"
        Me.Guna2ShadowPanel1.Radius = 10
        Me.Guna2ShadowPanel1.ShadowColor = System.Drawing.Color.Black
        Me.Guna2ShadowPanel1.Size = New System.Drawing.Size(899, 169)
        Me.Guna2ShadowPanel1.TabIndex = 6
        '
        'lblTotalReserved
        '
        Me.lblTotalReserved.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.lblTotalReserved.AutoSize = True
        Me.lblTotalReserved.Font = New System.Drawing.Font("Roboto", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotalReserved.ForeColor = System.Drawing.SystemColors.Control
        Me.lblTotalReserved.Location = New System.Drawing.Point(45, 59)
        Me.lblTotalReserved.Name = "lblTotalReserved"
        Me.lblTotalReserved.Size = New System.Drawing.Size(158, 20)
        Me.lblTotalReserved.TabIndex = 12
        Me.lblTotalReserved.Text = "Total Plots Reserved:"
        '
        'lblOverallAmount
        '
        Me.lblOverallAmount.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.lblOverallAmount.AutoSize = True
        Me.lblOverallAmount.Font = New System.Drawing.Font("Roboto", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblOverallAmount.ForeColor = System.Drawing.SystemColors.Control
        Me.lblOverallAmount.Location = New System.Drawing.Point(450, 89)
        Me.lblOverallAmount.Name = "lblOverallAmount"
        Me.lblOverallAmount.Size = New System.Drawing.Size(122, 20)
        Me.lblOverallAmount.TabIndex = 11
        Me.lblOverallAmount.Text = "Overall Amount:"
        '
        'lblOverallBal
        '
        Me.lblOverallBal.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.lblOverallBal.AutoSize = True
        Me.lblOverallBal.Font = New System.Drawing.Font("Roboto", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblOverallBal.ForeColor = System.Drawing.SystemColors.Control
        Me.lblOverallBal.Location = New System.Drawing.Point(450, 59)
        Me.lblOverallBal.Name = "lblOverallBal"
        Me.lblOverallBal.Size = New System.Drawing.Size(128, 20)
        Me.lblOverallBal.TabIndex = 10
        Me.lblOverallBal.Text = "Overrall Balance:"
        '
        'lblName
        '
        Me.lblName.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.lblName.AutoSize = True
        Me.lblName.Font = New System.Drawing.Font("Roboto", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblName.ForeColor = System.Drawing.SystemColors.Control
        Me.lblName.Location = New System.Drawing.Point(45, 21)
        Me.lblName.Name = "lblName"
        Me.lblName.Size = New System.Drawing.Size(64, 24)
        Me.lblName.TabIndex = 9
        Me.lblName.Text = "Name:"
        '
        'Guna2ShadowPanel2
        '
        Me.Guna2ShadowPanel2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Guna2ShadowPanel2.BackColor = System.Drawing.Color.Transparent
        Me.Guna2ShadowPanel2.Controls.Add(Me.ReservAccount)
        Me.Guna2ShadowPanel2.FillColor = System.Drawing.Color.FromArgb(CType(CType(14, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(90, Byte), Integer))
        Me.Guna2ShadowPanel2.Location = New System.Drawing.Point(0, 330)
        Me.Guna2ShadowPanel2.Name = "Guna2ShadowPanel2"
        Me.Guna2ShadowPanel2.Radius = 10
        Me.Guna2ShadowPanel2.ShadowColor = System.Drawing.Color.Black
        Me.Guna2ShadowPanel2.Size = New System.Drawing.Size(899, 218)
        Me.Guna2ShadowPanel2.TabIndex = 7
        '
        'ReservAccount
        '
        Me.ReservAccount.AllowColumnReorder = True
        Me.ReservAccount.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ReservAccount.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.ReservAccount.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.ReservAccount.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.id, Me.plot, Me.total_Paid, Me.total_Amount, Me.balance, Me.payment_status, Me.datepurchased})
        Me.ReservAccount.Font = New System.Drawing.Font("Roboto", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ReservAccount.ForeColor = System.Drawing.SystemColors.Window
        Me.ReservAccount.FullRowSelect = True
        Me.ReservAccount.GridLines = True
        Me.ReservAccount.HideSelection = False
        Me.ReservAccount.Location = New System.Drawing.Point(20, 20)
        Me.ReservAccount.Margin = New System.Windows.Forms.Padding(20)
        Me.ReservAccount.Name = "ReservAccount"
        Me.ReservAccount.ShowItemToolTips = True
        Me.ReservAccount.Size = New System.Drawing.Size(859, 178)
        Me.ReservAccount.TabIndex = 2
        Me.ReservAccount.TileSize = New System.Drawing.Size(10, 10)
        Me.ReservAccount.UseCompatibleStateImageBehavior = False
        Me.ReservAccount.View = System.Windows.Forms.View.Details
        '
        'id
        '
        Me.id.Text = "ID"
        Me.id.Width = 50
        '
        'plot
        '
        Me.plot.Text = "Reserved Plot"
        Me.plot.Width = 270
        '
        'total_Paid
        '
        Me.total_Paid.Text = "Total Paid"
        Me.total_Paid.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.total_Paid.Width = 120
        '
        'total_Amount
        '
        Me.total_Amount.Text = "Total Amount"
        Me.total_Amount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.total_Amount.Width = 120
        '
        'balance
        '
        Me.balance.Text = "Remaining Balance"
        Me.balance.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.balance.Width = 120
        '
        'payment_status
        '
        Me.payment_status.Text = "Payment Status"
        Me.payment_status.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.payment_status.Width = 120
        '
        'datepurchased
        '
        Me.datepurchased.Text = "Date Purchased"
        Me.datepurchased.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.datepurchased.Width = 170
        '
        'Label3
        '
        Me.Label3.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Roboto", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.Control
        Me.Label3.Location = New System.Drawing.Point(27, 94)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(68, 26)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "Client"
        '
        'Label4
        '
        Me.Label4.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Roboto", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.Control
        Me.Label4.Location = New System.Drawing.Point(27, 301)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(262, 26)
        Me.Label4.TabIndex = 8
        Me.Label4.Text = "Plots and Accountabilities"
        '
        'Guna2ShadowPanel3
        '
        Me.Guna2ShadowPanel3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Guna2ShadowPanel3.BackColor = System.Drawing.Color.Transparent
        Me.Guna2ShadowPanel3.Controls.Add(Me.lstPaymentHistory)
        Me.Guna2ShadowPanel3.FillColor = System.Drawing.Color.FromArgb(CType(CType(14, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(90, Byte), Integer))
        Me.Guna2ShadowPanel3.Location = New System.Drawing.Point(905, 123)
        Me.Guna2ShadowPanel3.Name = "Guna2ShadowPanel3"
        Me.Guna2ShadowPanel3.Radius = 10
        Me.Guna2ShadowPanel3.ShadowColor = System.Drawing.Color.Black
        Me.Guna2ShadowPanel3.Size = New System.Drawing.Size(449, 425)
        Me.Guna2ShadowPanel3.TabIndex = 8
        '
        'lstPaymentHistory
        '
        Me.lstPaymentHistory.AllowColumnReorder = True
        Me.lstPaymentHistory.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lstPaymentHistory.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.lstPaymentHistory.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.lstPaymentHistory.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader2, Me.ColumnHeader3})
        Me.lstPaymentHistory.Font = New System.Drawing.Font("Roboto", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstPaymentHistory.ForeColor = System.Drawing.SystemColors.Window
        Me.lstPaymentHistory.FullRowSelect = True
        Me.lstPaymentHistory.GridLines = True
        Me.lstPaymentHistory.HideSelection = False
        Me.lstPaymentHistory.Location = New System.Drawing.Point(20, 20)
        Me.lstPaymentHistory.Margin = New System.Windows.Forms.Padding(20)
        Me.lstPaymentHistory.Name = "lstPaymentHistory"
        Me.lstPaymentHistory.ShowItemToolTips = True
        Me.lstPaymentHistory.Size = New System.Drawing.Size(409, 385)
        Me.lstPaymentHistory.TabIndex = 2
        Me.lstPaymentHistory.TileSize = New System.Drawing.Size(10, 10)
        Me.lstPaymentHistory.UseCompatibleStateImageBehavior = False
        Me.lstPaymentHistory.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "Official Receipt No."
        Me.ColumnHeader1.Width = 150
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "Amount"
        Me.ColumnHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.ColumnHeader2.Width = 130
        '
        'ColumnHeader3
        '
        Me.ColumnHeader3.Text = "Date"
        Me.ColumnHeader3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.ColumnHeader3.Width = 130
        '
        'Label5
        '
        Me.Label5.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Roboto", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.Control
        Me.Label5.Location = New System.Drawing.Point(929, 94)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(171, 26)
        Me.Label5.TabIndex = 9
        Me.Label5.Text = "Payment History"
        '
        'btnExport
        '
        Me.btnExport.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.btnExport.Animated = True
        Me.btnExport.FillColor = System.Drawing.Color.DarkOrange
        Me.btnExport.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnExport.ForeColor = System.Drawing.Color.White
        Me.btnExport.Image = Global.dashboard.My.Resources.Resources.pdf
        Me.btnExport.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.btnExport.ImageOffset = New System.Drawing.Point(-5, 0)
        Me.btnExport.Location = New System.Drawing.Point(1070, 44)
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Size = New System.Drawing.Size(122, 25)
        Me.btnExport.TabIndex = 8
        Me.btnExport.Text = "Export to PDF"
        Me.btnExport.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.btnExport.TextOffset = New System.Drawing.Point(2, 0)
        '
        'frmViewPayment
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(23, Byte), Integer), CType(CType(107, Byte), Integer), CType(CType(135, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1366, 571)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Guna2ShadowPanel3)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Guna2ShadowPanel2)
        Me.Controls.Add(Me.Guna2ShadowPanel1)
        Me.Controls.Add(Me.Guna2Panel2)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmViewPayment"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "View Payment"
        Me.Guna2Panel2.ResumeLayout(False)
        Me.Guna2Panel2.PerformLayout()
        Me.Guna2ShadowPanel1.ResumeLayout(False)
        Me.Guna2ShadowPanel1.PerformLayout()
        Me.Guna2ShadowPanel2.ResumeLayout(False)
        Me.Guna2ShadowPanel3.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Guna2Panel2 As Guna.UI2.WinForms.Guna2Panel
    Friend WithEvents btnShowAll As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Guna2ShadowPanel1 As Guna.UI2.WinForms.Guna2ShadowPanel
    Friend WithEvents Guna2ShadowPanel2 As Guna.UI2.WinForms.Guna2ShadowPanel
    Friend WithEvents Label4 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents ReservAccount As ListView
    Friend WithEvents id As ColumnHeader
    Friend WithEvents plot As ColumnHeader
    Friend WithEvents total_Paid As ColumnHeader
    Friend WithEvents total_Amount As ColumnHeader
    Friend WithEvents balance As ColumnHeader
    Friend WithEvents payment_status As ColumnHeader
    Friend WithEvents lblTotalReserved As Label
    Friend WithEvents lblOverallAmount As Label
    Friend WithEvents lblOverallBal As Label
    Friend WithEvents lblName As Label
    Friend WithEvents Guna2ShadowPanel3 As Guna.UI2.WinForms.Guna2ShadowPanel
    Friend WithEvents lstPaymentHistory As ListView
    Friend WithEvents ColumnHeader1 As ColumnHeader
    Friend WithEvents ColumnHeader2 As ColumnHeader
    Friend WithEvents ColumnHeader3 As ColumnHeader
    Friend WithEvents Label5 As Label
    Friend WithEvents btnPayment As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents datepurchased As ColumnHeader
    Friend WithEvents btnExport As Guna.UI2.WinForms.Guna2Button
End Class
