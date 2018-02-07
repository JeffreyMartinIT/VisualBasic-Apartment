''' <summary>
''' Code for page to add residents.
''' </summary>
Public Class AddResidents
    Inherits System.Web.UI.Page

#Region "Variables"

    'Create the class that handles information needed for the Leases table.
    Dim factoryLeases As FactoryLeases = New FactoryLeases()

    Dim factoryLeaseResidents As FactoryLeasesResidents = New FactoryLeasesResidents()

    Private _leaseNumber As String = ""

#End Region

#Region "Subs"
    ''' <summary>
    ''' Items done upon loading of the page.
    ''' </summary>
    ''' <param name="sender">Object</param>
    ''' <param name="e">System.EventArgs</param>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim _apartmentNumber As Int16

        'Places appropriate message into each error message.
        ResidentCalendarErrofLabel.Text = " Select a Date on the Calendar."
        ResidentLastNameErrorLabel.Text = " You must enter a Last Name"
        ResidentFirstNameErrorLable.Text = " You must enter a First Name."
        SignedLeaseDropDownListErrorLabel.Text = " You must select if the person signed the lease or not."
        ResidentsApartmentNumberLabelMessage.Text = " You must choose an apartment number first. <br>
                                                          If the apartment number is not shown you need to add the contract first."

        'Check that the list has not already been loaded. Depending on the life cycle of the page,
        'Page_Load can be rerun with out the page being destroyed first. If this happens and
        'the ListBox would get duplicate items listed in it.
        If ResidentAppartmentNumberListBox.Items.Count <= 1 Then

            AddResidentsPanel.Visible = False

            'Try each apartment number.
            For _apartmentNumber = 1 To 8

                'Use the method that will check if the apartment number has a lease that is active and
                'then add it to the List box if it is active.
                If (factoryLeases.ApartmentNumberLeased(_apartmentNumber)) Then

                    ResidentAppartmentNumberListBox.Items.Add(_apartmentNumber)

                End If

            Next

            'Load the appropriate message based on if the list is empty or not of apartment numbers.
            If ResidentAppartmentNumberListBox.Items.Count <= 1 Then

                ResidentsApartmentNumberLabelMessage.Text = "All apartments are full. Can not sign a new lease. Good work!"

            Else

                ResidentsApartmentNumberLabelMessage.Text = ""

            End If

        End If



        ResetErrorLabels()

    End Sub

    ''' <summary>
    ''' Validates data has been entered in each category and if valid adds the new resident to the database and
    ''' gives information back to the user of what has been done.
    ''' </summary>
    ''' <param name="sender">Object</param>
    ''' <param name="e">EventArgs</param>
    Protected Sub AddResidentButton_Click(sender As Object, e As EventArgs) Handles AddResidentButton.Click
        Dim _validatedBoolean As Boolean = False

        ResetErrorLabels()

        'Validate items from top to bottom on the page. Create an error message 
        'that will pop up and set _validatedBoolean to false if it fails
        'validation.
        Select Case (False)

            'Check that the default date value is not present.
            Case (DateResidentAddedCalendar.SelectedDate.Date <> DateTime.MinValue)
                ResidentCalendarErrofLabel.Visible = True


            'Check that somthing has been entered.
            Case (ResidentLastNameTextBox.Text.Trim() <> "")
                ResidentLastNameErrorLabel.Visible = True
                ResidentLastNameTextBox.Focus()

            'Check that somthing has been entered.
            Case (ResidentFirstNameTextBox.Text.Trim() <> "")
                ResidentFirstNameErrorLable.Visible = True
                ResidentFirstNameTextBox.Focus()

            'Check that somthing has been selected in the SignedLeaseDropDownList
            Case (SignedLeaseDropDownList.SelectedValue <> "Select")
                SignedLeaseDropDownListErrorLabel.Visible = True
                SignedLeaseDropDownList.Focus()

            Case Else
                _validatedBoolean = True
        End Select

        'If data has been validated then create new person ID and add the person to the database. Then reset the for for adding the next resident to the same apartment.
        If _validatedBoolean Then
            Dim _numberOfResidentWithLeaseNumber As Int16 = 0 'Initialize the count to zero

            'Get the lease number from the database, this redone due to the fact it is possible that with the life cycle of the page, the information has been lost.
            _leaseNumber = FetchLeaseNumberForApartment().Trim()

            'Output the Header for the residents in the database for the apartment number chosen.
            CreateResidentsInApartmentHeader()

            'Get the current number of residents and add on for the new one being added.
            _numberOfResidentWithLeaseNumber = 1 + factoryLeaseResidents.GetNumberOfResidentsWithSameLeaseNumber(_leaseNumber)

            'Create the unique person Id by tacking the lease number concatonating the number of residents that number of residents that have the same lease number to it.
            Dim _personId As String = _leaseNumber & _numberOfResidentWithLeaseNumber.ToString()

            'Transform the drop down list chosen into a boolean needed in the database.
            Dim _signedBool As Boolean
            If (SignedLeaseDropDownList.SelectedValue = "Signed Lease") Then
                _signedBool = True
            Else
                _signedBool = False

            End If

            'Add the new resident to the database
            factoryLeaseResidents.AddNewResident(ResidentLastNameTextBox.Text, ResidentFirstNameTextBox.Text, DateResidentAddedCalendar.SelectedDate.ToString(), _leaseNumber, _personId, _signedBool)

            'Add the new resident to the list of residents on the view.
            PrimaryLeaserLabel.Text += $"<br>{ResidentLastNameTextBox.Text}, {ResidentFirstNameTextBox.Text}, {SignedLeaseDropDownList.SelectedValue}"

            'Set form to be ready to add next resident.
            ResidentFirstNameTextBox.Text = ""
            SignedLeaseDropDownList.SelectedValue = "Resident"


        End If
    End Sub

    ''' <summary>
    ''' Makes all error messages invisable.
    ''' </summary>
    Private Sub ResetErrorLabels()

        ResidentCalendarErrofLabel.Visible = False
        ResidentLastNameErrorLabel.Visible = False
        ResidentFirstNameErrorLable.Visible = False
        SignedLeaseDropDownListErrorLabel.Visible = False
        ResidentsApartmentNumberLabelMessage.Visible = False
    End Sub

    ''' <summary>
    ''' Creates the header needed for the list of residents in an apartment.
    ''' </summary>
    Private Sub CreateResidentsInApartmentHeader()

        PrimaryLeaserLabel.Text = $"Residents for apartment {ResidentAppartmentNumberListBox.Text} with lease number of {_leaseNumber} 
                                         {FetchNameOfResidentsForApartment(_leaseNumber)}"
    End Sub

    ''' <summary>
    ''' Validates that an apartment number has been chosen is so will change the panel that is visable to the one used for
    ''' inputting data of the new resident.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub StartAddResidentButton_Click(sender As Object, e As EventArgs) Handles AddResidentForApartmentChosenButton.Click


        If (ResidentAppartmentNumberListBox.SelectedValue = "Select") Then

            'Show the error message as an apartment has not been chosen.
            ResidentsApartmentNumberLabelMessage.Visible = True

        Else


            'Get the lease number from the database.
            _leaseNumber = FetchLeaseNumberForApartment().Trim()

            CreateResidentsInApartmentHeader()

            'Change the panel that is visable to the one used for inputting data of the new resident.
            AddResidentsPanel.Visible = True
            SelectApartmentNumberPanel.Visible = False

        End If
    End Sub

#End Region

#Region "Functions"

    ''' <summary>
    ''' Use the FactoryLeases that hooks up with the data fetcher to get the Lease Number of the apartment chosen.
    ''' </summary>
    ''' <returns>String</returns>
    Private Function FetchLeaseNumberForApartment() As String

        Return factoryLeases.LeaseNumberForApartment(ResidentAppartmentNumberListBox.SelectedValue)

    End Function

    ''' <summary>
    ''' Use the FactoryLeaseResident that hooks up with the data fetcher to get the names of the residents of the apartment.
    ''' </summary>
    ''' <param name="leaseNumber">String</param>
    ''' <returns>String</returns>
    Private Function FetchNameOfResidentsForApartment(leaseNumber As String) As String

        Return factoryLeaseResidents.GetResidentsInAnApartment(leaseNumber)

    End Function


#End Region


End Class