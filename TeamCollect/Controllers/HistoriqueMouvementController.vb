Imports System.Data.Entity
Imports System.Net
Imports PagedList
Imports Microsoft.AspNet.Identity
Imports System.Data.Entity.Validation
Imports System.IO
Imports ClosedXML.Excel
Imports System.Data.SqlClient
Imports System.Data.OleDb
Imports System.Threading.Tasks

Namespace TeamCollect
    Public Class HistoriqueMouvementController
        Inherits BaseController

        Private db As New ApplicationDbContext
        Private _errorMsg As String

        Public Property ErrorMsg As String
            Get
                Return _errorMsg
            End Get
            Set(value As String)
                _errorMsg = value
            End Set
        End Property

        Enum OperationType
            AnnulationCollecte
            AnnulationVente
            AnnulationRetrait
        End Enum

        ' GET: /HistoriqueMouvement/
        'Function Index() As ActionResult
        '    Dim historiquemouvements = db.HistoriqueMouvements.Include(Function(h) h.Client).Include(Function(h) h.Collecteur).Include(Function(h) h.JournalCaisse)
        '    Return View(historiquemouvements.ToList())
        'End Function

        Private Function GetCurrentUser() As ApplicationUser
            Dim id = User.Identity.GetUserId
            Dim aspuser = db.Users.Find(id)
            Return aspuser
        End Function


        Public Sub LoadComboStat(VM As StatViewModel)

            Dim ListeAnnee As New List(Of SelectListItem)
            Dim Listemois As New List(Of SelectListItem)

            For i As Integer = 1 To 12
                Dim li As New SelectListItem With {
                    .Value = i,
                    .Text = i
                }
                Listemois.Add(li)
            Next


            For i As Integer = 2019 To Now.Year
                Dim li As New SelectListItem With {
                    .Value = i,
                    .Text = i
                }
                ListeAnnee.Add(li)
            Next

            VM.ListeMois = Listemois
            VM.ListeAnnee = ListeAnnee
        End Sub

        <LocalizedAuthorize(Roles:="CHEFCOLLECTEUR")>
        Function FicheCollecteJournaliere() As ActionResult

            Dim entityVM As New StatViewModel
            '---------------les collecteurs-----------------
            Dim userAgenceId = 0
            If Not User.IsInRole("ADMINISTRATEUR") And Not User.IsInRole("MANAGER") Then
                userAgenceId = GetCurrentUser.Personne.AgenceId.Value
            End If
            'Dim listcollecteur = db.Personnes.OfType(Of Collecteur).Where(Function(i) i.AgenceId = userAgenceId).ToList

            Dim LesJournauxCaisse = db.JournalCaisses.OrderByDescending(Function(e) e.DateCreation).ToList

            Dim LesJournauxCaisse2 As New List(Of SelectListItem)
            For Each item In LesJournauxCaisse
                LesJournauxCaisse2.Add(New SelectListItem With {.Value = item.Id, .Text = "Caisse de " & item.Collecteur.Nom & " du " & item.DateOuverture})
            Next
            entityVM.IDsJournalCaisse = LesJournauxCaisse2
            '---------------------------
            ViewBag.UserAgenceId = userAgenceId
            Return View(entityVM)

        End Function

        <LocalizedAuthorize(Roles:="SA,ADMINISTRATEUR,CHEFCOLLECTEUR,MANAGER")>
        Function FicheCollecteJournaliereParPeriode() As ActionResult

            Dim entityVM As New StatViewModel
            '---------------les collecteurs-----------------
            Dim userAgenceId = 0
            If Not User.IsInRole("ADMINISTRATEUR") And Not User.IsInRole("MANAGER") Then
                userAgenceId = GetCurrentUser.Personne.AgenceId.Value
            End If
            'Dim listcollecteur = db.Personnes.OfType(Of Collecteur).Where(Function(i) i.AgenceId = userAgenceId).ToList
            Dim CollecteurSystem = ConfigurationManager.AppSettings("CollecteurSystemeId") 'Il s'agit de l'identitfiant du collecteur système

            Dim Collecteurs = (From collecteur In db.Collecteurs Where collecteur.Id <> CollecteurSystem Select collecteur).ToList

            'Dim Collecteurs = (From collecteur In db.Collecteurs Select collecteur).ToList

            If Not User.IsInRole("ADMINISTRATEUR") And Not User.IsInRole("MANAGER") Then
                Collecteurs = Collecteurs.Where(Function(e) e.AgenceId = userAgenceId).ToList
            End If

            Dim LesCollecteurs As New List(Of SelectListItem)
            For Each item In Collecteurs
                If String.IsNullOrEmpty(item.Prenom) Then
                    LesCollecteurs.Add(New SelectListItem With {.Value = item.Id, .Text = item.Nom})
                Else
                    LesCollecteurs.Add(New SelectListItem With {.Value = item.Id, .Text = item.Nom & " " & item.Prenom})
                End If
            Next
            entityVM.IDsCollecteur = LesCollecteurs
            '---------------------------

            ViewBag.dateDebut = Now.Date.ToString(AppSession.DateFormat)
            ViewBag.dateFin = Now.Date.ToString(AppSession.DateFormat)
            ViewBag.UserAgenceId = userAgenceId
            Return View(entityVM)

        End Function

        <LocalizedAuthorize(Roles:="SA,ADMINISTRATEUR,CHEFCOLLECTEUR,MANAGER")>
        Function FicheCommissionsParPorteFeuille() As ActionResult

            Dim entityVM As New StatViewModel
            '---------------les collecteurs-----------------
            LoadComboStat(entityVM)
            Dim userAgenceId = 0
            If Not User.IsInRole("ADMINISTRATEUR") And Not User.IsInRole("MANAGER") Then
                userAgenceId = GetCurrentUser.Personne.AgenceId.Value
            End If
            ViewBag.UserAgenceId = userAgenceId

            Return View(entityVM)

        End Function

        <LocalizedAuthorize(Roles:="SA,ADMINISTRATEUR,CHEFCOLLECTEUR,MANAGER")>
        Function FicheOperationsParPeriode() As ActionResult

            Dim entityVM As New StatViewModel
            '---------------les collecteurs-----------------

            Dim userAgenceId = 0
            If Not User.IsInRole("ADMINISTRATEUR") And Not User.IsInRole("MANAGER") Then
                userAgenceId = GetCurrentUser.Personne.AgenceId.Value
            End If
            'Dim listcollecteur = db.Personnes.OfType(Of Collecteur).Where(Function(i) i.AgenceId = userAgenceId).ToList
            Dim CollecteurSystem = ConfigurationManager.AppSettings("CollecteurSystemeId") 'Il s'agit de l'identitfiant du collecteur système

            Dim Collecteurs = (From collecteur In db.Collecteurs Where collecteur.Id <> CollecteurSystem Select collecteur).ToList

            'Dim Collecteurs = (From collecteur In db.Collecteurs Select collecteur).ToList

            If Not User.IsInRole("ADMINISTRATEUR") And Not User.IsInRole("MANAGER") Then
                Collecteurs = Collecteurs.Where(Function(e) e.AgenceId = userAgenceId).ToList
            End If

            Dim ListeOperations As New List(Of SelectListItem) From {
                New SelectListItem With {.Value = "RETRAIT", .Text = "RETRAITS"},
                New SelectListItem With {.Value = "VENTE CARNET", .Text = "VENTE DE CARNETS"}
            }
            entityVM.ListeOperations = ListeOperations
            '---------------------------

            Dim Agences = (From e In db.Agences Select e).ToList
            If (userAgenceId > 0) Then
                Agences = Agences.Where(Function(a) a.Id = userAgenceId).ToList()
            End If
            Dim AgenceList As New List(Of SelectListItem)
            For Each agence In Agences
                AgenceList.Add(New SelectListItem With {.Value = agence.Id, .Text = agence.Libelle})
            Next
            entityVM.ListeAgence = AgenceList

            ViewBag.dateDebut = Now.Date.ToString(AppSession.DateFormat)
            ViewBag.dateFin = Now.Date.ToString(AppSession.DateFormat)
            ViewBag.UserAgenceId = userAgenceId
            Return View(entityVM)

        End Function

        <LocalizedAuthorize(Roles:="SA,ADMINISTRATEUR,CHEFCOLLECTEUR,MANAGER")>
        Function HistoriqueCollectriceParPeriode() As ActionResult

            Dim entityVM As New StatViewModel
            '---------------les collecteurs-----------------
            Dim userAgenceId = 0
            If Not User.IsInRole("ADMINISTRATEUR") And Not User.IsInRole("MANAGER") Then
                userAgenceId = GetCurrentUser.Personne.AgenceId.Value
            End If
            'Dim listcollecteur = db.Personnes.OfType(Of Collecteur).Where(Function(i) i.AgenceId = userAgenceId).ToList
            Dim CollecteurSystem = ConfigurationManager.AppSettings("CollecteurSystemeId") 'Il s'agit de l'identitfiant du collecteur système

            Dim Collecteurs = (From collecteur In db.Collecteurs Where collecteur.Id <> CollecteurSystem Select collecteur).ToList

            'Dim Collecteurs = (From collecteur In db.Collecteurs Select collecteur).ToList

            If Not User.IsInRole("ADMINISTRATEUR") And Not User.IsInRole("MANAGER") Then
                Collecteurs = Collecteurs.Where(Function(e) e.AgenceId = userAgenceId).ToList
            End If
            '---------------------------

            Dim Agences = (From e In db.Agences Select e).ToList
            Dim AgenceList As New List(Of SelectListItem)
            For Each agence In Agences
                AgenceList.Add(New SelectListItem With {.Value = agence.Id, .Text = agence.Libelle})
            Next
            entityVM.ListeAgence = AgenceList

            Dim ListeCollectrice As New List(Of SelectListItem)
            For Each item In Collecteurs
                If (String.IsNullOrEmpty(item.Prenom)) Then
                    ListeCollectrice.Add(New SelectListItem With {.Value = item.Id, .Text = item.Nom})
                Else
                    ListeCollectrice.Add(New SelectListItem With {.Value = item.Id, .Text = item.Nom & " " & item.Prenom})
                End If
            Next
            entityVM.IDsCollecteur = ListeCollectrice

            ViewBag.dateDebut = Now.Date.ToString(AppSession.DateFormat)
            ViewBag.dateFin = Now.Date.ToString(AppSession.DateFormat)
            ViewBag.UserAgenceId = userAgenceId
            Return View(entityVM)

        End Function

        <LocalizedAuthorize(Roles:="SA,ADMINISTRATEUR,CHEFCOLLECTEUR,MANAGER")>
        Function FicheCommissionsParPorteFeuilleSimplifiee() As ActionResult

            Dim entityVM As New StatViewModel
            '---------------les collecteurs-----------------
            LoadComboStat(entityVM)
            Dim userAgenceId = 0
            If Not User.IsInRole("ADMINISTRATEUR") And Not User.IsInRole("MANAGER") Then
                userAgenceId = GetCurrentUser.Personne.AgenceId.Value
            End If
            ViewBag.UserAgenceId = userAgenceId

            Return View(entityVM)

        End Function

        <LocalizedAuthorize(Roles:="MANAGER")>
        Function CommissionCollectriceAvecGrilleRemunerationGlobal() As ActionResult

            Dim entityVM As New StatViewModel
            '---------------les collecteurs-----------------
            LoadComboStat(entityVM)

            ViewBag.dateDebut = Now.Date.ToString(AppSession.DateFormat)
            ViewBag.dateFin = Now.Date.ToString(AppSession.DateFormat)

            '----------------on recupère la liste des agences pour filtrer---------------
            Dim Agences = db.Agences.OfType(Of Agence)().ToList
            Dim LesAgences As New List(Of SelectListItem)
            For Each item In Agences
                LesAgences.Add(New SelectListItem With {.Value = item.Id, .Text = item.Societe.Libelle & ":-- [" & item.Libelle & "] --"})
            Next
            ViewBag.LesAgences = LesAgences.ToList

            'Dim userAgenceId = GetCurrentUser.Personne.AgenceId
            'ViewBag.UserAgenceId=userAgenceId

            Return View(entityVM)

        End Function

        <LocalizedAuthorize(Roles:="MANAGER")>
        Function CommissionCollectriceAvecGrilleRemuneration() As ActionResult

            Dim entityVM As New StatViewModel
            '---------------les collecteurs-----------------
            LoadComboStat(entityVM)
            Dim userAgenceId = 0
            If Not User.IsInRole("ADMINISTRATEUR") And Not User.IsInRole("MANAGER") Then
                userAgenceId = GetCurrentUser.Personne.AgenceId.Value
            End If
            ViewBag.UserAgenceId = userAgenceId

            Return View(entityVM)

        End Function

        <LocalizedAuthorize(Roles:="SA,ADMINISTRATEUR,CHEFCOLLECTEUR,MANAGER")>
        Function FicheCommissionsCollecteurs() As ActionResult

            Dim entityVM As New StatViewModel
            '---------------les collecteurs-----------------
            LoadComboStat(entityVM)
            Dim userAgenceId = 0
            If Not User.IsInRole("ADMINISTRATEUR") And Not User.IsInRole("MANAGER") Then
                userAgenceId = GetCurrentUser.Personne.AgenceId.Value
            End If
            ViewBag.UserAgenceId = userAgenceId

            Return View(entityVM)

        End Function

        <LocalizedAuthorize(Roles:="SA,ADMINISTRATEUR,CHEFCOLLECTEUR,MANAGER")>
        Function AgiosParClient() As ActionResult

            Dim entityVM As New StatViewModel
            '---------------les collecteurs-----------------
            LoadComboStat(entityVM)
            Dim userAgenceId = 0
            If Not User.IsInRole("ADMINISTRATEUR") And Not User.IsInRole("MANAGER") Then
                userAgenceId = GetCurrentUser.Personne.AgenceId.Value
            End If
            ViewBag.UserAgenceId = userAgenceId

            Return View(entityVM)

        End Function



        <LocalizedAuthorize(Roles:="SA,ADMINISTRATEUR,CHEFCOLLECTEUR,MANAGER")>
        Function HistoriqueClient() As ActionResult

            Dim entityVM As New StatViewModel
            '---------------les collecteurs-----------------
            Dim listcollecteur = db.Personnes.OfType(Of Collecteur).ToList
            Dim userAgenceId = 0
            Dim listclient = db.Personnes.OfType(Of Client)().Where(Function(i) i.Etat = True).ToList
            If Not AppSession.IsManagerOrAdmin Then
                userAgenceId = GetCurrentUser.Personne.AgenceId.Value
                listcollecteur = listcollecteur.Where(Function(i) i.AgenceId = userAgenceId).ToList
                listclient = listclient.Where(Function(i) i.AgenceId = userAgenceId).ToList

            End If
            Dim listPersonne2 As New List(Of SelectListItem)
            For Each item In listcollecteur
                listPersonne2.Add(New SelectListItem With {.Value = item.Id, .Text = item.Nom & " " & item.Prenom})
            Next
            entityVM.IDsCollecteur = listPersonne2
            '---------------------------

            '----------------les clients---------------------
            Dim listPersonne22 As New List(Of SelectListItem)
            For Each item In listclient
                listPersonne22.Add(New SelectListItem With {.Value = item.Id, .Text = item.Nom & " " & item.Prenom})
            Next
            entityVM.IDsClient = listPersonne22
            '----------------------------------------------
            ViewBag.dateDebut = Now.Date.ToString(AppSession.DateFormat)
            ViewBag.dateFin = Now.Date.ToString(AppSession.DateFormat)
            ViewBag.UserAgenceId = userAgenceId
            Return View(entityVM)

        End Function

        <LocalizedAuthorize(Roles:="SA,ADMINISTRATEUR,CHEFCOLLECTEUR,MANAGER")>
        Function HistoriqueClientGlobal() As ActionResult

            Dim entityVM As New StatViewModel
            '---------------les collecteurs-----------------
            Dim userAgenceId = 0
            If Not User.IsInRole("ADMINISTRATEUR") And Not User.IsInRole("MANAGER") Then
                userAgenceId = GetCurrentUser.Personne.AgenceId.Value
            End If
            Dim listcollecteur = db.Personnes.OfType(Of Collecteur).Where(Function(i) i.AgenceId = userAgenceId).ToList
            Dim listPersonne2 As New List(Of SelectListItem)
            For Each item In listcollecteur
                listPersonne2.Add(New SelectListItem With {.Value = item.Id, .Text = item.Nom & " " & item.Prenom})
            Next
            entityVM.IDsCollecteur = listPersonne2
            '---------------------------

            '----------------les clients---------------------
            Dim listclient = db.Personnes.OfType(Of Client)().Where(Function(i) i.AgenceId = userAgenceId And i.Etat = True).ToList
            Dim listPersonne22 As New List(Of SelectListItem)
            For Each item In listclient
                listPersonne22.Add(New SelectListItem With {.Value = item.Id, .Text = item.Nom & " " & item.Prenom})
            Next
            entityVM.IDsClient = listPersonne22
            '----------------------------------------------

            '----------------on recupère la liste des agences pour filtrer---------------
            Dim listAgence = db.Agences.OfType(Of Agence)().ToList
            Dim listAgence2 As New List(Of SelectListItem)
            For Each item In listAgence
                listAgence2.Add(New SelectListItem With {.Value = item.Id, .Text = item.Societe.Libelle & ":-- [" & item.Libelle & "] --"})
            Next
            ViewBag.lesagences = listAgence2.ToList

            ViewBag.dateDebut = Now.Date.ToString(AppSession.DateFormat)
            ViewBag.dateFin = Now.Date.ToString(AppSession.DateFormat)
            ViewBag.UserAgenceId = userAgenceId
            Return View(entityVM)

        End Function

        <LocalizedAuthorize(Roles:="SA,ADMINISTRATEUR,CHEFCOLLECTEUR,MANAGER")>
        Function HistoriqueColParClient() As ActionResult

            Dim entityVM As New StatViewModel
            '---------------les collecteurs-----------------
            Dim userAgenceId = 0
            If Not User.IsInRole("ADMINISTRATEUR") And Not User.IsInRole("MANAGER") Then
                userAgenceId = GetCurrentUser.Personne.AgenceId.Value
            End If
            Dim listcollecteur = db.Personnes.OfType(Of Collecteur).Where(Function(i) i.AgenceId = userAgenceId).ToList

            Dim listPersonne2 As New List(Of SelectListItem)
            For Each item In listcollecteur
                listPersonne2.Add(New SelectListItem With {.Value = item.Id, .Text = item.Nom & " " & item.Prenom})
            Next
            entityVM.IDsCollecteur = listPersonne2
            '---------------------------

            '----------------les clients---------------------
            Dim listclient = db.Personnes.OfType(Of Client)().Where(Function(i) i.AgenceId = userAgenceId And i.Etat = True).ToList
            Dim listPersonne22 As New List(Of SelectListItem)
            For Each item In listclient
                listPersonne22.Add(New SelectListItem With {.Value = item.Id, .Text = item.Nom & " " & item.Prenom})
            Next
            entityVM.IDsClient = listPersonne22
            '----------------------------------------------

            ViewBag.dateDebut = Now.Date.ToString(AppSession.DateFormat)
            ViewBag.dateFin = Now.Date.ToString(AppSession.DateFormat)
            ViewBag.UserAgenceId = userAgenceId
            Return View(entityVM)

        End Function

        <LocalizedAuthorize(Roles:="SA,ADMINISTRATEUR,CHEFCOLLECTEUR,MANAGER")>
        Function HistoriqueColParClientGlobal() As ActionResult

            Dim entityVM As New StatViewModel
            '---------------les collecteurs-----------------
            Dim userAgenceId = 0
            If Not User.IsInRole("ADMINISTRATEUR") And Not User.IsInRole("MANAGER") Then
                userAgenceId = GetCurrentUser.Personne.AgenceId.Value
            End If
            Dim listcollecteur = db.Personnes.OfType(Of Collecteur).Where(Function(i) i.AgenceId = userAgenceId).ToList

            Dim listPersonne2 As New List(Of SelectListItem)
            For Each item In listcollecteur
                listPersonne2.Add(New SelectListItem With {.Value = item.Id, .Text = item.Nom & " " & item.Prenom})
            Next
            entityVM.IDsCollecteur = listPersonne2
            '---------------------------

            '----------------les clients---------------------
            Dim listclient = db.Personnes.OfType(Of Client)().Where(Function(i) i.AgenceId = userAgenceId And i.Etat = True).ToList
            Dim listPersonne22 As New List(Of SelectListItem)
            For Each item In listclient
                listPersonne22.Add(New SelectListItem With {.Value = item.Id, .Text = item.Nom & " " & item.Prenom})
            Next
            entityVM.IDsClient = listPersonne22
            '----------------------------------------------

            '----------------on recupère la liste des agences pour filtrer---------------
            Dim listAgence = db.Agences.OfType(Of Agence)().ToList
            Dim listAgence2 As New List(Of SelectListItem)
            For Each item In listAgence
                listAgence2.Add(New SelectListItem With {.Value = item.Id, .Text = item.Societe.Libelle & ":-- [" & item.Libelle & "] --"})
            Next
            ViewBag.lesagences = listAgence2.ToList

            ViewBag.dateDebut = Now.Date.ToString(AppSession.DateFormat)
            ViewBag.dateFin = Now.Date.ToString(AppSession.DateFormat)
            ViewBag.UserAgenceId = userAgenceId
            Return View(entityVM)

        End Function

        <LocalizedAuthorize(Roles:="SA,ADMINISTRATEUR,CHEFCOLLECTEUR,MANAGER")>
        Function HistoriqueAgence() As ActionResult

            Dim entityVM As New StatViewModel

            Dim userAgenceId = 0
            If Not User.IsInRole("ADMINISTRATEUR") And Not User.IsInRole("MANAGER") Then
                userAgenceId = GetCurrentUser.Personne.AgenceId.Value
            End If
            ViewBag.dateDebut = Now.Date.ToString(AppSession.DateFormat)
            ViewBag.dateFin = Now.Date.ToString(AppSession.DateFormat)
            ViewBag.UserAgenceId = userAgenceId
            Return View(entityVM)

        End Function

        <LocalizedAuthorize(Roles:="SA,ADMINISTRATEUR,CHEFCOLLECTEUR,MANAGER")>
        Function HistoriqueBank() As ActionResult

            Dim entityVM As New StatViewModel


            ViewBag.dateDebut = Now.Date.ToString(AppSession.DateFormat)
            ViewBag.dateFin = Now.Date.ToString(AppSession.DateFormat)
            Return View(entityVM)

        End Function

        '<LocalizedAuthorize(Roles:="SA,ADMINISTRATEUR,CHEFCOLLECTEUR,MANAGER")>
        <LocalizedAuthorize(Roles:="TEAMINFOSYSTEM***SARL")>
        Function HistoriqueCollecteur() As ActionResult

            Dim entityVM As New StatViewModel
            '---------------les collecteurs-----------------
            Dim userAgenceId = 0
            If Not User.IsInRole("ADMINISTRATEUR") And Not User.IsInRole("MANAGER") Then
                userAgenceId = GetCurrentUser.Personne.AgenceId.Value
            End If
            Dim listcollecteur = db.Personnes.OfType(Of Collecteur).Where(Function(i) i.AgenceId = userAgenceId).ToList

            Dim listPersonne2 As New List(Of SelectListItem)
            For Each item In listcollecteur
                listPersonne2.Add(New SelectListItem With {.Value = item.Id, .Text = item.Nom & " " & item.Prenom})
            Next
            entityVM.IDsCollecteur = listPersonne2
            '---------------------------

            '----------------les clients---------------------
            Dim listclient = db.Personnes.OfType(Of Client)().Where(Function(i) i.AgenceId = userAgenceId And i.Etat = True).ToList
            Dim listPersonne22 As New List(Of SelectListItem)
            For Each item In listclient
                listPersonne22.Add(New SelectListItem With {.Value = item.Id, .Text = item.Nom & " " & item.Prenom})
            Next
            entityVM.IDsClient = listPersonne22
            '----------------------------------------------

            ViewBag.dateDebut = Now.Date.ToString(AppSession.DateFormat)
            ViewBag.dateFin = Now.Date.ToString(AppSession.DateFormat)
            ViewBag.UserAgenceId = userAgenceId
            Return View(entityVM)

        End Function

        <LocalizedAuthorize(Roles:="SA,ADMINISTRATEUR,CHEFCOLLECTEUR,MANAGER")>
        Function HistoriqueCollecteurGlobal() As ActionResult

            Dim entityVM As New StatViewModel
            '---------------les collecteurs-----------------
            Dim userAgenceId = 0
            If Not User.IsInRole("ADMINISTRATEUR") And Not User.IsInRole("MANAGER") Then
                userAgenceId = GetCurrentUser.Personne.AgenceId.Value
            End If
            Dim listcollecteur = db.Personnes.OfType(Of Collecteur).Where(Function(i) i.AgenceId = userAgenceId).ToList

            Dim listPersonne2 As New List(Of SelectListItem)
            For Each item In listcollecteur
                listPersonne2.Add(New SelectListItem With {.Value = item.Id, .Text = item.Nom & " " & item.Prenom})
            Next
            entityVM.IDsCollecteur = listPersonne2
            '---------------------------

            '----------------les clients---------------------
            Dim listclient = db.Personnes.OfType(Of Client)().Where(Function(i) i.AgenceId = userAgenceId And i.Etat = True).ToList
            Dim listPersonne22 As New List(Of SelectListItem)
            For Each item In listclient
                listPersonne22.Add(New SelectListItem With {.Value = item.Id, .Text = item.Nom & " " & item.Prenom})
            Next
            entityVM.IDsClient = listPersonne22
            '----------------------------------------------

            '----------------on recupère la liste des agences pour filtrer---------------
            Dim listAgence = db.Agences.OfType(Of Agence)().ToList
            Dim listAgence2 As New List(Of SelectListItem)
            For Each item In listAgence
                listAgence2.Add(New SelectListItem With {.Value = item.Id, .Text = item.Societe.Libelle & ":-- [" & item.Libelle & "] --"})
            Next
            ViewBag.lesagences = listAgence2.ToList

            ViewBag.dateDebut = Now.Date.ToString(AppSession.DateFormat)
            ViewBag.dateFin = Now.Date.ToString(AppSession.DateFormat)
            ViewBag.UserAgenceId = userAgenceId
            Return View(entityVM)

        End Function

        <LocalizedAuthorize(Roles:="SA,ADMINISTRATEUR,CHEFCOLLECTEUR,MANAGER")>
        Function RecetteClient() As ActionResult

            Dim entityVM As New StatViewModel
            '---------------les collecteurs-----------------
            Dim userAgenceId = 0
            If Not User.IsInRole("ADMINISTRATEUR") And Not User.IsInRole("MANAGER") Then
                userAgenceId = GetCurrentUser.Personne.AgenceId.Value
            End If
            Dim listcollecteur = db.Personnes.OfType(Of Collecteur).Where(Function(i) i.AgenceId = userAgenceId).ToList

            Dim listPersonne2 As New List(Of SelectListItem)
            For Each item In listcollecteur
                listPersonne2.Add(New SelectListItem With {.Value = item.Id, .Text = item.Nom & " " & item.Prenom})
            Next
            entityVM.IDsCollecteur = listPersonne2
            '---------------------------

            '----------------les clients---------------------
            Dim listclient = db.Personnes.OfType(Of Client)().Where(Function(i) i.AgenceId = userAgenceId And i.Etat = True).ToList
            Dim listPersonne22 As New List(Of SelectListItem)
            For Each item In listclient
                listPersonne22.Add(New SelectListItem With {.Value = item.Id, .Text = item.Nom & " " & item.Prenom})
            Next
            entityVM.IDsClient = listPersonne22
            '----------------------------------------------

            ViewBag.dateDebut = Now.Date.ToString(AppSession.DateFormat)
            ViewBag.dateFin = Now.Date.ToString(AppSession.DateFormat)
            ViewBag.UserAgenceId = userAgenceId
            Return View(entityVM)

        End Function

        <LocalizedAuthorize(Roles:="SA,ADMINISTRATEUR,CHEFCOLLECTEUR,MANAGER")>
        Function RecetteClientGlobal() As ActionResult

            Dim entityVM As New StatViewModel
            '---------------les collecteurs-----------------
            Dim userAgenceId = 0
            If Not User.IsInRole("ADMINISTRATEUR") And Not User.IsInRole("MANAGER") Then
                userAgenceId = GetCurrentUser.Personne.AgenceId.Value
            End If
            Dim listcollecteur = db.Personnes.OfType(Of Collecteur).Where(Function(i) i.AgenceId = userAgenceId).ToList

            Dim listPersonne2 As New List(Of SelectListItem)
            For Each item In listcollecteur
                listPersonne2.Add(New SelectListItem With {.Value = item.Id, .Text = item.Nom & " " & item.Prenom})
            Next
            entityVM.IDsCollecteur = listPersonne2
            '---------------------------

            '----------------les clients---------------------
            Dim listclient = db.Personnes.OfType(Of Client)().Where(Function(i) i.AgenceId = userAgenceId And i.Etat = True).ToList
            Dim listPersonne22 As New List(Of SelectListItem)
            For Each item In listclient
                listPersonne22.Add(New SelectListItem With {.Value = item.Id, .Text = item.Nom & " " & item.Prenom})
            Next
            entityVM.IDsClient = listPersonne22
            '----------------------------------------------

            '----------------on recupère la liste des agences pour filtrer---------------
            Dim listAgence = db.Agences.OfType(Of Agence)().ToList
            Dim listAgence2 As New List(Of SelectListItem)
            For Each item In listAgence
                listAgence2.Add(New SelectListItem With {.Value = item.Id, .Text = item.Societe.Libelle & ":-- [" & item.Libelle & "] --"})
            Next
            ViewBag.lesagences = listAgence2.ToList

            ViewBag.dateDebut = Now.Date.ToString(AppSession.DateFormat)
            ViewBag.dateFin = Now.Date.ToString(AppSession.DateFormat)
            ViewBag.UserAgenceId = userAgenceId
            Return View(entityVM)

        End Function

        <LocalizedAuthorize(Roles:="SA,ADMINISTRATEUR,CHEFCOLLECTEUR,MANAGER")>
        Function RecetteCollecteur() As ActionResult

            Dim entityVM As New StatViewModel
            '---------------les collecteurs-----------------
            Dim userAgenceId = 0
            If Not User.IsInRole("ADMINISTRATEUR") And Not User.IsInRole("MANAGER") Then
                userAgenceId = GetCurrentUser.Personne.AgenceId.Value
            End If
            Dim listcollecteur = db.Personnes.OfType(Of Collecteur).Where(Function(i) i.AgenceId = userAgenceId).ToList

            Dim listPersonne2 As New List(Of SelectListItem)
            For Each item In listcollecteur
                listPersonne2.Add(New SelectListItem With {.Value = item.Id, .Text = item.Nom & " " & item.Prenom})
            Next
            entityVM.IDsCollecteur = listPersonne2
            '---------------------------

            '----------------les clients---------------------
            Dim listclient = db.Personnes.OfType(Of Client)().Where(Function(i) i.AgenceId = userAgenceId And i.Etat = True).ToList
            Dim listPersonne22 As New List(Of SelectListItem)
            For Each item In listclient
                listPersonne22.Add(New SelectListItem With {.Value = item.Id, .Text = item.Nom & " " & item.Prenom})
            Next
            entityVM.IDsClient = listPersonne22
            '----------------------------------------------



            ViewBag.dateDebut = Now.Date.ToString(AppSession.DateFormat)
            ViewBag.dateFin = Now.Date.ToString(AppSession.DateFormat)
            ViewBag.UserAgenceId = userAgenceId
            Return View(entityVM)

        End Function

        <LocalizedAuthorize(Roles:="SA,ADMINISTRATEUR,CHEFCOLLECTEUR,MANAGER")>
        Function RecetteCollecteurGlobal() As ActionResult

            Dim entityVM As New StatViewModel
            '---------------les collecteurs-----------------
            Dim userAgenceId = 0
            If Not User.IsInRole("ADMINISTRATEUR") And Not User.IsInRole("MANAGER") Then
                userAgenceId = GetCurrentUser.Personne.AgenceId.Value
            End If
            Dim listcollecteur = db.Personnes.OfType(Of Collecteur).Where(Function(i) i.AgenceId = userAgenceId).ToList

            Dim listPersonne2 As New List(Of SelectListItem)
            For Each item In listcollecteur
                listPersonne2.Add(New SelectListItem With {.Value = item.Id, .Text = item.Nom & " " & item.Prenom})
            Next
            entityVM.IDsCollecteur = listPersonne2
            '---------------------------

            '----------------les clients---------------------
            Dim listclient = db.Personnes.OfType(Of Client)().Where(Function(i) i.AgenceId = userAgenceId And i.Etat = True).ToList
            Dim listPersonne22 As New List(Of SelectListItem)
            For Each item In listclient
                listPersonne22.Add(New SelectListItem With {.Value = item.Id, .Text = item.Nom & " " & item.Prenom})
            Next
            entityVM.IDsClient = listPersonne22
            '----------------------------------------------

            '----------------on recupère la liste des agences pour filtrer---------------
            Dim listAgence = db.Agences.OfType(Of Agence)().ToList
            Dim listAgence2 As New List(Of SelectListItem)
            For Each item In listAgence
                listAgence2.Add(New SelectListItem With {.Value = item.Id, .Text = item.Societe.Libelle & ":-- [" & item.Libelle & "] --"})
            Next
            ViewBag.lesagences = listAgence2.ToList

            ViewBag.dateDebut = Now.Date.ToString(AppSession.DateFormat)
            ViewBag.dateFin = Now.Date.ToString(AppSession.DateFormat)
            ViewBag.UserAgenceId = userAgenceId
            Return View(entityVM)

        End Function

        ' GET: /Rapport
        <LocalizedAuthorize(Roles:="SA,ADMINISTRATEUR,CHEFCOLLECTEUR,MANAGER")>
        Function ClientInactif() As ActionResult
            ViewBag.dateDebut = Now.Date.ToString(AppSession.DateFormat)
            ViewBag.dateFin = Now.Date.ToString(AppSession.DateFormat)
            Dim userAgenceId = 0
            If Not User.IsInRole("ADMINISTRATEUR") And Not User.IsInRole("MANAGER") Then
                userAgenceId = GetCurrentUser.Personne.AgenceId.Value
            End If
            ViewBag.UserAgenceId = userAgenceId
            Return View()
        End Function
        ' GET: /Collecteur/
        <LocalizedAuthorize(Roles:="CHEFCOLLECTEUR,ADMINISTRATEUR,MANAGER")>
        Function Index(page As Integer?, dateDebut As String, dateFin As String, ClientId As Long?, CollecteurId As Long?) As ActionResult


            Dim entities = From e In db.HistoriqueMouvements.Include(Function(h) h.Client).Include(Function(h) h.Collecteur).Include(Function(h) h.JournalCaisse).Where(Function(h) h.Id = -1).ToList


            If Not IsNothing(ClientId) And Not IsNothing(CollecteurId) Then
                entities = entities.Where(Function(h) h.DateOperation.Value.Date >= dateDebut And h.DateOperation.Value.Date <= dateFin And h.ClientId = ClientId And h.CollecteurId = CollecteurId).ToList
            Else
                If (IsNothing(ClientId) And IsNothing(CollecteurId)) Then
                    'entities = entities
                Else
                    If (IsNothing(ClientId)) Then
                        If Not (IsNothing(CollecteurId)) Then
                            entities = From e In db.HistoriqueMouvements.OfType(Of HistoriqueMouvement).Where(Function(h) h.CollecteurId = CollecteurId).ToList

                            If Not IsNothing(dateDebut) And Not IsNothing(dateFin) Then
                                entities = entities.Where(Function(h) h.DateOperation.Value.Date >= dateDebut And h.DateOperation.Value.Date <= dateFin).ToList
                            Else
                                If (IsNothing(dateDebut) And IsNothing(dateFin)) Then
                                    entities = entities.Where(Function(h) h.DateOperation.Value.Date = Now.Date Or h.DateOperation.Value.Date = Now.Date).ToList
                                Else
                                    If (IsNothing(dateFin)) Then
                                        entities = entities.Where(Function(h) h.DateOperation.Value.Date >= dateDebut).ToList
                                    Else
                                        entities = entities.Where(Function(h) h.DateOperation.Value.Date <= dateFin).ToList
                                    End If

                                End If
                            End If
                        End If
                    Else
                        entities = From e In db.HistoriqueMouvements.OfType(Of HistoriqueMouvement).Where(Function(h) h.ClientId = ClientId).ToList

                        If Not IsNothing(dateDebut) And Not IsNothing(dateFin) Then
                            entities = entities.Where(Function(h) h.DateOperation.Value.Date >= dateDebut And h.DateOperation.Value.Date <= dateFin).ToList
                        Else
                            If (IsNothing(dateDebut) And IsNothing(dateFin)) Then
                                dateDebut = Now.Date.ToString(AppSession.DateFormat)
                                dateFin = Now.Date.ToString(AppSession.DateFormat)
                                entities = entities.Where(Function(h) h.DateOperation.Value.Date = Now.Date Or h.DateOperation.Value.Date = Now.Date).ToList
                            Else
                                If (IsNothing(dateFin)) Then
                                    entities = entities.Where(Function(h) h.DateOperation.Value.Date >= dateDebut).ToList
                                Else
                                    entities = entities.Where(Function(h) h.DateOperation.Value.Date <= dateFin).ToList
                                End If

                            End If
                        End If
                    End If

                End If
            End If

            'If Not IsNothing(ClientId) Or Not IsNothing(CollecteurId) Then
            '    page = 1
            'Else
            '    dateDebut = Now.Date
            '    dateFin = Now.Date
            'End If

            entities = entities.OrderByDescending(Function(e) e.DateOperation)

            ViewBag.EnregCount = entities.Count
            Dim pageSize As Integer = ConfigurationManager.AppSettings("PageSize")
            Dim pageNumber As Integer = If(page, 1)

            ViewBag.ClientId = ClientId
            ViewBag.CollecteurId = CollecteurId
            ViewBag.dateDebut = dateDebut
            ViewBag.dateFin = dateFin


            ViewBag.masom = entities.Sum(Function(s) s.Montant).Value.ToString

            Return View(entities.ToPagedList(pageNumber, pageSize))
        End Function

        ''' <summary>
        ''' Méthode permettant de rechercher un enregistrement de retrait dans la table Retrait en fonction d'un historique de mouvement.
        ''' </summary>
        ''' <remarks>
        ''' Si l'opération est liée à un retrait (via clé étrangère), alors on retourne le seul retrait de la liste.
        ''' Si l'opération n'est pas liée à un retrait (via clé étrangère), alors on retourne le seul retrait dont les informations (date et montant) se rapprochent le plus de la date d'opération.
        ''' </remarks>
        ''' <param name="historiqueMouvement">Opération concernée par l'annulation</param>
        ''' <returns>Retrait concerné par l'annulation</returns>
        Private Function GetRetraitByHistoriqueMouvement(historiqueMouvement As HistoriqueMouvement) As Retrait
            Dim Retrait As New Retrait()
            If (historiqueMouvement.Retrait.Count = 1) Then
                Retrait = historiqueMouvement.Retrait.FirstOrDefault()
            Else
                Retrait = (From e In db.Retraits Where Math.Abs(e.Montant) = Math.Abs(historiqueMouvement.Montant.Value) And
                                                     DbFunctions.TruncateTime(e.DateRetrait.Value) = DbFunctions.TruncateTime(historiqueMouvement.DateOperation.Value) And
                                                     e.Etat = True Select e).FirstOrDefault()
            End If
            Return Retrait
        End Function


        ''' <summary>
        ''' Méthode permettant de rechercher un enregistrement de vente de carnet dans la table CarnetClient en fonction d'un historique de mouvement.
        ''' </summary>
        ''' <remarks>
        ''' Si l'opération est liée à un retrait (via clé étrangère), alors on retourne la ligne de vente de carnet de la table CarnetClient.
        ''' Si l'opération n'est pas liée à un retrait (via clé étrangère), alors on retourne la ligne de vente de carnet de la table CarnetClient dont les informations (date et montant) se rapprochent le plus de la date d'opération.
        ''' </remarks>
        ''' <param name="historiqueMouvement">Opération concernée par l'annulation</param>
        ''' <returns>Ligne de vente du carnet concernée par l'annulation</returns>
        Private Function GetCarnetClientByHistoriqueMouvement(historiqueMouvement As HistoriqueMouvement) As CarnetClient
            Dim CarnetClient As New CarnetClient()
            If (historiqueMouvement.CarnetClient.Count = 1) Then
                CarnetClient = historiqueMouvement.CarnetClient.FirstOrDefault()
            Else
                CarnetClient = (From e In db.CarnetClients Where Math.Abs(e.TypeCarnet.Prix.Value) = Math.Abs(historiqueMouvement.Montant.Value) And
                                                      DbFunctions.TruncateTime(e.DateAffectation.Value) = DbFunctions.TruncateTime(historiqueMouvement.DateOperation.Value) And
                                                               e.Etat = True Select e).FirstOrDefault()
            End If
            Return CarnetClient
        End Function

        Private Async Function CancelFunction(entityVM As AnnulationViewModel, operationType As OperationType) As Task(Of ActionResult)
            Dim ChefCollecteur = GetCurrentUser() 'Récupération de l'utilisateur connecté (chef collecteur)

            'on se rassure que l'objet envoyé depuis le formulaire possède bien un identifiant non null et différent de zéro. Dans le cas échéant, on retourne un erreur.
            If IsNothing(entityVM.Id) Or entityVM.Id = 0 Then
                ModelState.AddModelError("", "Un problème est survenu durant le traitement. L'opération sélectionnée ne correspond à aucune opération valide.")
            End If

            'on cherche en base de données l'opération à annuler, puis on se rassure que l'objet retourné n'est pas vide. Dans le cas échéant, on retourne un erreur.
            Dim HistoMvt As HistoriqueMouvement = db.HistoriqueMouvements.Find(entityVM.Id)
            If IsNothing(HistoMvt) Then
                ModelState.AddModelError("", "Un problème est survenu durant le traitement. L'opération sélectionnée n'existe pas.")
            End If

            'On se rassure qu'un motif a été entré
            If String.IsNullOrEmpty(entityVM.Motif) Then
                ModelState.AddModelError("Motif", Resource.champ_Manquant)
            End If

            'On se rassure qu'il ne s'agit pas d'une opération déjà extournée. Dans le cas échéant, on retourne un erreur.
            If (HistoMvt.Extourner.HasValue) Then
                ModelState.AddModelError("", "Cette opération a déjà été extournée. Veuillez contacter l'administrateur en cas de problème.")
            End If

            If IsNothing(HistoMvt.Client) Then 'On se rassure que le client existe bel et bien. Dans le cas échéant, on retourne un erreur.
                ModelState.AddModelError("", "Un problème est survenu durant le traitement. Le client sélectionné n'existe pas.")
            End If

            'On vérifie si la caisse utilisée pour l'opération est encore ouverte.
            If IsNothing(HistoMvt.JournalCaisse) Then
                ErrorMsg = "La Caisse concernée par l'opération est déjà fermée."
                ModelState.AddModelError("", "Un problème est survenu durant le traitement. La Caisse concernée par l'opération est déjà fermée ou n'existe pas.")
            End If
            If (ModelState.IsValid) Then
                Using transaction = db.Database.BeginTransaction
                    Try

                        'On passe l'opération à l'état "opération extournée
                        HistoMvt.Extourner = True
                        db.Entry(HistoMvt).State = EntityState.Modified

                        db.Annulation.Add(
                            New Annulation With {.DateAnnulation = Now,
                            .Motif = entityVM.Motif,
                            .HistoriqueMouvementId = HistoMvt.Id,
                            .UserId = ChefCollecteur.Id}
                            )

                        Dim LibOperation As String = ""
                        If (operationType = OperationType.AnnulationCollecte) Then
                            LibOperation = "ANNULATION COLLECT- " & HistoMvt.Id & "de " & HistoMvt.Montant & "Du " & HistoMvt.DateOperation
                            'mise a jour du solde du client
                            HistoMvt.Client.Solde = HistoMvt.Client.Solde.Value - HistoMvt.Montant.Value
                        ElseIf (operationType = OperationType.AnnulationRetrait) Then
                            LibOperation = "ANNULATION RETRAIT- " & HistoMvt.Id & "de " & HistoMvt.Montant & "Du " & HistoMvt.DateOperation
                            'mise a jour du solde du client
                            HistoMvt.Client.Solde = HistoMvt.Client.Solde.Value + Math.Abs(HistoMvt.Montant.Value)

                            Dim Retrait = GetRetraitByHistoriqueMouvement(HistoMvt)
                            Retrait.Etat = False
                            db.Entry(Retrait).State = EntityState.Modified
                        Else
                            LibOperation = "ANNULATION VENTE DE CARNET- " & HistoMvt.Id & "de " & HistoMvt.Montant & "Du " & HistoMvt.DateOperation
                            'mise a jour du solde du client
                            HistoMvt.Client.Solde = HistoMvt.Client.Solde.Value + Math.Abs(HistoMvt.Montant.Value)

                            Dim CarnetClient = GetCarnetClientByHistoriqueMouvement(HistoMvt)
                            CarnetClient.Etat = False
                            db.Entry(CarnetClient).State = EntityState.Modified
                        End If

                        'avant de valider la mise à jour du solde, on vérifie que le solde du client est toujours positif. Dans le cas contraire on lève une exception et on arrête la transaction
                        If (HistoMvt.Client.Solde.Value < 0) Then
                            ErrorMsg = String.Format("Le solde de ce client ne permet pas d'effectuer cette opération. Solde client disponible = {0} Fcfa",
                                                     String.Format("{0:#,#.00#######}", HistoMvt.Client.SoldeDisponible.ToString()))

                            Throw New Exception(ErrorMsg, New Exception(ErrorMsg))
                        End If
                        db.Entry(HistoMvt.Client).State = EntityState.Modified

                        Dim historiqueMouvement As New HistoriqueMouvement With {
                                            .ClientId = HistoMvt.Client.Id,
                                            .CollecteurId = HistoMvt.CollecteurId,
                                            .Montant = -HistoMvt.Montant,
                                            .DateOperation = DateTime.Now,
                                            .Pourcentage = 0,
                                            .MontantRetenu = 0,
                                            .EstTraiter = 0,
                                            .Etat = False,
                                            .DateCreation = DateTime.Now,
                                            .UserId = ChefCollecteur.Id,
                                            .JournalCaisseId = HistoMvt.JournalCaisseId,
                                            .LibelleOperation = LibOperation
                                        }

                        db.HistoriqueMouvements.Add(historiqueMouvement)
                        Await db.SaveChangesAsync()

                        transaction.Commit()
                        Return RedirectToAction("IndexAgence", "Client")
                    Catch ex As Exception
                        transaction.Rollback()
                        ModelState.AddModelError("", ex.InnerException.Message.ToString())
                    End Try
                End Using
            End If

            Return View(entityVM)
        End Function

        'Get/Annulation/5
        <HttpGet()>
        <LocalizedAuthorize(Roles:="SA,ADMINISTRATEUR,CHEFCOLLECTEUR")>
        Function Annulation(ByVal id As Long?, dateDebut As String, dateFin As String, CollecteurId As Long?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim HistoMvt As HistoriqueMouvement = db.HistoriqueMouvements.Find(id)
            If IsNothing(HistoMvt) Then
                Return HttpNotFound()
            End If

            Dim entityVM As New AnnulationViewModel With {
                .Id = HistoMvt.Id,
                .DateDebut = dateDebut,
                .DateFin = dateFin,
                .CollecteurId = CollecteurId
            }

            Return View(entityVM)
        End Function


        ' POST: HistoriqueMouvement/Annulation/5
        'Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        'plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        <HttpPost()>
        <LocalizedAuthorize(Roles:="SA,ADMINISTRATEUR,CHEFCOLLECTEUR")>
        Async Function Annulation(entityVM As AnnulationViewModel) As Task(Of ActionResult)
            Return Await CancelFunction(entityVM, OperationType.AnnulationCollecte)
        End Function

        'Get/AnnulationRetrait/5
        <HttpGet()>
        <LocalizedAuthorize(Roles:="SA,ADMINISTRATEUR,CHEFCOLLECTEUR")>
        Function AnnulationRetrait(ByVal id As Long?, dateDebut As String, dateFin As String, CollecteurId As Long?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim HistoMvt As HistoriqueMouvement = db.HistoriqueMouvements.Find(id)
            If IsNothing(HistoMvt) Then
                Return HttpNotFound()
            End If

            Dim entityVM As New AnnulationViewModel With {
                .Id = HistoMvt.Id,
                .DateDebut = dateDebut,
                .DateFin = dateFin,
                .CollecteurId = CollecteurId
            }

            Return View(entityVM)
        End Function

        'POST/AnnulationRetrait/5
        <HttpPost()>
        <LocalizedAuthorize(Roles:="SA,ADMINISTRATEUR,CHEFCOLLECTEUR")>
        Async Function AnnulationRetrait(entityVM As AnnulationViewModel) As Task(Of ActionResult)
            Return Await CancelFunction(entityVM, OperationType.AnnulationRetrait)
        End Function

        'Get/AnnulationVente/5
        <HttpGet()>
        <LocalizedAuthorize(Roles:="SA,ADMINISTRATEUR,CHEFCOLLECTEUR")>
        Function AnnulationVente(ByVal id As Long?, dateDebut As String, dateFin As String, CollecteurId As Long?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim HistoMvt As HistoriqueMouvement = db.HistoriqueMouvements.Find(id)
            If IsNothing(HistoMvt) Then
                Return HttpNotFound()
            End If

            Dim entityVM As New AnnulationViewModel With {
                .Id = HistoMvt.Id,
                .DateDebut = dateDebut,
                .DateFin = dateFin,
                .CollecteurId = CollecteurId
            }

            Return View(entityVM)
        End Function

        'POST/AnnulationVente/5
        <HttpPost()>
        <LocalizedAuthorize(Roles:="SA,ADMINISTRATEUR,CHEFCOLLECTEUR")>
        Async Function AnnulationVente(entityVM As AnnulationViewModel) As Task(Of ActionResult)
            Return Await CancelFunction(entityVM, OperationType.AnnulationVente)
        End Function


        <LocalizedAuthorize(Roles:="CHEFCOLLECTEUR,ADMINISTRATEUR")>
        Function Export(page As Integer?, dateDebut As String, dateFin As String, ClientId As Long?, TypeExport As Long?) As ActionResult

            Dim userAgenceId = GetCurrentUser.Personne.AgenceId

            'on test si une caisse est ouverte
            Dim nbreDouverture = db.JournalCaisses.OfType(Of JournalCaisse).Where(Function(h) h.Id = -1 Or (Not h.DateOuverture Is Nothing And h.DateCloture Is Nothing And h.Collecteur.AgenceId = userAgenceId)).Count

            'les donnees à traiter
            Dim lesdonnessTraitees = (From c In db.HistoriqueMouvements.Include(Function(h) h.Client).Include(Function(h) h.Collecteur).Include(Function(h) h.JournalCaisse).Where(Function(h) h.EstTraiter = 0 And h.Collecteur.AgenceId = userAgenceId)).ToList

            'total par client (group by client) ayant estraiter à 0 (non traité)
            Dim entities = (From c In db.HistoriqueMouvements.Include(Function(h) h.Client).Include(Function(h) h.Collecteur).Include(Function(h) h.JournalCaisse).Where(Function(h) h.EstTraiter = 0 And h.Collecteur.AgenceId = userAgenceId).ToList
                            Group By c.Client
                                Into Total = Sum(c.Montant), PartBank = Sum(c.PartBANK), PartClient = Sum(c.PartCLIENT)
                            Order By Total Descending
                            Select Client, Client.CodeSecret, Client.NumeroCompte, Client.CNI, Client.Nom, Client.Prenom, PartBank, PartClient, Total).Distinct.ToList

            If (nbreDouverture >= 1) Then
                ModelState.AddModelError("", "Il ya au moins une caisse ouverte feuillez la fermer, avant d'exporter les données.")
                ViewBag.nbreDouverture = nbreDouverture
            Else

                If (TypeExport.HasValue) Then

                    If (IsNothing(ClientId)) Then
                        'ON PUT A JOUR LE SOLDE DES CLIENTS SANS NUMERO DE COMPTE
                        For Each clt In entities
                            Dim unclient = New Client
                            If (String.IsNullOrEmpty(clt.NumeroCompte)) Then
                                unclient = clt.Client
                                unclient.Solde = clt.Total
                                Try
                                    db.SaveChanges()
                                Catch ex As DbEntityValidationException
                                    Util.GetError(ex, ModelState)
                                Catch ex As Exception
                                    Util.GetError(ex, ModelState)
                                End Try
                            End If
                        Next

                        'generation du fichier excel pour tous les clients ayant un numero de compte
                        Dim gv = New GridView With {
                            .DataSource = entities.Where(Function(h) h.Client.NumeroCompte IsNot Nothing)
                        }
                        gv.DataBind()

                        Dim dt = New DataTable("ExportClient")
                        For Each cell As TableCell In gv.HeaderRow.Cells
                            dt.Columns.Add(cell.Text)
                        Next
                        For Each row As GridViewRow In gv.Rows
                            dt.Rows.Add()
                            For i As Integer = 0 To row.Cells.Count - 1
                                dt.Rows(dt.Rows.Count - 1)(i) = row.Cells(i).Text
                            Next
                        Next

                        Using wb As New XLWorkbook()
                            wb.Worksheets.Add(dt)

                            Response.Clear()
                            Response.Buffer = True
                            Response.Charset = ""
                            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                            Response.AddHeader("content-disposition", "attachment;filename=EportTotalClientExcel_DU" & Now.Date & "_A" & Now.Hour & Now.Minute & Now.Second & ".xlsx")
                            Using MyMemoryStream As New MemoryStream()
                                wb.SaveAs(MyMemoryStream)
                                MyMemoryStream.WriteTo(Response.OutputStream)
                                Response.Flush()
                                Response.End()
                            End Using
                        End Using

                        'on update le champ EstTraite
                        For Each item In lesdonnessTraitees
                            Dim idclt = item.ClientId
                            Dim clt As Client = db.Personnes.Find(idclt)
                            If (String.IsNullOrEmpty(clt.NumeroCompte)) Then
                                item.EstTraiter = 2
                            Else
                                item.EstTraiter = 1
                            End If
                        Next
                        Try
                            db.SaveChanges()
                            Return RedirectToAction("Export", "HistoriqueMouvement")
                        Catch ex As DbEntityValidationException
                            Util.GetError(ex, ModelState)
                        Catch ex As Exception
                            Util.GetError(ex, ModelState)
                        End Try

                    End If

                    '------------------------
                    's'il ya un client 
                    If Not (IsNothing(ClientId)) Then

                        entities = entities.Where(Function(h) h.Client.Id = ClientId).ToList

                        lesdonnessTraitees = lesdonnessTraitees.Where(Function(h) h.Client.Id = ClientId).ToList

                        'generation du fichier excel pour le client encours
                        If Not (IsNothing(TypeExport)) Then

                            'generation du fichier excel pour le client
                            Dim gv = New GridView With {
                                .DataSource = entities.Where(Function(h) h.Client.NumeroCompte IsNot Nothing)
                            }
                            gv.DataBind()

                            Dim dt = New DataTable()
                            For Each cell As TableCell In gv.HeaderRow.Cells
                                dt.Columns.Add(cell.Text)
                            Next
                            For Each row As GridViewRow In gv.Rows
                                dt.Rows.Add()
                                For i As Integer = 0 To row.Cells.Count - 1
                                    dt.Rows(dt.Rows.Count - 1)(i) = row.Cells(i).Text
                                Next
                            Next

                            Using wb As New XLWorkbook()
                                wb.Worksheets.Add(dt)

                                Response.Clear()
                                Response.Buffer = True
                                Response.Charset = ""
                                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                                Response.AddHeader("content-disposition", "attachment;filename=SqlExport.xlsx")
                                Using MyMemoryStream As New MemoryStream()
                                    wb.SaveAs(MyMemoryStream)
                                    MyMemoryStream.WriteTo(Response.OutputStream)
                                    Response.Flush()
                                    Response.End()
                                End Using
                            End Using


                            '------------------
                            'on update le champ EstTraite
                            For Each item In lesdonnessTraitees
                                Dim idclt = item.ClientId
                                Dim clt As Client = db.Personnes.Find(idclt)
                                If (String.IsNullOrEmpty(clt.NumeroCompte)) Then
                                    item.EstTraiter = 2
                                Else
                                    item.EstTraiter = 1
                                End If
                            Next
                            Try
                                db.SaveChanges()
                                Return RedirectToAction("Export", "HistoriqueMouvement")

                            Catch ex As DbEntityValidationException
                                Util.GetError(ex, ModelState)
                            Catch ex As Exception
                                Util.GetError(ex, ModelState)
                            End Try
                        End If
                    End If

                Else
                    ViewBag.EnregCount = entities.Count

                    Dim pageSize As Integer = ConfigurationManager.AppSettings("PageSize")
                    Dim pageNumber As Integer = If(page, 1)

                    ViewBag.ClientId = ClientId

                    'on envoie le resultat de la requette à la vue
                    Dim Listresult As New List(Of HistoriqueMouvement)
                    For Each Item In entities
                        Dim result = New HistoriqueMouvement With {
                            .Client = Item.Client,
                            .Montant = Item.Total
                        }
                        Listresult.Add(result)
                    Next

                    Return View(Listresult.ToPagedList(pageNumber, pageSize))

                End If
            End If

            Return View()

        End Function

        <LocalizedAuthorize(Roles:="CHEFCOLLECTEUR,ADMINISTRATEUR")>
        Function Import(VientDeForm As Boolean?) As ActionResult

            Dim userAgenceId = GetCurrentUser.Personne.AgenceId

            'on test si une caisse est ouverte
            Dim nbreDouverture = db.JournalCaisses.OfType(Of JournalCaisse).Where(Function(h) h.Id = -1 Or (Not h.DateOuverture Is Nothing And h.DateCloture Is Nothing And h.Collecteur.AgenceId = userAgenceId)).Count
            If (nbreDouverture >= 1) Then
                ModelState.AddModelError("", "Il ya au moins une caisse ouverte feuillez la fermer, avant d'importer les données.")
            Else
                If (VientDeForm = True) Then
                    'Try
                    Dim fileExtension As String = System.IO.Path.GetExtension(Request.Files("Fichier").FileName)
                    If fileExtension = ".xls" OrElse fileExtension = ".xlsx" Then
                        Dim ds As New DataSet()
                        If Request.Files("Fichier").ContentLength > 0 Then

                            Dim fileLocation As String = Server.MapPath("~/Importation/") + Request.Files("Fichier").FileName

                            Try

                                If System.IO.File.Exists(fileLocation) Then
                                    System.IO.File.Delete(fileLocation)
                                End If
                                Request.Files("Fichier").SaveAs(fileLocation)
                                Dim excelConnectionString As String = String.Empty
                                excelConnectionString = (Convert.ToString("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=") & fileLocation) + ";Extended Properties=""Excel 12.0;HDR=Yes;IMEX=2"""
                                'connection String pour l'extension xls.
                                If fileExtension = ".xls" Then
                                    excelConnectionString = (Convert.ToString("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=") & fileLocation) + ";Extended Properties=""Excel 8.0;HDR=Yes;IMEX=1"""
                                    'connection String pour l'extension xlsx.
                                ElseIf fileExtension = ".xlsx" Then
                                    excelConnectionString = (Convert.ToString("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=") & fileLocation) + ";Extended Properties=""Excel 12.0;HDR=Yes;IMEX=2"""
                                End If
                                'Create Connection pour Excel
                                Dim excelConnection As New OleDbConnection(excelConnectionString)
                                excelConnection.Open()
                                Dim dt As New DataTable()
                                dt = excelConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, Nothing)
                                If dt Is Nothing Then
                                    Return Nothing
                                End If

                                Dim excelSheets As [String]() = New [String](dt.Rows.Count - 1) {}
                                Dim t As Integer = 0
                                'recuperation des données excel .
                                For Each row As DataRow In dt.Rows
                                    excelSheets(t) = row("TABLE_NAME").ToString()
                                    t += 1
                                Next
                                Dim excelConnection1 As New OleDbConnection(excelConnectionString)
                                Dim query As String = String.Format("Select * from [{0}]", excelSheets(0))
                                Using dataAdapter As New OleDbDataAdapter(query, excelConnection1)
                                    dataAdapter.Fill(ds)
                                End Using
                                For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                                    Dim con As New SqlConnection(ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString)
                                    'Dim monsolde As Decimal = CType(ds.Tables(0).Rows(i)(7).ToString, Decimal)
                                    Dim monsolde = Replace(ds.Tables(0).Rows(i)(7).ToString, ",", ".")

                                    'Decimal.TryParse(ds.Tables(0).Rows(i)(7).ToString, monsolde)
                                    Dim querySolde = "Update Client SET Solde=" & monsolde & " from Client,Personne where Client.Id = Personne.Id AND  NumeroCompte='" & ds.Tables(0).Rows(i)(1) & "' AND CNI='" & ds.Tables(0).Rows(i)(2) & "'"
                                    Dim queryChangeEtat = "Update HistoriqueMouvement SET EstTraiter = 2, DateTraitement ='" + Now.Date + "' from HistoriqueMouvement, Client where HistoriqueMouvement.ClientId = Client.Id  AND  NumeroCompte='" + ds.Tables(0).Rows(i)(1) + "' AND EstTraiter = 1"
                                    con.Open()
                                    Dim cmd As New SqlCommand(querySolde, con)
                                    Dim cmd2 As New SqlCommand(queryChangeEtat, con)
                                    cmd.ExecuteNonQuery()
                                    cmd2.ExecuteNonQuery()
                                    con.Close()
                                Next
                                excelConnection.Close()
                                excelConnection.Dispose()
                                excelConnection1.Close()
                                excelConnection1.Dispose()
                                Return RedirectToAction("Index", "Client")
                            Catch ex As SqlException
                                ViewBag.Error = "true"

                            Catch ex As Exception
                                ViewBag.Error = "true"

                            End Try
                        Else

                        End If
                    Else

                    End If

                End If
            End If

            ViewBag.nbreDouverture = nbreDouverture
            Return View()

        End Function

        ' GET: /HistoriqueMouvement/Create
        Function Create() As ActionResult
            Dim colVM As New LaCollecteViewModel

            Return View(colVM)
        End Function

    End Class
End Namespace