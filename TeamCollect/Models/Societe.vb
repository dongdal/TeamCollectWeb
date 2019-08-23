
Imports System
Imports System.Collections.Generic

Partial Public Class Societe
    Public Property Id As Long
    Public Property Libelle As String
    Public Property BP As String
    Public Property Telephone As String
    Public Property Email As String
    Public Property Adresse As String
    Public Property PlafondDeCollecte As Decimal?
    Public Property MinCollecte As Decimal? = 0
    Public Property MAxCollecte As Decimal? = 0

    Public Property Etat As Boolean = False
    Public Property DateCreation As DateTime = Now

    Public Property UserId As String


    Public Overridable Property Agences As ICollection(Of Agence) = New HashSet(Of Agence)
    Public Overridable Property Grille As ICollection(Of Grille) = New HashSet(Of Grille)


End Class

