'Jeffrey Martin

''' <summary>
''' Class used when interacting only with the Leases Table. 
''' </summary>
Public Class FactoryLeases

#Region "Variables"

    'The DataFetcher class is the class that actually interacts with the database
    Dim dataFetcher As DataFetcher = New DataFetcher()

#End Region

#Region "Properties"

    'Used to pass back last error message generated in this class
    Public ReadOnly Property ErrorMessage As String

#End Region

#Region "Functions"

    ''' <summary>
    ''' Take in a apartmentNumber and check if there is a valid lease for it.
    ''' This is only used internally as sql injection is not accounted for.
    ''' </summary>
    ''' <param name="apartmentNumber">Int16</param>
    ''' <returns>Boolean</returns>
    Public Function ApartmentNumberLeased(apartmentNumber As Int16) As Boolean
        Try

            'Build the sql statement needed to check that the apartment number is active in the 
            'Leases table.
            Return dataFetcher.ObjectFoundOleDbCommand($"SELECT [ApartmentNumber]
                                                    From [dbo].[Leases]
                                                    Where [ApartmentNumber] = {apartmentNumber}
                                                    AND [Active] = 1")

            'As an error has happened capture the error message and return false.
        Catch ex As Exception

            _ErrorMessage = ex.ToString() + " Error"
            MsgBox(ErrorMessage)
            Return False

        End Try

    End Function

    ''' <summary>
    ''' Return the lease number for active apartmentNumber
    ''' </summary>
    ''' <param name="apartmentNumber">String</param>
    ''' <returns>String</returns>
    Public Function LeaseNumberForApartment(apartmentNumber As String) As String

        Try
            Return dataFetcher.GetLeaseNumnber(apartmentNumber)

            'As an error has happened capture the error message and return false.
        Catch ex As Exception

            _ErrorMessage = ex.ToString() + " Error"
            MsgBox(ErrorMessage)
            Return False

        End Try

    End Function

    ''' <summary>
    ''' Return the list of transaction for the lease number supplied.
    ''' </summary>
    ''' <param name="leaseNumber">String</param>
    ''' <returns>String</returns>
    Public Function ListOfTransactionForLease(leaseNumber As String) As String()

        Try

            Return dataFetcher.GetListOfApartmentTransactions(leaseNumber)

        Catch ex As Exception
            Dim ErrorMessage(4) As String
            _ErrorMessage = ex.ToString() + " Error"
            MsgBox(ErrorMessage)

            For Each Message In ErrorMessage
                ErrorMessage(Message) = _ErrorMessage
            Next

            Return ErrorMessage

        End Try

    End Function

    ''' <summary>
    ''' Retrun the current rent amount based upon the lease number supplied.
    ''' </summary>
    ''' <param name="leaseNumber">String</param>
    ''' <returns>String</returns>
    Public Function GetRentAmountForLease(leaseNumber As String) As String
        Try

            Return dataFetcher.GetRentAmount(leaseNumber)

        Catch ex As Exception

            _ErrorMessage = ex.ToString() + " Error"
            MsgBox(ErrorMessage)
            Return False

        End Try
    End Function

#End Region

#Region "Subs"

    Public Sub AddTransaction(description As String, type As String, amount As Decimal, leaseNumber As String, transactionDate As String, paymentType As String)

        dataFetcher.AddTransaction(description, type, amount, leaseNumber, transactionDate, paymentType)

    End Sub

#End Region

End Class
