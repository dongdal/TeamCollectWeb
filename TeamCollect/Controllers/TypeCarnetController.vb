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
    Public Class TypeCarnetController
        Inherits BaseController

        Private db As New ApplicationDbContext

        ' GET: TypeCarnet
        <LocalizedAuthorize(Roles:="ADMINISTRATEUR")>
        Function Index(sortOrder As String, currentFilter As String, searchString As String, page As Integer?) As ActionResult
            ViewBag.CurrentSort = sortOrder

            If Not String.IsNullOrEmpty(searchString) Then
                page = 1
            Else
                searchString = currentFilter
            End If

            ViewBag.CurrentFilter = searchString
            Dim entities = db.TypeCarnets.ToList

            If Not String.IsNullOrEmpty(searchString) Then
                entities = entities.Where(Function(e) e.Libelle.ToUpper.Contains(searchString.ToUpper)).ToList
            End If
            ViewBag.EnregCount = entities.Count

            Dim pageSize As Integer = ConfigurationManager.AppSettings("PageSize")
            Dim pageNumber As Integer = If(page, 1)

            Return View(entities.ToPagedList(pageNumber, pageSize))
        End Function

        ' GET: TypeCarnet/Details/5
        <LocalizedAuthorize(Roles:="ADMINISTRATEUR")>
        Function Details(ByVal id As Long?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim typeCarnet As TypeCarnet = db.TypeCarnets.Find(id)
            If IsNothing(typeCarnet) Then
                Return HttpNotFound()
            End If
            Return View(typeCarnet)
        End Function

        ' GET: TypeCarnet/Create
        <LocalizedAuthorize(Roles:="ADMINISTRATEUR")>
        Function Create() As ActionResult

            Return View()
        End Function

        ' POST: TypeCarnet/Create
        'Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        'plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        <LocalizedAuthorize(Roles:="ADMINISTRATEUR")>
        Function Create(<Bind(Include:="Id,Libelle,Prix,Etat")> ByVal typeCarnet As TypeCarnetViewModel) As ActionResult
            If ModelState.IsValid Then
                Dim jrn = typeCarnet.getEntity()
                db.TypeCarnets.Add(jrn)
                db.SaveChanges()
                Return RedirectToAction("Index")
            End If
            Return View(typeCarnet)
        End Function

        ' GET: TypeCarnet/Edit/5
        <LocalizedAuthorize(Roles:="ADMINISTRATEUR")>
        Function Edit(ByVal id As Long?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim typeCarnet As TypeCarnet = db.TypeCarnets.Find(id)
            If IsNothing(typeCarnet) Then
                Return HttpNotFound()
            End If
            Dim journalVM = New TypeCarnetViewModel(typeCarnet)
            Return View(journalVM)
        End Function

        ' POST: TypeCarnet/Edit/5
        'Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        'plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        <LocalizedAuthorize(Roles:="ADMINISTRATEUR")>
        Function Edit(<Bind(Include:="Id,Libelle,Prix,Etat,DateCreation")> ByVal typeCarnet As TypeCarnetViewModel) As ActionResult
            If ModelState.IsValid Then
                Dim jrn = typeCarnet.getEntity()
                db.Entry(jrn).State = EntityState.Modified
                db.SaveChanges()
                Return RedirectToAction("Index")
            End If
            Return View(typeCarnet)
        End Function

        ' GET: TypeCarnet/Delete/5
        <LocalizedAuthorize(Roles:="ADMINISTRATEUR")>
        Function Delete(ByVal id As Long?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim typeCarnet As TypeCarnet = db.TypeCarnets.Find(id)
            If IsNothing(typeCarnet) Then
                Return HttpNotFound()
            End If
            Return View(typeCarnet)
        End Function

        ' POST: TypeCarnet/Delete/5
        <HttpPost()>
        <ActionName("Delete")>
        <ValidateAntiForgeryToken()>
        Function DeleteConfirmed(ByVal id As Long) As ActionResult
            Dim typeCarnet As TypeCarnet = db.TypeCarnets.Find(id)
            db.TypeCarnets.Remove(typeCarnet)
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
