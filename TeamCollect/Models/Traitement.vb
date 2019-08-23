
Imports System
Imports System.Collections.Generic

Partial Public Class Traitement
    Public Property Id As Long
    Public Property Fichier As String
    Public Property DateTraitement As Date?

    Public Property Etat As Boolean = False
    Public Property DateCreation As DateTime = Now

    Public Property UserId As String
    Public Overridable Property User As ApplicationUser

    Public Overridable Property HistoriqueMouvement As ICollection(Of HistoriqueMouvement) = New HashSet(Of HistoriqueMouvement)

End Class
