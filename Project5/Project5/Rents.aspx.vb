'Jeffrey Martin

''' <summary>
''' Code to keep track of charges to residents and payments.
''' </summary>
Public Class Rents
    Inherits System.Web.UI.Page

#Region "Variables"

    Dim factoryLeases As FactoryLeases = New FactoryLeases()

    Dim factoryLeasesResidents As FactoryLeasesResidents = New FactoryLeasesResidents()

    Dim _paymentBool As Boolean = False

    Dim _transactionAmountDecimal As Decimal

    Dim _leaseNumber As String

#End Region

#Region "Subs"

    ''' <summary>
    ''' Items done upon loading of page
    ''' </summary>
    ''' <param name="sender">Object</param>
    ''' <param name="e">System.EventArgs</param>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'Check that the list has not already been loaded. Depending on the life cycle of the page,
        'Page_Load can be rerun with out the page being destroyed first. If this happens and
        'the ListBox would get duplicate items listed in it.
        If ResidentAppartmentNumberListBox.Items.Count <= 1 Then

            AmountErrorLabel.Text = "Please enter a number"
            DescriptionLabel.Text = "Transaction Detail:"
            TransactionChoiceDropErrorLabel.Text = "Please choose a transaction type."
            DescriptionTextBoxErrorLabel.Text = "Please enter a description."
            TransactionChoiceDropErrorLabel.Visible = False
            InputDataPanel.Visible = False
            ManageRents.Visible = False
            AmountErrorLabel.Visible = False
            DateTextBoxErrorLabel.Visible = False
            DescriptionTextBoxErrorLabel.Visible = False

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

                ResidentsApartmentNumberLabelMessage.Visible = True
                ResidentsApartmentNumberLabelMessage.Text = "No apartments are rented. Get selling!"

            Else

                ResidentsApartmentNumberLabelMessage.Visible = False
                ResidentsApartmentNumberLabelMessage.Text = "Please select an apartment to manage rents for."

            End If

        End If

        ResetErrorLabels()
    End Sub

    ''' <summary>
    ''' Gets rid of any error messages on the page.
    ''' </summary>
    Private Sub ResetErrorLabels()
        ResidentsApartmentNumberLabelMessage.Visible = False
    End Sub

    ''' <summary>
    ''' Refreshes the page so the user can change apartment numbers.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Protected Sub ChangeApartmentNumberButton_Click(sender As Object, e As EventArgs) Handles ChangeApartmentNumberButton.Click
        Response.Redirect("Rents.aspx")
    End Sub

    ''' <summary>
    ''' Event handler to save the transaction.
    ''' </summary>
    ''' <param name="sender">Object</param>
    ''' <param name="e">EventArgs</param>
    Protected Sub SaveTransactionButton_Click(sender As Object, e As EventArgs) Handles SaveTransactionButton.Click

        'Clear ot the Error labels
        AmountErrorLabel.Visible = False
        DateTextBoxErrorLabel.Visible = False
        DescriptionTextBoxErrorLabel.Visible = False

        'Validation starts out as false, till all inputs have been validated.
        Dim _validatedBoolean As Boolean = False

        'Validates the inputs, using case the error messages will pop up one at a time.
        Select Case (False)

            Case (DescriptionTextBox.Text.Trim() <> "")
                DescriptionTextBoxErrorLabel.Visible = True

            Case (DateTextBox.Text.Trim() <> "")
                DateTextBoxErrorLabel.Visible = True

            Case (EnterAmountLabel.Text.Trim() <> "")
                AmountErrorLabel.Visible = True

            Case (AmountTextBox.Text <> "")
                AmountErrorLabel.Visible = True

            Case Else
                _validatedBoolean = True
                AddTransaction()
        End Select

    End Sub

    ''' <summary>
    ''' Adds the transaction to the database.
    ''' </summary>
    Private Sub AddTransaction()

        _transactionAmountDecimal = Convert.ToDecimal(AmountTextBox.Text)

        'Get the lease number bases upon the apartment number selected.
        _leaseNumber = factoryLeases.LeaseNumberForApartment(ResidentAppartmentNumberListBox.SelectedValue).Trim()

        Dim paymentType As String = ""

        If TransactionChoiceDropDownList.SelectedValue = "Payment" Then
            paymentType = PaymentTypeDropDownList.SelectedValue
        End If

        'Add the transaction to the databse.
        factoryLeases.AddTransaction(DescriptionTextBox.Text, TransactionChoiceDropDownList.SelectedValue,
                                     _transactionAmountDecimal, _leaseNumber, DateTextBox.Text, paymentType)

        'Reprint all the transaction for the apartment number.
        OutputTransactions(_leaseNumber)

    End Sub

    ''' <summary>
    ''' Gets all the transactions for an apartment number and displays them on the rents page.
    ''' </summary>
    ''' <param name="leaseNumber">String</param>
    Private Sub OutputTransactions(leaseNumber As String)

        Dim TransactionListArray(4) As String

        'Get the list of transactions
        TransactionListArray = factoryLeases.ListOfTransactionForLease(leaseNumber)

        'Place the data into the table
        Cell0.Text = TransactionListArray(0)
        Cell1.Text = TransactionListArray(1)
        Cell2.Text = TransactionListArray(2)
        Cell3.Text = TransactionListArray(3)

    End Sub

    ''' <summary>
    ''' Event handler to select what type of transaction is being done.
    ''' </summary>
    ''' <param name="sender">Object</param>
    ''' <param name="e">EventArgs</param>
    Protected Sub TransactionChoiceDropDownList_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TransactionChoiceDropDownList.SelectedIndexChanged

        PaymentTypeDropDownList.Visible = False

        If TransactionChoiceDropDownList.SelectedValue = "Select" Then
            TransactionChoiceDropErrorLabel.Visible = True

        Else

            _paymentBool = False

            DescriptionTextBox.Enabled = False

            AmountTextBox.Enabled = True

            _leaseNumber = factoryLeases.LeaseNumberForApartment(ResidentAppartmentNumberListBox.SelectedValue)

            'Do the appropriate action based upon the transaction type chosen.
            Select Case TransactionChoiceDropDownList.SelectedValue

                Case "Retrieve Rent Amount"
                    DescriptionTextBox.Text = "Rent Due"
                    AmountTextBox.Text = factoryLeases.GetRentAmountForLease(_leaseNumber)
                    AmountTextBox.Enabled = False
                Case "Payment"
                    _paymentBool = True
                    DescriptionTextBox.Text = "Payment"
                    PaymentTypeDropDownList.Visible = True
                Case "Late Charge"
                    DescriptionTextBox.Text = "Late Charge"

                Case "Other Charge"
                    DescriptionTextBox.Text = ""
                    DescriptionTextBox.Enabled = True

                Case "Deposit"
                    DescriptionTextBox.Text = "Deposit"

                Case "Pet Deposit"
                    DescriptionTextBox.Text = "Pet Deposit"

                Case "Credit"
                    DescriptionTextBox.Text = ""
                    DescriptionTextBox.Enabled = True
                    _paymentBool = True

                Case Else
                    TransactionChoiceDropErrorLabel.Visible = True
                    TransactionChoiceDropErrorLabel.Text = $"Error, please contact system adminstrator. Tell them that the selected item '{TransactionChoiceDropDownList.SelectedValue}' is not working"

            End Select

            InputDataPanel.Visible = True
            TransactionChoiceDropErrorLabel.Visible = False
        End If
    End Sub

    ''' <summary>
    ''' Event handler for when the user chooses the apartment number.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Protected Sub ResidentAppartmentNumberListBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ResidentAppartmentNumberListBox.SelectedIndexChanged
        If (ResidentAppartmentNumberListBox.SelectedValue = "Select") Then

            'Show the error message as an apartment has not been chosen.
            ResidentsApartmentNumberLabelMessage.Visible = True

        Else

            'Change the panel that is visable to the one used for inputting data of the transaction.
            ManageRents.Visible = True
            ChooseApartmentNumber.Visible = False
            DateTextBox.Text = System.DateTime.Now.ToShortDateString()
            _leaseNumber = factoryLeases.LeaseNumberForApartment(ResidentAppartmentNumberListBox.SelectedValue)
            ApartmentChosenLabel.Text = factoryLeasesResidents.GetResidentsInAnApartment(_leaseNumber)
            OutputTransactions(_leaseNumber)

        End If
    End Sub

#End Region

End Class