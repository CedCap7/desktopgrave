Public Class dashboard
    ' Add a field to track the current map form instance
    Private currentMapForm As frmWebMap = Nothing

    Private Sub LoadfrmDashboard()
        ' Create a new instance of frmDashboard
        Dim frmDashboard As New frmDashboard()

        ' Ensure that the frmDashboard is not opened as a separate window
        frmDashboard.TopLevel = False
        frmDashboard.FormBorderStyle = FormBorderStyle.None
        frmDashboard.Dock = DockStyle.Fill ' Makes frmDashboard fill the container

        ' Assuming you have a Panel control named 'PanelContainer' in dashboard where you want to display frmDashboard
        subPanel.Controls.Clear()  ' Clear any existing controls in the panel (optional)
        subPanel.Controls.Add(frmDashboard)  ' Add frmDashboard to the panel

        ' Show frmDashboard
        frmDashboard.Show()
    End Sub

    ' Event handler for when dashboard is loaded
    Private Sub dashboard_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Call the method to load frmDashboard inside the container
        LoadfrmDashboard()
    End Sub

    Public Sub subForm(panel As Form)
        subPanel.Controls.Clear()

        ' If we're switching away from the map form, dispose of it
        If TypeOf panel IsNot frmWebMap AndAlso currentMapForm IsNot Nothing Then
            currentMapForm.Dispose()
            currentMapForm = Nothing
        End If

        panel.TopLevel = False
        panel.FormBorderStyle = FormBorderStyle.None
        panel.Dock = DockStyle.Fill
        subPanel.Controls.Add(panel)
        panel.Show()
    End Sub

    Private Sub mapbtn_Click(sender As Object, e As EventArgs) Handles mapbtn.Click
        ' Dispose of any existing map form
        If currentMapForm IsNot Nothing Then
            currentMapForm.Dispose()
            currentMapForm = Nothing
        End If

        ' Create new instance of frmWebMap
        currentMapForm = New frmWebMap()
        subForm(currentMapForm)
    End Sub

    Private Sub registrybtn_Click(sender As Object, e As EventArgs) Handles registrybtn.Click
        subForm(frmDeceasedReg)
    End Sub

    Private Sub clientregistrybtn_Click(sender As Object, e As EventArgs) Handles clientregistrybtn.Click
        subForm(frmClientReg)
    End Sub

    Private Sub formsbtn_Click(sender As Object, e As EventArgs) Handles formsbtn.Click
        Dim formInstance As New frmForms(Me)
        subForm(formInstance)
    End Sub

    Private Sub payreservbtn_Click(sender As Object, e As EventArgs) Handles payreservbtn.Click
        subForm(frmReservationsReg)
    End Sub

    Private Sub btnDashboard_Click(sender As Object, e As EventArgs) Handles btnDashboard.Click
        subForm(frmDashboard)
    End Sub

    Private Sub paymentbtn_Click(sender As Object, e As EventArgs) Handles paymentbtn.Click
        subForm(frmPaymentReg)
    End Sub
End Class