Imports System.ComponentModel.DataAnnotations
Imports TeamCollect.My.Resources

Public Class CollecteurViewModel

    Public Property Id As Long

    Public Property CodeSecret As String

    <Required(ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="champ_Manquant")> _
    Public Property Nom As String

    Public Property Prenom As String

    <Required(ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="champ_Manquant")> _
    Public Property Sexe As String

    <Required(ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="champ_Manquant")> _
    Public Property AdrMac As String

    <RegularExpression("^(\d+(((\,))\d+)?)$", ErrorMessageResourceName:="decimalType_error", ErrorMessageResourceType:=GetType(Resource))>
    Public Property Pourcentage As String

    <Required(ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="champ_Manquant")> _
    Public Property CNI As String

    <Required(ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="champ_Manquant")> _
    Public Property AgenceId As Long
    Public Overridable Property IDsAgence As List(Of SelectListItem)
    Public Overridable Property Agence As Agence

    Public Property Telephone As String
    Public Property Adresse As String
    Public Property Quartier As String
    Public Property DateCreation As Nullable(Of Date)
    Public Property Etat As Nullable(Of Integer)


    <Required(ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="champ_Manquant")>
    <Display(Name:="CategorieRemuneration", ResourceType:=GetType(Resource))>
    Public Property CategorieRemunerationId As Long
    Public Overridable Property LesCategorieRemuneration As List(Of SelectListItem)
    Public Overridable Property CategorieRemuneration As CategorieRemuneration


    Public Sub New()
    End Sub

    Public Sub New(entity As Collecteur)
        With Me
            .Id = entity.Id
            '.AgenceId = entity.AgenceId
            .CodeSecret = entity.CodeSecret
            .AdrMac = entity.AdrMac
            .Nom = entity.Nom
            .Prenom = entity.Prenom
            .Sexe = entity.Sexe
            .CNI = entity.CNI
            .Telephone = entity.Telephone
            .Adresse = entity.Adresse
            .Quartier = entity.Quartier
            .Pourcentage = entity.Pourcentage
            .DateCreation = entity.DateCreation
            .CategorieRemuneration = entity.CategorieRemuneration
            .CategorieRemunerationId = entity.CategorieRemunerationId
            .Etat = entity.Etat
        End With
    End Sub

    Public Function GetEntity() As Collecteur
        Dim entity As New Collecteur

        With entity
            .Id = Me.Id
            .CodeSecret = Me.CodeSecret
            .AgenceId = Me.AgenceId
            .AdrMac = Me.AdrMac
            .Nom = Me.Nom
            .Prenom = Me.Prenom
            .Sexe = Me.Sexe
            .CNI = Me.CNI
            .Telephone = Me.Telephone
            .Adresse = Me.Adresse
            .Quartier = Me.Quartier
            .Pourcentage = Me.Pourcentage
            .DateCreation = Me.DateCreation
            .CategorieRemunerationId = Me.CategorieRemunerationId
            .Etat = Me.Etat
        End With

        Return entity
    End Function


End Class
