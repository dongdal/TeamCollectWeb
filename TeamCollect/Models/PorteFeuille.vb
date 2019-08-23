
Imports System
Imports System.Collections.Generic

Partial Public Class PorteFeuille
    Public Property CollecteurId As Long
    Public Property Id As Long
    Public Property Libelle As String
    Public Property Etat As Boolean = False
    Public Property DateCreation As DateTime = Now

    Public Property UserId As String
    Public Overridable Property User As ApplicationUser

    Public Overridable Property Collecteur As Collecteur

    Public Overridable Property Client As ICollection(Of Client) = New HashSet(Of Client)

End Class

