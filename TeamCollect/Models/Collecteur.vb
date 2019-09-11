
Imports System
Imports System.Collections.Generic

Partial Public Class Collecteur
    Inherits Personne

    Public Property Pourcentage As Decimal?
    Public Property AdrMac As String

    Public Property CategorieRemunerationId As Long?
    Public Overridable Property CategorieRemuneration As CategorieRemuneration

    Public Overridable Property HistoriqueMouvement As ICollection(Of HistoriqueMouvement) = New HashSet(Of HistoriqueMouvement)
    Public Overridable Property JournalCaisse As ICollection(Of JournalCaisse) = New HashSet(Of JournalCaisse)
    Public Overridable Property PorteFeuille As ICollection(Of PorteFeuille) = New HashSet(Of PorteFeuille)
    Public Overridable Property CollecteurCategorie As ICollection(Of HistoriqueCollecteurCategorie) = New HashSet(Of HistoriqueCollecteurCategorie)

End Class


'Public Property PersonneId As Long
'Public Overridable Property Personne As Personne