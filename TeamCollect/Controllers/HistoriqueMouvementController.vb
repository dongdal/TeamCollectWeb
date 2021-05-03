Imports System.Data.Entity
Imports System.Net
Imports PagedList
Imports Microsoft.AspNet.Identity
Imports System.Data.Entity.Validation
Imports System.IO
Imports ClosedXML.Excel
Imports System.Data.SqlClient
Imports System.Data.OleDb



Namespace TeamCollect
    Public Class HistoriqueMouvementController
        Inherits BaseController

        Private db As New ApplicationDbContext

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

            Dim LesJournauxCaisse = db.JournalCaisses.ToList

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

            ViewBag.dateDebut = Now.Date.ToString("d")
            ViewBag.dateFin = Now.Date.ToString("d")
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

            ViewBag.dateDebut = Now.Date.ToString("d")
            ViewBag.dateFin = Now.Date.ToString("d")
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

            ViewBag.dateDebut = Now.Date.ToString("d")
            ViewBag.dateFin = Now.Date.ToString("d")
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

            ViewBag.dateDebut = Now.Date.ToString("d")
            ViewBag.dateFin = Now.Date.ToString("d")

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

            ViewBag.dateDebut = Now.Date.ToString("d")
            ViewBag.dateFin = Now.Date.ToString("d")
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

            ViewBag.dateDebut = Now.Date.ToString("d")
            ViewBag.dateFin = Now.Date.ToString("d")
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

            ViewBag.dateDebut = Now.Date.ToString("d")
            ViewBag.dateFin = Now.Date.ToString("d")
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

            ViewBag.dateDebut = Now.Date.ToString("d")
            ViewBag.dateFin = Now.Date.ToString("d")
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
            ViewBag.dateDebut = Now.Date.ToString("d")
            ViewBag.dateFin = Now.Date.ToString("d")
            ViewBag.UserAgenceId = userAgenceId
            Return View(entityVM)

        End Function

        <LocalizedAuthorize(Roles:="SA,ADMINISTRATEUR,CHEFCOLLECTEUR,MANAGER")>
        Function HistoriqueBank() As ActionResult

            Dim entityVM As New StatViewModel


            ViewBag.dateDebut = Now.Date.ToString("d")
            ViewBag.dateFin = Now.Date.ToString("d")
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

            ViewBag.dateDebut = Now.Date.ToString("d")
            ViewBag.dateFin = Now.Date.ToString("d")
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

            ViewBag.dateDebut = Now.Date.ToString("d")
            ViewBag.dateFin = Now.Date.ToString("d")
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

            ViewBag.dateDebut = Now.Date.ToString("d")
            ViewBag.dateFin = Now.Date.ToString("d")
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

            ViewBag.dateDebut = Now.Date.ToString("d")
            ViewBag.dateFin = Now.Date.ToString("d")
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



            ViewBag.dateDebut = Now.Date.ToString("d")
            ViewBag.dateFin = Now.Date.ToString("d")
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

            ViewBag.dateDebut = Now.Date.ToString("d")
            ViewBag.dateFin = Now.Date.ToString("d")
            ViewBag.UserAgenceId = userAgenceId
            Return View(entityVM)

        End Function

        ' GET: /Rapport
        <LocalizedAuthorize(Roles:="SA,ADMINISTRATEUR,CHEFCOLLECTEUR,MANAGER")>
        Function ClientInactif() As ActionResult
            ViewBag.dateDebut = Now.Date.ToString("d")
            ViewBag.dateFin = Now.Date.ToString("d")
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


        '<ValidateAntiForgeryToken()>
        <HttpPost()>
        <LocalizedAuthorize(Roles:="SA,ADMINISTRATEUR,CHEFCOLLECTEUR")>
        Function Annulation(entityVM As AnnulationViewModel) As JsonResult
            'on recupere l'id du collecteur chef collect connecter
            Dim HistoriqueMouvementId = entityVM.Id
            Dim Motif = entityVM.Motif

            If IsNothing(HistoriqueMouvementId) Then
                'Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
                Return Json(New With {.Result = "Error " & DirectCast(HttpStatusCode.BadRequest, Integer).ToString()})
            End If

            Dim HistoMvt As HistoriqueMouvement = db.HistoriqueMouvements.Find(HistoriqueMouvementId)

            If IsNothing(HistoMvt) Then
                'Return HttpNotFound()
                Return Json(New With {.Result = "Error " & HttpNotFound().StatusCode.ToString()})
            End If

            If (HistoMvt.Extourner) Then
                ModelState.AddModelError("", "Cette opération a déjà été extournée. Veuillez contacter l'administrateur en cas de problème.")
                'Return View(entityVM)
                Return Json(New With {.Result = "Error: Cette opération a déjà été extournée. Veuillez contacter l'administrateur en cas de problème."})
            End If

            Dim DateCollect = HistoMvt.DateOperation
            Dim clientId = HistoMvt.ClientId
            Dim Montant = HistoMvt.Montant

            Dim client = db.Clients.Find(clientId)
            Dim UserId = GetCurrentUser.Id
            If IsNothing(client) Then
                ModelState.AddModelError("Motif", "La transaction n'a pas été éffectuer: client introuvable...")
                Return Json(New With {.Result = "Error: La transaction n'a pas été éffectuer: client introuvable..."})
                'Return View(entityVM)
            End If

            'on testte si le collecter connecter a une caisse ouverte
            Dim CollecteurId = HistoMvt.CollecteurId 'entityVM.CollecteurId 'ConfigurationManager.AppSettings("CollecteurSystemeId")
            Dim LacaisseConcerner = HistoMvt.JournalCaisseId
            Dim LesJournalCaisse = (From J In db.JournalCaisses Where J.CollecteurId = CollecteurId And J.Id = LacaisseConcerner And J.Etat = 0 Select J).ToArray
            If (LesJournalCaisse.Count = 0) Then
                ModelState.AddModelError("Motif", "La Caisse concernée par l'opération est déjà fermée")
                Return Json(New With {.Result = "Error: La Caisse concernée par l'opération est déjà fermée"})
                'Return View(entityVM)
            End If

            'HistoMvt.Extourner = True
            'db.Entry(HistoMvt).State = EntityState.Modified
            'db.SaveChanges()
            Dim Extour As Boolean = True
            Dim myUpdateQuery As String = "Update HistoriqueMouvement Set Extourner = @Extour Where Id=@Id"
            Dim parameterList1 As New List(Of SqlParameter) From {
                New SqlParameter("@Id", HistoMvt.Id),
                New SqlParameter("@Extour", Extour)
            }
            Dim parameters1 As SqlParameter() = parameterList1.ToArray()
            db.Database.ExecuteSqlCommand(myUpdateQuery, parameters1)
            'If Not (db.Database.ExecuteSqlCommand(myUpdateQuery, parameters1)) Then
            '    ModelState.AddModelError("Motif", "Mise à l'jour Impossible de l'historique...")
            '    Return View(entityVM)
            'End If

            Dim Annul As New Annulation With {
                .DateAnnulation = Now,
                .Motif = Motif,
                .HistoriqueMouvementId = HistoMvt.Id
            }

            db.Annulation.Add(Annul)
            'db.SaveChanges()

            'mise a jour du solde du client
            client.Solde -= Montant
            db.Entry(client).State = EntityState.Modified
            'db.SaveChanges()



            Dim JCID = LacaisseConcerner 'LesJournalCaisse.FirstOrDefault.Id
            'on remet credite  la caisse du colleur
            'Dim JournalCaisse = db.JournalCaisses.Find(JCID)
            'JournalCaisse.PlafondEnCours += Montant
            'db.Entry(JournalCaisse).State = EntityState.Modified
            'db.SaveChanges()

            '3- on recupere le journal caisse et on enregistre dans mouvement historique

            Dim LibOperation As String = "ANNULATION COLLECT- " & HistoMvt.Id & "de " & Montant & "Du " & DateCollect

            Dim parameterList As New List(Of SqlParameter) From {
                New SqlParameter("@ClientId", clientId),
                New SqlParameter("@CollecteurId", CollecteurId),
                New SqlParameter("@Montant", -Montant),
                New SqlParameter("@DateOperation", Now),
                New SqlParameter("@Pourcentage", 0),
                New SqlParameter("@MontantRetenu", 0),
                New SqlParameter("@EstTraiter", 0),
                New SqlParameter("@Etat", False),
                New SqlParameter("@DateCreation", Now),
                New SqlParameter("@UserId", UserId),
                New SqlParameter("@JournalCaisseId", JCID),
                New SqlParameter("@LibelleOperation", LibOperation)
            }
            Dim parameters As SqlParameter() = parameterList.ToArray()

            Try
                db.SaveChanges()
                'Dim myInsertQuery As String = "INSERT INTO HistoriqueMouvement (ClientId, CollecteurId, Montant, DateOperation, MontantRetenu, Pourcentage, EstTraiter, Etat, DateCreation, UserId, JournalCaisseId) VALUES (@ClientId, @CollecteurId, @Montant, @DateOperation, @MontantRetenu, @Pourcentage, @EstTraiter, @Etat, @DateCreation, @UserId, @JournalCaisseId)"
                Dim myInsertQuery As String = "INSERT INTO HistoriqueMouvement (ClientId, CollecteurId, Montant, DateOperation, MontantRetenu, Pourcentage, EstTraiter, Etat, DateCreation, UserId, JournalCaisseId, LibelleOperation) VALUES (@ClientId, @CollecteurId, @Montant, @DateOperation, @MontantRetenu, @Pourcentage, @EstTraiter, @Etat, @DateCreation, @UserId, @JournalCaisseId, @LibelleOperation)"
                'Dim laDate As Date = Now
                If (db.Database.ExecuteSqlCommand(myInsertQuery, parameters)) Then
                    Dim historiquesMouvements = (From h In db.HistoriqueMouvements Where (h.UserId = UserId) Select HistoriqueId = h.Id, JournalCaisseId = h.JournalCaisseId,
                                        IdClient = h.ClientId, NomClient = h.Client.Nom, PrenomClient = h.Client.Prenom, IdCollecteur = h.CollecteurId, NomCollecteur = h.Collecteur.Nom, PrenomCollecteur = h.Collecteur.Prenom, MontantCollecte = h.Montant,
                                        FraisFixes = h.MontantRetenu, Taux = h.Pourcentage, h.DateOperation, h.LibelleOperation).ToList

                    Dim historique = historiquesMouvements.ElementAtOrDefault((historiquesMouvements.Count - 1))

                    'db.SaveChanges()

                    'Return RedirectToAction("Index", "HistoriqueMouvement", New With {CollecteurId, entityVM.DateDebut, entityVM.DateFin})
                    Return Json(New With {.Result = "OK"})

                    'Return Ok(historique)
                Else
                    ModelState.AddModelError("Motif", "Une erreur est survenue pendant l'exécution de la requête: veuillez contacter l'administrateur. ")
                    Return Json(New With {.Result = "Error: Une erreur est survenue pendant l'exécution de la requête: veuillez contacter l'administrateur."})
                    'Return View(entityVM)
                End If
            Catch ex As DbEntityValidationException
                Util.GetError(ex)
                ModelState.AddModelError("Motif", "Une erreur est survenue pendant le traitement: veuillez contacter l'administrateur.")
                Return Json(New With {.Result = "Error: Une erreur est survenue pendant le traitement: veuillez contacter l'administrateur."})
            Catch ex As Exception
                Util.GetError(ex)
                ModelState.AddModelError("Motif", "Une erreur est survenue pendant le traitement: veuillez contacter l'administrateur. ")
                Return Json(New With {.Result = "Error: Une erreur est survenue pendant le traitement: veuillez contacter l'administrateur."})
            End Try

            Return Json(New With {.Result = "Error"})
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
        Function AnnulationRetrait(entityVM As AnnulationViewModel) As JsonResult
            'on recupere l'id du collecteur chef collect connecter
            Dim HistoriqueMouvementId = entityVM.Id
            Dim Motif = entityVM.Motif

            If IsNothing(HistoriqueMouvementId) Then
                'Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
                Return Json(New With {.Result = "Error " & DirectCast(HttpStatusCode.BadRequest, Integer).ToString()})
            End If

            Dim HistoMvt As HistoriqueMouvement = db.HistoriqueMouvements.Find(HistoriqueMouvementId)

            If IsNothing(HistoMvt) Then
                'Return HttpNotFound()
                Return Json(New With {.Result = "Error " & HttpNotFound().StatusCode.ToString()})
            End If

            If (HistoMvt.Extourner) Then
                ModelState.AddModelError("", "Cette opération a déjà été extournée. Veuillez contacter l'administrateur en cas de problème.")
                'Return View(entityVM)
                Return Json(New With {.Result = "Error: Cette opération a déjà été extournée. Veuillez contacter l'administrateur en cas de problème."})
            End If

            Dim DateCollect = HistoMvt.DateOperation
            Dim clientId = HistoMvt.ClientId
            Dim Montant = HistoMvt.Montant

            Dim client = db.Clients.Find(clientId)
            Dim UserId = GetCurrentUser.Id
            If IsNothing(client) Then
                ModelState.AddModelError("Motif", "La transaction n'a pas été éffectuer: client introuvable...")
                Return Json(New With {.Result = "Error: La transaction n'a pas été éffectuer: client introuvable..."})
                'Return View(entityVM)
            End If

            'on testte si le collecter connecter a une caisse ouverte
            Dim CollecteurId = HistoMvt.CollecteurId 'entityVM.CollecteurId 'ConfigurationManager.AppSettings("CollecteurSystemeId")
            Dim LacaisseConcerner = HistoMvt.JournalCaisseId
            Dim LesJournalCaisse = (From J In db.JournalCaisses Where J.CollecteurId = CollecteurId And J.Id = LacaisseConcerner And J.Etat = 0 Select J).ToArray
            If (LesJournalCaisse.Count = 0) Then
                ModelState.AddModelError("Motif", "La Caisse concernée par l'opération est déjà fermée")
                Return Json(New With {.Result = "Error: La Caisse concernée par l'opération est déjà fermée"})
                'Return View(entityVM)
            End If

            'HistoMvt.Extourner = True
            'db.Entry(HistoMvt).State = EntityState.Modified
            'db.SaveChanges()
            Dim Extour As Boolean = True
            Dim myUpdateQuery As String = "Update HistoriqueMouvement Set Extourner = @Extour Where Id=@Id"
            Dim parameterList1 As New List(Of SqlParameter) From {
                New SqlParameter("@Id", HistoMvt.Id),
                New SqlParameter("@Extour", Extour)
            }
            Dim parameters1 As SqlParameter() = parameterList1.ToArray()
            db.Database.ExecuteSqlCommand(myUpdateQuery, parameters1)
            'If Not (db.Database.ExecuteSqlCommand(myUpdateQuery, parameters1)) Then
            '    ModelState.AddModelError("Motif", "Mise à l'jour Impossible de l'historique...")
            '    Return View(entityVM)
            'End If

            Dim Annul As New Annulation With {
                .DateAnnulation = Now,
                .Motif = Motif,
                .HistoriqueMouvementId = HistoMvt.Id
            }

            db.Annulation.Add(Annul)
            'db.SaveChanges()

            'mise a jour du solde du client
            client.Solde -= Montant
            db.Entry(client).State = EntityState.Modified
            'db.SaveChanges()



            Dim JCID = LacaisseConcerner 'LesJournalCaisse.FirstOrDefault.Id
            'on remet credite  la caisse du colleur
            'Dim JournalCaisse = db.JournalCaisses.Find(JCID)
            'JournalCaisse.PlafondEnCours += Montant
            'db.Entry(JournalCaisse).State = EntityState.Modified
            'db.SaveChanges()

            '3- on recupere le journal caisse et on enregistre dans mouvement historique

            Dim LibOperation As String = "ANNULATION RETRAIT- " & HistoMvt.Id & "de " & Montant & "Du " & DateCollect

            Dim parameterList As New List(Of SqlParameter) From {
                New SqlParameter("@ClientId", clientId),
                New SqlParameter("@CollecteurId", CollecteurId),
                New SqlParameter("@Montant", -Montant),
                New SqlParameter("@DateOperation", Now),
                New SqlParameter("@Pourcentage", 0),
                New SqlParameter("@MontantRetenu", 0),
                New SqlParameter("@EstTraiter", 0),
                New SqlParameter("@Etat", False),
                New SqlParameter("@DateCreation", Now),
                New SqlParameter("@UserId", UserId),
                New SqlParameter("@JournalCaisseId", JCID),
                New SqlParameter("@LibelleOperation", LibOperation)
            }
            Dim parameters As SqlParameter() = parameterList.ToArray()

            Try
                db.SaveChanges()
                'Dim myInsertQuery As String = "INSERT INTO HistoriqueMouvement (ClientId, CollecteurId, Montant, DateOperation, MontantRetenu, Pourcentage, EstTraiter, Etat, DateCreation, UserId, JournalCaisseId) VALUES (@ClientId, @CollecteurId, @Montant, @DateOperation, @MontantRetenu, @Pourcentage, @EstTraiter, @Etat, @DateCreation, @UserId, @JournalCaisseId)"
                Dim myInsertQuery As String = "INSERT INTO HistoriqueMouvement (ClientId, CollecteurId, Montant, DateOperation, MontantRetenu, Pourcentage, EstTraiter, Etat, DateCreation, UserId, JournalCaisseId, LibelleOperation) VALUES (@ClientId, @CollecteurId, @Montant, @DateOperation, @MontantRetenu, @Pourcentage, @EstTraiter, @Etat, @DateCreation, @UserId, @JournalCaisseId, @LibelleOperation)"
                'Dim laDate As Date = Now
                If (db.Database.ExecuteSqlCommand(myInsertQuery, parameters)) Then
                    Dim historiquesMouvements = (From h In db.HistoriqueMouvements Where (h.UserId = UserId) Select HistoriqueId = h.Id, JournalCaisseId = h.JournalCaisseId,
                                        IdClient = h.ClientId, NomClient = h.Client.Nom, PrenomClient = h.Client.Prenom, IdCollecteur = h.CollecteurId, NomCollecteur = h.Collecteur.Nom, PrenomCollecteur = h.Collecteur.Prenom, MontantCollecte = h.Montant,
                                        FraisFixes = h.MontantRetenu, Taux = h.Pourcentage, h.DateOperation, h.LibelleOperation).ToList

                    Dim historique = historiquesMouvements.ElementAtOrDefault((historiquesMouvements.Count - 1))

                    'db.SaveChanges()

                    'Return RedirectToAction("Index", "HistoriqueMouvement", New With {CollecteurId, entityVM.DateDebut, entityVM.DateFin})
                    Return Json(New With {.Result = "OK"})

                    'Return Ok(historique)
                Else
                    ModelState.AddModelError("Motif", "Une erreur est survenue pendant l'exécution de la requête: veuillez contacter l'administrateur. ")
                    Return Json(New With {.Result = "Error: Une erreur est survenue pendant l'exécution de la requête: veuillez contacter l'administrateur."})
                    'Return View(entityVM)
                End If
            Catch ex As DbEntityValidationException
                Util.GetError(ex)
                ModelState.AddModelError("Motif", "Une erreur est survenue pendant le traitement: veuillez contacter l'administrateur.")
                Return Json(New With {.Result = "Error: Une erreur est survenue pendant le traitement: veuillez contacter l'administrateur."})
            Catch ex As Exception
                Util.GetError(ex)
                ModelState.AddModelError("Motif", "Une erreur est survenue pendant le traitement: veuillez contacter l'administrateur. ")
                Return Json(New With {.Result = "Error: Une erreur est survenue pendant le traitement: veuillez contacter l'administrateur."})
            End Try

            Return Json(New With {.Result = "Error"})
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
        Function AnnulationVente(entityVM As AnnulationViewModel) As JsonResult
            'on recupere l'id du collecteur chef collect connecter
            Dim HistoriqueMouvementId = entityVM.Id
            Dim Motif = entityVM.Motif

            If IsNothing(HistoriqueMouvementId) Then
                'Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
                Return Json(New With {.Result = "Error " & DirectCast(HttpStatusCode.BadRequest, Integer).ToString()})
            End If

            Dim HistoMvt As HistoriqueMouvement = db.HistoriqueMouvements.Find(HistoriqueMouvementId)

            If IsNothing(HistoMvt) Then
                'Return HttpNotFound()
                Return Json(New With {.Result = "Error " & HttpNotFound().StatusCode.ToString()})
            End If

            If (HistoMvt.Extourner) Then
                ModelState.AddModelError("", "Cette opération a déjà été extournée. Veuillez contacter l'administrateur en cas de problème.")
                'Return View(entityVM)
                Return Json(New With {.Result = "Error: Cette opération a déjà été extournée. Veuillez contacter l'administrateur en cas de problème."})
            End If

            Dim DateCollect = HistoMvt.DateOperation
            Dim clientId = HistoMvt.ClientId
            Dim Montant = HistoMvt.Montant

            Dim client = db.Clients.Find(clientId)
            Dim UserId = GetCurrentUser.Id
            If IsNothing(client) Then
                ModelState.AddModelError("Motif", "La transaction n'a pas été éffectuer: client introuvable...")
                Return Json(New With {.Result = "Error: La transaction n'a pas été éffectuer: client introuvable..."})
                'Return View(entityVM)
            End If

            'on testte si le collecter connecter a une caisse ouverte
            Dim CollecteurId = HistoMvt.CollecteurId 'entityVM.CollecteurId 'ConfigurationManager.AppSettings("CollecteurSystemeId")
            Dim LacaisseConcerner = HistoMvt.JournalCaisseId
            Dim LesJournalCaisse = (From J In db.JournalCaisses Where J.CollecteurId = CollecteurId And J.Id = LacaisseConcerner And J.Etat = 0 Select J).ToArray
            If (LesJournalCaisse.Count = 0) Then
                ModelState.AddModelError("Motif", "La Caisse concernée par l'opération est déjà fermée")
                Return Json(New With {.Result = "Error: La Caisse concernée par l'opération est déjà fermée"})
                'Return View(entityVM)
            End If

            'HistoMvt.Extourner = True
            'db.Entry(HistoMvt).State = EntityState.Modified
            'db.SaveChanges()
            Dim Extour As Boolean = True
            Dim myUpdateQuery As String = "Update HistoriqueMouvement Set Extourner = @Extour Where Id=@Id"
            Dim parameterList1 As New List(Of SqlParameter) From {
                New SqlParameter("@Id", HistoMvt.Id),
                New SqlParameter("@Extour", Extour)
            }
            Dim parameters1 As SqlParameter() = parameterList1.ToArray()
            db.Database.ExecuteSqlCommand(myUpdateQuery, parameters1)
            'If Not (db.Database.ExecuteSqlCommand(myUpdateQuery, parameters1)) Then
            '    ModelState.AddModelError("Motif", "Mise à l'jour Impossible de l'historique...")
            '    Return View(entityVM)
            'End If

            Dim Annul As New Annulation With {
                .DateAnnulation = Now,
                .Motif = Motif,
                .HistoriqueMouvementId = HistoMvt.Id
            }

            db.Annulation.Add(Annul)
            'db.SaveChanges()

            'mise a jour du solde du client
            client.Solde -= Montant
            db.Entry(client).State = EntityState.Modified
            'db.SaveChanges()



            Dim JCID = LacaisseConcerner 'LesJournalCaisse.FirstOrDefault.Id
            'on remet credite  la caisse du colleur
            'Dim JournalCaisse = db.JournalCaisses.Find(JCID)
            'JournalCaisse.PlafondEnCours += Montant
            'db.Entry(JournalCaisse).State = EntityState.Modified
            'db.SaveChanges()

            '3- on recupere le journal caisse et on enregistre dans mouvement historique

            Dim LibOperation As String = "ANNULATION VENTE DE CARNET- " & HistoMvt.Id & "de " & Montant & "Du " & DateCollect

            Dim parameterList As New List(Of SqlParameter) From {
                New SqlParameter("@ClientId", clientId),
                New SqlParameter("@CollecteurId", CollecteurId),
                New SqlParameter("@Montant", -Montant),
                New SqlParameter("@DateOperation", Now),
                New SqlParameter("@Pourcentage", 0),
                New SqlParameter("@MontantRetenu", 0),
                New SqlParameter("@EstTraiter", 0),
                New SqlParameter("@Etat", False),
                New SqlParameter("@DateCreation", Now),
                New SqlParameter("@UserId", UserId),
                New SqlParameter("@JournalCaisseId", JCID),
                New SqlParameter("@LibelleOperation", LibOperation)
            }
            Dim parameters As SqlParameter() = parameterList.ToArray()

            Try
                db.SaveChanges()
                'Dim myInsertQuery As String = "INSERT INTO HistoriqueMouvement (ClientId, CollecteurId, Montant, DateOperation, MontantRetenu, Pourcentage, EstTraiter, Etat, DateCreation, UserId, JournalCaisseId) VALUES (@ClientId, @CollecteurId, @Montant, @DateOperation, @MontantRetenu, @Pourcentage, @EstTraiter, @Etat, @DateCreation, @UserId, @JournalCaisseId)"
                Dim myInsertQuery As String = "INSERT INTO HistoriqueMouvement (ClientId, CollecteurId, Montant, DateOperation, MontantRetenu, Pourcentage, EstTraiter, Etat, DateCreation, UserId, JournalCaisseId, LibelleOperation) VALUES (@ClientId, @CollecteurId, @Montant, @DateOperation, @MontantRetenu, @Pourcentage, @EstTraiter, @Etat, @DateCreation, @UserId, @JournalCaisseId, @LibelleOperation)"
                'Dim laDate As Date = Now
                If (db.Database.ExecuteSqlCommand(myInsertQuery, parameters)) Then
                    Dim historiquesMouvements = (From h In db.HistoriqueMouvements Where (h.UserId = UserId) Select HistoriqueId = h.Id, JournalCaisseId = h.JournalCaisseId,
                                        IdClient = h.ClientId, NomClient = h.Client.Nom, PrenomClient = h.Client.Prenom, IdCollecteur = h.CollecteurId, NomCollecteur = h.Collecteur.Nom, PrenomCollecteur = h.Collecteur.Prenom, MontantCollecte = h.Montant,
                                        FraisFixes = h.MontantRetenu, Taux = h.Pourcentage, h.DateOperation, h.LibelleOperation).ToList

                    Dim historique = historiquesMouvements.ElementAtOrDefault((historiquesMouvements.Count - 1))

                    'db.SaveChanges()

                    'Return RedirectToAction("Index", "HistoriqueMouvement", New With {CollecteurId, entityVM.DateDebut, entityVM.DateFin})
                    Return Json(New With {.Result = "OK"})

                    'Return Ok(historique)
                Else
                    ModelState.AddModelError("Motif", "Une erreur est survenue pendant l'exécution de la requête: veuillez contacter l'administrateur. ")
                    Return Json(New With {.Result = "Error: Une erreur est survenue pendant l'exécution de la requête: veuillez contacter l'administrateur."})
                    'Return View(entityVM)
                End If
            Catch ex As DbEntityValidationException
                Util.GetError(ex)
                ModelState.AddModelError("Motif", "Une erreur est survenue pendant le traitement: veuillez contacter l'administrateur.")
                Return Json(New With {.Result = "Error: Une erreur est survenue pendant le traitement: veuillez contacter l'administrateur."})
            Catch ex As Exception
                Util.GetError(ex)
                ModelState.AddModelError("Motif", "Une erreur est survenue pendant le traitement: veuillez contacter l'administrateur. ")
                Return Json(New With {.Result = "Error: Une erreur est survenue pendant le traitement: veuillez contacter l'administrateur."})
            End Try

            Return Json(New With {.Result = "Error"})
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

            Dim userAgenceId = getCurrentUser.Personne.AgenceId

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

        '' GET: /HistoriqueMouvement/Details/5
        'Function Details(ByVal id As Long?) As ActionResult
        '    If IsNothing(id) Then
        '        Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
        '    End If
        '    Dim historiquemouvement As HistoriqueMouvement = db.HistoriqueMouvements.Find(id)
        '    If IsNothing(historiquemouvement) Then
        '        Return HttpNotFound()
        '    End If
        '    Return View(historiquemouvement)
        'End Function

        ' GET: /HistoriqueMouvement/Create
        Function Create() As ActionResult
            Dim colVM As New LaCollecteViewModel

            Return View(colVM)
        End Function

    End Class
