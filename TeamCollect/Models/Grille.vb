
Imports System
Imports System.Collections.Generic

Partial Public Class Grille
    Public Property SocieteId As Long
    Public Property Id As Long
    Public Property Libelle As String
    Public Property Etat As Boolean = False
    Public Property DateCreation As DateTime = Now
    Public Overridable Property Societe As Societe

    Public Property UserId As String
    Public Overridable Property User As ApplicationUser

    Public Overridable Property InfoFrais As ICollection(Of InfoFrais) = New HashSet(Of InfoFrais)

End Class

