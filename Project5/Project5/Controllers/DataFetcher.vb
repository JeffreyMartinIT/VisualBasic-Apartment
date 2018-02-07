'Jeffrey Martin


Imports System.Data.OleDb
Imports System.Configuration
Imports System.Data.SqlClient

''' <summary>
''' Class that is used to interact with the database
''' </summary>
Public Class DataFetcher

#Region "Variables"

    'Variables used to create and hold the connection string settings
    Dim rootWebConfig As Configuration
    Dim connString As ConnectionStringSettings

#End Region

#Region "Properties"

    'Used to pass back last error message generated in this class
    Public ReadOnly Property ErrorMessage As String

#End Region

#Region "Constructor"

    ''' <summary>
    ''' Default constructor that creates the connection string when class is istanciated.
    ''' </summary>
    Public Sub New()

        rootWebConfig = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("/Project5")

        'If rootWebCnfig did not fine the file, then do not try to hook up the connection string setting as
        'this would cause an error.
        If (rootWebConfig.ConnectionStrings.ConnectionStrings.Count > 0) Then

            connString = rootWebConfig.ConnectionStrings.ConnectionStrings("apartmentConnectionString")

        End If

    End Sub

#End Region

#Region "Functions"

    ''' <summary>
    ''' Add a new lease in the Leases Table.  This is sql injection protected so can use data from a user.
    ''' </summary>
    ''' <param name="apartmentNumber">Integer</param>
    ''' <param name="currentRate">Decimal</param>
    ''' <param name="dateSelected">String</param>
    ''' <param name="leaseNumber">String</param>
    ''' <returns>Boolean</returns>
    Public Function AddNewLease(apartmentNumber As Integer, currentRate As Decimal,
                                   dateSelected As String, leaseNumber As String) As Boolean

        _ErrorMessage = ""

        'The sql string needed to add a new lease.  As it is a new lease active is set to true.
        Dim sqlLeaseString As String = "Begin Try
                                            INSERT INTO [dbo].[Leases]
                                                ([LeaseNumber],[ApartmentNumber],[DateSigned],[CurrentRate],[Active])
                                            VALUES(@LeaseNumber, @ApartmentNumber, @DateSigned, @CurrentRate, 1);
                                        COMMIT
                                        END TRY
                                        BEGIN CATCH
                                            If @@TRANCOUNT > 0
                                                ROLLBACK
                                        END CATCH"

        'Create the connection string that is closed at end using.
        Using conn As New SqlConnection(connString.ConnectionString)

            Dim cmd As New SqlCommand(sqlLeaseString, conn)

            Try

                conn.Open()

                'Add the parameters to protect against sql injection
                cmd.Parameters.AddWithValue("@LeaseNumber", leaseNumber)
                cmd.Parameters.AddWithValue("@ApartmentNumber", apartmentNumber)
                cmd.Parameters.AddWithValue("@DateSigned", dateSelected)
                cmd.Parameters.AddWithValue("@CurrentRate", currentRate)

                Dim rows As Integer
                rows = cmd.ExecuteNonQuery()

            Catch ex As Exception

                _ErrorMessage = ex.Message.ToString() & " Error "
                Return False

            End Try

        End Using

        Return True

    End Function

    ''' <summary>
    ''' Create a new resident in the Resident table.This is sql injection protected so can use data from a user.
    ''' </summary>
    ''' <param name="lastName"></param>
    ''' <param name="firstName"></param>
    ''' <param name="dateSelected"></param>
    ''' <param name="leaseNumber"></param>
    ''' <param name="personID"></param>
    ''' <param name="signed"></param>
    ''' <returns></returns>
    Public Function AddNewResident(lastName As String, firstName As String,
                                   dateSelected As String, leaseNumber As String, personID As String, signed As String) As Boolean

        _ErrorMessage = ""

        'Create the sql string used to add a new resident. As it is a new resident active must be true.
        Dim sqlResidentString As String = "Begin Try
                                                 INSERT INTO [dbo].[Residents]
                                                      ([PersonID], [LeaseNumber], [LastName], [FirstName], [AddedDate], [Signed], [Active])
                                                VALUES(@PersonID, @LeaseNumber, @LastName, @FirstName, @AddedDate, @Signed, 1);

                                            COMMIT
                                            END TRY
                                            BEGIN CATCH
                                                If @@TRANCOUNT > 0
                                                    ROLLBACK
                                            END CATCH"


        'Create the connection string that is closed at end using.
        Using conn As New SqlConnection(connString.ConnectionString)

            Dim cmd2 As New SqlCommand(sqlResidentString, conn)

            Try

                conn.Open()

                'Add the parameters to protect against sql injection
                cmd2.Parameters.AddWithValue("@PersonID", personID)
                cmd2.Parameters.AddWithValue("@LeaseNumber", leaseNumber)
                cmd2.Parameters.AddWithValue("@LastName", lastName)
                cmd2.Parameters.AddWithValue("@FirstName", firstName)
                cmd2.Parameters.AddWithValue("@AddedDate", dateSelected)
                cmd2.Parameters.AddWithValue("@Signed", signed)

                Dim rows As Integer
                rows = cmd2.ExecuteNonQuery()

            Catch ex As Exception

                _ErrorMessage = ex.Message.ToString() & " Error "
                Return False

            End Try

        End Using

        Return True

    End Function

    Public Function AddTransaction(description As String, type As String, amount As Decimal, leaseNumber As String, transactionDate As String, paymentType As String) As Boolean

        _ErrorMessage = ""

        Dim sqlResidentString As String = "INSERT INTO [dbo].[BillingResidents]
                                                      ([Description], [Type], [Amount], [LeaseNumber], [TransactionDate], [PaymentType])
                                                VALUES(@Description, @Type, @Amount, @LeaseNumber, @TransactionDate, @PaymentType);"


        'Dim sqlResidentString As String = "Begin Try
        '                                         INSERT INTO [dbo].[BillingResidents]
        '                                              ([Description], [Type], [Amount], [LeaseNumber], [TransactionDate], [PaymentType])
        '                                        VALUES(@Description, @Type, @Amount, @LeaseNumber, @TransactionDate, @PaymentType);

        '                                    COMMIT
        '                                    END TRY
        '                                    BEGIN CATCH
        '                                        If @@TRANCOUNT > 0
        '                                            ROLLBACK
        '                                    END CATCH"


        'Create the connection string that is closed at end using.
        Using conn As New SqlConnection(connString.ConnectionString)

            Dim cmd2 As New SqlCommand(sqlResidentString, conn)

            Try

                conn.Open()

                'Add the parameters to protect against sql injection
                cmd2.Parameters.AddWithValue("@Description", description)
                cmd2.Parameters.AddWithValue("@Type", type)
                cmd2.Parameters.AddWithValue("@Amount", amount)
                cmd2.Parameters.AddWithValue("@LeaseNumber", leaseNumber)
                cmd2.Parameters.AddWithValue("@TransactionDate", transactionDate)
                cmd2.Parameters.AddWithValue("@PaymentType", paymentType)

                Dim rows As Integer
                rows = cmd2.ExecuteNonQuery()

            Catch ex As Exception

                _ErrorMessage = ex.Message.ToString() & " Error "
                Return False

            End Try

            If type = "Payment" Then

            End If

        End Using



        Return True

    End Function



    ''' <summary>
    ''' Search a database and return if found. The queryString sent in needs to be one that will return
    ''' a boolean. This is not sql injection protected so should only be used internally.
    ''' </summary>
    ''' <param name="queryString">String</param>
    ''' <returns>Boolean</returns>
    Public Function ObjectFoundOleDbCommand(ByVal queryString As String) As Boolean

        _ErrorMessage = ""
        Dim foundBool As Boolean = False

        'Create the connection string that is closed at end using.
        Using conn As New SqlConnection(connString.ConnectionString)

            Dim cmd As New SqlCommand(queryString, conn)

            Try

                conn.Open()
                foundBool = cmd.ExecuteScalar

            Catch ex As Exception

                _ErrorMessage = ex.Message.ToString() & " Error "
                Return False

            End Try

        End Using

        Return foundBool

    End Function

    ''' <summary>
    ''' Get the lease number from the Leases table base upon the apartment number.
    ''' This is sql injection protected so can use data from a user.
    ''' </summary>
    ''' <param name="apartmentNumber">String</param>
    ''' <returns>String</returns>
    Public Function GetLeaseNumnber(apartmentNumber As String) As String

        _ErrorMessage = ""

        'Create the sql string that will be used to find the lease number from the Leases database.
        'Note that this will only find active leases for a given apartment number.
        Dim sqlLeaseString As String = "SELECT [LeaseNumber] FROM [dbo].[Leases]
                                        WHERE [ApartmentNumber] = @ApartmentNumber AND [Active] = 1;"

        Dim returnString As String

        'Create the connection string that is closed at end using.
        Using conn As New SqlConnection(connString.ConnectionString)

            Dim cmd As New SqlCommand(sqlLeaseString, conn)

            Try
                conn.Open()

                'Add the parameters to protect against sql injection
                cmd.Parameters.AddWithValue("@ApartmentNumber", apartmentNumber)

                returnString = cmd.ExecuteScalar()

            Catch ex As Exception

                _ErrorMessage = ex.Message.ToString() & " Error "

                Return False

            End Try

        End Using

        Return returnString

    End Function

    Public Function GetRentAmount(leaseNumber As String) As String

        _ErrorMessage = ""

        'Create the sql string that will be used to find the lease number from the Leases database.
        'Note that this will only find active leases for a given apartment number.
        Dim sqlLeaseString As String = "SELECT [CurrentRate] FROM [dbo].[Leases]
                                        WHERE [LeaseNumber] = @LeaseNumber;"

        Dim returnString As String

        'Create the connection string that is closed at end using.
        Using conn As New SqlConnection(connString.ConnectionString)

            Dim cmd As New SqlCommand(sqlLeaseString, conn)

            Try
                conn.Open()

                'Add the parameters to protect against sql injection
                cmd.Parameters.AddWithValue("@LeaseNumber", leaseNumber)

                returnString = cmd.ExecuteScalar()

            Catch ex As Exception

                _ErrorMessage = ex.Message.ToString() & " Error "

                Return False

            End Try

        End Using

        Return returnString

    End Function

    Public Function GetNameOfResidents(leaseNumber As String) As String
        _ErrorMessage = ""

        'Create the sql string that will be used to find the lease number from the Leases database.
        'Note that this will only find active leases for a given apartment number.
        Dim sqlLeaseString As String = "SELECT [LastName], [FirstName] ,[Signed] FROM [dbo].[Residents]
                                        WHERE [LeaseNumber] = @LeaseNumber AND [Active] = 1;"

        Dim returnString As String = ""

        'Create the connection string that is closed at end using.
        Using conn As New SqlConnection(connString.ConnectionString)

            Dim cmd As New SqlCommand(sqlLeaseString, conn)

            Try
                conn.Open()

                'Add the parameters to protect against sql injection
                cmd.Parameters.AddWithValue("@LeaseNumber", leaseNumber)


                Dim reader As SqlDataReader = cmd.ExecuteReader()
                Dim resident As String = ""

                While reader.Read()
                    If (reader(2) = "True") Then
                        resident = "Signed Lease"
                    Else
                        resident = "Resident"
                    End If
                    returnString += String.Format("<br>{0}, {1}, {2}",
                            reader(0), reader(1), resident)
                End While

            Catch ex As Exception

                returnString = ex.Message.ToString() & " Error "

                Return False

            End Try

        End Using

        Return returnString
    End Function

    Public Function GetListOfApartmentTransactions(leaseNumber As String) As String()
        _ErrorMessage = ""

        'Create the sql string that will ............
        Dim sqlLeaseString As String = "SELECT [Description], [Type] ,[Amount], [TransactionDate], [TransactioinID], [PaymentType] FROM [dbo].[BillingResidents]
                                        WHERE [LeaseNumber] = @LeaseNumber
                                        ORDER BY [TransactionDate];"

        Dim returnString(4) As String
        returnString(0) = "Date<br>"
        returnString(1) = "Description<br>"
        returnString(2) = "Amount<br>"
        returnString(3) = "Ballance<br>"

        Dim ballanceAccumulator As Decimal = 0

        'Create the connection string that is closed at end using.
        Using conn As New SqlConnection(connString.ConnectionString)

            Dim cmd As New SqlCommand(sqlLeaseString, conn)

            Try
                conn.Open()

                'Add the parameters to protect against sql injection
                cmd.Parameters.AddWithValue("@LeaseNumber", leaseNumber)


                Dim reader As SqlDataReader = cmd.ExecuteReader()
                Dim paymentType As String = ""
                Dim amount As String
                Dim type As String

                While reader.Read()
                    type = reader(1)
                    If (type.Trim() = "Payment") Then
                        'Get the payment type 
                        paymentType = $"{reader(5)}"
                    Else
                        paymentType = ""
                    End If

                    If (type.Trim() = "Payment" Or type.Trim() = "Credit") Then

                        amount = reader(2)
                        ballanceAccumulator += reader(2)

                    Else

                        amount = "(" & reader(2) & ")"
                        ballanceAccumulator -= reader(2)

                    End If

                    returnString(0) += $"{reader(3)} <br>"
                    returnString(1) += $"{reader(0)} {paymentType} <br>"
                    returnString(2) += $"{amount} <br>"
                    returnString(3) += $"{ballanceAccumulator:c} <br>"
                End While



            Catch ex As Exception

                returnString(3) = ex.Message.ToString() & " Error "

                Return returnString

            End Try

        End Using

        Return returnString
    End Function


    Public Function GetNumberOfResidentsPerLease(leaseNumber As String) As Int16

        _ErrorMessage = ""

        'Create the sql string that will be used to find how many residents have the same lease number.
        Dim sqlLeaseString As String = "SELECT [LeaseNumber] FROM [dbo].[Residents]
                                        WHERE [LeaseNumber] = @LeaseNumber;"

        Dim returnInt As Int16 = 0

        'Create the connection string that is closed at end using.
        Using conn As New SqlConnection(connString.ConnectionString)

            Dim cmd As New SqlCommand(sqlLeaseString, conn)

            Try
                conn.Open()

                'Add the parameters to protect against sql injection
                cmd.Parameters.AddWithValue("@LeaseNumber", leaseNumber)


                Dim reader As SqlDataReader = cmd.ExecuteReader()

                While reader.Read()
                    returnInt += 1
                End While

            Catch ex As Exception

                returnInt = ex.Message.ToString() & " Error "

                Return False

            End Try

        End Using

        Return returnInt
    End Function

