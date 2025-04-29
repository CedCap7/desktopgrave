Imports MySql.Data.MySqlClient

' Shared approval logic
Imports System.Net
Imports System.Net.Mail

Public Class frmAccApproval

    Dim toolTip As New ToolTip()
    Private Sub frmAccApproval_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        KeyPreview = True
        LoadUsers("") ' Load all users initially with empty search

        ToolTip.SetToolTip(btnApproval, "Click to approve the selected application.")  ' Tooltip for btnApproval
        ToolTip.SetToolTip(txtSearch, "Enter the name of the client to search.")  ' Tooltip for txtSearch
    End Sub

    ' Load applications from database into ListView
    Sub LoadUsers(Optional searchText As String = "")
        Try
            dbconn()
            cn.Open()

            Dim sql As String = "SELECT * FROM application " &
                                "WHERE LastName LIKE @txtsearch OR FirstName LIKE @txtsearch OR MiddleName LIKE @txtsearch " &
                                "ORDER BY LastName ASC"

            cmd = New MySqlCommand(sql, cn)
            cmd.Parameters.AddWithValue("@txtsearch", "%" & searchText & "%")
            dr = cmd.ExecuteReader

            ApprovalList.Items.Clear()

            While dr.Read
                Dim newLine = ApprovalList.Items.Add(dr("Client_ID").ToString())
                newLine.SubItems.Add(dr("LastName") & ", " & dr("FirstName") & ", " & dr("MiddleName"))
                newLine.SubItems.Add(dr("Gender").ToString())
                newLine.SubItems.Add(dr("Mobile").ToString())
                newLine.SubItems.Add(dr("Email").ToString())
                newLine.SubItems.Add(dr("Address").ToString())

                Dim dateApplied As String = If(dr("Date_Applied") IsNot DBNull.Value,
                    Convert.ToDateTime(dr("Date_Applied")).ToString("MMMM dd, yyyy"), "N/A")
                newLine.SubItems.Add(dateApplied)

                ' Display Status
                ' Get and display status
                Dim statusValue As Integer = Convert.ToInt32(dr("Status"))
                Dim statusText As String = "Pending"
                Dim rowColor As Color = Color.White

                If statusValue = 1 Then
                    statusText = "Approved"
                ElseIf statusValue = 2 Then
                    statusText = "Rejected"
                End If

                newLine.SubItems.Add(statusText)
                newLine.ForeColor = rowColor ' Apply color to the whole row


                newLine.SubItems.Add(statusText)

            End While

            cmd.Dispose()
            dr.Close()
            cn.Close()
        Catch ex As Exception
            MessageBox.Show("Error loading applications: " & ex.Message)
        End Try
    End Sub


    Private Sub ApproveSelectedApplication()
        If ApprovalList.SelectedItems.Count > 0 Then
            Dim clientID As Integer = Convert.ToInt32(ApprovalList.SelectedItems(0).Text)

            Try
                dbconn()

                ' Ensure the connection is closed before opening again
                If cn.State = ConnectionState.Open Then
                    cn.Close()
                End If

                ' Ensure connection is open
                If cn.State = ConnectionState.Closed Then
                    cn.Open()
                End If

                ' Check if already approved
                Dim statusSQL As String = "SELECT Status, Email, FirstName, LastName FROM application WHERE Client_ID = @ClientID"
                cmd = New MySqlCommand(statusSQL, cn)
                cmd.Parameters.AddWithValue("@ClientID", clientID)

                Dim dr As MySqlDataReader = cmd.ExecuteReader()

                If dr.Read() Then
                    Dim statusResult As Integer = Convert.ToInt32(dr("Status"))
                    If statusResult = 1 Then
                        MessageBox.Show("This application has already been approved!", "Already Approved", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        dr.Close() ' Close reader before exiting
                        If cn.State = ConnectionState.Open Then
                            cn.Close() ' Close connection after usage
                        End If
                        Exit Sub
                    End If

                    ' Get applicant's details
                    Dim email As String = dr("Email").ToString()
                    Dim firstName As String = dr("FirstName").ToString()
                    Dim lastName As String = dr("LastName").ToString()

                    ' Close the DataReader before executing new commands on the same connection
                    dr.Close()

                    ' Confirm approval
                    Dim result As DialogResult = MessageBox.Show("Do you want to approve this application?", "Approval Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

                    If result = DialogResult.Yes Then
                        ' Insert into client table and set status to 1
                        Dim insertSQL As String = "INSERT INTO client (FirstName, MiddleName, LastName, Gender, Mobile, Address, Email, status, Date_Registered) " &
                                              "SELECT FirstName, MiddleName, LastName, Gender, Mobile, Address, Email, 1, @DateRegistered " &
                                              "FROM application WHERE Client_ID = @ClientID"

                        ' Insert into client table and set status to 1
                        cmd = New MySqlCommand(insertSQL, cn)
                        cmd.Parameters.AddWithValue("@ClientID", clientID)
                        cmd.Parameters.AddWithValue("@DateRegistered", DateTime.Now) ' Add Date_Registered with current date and time
                        cmd.ExecuteNonQuery()

                        ' Update application status to approved
                        Dim updateSQL As String = "UPDATE application SET Status = 1 WHERE Client_ID = @ClientID"
                        cmd = New MySqlCommand(updateSQL, cn)
                        cmd.Parameters.AddWithValue("@ClientID", clientID)
                        cmd.ExecuteNonQuery()

                        If cn.State = ConnectionState.Open Then
                            cn.Close() ' Close connection after usage
                        End If

                        ' Send Email Notification
                        SendApprovalEmail(email, firstName, lastName)

                        MessageBox.Show("Application approved successfully! A notification email has been sent.")
                        LoadUsers()
                    Else
                        If cn.State = ConnectionState.Open Then
                            cn.Close() ' Close connection after usage
                        End If
                    End If
                Else
                    ' If no record was found
                    If cn.State = ConnectionState.Open Then
                        cn.Close() ' Close connection after usage
                    End If
                End If

            Catch ex As Exception
                MessageBox.Show("Error during approval: " & ex.Message)
                If cn.State = ConnectionState.Open Then
                    cn.Close() ' Ensure connection is closed after error
                End If
            End Try
        Else
            MessageBox.Show("Please select an application to approve.")
        End If
    End Sub


    Private Sub SendApprovalEmail(toEmail As String, firstName As String, lastName As String)
        Try
            ' Set up the SMTP client
            Dim smtpClient As New SmtpClient("smtp.gmail.com") ' Replace with your SMTP server
            smtpClient.Port = 587  ' Typically 587 for TLS/STARTTLS
            smtpClient.Credentials = New NetworkCredential("cedrickcap7@gmail.com", "empj kkji wjhl sfij") ' Replace with your email and password
            smtpClient.EnableSsl = True

            ' Create the email message
            Dim mailMessage As New MailMessage()
            mailMessage.From = New MailAddress("cedrickcap7@gmail.com") ' Replace with your email
            mailMessage.To.Add(toEmail)
            mailMessage.Subject = "Your Application Has Been Approved"
            mailMessage.Body = String.Format("Dear {0} {1},{2}{2}Congratulations! Your application has been approved. You may now proceed to the municipal office to reserve or purchase a grave space.{2}{2}Best regards,{2}Your Cemetery Management Team", firstName, lastName, Environment.NewLine)

            ' Send the email
            smtpClient.Send(mailMessage)

        Catch ex As Exception
            MessageBox.Show("Error sending email: " & ex.Message)
        End Try
    End Sub


    ' Double-click to approve
    Private Sub ApprovalList_DoubleClick(sender As Object, e As EventArgs) Handles ApprovalList.DoubleClick
        ApproveSelectedApplication()
    End Sub

    ' Approve button
    Private Sub btnApproval_Click(sender As Object, e As EventArgs) Handles btnApproval.Click
        ApproveSelectedApplication()
    End Sub

    ' Reject button (deletes application)
    Private Sub btnReject_Click(sender As Object, e As EventArgs) Handles btnReject.Click
        If ApprovalList.SelectedItems.Count > 0 Then
            Try
                Dim clientID As Integer = Convert.ToInt32(ApprovalList.SelectedItems(0).Text)

                ' Check if the application is approved or rejected
                dbconn()

                If cn.State = ConnectionState.Closed Then
                    cn.Open()
                End If

                Dim statusSQL As String = "SELECT Status FROM application WHERE Client_ID = @ClientID"
                cmd = New MySqlCommand(statusSQL, cn)
                cmd.Parameters.AddWithValue("@ClientID", clientID)

                Dim statusResult = cmd.ExecuteScalar()

                If statusResult IsNot Nothing Then
                    Dim statusValue As Integer = Convert.ToInt32(statusResult)

                    ' If application is approved (status 1) or rejected (status 2), prevent rejection
                    If statusValue = 1 Then
                        MessageBox.Show("This application has already been approved and cannot be rejected!", "Cannot Reject", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        cn.Close()
                        Exit Sub
                    ElseIf statusValue = 2 Then
                        MessageBox.Show("This application has already been rejected and cannot be rejected again!", "Cannot Reject", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        cn.Close()
                        Exit Sub
                    End If
                End If

                ' Confirm rejection
                Dim result As DialogResult = MessageBox.Show("Are you sure you want to reject and delete this application?",
                "Reject Application", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)

                If result = DialogResult.Yes Then
                    Dim deleteSQL As String = "DELETE FROM application WHERE Client_ID = @ClientID"
                    cmd = New MySqlCommand(deleteSQL, cn)
                    cmd.Parameters.AddWithValue("@ClientID", clientID)
                    cmd.ExecuteNonQuery()

                    If cn.State = ConnectionState.Open Then
                        cn.Close()
                    End If
                    MessageBox.Show("Application rejected and deleted successfully!")
                    LoadUsers()
                End If
            Catch ex As Exception
                MessageBox.Show("Error rejecting application: " & ex.Message)
            End Try
        Else
            MessageBox.Show("Please select an application to reject.")
        End If
    End Sub

    ' Delete button
    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        If ApprovalList.SelectedItems.Count > 0 Then
            Dim clientID As Integer = Convert.ToInt32(ApprovalList.SelectedItems(0).Text)

            Dim confirmDelete As DialogResult = MessageBox.Show(
            "Are you sure you want to permanently delete this application?",
            "Confirm Delete",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Warning)

            If confirmDelete = DialogResult.Yes Then
                Try
                    dbconn()
                    If cn.State = ConnectionState.Closed Then
                        cn.Open()
                    End If

                    Dim deleteSQL As String = "DELETE FROM application WHERE Client_ID = @ClientID"
                    cmd = New MySqlCommand(deleteSQL, cn)
                    cmd.Parameters.AddWithValue("@ClientID", clientID)
                    Dim rowsAffected = cmd.ExecuteNonQuery()

                    If rowsAffected > 0 Then
                        MessageBox.Show("Application deleted successfully!", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Else
                        MessageBox.Show("No matching record found to delete.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If

                    If cn.State = ConnectionState.Open Then
                        cn.Close()
                    End If
                    LoadUsers()
                Catch ex As Exception
                    MessageBox.Show("Error deleting application: " & ex.Message)
                End Try
            End If
        Else
            MessageBox.Show("Please select an application to delete.")
        End If
    End Sub


    ' Search on text change
    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        LoadUsers(txtSearch.Text.Trim())
    End Sub

    ' Close button
    Private Sub btnClose_Click(sender As Object, e As EventArgs) 
        Me.Close()
    End Sub

    ' Show all applications
    Private Sub btnShowAll_Click(sender As Object, e As EventArgs) Handles btnShowAll.Click
        LoadUsers()
    End Sub


End Class
