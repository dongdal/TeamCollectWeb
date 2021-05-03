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

Namespace TFCenter
    Public Class PersonneController
        Inherits BaseController

        Private db As New ApplicationDbContext

        Private Function GetCurrentUser() As ApplicationUser
            Dim id = User.Identity.GetUserId
            Dim aspuser = db.Users.Find(id)
            Return aspuser
        End Function

        ' GET: /Personne/
        <LocalizedAuthorize(Roles:="ADMINISTRATEUR")>
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

            Dim CLT = (From e In db.Personnes.OfType(Of Client)).ToList()
            'Dim COL = From e In db.Personnes.OfType(Of Collecteur).ToList

            'Dim entities = From e In db.Personnes.OfType(Of Personne).Where(Function(p) p.Id = -1 Or (Not CLT.Contains(p.Id) And Not COL.Contains(p.Id) And p.Nom <> "sa")).ToList

            Dim entities = (From e In db.Personnes.Where(Function(p) p.Nom <> "sa")).ToList()
            entities = entities.Except(CLT).ToList()

            If Not String.IsNullOrEmpty(searchString) Then
                entities = entities.Where(Function(e) e.Nom.ToUpper.Contains(searchString.ToUpper) Or e.Prenom.ToUpper.Contains(searchString.ToUpper) Or e.Sexe.ToUpper.Contains(searchString.ToUpper) Or e.Telephone.ToUpper.Contains(searchString.ToUpper) Or e.Adresse.ToUpper.Contains(searchString.ToUpper) Or e.Quartier.ToUpper.Contains(searchString.ToUpper))
            End If
            ViewBag.EnregCount = entities.Count

            Dim pageSize As Integer = ConfigurationManager.AppSettings("PageSize")
            Dim pageNumber As Integer = If(page, 1)

            Return View(entities.ToPagedList(pageNumber, pageSize))
        End Function

        ' GET: /Personne/Details/5
        <LocalizedAuthorize(Roles:="ADMINISTRATEUR")>
        Function Details(ByVal id As Long?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim personne As Personne = db.Personnes.Find(id)
            If IsNothing(personne) Then
                Return HttpNotFound()
            End If
            Return View(personne)
        End Function

        Private Sub LoadComboBox(entityVM As PersonneViewModel)
            Dim SecteurActivites = (From sectAct In db.SecteurActivites Select sectAct).OrderBy(Function(e) e.Libelle)
            Dim LesSecteurActivites As New List(Of SelectListItem)
            For Each item In SecteurActivites
                LesSecteurActivites.Add(New SelectListItem With {.Value = item.Id, .Text = item.Libelle})
            Next

            Dim Agences = (From agenc In db.Agences Select agenc).OrderBy(Function(e) e.Libelle)
            Dim LesAgences As New List(Of SelectListItem)
            For Each item In Agences
                LesAgences.Add(New SelectListItem With {.Value = item.Id, .Text = item.Libelle})
            Next

            entityVM.LesSecteursActivite = LesSecteurActivites
            entityVM.IDsAgence = LesAgences

        End Sub

        ' GET: /Personne/Create
        <LocalizedAuthorize(Roles:="ADMINISTRATEUR")>
        Function Create() As ActionResult
            Dim pVM As New PersonneViewModel

            LoadComboBox(pVM)
            Return View(pVM)
        End Function

        ' POST: /Personne/Create
        'Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        'plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        <LocalizedAuthorize(Roles:="ADMINISTRATEUR")>
        Function Create(ByVal personne As PersonneViewModel) As ActionResult
            If ModelState.IsValid Then
                personne.Etat = False
                personne.DateCreation = Now.Date
                Dim pers = personne.getEntity()
                pers.UserId = GetCurrentUser.Id

                db.Personnes.Add(pers)
                Try
                    db.SaveChanges()
                    Return RedirectToAction("Index")
                Catch ex As DbEntityValidationException
                    Util.GetError(ex, ModelState)
                Catch ex As Exception
                    Util.GetError(ex, ModelState)
                End Try
            End If
            LoadComboBox(personne)
            Return View(personne)
        End Function

        ' GET: /Personne/Edit/5
        <LocalizedAuthorize(Roles:="ADMINISTRATEUR")>
        Function Edit(ByVal id As Long?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim personne As Personne = db.Personnes.Find(id)
            If IsNothing(personne) Then
                Return HttpNotFound()
            End If
            Dim personneVM = New PersonneViewModel(personne)

            LoadComboBox(personneVM)

            Return View(personneVM)
        End Function

        ' POST: /Personne/Edit/5
        'Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        'plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        <LocalizedAuthorize(Roles:="ADMINISTRATEUR")>
        Function Edit(ByVal personne As PersonneViewModel) As ActionResult
            If ModelState.IsValid Then
                Dim entity = personne.getEntity()
                entity.UserId = GetCurrentUser.Id

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

            LoadComboBox(personne)

            Return View(personne)
        End Function

        ' GET: /Personne/Delete/5
        <LocalizedAuthorize(Roles:="ADMINISTRATEUR")>
        Function Delete(ByVal id As Long?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim personne As Personne = db.Personnes.Find(id)
            If IsNothing(personne) Then
                Return HttpNotFound()
            End If
            Return View(personne)
        End Function

        ' POST: /Personne/Delete/5
        <HttpPost()>
        <ActionName("Delete")>
        <ValidateAntiForgeryToken()>
        <LocalizedAuthorize(Roles:="ADMINISTRATEUR")>
        Function DeleteConfirmed(ByVal id As Long) As ActionResult
            Dim personne As Personne = db.Personnes.Find(id)
            db.Personnes.Remove(personne)
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
