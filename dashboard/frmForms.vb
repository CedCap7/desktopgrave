Public Class frmForms

    Private mainDashboard As dashboard

    ' Constructor to pass the main dashboard form
    Public Sub New(dashboardForm As dashboard)
        InitializeComponent()
        If dashboardForm Is Nothing Then
            Throw New ArgumentNullException(NameOf(dashboardForm), "Dashboard form cannot be null.")
        End If
        mainDashboard = dashboardForm
    End Sub

    Private Sub frmForms_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Any initialization logic goes here
    End Sub

    Private Sub btnApplicationForm_Click(sender As Object, e As EventArgs) Handles btnApplicationForm.Click
        Dim applicationForm As New frmApplication(mainDashboard) ' Pass dashboard if required
        applicationForm.ShowDialog()
        Me.Visible = True
    End Sub

    Private Sub btnReservationForm_Click(sender As Object, e As EventArgs)
        Try
            ' Create an instance of frmPaymentReserv
            Dim reservationForm As New frmPlotPurchAndAssign()
            ' Show it as a dialog
            reservationForm.ShowDialog()
        Catch ex As Exception
            MessageBox.Show("Error opening the reservation form: " & ex.Message)
        End Try
    End Sub

    Private Sub btnDeceasedForm_Click(sender As Object, e As EventArgs) Handles btnDeceasedForm.Click
        Try
            ' Create an instance of frmDeceased with the dashboard reference
            Dim deceasedForm As New frmDeceased(mainDashboard)
            ' Show it in the dashboard's subPanel instead of as a dialog
            mainDashboard.subForm(deceasedForm)
        Catch ex As Exception
            MessageBox.Show("Error opening the deceased form: " & ex.Message)
        End Try
    End Sub

    Private Sub btnPlotReservation_Click(sender As Object, e As EventArgs) Handles btnPlotReservation.Click
        Try
            ' Create an instance of frmDeceased with the dashboard reference
            Dim plotreservationForm As New frmPlotReservation(mainDashboard)
            ' Show it as a dialog
            plotreservationForm.ShowDialog()
        Catch ex As Exception
            MessageBox.Show("Error opening the plot reservation form: " & ex.Message)
        End Try
    End Sub

    Private Sub btnAccApprov_Click(sender As Object, e As EventArgs) Handles btnAccApprov.Click
        Try
            Dim AccountApprovalForm As New frmAccApproval()
            ' Show it as a dialog
            AccountApprovalForm.ShowDialog()
        Catch ex As Exception
            MessageBox.Show("Error opening the approval form: " & ex.Message)
        End Try
    End Sub


    Private Sub Guna2Panel1_MouseLeave(sender As Object, e As EventArgs) Handles Guna2Panel1.MouseLeave
        Guna2Panel1.FillColor = Color.FromArgb(40, 171, 189) ' Original teal color
    End Sub

    Private Sub Guna2Panel1_MouseEnter(sender As Object, e As EventArgs) Handles Guna2Panel1.MouseEnter
        Guna2Panel1.FillColor = Color.FromArgb(51, 153, 204) ' Change to a lighter shade
    End Sub

    Private Sub Guna2Panel3_MouseLeave(sender As Object, e As EventArgs) Handles Guna2Panel3.MouseLeave
        Guna2Panel3.FillColor = Color.FromArgb(40, 171, 189) ' Original teal color
    End Sub

    Private Sub Guna2Panel3_MouseEnter(sender As Object, e As EventArgs) Handles Guna2Panel3.MouseEnter
        Guna2Panel3.FillColor = Color.FromArgb(51, 153, 204) ' Change to a lighter shade
    End Sub




    Private Sub Guna2Panel4_MouseLeave(sender As Object, e As EventArgs) Handles Guna2Panel4.MouseLeave
        Guna2Panel4.FillColor = Color.FromArgb(40, 171, 189) ' Original teal color
    End Sub

    Private Sub Guna2Panel4_MouseEnter(sender As Object, e As EventArgs) Handles Guna2Panel4.MouseEnter
        Guna2Panel4.FillColor = Color.FromArgb(51, 153, 204) ' Change to a lighter shade
    End Sub

    Private Sub Guna2Panel6_MouseLeave(sender As Object, e As EventArgs) Handles Guna2Panel6.MouseLeave
        Guna2Panel6.FillColor = Color.FromArgb(40, 171, 189) ' Original teal color
    End Sub

    Private Sub Guna2Panel6_MouseEnter(sender As Object, e As EventArgs) Handles Guna2Panel6.MouseEnter
        Guna2Panel6.FillColor = Color.FromArgb(51, 153, 204) ' Change to a lighter shade
    End Sub



End Class
