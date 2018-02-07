'Jeffrey Martin

''' <summary>
''' New Contract page.  Here the user can create a new contract.
''' </summary>
Public Class About
    Inherits Page

#Region "Backing fields"

    'Needed to hold the parsed value for validation
    Private _currentRateDecimal As Decimal

#End Region

#Region "Subs"

    ''' <summary>
    ''' Fill the list box with apartments numbers that do not have an active lease upon loading of the page.
    ''' </summary>
    ''' <param name="sender">Object</param>
    ''' <param name="e">EventArgs</param>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

        'Places appropriate message into each error message.
        CalendarErrorLabel.Text = " Select a Date on the Calendar."
        ApartmnetFullLabel.Text = " You must select an apartment number."
        RateErrorLabel.Text = " You must enter the current monthly lease amount."
        LastNameErrorLabel.Text = " You must enter a Last Name"
        FirstNameErrorLabel.Text = " You must enter a First Name."


        'Create the class that handles information needed for the Leases table.
        Dim factoryLeases As FactoryLeases = New FactoryLeases()

        Dim _apartmentNumber As Int16

        ResetErrorLabels()

        'Check that the list has not already been loaded. Depending on the life cycle of the page,
        'Page_Load can be rerun with out the page being destroyed first. If this happens and
        'the ListBox would get duplicate items listed in it.
        If AppartmentNumberListBox.Items.Count <= 1 Then

            'Try each apartment number.
            For _apartmentNumber = 1 To 8

                'Use the method that will check if the apartment number has a lease that is active and
                'then add it to the List box if it is active.
                If (Not factoryLeases.ApartmentNumberLeased(_apartmentNumber)) Then

                    AppartmentNumberListBox.Items.Add(_apartmentNumber)

                End If

            Next

            'Load the appropriate message based on if the list is empty or not of apartment numbers.
            If AppartmentNumberListBox.Items.Count <= 1 Then

                ApartmnetFullLabel.Text = "All apartments are full. Can not sign a new lease. Good work!"
                ApartmnetFullLabel.Visible = True
            Else

                ApartmnetFullLabel.Text = ""

            End If

        End If

    End Sub

    ''' <summary>
    ''' When button is pressed, validate data needed to create a new lease and then create it.
    ''' </summary>
    ''' <param name="sender">Object</param>
    ''' <param name="e">EventArgs</param>
    Protected Sub CreateContractButton_Click(sender As Object, e As EventArgs) Handles CreateContractButton.Click

        Dim _validatedBoolean As Boolean = True

        ResetErrorLabels()

        'Validate items from top to bottom on the page. Create an error message 
        'that will pop up and set _validatedBoolean to false if it fails
        'validation.
        Select Case (False)

            'Check that the default date value is not present.
            Case (DateSignedCalendar.SelectedDate.Date <> DateTime.MinValue)
                CalendarErrorLabel.Visible = True
                _validatedBoolean = False

            'Check that an apartment number has been selected
            Case (AppartmentNumberListBox.SelectedValue <> "Select")
                ApartmnetFullLabel.Visible = True
                AppartmentNumberListBox.Focus()
                _validatedBoolean = False

            'Check that the value will parce into a decimal.
            'Buisness rule could establish a range the value needs to be in or
            'could be a drop down list with rates in a rent amounts table.
            Case (Decimal.TryParse(CurrentRateTextBox.Text, _currentRateDecimal))
                RateErrorLabel.Visible = True
                CurrentRateTextBox.Focus()
                _validatedBoolean = False

            'Check that somthing has been entered.
            Case (LastNameTextBox.Text.Trim() <> "")
                LastNameErrorLabel.Visible = True
                LastNameTextBox.Focus()
                _validatedBoolean = False

            'Check that somthing has been entered.
            Case (FirstNameTextBox.Text.Trim() <> "")
                FirstNameErrorLabel.Visible = True
                FirstNameTextBox.Focus()
                _validatedBoolean = False

        End Select

        'If data has been validated then use the method in the class that handles the leases and residents tables to add the new lease and the resident that signed it.
        'Then go to the homepage and see the new list of leases.
        If _validatedBoolean Then

            Dim factoryLeasesResidents As FactoryLeasesResidents = New FactoryLeasesResidents()

            factoryLeasesResidents.AddNewLease(Integer.Parse(AppartmentNumberListBox.SelectedValue.ToString()), _currentRateDecimal,
                                               LastNameTextBox.Text, FirstNameTextBox.Text, DateSignedCalendar.SelectedDate.ToString())

            Response.Redirect("CurrentContracts.aspx")

        End If

    End Sub

    ''' <summary>
    ''' Reset all the error labels
    ''' </summary>
    Private Sub ResetErrorLabels()
        CalendarErrorLabel.Visible = False
        ApartmnetFullLabel.Visible = False
        RateErrorLabel.Visible = False
        LastNameErrorLabel.Visible = False
        FirstNameErrorLabel.Visible = False
    End Sub

#End Region

End Class