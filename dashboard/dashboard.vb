Imports MySql.Data.MySqlClient

Public Class dashboard
    ' Add a field to track the current map form instance
    Private currentMapForm As frmWebMap = Nothing
    ' Removed the unnecessary LoginInfoModule property
    ' Public Property LoginInfoModule As Object 

    ' Add the public property to receive the logged-in user ID
    Public Property LoggedInUserId As Integer

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


    Private Sub dashboard_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Call the method to load frmDashboard inside the container
        LoadfrmDashboard()

        ' --- Code to fetch and display the username from the database ---
        LoggedInUserId = Me.LoggedInUserId

        Dim fetchedUsername As String = "Unknown User" ' Default value in case fetching fails

        ' <<< Replace this entire block with your database query code >>>
        ' This is a conceptual example using generic database objects.
        ' You need to adapt this to your specific database type and data access methods.
        Try
            ' Assuming you have a connection string and a way to create a connection
            Using connection As New MySqlConnection("server=srv594.hstgr.io; database=u976878483_cemetery; username=u976878483_doncarlos; password=d0Nc4los; port=3306")
                connection.Open()


                Dim query As String = "SELECT Username FROM user WHERE user_id = @UserId" ' <<< Replace "YourUserTable" with your actual table name
                Using command As New MySqlCommand(query, connection)
                    ' Add parameter for user_id to prevent SQL injection
                    command.Parameters.AddWithValue("@UserId", LoggedInUserId)

                    Dim result As Object = command.ExecuteScalar()

                    If result IsNot Nothing Then
                        fetchedUsername = result.ToString()
                    End If
                End Using
            End Using
        Catch ex As Exception
            ' Handle any database errors (e.g., show an error message)
            Console.WriteLine("Error fetching username: " & ex.Message)
            ' Optionally display an error in the label or a message box
            lblUsername.Text = "Error"
        End Try
        ' <<< End of database query code block >>>


        ' Set the username label text
        lblUsername.Text = fetchedUsername
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

    Private Sub btnUserManagement_Click(sender As Object, e As EventArgs) Handles btnUserManagement.Click
        subForm(frmUserManagement)
    End Sub

    Private Sub btnPackages_Click(sender As Object, e As EventArgs) Handles btnPackages.Click
        subForm(frmPackages)
    End Sub

    Public Sub SignOut()
        ' Show a new login form
        Dim loginForm As New frmLogin()
        loginForm.Show()
        ' Hide the dashboard form to ensure a clean sign out
        Me.Hide()
    End Sub

    Private Sub Guna2Button1_Click(sender As Object, e As EventArgs) Handles Guna2Button1.Click
        SignOut()
    End Sub

    Private Sub Guna2Button2_Click(sender As Object, e As EventArgs) Handles btnLogs.Click
        subForm(frmLogs)
    End Sub

    Private Sub lblUsername_Click(sender As Object, e As EventArgs) Handles lblUsername.Click

    End Sub

    Private Sub subPanel_Paint(sender As Object, e As PaintEventArgs) Handles subPanel.Paint

    End Sub
End Class