End Namespace


'<HttpPost()>
'<LocalizedAuthorize(Roles:="SA,ADMINISTRATEUR,CHEFCOLLECTEUR")>
'<ValidateAntiForgeryToken()>
'Function Annulation(entityVM As AnnulationViewModel) As ActionResult
'    'on recupere l'id du collecteur chef collect connecter
'    Dim HistoriqueMouvementId = entityVM.Id
'    Dim Motif = entityVM.Motif

'    If IsNothing(HistoriqueMouvementId) Then
'        Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
'    End If

'    Dim HistoMvt As HistoriqueMouvement = db.HistoriqueMouvements.Find(HistoriqueMouvementId)

'    If IsNothing(HistoMvt) Then
'        Return HttpNotFound()
'    End If

'    If (HistoMvt.Extourner) Then
'        ModelState.AddModelError("", "Cette opération a déjà été extournée. Veuillez contacter l'administrateur en cas de problème.")
'        Return View(entityVM)
'    End If

'    Dim DateCollect = HistoMvt.DateOperation
'    Dim clientId = HistoMvt.ClientId
'    Dim Montant = HistoMvt.Montant

'    'HistoMvt.Extourner = True
'    'db.Entry(HistoMvt).State = EntityState.Modified
'    'db.SaveChanges()
'    Dim Extour As Boolean = True
'    Dim myUpdateQuery As String = "Update HistoriqueMouvement Set Extourner = @Extour Where Id=@Id"
'    Dim parameterList1 As New List(Of SqlParameter) From {
'                New SqlParameter("@Id", HistoMvt.Id),
'                New SqlParameter("@Extour", Extour)
'            }
'    Dim parameters1 As SqlParameter() = parameterList1.ToArray()
'    db.Database.ExecuteSqlCommand(myUpdateQuery, parameters1)
'    'If Not (db.Database.ExecuteSqlCommand(myUpdateQuery, parameters1)) Then
'    '    ModelState.AddModelError("Motif", "Mise à l'jour Impossible de l'historique...")
'    '    Return View(entityVM)
'    'End If

