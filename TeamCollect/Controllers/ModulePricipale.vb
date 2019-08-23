Imports System.Security.Cryptography
Imports System.Net
Imports System.Net.Http.Headers
Imports System.Net.Http
Imports System.IO

Module ModulePrincipale

    Public Function GetConnectionString() As String

        Dim ServeurBD As String = ConfigurationManager.AppSettings("ServeurBD")
        Dim NameBD As String = ConfigurationManager.AppSettings("NameBD")
        Dim UserBD As String = ConfigurationManager.AppSettings("UserBD")
        Dim PasswordBD As String = ConfigurationManager.AppSettings("PasswordBD")
        Dim IntegretedSecurity As String = ConfigurationManager.AppSettings("IntegretedSecurity")
        'Dim connectionString = "Data Source=.;Initial Catalog=dbVendeur;Integrated Security=True"
        Dim connectionString = "Data Source=" + ServeurBD + ";Initial Catalog=" + NameBD + ";Integrated Security=True"

        If IntegretedSecurity = False Then
            connectionString = "Data Source=" + ServeurBD + ";initial catalog=" + NameBD + ";User ID=" + UserBD + ";Password=" + PasswordBD
            'Integrated Security=SSPI;" providerName="System.Data.SqlClient"
            'providerName=System.Data.SqlClient"
        End If

        Return connectionString
    End Function

End Module
