Imports System.Data.Entity
Imports System.Data.Entity.Validation
Imports System.Data.SqlClient
Imports System.Net
Imports Microsoft.AspNet.Identity
Imports PagedList

Namespace Controllers
    Public Class RetraitController
        Inherits BaseController

        Private db As New ApplicationDbContext

        Private Function GetCurrentUser() As ApplicationUser
            Dim id = User.Identity.GetUserId
            Dim aspuser = db.Users.Find(id)
            Return aspuser
        End Function

        <HttpPost()>
        Public Function GetMessage(ClientId As String) As ActionResult
            Dim results = (From client In db.Clients Where client.Id = ClientId Select client.MessageAlerte).FirstOrDefault()
            Return Json(results, JsonRequestBehavior.AllowGet)
        End Function



        'Private Function ConvertDate(dateConvert As Date) As String
        '    Dim mydate() = dateConvert.ToString("").Split(" ")
        '    Dim time = mydate(1)
        '    Dim tempoDateTable() = mydate(0).Split("/")
        '    'Dim day = dateConvert.Day
        '    'Dim month = dateConvert.Month
        '    'Dim year = dateConvert.Year

        '    Dim jour = tempoDateTable(0)
        '    Dim mois = tempoDateTable(1)
        '    Dim annee = tempoDateTable(2)
        '    'Dim resultDate = annee & "-" & mois & "-" + jour & " " & mydate(1)
        '    Dim resultDate = annee & "-" & mois & "-" + jour
        '    Return resultDate
        'End Function

        ' GET: Retrait
        <LocalizedAuthorize(Roles:="SA,ADMINISTRATEUR,CHEFCOLLECTEUR")>
        Function IndexAdmin(sortOrder As String, currentFilter As String, searchString As String, page As Integer?) As ActionResult
            ViewBag.CurrentSort = sortOrder

            If Not String.IsNullOrEmpty(searchString) Then
                page = 1
            Else
                searchString = currentFilter
            End If

            ViewBag.CurrentFilter = searchString
            'Dim entities = db.Retraits.ToList.Where(Function(m) m.Etat = True)
            Dim CurrentAgenceId = GetCurrentUser.Personne.AgenceId
            Dim entities = (From retrait In db.Retraits Where retrait.Client.AgenceId = CurrentAgenceId Select retrait).ToList

            If Not String.IsNullOrEmpty(searchString) Then
                entities = entities.Where(Function(e) e.Client.Nom.ToUpper.Equals(searchString.ToUpper) Or e.Client.Prenom.ToUpper.Equals(searchString.ToUpper))
            End If

            ViewBag.EnregCount = entities.Count

            Dim pageSize As Integer = ConfigurationManager.AppSettings("PageSize")
            Dim pageNumber As Integer = If(page, 1)

            Return View(entities.ToPagedList(pageNumber, pageSize))
        End Function

        <LocalizedAuthorize(Roles:="SA,ADMINISTRATEUR,CHEFCOLLECTEUR")>
        Function Index(sortOrder As String, currentFilter As String, searchString As String, page As Integer?) As ActionResult
            ViewBag.CurrentSort = sortOrder

            If Not String.IsNullOrEmpty(searchString) Then
                page = 1
            Else
                searchString = currentFilter
            End If

            ViewBag.CurrentFilter = searchString
            'Dim entities = db.Retraits.ToList.Where(Function(m) m.Etat = True)
            Dim entities = db.Retraits.ToList

            ViewBag.EnregCount = entities.Count

            Dim pageSize As Integer = ConfigurationManager.AppSettings("PageSize")
            Dim pageNumber As Integer = If(page, 1)

            Return View(entities.ToPagedList(pageNumber, pageSize))
        End Function

        ' GET: Retrait/Details/5
        <LocalizedAuthorize(Roles:="SA,ADMINISTRATEUR,CHEFCOLLECTEUR")>
        Function Details(ByVal id As Long?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim retrait As Retrait = db.Retraits.Find(id)
            If IsNothing(retrait) Then
                Return HttpNotFound()
            End If
            Return View(retrait)
        End Function



        Public Sub LoadCombo(pVM As RetraitViewModel)
            Dim listcollecteur = db.Collecteurs.OfType(Of Collecteur)().ToList
            Dim listcollecteur1 As New List(Of SelectListItem)


            Dim listclient = db.Clients.OfType(Of Client)().ToList
            Dim listclient1 As New List(Of SelectListItem)

            Dim CurrentUser = GetCurrentUser()

            If User.IsInRole("CHEFCOLLECTEUR") Then
                listcollecteur = listcollecteur.Where(Function(e) e.AgenceId = CurrentUser.Personne.AgenceId).ToList()
                listclient = listclient.Where(Function(e) e.AgenceId = CurrentUser.Personne.AgenceId).ToList()
            End If

            For Each item In listcollecteur
                listcollecteur1.Add(New SelectListItem With {.Value = item.Id, .Text = item.Nom.ToUpper & " :-- [" & item.Prenom.ToUpper & "]"})
            Next

            'For Each item In listclient
            '    listclient1.Add(New SelectListItem With {.Value = item.Id, .Text = item.Nom.ToUpper & " :-- " & item.Prenom.ToUpper & "[Portefeuille: " & item.PorteFeuille.Libelle.ToUpper & "]" & " :-- " & " [Solde Dispo: " & item.SoldeDisponible & "]"})
            'Next

            For Each item In listclient
                Dim PorteFeuilleLibelle As String = "AUCUN PORTEFEUILLE"
                If (Not IsNothing(item.PorteFeuille)) Then
                    PorteFeuilleLibelle = item.PorteFeuille.Libelle.ToUpper
                End If

                If (String.IsNullOrEmpty(item.Prenom)) Then
                    listclient1.Add(New SelectListItem With {.Value = item.Id, .Text = item.Nom.ToUpper & " :-- " & "[Portefeuille: " & PorteFeuilleLibelle.ToUpper & "]" & " :-- " & " [Solde Dispo: " & item.SoldeDisponible & "]"})
                Else
                    listclient1.Add(New SelectListItem With {.Value = item.Id, .Text = item.Nom.ToUpper & " " & item.Prenom.ToUpper & "[Portefeuille: " & PorteFeuilleLibelle.ToUpper & "]" & " :-- " & " [Solde Dispo: " & item.SoldeDisponible & "]"})
                End If
            Next

            pVM.IDsCollecteur = listcollecteur1
            pVM.IDsClient = listclient1
        End Sub

        'Get
        Function Annulation(ByVal id As Long?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim retrait As Retrait = db.Retraits.Find(id)
            If IsNothing(retrait) Then
                Return HttpNotFound()
            End If

            Dim entityVM As New AnnulationViewModel With {
                .Id = retrait.Id
            }

            Return View(entityVM)
        End Function


        ' GET: Retrait/Create
        <LocalizedAuthorize(Roles:="SA,ADMINISTRATEUR,CHEFCOLLECTEUR")>
        Function Create() As ActionResult
            Dim pVM As New RetraitViewModel

            LoadCombo(pVM)
            Return View(pVM)
        End Function


        <HttpPost()>
        <LocalizedAuthorize(Roles:="SA,ADMINISTRATEUR,CHEFCOLLECTEUR")>
        <ValidateAntiForgeryToken()>
        Function Annulation(entityVM As AnnulationViewModel) As ActionResult
            'on recupere l'id du collecteur chef collect connecter
            Dim RetraitId = entityVM.Id
            Dim Motif = entityVM.Motif

            If IsNothing(RetraitId) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim retrait As Retrait = db.Retraits.Find(RetraitId)
            If IsNothing(retrait) Then
                Return HttpNotFound()
            End If

            Dim DateRetrait = retrait.DateRetrait
            Dim clientId = retrait.ClientId
            Dim Montant = -(retrait.Montant)
            ' Dim DateRetraitCovert = ConvertDate(DateRetrait)
            ' DateHier = ConvertDate(DateHier)
            Dim LesHistoMvt = (From H In db.HistoriqueMouvements Where H.ClientId = clientId And H.Montant = Montant Select H).ToList
            If (LesHistoMvt.Count = 0) Then
                'on n'a pas trouver l'historique correcpondantes
                ModelState.AddModelError("Motif", "Impossible de retrouver l'historique du mouvement...")
                Return View(entityVM)
            End If
            Dim HistMvt = LesHistoMvt.FirstOrDefault

            retrait.Etat = False
            db.Entry(retrait).State = EntityState.Modified
            db.SaveChanges()

            Dim Annul As New Annulation With {
                .DateAnnulation = Now,
                .Motif = Motif,
                .HistoriqueMouvementId = HistMvt.Id
            }

            db.Annulation.Add(Annul)
            db.SaveChanges()

            Dim client = db.Clients.Find(clientId)
            Dim UserId = getCurrentUser.Id
            If IsNothing(client) Then
                ModelState.AddModelError("Motif", "La transaction n'a pas été éffectuer: client introuvable...")
                Return View(entityVM)
            End If

            'mise a jour du solde du client
            client.Solde += Montant
            db.Entry(client).State = EntityState.Modified
            db.SaveChanges()


            'on testte si le collecter connecter a une caisse ouverte
            Dim CollecteurId = ConfigurationManager.AppSettings("CollecteurSystemeId")
            Dim LesJournalCaisse = (From J In db.JournalCaisses Where J.CollecteurId = CollecteurId Select J).ToArray
            If (LesJournalCaisse.Count = 0) Then
                ModelState.AddModelError("Motif", "Vous n'avez pas de caisse ouverte pour effectuer cette opération ")
                Return View(entityVM)
            End If

            '3- on recupere le journal caisse et on enregistre dans mouvement historique
            Dim JCID = LesJournalCaisse.FirstOrDefault.Id
            Dim LibOperation As String = "ANNULATION RETRAIT-" & RetraitId & "de " & Montant & "Du " & DateRetrait

            Dim parameterList As New List(Of SqlParameter) From {
                New SqlParameter("@ClientId", clientId),
                New SqlParameter("@CollecteurId", CollecteurId),
                New SqlParameter("@Montant", +Montant),
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
                'Dim myInsertQuery As String = "INSERT INTO HistoriqueMouvement (ClientId, CollecteurId, Montant, DateOperation, MontantRetenu, Pourcentage, EstTraiter, Etat, DateCreation, UserId, JournalCaisseId) VALUES (@ClientId, @CollecteurId, @Montant, @DateOperation, @MontantRetenu, @Pourcentage, @EstTraiter, @Etat, @DateCreation, @UserId, @JournalCaisseId)"
                Dim myInsertQuery As String = "INSERT INTO HistoriqueMouvement (ClientId, CollecteurId, Montant, DateOperation, MontantRetenu, Pourcentage, EstTraiter, Etat, DateCreation, UserId, JournalCaisseId, LibelleOperation) VALUES (@ClientId, @CollecteurId, @Montant, @DateOperation, @MontantRetenu, @Pourcentage, @EstTraiter, @Etat, @DateCreation, @UserId, @JournalCaisseId, @LibelleOperation)"
                'Dim laDate As Date = Now
                If (db.Database.ExecuteSqlCommand(myInsertQuery, parameters)) Then
                    Dim historiquesMouvements = (From h In db.HistoriqueMouvements Where (h.UserId = UserId) Select HistoriqueId = h.Id, JournalCaisseId = h.JournalCaisseId,
                                        IdClient = h.ClientId, NomClient = h.Client.Nom, PrenomClient = h.Client.Prenom, IdCollecteur = h.CollecteurId, NomCollecteur = h.Collecteur.Nom, PrenomCollecteur = h.Collecteur.Prenom, MontantCollecte = h.Montant,
                                        FraisFixes = h.MontantRetenu, Taux = h.Pourcentage, h.DateOperation, h.LibelleOperation).ToList

                    Dim historique = historiquesMouvements.ElementAtOrDefault((historiquesMouvements.Count - 1))
                    Return RedirectToAction("IndexAdmin")
                    'Return Ok(historique)
                Else
                    ModelState.AddModelError("Motif", "Une erreur est survenue pendant l'exécution de la requête: veuillez contacter l'administrateur. ")
                    Return View(entityVM)
                End If
            Catch ex As DbEntityValidationException
                Util.GetError(ex)
                ModelState.AddModelError("Motif", "Une erreur est survenue pendant le traitement: veuillez contacter l'administrateur.")
                Return View(entityVM)
            Catch ex As Exception
                Util.GetError(ex)
                ModelState.AddModelError("Motif", "Une erreur est survenue pendant le traitement: veuillez contacter l'administrateur. ")
                Return View(entityVM)
            End Try

            Return View(entityVM)
        End Function

        ' POST: Retrait/Create
        'Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        'plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        '<HttpPost()>
        '<LocalizedAuthorize(Roles:="SA,ADMINISTRATEUR,CHEFCOLLECTEUR")>
        '<ValidateAntiForgeryToken()>
        'Function Create(<Bind(Include:="Id,ClientId,Montant,SoldeApreOperation,DateRetrait,DateCloture,Etat,DateCreation")> ByVal retraitVM As RetraitViewModel) As ActionResult
        '    'on recupere l'id du collecteur chef collect connecter
        '    Dim CollecteurId = ConfigurationManager.AppSettings("CollecteurSystemeId") 'getCurrentUser.PersonneId
        '    retraitVM.CollecteurId = CollecteurId

        '    If ModelState.IsValid Then
        '        '1 on recupere le client on teste son solde et on le modifi

        '        Dim ClientId = retraitVM.ClientId
        '        Dim Montant = Math.Abs(retraitVM.Montant)
        '        Dim client = db.Clients.Find(ClientId)
        '        Dim UserId = GetCurrentUser.Id
        '        If IsNothing(client) Then
        '            ModelState.AddModelError("ClientId", "Le client Selectionner n'a pas de compte ")
        '            LoadCombo(retraitVM)
        '            Return View(retraitVM)
        '        End If

        '        'on teste si le collecter connecter a une caisse ouverte
        '        Dim LesJournalCaisse = (From J In db.JournalCaisses Where J.CollecteurId = CollecteurId Select J).ToArray
        '        If (LesJournalCaisse.Count = 0) Then
        '            ModelState.AddModelError("Montant", "Vous n'avez pas de caisse ouverte pour effectuer cette opération ")
        '            LoadCombo(retraitVM)
        '            Return View(retraitVM)
        '        End If

        '        If Not (client.SoldeDisponible - Montant >= 0) Then
        '            ModelState.AddModelError("Montant", "Le solde disponible du client est insuiffisant pour un retrait de " & Montant)
        '            LoadCombo(retraitVM)
        '            Return View(retraitVM)
        '        End If
        '        'mise a jour du solde du client
        '        client.Solde -= Montant
        '        db.Entry(client).State = EntityState.Modified
        '        db.SaveChanges()
        '        '2- on cree le retrait
        '        retraitVM.ClientId = ClientId
        '        retraitVM.CollecteurId = CollecteurId
        '        retraitVM.SoldeApreOperation = client.Solde
        '        retraitVM.DateRetrait = Now
        '        retraitVM.DateCreation = Now
        '        retraitVM.Montant = -Montant
        '        retraitVM.Etat = True
        '        Dim retrait = retraitVM.GetEntity
        '        retrait.UserId = UserId
        '        db.Retraits.Add(retrait)
        '        db.SaveChanges()
        '        '3- on recupere le journal caisse et on enregistre dans mouvement historique
        '        Dim JCID = LesJournalCaisse.FirstOrDefault.Id
        '        Dim LibOperation As String = "RETRAIT-" & DateTime.Now.ToString & "-AG-" & GetPositionAgence(GetCurrentUser.Personne.AgenceId, GetCurrentUser.Personne.Agence.SocieteId)

        '        Dim parameterList As New List(Of SqlParameter) From {
        '            New SqlParameter("@ClientId", ClientId),
        '            New SqlParameter("@CollecteurId", CollecteurId),
        '            New SqlParameter("@Montant", -Montant),
        '            New SqlParameter("@DateOperation", Now),
        '            New SqlParameter("@Pourcentage", 0),
        '            New SqlParameter("@MontantRetenu", 0),
        '            New SqlParameter("@EstTraiter", 0),
        '            New SqlParameter("@Etat", False),
        '            New SqlParameter("@DateCreation", Now),
        '            New SqlParameter("@UserId", UserId),
        '            New SqlParameter("@JournalCaisseId", JCID),
        '            New SqlParameter("@LibelleOperation", LibOperation)
        '        }
        '        Dim parameters As SqlParameter() = parameterList.ToArray()

        '        Try
        '            'Dim myInsertQuery As String = "INSERT INTO HistoriqueMouvement (ClientId, CollecteurId, Montant, DateOperation, MontantRetenu, Pourcentage, EstTraiter, Etat, DateCreation, UserId, JournalCaisseId) VALUES (@ClientId, @CollecteurId, @Montant, @DateOperation, @MontantRetenu, @Pourcentage, @EstTraiter, @Etat, @DateCreation, @UserId, @JournalCaisseId)"
        '            Dim myInsertQuery As String = "INSERT INTO HistoriqueMouvement (ClientId, CollecteurId, Montant, DateOperation, MontantRetenu, Pourcentage, EstTraiter, Etat, DateCreation, UserId, JournalCaisseId, LibelleOperation) VALUES (@ClientId, @CollecteurId, @Montant, @DateOperation, @MontantRetenu, @Pourcentage, @EstTraiter, @Etat, @DateCreation, @UserId, @JournalCaisseId, @LibelleOperation)"
        '            'Dim laDate As Date = Now
        '            If (db.Database.ExecuteSqlCommand(myInsertQuery, parameters)) Then
        '                Dim historiquesMouvements = (From h In db.HistoriqueMouvements Where (h.UserId = UserId) Select HistoriqueId = h.Id, JournalCaisseId = h.JournalCaisseId,
        '                                IdClient = h.ClientId, NomClient = h.Client.Nom, PrenomClient = h.Client.Prenom, IdCollecteur = h.CollecteurId, NomCollecteur = h.Collecteur.Nom, PrenomCollecteur = h.Collecteur.Prenom, MontantCollecte = h.Montant,
        '                                FraisFixes = h.MontantRetenu, Taux = h.Pourcentage, h.DateOperation, h.LibelleOperation).ToList

        '                Dim historique = historiquesMouvements.ElementAtOrDefault((historiquesMouvements.Count - 1))
        '                Return RedirectToAction("Index")
        '                'Return Ok(historique)
        '            Else
        '                ModelState.AddModelError("Montant", "Une erreur est survenue pendant l'exécution de la requête: veuillez contacter l'administrateur. ")
        '                LoadCombo(retraitVM)
        '                Return View(retraitVM)
        '            End If
        '        Catch ex As DbEntityValidationException
        '            Util.GetError(ex)
        '            ModelState.AddModelError("Montant", "Une erreur est survenue pendant le traitement: veuillez contacter l'administrateur.")
        '            LoadCombo(retraitVM)
        '            Return View(retraitVM)
        '        Catch ex As Exception
        '            Util.GetError(ex)
        '            ModelState.AddModelError("Montant", "Une erreur est survenue pendant le traitement: veuillez contacter l'administrateur. ")
        '            LoadCombo(retraitVM)
        '            Return View(retraitVM)
        '        End Try
        '    End If
        '    LoadCombo(retraitVM)
        '    Return View(retraitVM)
        'End Function


        ' POST: Retrait/Create
        'Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        'plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        <HttpPost()>
        <LocalizedAuthorize(Roles:="SA,ADMINISTRATEUR,CHEFCOLLECTEUR")>
        <ValidateAntiForgeryToken()>
        Function DemandeDeRetrait(retraitJSON As RetraitJSON) As JsonResult
            Dim retraitVM As New RetraitViewModel(retraitJSON:=retraitJSON)
            'on recupere l'id du collecteur chef collect connecter
            Dim CollecteurId = ConfigurationManager.AppSettings("CollecteurSystemeId") 'getCurrentUser.PersonneId
            retraitVM.CollecteurId = CollecteurId

            If (retraitVM.Montant <= 0) Then
                ModelState.AddModelError("Montant", Resource.MontantInvalid)
            End If

            If ModelState.IsValid Then
                '1 on recupere le client on teste son solde et on le modifi

                Dim ClientId = retraitVM.ClientId
                Dim Montant = Math.Abs(retraitVM.Montant)
                Dim client = db.Clients.Find(ClientId)
                Dim UserId = GetCurrentUser.Id
                If IsNothing(client) Then
                    ModelState.AddModelError("ClientId", "Le client Selectionné n'a pas de compte ")
                    LoadCombo(retraitVM)
                    Return Json(New With {.Result = "Error"})
                End If

                'on teste si le collecter connecter a une caisse ouverte
                Dim LesJournalCaisse = (From J In db.JournalCaisses Where J.CollecteurId = CollecteurId Select J).ToArray
                If (LesJournalCaisse.Count = 0) Then
                    ModelState.AddModelError("Montant", "Vous n'avez pas de caisse ouverte pour effectuer cette opération ")
                    LoadCombo(retraitVM)
                    Return Json(New With {.Result = "Error"})
                End If

                If Not (client.SoldeDisponible - Montant >= 0) Then
                    ModelState.AddModelError("Montant", "Le solde disponible du client est insuiffisant pour un retrait de " & Montant)
                    LoadCombo(retraitVM)
                    Return Json(New With {.Result = "Error"})
                End If
                'mise a jour du solde du client
                client.Solde -= Montant
                db.Entry(client).State = EntityState.Modified
                db.SaveChanges()
                '2- on cree le retrait
                retraitVM.ClientId = ClientId
                retraitVM.CollecteurId = CollecteurId
                retraitVM.SoldeApreOperation = client.Solde
                retraitVM.DateRetrait = Now
                retraitVM.DateCreation = Now
                retraitVM.Montant = -Montant
                retraitVM.Etat = True
                Dim retrait = retraitVM.GetEntity
                retrait.UserId = UserId
                db.Retraits.Add(retrait)
                db.SaveChanges()
                '3- on recupere le journal caisse et on enregistre dans mouvement historique
                Dim JCID = LesJournalCaisse.FirstOrDefault.Id
                Dim LibOperation As String = "RETRAIT-" & DateTime.Now.ToString & "-AG-" & GetPositionAgence(GetCurrentUser.Personne.AgenceId, GetCurrentUser.Personne.Agence.SocieteId)

                Dim parameterList As New List(Of SqlParameter) From {
                    New SqlParameter("@ClientId", ClientId),
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
                    'Dim myInsertQuery As String = "INSERT INTO HistoriqueMouvement (ClientId, CollecteurId, Montant, DateOperation, MontantRetenu, Pourcentage, EstTraiter, Etat, DateCreation, UserId, JournalCaisseId) VALUES (@ClientId, @CollecteurId, @Montant, @DateOperation, @MontantRetenu, @Pourcentage, @EstTraiter, @Etat, @DateCreation, @UserId, @JournalCaisseId)"
                    Dim myInsertQuery As String = "INSERT INTO HistoriqueMouvement (ClientId, CollecteurId, Montant, DateOperation, MontantRetenu, Pourcentage, EstTraiter, Etat, DateCreation, UserId, JournalCaisseId, LibelleOperation) VALUES (@ClientId, @CollecteurId, @Montant, @DateOperation, @MontantRetenu, @Pourcentage, @EstTraiter, @Etat, @DateCreation, @UserId, @JournalCaisseId, @LibelleOperation)"
                    'Dim laDate As Date = Now
                    If (db.Database.ExecuteSqlCommand(myInsertQuery, parameters)) Then
                        Dim historiquesMouvements = (From h In db.HistoriqueMouvements Where (h.UserId = UserId) Select HistoriqueId = h.Id, JournalCaisseId = h.JournalCaisseId,
                                        IdClient = h.ClientId, NomClient = h.Client.Nom, PrenomClient = h.Client.Prenom, IdCollecteur = h.CollecteurId, NomCollecteur = h.Collecteur.Nom, PrenomCollecteur = h.Collecteur.Prenom, MontantCollecte = h.Montant,
                                        FraisFixes = h.MontantRetenu, Taux = h.Pourcentage, h.DateOperation, h.LibelleOperation).ToList

                        Dim historique = historiquesMouvements.ElementAtOrDefault((historiquesMouvements.Count - 1))
                        Return Json(New With {.Result = "OK"})
                        'Return Ok(historique)
                    Else
                        ModelState.AddModelError("Montant", "Une erreur est survenue pendant l'exécution de la requête: veuillez contacter l'administrateur. ")
                        LoadCombo(retraitVM)
                        Return Json(New With {.Result = "Error: Une erreur est survenue pendant le traitement: veuillez contacter l'administrateur. "})
                    End If
                Catch ex As DbEntityValidationException
                    Util.GetError(ex)
                    ModelState.AddModelError("Montant", "Une erreur est survenue pendant le traitement: veuillez contacter l'administrateur.")
                    LoadCombo(retraitVM)
                    Return Json(New With {.Result = "Error: Une erreur est survenue pendant le traitement: veuillez contacter l'administrateur. "})
                Catch ex As Exception
                    Util.GetError(ex)
                    ModelState.AddModelError("Montant", "Une erreur est survenue pendant le traitement: veuillez contacter l'administrateur. ")
                    LoadCombo(retraitVM)
                    Return Json(New With {.Result = "Error: Une erreur est survenue pendant le traitement: veuillez contacter l'administrateur. "})
                End Try
            End If
            LoadCombo(retraitVM)
            Return Json(New With {.Result = "Error"})
        End Function


        ' GET: Retrait/Edit/5
        <LocalizedAuthorize(Roles:="SA,ADMINISTRATEUR,CHEFCOLLECTEUR")>
        Function Edit(ByVal id As Long?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim retrait As Retrait = db.Retraits.Find(id)
            If IsNothing(retrait) Then
                Return HttpNotFound()
            End If

            Dim pVM As New RetraitViewModel

            Dim listcollecteur = db.Collecteurs.OfType(Of Collecteur)().ToList
            Dim listcollecteur1 As New List(Of SelectListItem)
            For Each item In listcollecteur
                listcollecteur1.Add(New SelectListItem With {.Value = item.Id, .Text = item.Nom & ":-- [" & item.Nom & "] --"})
            Next

            pVM.IDsCollecteur = listcollecteur1
            Return View(pVM)
        End Function

        ' POST: Retrait/Edit/5
        'Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        'plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        <LocalizedAuthorize(Roles:="SA,ADMINISTRATEUR,CHEFCOLLECTEUR")>
        Function Edit(<Bind(Include:="Id,CollecteurId,Montant,SoldeApreOperation,DateRetrait,DateCloture,Etat,DateCreation")> ByVal retrait As RetraitViewModel) As ActionResult
            If ModelState.IsValid Then
                Dim ca = retrait.GetEntity
                db.Entry(retrait).State = EntityState.Modified
                db.SaveChanges()
                Return RedirectToAction("Index")
            End If
            Return View(retrait)
        End Function

        ' GET: Retrait/Delete/5
        <LocalizedAuthorize(Roles:="SA,ADMINISTRATEUR,CHEFCOLLECTEUR")>
        Function Delete(ByVal id As Long?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim retrait As Retrait = db.Retraits.Find(id)
            If IsNothing(retrait) Then
                Return HttpNotFound()
            End If
            Return View(retrait)
        End Function

        ' POST: Retrait/Delete/5
        <HttpPost()>
        <ActionName("Delete")>
        <ValidateAntiForgeryToken()>
        <LocalizedAuthorize(Roles:="SA,ADMINISTRATEUR,CHEFCOLLECTEUR")>
        Function DeleteConfirmed(ByVal id As Long) As ActionResult
            Dim retrait As Retrait = db.Retraits.Find(id)
            db.Retraits.Remove(retrait)
            db.SaveChanges()
            Return RedirectToAction("Index")
        End Function

        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If (disposing) Then
                db.Dispose()
            End If
            MyBase.Dispose(disposing)
        End Sub

        ''' <summary>
        ''' Fonction permettant de déterminer la position d'un agence dans un groupe d'agence appartenant à une même société
        ''' </summary>
        ''' <param name="agenceId"></param>
        ''' <param name="societeId"></param>
        ''' <returns>String (chaîne de caractères)</returns>
        ''' <remarks></remarks>
        Private Function GetPositionAgence(ByVal agenceId As Long, ByVal societeId As String) As String
            Dim agences = (From ag In db.Agences Where ag.SocieteId = societeId Select ag).ToList
            Dim position = 0
            For Each agenc In agences
                position += 1
                If agenc.Id = agenceId Then
                    Exit For
                End If
            Next
            Dim positionString As String = position
            While positionString.Length < 3
                positionString = "0" & positionString
            End While
            Return positionString
        End Function
    End Class

    Public Class RetraitJSON
        Private _clientId As Long
        Public Property ClientId As Long
            Get
                Return _clientId
            End Get
            Set(value As Long)
                _clientId = value
            End Set
        End Property

        Private _montant As String
        Public Property Montant As String
            Get
                Return _montant
            End Get
            Set(value As String)
                _montant = value
            End Set
        End Property


    End Class
End Namespace
