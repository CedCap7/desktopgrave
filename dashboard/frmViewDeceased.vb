Imports MySql.Data.MySqlClient

Public Class frmViewDeceased

    Public Sub LoadDeceasedInfo(selectedID As Integer)
        Try
            dbconn()
            cn.Open()

            ' SQL query to join deceased, client, location, and beneficiaries tables
            sql = "SELECT d.FirstName AS DeceasedFirstName, d.MiddleName AS DeceasedMiddleName, d.LastName AS DeceasedLastName, " &
              "d.DateOfBirth, d.DateOfDeath, " &
              "c.FirstName AS ClientFirstName, c.LastName AS ClientLastName, c.Mobile, c.Email, c.Address, " &
              "l.Type, l.Block, l.Section, l.Row, l.Plot, " &
              "b1.FullName AS Beneficiary1, b1.Contact AS Contact1, " &
              "b2.FullName AS Beneficiary2, b2.Contact AS Contact2 " &
              "FROM deceased d " &
              "LEFT JOIN client c ON d.Client_ID = c.Client_ID " &
              "LEFT JOIN location l ON d.Plot_ID = l.id " &
              "LEFT JOIN beneficiaries b1 ON d.Deceased_ID = b1.Deceased_ID AND b1.`Order` = 1 " &
              "LEFT JOIN beneficiaries b2 ON d.Deceased_ID = b2.Deceased_ID AND b2.`Order` = 2 " &
              "WHERE d.Deceased_ID = @ID"
            cmd = New MySqlCommand(sql, cn)
            cmd.Parameters.AddWithValue("@ID", selectedID)
            dr = cmd.ExecuteReader()

            If dr.Read() Then
                ' Check if client data exists
                Dim hasClientData As Boolean = Not (dr("ClientFirstName") Is DBNull.Value And dr("ClientLastName") Is DBNull.Value)

                ' Client Information
                If hasClientData Then
                    LblClient.Text = dr("ClientLastName") & ", " & dr("ClientFirstName")
                    LblContact.Text = If(dr("Mobile") IsNot DBNull.Value, dr("Mobile").ToString(), "N/A")
                    LblEmail.Text = If(dr("Email") IsNot DBNull.Value, dr("Email").ToString(), "N/A")
                    LblAddress.Text = If(dr("Address") IsNot DBNull.Value, dr("Address").ToString(), "N/A")
                Else
                    LblClient.Text = "No Client"
                    LblContact.Text = "N/A"
                    LblEmail.Text = "N/A"
                    LblAddress.Text = "N/A"
                End If

                ' Deceased Information
                LblDeceased.Text = dr("DeceasedLastName") & ", " & dr("DeceasedFirstName") & " " &
                               If(dr("DeceasedMiddleName") IsNot DBNull.Value, dr("DeceasedMiddleName").ToString(), "")

                ' Format dates
                LblDob.Text = If(dr("DateOfBirth") IsNot DBNull.Value, Convert.ToDateTime(dr("DateOfBirth")).ToString("MMMM dd, yyyy"), "N/A")
                LblDod.Text = If(dr("DateOfDeath") IsNot DBNull.Value, Convert.ToDateTime(dr("DateOfDeath")).ToString("MMMM dd, yyyy"), "N/A")

                ' Beneficiary Information
                Lbl1stBeneficiary.Text = "1st Beneficiary: " & If(dr("Beneficiary1") IsNot DBNull.Value, dr("Beneficiary1").ToString(), "N/A")
                Lbl2ndBeneficiary.Text = "2nd Beneficiary: " & If(dr("Beneficiary2") IsNot DBNull.Value, dr("Beneficiary2").ToString(), "N/A")

                ' Plot Location Information
                Dim plotLocation As String = ""
                If dr("Type") IsNot DBNull.Value Then
                    Dim typeString As String = ConvertTypeToString(dr("Type"))
                    plotLocation = $"{typeString}, Block {dr("Block")}, Section {dr("Section")}, Row {dr("Row")}, Plot {dr("Plot")}"
                End If
                LblPlot.Text = If(plotLocation <> "", plotLocation, "N/A")
            Else
                ' Handle case where no records are found
                MessageBox.Show("No records found for the selected deceased ID.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

        Catch ex As Exception
            MessageBox.Show("Error loading information: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If dr IsNot Nothing Then dr.Close()
            If cn IsNot Nothing Then cn.Close()
        End Try
    End Sub

    Private Function ConvertTypeToString(typeValue As Object) As String
        ' Convert type numeric value to string description
        Select Case typeValue
            Case 1
                Return "Apartment"
            Case 2
                Return "Family Lawn Lots"
            Case 3
                Return "Bone Niche"
            Case 4
                Return "Private"
            Case Else
                Return "Unknown"
        End Select
    End Function



    Public Sub SetDeceasedInfo(deceasedId As String, fullName As String, birthdate As String, deathdate As String, location As String)
        LblDeceased.Text = fullName
        LblDob.Text = birthdate
        LblDod.Text = deathdate
        LblPlot.Text = location
    End Sub
End Class