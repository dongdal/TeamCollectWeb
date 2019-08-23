
Imports System
Imports System.Collections.Generic

Partial Public Class Personne
    Public Property Id As Long
    Public Property AgenceId As Nullable(Of Long)
    Public Property CodeSecret As String
    Public Property Nom As String
    Public Property Prenom As String
    Public Property Sexe As String
    Public Property CNI As String
    Public Property Telephone As String
    Public Property Telephone2 As String
    Public Property Adresse As String
    Public Property Quartier As String

    Public Overridable Property Agence As Agence

    Public Property Etat As Boolean = False
    Public Property DateCreation As DateTime = Now

    Public Property UserId As String

    ''Modification du 28-01-2019
    Public Property SecteurActiviteId As Long?
    Public Overridable Property SecteurActivite As SecteurActivite
    'Public Overridable Property ApplicationUser As ICollection(Of ApplicationUser) = New HashSet(Of ApplicationUser)

End Class
