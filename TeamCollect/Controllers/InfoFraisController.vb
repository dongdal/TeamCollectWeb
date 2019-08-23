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
    Public Class InfoFraisController
        Inherits BaseController

        Private db As New ApplicationDbContext

        ' GET: /InfoFrais/

        <LocalizedAuthorize(Roles:="ADMINISTRATEUR")>
        Function Index(page As Integer?, GrilleId As Long?, Libelle As String) As ActionResult


            Dim entities = From e In db.InfoFrais.Include(Function(i) i.Grille).Where(Function(i) i.GrilleId = GrilleId).OrderBy(Function(i) i.BornInf).ToList


            ViewBag.EnregCount = entities.Count
            Dim pageSize As Integer = ConfigurationManager.AppSettings("PageSize")
            Dim pageNumber As Integer = If(page, 1)

            ViewBag.GrilleId = GrilleId
            ViewBag.Libelle = Libelle

            Return View(entities.ToPagedList(pageNumber, pageSize))
        End Function


        ' GET: /InfoFrais/Details/5
        Function Details(ByVal id As Long?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim infofrais As InfoFrais = db.InfoFrais.Find(id)
            If IsNothing(infofrais) Then
                Return HttpNotFound()
            End If
            Return View(infofrais)
        End Function

        ' GET: /InfoFrais/Create
        <LocalizedAuthorize(Roles:="ADMINISTRATEUR")>
        Function Create(ByVal GrilleId As Long) As ActionResult
            Dim infoFraisVM As New InfoFraisViewModel
                Dim Grille As Grille = db.Grilles.Find(GrilleId)
            infoFraisVM.GrilleId = GrilleId

            ViewBag.Libelle = Grille.Libelle
            infoFraisVM.LibelleDelaGrille = Grille.Libelle

            Dim CurentFrais = (From i In db.InfoFrais.OfType(Of InfoFrais).OrderBy(Function(i) i.BornSup)).ToList.LastOrDefault
            If Not (IsNothing(CurentFrais)) Then
                infoFraisVM.BornInf = (CurentFrais.BornSup + 1)
            Else
                infoFraisVM.BornInf = 0
            End If
            Return View(infoFraisVM)
        End Function

        ' POST: /InfoFrais/Create
        'Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        'plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        <LocalizedAuthorize(Roles:="ADMINISTRATEUR")>
        Function Create(ByVal infofraisVM As InfoFraisViewModel) As ActionResult
            If ModelState.IsValid Then
                Dim Inff = infofraisVM.getEntity()
                If (Inff.BornInf > Inff.BornSup) Then
                    ModelState.AddModelError("", "Votre interval de facturation est incohérent!")
                Else
                    Dim curentBorneInf = Inff.BornInf
                    Dim verif = (From v In db.InfoFrais.OfType(Of InfoFrais).Where(Function(i) i.BornSup >= curentBorneInf)).Count
                    If (verif = 0) Then
                        db.InfoFrais.Add(Inff)
                        Try
                            db.SaveChanges()
                            Return RedirectToAction("Index", New With {.GrilleId = Inff.GrilleId})
                        Catch ex As DbEntityValidationException
                            Util.GetError(ex, ModelState)
                        Catch ex As Exception
                            Util.GetError(ex, ModelState)
                        End Try
                    Else
                        ModelState.AddModelError("", "La plage pour laquelle vous voulez faire une facturation existe déjà!")
                    End If
                End If
               
            End If
            Return View(infofraisVM)
        End Function

        ' GET: /InfoFrais/Edit/5
        <LocalizedAuthorize(Roles:="ADMINISTRATEUR")>
        Function Edit(ByVal id As Long?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim infofrais As InfoFrais = db.InfoFrais.Find(id)
            If IsNothing(infofrais) Then
                Return HttpNotFound()
            End If
            Dim gVM = New InfoFraisViewModel(infofrais)
            Return View(gVM)
        End Function

        ' POST: /InfoFrais/Edit/5
        'Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        'plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        <LocalizedAuthorize(Roles:="ADMINISTRATEUR")>
        Function Edit(ByVal infofraisVM As InfoFraisViewModel) As ActionResult
            If ModelState.IsValid Then

                Dim Inff = infofraisVM.getEntity()
                If (Inff.BornInf > Inff.BornSup) Then
                    ModelState.AddModelError("", "Votre interval de facturation est incohérent!")
                Else
                    Dim curentBorneInf = Inff.BornInf
                    Dim verif = (From v In db.InfoFrais.OfType(Of InfoFrais).Where(Function(i) i.BornSup >= curentBorneInf)).Count
                    If (verif = 0) Then
                        db.InfoFrais.Add(Inff)
                        db.Entry(Inff).State = EntityState.Modified

                        Try
                            db.SaveChanges()
                            Return RedirectToAction("Index", New With {.GrilleId = Inff.GrilleId})
                        Catch ex As DbEntityValidationException
                            Util.GetError(ex, ModelState)
                        Catch ex As Exception
                            Util.GetError(ex, ModelState)
                        End Try
                    Else
                        ModelState.AddModelError("", "La plage pour laquelle vous voulez faire une facturation existe déjà!")
                    End If
                End If


            End If
            Return View(infofraisVM)
        End Function

        ' GET: /InfoFrais/Delete/5
        Function Delete(ByVal id As Long?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim infofrais As InfoFrais = db.InfoFrais.Find(id)
            If IsNothing(infofrais) Then
                Return HttpNotFound()
            End If
            Return View(infofrais)
        End Function

        ' POST: /InfoFrais/Delete/5
        <HttpPost()>
        <ActionName("Delete")>
        <ValidateAntiForgeryToken()>
        Function DeleteConfirmed(ByVal id As Long) As ActionResult
            Dim infofrais As InfoFrais = db.InfoFrais.Find(id)
            db.InfoFrais.Remove(infofrais)
            Try
                db.SaveChanges()
                Return RedirectToAction("Index", New With {.GrilleId = infofrais.GrilleId})
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
