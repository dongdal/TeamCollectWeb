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
    Public Class SocieteController
        Inherits BaseController

        Private db As New ApplicationDbContext

        Private Function getCurrentUser() As ApplicationUser
            Dim id = User.Identity.GetUserId
            Dim aspuser = db.Users.Find(id)
            Return aspuser
        End Function


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

            Dim entities = From e In db.Societes.OfType(Of Societe)().ToList



            If Not String.IsNullOrEmpty(searchString) Then
                entities = entities.Where(Function(e) e.Libelle.ToUpper.Contains(searchString.ToUpper) Or e.BP.ToUpper.Contains(searchString.ToUpper) Or e.Telephone.ToUpper.Contains(searchString.ToUpper) Or e.Email.ToUpper.Contains(searchString.ToUpper) Or e.Adresse.ToUpper.Contains(searchString.ToUpper))
            End If
            ViewBag.EnregCount = entities.Count

            Dim pageSize As Integer = ConfigurationManager.AppSettings("PageSize")
            Dim pageNumber As Integer = If(page, 1)

            Return View(entities.ToPagedList(pageNumber, pageSize))
        End Function

        ' GET: /Societe/Details/5
        Function Details(ByVal id As Long?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim societe As Societe = db.Societes.Find(id)
            If IsNothing(societe) Then
                Return HttpNotFound()
            End If
            Return View(societe)
        End Function

        ' GET: /Societe/Create
        <LocalizedAuthorize(Roles:="ADMINISTRATEUR")>
        Function Create() As ActionResult
            Dim scteVM As New SocieteViewModel

            Return View(scteVM)
        End Function

        ' POST: /Societe/Create
        'Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        'plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        <LocalizedAuthorize(Roles:="ADMINISTRATEUR")>
        Function Create(ByVal societeVM As SocieteViewModel) As ActionResult
            If ModelState.IsValid Then
                societeVM.Etat = False
                societeVM.DateCreation = Now.Date
                If (societeVM.MinCollecte > societeVM.MAxCollecte) Then
                    Dim tempo = societeVM.MinCollecte
                    societeVM.MinCollecte = societeVM.MAxCollecte
                    societeVM.MAxCollecte = tempo
                End If
                Dim SCTE = societeVM.getEntity()
                SCTE.UserId = getCurrentUser.Id

                db.Societes.Add(SCTE)
                Try
                    db.SaveChanges()
                    Return RedirectToAction("Index")
                Catch ex As DbEntityValidationException
                    Util.GetError(ex, ModelState)
                Catch ex As Exception
                    Util.GetError(ex, ModelState)
                End Try
            End If
            Return View(societeVM)
        End Function

        ' GET: /Societe/Edit/5
        <LocalizedAuthorize(Roles:="ADMINISTRATEUR")>
        Function Edit(ByVal id As Long?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim societe As Societe = db.Societes.Find(id)
            If IsNothing(societe) Then
                Return HttpNotFound()
            End If
            Dim SocieteVM = New SocieteViewModel(societe)
            Return View(SocieteVM)
        End Function

        ' POST: /Societe/Edit/5
        'Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        'plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        <LocalizedAuthorize(Roles:="ADMINISTRATEUR")>
        Function Edit(ByVal societe As SocieteViewModel) As ActionResult
            If ModelState.IsValid Then
                Dim entity = societe.getEntity()
                entity.UserId = getCurrentUser.Id
                If (societe.MinCollecte > societe.MAxCollecte) Then
                    Dim tempo = societe.MinCollecte
                    societe.MinCollecte = societe.MAxCollecte
                    societe.MAxCollecte = tempo
                End If
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
            Return View(societe)
        End Function

        ' GET: /Societe/Delete/5
        <LocalizedAuthorize(Roles:="ADMINISTRATEUR")>
        Function Delete(ByVal id As Long?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim societe As Societe = db.Societes.Find(id)
            If IsNothing(societe) Then
                Return HttpNotFound()
            End If
            Return View(societe)
        End Function

        ' POST: /Societe/Delete/5
        <HttpPost()>
        <ActionName("Delete")>
        <ValidateAntiForgeryToken()>
        <LocalizedAuthorize(Roles:="ADMINISTRATEUR")>
        Function DeleteConfirmed(ByVal id As Long) As ActionResult
            Dim societe As Societe = db.Societes.Find(id)
            db.Societes.Remove(societe)
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
