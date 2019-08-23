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
    Public Class InfoCompensationController
        Inherits BaseController

        Private db As New ApplicationDbContext

        Private Function getCurrentUser() As ApplicationUser
            Dim id = User.Identity.GetUserId
            Dim aspuser = db.Users.Find(id)
            Return aspuser
        End Function

        ' GET: /InfoCompensation/
        Function Index() As ActionResult
            Dim infocompensation = db.InfoCompensation.Include(Function(i) i.JournalCaisse).Include(Function(i) i.User)
            Return View(infocompensation.ToList())
        End Function


        ' GET: /InfoCompensation/Details/5
        Function Details(ByVal id As Long?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim infocompensation As InfoCompensation = db.InfoCompensation.Find(id)
            If IsNothing(infocompensation) Then
                Return HttpNotFound()
            End If
            Return View(infocompensation)
        End Function

        ' GET: /InfoCompensation/Create
        Function Create() As ActionResult
            
            Return View()
        End Function

        ' POST: /InfoCompensation/Create
        'Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        'plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function Create(<Bind(Include := "Id,JournalCaisseId,Libelle,MontantVerse,Etat,DateCreation,UserId")> ByVal infocompensation As InfoCompensation) As ActionResult
            If ModelState.IsValid Then
                infocompensation.DateCreation = Now.Date
                infocompensation.UserId = getCurrentUser.Id
                db.InfoCompensation.Add(infocompensation)
                db.SaveChanges()
                Return RedirectToAction("Index", "JournalCaisse", New With {.Id = infocompensation.JournalCaisseId})
            End If

            Return View(infocompensation)
        End Function

        ' GET: /InfoCompensation/Edit/5
        Function Edit(ByVal id As Long?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim infocompensation As InfoCompensation = db.InfoCompensation.Find(id)
            If IsNothing(infocompensation) Then
                Return HttpNotFound()
            End If
            Return View(infocompensation)
        End Function

        ' POST: /InfoCompensation/Edit/5
        'Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        'plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function Edit(<Bind(Include := "Id,JournalCaisseId,Libelle,MontantVerse,Etat,DateCreation,UserId")> ByVal infocompensation As InfoCompensation) As ActionResult
            If ModelState.IsValid Then
                db.Entry(infocompensation).State = EntityState.Modified
                db.SaveChanges()
                Return RedirectToAction("Index")
            End If
            Return View(infocompensation)
        End Function

        ' GET: /InfoCompensation/Delete/5
        Function Delete(ByVal id As Long?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim infocompensation As InfoCompensation = db.InfoCompensation.Find(id)
            If IsNothing(infocompensation) Then
                Return HttpNotFound()
            End If
            Return View(infocompensation)
        End Function

        ' POST: /InfoCompensation/Delete/5
        <HttpPost()>
        <ActionName("Delete")>
        <ValidateAntiForgeryToken()>
        Function DeleteConfirmed(ByVal id As Long) As ActionResult
            Dim infocompensation As InfoCompensation = db.InfoCompensation.Find(id)
            db.InfoCompensation.Remove(infocompensation)
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
