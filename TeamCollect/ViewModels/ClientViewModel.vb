Imports System.ComponentModel.DataAnnotations
Imports TeamCollect.My.Resources

Public Class ClientViewModel

    Public Property Id As Long

    Public Property CodeSecret As String

    <Required(ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="champ_Manquant")> _
    Public Property Nom As String

    Public Property Prenom As String

    <Required(ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="champ_Manquant")> _
    Public Property Sexe As String

    <Required(ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="champ_Manquant")> _
    Public Property CNI As String

    <Required(ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="champ_Manquant")> _
    Public Property AgenceId As Long
    Public Overridable Property IDsAgence As List(Of SelectListItem)
    Public Overridable Property Agence As Agence


    <Required(ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="champ_Manquant")>
    Public Property PorteFeuilleId As Long?
    Public Overridable Property LesPorteFeuilles As List(Of SelectListItem)
    Public Overridable Property PorteFeuilles As PorteFeuille

    Public Property SecteurActiviteId As Long?
    Public Overridable Property LesSecteursActivite As List(Of SelectListItem)
    Public Overridable Property SecteurActivite As SecteurActivite

    <Display(Name:="MessageAlerte", ResourceType:=GetType(Resource))>
    Public Property MessageAlerte As String

    Public Property Solde As Decimal? = 0
    Public Property SoldeDisponible As Decimal? = 0
    Public Property Pourcentage As Decimal? = 0
    Public Property NumeroCompte As String
    Public Property Telephone As String
    Public Property Telephone2 As String
    Public Property Adresse As String
    Public Property Quartier As String
    Public Property DateCreation As Date? = Now
    Public Property Etat As Integer? = True
    Public Property UserId As String

    Public Sub New()
    End Sub

    Public Sub New(entity As Client)
        With Me
            .Id = entity.Id
            .CodeSecret = entity.CodeSecret
            .SecteurActiviteId = entity.SecteurActiviteId
            .PorteFeuilleId = entity.PorteFeuilleId
            .Pourcentage = entity.Pourcentage
            .AgenceId = entity.AgenceId
            .Solde = entity.Solde
            .SoldeDisponible = entity.SoldeDisponible
            .Nom = entity.Nom
            .Prenom = entity.Prenom
            .Sexe = entity.Sexe
            .CNI = entity.CNI
            .Telephone = entity.Telephone
            .Telephone2 = entity.Telephone2
            .Adresse = entity.Adresse
            .Quartier = entity.Quartier
            .MessageAlerte = entity.MessageAlerte
            '.AgenceId = entity.AgenceId
            .NumeroCompte = entity.NumeroCompte
            .DateCreation = entity.DateCreation
            .Etat = entity.Etat
            .UserId = entity.UserId
        End With
    End Sub

    Public Function getEntity() As Client
        Dim entity As New Client

        With entity
            .Id = Me.Id
            .CodeSecret = Me.CodeSecret
            .PorteFeuilleId = Me.PorteFeuilleId
            .SecteurActiviteId = Me.SecteurActiviteId
            .Solde = Me.Solde
            .SoldeDisponible = Me.SoldeDisponible
            .Pourcentage = Me.Pourcentage
            .Nom = Me.Nom
            .Prenom = Me.Prenom
            .Sexe = Me.Sexe
            .CNI = Me.CNI
            .Telephone = Me.Telephone
            .Telephone2 = Me.Telephone2
            .Adresse = Me.Adresse
            .Quartier = Me.Quartier
            .AgenceId = Me.AgenceId
            .NumeroCompte = Me.NumeroCompte
            .DateCreation = Me.DateCreation
            .Etat = Me.Etat
            .MessageAlerte = Me.MessageAlerte
            .UserId = Me.UserId
        End With

        Return entity
    End Function

End Class
