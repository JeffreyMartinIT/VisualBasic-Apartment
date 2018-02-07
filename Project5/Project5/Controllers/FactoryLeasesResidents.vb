'Jeffrey Martin

''' <summary>
''' Class uses to create data needed for interaction with Leases and Residents tables.
''' </summary>
Public Class FactoryLeasesResidents

#Region "Properties"

    'Used to pass back last error message generated in this class
    Public ReadOnly Property ErrorMessage As String

    'The DataFetcher class is the class that actually interacts with the database
    Private dataFetcher As DataFetcher = New DataFetcher()

#End Region

#Region "Subs"

    ''' <summary>
    ''' Create the data needed for adding a new lease and resident.
    ''' </summary>
    ''' <param name="apartmentNumber">Integer</param>
    ''' <param name="currentRate">Decimal</param>
    ''' <param name="lastName">String</param>
    ''' <param name="firstName">String</param>
    ''' <param name="dateSelected">String</param>
    Public Sub AddNewLease(apartmentNumber As Integer, currentRate As Decimal, lastName As String, firstName As String, dateSelected As String)

        'If the dateSelected is today add the current time to it.
        If dateSelected = Today.ToString() Then

            dateSelected = Date.Now.ToString()

        End If

        'To create the unique lease number take the date and add the apartmentNumber. 
        Dim _leaseNumber As String = dateSelected + apartmentNumber.ToString()

        'To create the unique person ID take the lease number and add the number residents. As this is the first person
        'added to the lease it is one.
        Dim _personID As String = _leaseNumber + "1"

        'Using the DataFetcher class add the new lease.
        dataFetcher.AddNewLease(apartmentNumber, currentRate, dateSelected, _leaseNumber)

        'Using the DataFetcher class add the new resident
        dataFetcher.AddNewResident(lastName, firstName, dateSelected, _leaseNumber, _personID, "1")

    End Sub

    ''' <summary>
    ''' Add a new resident using todays date.
    ''' </summary>
    ''' <param name="lastName">String</param>
    ''' <param name="firstName">String</param>
    ''' <param name="dateSelected">String</param>
    ''' <param name="leaseNumber">String</param>
    ''' <param name="personID">String</param>
    ''' <param name="signedBool">Boolean</param>
    Public Sub AddNewResident(lastName As String, firstName As String, dateSelected As String, leaseNumber As String, personID As String, signedBool As Boolean)

        'If the dateSelected is today add the current time to it.
        If dateSelected = Today.ToString() Then

            dateSelected = Date.Now.ToString()

        End If

        _ErrorMessage = dataFetcher.ErrorMessage = ""

        'Using the DataFetcher class add the new resident
        dataFetcher.AddNewResident(lastName, firstName, dateSelected, leaseNumber, personID, signedBool)

        _ErrorMessage = dataFetcher.ErrorMessage

    End Sub

    ''' <summary>
    ''' Takes in the apartment number and finds the leases number passes it through to the DataFetcher class to deactive
    ''' all the residents and the lease.
    ''' </summary>
    ''' <param name="apartmentNumber">Integer</param>
    Public Sub DeactivateLease(apartmentNumber As Integer)

        'Find the lease number
        Dim _leaseNumber As String = dataFetcher.GetLeaseNumnber(apartmentNumber)

        'Using the DataFetcher classes deactivate all the residents and the lease associtaed with the lease number.
        dataFetcher.DeactivateResidentsForALease(_leaseNumber)
        dataFetcher.DeactivateLease(_leaseNumber)

    End Sub

#End Region

#Region "Functions"

    ''' <summary>
    ''' Returns the number of active residents associated with a lease number
    ''' </summary>
    ''' <param name="leaseNumber">String</param>
    ''' <returns>Int16</returns>
    Public Function GetNumberOfResidentsWithSameLeaseNumber(leaseNumber As String) As Int16
        Return dataFetcher.GetNumberOfResidentsPerLease(leaseNumber)
    End Function

    ''' <summary>
    ''' Retruns a list of residents for a supplied lease number.
    ''' </summary>
    ''' <param name="leaseNumber">String</param>
    ''' <returns>String</returns>
    Public Function GetResidentsInAnApartment(leaseNumber As String) As String
        Return dataFetcher.GetNameOfResidents(leaseNumber)
    End Function

#End Region

End Class
