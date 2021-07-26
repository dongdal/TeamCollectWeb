Imports System.ComponentModel.DataAnnotations

Public Class CarnetClientViewModel

    Public Property Id As Long

    <Required(ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="champ_Manquant")>
    Public Property ClientId As Long
    Public Overridable Property IDsClient As List(Of SelectListItem)
    Public Overridable Property client As Client

    '<Required(ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="champ_Manquant")>
    Public Property HistoriqueMouvementId As Long?
    Public Overridable Property LesHistoriqueMouvements As List(Of SelectListItem)
    Public Overridable Property HistoriqueMouvement As HistoriqueMouvement

    <Required(ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="champ_Manquant")>
    Public Property TypeCarnetId As Long
    Public Overridable Property IDsTypeCarnet As List(Of SelectListItem)
    Public Overridable Property typeCarnet As TypeCarnet

    Public Property Etat As Boolean = True
    Public Property DateAffectation As DateTime? = Now


    Public Sub New()
    End Sub

    Public Sub New(entity As CarnetClient)
        With Me
            .Id = entity.Id
            .ClientId = entity.ClientId
            .TypeCarnetId = entity.TypeCarnetId
            .HistoriqueMouvementId = entity.HistoriqueMouvementId
            .Etat = entity.Etat
            .DateAffectation = entity.DateAffectation
        End With
    End Sub

    Public Function getEntity() As CarnetClient
        Dim entity As New CarnetClient

        With entity
            .Id = Me.Id
            .ClientId = Me.ClientId
            .TypeCarnetId = Me.TypeCarnetId
            .HistoriqueMouvementId = Me.HistoriqueMouvementId
            .Etat = Me.Etat
            .DateAffectation = Me.DateAffectation
        End With

        Return entity
    End Function
End Class
