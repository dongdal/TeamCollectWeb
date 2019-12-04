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
    Public Class CarnetClientController
        Inherits BaseController

        Private db As New ApplicationDbContext

        Private Function GetCurrentUser() As ApplicationUser
            Dim id = User.Identity.GetUserId
            Dim aspuser = db.Users.Find(id)
            Return aspuser
        End Function


        ' GET: CarnetClient
        <LocalizedAuthorize(Roles:="CHEFCOLLECTEUR")>
        Function Index(sortOrder As String, currentFilter As String, searchString As String, page As Integer?) As ActionResult
            ViewBag.CurrentSort = sortOrder

            If Not String.IsNullOrEmpty(searchString) Then
                page = 1
            Else
                searchString = currentFilter
            End If

            ViewBag.CurrentFilter = searchString
            Dim CurrentAgenceId = getCurrentUser.Personne.AgenceId

            Dim entities = (From carnet In db.CarnetClients Where carnet.Client.AgenceId = CurrentAgenceId Select carnet).ToList
            entities = entities.OrderByDescending(Function(e) e.DateAffectation).ToList
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

        Public Sub LoadCombo(pVM As CarnetClientViewModel)
            Dim listClient = db.Clients.OfType(Of Client)().ToList
            Dim listClient1 As New List(Of SelectListItem)
            Dim CurrentUser = getCurrentUser()

            If User.IsInRole("CHEFCOLLECTEUR") Then
                listClient = listClient.Where(Function(e) e.AgenceId = CurrentUser.Personne.AgenceId).ToList()
            End If

            'For Each item In listClient
            '    listClient2.Add(New SelectListItem With {.Value = item.Id, .Text = item.Nom & ":-- [" & item.Nom & "] --"})
            '    listclient1.Add(New SelectListItem With {.Value = item.Id, .Text = item.Nom.ToUpper & " " & item.Prenom.ToUpper & "[Portefeuille: " & item.PorteFeuille.Libelle.ToUpper & "]" & " :-- " & " [Solde Dispo: " & item.SoldeDisponible & "]"})
            'Next

            For Each item In listClient
                Dim PorteFeuilleLibelle As String = "AUCUN PORTEFEUILLE"
                If (Not IsNothing(item.PorteFeuille)) Then
                    PorteFeuilleLibelle = item.PorteFeuille.Libelle.ToUpper
                End If

                If (String.IsNullOrEmpty(item.Prenom)) Then
                    listClient1.Add(New SelectListItem With {.Value = item.Id, .Text = item.Nom.ToUpper & " :-- " & "[Portefeuille: " & PorteFeuilleLibelle.ToUpper & "]" & " :-- " & " [Solde Dispo: " & item.SoldeDisponible & "]"})
                Else
                    listClient1.Add(New SelectListItem With {.Value = item.Id, .Text = item.Nom.ToUpper & " " & item.Prenom.ToUpper & "[Portefeuille: " & PorteFeuilleLibelle.ToUpper & "]" & " :-- " & " [Solde Dispo: " & item.SoldeDisponible & "]"})
                End If
            Next

            Dim listType = db.TypeCarnets.OfType(Of TypeCarnet)().ToList
            Dim listcarnet As New List(Of SelectListItem)
            For Each item In listType
                listcarnet.Add(New SelectListItem With {.Value = item.Id, .Text = item.Libelle.ToUpper & ":-- [" & item.Prix & "] --"})
            Next

            pVM.IDsClient = listClient1
            pVM.IDsTypeCarnet = listcarnet
        End Sub


        ' GET: CarnetClient/Create
        <LocalizedAuthorize(Roles:="CHEFCOLLECTEUR")>
        Function Create() As ActionResult
            Dim pVM As New CarnetClientViewModel
            LoadCombo(pVM)
            Return View(pVM)
        End Function

        ' POST: CarnetClient/Create
        'Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        'plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        <LocalizedAuthorize(Roles:="CHEFCOLLECTEUR")>
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function Create(<Bind(Include:="Id,ClientId,TypeCarnetId,Etat,DateAffectation")> ByVal carnetClientVM As CarnetClientViewModel) As ActionResult
            If ModelState.IsValid Then
                Dim CollecteurId = ConfigurationManager.AppSettings("CollecteurSystemeId") 'getCurrentUser.PersonneId
                Dim ClientId = carnetClientVM.ClientId
                Dim client = db.Clients.Find(ClientId)
                Dim UserId = getCurrentUser.Id
                If IsNothing(client) Then
                    ModelState.AddModelError("ClientId", "Le client Selectionner n'a pas de compte ")
                    LoadCombo(carnetClientVM)
                    Return View(carnetClientVM)
                End If

                Dim LesJournalCaisse = (From J In db.JournalCaisses Where J.CollecteurId = CollecteurId Select J).ToArray
                If (LesJournalCaisse.Count = 0) Then
                    ModelState.AddModelError("ClientId", "Vous n'avez pas de caisse ouverte pour effectuer cette opération ")
                    LoadCombo(carnetClientVM)
                    Return View(carnetClientVM)
                End If

                Dim TypeCarnetId = carnetClientVM.TypeCarnetId

                Dim typecarnet = db.TypeCarnets.Find(TypeCarnetId)

                If IsNothing(typecarnet) Then
                    ModelState.AddModelError("TypeCarnetId", "Le Carnet que vous avez selectionner n'existe pas! ")
                    LoadCombo(carnetClientVM)
                    Return View(carnetClientVM)
                End If

                Dim Montant = typecarnet.Prix
                If Not (client.Solde - Montant >= 0) Then
                    ModelState.AddModelError("Monatnt", "Le solde du client est insuiffisant pour un retrait de " & Montant)
                    LoadCombo(carnetClientVM)
                    Return View(carnetClientVM)
                End If

                'mise a jour du solde du client
                client.Solde -= Montant
                db.Entry(client).State = EntityState.Modified
                db.SaveChanges()

                'enregistrer le carnet

                Dim ca = carnetClientVM.getEntity
                ca.UserId = UserId
                ca.DateAffectation = Now
                db.CarnetClients.Add(ca)
                db.SaveChanges()

                '3- on recupere le journal caisse et on enregistre dans mouvement historique
                Dim JCID = LesJournalCaisse.FirstOrDefault.Id
                'Dim LibOperation As String = "Achat Carnet : " & ca.TypeCarnet.Libelle & " - Le -" & DateTime.Now.ToString
                Dim LibOperation As String = "VENTE CARNET : " & ca.TypeCarnet.Libelle & " - Le -" & DateTime.Now.ToString

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
                    Dim myInsertQuery As String = "INSERT INTO HistoriqueMouvement (ClientId, CollecteurId, Montant, DateOperation, MontantRetenu, Pourcentage, EstTraiter, Etat, DateCreation, 
