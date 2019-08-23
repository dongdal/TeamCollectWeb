Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.Entity
Imports System.Linq
Imports System.Net
Imports System.Web
Imports System.Web.Mvc
Imports PagedList
Imports System.Web.SessionState
Imports System.Web.Script.Serialization
Imports Microsoft.AspNet.Identity
Imports Microsoft.AspNet.Identity.EntityFramework
Imports TeamCollect.My.Resources
Imports System.Data.Entity.Validation
Imports System.IO

Namespace TeamCollect
    Public Class PorteFeuilleController
        Inherits BaseController

        Private db As New ApplicationDbContext

        Private Function getCurrentUser() As ApplicationUser
            Dim id = User.Identity.GetUserId
            Dim aspuser = db.Users.Find(id)
            Return aspuser
        End Function

        <LocalizedAuthorize(Roles:="CHEFCOLLECTEUR")>
        Function Index(sortOrder As String, currentFilter As String, searchString As String, page As Integer?) As ActionResult
            'Dim exercices = db.Exercices.Include(Function(e) e.User)
            'Return View(exercices.ToList())

            ViewBag.CurrentSort = sortOrder

            If Not String.IsNullOrEmpty(searchString) Then
                page = 1
            Else
                searchString = currentFilter
            End If

            ViewBag.CurrentFilter = searchString
            Dim userAgenceId = getCurrentUser.Personne.AgenceId
            Dim entities = db.PorteFeuilles.Include(Function(j) j.Collecteur).Where(Function(e) e.Collecteur.AgenceId = userAgenceId).ToList

            'For Each item In entities
            '    For Each client In item.Client
            '        If (client.Nom.ToUpper.Contains(searchString.ToUpper) Or client.Prenom.ToUpper.Contains(searchString.ToUpper)) Then

            '        End If
            '    Next
            'Next

            If Not String.IsNullOrEmpty(searchString) Then
                entities = entities.Where(Function(e) e.Libelle.ToUpper.Contains(searchString.ToUpper) Or e.Collecteur.Nom.ToUpper.Contains(searchString.ToUpper) Or
                    e.Collecteur.Prenom.ToUpper.Contains(searchString.ToUpper) Or e.Collecteur.Prenom.ToUpper.Contains(searchString.ToUpper)).ToList
            End If
            ViewBag.EnregCount = entities.Count

            Dim pageSize As Integer = ConfigurationManager.AppSettings("PageSizePorteFeuille")
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
            Dim entityVM As New PorteFeuilleViewModel

            Dim userAgenceId = getCurrentUser.Personne.AgenceId
            Dim listPersonne = db.Personnes.OfType(Of Collecteur).Where(Function(e) e.AgenceId = userAgenceId).ToList
            Dim listPersonne2 As New List(Of SelectListItem)
            For Each item In listPersonne
                listPersonne2.Add(New SelectListItem With {.Value = item.Id, .Text = item.Nom & " " & item.Prenom})
            Next
            entityVM.IDsCollecteur = listPersonne2
            Return View(entityVM)
        End Function

        ' POST: /JournalCaisse/Create
        'Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        'plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        <LocalizedAuthorize(Roles:="CHEFCOLLECTEUR")>
        Function Create(ByVal PorteFeuille As PorteFeuilleViewModel) As ActionResult
            Dim userAgenceId = getCurrentUser.Personne.AgenceId
            Dim NombrePorteFeuilleCollecteur = (From porteFeuil In db.PorteFeuilles Where porteFeuil.CollecteurId = PorteFeuille.CollecteurId Select porteFeuil).ToList.Count
            If (NombrePorteFeuilleCollecteur > 0) Then
                ModelState.AddModelError("", "Ce collecteur possède déjà un portefeuille.")
            End If

            If ModelState.IsValid Then
                Dim jrn = PorteFeuille.getEntity()
                jrn.DateCreation = Now.Date
                jrn.Etat = False
                jrn.UserId = getCurrentUser.Id
                db.PorteFeuilles.Add(jrn)
                Try
                    db.SaveChanges()
                    Return RedirectToAction("Index")
                Catch ex As DbEntityValidationException
                    Util.GetError(ex, ModelState)
                Catch ex As Exception
                    Util.GetError(ex, ModelState)
                End Try
            End If

            Dim listPersonne = db.Personnes.OfType(Of Collecteur).Where(Function(e) e.AgenceId = userAgenceId).ToList
            Dim listPersonne2 As New List(Of SelectListItem)
            For Each item In listPersonne
                listPersonne2.Add(New SelectListItem With {.Value = item.Id, .Text = item.Nom & " " & item.Prenom})
            Next
            PorteFeuille.IDsCollecteur = listPersonne2

            Return View(PorteFeuille)
        End Function

        ' GET: /JournalCaisse/Edit/5
        <LocalizedAuthorize(Roles:="CHEFCOLLECTEUR")>
        Function Edit(ByVal id As Long?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim PorteFeuille As PorteFeuille = db.PorteFeuilles.Find(id)
            If IsNothing(PorteFeuille) Then
                Return HttpNotFound()
            End If
            Dim PorteFeuilleVM = New PorteFeuilleViewModel(PorteFeuille)
            Dim listPersonne = db.Personnes.OfType(Of Collecteur)().ToList
            Dim listPersonne2 As New List(Of SelectListItem)
            For Each item In listPersonne
                listPersonne2.Add(New SelectListItem With {.Value = item.Id, .Text = item.Nom & " " & item.Prenom})
            Next
            PorteFeuilleVM.IDsCollecteur = listPersonne2
            Return View(PorteFeuilleVM)
        End Function

        ' POST: /JournalCaisse/Edit/5
        'Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        'plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        <LocalizedAuthorize(Roles:="CHEFCOLLECTEUR")>
        Function Edit(ByVal PorteFeuille As PorteFeuilleViewModel) As ActionResult
            If ModelState.IsValid Then
                Dim entity = PorteFeuille.getEntity()
                entity.Etat = 1
                entity.UserId = getCurrentUser.Id

                db.Entry(entity).State = EntityState.Modified

                Try
                    db.SaveChanges()
                    Return RedirectToAction("Index")
                Catch ex As DbEntityValidationException
                    Util.GetError(ex, ModelState)
                Catch ex As Exception
                    Util.GetError(ex, ModelState)
                End Try
            End If

            Dim userAgenceId = getCurrentUser.Personne.AgenceId
            Dim listPersonne = db.Personnes.OfType(Of Collecteur).Where(Function(e) e.AgenceId = userAgenceId).ToList
            Dim listPersonne2 As New List(Of SelectListItem)
            For Each item In listPersonne
                listPersonne2.Add(New SelectListItem With {.Value = item.Id, .Text = item.Nom & " " & item.Prenom})
            Next
            PorteFeuille.IDsCollecteur = listPersonne2

            Return View(PorteFeuille)
        End Function

        '' GET: /JournalCaisse/Delete/5
        '<LocalizedAuthorize(Roles:="CHEFCOLLECTEUR")>
        'Function Delete(ByVal id As Long?) As ActionResult
        '    If IsNothing(id) Then
        '        Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
        '    End If
        '    Dim journalcaisse As JournalCaisse = db.JournalCaisses.Find(id)
        '    If IsNothing(journalcaisse) Then
        '        Return HttpNotFound()
        '    End If
        '    Return View(journalcaisse)
        'End Function

        ' POST: /JournalCaisse/Delete/5
        <HttpPost()>
        <ActionName("Delete")>
        <ValidateAntiForgeryToken()>
        <LocalizedAuthorize(Roles:="CHEFCOLLECTEUR")>
        Function DeleteConfirmed(ByVal id As Long) As ActionResult
            Dim PorteFeuille As PorteFeuille = db.PorteFeuilles.Find(id)
            db.PorteFeuilles.Remove(PorteFeuille)
            Try
                db.SaveChanges()
                Return RedirectToAction("Index")
            Catch ex As DbEntityValidationException
                Util.GetError(ex, ModelState)
            Catch ex As Exception
                Util.GetError(ex, ModelState)
            End Try
            Return View()
        End Function

        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If (disposing) Then
                db.Dispose()
            End If
            MyBase.Dispose(disposing)
        End Sub
    End Class
End Namespace
