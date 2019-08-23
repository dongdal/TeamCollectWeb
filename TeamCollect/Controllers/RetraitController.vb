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
            For Each item In listcollecteur
                listcollecteur1.Add(New SelectListItem With {.Value = item.Id, .Text = item.Nom & ":-- [" & item.Nom & "] --"})
            Next

            Dim listclient = db.Clients.OfType(Of Client)().ToList
            Dim listclient1 As New List(Of SelectListItem)
            For Each item In listclient
                listclient1.Add(New SelectListItem With {.Value = item.Id, .Text = item.Nom & ":-- " & item.Prenom & " [Solde Dispo: " & item.SoldeDisponible & "]"})
            Next

            'For Each item In listclient
            '    If (String.IsNullOrEmpty(item.PorteFeuille.Libelle)) Then
            '        listclient1.Add(New SelectListItem With {.Value = item.Id, .Text = item.Nom & ":-- " & item.Prenom & " [Solde: " & item.Solde & "] "})
            '    Else
            '        listclient1.Add(New SelectListItem With {.Value = item.Id, .Text = item.Nom & ":-- " & item.Prenom & " [Solde: " & item.Solde & "] - " & item.PorteFeuille.Libelle})
            '    End If
            'Next

            pVM.IDsCollecteur = listcollecteur1
            pVM.IDsClient = listclient1
        End Sub

        ' GET: Retrait/Create
        <LocalizedAuthorize(Roles:="SA,ADMINISTRATEUR,CHEFCOLLECTEUR")>
        Function Create() As ActionResult
            Dim pVM As New RetraitViewModel

            LoadCombo(pVM)
            Return View(pVM)
        End Function

        'Get
        Function Annulation(ByVal id As Long?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim retrait As Retrait = db.Retraits.Find(id)
            If IsNothing(retrait) Then
                Return HttpNotFound()
            End If
            Dim entityVM As New AnnulationViewModel
            entityVM.Id = retrait.Id

            Return View(entityVM)
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

            Dim Annul As New Annulation
            Annul.DateAnnulation = Now
            Annul.Motif = Motif
            Annul.HistoriqueMouvementId = HistMvt.Id

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

            Dim parameterList As New List(Of SqlParameter)()
            parameterList.Add(New SqlParameter("@ClientId", clientId))
            parameterList.Add(New SqlParameter("@CollecteurId", CollecteurId))
            parameterList.Add(New SqlParameter("@Montant", +Montant))
            parameterList.Add(New SqlParameter("@DateOperation", Now))
            parameterList.Add(New SqlParameter("@Pourcentage", 0))
            parameterList.Add(New SqlParameter("@MontantRetenu", 0))
            parameterList.Add(New SqlParameter("@EstTraiter", 0))
            parameterList.Add(New SqlParameter("@Etat", False))
            parameterList.Add(New SqlParameter("@DateCreation", Now))
            parameterList.Add(New SqlParameter("@UserId", UserId))
            parameterList.Add(New SqlParameter("@JournalCaisseId", JCID))
            parameterList.Add(New SqlParameter("@LibelleOperation", LibOperation))
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
        <HttpPost()>
        <LocalizedAuthorize(Roles:="SA,ADMINISTRATEUR,CHEFCOLLECTEUR")>
        <ValidateAntiForgeryToken()>
        Function Create(<Bind(Include:="Id,ClientId,Montant,SoldeApreOperation,DateRetrait,DateCloture,Etat,DateCreation")> ByVal retraitVM As RetraitViewModel) As ActionResult
            'on recupere l'id du collecteur chef collect connecter
            Dim CollecteurId = ConfigurationManager.AppSettings("CollecteurSystemeId") 'getCurrentUser.PersonneId
            retraitVM.CollecteurId = CollecteurId

            If ModelState.IsValid Then
                '1 on recupere le client on teste son solde et on le modifi

                Dim ClientId = retraitVM.ClientId
                Dim Montant = Math.Abs(retraitVM.Montant)
                Dim client = db.Clients.Find(ClientId)
                Dim UserId = getCurrentUser.Id
                If IsNothing(client) Then
                    ModelState.AddModelError("ClientId", "Le client Selectionner n'a pas de compte ")
                    LoadCombo(retraitVM)
                    Return View(retraitVM)
                End If

                'on teste si le collecter connecter a une caisse ouverte
                Dim LesJournalCaisse = (From J In db.JournalCaisses Where J.CollecteurId = CollecteurId Select J).ToArray
                If (LesJournalCaisse.Count = 0) Then
                    ModelState.AddModelError("Montant", "Vous n'avez pas de caisse ouverte pour effectuer cette opération ")
                    LoadCombo(retraitVM)
                    Return View(retraitVM)
                End If

                If Not (client.SoldeDisponible - Montant >= 0) Then
                    ModelState.AddModelError("Montant", "Le solde disponible du client est insuiffisant pour un retrait de " & Montant)
                    LoadCombo(retraitVM)
                    Return View(retraitVM)
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
                Dim retrait = retraitVM.getEntity
                retrait.UserId = UserId
                db.Retraits.Add(retrait)
                db.SaveChanges()
                '3- on recupere le journal caisse et on enregistre dans mouvement historique
                Dim JCID = LesJournalCaisse.FirstOrDefault.Id
                Dim LibOperation As String = "RETRAIT-" & DateTime.Now.ToString & "-AG-" & GetPositionAgence(getCurrentUser.Personne.AgenceId, getCurrentUser.Personne.Agence.SocieteId)

                Dim parameterList As New List(Of SqlParameter)()
                parameterList.Add(New SqlParameter("@ClientId", ClientId))
                parameterList.Add(New SqlParameter("@CollecteurId", CollecteurId))
                parameterList.Add(New SqlParameter("@Montant", -Montant))
                parameterList.Add(New SqlParameter("@DateOperation", Now))
                parameterList.Add(New SqlParameter("@Pourcentage", 0))
                parameterList.Add(New SqlParameter("@MontantRetenu", 0))
                parameterList.Add(New SqlParameter("@EstTraiter", 0))
                parameterList.Add(New SqlParameter("@Etat", False))
                parameterList.Add(New SqlParameter("@DateCreation", Now))
                parameterList.Add(New SqlParameter("@UserId", UserId))
                parameterList.Add(New SqlParameter("@JournalCaisseId", JCID))
                parameterList.Add(New SqlParameter("@LibelleOperation", LibOperation))
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
                        Return RedirectToAction("Index")
                        'Return Ok(historique)
                    Else
                        ModelState.AddModelError("Montant", "Une erreur est survenue pendant l'exécution de la requête: veuillez contacter l'administrateur. ")
                        LoadCombo(retraitVM)
                        Return View(retraitVM)
                    End If
                Catch ex As DbEntityValidationException
                    Util.GetError(ex)
                    ModelState.AddModelError("Montant", "Une erreur est survenue pendant le traitement: veuillez contacter l'administrateur.")
                    LoadCombo(retraitVM)
                    Return View(retraitVM)
                Catch ex As Exception
                    Util.GetError(ex)
                    ModelState.AddModelError("Montant", "Une erreur est survenue pendant le traitement: veuillez contacter l'administrateur. ")
                    LoadCombo(retraitVM)
                    Return View(retraitVM)
                End Try
            End If
            LoadCombo(retraitVM)
            Return View(retraitVM)
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
                Dim ca = retrait.getEntity
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
End Namespace