#End Region

#Region "Subs"

    ''' <summary>
    ''' Updates the Active field to false for all residents who have the passed in lease number.
    ''' This is sql injection protected so can use data from a user.
    ''' </summary>
    ''' <param name="leaseNumber">String</param>
    Public Sub DeactivateResidentsForALease(leaseNumber As String)

        _ErrorMessage = ""

        'Sql string needed to deactivate all residents 
        Dim sqlResidentString As String = "Begin Try
                                                 UPDATE [dbo].[Residents]
                                                 SET [Active] = 0
                                                 WHERE [LeaseNumber] = @LeaseNumber;

                                            COMMIT
                                            END TRY
                                            BEGIN CATCH
                                                If @@TRANCOUNT > 0
                                                    ROLLBACK
                                            END CATCH"

        'Create the connection string that is closed at end using.
        Using conn As New SqlConnection(connString.ConnectionString)

            Dim cmd2 As New SqlCommand(sqlResidentString, conn)

            Try

                conn.Open()

                'Add the parameters to protect against sql injection
                cmd2.Parameters.AddWithValue("@LeaseNumber", leaseNumber)

                Dim rows As Integer
                rows = cmd2.ExecuteNonQuery()

            Catch ex As Exception

                _ErrorMessage = ex.Message.ToString() & " Error "

            End Try

        End Using

    End Sub

    ''' <summary>
    ''' Updates the Active field to false for the lease based upon the lease number.
    ''' This is sql injection protected so can use data from a user.
    ''' </summary>
    ''' <param name="leaseNumber">String</param>
    Public Sub DeactivateLease(leaseNumber As String)

        _ErrorMessage = ""

        'The sql string needed to update the active field to false based upon the lease number.
        Dim sqlLeaseString As String = "Begin Try
                                            UPDATE [dbo].[Leases]
                                            SET [Active] = 0
                                            WHERE [LeaseNumber] = @LeaseNumber;
                                        COMMIT
                                        END TRY
                                        BEGIN CATCH
                                            If @@TRANCOUNT > 0
                                                ROLLBACK
                                        END CATCH"

        'Create the connection string that is closed at end using.
        Using conn As New SqlConnection(connString.ConnectionString)

            Dim cmd As New SqlCommand(sqlLeaseString, conn)

            Try

                conn.Open()

                'Add the parameters to protect against sql injection
                cmd.Parameters.AddWithValue("@LeaseNumber", leaseNumber)

                Dim rows As Integer
                rows = cmd.ExecuteNonQuery()

            Catch ex As Exception

                _ErrorMessage = ex.Message.ToString() & " Error "

            End Try

        End Using

    End Sub

#End Region

End Class
