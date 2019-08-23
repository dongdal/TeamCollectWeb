Imports System.ComponentModel.DataAnnotations
Imports TeamCollect.My.Resources

Public Class AnnulationViewModel

    Public Property Id As Long



    <Required(ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="champ_Manquant")>
    Public Property Motif As String

    Public Property DateDebut As String
    Public Property DateFin As String
    Public Property CollecteurId As Long?


    Public Sub New()
    End Sub

    'Public Sub New(entity As Grille)
    '    With Me
    '        .Id = entity.Id
    '        .UserId = entity.UserId
    '        .SocieteId = entity.SocieteId
    '        .Libelle = entity.Libelle
    '        .DateCreation = entity.DateCreation
    '        .Etat = entity.Etat
    '    End With
    'End Sub

    'Public Function getEntity() As Grille
    '    Dim entity As New Grille

    '    With entity
    '        .Id = Me.Id
    '        .UserId = Me.UserId
    '        .SocieteId = Me.SocieteId
    '        .Libelle = Me.Libelle
    '        .DateCreation = Me.DateCreation
    '        .Etat = Me.Etat
    '    End With

    '    Return entity
    'End Function


End Class
