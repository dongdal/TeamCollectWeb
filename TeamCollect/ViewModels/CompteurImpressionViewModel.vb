Imports System.ComponentModel.DataAnnotations
Imports TeamCollect.My.Resources

Public Class CompteurImpressionViewModel
    Public Property Id As Long

    <Display(Name:="HistoriqueMouvementId", ResourceType:=GetType(Resource))>
    Public Property HistoriqueMouvementId As Long
    Public Property HistoriqueMouvement As HistoriqueMouvement

    <Display(Name:="CollectriceId", ResourceType:=GetType(Resource))>
    Public Property CollectriceId As String 'Utilisateur
    Public Property Collectrice As ApplicationUser

    <Display(Name:="NombreImpression", ResourceType:=GetType(Resource))>
    Public Property NombreImpression As Long = 0

    <Display(Name:="DatePremiereImpression", ResourceType:=GetType(Resource))>
    Public Property DatePremiereImpression As DateTime = Now

    Public Sub New(entity As CompteurImpression)
        With Me
            .Id = entity.Id
            .HistoriqueMouvementId = entity.HistoriqueMouvementId
            .HistoriqueMouvement = entity.HistoriqueMouvement
            .CollectriceId = entity.CollectriceId
            .Collectrice = entity.Collectrice
            .NombreImpression = entity.NombreImpression
            .DatePremiereImpression = entity.DatePremiereImpression
        End With
    End Sub

    Public Function getEntity() As CompteurImpression
        Dim entity As New CompteurImpression

        With entity
            .Id = Me.Id
            .HistoriqueMouvementId = Me.HistoriqueMouvementId
            .HistoriqueMouvement = Me.HistoriqueMouvement
            .CollectriceId = Me.CollectriceId
            .Collectrice = Me.Collectrice
            .NombreImpression = Me.NombreImpression
            .DatePremiereImpression = Me.DatePremiereImpression
        End With

        Return entity
    End Function


End Class
