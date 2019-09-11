Imports System.Data.Entity
Imports System.Data.Entity.Validation
Imports System.Net
Imports Microsoft.AspNet.Identity
Imports PagedList

Namespace Controllers
    Public Class CategorieRemunerationsController
        Inherits BaseController

        Private db As New ApplicationDbContext

        Private Function GetCurrentUser() As ApplicationUser
            Dim id = User.Identity.GetUserId
            Dim aspuser = db.Users.Find(id)
            Return aspuser
        End Function

        ' GET: CategorieRemunerations
        <LocalizedAuthorize(Roles:="SA,ADMINISTRATEUR,MANAGER")>
        Function Index(sortOrder As String, currentFilter As String, searchString As String, page As Integer?) As ActionResult
            ViewBag.CurrentSort = sortOrder

            If Not String.IsNullOrEmpty(searchString) Then
                page = 1
            Else
                searchString = currentFilter
            End If

            ViewBag.CurrentFilter = searchString

            Dim entities = (From e In db.CategorieRemunerations Where e.StatutExistant = True).ToList

            If Not String.IsNullOrEmpty(searchString) Then
                entities = entities.Where(Function(e) e.Libelle.ToUpper.Contains(searchString.ToUpper))
            End If
            ViewBag.EnregCount = entities.Count

            Dim pageSize As Integer = ConfigurationManager.AppSettings("PageSize")
            Dim pageNumber As Integer = If(page, 1)

            Return View(entities.ToPagedList(pageNumber, pageSize))

        End Function

        ' GET: CategorieRemunerations/Details/5
        <LocalizedAuthorize(Roles:="SA,ADMINISTRATEUR,MANAGER")>
        Function Details(ByVal id As Long?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim categorieRemuneration As CategorieRemuneration = db.CategorieRemunerations.Find(id)
            If IsNothing(categorieRemuneration) Then
                Return HttpNotFound()
            End If
            Return View(New CategorieRemunerationViewModel(categorieRemuneration))
        End Function

        Public Sub LoadComboBox(ByVal entityVM As CategorieRemunerationViewModel)
            Dim AspNetUser = (From e In db.Users Select e).ToList
            Dim LesUtilisateurs As New List(Of SelectListItem)
            For Each item In AspNetUser
                If String.IsNullOrEmpty(item.Personne.Prenom) Then
                    LesUtilisateurs.Add(New SelectListItem With {.Value = item.Id, .Text = item.Personne.Nom})
                Else
                    LesUtilisateurs.Add(New SelectListItem With {.Value = item.Id, .Text = item.Personne.Nom & " | " & item.Personne.Prenom})
                End If
            Next
            'kLesUtilisateurs.Add(New SelectListItem With {.Value = item.Id, .Text = item.Libelle & " -- " & String.Format("{0:0,0}", item.SalaireDeBase) & "F CFA"})

            entityVM.LesUtilisateurs = LesUtilisateurs
        End Sub

        ' GET: CategorieRemunerations/Create
        <LocalizedAuthorize(Roles:="SA,ADMINISTRATEUR,MANAGER")>
        Function Create() As ActionResult
            Dim entityVM As New CategorieRemunerationViewModel()
            LoadComboBox(entityVM)
            Return View(entityVM)
        End Function

        ' POST: CategorieRemunerations/Create
        'Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        'plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        <LocalizedAuthorize(Roles:="SA,ADMINISTRATEUR,MANAGER")>
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function Create(ByVal entityVM As CategorieRemunerationViewModel) As ActionResult
            entityVM.UserId = GetCurrentUser.Id
            If ModelState.IsValid Then
                db.CategorieRemunerations.Add(entityVM.GetEntity)
                Try
                    db.SaveChanges()
                    Return RedirectToAction("Index")
                Catch ex As DbEntityValidationException
                    Util.GetError(ex, ModelState)
                Catch ex As Exception
                    Util.GetError(ex, ModelState)
                End Try
            End If
            LoadComboBox(entityVM)
            Return View(entityVM)
        End Function

        ' GET: CategorieRemunerations/Edit/5
        <LocalizedAuthorize(Roles:="SA,ADMINISTRATEUR,MANAGER")>
        Function Edit(ByVal id As Long?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim categorieRemuneration As CategorieRemuneration = db.CategorieRemunerations.Find(id)
            If IsNothing(categorieRemuneration) Then
                Return HttpNotFound()
            End If
            Dim entityVM As New CategorieRemunerationViewModel(categorieRemuneration)
            LoadComboBox(entityVM)
            Return View(entityVM)
        End Function

        ' POST: CategorieRemunerations/Edit/5
        'Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        'plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        <LocalizedAuthorize(Roles:="SA,ADMINISTRATEUR,MANAGER")>
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function Edit(ByVal entityVM As CategorieRemunerationViewModel) As ActionResult
            If ModelState.IsValid Then
                db.Entry(entityVM.GetEntity).State = EntityState.Modified
                Try
                    db.SaveChanges()
                    Return RedirectToAction("Index")
                Catch ex As DbEntityValidationException
                    Util.GetError(ex, ModelState)
                Catch ex As Exception
                    Util.GetError(ex, ModelState)
                End Try
            End If
            LoadComboBox(entityVM)
            Return View(entityVM)
        End Function

        ' GET: CategorieRemunerations/Delete/5
        <LocalizedAuthorize(Roles:="SA,ADMINISTRATEUR,MANAGER")>
        Function Delete(ByVal id As Long?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim categorieRemuneration As CategorieRemuneration = db.CategorieRemunerations.Find(id)
            If IsNothing(categorieRemuneration) Then
                Return HttpNotFound()
            End If
            Dim entityVM As New CategorieRemunerationViewModel(categorieRemuneration)
            LoadComboBox(entityVM)
            Return View(entityVM)
        End Function

        ' POST: CategorieRemunerations/Delete/5
        <LocalizedAuthorize(Roles:="SA,ADMINISTRATEUR,MANAGER")>
        <HttpPost()>
        <ActionName("Delete")>
        <ValidateAntiForgeryToken()>
        Function DeleteConfirmed(ByVal id As Long) As ActionResult
            Dim categorieRemuneration As CategorieRemuneration = db.CategorieRemunerations.Find(id)
            db.CategorieRemunerations.Remove(categorieRemuneration)
            Try
                db.SaveChanges()
                Return RedirectToAction("Index")
            Catch ex As DbEntityValidationException
                Util.GetError(ex, ModelState)
            Catch ex As Exception
                Util.GetError(ex, ModelState)
            End Try
            Return View(New CategorieRemunerationViewModel(categorieRemuneration))
        End Function

        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If (disposing) Then
                db.Dispose()
            End If
            MyBase.Dispose(disposing)
        End Sub
    End Class
End Namespace
