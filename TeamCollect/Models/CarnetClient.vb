Imports System
Imports System.Collections.Generic

Partial Public Class CarnetClient
    Public Property Id As Long
    Public Property ClientId As Long
    Public Property TypeCarnetId As Long
    Public Property Etat As Boolean = False
    Public Property DateAffectation As DateTime? = Now

    Public Property UserId As String
    Public Overridable Property User As ApplicationUser

    Public Overridable Property Client As Client
    Public Overridable Property TypeCarnet As TypeCarnet
End Class



'Public Property PersonneId As Long
'Public Property UserId As String
'Public Overridable Property User As ApplicationDbContext
'Public Overridable Property Personne As Personne