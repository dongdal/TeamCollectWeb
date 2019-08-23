Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.Entity
Imports System.Linq
Imports System.Net
Imports System.Web
Imports System.Web.Mvc
Imports PagedList
Imports TeamCollect

Namespace Controllers
    Public Class SecteurActiviteController
        Inherits BaseController

        Private db As New ApplicationDbContext

        ' GET: SecteurActivite
        <LocalizedAuthorize(Roles:="ADMINISTRATEUR")>
        Function Index(sortOrder As String, currentFilter As String, searchString As String, page As Integer?) As ActionResult

            ViewBag.CurrentSort = sortOrder

            If Not String.IsNullOrEmpty(searchString) Then
                page = 1
            Else
                searchString = currentFilter
            End If

            ViewBag.CurrentFilter = searchString

            Dim entities = From e In db.SecteurActivites.OfType(Of SecteurActivite).ToList
            If Not String.IsNullOrEmpty(searchString) Then
                entities = entities.Where(Function(e) e.Libelle.ToUpper.Contains(searchString.ToUpper))
            End If
            ViewBag.EnregCount = entities.Count

            Dim pageSize As Integer = ConfigurationManager.AppSettings("PageSize")
            Dim pageNumber As Integer = If(page, 1)

            Return View(entities.ToPagedList(pageNumber, pageSize))
        End Function

        ' GET: SecteurActivite/Details/5
        <LocalizedAuthorize(Roles:="ADMINISTRATEUR")>
        Function Details(ByVal id As Long?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim secteurActivite As SecteurActivite = db.SecteurActivites.Find(id)
            If IsNothing(secteurActivite) Then
                Return HttpNotFound()
            End If
            Return View(secteurActivite)
        End Function

        ' GET: SecteurActivite/Create
        <LocalizedAuthorize(Roles:="ADMINISTRATEUR")>
        Function Create() As ActionResult
            Return View()
        End Function

        ' POST: SecteurActivite/Create
        'Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        'plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        <LocalizedAuthorize(Roles:="ADMINISTRATEUR")>
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function Create(<Bind(Include:="Id,Libelle,Etat,DateCreation")> ByVal secteurActivite As SecteurActiviteViewModel) As ActionResult
            If ModelState.IsValid Then
                Dim sec = secteurActivite.getEntity()
                db.SecteurActivites.Add(sec)
                db.SaveChanges()
                Return RedirectToAction("Index")
            End If
            Return View(secteurActivite)
        End Function

        ' GET: SecteurActivite/Edit/5
        <LocalizedAuthorize(Roles:="ADMINISTRATEUR")>
        Function Edit(ByVal id As Long?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim secteurActivite As SecteurActivite = db.SecteurActivites.Find(id)
            If IsNothing(secteurActivite) Then
                Return HttpNotFound()
            End If
            Dim sec = New SecteurActiviteViewModel(secteurActivite)
            Return View(sec)
        End Function

        ' POST: SecteurActivite/Edit/5
        'Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        'plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        <LocalizedAuthorize(Roles:="ADMINISTRATEUR")>
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function Edit(<Bind(Include:="Id,Libelle,Etat,DateCreation")> ByVal secteurActivite As SecteurActiviteViewModel) As ActionResult
            If ModelState.IsValid Then
                Dim sec = secteurActivite.getEntity()
                db.Entry(sec).State = EntityState.Modified
                db.SaveChanges()
                Return RedirectToAction("Index")
            End If
            Return View(secteurActivite)
        End Function

        ' GET: SecteurActivite/Delete/5
        <LocalizedAuthorize(Roles:="ADMINISTRATEUR")>
        Function Delete(ByVal id As Long?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim secteurActivite As SecteurActivite = db.SecteurActivites.Find(id)
            If IsNothing(secteurActivite) Then
                Return HttpNotFound()
            End If
            Return View(secteurActivite)
        End Function

        ' POST: SecteurActivite/Delete/5
        <LocalizedAuthorize(Roles:="ADMINISTRATEUR")>
        <HttpPost()>
        <ActionName("Delete")>
        <ValidateAntiForgeryToken()>
        Function DeleteConfirmed(ByVal id As Long) As ActionResult
            Dim secteurActivite As SecteurActivite = db.SecteurActivites.Find(id)
            db.SecteurActivites.Remove(secteurActivite)
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
