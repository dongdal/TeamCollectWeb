Imports System.ComponentModel.DataAnnotations
Imports TeamCollect.My.Resources

Public Class PersonneViewModel

    Public Property Id As Long

    Public Property CodeSecret As String

    <Required(ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="champ_Manquant")> _
    Public Property Nom As String

    Public Property Prenom As String

    <Required(ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="champ_Manquant")> _
    Public Property Sexe As String

    <Required(ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="champ_Manquant")> _
    Public Property CNI As String

    Public Property AgenceId As Nullable(Of Long)

    Public Overridable Property IDsAgence As List(Of SelectListItem)
    Public Overridable Property Agence As Agence

    Public Property Telephone As String
    Public Property Adresse As String
    Public Property Quartier As String
    Public Property DateCreation As Nullable(Of Date)
    Public Property Etat As Nullable(Of Integer)


    Public Property SecteurActiviteId As Long?
    Public Overridable Property LesSecteursActivite As List(Of SelectListItem)
    Public Overridable Property SecteurActivite As SecteurActivite




    Public Sub New()
    End Sub

    Public Sub New(entity As Personne)
        With Me
            .Id = entity.Id
            .AgenceId = entity.AgenceId
            .CodeSecret = entity.CodeSecret
            .Nom = entity.Nom
            .Prenom = entity.Prenom
            .Sexe = entity.Sexe
            .CNI = entity.CNI
            .Telephone = entity.Telephone
            .Adresse = entity.Adresse
            .Quartier = entity.Quartier
            .DateCreation = entity.DateCreation
            .Etat = entity.Etat
            .SecteurActiviteId = entity.SecteurActiviteId
        End With
    End Sub

    Public Function getEntity() As Personne
        Dim entity As New Personne

        With entity
            .Id = Me.Id
            .AgenceId = Me.AgenceId
            .CodeSecret = Me.CodeSecret
            .SecteurActiviteId = Me.SecteurActiviteId
            .Nom = Me.Nom
            .Prenom = Me.Prenom
            .Sexe = Me.Sexe
            .CNI = Me.CNI
            .Telephone = Me.Telephone
            .Adresse = Me.Adresse
            .Quartier = Me.Quartier
            .DateCreation = Me.DateCreation
            .Etat = Me.Etat
        End With

        Return entity
    End Function


End Class