'    Dim Annul As New Annulation With {
'                .DateAnnulation = Now,
'                .Motif = Motif,
'                .HistoriqueMouvementId = HistoMvt.Id
'            }

'    db.Annulation.Add(Annul)
'    'db.SaveChanges()

'    Dim client = db.Clients.Find(clientId)
'    Dim UserId = getCurrentUser.Id
'    If IsNothing(client) Then
'        ModelState.AddModelError("Motif", "La transaction n'a pas été éffectuer: client introuvable...")
'        Return View(entityVM)
'    End If

'    'mise a jour du solde du client
'    client.Solde -= Montant
'    db.Entry(client).State = EntityState.Modified
'    'db.SaveChanges()


'    'on testte si le collecter connecter a une caisse ouverte
'    Dim CollecteurId = HistoMvt.CollecteurId 'entityVM.CollecteurId 'ConfigurationManager.AppSettings("CollecteurSystemeId")
'    Dim LacaisseConcerner = HistoMvt.JournalCaisseId
'    Dim LesJournalCaisse = (From J In db.JournalCaisses Where J.CollecteurId = CollecteurId And J.Id = LacaisseConcerner And J.Etat = 0 Select J).ToArray
'    If (LesJournalCaisse.Count = 0) Then
'        ModelState.AddModelError("Motif", "La Caisse concernée par l'opération est déjà fermée")
'        Return View(entityVM)
'    End If

