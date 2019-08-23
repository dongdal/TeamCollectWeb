Imports System.ComponentModel.DataAnnotations
Imports TeamCollect.My.Resources

Public Class AgenceViewModel

    Public Property Id As Long

    Public Property SocieteId As Long
    Public Overridable Property Societe As Societe

    <Required(ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="champ_Manquant")> _
    Public Property Libelle As String

    Public Property BP As String

    <Required(ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="champ_Manquant")> _
    Public Property Email As String

    Public Property Telephone As String
    Public Property Adresse As String
    Public Property DateCreation As Nullable(Of Date)
    Public Property Etat As Nullable(Of Integer)





    Public Sub New()
    End Sub

    Public Sub New(entity As Agence)
        With Me
            .Id = entity.Id
            .SocieteId = entity.SocieteId
            .Libelle = entity.Libelle
            .BP = entity.BP
            .Telephone = entity.Telephone
            .Email = entity.Email
            .Adresse = entity.Adresse
            .DateCreation = entity.DateCreation
            .Etat = entity.Etat
        End With
    End Sub

    Public Function getEntity() As Agence
        Dim entity As New Agence

        With entity
            .Id = Me.Id
            .SocieteId = Me.SocieteId
            .Libelle = Me.Libelle
            .BP = Me.BP
            .Telephone = Me.Telephone
            .Email = Me.Email
            .Adresse = Me.Adresse
            .DateCreation = Me.DateCreation
            .Etat = Me.Etat
        End With

        Return entity
    End Function


End Class
