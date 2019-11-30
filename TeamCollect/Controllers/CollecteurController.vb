Imports System.Data.Entity
Imports System.Net
Imports PagedList
Imports Microsoft.AspNet.Identity
Imports System.Data.Entity.Validation

Namespace TeamCollect
    Public Class CollecteurController
        Inherits BaseController

        Private db As New ApplicationDbContext

        Private Function GetCurrentUser() As ApplicationUser
            Dim id = User.Identity.GetUserId
            Dim aspuser = db.Users.Find(id)
            Return aspuser
        End Function
        ' GET: /Rapport
        Function ListeCollecteur() As ActionResult
            ViewBag.dateDebut = Now.Date.ToString("d")
            ViewBag.dateFin = Now.Date.ToString("d")
            ViewBag.UserAgenceId = GetCurrentUser.Personne.AgenceId
            Return View()
        End Function

        Function ListeCollecteurGlobal() As ActionResult
            ViewBag.dateDebut = Now.Date.ToString("d")
            ViewBag.dateFin = Now.Date.ToString("d")
            ViewBag.UserAgenceId = GetCurrentUser.Personne.AgenceId

            '----------------on recupère la liste des agences pour filtrer---------------
            Dim listPersonne = db.Agences.OfType(Of Agence)().ToList
            Dim listPersonne2 As New List(Of SelectListItem)
            For Each item In listPersonne
                listPersonne2.Add(New SelectListItem With {.Value = item.Id, .Text = item.Societe.Libelle & ":-- [" & item.Libelle & "] --"})
            Next
            ViewBag.lesagences = listPersonne2.ToList
            Return View()
        End Function

        ' GET: /Collecteur/
        <LocalizedAuthorize(Roles:="ADMINISTRATEUR,MANAGER")>
        Function Index(sortOrder As String, currentFilter As String, searchString As String, page As Integer?, AgenceId As Long?) As ActionResult
            'Dim exercices = db.Exercices.Include(Function(e) e.User)
            'Return View(exercices.ToList())

            ViewBag.CurrentSort = sortOrder

            If Not String.IsNullOrEmpty(searchString) Then
                page = 1
            Else
                searchString = currentFilter
            End If

            ViewBag.CurrentFilter = searchString

            Dim entities = From e In db.Personnes.OfType(Of Collecteur).ToList

            If (AgenceId.HasValue) Then
                entities = From e In db.Personnes.OfType(Of Collecteur).Where(Function(e) e.AgenceId = AgenceId).ToList
            Else
                entities = entities.ToList
            End If


            If Not String.IsNullOrEmpty(searchString) Then
                entities = entities.Where(Function(e) e.Nom.ToUpper.Contains(searchString.ToUpper) Or e.Prenom.ToUpper.Contains(searchString.ToUpper) Or e.Sexe.ToUpper.Contains(searchString.ToUpper) Or e.Telephone.ToUpper.Contains(searchString.ToUpper) Or e.Adresse.ToUpper.Contains(searchString.ToUpper) Or e.Quartier.ToUpper.Contains(searchString.ToUpper))
            End If
            ViewBag.EnregCount = entities.Count

            Dim pageSize As Integer = ConfigurationManager.AppSettings("PageSize")
            Dim pageNumber As Integer = If(page, 1)

            '----------------on recupère la liste des agences pour filtrer---------------
            Dim listPersonne = db.Agences.OfType(Of Agence)().ToList
            Dim listPersonne2 As New List(Of SelectListItem)
            For Each item In listPersonne
                listPersonne2.Add(New SelectListItem With {.Value = item.Id, .Text = item.Societe.Libelle & ":-- [" & item.Libelle & "] --"})
            Next
            ViewBag.lesagences = listPersonne2.ToList

            Return View(entities.ToPagedList(pageNumber, pageSize))
        End Function



        <LocalizedAuthorize(Roles:="CHEFCOLLECTEUR")>
        Function IndexAgence(sortOrder As String, currentFilter As String, searchString As String, page As Integer?) As ActionResult
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
            Dim entities = From e In db.Personnes.OfType(Of Collecteur).Where(Function(e) e.AgenceId = userAgenceId).ToList

            If Not String.IsNullOrEmpty(searchString) Then
                entities = entities.Where(Function(e) e.Nom.ToUpper.Contains(searchString.ToUpper) Or e.Prenom.ToUpper.Contains(searchString.ToUpper) Or e.Sexe.ToUpper.Contains(searchString.ToUpper) Or e.Telephone.ToUpper.Contains(searchString.ToUpper) Or e.Adresse.ToUpper.Contains(searchString.ToUpper) Or e.Quartier.ToUpper.Contains(searchString.ToUpper))
            End If
            ViewBag.EnregCount = entities.Count

            Dim pageSize As Integer = ConfigurationManager.AppSettings("PageSize")
            Dim pageNumber As Integer = If(page, 1)

            Return View(entities.ToPagedList(pageNumber, pageSize))
        End Function


        ' GET: /Collecteur/Details/5
        <LocalizedAuthorize(Roles:="CHEFCOLLECTEUR")>
        Function Details(ByVal id As Long?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim collecteur As Collecteur = db.Personnes.Find(id)
            If IsNothing(collecteur) Then
                Return HttpNotFound()
            End If
            Return View(collecteur)
        End Function


        Public Sub LoadComboBox(ByVal entityVM As CollecteurViewModel)
            Dim CategorieRemuneration = (From e In db.CategorieRemunerations Where e.StatutExistant = True Select e)
            Dim LesCategorieRemuneration As New List(Of SelectListItem)
            For Each item In CategorieRemuneration
                LesCategorieRemuneration.Add(New SelectListItem With {.Value = item.Id, .Text = item.Libelle & " -- [" & String.Format("{0:0,0}", item.SalaireDeBase) & "F CFA]"})
            Next
            'LesCategorieRemuneration.Add(New SelectListItem With {.Value = item.Id, .Text = item.Libelle & " -- " & String.Format("{0:0,0}", item.SalaireDeBase) & "F CFA"})

            entityVM.LesCategorieRemuneration = LesCategorieRemuneration
        End Sub

        ' GET: /Collecteur/Create
        <LocalizedAuthorize(Roles:="CHEFCOLLECTEUR")>
        Function Create() As ActionResult
            Dim colVM As New CollecteurViewModel

            Dim userAgenceId = GetCurrentUser.Personne.AgenceId
            Dim listPersonne = db.Agences.OfType(Of Agence).Where(Function(e) e.Id = userAgenceId).ToList
            Dim listPersonne2 As New List(Of SelectListItem)
            For Each item In listPersonne
                listPersonne2.Add(New SelectListItem With {.Value = item.Id, .Text = item.Societe.Libelle & ":-- [" & item.Libelle & "] --"})
            Next
            colVM.IDsAgence = listPersonne2
            LoadComboBox(colVM)
            Return View(colVM)
        End Function

        ' POST: /Collecteur/Create
        'Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        'plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        <LocalizedAuthorize(Roles:="CHEFCOLLECTEUR")>
        Function Create(ByVal collecteur As CollecteurViewModel) As ActionResult
            If ModelState.IsValid Then
                collecteur.DateCreation = Now
                collecteur.Etat = 0
                Dim entities = (From e In db.Personnes.OfType(Of Collecteur)() Select e)
                Dim list As List(Of Personne) = db.Personnes.OfType(Of Personne)().ToList
                collecteur.Id = list.ElementAt(list.Count - 1).Id + 1
                Dim monwebser = New TeamCollecteServices
                collecteur.CodeSecret = monwebser.GetRandomString(collecteur.Nom, collecteur.Prenom)
                Dim entity = collecteur.GetEntity
                db.Personnes.Add(entity)
                Try
                    db.SaveChanges()
                    Dim historiqueCollecteurCategorie As New HistoriqueCollecteurCategorie With {
                        .CategorieRemunerationId = entity.CategorieRemunerationId,
                        .CollecteurId = entity.Id,
                        .Libelle = entity.CategorieRemuneration.Libelle,
                        .SalaireDeBase = entity.CategorieRemuneration.SalaireDeBase,
                        .CommissionMinimale = entity.CategorieRemuneration.CommissionMinimale,
                        .PourcentageCommission = entity.CategorieRemuneration.PourcentageCommission,
                        .StatutExistant = True,
                        .DateCreation = DateTime.Now,
                        .UserId = GetCurrentUser.Id
                        }
                    db.HistoriqueCollecteurCategories.Add(historiqueCollecteurCategorie)
                    Try
                        db.SaveChanges()
                    Catch ex As DbEntityValidationException
                        Util.GetError(ex, ModelState)
                    Catch ex As Exception
                        Util.GetError(ex, ModelState)
                    End Try
                    Return RedirectToAction("IndexAgence")
                Catch ex As DbEntityValidationException
                    Util.GetError(ex, ModelState)
                Catch ex As Exception
                    Util.GetError(ex, ModelState)
                End Try
            End If

            Dim userAgenceId = GetCurrentUser.Personne.AgenceId
            Dim listPersonne = db.Agences.OfType(Of Agence).Where(Function(e) e.Id = userAgenceId).ToList
            Dim listPersonne2 As New List(Of SelectListItem)
            For Each item In listPersonne
                listPersonne2.Add(New SelectListItem With {.Value = item.Id, .Text = item.Societe.Libelle & ":-- [" & item.Libelle & "] --"})
            Next
            collecteur.IDsAgence = listPersonne2
            LoadComboBox(collecteur)
            Return View(collecteur)
        End Function

        ' GET: /Collecteur/Edit/5
        <LocalizedAuthorize(Roles:="CHEFCOLLECTEUR,MANAGER")>
        Function Edit(ByVal id As Long?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim collecteur As Collecteur = db.Personnes.Find(id)
            If IsNothing(collecteur) Then
                Return HttpNotFound()
            End If
            Dim colVM = New CollecteurViewModel(collecteur)

            Dim userAgenceId = GetCurrentUser.Personne.AgenceId
            Dim listPersonne = db.Agences.OfType(Of Agence).Where(Function(e) e.Id = userAgenceId).ToList
            Dim listPersonne2 As New List(Of SelectListItem)
            For Each item In listPersonne
                listPersonne2.Add(New SelectListItem With {.Value = item.Id, .Text = item.Societe.Libelle & ":-- [" & item.Libelle & "] --"})
            Next
            colVM.IDsAgence = listPersonne2
            LoadComboBox(colVM)
            Return View(colVM)
        End Function

        ' POST: /Collecteur/Edit/5
        'Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        'plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        <LocalizedAuthorize(Roles:="CHEFCOLLECTEUR,MANAGER")>
        Function Edit(ByVal collecteur As CollecteurViewModel) As ActionResult
            Dim categorieRemuneration = db.CategorieRemunerations.Find(collecteur.CategorieRemunerationId)
            If IsNothing(categorieRemuneration) Then
                ModelState.AddModelError("CategorieRemunerationId", "Veuillez sélectionner une catégorie de rémuneration valide")
            End If

            If ModelState.IsValid Then
                Dim entity = collecteur.GetEntity()
                db.Entry(entity).State = EntityState.Modified
                Try
                    'db.SaveChanges()

                    Dim OldHistoriqueCollecteurCategorie = (From e In db.HistoriqueCollecteurCategories Where e.StatutExistant = True And e.CollecteurId = collecteur.Id Order By e.Id Descending).FirstOrDefault()
                    If Not IsNothing(OldHistoriqueCollecteurCategorie) Then
                        If (OldHistoriqueCollecteurCategorie.Id > 0 And OldHistoriqueCollecteurCategorie.CategorieRemunerationId <> entity.CategorieRemunerationId) Then
                            OldHistoriqueCollecteurCategorie.StatutExistant = False
                            db.Entry(OldHistoriqueCollecteurCategorie).State = EntityState.Modified
                        End If
                    ElseIf IsNothing(OldHistoriqueCollecteurCategorie.Id) Then
                        Dim NewHistoriqueCollecteurCategorie As New HistoriqueCollecteurCategorie With {
                            .CategorieRemunerationId = categorieRemuneration.Id,
                            .CollecteurId = entity.Id,
                            .Libelle = categorieRemuneration.Libelle,
                            .SalaireDeBase = categorieRemuneration.SalaireDeBase,
                            .CommissionMinimale = categorieRemuneration.CommissionMinimale,
                            .PourcentageCommission = categorieRemuneration.PourcentageCommission,
                            .StatutExistant = True,
                            .DateCreation = DateTime.Now,
                            .UserId = GetCurrentUser.Id
                        }
                        db.HistoriqueCollecteurCategories.Add(NewHistoriqueCollecteurCategorie)
                        Try
                            db.SaveChanges()
                        Catch ex As DbEntityValidationException
                            Util.GetError(ex, ModelState)
                        Catch ex As Exception
                            Util.GetError(ex, ModelState)
                        End Try
                    End If
                    If User.IsInRole("MANAGER") Then
                        Return RedirectToAction("Index")
                    End If
                    Return RedirectToAction("IndexAgence")
                Catch ex As DbEntityValidationException
                    Util.GetError(ex, ModelState)
                Catch ex As Exception
                    Util.GetError(ex, ModelState)
                End Try
            End If

            Dim userAgenceId = GetCurrentUser.Personne.AgenceId
            Dim listPersonne = db.Agences.OfType(Of Agence).Where(Function(e) e.Id = userAgenceId).ToList
            Dim listPersonne2 As New List(Of SelectListItem)
            For Each item In listPersonne
                listPersonne2.Add(New SelectListItem With {.Value = item.Id, .Text = item.Societe.Libelle & ":-- [" & item.Libelle & "] --"})
            Next
            collecteur.IDsAgence = listPersonne2
            LoadComboBox(collecteur)
            Return View(collecteur)
        End Function

        ' GET: /Collecteur/Delete/5
        <LocalizedAuthorize(Roles:="CHEFCOLLECTEUR")>
        Function Delete(ByVal id As Long?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim collecteur As Collecteur = db.Personnes.Find(id)
            If IsNothing(collecteur) Then
                Return HttpNotFound()
            End If
            Return View(collecteur)
        End Function

        ' POST: /Collecteur/Delete/5
        <HttpPost()>
        <ActionName("Delete")>
        <ValidateAntiForgeryToken()>
        <LocalizedAuthorize(Roles:="CHEFCOLLECTEUR")>
        Function DeleteConfirmed(ByVal id As Long) As ActionResult
            Dim collecteur As Collecteur = db.Personnes.Find(id)
            db.Personnes.Remove(collecteur)
            Try
                db.SaveChanges()
                Return RedirectToAction("IndexAgence")
            Catch ex As DbEntityValidationException
                Util.GetError(ex, ModelState)
            Catch ex As Exception
                Util.GetError(ex, ModelState)
            End Try
            Return View(collecteur)
        End Function

        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If (disposing) Then
                db.Dispose()
            End If
            MyBase.Dispose(disposing)
        End Sub
    End Class
End Namespace
