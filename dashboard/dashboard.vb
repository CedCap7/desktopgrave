Public Class dashboard

    Sub subForm(panel As Form)
        subPanel.Controls.Clear()
        panel.TopLevel = False
        panel.FormBorderStyle = FormBorderStyle.None
        panel.Dock = DockStyle.Fill
        subPanel.Controls.Add(panel)

        panel.Show()

    End Sub
    Private Sub registrybtn_Click(sender As Object, e As EventArgs) Handles registrybtn.Click
        subForm(frmDeceasedReg)
    End Sub

    Private Sub formsbtn_Click(sender As Object, e As EventArgs) Handles formsbtn.Click
        subForm(frmForms)
    End Sub

End Class
