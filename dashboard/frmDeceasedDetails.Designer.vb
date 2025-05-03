<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDeceasedDetails
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblName = New System.Windows.Forms.Label()
        Me.lblPlotLocation = New System.Windows.Forms.Label()
        Me.lblStatus = New System.Windows.Forms.Label()
        Me.lblIntermentDate = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 24.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.Control
        Me.Label1.Location = New System.Drawing.Point(232, 23)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(349, 46)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Deceased Details"
        '
        'lblName
        '
        Me.lblName.AutoSize = True
        Me.lblName.Font = New System.Drawing.Font("Microsoft Sans Serif", 24.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblName.ForeColor = System.Drawing.SystemColors.Control
        Me.lblName.Location = New System.Drawing.Point(285, 90)
        Me.lblName.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblName.Name = "lblName"
        Me.lblName.Size = New System.Drawing.Size(203, 46)
        Me.lblName.TabIndex = 2
        Me.lblName.Text = "Fullname:"
        '
        'lblPlotLocation
        '
        Me.lblPlotLocation.AutoSize = True
        Me.lblPlotLocation.Font = New System.Drawing.Font("Microsoft Sans Serif", 24.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPlotLocation.ForeColor = System.Drawing.SystemColors.Control
        Me.lblPlotLocation.Location = New System.Drawing.Point(243, 204)
        Me.lblPlotLocation.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblPlotLocation.Name = "lblPlotLocation"
        Me.lblPlotLocation.Size = New System.Drawing.Size(291, 46)
        Me.lblPlotLocation.TabIndex = 6
        Me.lblPlotLocation.Text = "Plot Location :"
        '
        'lblStatus
        '
        Me.lblStatus.AutoSize = True
        Me.lblStatus.Font = New System.Drawing.Font("Microsoft Sans Serif", 24.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStatus.ForeColor = System.Drawing.SystemColors.Control
        Me.lblStatus.Location = New System.Drawing.Point(306, 311)
        Me.lblStatus.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(163, 46)
        Me.lblStatus.TabIndex = 7
        Me.lblStatus.Text = "Status :"
        '
        'lblIntermentDate
        '
        Me.lblIntermentDate.AutoSize = True
        Me.lblIntermentDate.Font = New System.Drawing.Font("Microsoft Sans Serif", 24.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblIntermentDate.ForeColor = System.Drawing.SystemColors.Control
        Me.lblIntermentDate.Location = New System.Drawing.Point(243, 145)
        Me.lblIntermentDate.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblIntermentDate.Name = "lblIntermentDate"
        Me.lblIntermentDate.Size = New System.Drawing.Size(312, 46)
        Me.lblIntermentDate.TabIndex = 5
        Me.lblIntermentDate.Text = "Interment date :"
        '
        'frmDeceasedDetails
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(14, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(90, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(843, 426)
        Me.Controls.Add(Me.lblStatus)
        Me.Controls.Add(Me.lblPlotLocation)
        Me.Controls.Add(Me.lblIntermentDate)
        Me.Controls.Add(Me.lblName)
        Me.Controls.Add(Me.Label1)
        Me.Name = "frmDeceasedDetails"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "frmDeceasedDetails"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents lblName As Label
    Friend WithEvents lblPlotLocation As Label
    Friend WithEvents lblStatus As Label
    Friend WithEvents lblIntermentDate As Label
End Class
