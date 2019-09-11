Imports System.Data.Entity
Imports System.Net
Imports PagedList
Imports Microsoft.AspNet.Identity
Imports System.Data.Entity.Validation

Namespace TeamCollect
    Public Class GrilleController
        Inherits BaseController

        Private db As New ApplicationDbContext

        Private Function getCurrentUser() As ApplicationUser
            Dim id = User.Identity.GetUserId
            Dim aspuser = db.Users.Find(id)
            Return aspuser
        End Function

        <LocalizedAuthorize(Roles:="SA,ADMINISTRATEUR")>
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

            Dim entities = From e In db.Grilles.OfType(Of Grille)().ToList



            If Not String.IsNullOrEmpty(searchString) Then
                entities = entities.Where(Function(e) e.Libelle.ToUpper.Contains(searchString.ToUpper))
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
        <LocalizedAuthorize(Roles:="SA,ADMINISTRATEUR")>
        Function Create() As ActionResult
            Dim gVM As New GrilleViewModel

            Return View(gVM)
        End Function

        ' POST: /Societe/Create
        'Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        'plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        <LocalizedAuthorize(Roles:="SA,ADMINISTRATEUR")>
        Function Create(ByVal gVM As GrilleViewModel) As ActionResult
            If ModelState.IsValid Then
                Dim scte = From e In db.Societes.OfType(Of Societe).ToList

                gVM.Etat = False
                gVM.SocieteId = scte.First.Id
                gVM.DateCreation = Now.Date
                Dim grille = gVM.getEntity()
                grille.UserId = getCurrentUser.Id

                db.Grilles.Add(grille)
                Try
                    db.SaveChanges()
                    Return RedirectToAction("Index")
                Catch ex As DbEntityValidationException
                    Util.GetError(ex, ModelState)
                Catch ex As Exception
                    Util.GetError(ex, ModelState)
                End Try
            End If
            Return View(gVM)
        End Function

        ' GET: /Societe/Edit/5
        <LocalizedAuthorize(Roles:="SA,ADMINISTRATEUR")>
        Function Edit(ByVal id As Long?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim grille As Grille = db.Grilles.Find(id)
            If IsNothing(grille) Then
                Return HttpNotFound()
            End If
            Dim gVM = New GrilleViewModel(grille)
            Return View(gVM)
        End Function

        ' POST: /Societe/Edit/5
        'Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        'plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        <LocalizedAuthorize(Roles:="SA,ADMINISTRATEUR")>
        Function Edit(ByVal gVM As GrilleViewModel) As ActionResult
            If ModelState.IsValid Then
                Dim entity = gVM.getEntity()
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
            Return View(gVM)
        End Function

        ' GET: /Societe/Delete/5
        <LocalizedAuthorize(Roles:="SA,ADMINISTRATEUR")>
        Function Delete(ByVal id As Long?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim grille As Grille = db.Grilles.Find(id)
            If IsNothing(grille) Then
                Return HttpNotFound()
            End If
            Return View(grille)
        End Function

        ' POST: /Societe/Delete/5
        <HttpPost()>
        <ActionName("Delete")>
        <ValidateAntiForgeryToken()>
        <LocalizedAuthorize(Roles:="SA,ADMINISTRATEUR")>
        Function DeleteConfirmed(ByVal id As Long) As ActionResult
            Dim grille As Grille = db.Grilles.Find(id)
            db.Grilles.Remove(grille)
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
