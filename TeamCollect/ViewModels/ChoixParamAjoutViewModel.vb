Imports System.ComponentModel.DataAnnotations

Public Class ChoixParamAjoutViewModel

    Public Property Id As Long
    '<Required(ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="champ_Manquant")>

    Public Property UserId As String
    Public Overridable Property User As ApplicationUser

    '<Required(ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="champ_Manquant")>
    Public Property Libelle As String

    Public Property DateCreation As Nullable(Of Date) = Now
    Public Property Etat As Nullable(Of Integer) = 1

    <Required(ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="champ_Manquant")>
    <Display(Name:="Mois", ResourceType:=GetType(Resource))>
    Public Property Mois As Integer

    <Required(ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="champ_Manquant")>
    <Display(Name:="Annee", ResourceType:=GetType(Resource))>
    Public Property Annee As Integer

    Public Property ListeMois As IEnumerable(Of SelectListItem)
    Public Property ListeAnnee As IEnumerable(Of SelectListItem)


    Public Sub New()
    End Sub

    Public Sub New(entity As HistoriqueCalculAjout)
        With Me
            .Id = entity.Id
            .Libelle = entity.Libelle
            .Mois = entity.Mois
            .Annee = entity.Annee
            .Etat = entity.Etat
            .UserId = entity.UserId
            .DateCreation = entity.DateCreation
        End With
    End Sub

    Public Function getEntity() As HistoriqueCalculAjout
        Dim entity As New HistoriqueCalculAjout

        With entity
            .Id = Me.Id
            .Libelle = Me.Libelle
            .Mois = Me.Mois
            .Annee = Me.Annee
            .Etat = Me.Etat
            .UserId = Me.UserId
            .DateCreation = Me.DateCreation
        End With

        Return entity
    End Function
End Class
