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
    Public Class BorneCommissionController
        Inherits BaseController

        Private db As New ApplicationDbContext

        ' GET: BorneCommission
        Function Index(sortOrder As String, currentFilter As String, searchString As String, page As Integer?) As ActionResult
            ViewBag.CurrentSort = sortOrder

            If Not String.IsNullOrEmpty(searchString) Then
                page = 1
            Else
                searchString = currentFilter
            End If

            ViewBag.CurrentFilter = searchString

            Dim entities = db.BorneCommissions.ToList
            ViewBag.EnregCount = entities.Count

            Dim pageSize As Integer = ConfigurationManager.AppSettings("PageSize")
            Dim pageNumber As Integer = If(page, 1)

            Return View(entities.ToPagedList(pageNumber, pageSize))
        End Function

        ' GET: BorneCommission/Details/5
        Function Details(ByVal id As Long?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim borneCommission As BorneCommission = db.BorneCommissions.Find(id)
            If IsNothing(borneCommission) Then
                Return HttpNotFound()
            End If
            Return View(borneCommission)
        End Function

        ' GET: BorneCommission/Create
        Function Create() As ActionResult
            Return View()
        End Function

        ' POST: BorneCommission/Create
        'Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        'plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function Create(<Bind(Include:="Id,BornInf,BornSup")> ByVal borneCommission As BorneCommissionViewModel) As ActionResult
            If ModelState.IsValid Then
                Dim ca = borneCommission.getEntity
                db.BorneCommissions.Add(ca)
                db.SaveChanges()
                Return RedirectToAction("Index")
            End If
            Return View(borneCommission)
        End Function

        ' GET: BorneCommission/Edit/5
        Function Edit(ByVal id As Long?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim borneCommission As BorneCommission = db.BorneCommissions.Find(id)
            If IsNothing(borneCommission) Then
                Return HttpNotFound()
            End If
            Dim ca = New BorneCommissionViewModel(borneCommission)

            Return View(ca)
        End Function

        ' POST: BorneCommission/Edit/5
        'Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        'plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function Edit(<Bind(Include:="Id,BornInf,BornSup")> ByVal borneCommission As BorneCommissionViewModel) As ActionResult
            If ModelState.IsValid Then
                Dim ca = borneCommission.getEntity
                db.Entry(ca).State = EntityState.Modified
                db.SaveChanges()
                Return RedirectToAction("Index")
            End If
            Return View(borneCommission)
        End Function

        ' GET: BorneCommission/Delete/5
        Function Delete(ByVal id As Long?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim borneCommission As BorneCommission = db.BorneCommissions.Find(id)
            If IsNothing(borneCommission) Then
                Return HttpNotFound()
            End If
            Return View(borneCommission)
        End Function

        ' POST: BorneCommission/Delete/5
        <HttpPost()>
        <ActionName("Delete")>
        <ValidateAntiForgeryToken()>
        Function DeleteConfirmed(ByVal id As Long) As ActionResult
            Dim borneCommission As BorneCommission = db.BorneCommissions.Find(id)
            db.BorneCommissions.Remove(borneCommission)
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
