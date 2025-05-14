<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmReservationsReg
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
        Me.Guna2Panel2 = New Guna.UI2.WinForms.Guna2Panel()
        Me.btnShowAll = New Guna.UI2.WinForms.Guna2Button()
        Me.btnExportPDF = New Guna.UI2.WinForms.Guna2Button()
        Me.btnAssign = New Guna.UI2.WinForms.Guna2Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Guna2Panel1 = New Guna.UI2.WinForms.Guna2Panel()
        Me.txtSearch = New Guna.UI2.WinForms.Guna2TextBox()
        Me.ReservationList = New System.Windows.Forms.ListView()
        Me.id = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.fullName = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.plotlocation = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.dateOfReserv = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.quantity = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.status = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.payment_status = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Guna2Elipse1 = New Guna.UI2.WinForms.Guna2Elipse(Me.components)
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.Guna2Panel2.SuspendLayout()
        Me.Guna2Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Guna2Panel2
        '
        Me.Guna2Panel2.AutoSize = True
        Me.Guna2Panel2.Controls.Add(Me.btnShowAll)
        Me.Guna2Panel2.Controls.Add(Me.btnExportPDF)
        Me.Guna2Panel2.Controls.Add(Me.btnAssign)
        Me.Guna2Panel2.Controls.Add(Me.Label2)
        Me.Guna2Panel2.Controls.Add(Me.Label1)
        Me.Guna2Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Guna2Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Guna2Panel2.Name = "Guna2Panel2"
        Me.Guna2Panel2.Size = New System.Drawing.Size(888, 81)
        Me.Guna2Panel2.TabIndex = 4
        '
        'btnShowAll
        '
        Me.btnShowAll.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.btnShowAll.Animated = True
        Me.btnShowAll.FillColor = System.Drawing.Color.DarkGoldenrod
        Me.btnShowAll.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnShowAll.ForeColor = System.Drawing.Color.White
        Me.btnShowAll.Location = New System.Drawing.Point(722, 44)
        Me.btnShowAll.Name = "btnShowAll"
        Me.btnShowAll.Size = New System.Drawing.Size(75, 25)
        Me.btnShowAll.TabIndex = 6
        Me.btnShowAll.Text = "Refresh"
        '
        'btnExportPDF
        '
        Me.btnExportPDF.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.btnExportPDF.Animated = True
        Me.btnExportPDF.FillColor = System.Drawing.Color.DarkOrange
        Me.btnExportPDF.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnExportPDF.ForeColor = System.Drawing.Color.White
        Me.btnExportPDF.Image = Global.dashboard.My.Resources.Resources.pdf
        Me.btnExportPDF.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.btnExportPDF.ImageOffset = New System.Drawing.Point(-5, 0)
        Me.btnExportPDF.Location = New System.Drawing.Point(595, 44)
        Me.btnExportPDF.Name = "btnExportPDF"
        Me.btnExportPDF.Size = New System.Drawing.Size(121, 25)
        Me.btnExportPDF.TabIndex = 4
        Me.btnExportPDF.Text = "Export to PDF"
        Me.btnExportPDF.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.btnExportPDF.TextOffset = New System.Drawing.Point(2, 0)
        '
        'btnAssign
        '
        Me.btnAssign.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.btnAssign.Animated = True
        Me.btnAssign.FillColor = System.Drawing.Color.Blue
        Me.btnAssign.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAssign.ForeColor = System.Drawing.Color.White
        Me.btnAssign.Location = New System.Drawing.Point(803, 44)
        Me.btnAssign.Name = "btnAssign"
        Me.btnAssign.Size = New System.Drawing.Size(75, 25)
        Me.btnAssign.TabIndex = 2
        Me.btnAssign.Text = "Assign"
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Roboto Condensed", 11.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.Control
        Me.Label2.Location = New System.Drawing.Point(15, 48)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(183, 19)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "List of client's purchased plots"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Roboto", 24.0!, System.Drawing.FontStyle.Bold)
        Me.Label1.ForeColor = System.Drawing.SystemColors.Control
        Me.Label1.Location = New System.Drawing.Point(12, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(261, 39)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Grave Purchases"
        '
        'Guna2Panel1
        '
        Me.Guna2Panel1.AutoSize = True
        Me.Guna2Panel1.Controls.Add(Me.txtSearch)
        Me.Guna2Panel1.Controls.Add(Me.ReservationList)
        Me.Guna2Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Guna2Panel1.Location = New System.Drawing.Point(0, 81)
        Me.Guna2Panel1.Name = "Guna2Panel1"
        Me.Guna2Panel1.ShadowDecoration.Enabled = True
        Me.Guna2Panel1.Size = New System.Drawing.Size(888, 355)
        Me.Guna2Panel1.TabIndex = 5
        '
        'txtSearch
        '
        Me.txtSearch.Animated = True
        Me.txtSearch.AutoRoundedCorners = True
        Me.txtSearch.BorderColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(171, Byte), Integer), CType(CType(189, Byte), Integer))
        Me.txtSearch.BorderRadius = 16
        Me.txtSearch.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSearch.DefaultText = ""
        Me.txtSearch.DisabledState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.txtSearch.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer))
        Me.txtSearch.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.txtSearch.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.txtSearch.FocusedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtSearch.Font = New System.Drawing.Font("Segoe UI", 11.25!)
        Me.txtSearch.ForeColor = System.Drawing.Color.Black
        Me.txtSearch.HoverState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtSearch.IconRight = Global.dashboard.My.Resources.Resources.search
        Me.txtSearch.IconRightOffset = New System.Drawing.Point(8, 0)
        Me.txtSearch.IconRightSize = New System.Drawing.Size(25, 25)
        Me.txtSearch.Location = New System.Drawing.Point(12, 7)
        Me.txtSearch.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.PlaceholderForeColor = System.Drawing.Color.Gray
        Me.txtSearch.PlaceholderText = "Search a name"
        Me.txtSearch.SelectedText = ""
        Me.txtSearch.Size = New System.Drawing.Size(292, 35)
        Me.txtSearch.TabIndex = 2
        '
        'ReservationList
        '
        Me.ReservationList.AllowColumnReorder = True
        Me.ReservationList.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ReservationList.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.ReservationList.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.ReservationList.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.id, Me.fullName, Me.plotlocation, Me.dateOfReserv, Me.quantity, Me.status, Me.payment_status})
        Me.ReservationList.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ReservationList.ForeColor = System.Drawing.SystemColors.Window
        Me.ReservationList.FullRowSelect = True
        Me.ReservationList.GridLines = True
        Me.ReservationList.HideSelection = False
        Me.ReservationList.Location = New System.Drawing.Point(12, 49)
        Me.ReservationList.Name = "ReservationList"
        Me.ReservationList.ShowItemToolTips = True
        Me.ReservationList.Size = New System.Drawing.Size(864, 301)
        Me.ReservationList.TabIndex = 1
        Me.ReservationList.TileSize = New System.Drawing.Size(10, 10)
        Me.ReservationList.UseCompatibleStateImageBehavior = False
        Me.ReservationList.View = System.Windows.Forms.View.Details
        '
        'id
        '
        Me.id.Text = "ID"
        Me.id.Width = 50
        '
        'fullName
        '
        Me.fullName.Text = "Name of Client"
        Me.fullName.Width = 270
        '
        'plotlocation
        '
        Me.plotlocation.Text = "Reserved Plot"
        Me.plotlocation.Width = 300
        '
        'dateOfReserv
        '
        Me.dateOfReserv.Text = "Date Reserved"
        Me.dateOfReserv.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.dateOfReserv.Width = 150
        '
        'quantity
        '
        Me.quantity.Text = "Quantity"
        Me.quantity.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.quantity.Width = 100
        '
        'status
        '
        Me.status.Text = "Status"
        Me.status.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.status.Width = 100
        '
        'payment_status
        '
        Me.payment_status.Text = "Payment Status"
        Me.payment_status.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.payment_status.Width = 120
        '
        'Guna2Elipse1
        '
        Me.Guna2Elipse1.BorderRadius = 20
        Me.Guna2Elipse1.TargetControl = Me
        '
        'ToolTip1
        '
        Me.ToolTip1.IsBalloon = True
        Me.ToolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info
        '
        'frmReservationsReg
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(14, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(90, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(888, 436)
        Me.Controls.Add(Me.Guna2Panel1)
        Me.Controls.Add(Me.Guna2Panel2)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "frmReservationsReg"
        Me.Text = "frmReservations"
        Me.Guna2Panel2.ResumeLayout(False)
        Me.Guna2Panel2.PerformLayout()
        Me.Guna2Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Guna2Panel2 As Guna.UI2.WinForms.Guna2Panel
    Friend WithEvents btnAssign As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Guna2Panel1 As Guna.UI2.WinForms.Guna2Panel
    Friend WithEvents ReservationList As ListView
    Friend WithEvents id As ColumnHeader
    Friend WithEvents dateOfReserv As ColumnHeader
    Friend WithEvents status As ColumnHeader
    Friend WithEvents quantity As ColumnHeader
    Friend WithEvents Guna2Elipse1 As Guna.UI2.WinForms.Guna2Elipse
    Friend WithEvents btnDelete As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents btnExportPDF As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents fullName As ColumnHeader
    Friend WithEvents btnShowAll As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents txtSearch As Guna.UI2.WinForms.Guna2TextBox
    Friend WithEvents plotlocation As ColumnHeader
    Friend WithEvents payment_status As ColumnHeader
    Friend WithEvents ToolTip1 As ToolTip
End Class
