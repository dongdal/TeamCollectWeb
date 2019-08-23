Imports System.ComponentModel.DataAnnotations
Imports TeamCollect.My.Resources

Public Class StatViewModel

    Public Property CollecteurId As Long
    Public Overridable Property IDsCollecteur As List(Of SelectListItem)
    Public Overridable Property Collecteur As Collecteur
    Public Overridable Property Operation As String

    Public Property ClientId As Long
    Public Overridable Property IDsClient As List(Of SelectListItem)
    Public Overridable Property Client As Client

    Public Property JournalCaisseId As Long
    Public Overridable Property IDsJournalCaisse As List(Of SelectListItem)
    Public Overridable Property JournalCaisse As JournalCaisse


    <Required(ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="champ_Manquant")>
    <Display(Name:="Mois", ResourceType:=GetType(Resource))>
    Public Property Mois As Integer

    <Required(ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="champ_Manquant")>
    <Display(Name:="Annee", ResourceType:=GetType(Resource))>
    Public Property Annee As Integer

    Public Property ListeMois As IEnumerable(Of SelectListItem)
    Public Property ListeAnnee As IEnumerable(Of SelectListItem)
    Public Property ListeOperations As IEnumerable(Of SelectListItem)


    Public Property myjson As String
    
    Public Sub New()
    End Sub


End Class
