
Imports System
Imports System.Collections.Generic

Partial Public Class HistoriqueMouvement
    Public Property Id As Long
    Public Property JournalCaisseId As Long
    Public Property CollecteurId As Long
    Public Property ClientId As Long
    Public Property TraitementId As Long?
    Public Property Latitude As String
    Public Property Longitude As String
    Public Property Montant As Decimal?
    Public Property DateOperation As Date?
    Public Property Pourcentage As Decimal?
    Public Property MontantRetenu As Decimal?
    Public Property PartBANK As Decimal?
    Public Property PartCLIENT As Decimal?
    Public Property EstTraiter As Integer = 0
    Public Property DateTraitement As Date?

    Public Property Etat As Boolean = False
    Public Property Extourner As Boolean?
    Public Property DateCreation As DateTime = Now

    Public Property UserId As String
    Public Overridable Property User As ApplicationUser

    Public Overridable Property Client As Client
    Public Overridable Property Collecteur As Collecteur
    Public Overridable Property Traitement As Traitement
    Public Overridable Property JournalCaisse As JournalCaisse


    Public Property LibelleOperation As String
    Public Overridable Property CompteurImpression As ICollection(Of CompteurImpression) = New HashSet(Of CompteurImpression)

    'Public Property CoordoneeGeographiqueId As Long?
    'Public Overridable Property CoordoneeGeographique As CoordonneeGeographique

    Public Overridable Property Retrait As ICollection(Of Retrait) = New HashSet(Of Retrait)
    Public Overridable Property CarnetClient As ICollection(Of CarnetClient) = New HashSet(Of CarnetClient)

End Class
