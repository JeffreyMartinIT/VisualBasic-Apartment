'Jeffrey Martin

''' <summary>
''' Home page. Shows the current active contracts and allows ending them.
''' </summary>
Public Class _Default
    Inherits Page

    ''' <summary>
    ''' Load the drop down list upon starting the page.
    ''' </summary>
    ''' <param name="sender">Object</param>
    ''' <param name="e">EventArgs</param>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

        'Create an instance of the FactoryLeases that is used to interact with the Lease database.
        Dim factoryLeases As FactoryLeases = New FactoryLeases()

        Dim _apartmentNumber As Int16

        DeleteButton.Visible = False
        CancelButton.Visible = False

        'Create thd drop down list filled with apartment numbers that is used to deactivate a lease.  
        If MoveOutDropDownList.Items.Count <= 1 Then

            'Check apartment numbers 1 through 8
            For _apartmentNumber = 1 To 8

                'Check if the current apartment number has a lease and is active.
                If (factoryLeases.ApartmentNumberLeased(_apartmentNumber)) Then

                    'As an active lease exist for the apartment number add it to the list.
                    MoveOutDropDownList.Items.Add(_apartmentNumber)

                End If

            Next

            'Place a comment into the label next to the MoveOutDropDownList when there are no active leases.
            If MoveOutDropDownList.Items.Count <= 1 Then

                MovedOutLabel.Text = "No leases for any of the apartments! Get selling!"

            Else

                MovedOutLabel.Text = ""

            End If

        End If

    End Sub


    ''' <summary>
    ''' Upon choosing an apartment number from the drop down list confirm deactivation and call the method
    ''' that will interact with the database to deactivate the resident and lease for the apartment selected.
    ''' </summary>
    ''' <param name="sender">Object</param>
    ''' <param name="e">EventArgs</param>
    Protected Sub MoveOutDropDownList_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MoveOutDropDownList.SelectedIndexChanged

        'If no aprtments are in the drop down list do nothing.
        If MoveOutDropDownList.Items.Count > 1 Then

            'Check that an item has been selected from the drop down list
            If MoveOutDropDownList.SelectedIndex <> 0 Then

                MovedOutLabel.Text = "Are you sure you wish to end apartment " + MoveOutDropDownList.SelectedValue + "'s lease?"
                DeleteButton.Visible = True
                CancelButton.Visible = True


            End If

        End If

    End Sub

    Protected Sub DeleteButton_Click(sender As Object, e As EventArgs) Handles DeleteButton.Click
        ' Create an instance of the class that will inteact with the Leases table and Residents Table
        Dim factoryLeasesResidents As FactoryLeasesResidents = New FactoryLeasesResidents()

        'Call the method that will deativate the lease and residents.
        factoryLeasesResidents.DeactivateLease(MoveOutDropDownList.SelectedValue)

        'Reset the home page.
        Response.Redirect("Default.aspx")
    End Sub

    Protected Sub CancelButton_Click(sender As Object, e As EventArgs) Handles CancelButton.Click
        'Reset the home page.
        Response.Redirect("Default.aspx")
    End Sub
End Class