'    Dim JCID = LacaisseConcerner 'LesJournalCaisse.FirstOrDefault.Id
'    'on remet credite  la caisse du colleur
'    'Dim JournalCaisse = db.JournalCaisses.Find(JCID)
'    'JournalCaisse.PlafondEnCours += Montant
'    'db.Entry(JournalCaisse).State = EntityState.Modified
'    'db.SaveChanges()

'    '3- on recupere le journal caisse et on enregistre dans mouvement historique

'    Dim LibOperation As String = "ANNULATION COLLECT- " & HistoMvt.Id & "de " & Montant & "Du " & DateCollect

'    Dim parameterList As New List(Of SqlParameter) From {
'                New SqlParameter("@ClientId", clientId),
'                New SqlParameter("@CollecteurId", CollecteurId),
'                New SqlParameter("@Montant", -Montant),
'                New SqlParameter("@DateOperation", Now),
'                New SqlParameter("@Pourcentage", 0),
'                New SqlParameter("@MontantRetenu", 0),
'                New SqlParameter("@EstTraiter", 0),
'                New SqlParameter("@Etat", False),
'                New SqlParameter("@DateCreation", Now),
'                New SqlParameter("@UserId", UserId),
'                New SqlParameter("@JournalCaisseId", JCID),
'                New SqlParameter("@LibelleOperation", LibOperation)
'            }
'    Dim parameters As SqlParameter() = parameterList.ToArray()

