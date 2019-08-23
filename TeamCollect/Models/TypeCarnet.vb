
Imports System
Imports System.Collections.Generic

Partial Public Class TypeCarnet

    Public Property Id As Long
    Public Property Libelle As String
    Public Property Prix As Double? = 0
    Public Property Etat As Boolean = False
    Public Property DateCreation As DateTime = Now

End Class



'Public Property PersonneId As Long
'Public Property UserId As String
'Public Overridable Property User As ApplicationDbContext
'Public Overridable Property Personne As Personne