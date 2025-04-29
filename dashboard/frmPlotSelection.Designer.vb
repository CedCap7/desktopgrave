<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmPlotSelection
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
        Me.webViewPlotSelection = New Microsoft.Web.WebView2.WinForms.WebView2()
        CType(Me.webViewPlotSelection, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'webViewPlotSelection
        '
        Me.webViewPlotSelection.AllowExternalDrop = True
        Me.webViewPlotSelection.CreationProperties = Nothing
        Me.webViewPlotSelection.DefaultBackgroundColor = System.Drawing.Color.White
        Me.webViewPlotSelection.Dock = System.Windows.Forms.DockStyle.Fill
        Me.webViewPlotSelection.Location = New System.Drawing.Point(0, 0)
        Me.webViewPlotSelection.Name = "webViewPlotSelection"
        Me.webViewPlotSelection.Size = New System.Drawing.Size(676, 436)
        Me.webViewPlotSelection.TabIndex = 15
        Me.webViewPlotSelection.ZoomFactor = 1.0R
        '
        'frmPlotSelection
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(23, Byte), Integer), CType(CType(107, Byte), Integer), CType(CType(135, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(676, 436)
        Me.Controls.Add(Me.webViewPlotSelection)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "frmPlotSelection"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "frmPlotSelection"
        CType(Me.webViewPlotSelection, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents webViewPlotSelection As Microsoft.Web.WebView2.WinForms.WebView2
End Class