UserId, JournalCaisseId, LibelleOperation) VALUES (@ClientId, @CollecteurId, @Montant, @DateOperation, @MontantRetenu, @Pourcentage, @EstTraiter, @Etat, @DateCreation, @UserId, @JournalCaisseId, 
@LibelleOperation)"

                    'Dim laDate As Date = Now
                    If (db.Database.ExecuteSqlCommand(myInsertQuery, parameters)) Then
                        Dim historiquesMouvements = (From h In db.HistoriqueMouvements Where (h.UserId = UserId) Select HistoriqueId = h.Id, JournalCaisseId = h.JournalCaisseId,
                                        IdClient = h.ClientId, NomClient = h.Client.Nom, PrenomClient = h.Client.Prenom, IdCollecteur = h.CollecteurId, NomCollecteur = h.Collecteur.Nom, PrenomCollecteur = h.Collecteur.Prenom, MontantCollecte = h.Montant,
                                        FraisFixes = h.MontantRetenu, Taux = h.Pourcentage, h.DateOperation, h.LibelleOperation).ToList

                        Dim historique = historiquesMouvements.ElementAtOrDefault((historiquesMouvements.Count - 1))
                        Return RedirectToAction("Index")
                        'Return Ok(historique)
                    Else
                        ModelState.AddModelError("Monatnt", "Une erreur est survenue pendant l'exécution de la requête: veuillez contacter l'administrateur. ")
                        LoadCombo(carnetClientVM)
                        Return View(carnetClientVM)
                    End If
                Catch ex As DbEntityValidationException
                    Util.GetError(ex)
                    ModelState.AddModelError("Monatnt", "Une erreur est survenue pendant le traitement: veuillez contacter l'administrateur.")
                    LoadCombo(carnetClientVM)
                    Return View(carnetClientVM)
                Catch ex As Exception
                    Util.GetError(ex)
                    ModelState.AddModelError("Monatnt", "Une erreur est survenue pendant le traitement: veuillez contacter l'administrateur. ")
                    LoadCombo(carnetClientVM)
                    Return View(carnetClientVM)
                End Try

                Return RedirectToAction("Index")
            End If
            LoadCombo(carnetClientVM)
            Return View(carnetClientVM)
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
