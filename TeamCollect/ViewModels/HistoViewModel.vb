'Imports System.ComponentModel.DataAnnotations
'Imports TeamCollect.My.Resources

'Public Class HistoViewModel

'    Public Property Id As Long
'    Public Property JournalCaisseId As Long
'    Public Property CollecteurId As Long

'    <Required(ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="champ_Manquant")> _
'    Public Property ClientId As Long
'    Public Property TraitementId As Long?
'    Public Property Latitude As String
'    Public Property Longitude As String

'    Public Property Montant As Decimal?
'    Public Property DateOperation As Date?
'    Public Property Pourcentage As Decimal?
'    Public Property MontantRetenu As Decimal?
'    Public Property PartBANK As Decimal?
'    Public Property PartCLIENT As Decimal?
'    Public Property EstTraiter As Integer = 0
'    Public Property DateTraitement As Date?

'    Public Property Etat As Boolean = False
'    Public Property DateCreation As DateTime = Now

'    Public Property UserId As String
'    Public Overridable Property User As ApplicationUser






'    Public Property Id As Long

'    Public Property CodeSecret As String

'    <Required(ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="champ_Manquant")> _
'    Public Property Nom As String

'    Public Property Prenom As String

'    <Required(ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="champ_Manquant")> _
'    Public Property Sexe As String

'    <Required(ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="champ_Manquant")> _
'    Public Property CNI As String

'    <Required(ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="champ_Manquant")> _
'    Public Property AgenceId As Long
'    Public Overridable Property IDsAgence As List(Of SelectListItem)
'    Public Overridable Property Agence As Agence

'    Public Property Solde As Nullable(Of Decimal)
'    Public Property NumeroCompte As String
'    Public Property Telephone As String
'    Public Property Adresse As String
'    Public Property Quartier As String
'    Public Property DateCreation As Nullable(Of Date)
'    Public Property Etat As Nullable(Of Integer)





'    Public Sub New()
'    End Sub

'    Public Sub New(entity As Client)
'        With Me
'            .Id = entity.Id
'            .CodeSecret = entity.CodeSecret
'            .Solde = entity.Solde
'            .Nom = entity.Nom
'            .Prenom = entity.Prenom
'            .Sexe = entity.Sexe
'            .CNI = entity.CNI
'            .Telephone = entity.Telephone
'            .Adresse = entity.Adresse
'            .Quartier = entity.Quartier
'            '.AgenceId = entity.AgenceId
'            .NumeroCompte = entity.NumeroCompte
'            .DateCreation = entity.DateCreation
'            .Etat = entity.Etat
'        End With
'    End Sub

'    Public Function getEntity() As Client
'        Dim entity As New Client

'        With entity
'            .Id = Me.Id
'            .CodeSecret = Me.CodeSecret
'            .Solde = Me.Solde
'            .Nom = Me.Nom
'            .Prenom = Me.Prenom
'            .Sexe = Me.Sexe
'            .CNI = Me.CNI
'            .Telephone = Me.Telephone
'            .Adresse = Me.Adresse
'            .Quartier = Me.Quartier
'            .AgenceId = Me.AgenceId
'            .NumeroCompte = Me.NumeroCompte
'            .DateCreation = Me.DateCreation
'            .Etat = Me.Etat
'        End With

'        Return entity
'    End Function


'End Class
