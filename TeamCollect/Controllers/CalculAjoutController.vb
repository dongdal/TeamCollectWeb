Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.Entity
Imports System.Data.Entity.Validation
Imports System.Data.SqlClient
Imports System.Linq
Imports System.Net
Imports System.Web
Imports System.Web.Mvc
Imports Microsoft.AspNet.Identity
Imports PagedList
Imports TeamCollect

Namespace Controllers
    Public Class CalculAjoutController
        Inherits BaseController

        Private db As New ApplicationDbContext

        Private Function getCurrentUser() As ApplicationUser
            Dim id = User.Identity.GetUserId
            Dim aspuser = db.Users.Find(id)
            Return aspuser
        End Function

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


        <LocalizedAuthorize(Roles:="SA,ADMINISTRATEUR,CHEFCOLLECTEUR")>
        Function IndexCommission(sortOrder As String, currentFilter As String, searchString As String, page As Integer?) As ActionResult
            ViewBag.CurrentSort = sortOrder

            If Not String.IsNullOrEmpty(searchString) Then
                page = 1
            Else
                searchString = currentFilter
            End If

            ViewBag.CurrentFilter = searchString
            Dim AgenceUserConnected = getCurrentUser.Personne.AgenceId
            Dim entities = (From calcul In db.HistoriqueCalculCommission Where calcul.AgenceId = AgenceUserConnected).ToList
            ViewBag.EnregCount = entities.Count

            Dim pageSize As Integer = ConfigurationManager.AppSettings("PageSize")
            Dim pageNumber As Integer = If(page, 1)

            Return View(entities.ToPagedList(pageNumber, pageSize))
        End Function

        ' GET: CarnetClient
        Function Index(sortOrder As String, currentFilter As String, searchString As String, page As Integer?) As ActionResult
            ViewBag.CurrentSort = sortOrder

            If Not String.IsNullOrEmpty(searchString) Then
                page = 1
            Else
                searchString = currentFilter
            End If

            ViewBag.CurrentFilter = searchString
            Dim AgenceUserConnected = getCurrentUser.Personne.AgenceId
            Dim entities = (From calcul In db.HistoriqueCalculAjout Where calcul.AgenceId = AgenceUserConnected).ToList
            ViewBag.EnregCount = entities.Count

            Dim pageSize As Integer = ConfigurationManager.AppSettings("PageSize")
            Dim pageNumber As Integer = If(page, 1)

            Return View(entities.ToPagedList(pageNumber, pageSize))
        End Function

        ' GET: CarnetClient/Details/5
        Function Details(ByVal id As Long?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim carnetClient As CarnetClient = db.CarnetClients.Find(id)
            If IsNothing(carnetClient) Then
                Return HttpNotFound()
            End If
            Return View(carnetClient)
        End Function

        Public Sub LoadCombo(VM As ChoixParamAjoutViewModel)

            Dim ListeAnnee As New List(Of SelectListItem)
            Dim Listemois As New List(Of SelectListItem)

            For i As Integer = 1 To 12
                Dim li As New SelectListItem
                li.Value = i
                li.Text = i
                Listemois.Add(li)
            Next


            For i As Integer = Now.Year To Now.Year + 7
                Dim li As New SelectListItem
                li.Value = i
                li.Text = i
                ListeAnnee.Add(li)
            Next

            VM.ListeMois = Listemois
            VM.ListeAnnee = ListeAnnee
        End Sub


        ' GET: CarnetClient/Create
        <LocalizedAuthorize(Roles:="SA,ADMINISTRATEUR,CHEFCOLLECTEUR")>
        Function CreateByProc() As ActionResult
            Dim entityVM As New ChoixParamAjoutViewModel
            LoadCombo(entityVM)
            Return View(entityVM)
        End Function

        ' POST: CarnetClient/Create
        'Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        'plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        <LocalizedAuthorize(Roles:="SA,ADMINISTRATEUR,CHEFCOLLECTEUR")>
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function CreateByProc(ByVal entityVM As ChoixParamAjoutViewModel) As ActionResult
            entityVM.UserId = getCurrentUser.Id
            If ModelState.IsValid Then
                Dim mois = entityVM.Mois : Dim annee = entityVM.Annee
                Dim AgenceId = getCurrentUser.Personne.AgenceId
                Dim CollecteurId = ConfigurationManager.AppSettings("CollecteurSystemeId")   'CollectMois.FirstOrDefault.CollecteurId
                Dim CollecteurSystemId = ConfigurationManager.AppSettings("CollecteurSystemeId")
                Try
                    Dim Con As New SqlConnection(GetConnectionString)
                    Con.Open()

                    Dim Cmd As New SqlCommand
                    Cmd.Connection = Con
                    Cmd.CommandType = CommandType.StoredProcedure
                    Cmd.CommandText = "CalculAgios"
                    Cmd.Parameters.AddWithValue("@Mois", mois)
                    Cmd.Parameters.AddWithValue("@Annee", annee)
                    Cmd.Parameters.AddWithValue("@CollecteurId", CollecteurId)
                    Cmd.Parameters.AddWithValue("@UserId", entityVM.UserId)
                    Cmd.Parameters.AddWithValue("@CollecteurSystemId", CollecteurSystemId)
                    Cmd.Parameters.AddWithValue("@AgenceId", AgenceId)

                    Try
                        'Cmd.ExecuteNonQueryAsync()
                        Dim result As Object = Cmd.ExecuteScalar()
                        If Not (IsNothing(result)) Then
                            ModelState.AddModelError("Mois", result.ToString)
                            LoadCombo(entityVM)
                            Return View(entityVM)
                        End If

                        'If (result.ToString <> "") Then
                        '    ModelState.AddModelError("Mois", result.ToString)
                        '    LoadCombo(entityVM)
                        '    Return View(entityVM)
                        'End If
                        Cmd.Dispose()
                        Con.Dispose()
                        Return RedirectToAction("Index")
                    Catch ex As SqlException
                        ModelState.AddModelError("Mois", ex.Message)
                        LoadCombo(entityVM)
                        Return View(entityVM)
                    End Try
                Catch ex As Exception
                    ModelState.AddModelError("Mois", ex.Message)
                    LoadCombo(entityVM)
                    Return View(entityVM)
                End Try
            End If

            LoadCombo(entityVM)
            Return View(entityVM)
        End Function


        <LocalizedAuthorize(Roles:="SA,ADMINISTRATEUR,CHEFCOLLECTEUR")>
        Function CalculCommissionByProc() As ActionResult
            Dim entityVM As New ChoixParamAjoutViewModel
            LoadCombo(entityVM)
            Return View(entityVM)
        End Function

        ' POST: CarnetClient/Create
        'Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        'plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        <LocalizedAuthorize(Roles:="SA,ADMINISTRATEUR,CHEFCOLLECTEUR")>
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function CalculCommissionByProc(ByVal entityVM As ChoixParamAjoutViewModel) As ActionResult
            entityVM.UserId = getCurrentUser.Id
            If ModelState.IsValid Then
                Dim mois = entityVM.Mois : Dim annee = entityVM.Annee
                Dim AgenceId = getCurrentUser.Personne.AgenceId
                'Dim CollecteurId = ConfigurationManager.AppSettings("CollecteurSystemeId")   'CollectMois.FirstOrDefault.CollecteurId
                Dim CollecteurSystemId = ConfigurationManager.AppSettings("CollecteurSystemeId")
                Try
                    Dim Con As New SqlConnection(GetConnectionString)
                    Con.Open()

                    Dim Cmd As New SqlCommand
                    Cmd.Connection = Con
                    Cmd.CommandType = CommandType.StoredProcedure
                    Cmd.CommandText = "CalculCommission"
                    Cmd.Parameters.AddWithValue("@Mois", mois)
                    Cmd.Parameters.AddWithValue("@Annee", annee)
                    Cmd.Parameters.AddWithValue("@UserId", entityVM.UserId)
                    Cmd.Parameters.AddWithValue("@CollecteurSystemId", CollecteurSystemId)
                    Cmd.Parameters.AddWithValue("@AgenceId", AgenceId)

                    Try
                        'Cmd.ExecuteNonQueryAsync()
                        Dim result As Object = Cmd.ExecuteScalar()
                        If Not (IsNothing(result)) Then
                            ModelState.AddModelError("Mois", result.ToString)
                            LoadCombo(entityVM)
                            Return View(entityVM)
                        End If

                        'If (result.ToString <> "") Then
                        '    ModelState.AddModelError("Mois", result.ToString)
                        '    LoadCombo(entityVM)
                        '    Return View(entityVM)
                        'End If
                        Cmd.Dispose()
                        Con.Dispose()
                        Return RedirectToAction("IndexCommission")
                    Catch ex As SqlException
                        ModelState.AddModelError("Mois", ex.Message)
                        LoadCombo(entityVM)
                        Return View(entityVM)
                    End Try
                Catch ex As Exception
                    ModelState.AddModelError("Mois", ex.Message)
                    LoadCombo(entityVM)
                    Return View(entityVM)
                End Try
            End If

            LoadCombo(entityVM)
            Return View(entityVM)
        End Function


        ' GET: CarnetClient/Create
        <LocalizedAuthorize(Roles:="SA,ADMINISTRATEUR,CHEFCOLLECTEUR")>
        Function Create() As ActionResult
            Dim entityVM As New ChoixParamAjoutViewModel
            LoadCombo(entityVM)
            Return View(entityVM)
        End Function

        ' POST: CarnetClient/Create
        'Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        'plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        <LocalizedAuthorize(Roles:="SA,ADMINISTRATEUR,CHEFCOLLECTEUR")>
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function Create(ByVal entityVM As ChoixParamAjoutViewModel) As ActionResult
            entityVM.UserId = getCurrentUser.Id
            Dim CollecteurId = ConfigurationManager.AppSettings("CollecteurSystemeId")   'CollectMois.FirstOrDefault.CollecteurId
            If ModelState.IsValid Then
                'on teste si il n'y a pas déjà des ajout calculer pour se moi
                Dim mois = entityVM.Mois : Dim annee = entityVM.Annee
                Dim ajouts = (From h In db.HistoriqueCalculAjout Where h.Mois = mois And h.Annee = annee Select h).ToArray

                If (ajouts.Count > 0) Then
                    ModelState.AddModelError("Mois", "Les Agios pour cette période ont déjà été Calculés")
                    LoadCombo(entityVM)
                    Return View(entityVM)
                End If
                'maintenant on calcul les ajout pour chaque client
                Dim CLients = (From Clt In db.Clients, HC In db.HistoriqueMouvements Where HC.ClientId = Clt.Id And HC.DateOperation.Value.Month = mois Select Clt).Distinct().ToArray
                If (CLients.Count = 0) Then
                    ModelState.AddModelError("Mois", "Aucun Client Collecté au cours de ce mois")
                    LoadCombo(entityVM)
                    Return View(entityVM)
                End If

                Dim LesBorneCommission = db.BorneCommissions.ToList
                If (LesBorneCommission.Count = 0) Then
                    ModelState.AddModelError("Mois", "Les Paramètres de calcul ne sont pas fixés")
                    LoadCombo(entityVM)
                    Return View(entityVM)
                End If

                Dim BorneCommission As New BorneCommission
                BorneCommission = LesBorneCommission.FirstOrDefault

                For Each clt As Client In CLients
                    Dim CLientId = clt.Id
                    Dim CollectMois = (From HC In db.HistoriqueMouvements Where HC.ClientId = CLientId And HC.DateOperation.Value.Month = mois And HC.DateOperation.Value.Year = annee And HC.CollecteurId <> CollecteurId Select HC).ToArray
                    Dim TotalCollect = 0

                    If (CollectMois.Count > 0) Then
                        'on calcul le montant total
                        For Each h As HistoriqueMouvement In CollectMois
                            TotalCollect += h.Montant
                        Next
                        'maintenant on cherche la grille de calcul correspondante
                        Dim InfoFrais = (From inf In db.InfoFrais Where inf.BornInf < TotalCollect And TotalCollect < inf.BornSup Select inf).ToArray
                        'If (InfoFrais.Count = 0) Then
                        '    ModelState.AddModelError("Mois", "Les grilles de calcul des frais ne sont pas fixer pour le montant de se client")
                        '    LoadCombo(entityVM)
                        '    Return View(entityVM)
                        'End If
                        Dim paramCout = InfoFrais.FirstOrDefault
                        Dim comm = (TotalCollect * paramCout.Taux) / 100 + paramCout.Frais
                        If (comm < BorneCommission.BornInf) Then
                            comm = BorneCommission.BornInf
                        ElseIf (comm > BorneCommission.BornSup) Then
                            comm = BorneCommission.BornSup
                        End If
                        'maintenant on met a jour le compte du client
                        Dim client = db.Clients.Find(CLientId)
                        client.Solde -= comm
                        db.Entry(Client).State = EntityState.Modified
                        'db.SaveChanges()
                        'maintenant on ecrit dans historique mouvement
                        Dim UserId = getCurrentUser.Id

                        '3- on recupere le journal caisse et on enregistre dans mouvement historique
                        Dim LesJournalCaisse = (From J In db.JournalCaisses Where J.CollecteurId = CollecteurId Select J).ToArray
                        Dim JCID = LesJournalCaisse.FirstOrDefault.Id
                        Dim LibOperation As String = "Calcul Commission : " & mois & " - " & annee

                        Dim parameterList As New List(Of SqlParameter)()
                        parameterList.Add(New SqlParameter("@ClientId", CLientId))
                        parameterList.Add(New SqlParameter("@CollecteurId", CollecteurId))
                        parameterList.Add(New SqlParameter("@Montant", -comm))
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
                                db.SaveChanges()
                                'Return RedirectToAction("Index")
                                'Return Ok(historique)
                            Else
                                ModelState.AddModelError("Mois", "Une erreur est survenue pendant l'exécution de la requête: veuillez contacter l'administrateur. ")
                                LoadCombo(entityVM)
                                Return View(entityVM)
                            End If
                        Catch ex As DbEntityValidationException
                            Util.GetError(ex)
                            ModelState.AddModelError("Monatnt", "Une erreur est survenue pendant le traitement: veuillez contacter l'administrateur.")
                            LoadCombo(entityVM)
                            Return View(entityVM)
                        Catch ex As Exception
                            Util.GetError(ex)
                            ModelState.AddModelError("Monatnt", "Une erreur est survenue pendant le traitement: veuillez contacter l'administrateur. ")
                            LoadCombo(entityVM)
                            Return View(entityVM)
                        End Try
                    End If
                Next

                Dim c = entityVM.getEntity
                c.DateCreation = Now
                c.Libelle = "Calcul Agios " & entityVM.Mois & " - " & entityVM.Annee
                db.HistoriqueCalculAjout.Add(c)
                db.SaveChanges()
                Return RedirectToAction("Index")
            End If

            LoadCombo(entityVM)
            Return View(entityVM)
        End Function

        ' GET: CarnetClient/Edit/5
        Function Edit(ByVal id As Long?) As ActionResult

            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim carnetClient As CarnetClient = db.CarnetClients.Find(id)
            If IsNothing(carnetClient) Then
                Return HttpNotFound()
            End If

            Dim pVM As New CarnetClientViewModel

            Dim listClient = db.Clients.OfType(Of Client)().ToList
            Dim listClient2 As New List(Of SelectListItem)
            For Each item In listClient
                listClient2.Add(New SelectListItem With {.Value = item.Id, .Text = item.Nom & ":-- [" & item.Nom & "] --"})
            Next

            Dim listType = db.TypeCarnets.OfType(Of TypeCarnet)().ToList
            Dim listcarnet As New List(Of SelectListItem)
            For Each item In listType
                listcarnet.Add(New SelectListItem With {.Value = item.Id, .Text = item.Libelle & ":-- [" & item.Libelle & "] --"})
            Next

            pVM.IDsClient = listClient2
            pVM.IDsTypeCarnet = listcarnet
            Return View(pVM)
        End Function

        ' POST: CarnetClient/Edit/5
        'Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        'plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function Edit(<Bind(Include:="Id,ClientId,TypeCarnetId,Etat,DateAffectation")> ByVal carnetClient As CarnetClientViewModel) As ActionResult
            If ModelState.IsValid Then
                Dim ca = carnetClient.getEntity
                db.Entry(ca).State = EntityState.Modified
                db.SaveChanges()
                Return RedirectToAction("Index")
            End If
            Return View(carnetClient)
        End Function

        ' GET: CarnetClient/Delete/5
        Function Delete(ByVal id As Long?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim carnetClient As CarnetClient = db.CarnetClients.Find(id)
            If IsNothing(carnetClient) Then
                Return HttpNotFound()
            End If
            Return View(carnetClient)
        End Function

        ' POST: CarnetClient/Delete/5
        <HttpPost()>
        <ActionName("Delete")>
        <ValidateAntiForgeryToken()>
        Function DeleteConfirmed(ByVal id As Long) As ActionResult
            Dim carnetClient As CarnetClient = db.CarnetClients.Find(id)
            db.CarnetClients.Remove(carnetClient)
            db.SaveChanges()
            Return RedirectToAction("Index")
        End Function

        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If (disposing) Then
                db.Dispose()
            End If
            MyBase.Dispose(disposing)
        End Sub
    End Class
End Namespace
