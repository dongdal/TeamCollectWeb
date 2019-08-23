
Imports System
Imports System.Collections.Generic

Partial Public Class Agence
    Public Property Id As Long
    Public Property SocieteId As Long
    Public Property Libelle As String
    Public Property BP As String
    Public Property Telephone As String
    Public Property Email As String
    Public Property Adresse As String

    Public Property Etat As Boolean = False
    Public Property DateCreation As DateTime = Now

    Public Property UserId As String
    Public Overridable Property User As ApplicationUser

    Public Overridable Property Societe As Societe

    Public Overridable Property Personne As ICollection(Of Personne) = New HashSet(Of Personne)
End Class

