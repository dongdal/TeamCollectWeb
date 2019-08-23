Imports System.ComponentModel.DataAnnotations
Imports TeamCollect.My.Resources

Public Class PorteFeuilleViewModel

    Public Property Id As Long

    <Required(ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="champ_Manquant")> _
    Public Property CollecteurId As Long
    Public Overridable Property IDsCollecteur As List(Of SelectListItem)
    Public Overridable Property Collecteur As Collecteur


    <Required(ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="champ_Manquant")> _
    Public Property Libelle As String

    Public Property UserId As String
    Public Overridable Property User As ApplicationUser

    Public Property DateCreation As Date = Now.Date
    Public Property Etat As Boolean = False





    Public Sub New()
    End Sub

    Public Sub New(entity As PorteFeuille)
        With Me
            .Id = entity.Id
            .CollecteurId = entity.CollecteurId
            .Libelle = entity.Libelle
            .UserId = entity.UserId
            .DateCreation = entity.DateCreation
            .Etat = entity.Etat
        End With
    End Sub

    Public Function getEntity() As PorteFeuille
        Dim entity As New PorteFeuille

        With entity
            .Id = Me.Id
            .CollecteurId = Me.CollecteurId
            .Libelle = Me.Libelle
            .UserId = Me.UserId
            .DateCreation = Me.DateCreation
            .Etat = Me.Etat
        End With

        Return entity
    End Function


End Class
