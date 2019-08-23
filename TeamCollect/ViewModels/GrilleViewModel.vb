Imports System.ComponentModel.DataAnnotations
Imports TeamCollect.My.Resources

Public Class GrilleViewModel

    Public Property Id As Long

    Public Property SocieteId As Long
    Public Overridable Property Societe As Societe

    Public Property UserId As String
    Public Overridable Property User As ApplicationUser

    <Required(ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="champ_Manquant")> _
    Public Property Libelle As String

    Public Property DateCreation As Nullable(Of Date)
    Public Property Etat As Nullable(Of Integer)





    Public Sub New()
    End Sub

    Public Sub New(entity As Grille)
        With Me
            .Id = entity.Id
            .UserId = entity.UserId
            .SocieteId = entity.SocieteId
            .Libelle = entity.Libelle
            .DateCreation = entity.DateCreation
            .Etat = entity.Etat
        End With
    End Sub

    Public Function getEntity() As Grille
        Dim entity As New Grille

        With entity
            .Id = Me.Id
            .UserId = Me.UserId
            .SocieteId = Me.SocieteId
            .Libelle = Me.Libelle
            .DateCreation = Me.DateCreation
            .Etat = Me.Etat
        End With

        Return entity
    End Function


End Class