'    Try
'        db.SaveChanges()
'        'Dim myInsertQuery As String = "INSERT INTO HistoriqueMouvement (ClientId, CollecteurId, Montant, DateOperation, MontantRetenu, Pourcentage, EstTraiter, Etat, DateCreation, UserId, JournalCaisseId) VALUES (@ClientId, @CollecteurId, @Montant, @DateOperation, @MontantRetenu, @Pourcentage, @EstTraiter, @Etat, @DateCreation, @UserId, @JournalCaisseId)"
'        Dim myInsertQuery As String = "INSERT INTO HistoriqueMouvement (ClientId, CollecteurId, Montant, DateOperation, MontantRetenu, Pourcentage, EstTraiter, Etat, DateCreation, UserId, JournalCaisseId, LibelleOperation) VALUES (@ClientId, @CollecteurId, @Montant, @DateOperation, @MontantRetenu, @Pourcentage, @EstTraiter, @Etat, @DateCreation, @UserId, @JournalCaisseId, @LibelleOperation)"
'        'Dim laDate As Date = Now
'        If (db.Database.ExecuteSqlCommand(myInsertQuery, parameters)) Then
'            Dim historiquesMouvements = (From h In db.HistoriqueMouvements Where (h.UserId = UserId) Select HistoriqueId = h.Id, JournalCaisseId = h.JournalCaisseId,
'                                        IdClient = h.ClientId, NomClient = h.Client.Nom, PrenomClient = h.Client.Prenom, IdCollecteur = h.CollecteurId, NomCollecteur = h.Collecteur.Nom, PrenomCollecteur = h.Collecteur.Prenom, MontantCollecte = h.Montant,
'                                        FraisFixes = h.MontantRetenu, Taux = h.Pourcentage, h.DateOperation, h.LibelleOperation).ToList

'            Dim historique = historiquesMouvements.ElementAtOrDefault((historiquesMouvements.Count - 1))

'            'db.SaveChanges()

'            Return RedirectToAction("Index", "HistoriqueMouvement", New With {CollecteurId, entityVM.DateDebut, entityVM.DateFin})

'            'Return Ok(historique)
'        Else
'            ModelState.AddModelError("Motif", "Une erreur est survenue pendant l'exécution de la requête: veuillez contacter l'administrateur. ")
'            Return View(entityVM)
'        End If
'    Catch ex As DbEntityValidationException
'        Util.GetError(ex)
'        ModelState.AddModelError("Motif", "Une erreur est survenue pendant le traitement: veuillez contacter l'administrateur.")
'        Return View(entityVM)
'    Catch ex As Exception
'        Util.GetError(ex)
'        ModelState.AddModelError("Motif", "Une erreur est survenue pendant le traitement: veuillez contacter l'administrateur. ")
'        Return View(entityVM)
'    End Try

'    Return View(entityVM)
'End Function
