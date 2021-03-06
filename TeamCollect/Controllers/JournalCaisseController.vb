Imports System.Data.Entity
Imports System.Net
Imports PagedList
Imports Microsoft.AspNet.Identity
Imports System.Data.Entity.Validation

Namespace TeamCollect
    Public Class JournalCaisseController
        Inherits BaseController

        Private db As New ApplicationDbContext

        Private Function GetCurrentUser() As ApplicationUser
            Dim id = User.Identity.GetUserId
            Dim aspuser = db.Users.Find(id)
            Return aspuser
        End Function

        <LocalizedAuthorize(Roles:="CHEFCOLLECTEUR")>
        Function Index(sortOrder As String, currentFilter As String, searchString As String, page As Integer?, Optional Message As String = "") As ActionResult
            'Dim exercices = db.Exercices.Include(Function(e) e.User)
            'Return View(exercices.ToList())

            ViewBag.CurrentSort = sortOrder

            If Not String.IsNullOrEmpty(searchString) Then
                page = 1
            Else
                searchString = currentFilter
            End If

            ViewBag.CurrentFilter = searchString
            Dim userAgenceId = GetCurrentUser.Personne.AgenceId
            Dim entities = db.JournalCaisses.Include(Function(j) j.Collecteur).Where(Function(i) i.Collecteur.AgenceId = userAgenceId).OrderByDescending(Function(e) e.DateOuverture).ToList



            If Not String.IsNullOrEmpty(searchString) Then
                entities = entities.Where(Function(e) e.Collecteur.Nom.ToUpper.Contains(searchString.ToUpper) Or e.Collecteur.Prenom.ToUpper.Contains(searchString.ToUpper)).ToList
            End If
            ViewBag.EnregCount = entities.Count
            ViewBag.Message = Message

            Dim pageSize As Integer = ConfigurationManager.AppSettings("PageSize")
            Dim pageNumber As Integer = If(page, 1)

            Return View(entities.ToPagedList(pageNumber, pageSize))
        End Function


        ' GET: /JournalCaisse/Details/5
        <LocalizedAuthorize(Roles:="CHEFCOLLECTEUR")>
        Function Details(ByVal id As Long?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim journalcaisse As JournalCaisse = db.JournalCaisses.Find(id)
            If IsNothing(journalcaisse) Then
                Return HttpNotFound()
            End If
            Return View(journalcaisse)
        End Function

        ' GET: /JournalCaisse/Create
        <LocalizedAuthorize(Roles:="CHEFCOLLECTEUR")>
        Function Create() As ActionResult
            Dim entityVM As New JournalCaisseViewModel

            Dim userAgenceId = GetCurrentUser.Personne.AgenceId
            Dim userSocieteId = GetCurrentUser.Personne.Agence.SocieteId
            Dim listPersonne = db.Personnes.OfType(Of Collecteur).Where(Function(i) i.AgenceId = userAgenceId).ToList
            Dim LePlafondDeCollecte = db.Societes.OfType(Of Societe).Where(Function(i) i.Id = userSocieteId).First.PlafondDeCollecte.ToString
            Dim listPersonne2 As New List(Of SelectListItem)
            For Each item In listPersonne
                listPersonne2.Add(New SelectListItem With {.Value = item.Id, .Text = item.Nom & " " & item.Prenom})
            Next
            entityVM.PlafondDebat = LePlafondDeCollecte
            entityVM.IDsCollecteur = listPersonne2
            Return View(entityVM)
        End Function

        ' POST: /JournalCaisse/Create
        'Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        'plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        <LocalizedAuthorize(Roles:="CHEFCOLLECTEUR")>
        Function Create(ByVal journalcaisse As JournalCaisseViewModel) As ActionResult
            Dim userAgenceId = GetCurrentUser.Personne.AgenceId

            If ModelState.IsValid Then

                Dim wherecloseId = journalcaisse.CollecteurId
                Dim nbreDouverture = db.JournalCaisses.OfType(Of JournalCaisse).Where(Function(h) h.CollecteurId = wherecloseId And (Not h.DateOuverture Is Nothing And h.DateCloture Is Nothing)).Count
                Dim nbreColEncourDeTraitemnt = db.HistoriqueMouvements.OfType(Of HistoriqueMouvement).Where(Function(h) h.EstTraiter = 1 And h.Collecteur.AgenceId = userAgenceId).Count

                If (nbreColEncourDeTraitemnt >= 1 Or nbreDouverture >= 1) Then
                    If (nbreColEncourDeTraitemnt >= 1) Then
                        ModelState.AddModelError("", "Il ya des données en attente de traitement!. VEUILLEZ TERMINER L'IMPORT DES DONNEES. ")
                    End If
                    If (nbreDouverture >= 1) Then
                        ModelState.AddModelError("", "Ce collecteur a une caisse ouverte Veuillez la fermer, avant d'ouvrir une autre. ")
                    End If
                Else
                    journalcaisse.Etat = 0
                    journalcaisse.DateCreation = Now.Date
                    journalcaisse.DateOuverture = Now.Date
                    journalcaisse.UserId = GetCurrentUser.Id
                    journalcaisse.PlafondEnCours = journalcaisse.PlafondDebat
                    Dim jrn = journalcaisse.getEntity()
                    db.JournalCaisses.Add(jrn)
                    Try
                        db.SaveChanges()
                        Return RedirectToAction("Index")
                    Catch ex As DbEntityValidationException
                        Util.GetError(ex, ModelState)
                    Catch ex As Exception
                        Util.GetError(ex, ModelState)
                    End Try
                End If

            End If

            Dim listPersonne = db.Personnes.OfType(Of Collecteur).Where(Function(i) i.AgenceId = userAgenceId).ToList
            Dim listPersonne2 As New List(Of SelectListItem)
            For Each item In listPersonne
                listPersonne2.Add(New SelectListItem With {.Value = item.Id, .Text = item.Nom & " " & item.Prenom})
            Next
            journalcaisse.IDsCollecteur = listPersonne2

            Return View(journalcaisse)
        End Function

        ' GET: /JournalCaisse/Edit/5
        <LocalizedAuthorize(Roles:="CHEFCOLLECTEUR")>
        Function Edit(ByVal id As Long?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim journalcaisse As JournalCaisse = db.JournalCaisses.Find(id)
            If IsNothing(journalcaisse) Then
                Return HttpNotFound()
            End If
            Dim journalVM = New JournalCaisseViewModel(journalcaisse)
            Dim userAgenceId = GetCurrentUser.Personne.AgenceId
            Dim IdJournal = journalcaisse.Id
            'Dim listPersonne = db.Personnes.OfType(Of Collecteur).Where(Function(i) i.AgenceId = userAgenceId).ToList
            Dim listPersonne = (From c In db.Collecteurs, j In db.JournalCaisses Where j.CollecteurId = c.Id And j.Id = IdJournal Select c).ToList
            Dim listPersonne2 As New List(Of SelectListItem)
            For Each item In listPersonne
                listPersonne2.Add(New SelectListItem With {.Value = item.Id, .Text = item.Nom & " " & item.Prenom})
            Next
            journalVM.IDsCollecteur = listPersonne2
            Return View(journalVM)
        End Function

        <HttpPost()>
        <LocalizedAuthorize(Roles:="SA,ADMINISTRATEUR,CHEFCOLLECTEUR")>
        Function CloturerCaisse(ByVal journalcaisse As JournalCaisseJSON) As JsonResult
            If ModelState.IsValid Then
                Dim entity = journalcaisse.getEntity()
                entity.DateCloture = Now.Date
                entity.Etat = 1
                entity.UserId = GetCurrentUser.Id

                Dim wherecloseId = entity.Id
                Dim montantTheo = db.HistoriqueMouvements.OfType(Of HistoriqueMouvement).Where(Function(h) h.JournalCaisseId = wherecloseId And
                                                                                                   Not h.Extourner.HasValue).Sum(Function(h) h.Montant)
                entity.MontantTheorique = (IIf(montantTheo.HasValue, montantTheo, 0) + entity.FondCaisse)
                entity.MontantReel = entity.MontantReel

                db.Entry(entity).State = EntityState.Modified

                Try
                    db.SaveChanges()
                    Return Json(New With {.Result = "OK"})
                Catch ex As DbEntityValidationException
                    Util.GetError(ex, ModelState)
                    Return Json(New With {.Result = "Error: Une erreur est survenue pendant l'exécution de la requête: veuillez contacter l'administrateur."})
                Catch ex As Exception
                    Util.GetError(ex, ModelState)
                    Return Json(New With {.Result = "Error: Une erreur est survenue pendant le traitement: veuillez contacter l'administrateur."})
                End Try
            End If

            Return Json(New With {.Result = "Error"})
        End Function

        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If (disposing) Then
                db.Dispose()
            End If
            MyBase.Dispose(disposing)
        End Sub
    End Class

    Public Class JournalCaisseJSON
        Public Property Id As Long = 0
        Public Property CollecteurId As Long = 0
        Public Property DateCreation As Nullable(Of Date)
        Public Property FondCaisse As String
        Public Property PlafondDebat As String
        Public Property PlafondEnCours As String
        Public Property MontantReel As String
        Public Property MontantTheorique As String
        Public Property DateOuverture As Date
        Public Property DateCloture As Nullable(Of Date)
        Public Property UserId As String
        Public Property Etat As Nullable(Of Integer)

        Public Function getEntity() As JournalCaisse
            Dim entity As New JournalCaisse

            With entity
                .Id = Me.Id
                .CollecteurId = Me.CollecteurId
                .MontantTheorique = Me.MontantTheorique
                .MontantReel = Me.MontantReel
                .FondCaisse = Me.FondCaisse
                .DateOuverture = Me.DateOuverture
                .DateCloture = Me.DateCloture
                .UserId = Me.UserId
                .DateCreation = Me.DateCreation
                .Etat = Me.Etat
                .PlafondDeDebat = Me.PlafondDebat
                .PlafondEnCours = Me.PlafondEnCours
            End With

            Return entity
        End Function
    End Class
End Namespace
