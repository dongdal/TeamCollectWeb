Imports System.Threading.Tasks
Imports Microsoft.AspNet.Identity
Imports Microsoft.AspNet.Identity.EntityFramework
Imports Microsoft.Owin.Security
Imports System.Net
Imports PagedList
Imports System.Data.Entity
Imports System.Threading

Public Class AccountController
    Inherits Controller

    Private db As New ApplicationDbContext

    Public Sub New()
        Me.New(New UserManager(Of ApplicationUser)(New UserStore(Of ApplicationUser)(New ApplicationDbContext())))
    End Sub

    Public Sub New(manager As UserManager(Of ApplicationUser))
        UserManager = manager
    End Sub

    Public Property UserManager As UserManager(Of ApplicationUser)


    Protected Overrides Function BeginExecuteCore(callback As AsyncCallback, state As Object) As IAsyncResult
        Dim cultureName As String = TryCast(RouteData.Values("culture"), String)

        ' Attempt to read the culture cookie from Request
        If cultureName Is Nothing Then
            cultureName = If(Request.UserLanguages IsNot Nothing AndAlso Request.UserLanguages.Length > 0, Request.UserLanguages(0), Nothing)
        End If
        ' obtain it from HTTP header AcceptLanguages
        ' Validate culture name
        cultureName = CultureHelper.GetImplementedCulture(cultureName)
        ' This is safe

        If TryCast(RouteData.Values("culture"), String) <> cultureName Then

            ' Force a valid culture in the URL
            RouteData.Values("culture") = cultureName.ToLowerInvariant()
            ' lower case too
            ' Redirect user
            Response.RedirectToRoute(RouteData.Values)
        End If


        ' Modify current thread's cultures            
        Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo(cultureName)
        Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture

        Return MyBase.BeginExecuteCore(callback, state)
    End Function


    <AllowAnonymous>
    Function TimeoutRedirect() As ActionResult
        Return View()
    End Function


    '
    ' GET: /Account/Login
    <AllowAnonymous>
    Public Function Login(returnUrl As String) As ActionResult
        ViewBag.ReturnUrl = returnUrl
        Return View()
    End Function

    <LocalizedAuthorize(Roles:="ADMINISTRATEUR")>
    Function Index(sortOrder As String, currentFilter As String, searchString As String, page As Integer?) As ActionResult

        If Not String.IsNullOrEmpty(searchString) Then
            page = 1
        Else
            searchString = currentFilter
        End If

        ViewBag.CurrentFilter = searchString

        Dim entities = From e In db.Users().Where(Function(u) u.UserName.ToLower <> "sa" And User.Identity.Name.ToLower <> "sa" Or u.UserName.ToLower <> "root").ToList
        If Not String.IsNullOrEmpty(searchString) Then
            entities = entities.Where(Function(e) e.UserName.ToUpper.Contains(searchString.ToUpper))
        End If
        ViewBag.EnregCount = entities.Count



        Dim pageSize As Integer = ConfigurationManager.AppSettings("pageSize")
        Dim pageNumber As Integer = If(page, 1)

        Return View(entities.ToPagedList(pageNumber, pageSize))
    End Function

    '
    ' POST: /Account/Login
    <HttpPost>
    <AllowAnonymous>
    <ValidateAntiForgeryToken>
    Public Async Function Login(model As LoginViewModel, returnUrl As String) As Task(Of ActionResult)
        If ModelState.IsValid Then
            ' Valider le mot de passe
            Dim appUser = Await UserManager.FindAsync(model.UserName, model.Password)
            If appUser IsNot Nothing Then
                'Return RedirectToLocal(returnUrl)
                'on recupere les infos de sessions
                AppSession.UserId = appUser.Id
                AppSession.UserName = appUser.UserName
                AppSession.PersonneId = appUser.PersonneId
                AppSession.CodeSecret = appUser.CodeSecret
                AppSession.AgenceId = appUser.Personne.AgenceId
                AppSession.AgenceLibelle = appUser.Personne.Agence.Libelle.ToUpper
                AppSession.PasswordExpiredDate = appUser.PasswordExpiredDate
                If (String.IsNullOrEmpty(appUser.Personne.Prenom)) Then
                    AppSession.NomPrenomUser = appUser.Personne.Nom.ToUpper
                Else
                    AppSession.NomPrenomUser = appUser.Personne.Nom.ToUpper & " " & appUser.Personne.Prenom.ToUpper
                End If
                Await SignInAsync(appUser, model.RememberMe)

                If AppSession.PasswordExpiredDate < DateTime.UtcNow Then
                    Return RedirectToAction("Manage", "Account")
                End If


                'My.Computer.Audio.Play("K:\Alarm.wav", AudioPlayMode.BackgroundLoop)
                Return RedirectToAction("Index", "Home")
                Else
                    ModelState.AddModelError("", "Invalid username or password.")
            End If
        End If

        ' Si nous sommes arrivés là, un échec s’est produit. Réafficher le formulaire
        Return View(model)
    End Function

    <LocalizedAuthorize(Roles:="ADMINISTRATEUR")>
    Public Function Edit(id As String, Optional Message As System.Nullable(Of ManageMessageId) = Nothing) As ActionResult
        If IsNothing(id) Then
            Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
        End If
        Dim user = db.Users.Find(id)
        If IsNothing(user) Then
            Return HttpNotFound()
        End If
        Dim model = New EditUserViewModel(user)
        ViewBag.MessageId = Message

        Dim listPersonne = db.Personnes.OfType(Of Personne).Where(Function(p) p.Nom <> "sa").ToList
        Dim listPersonne2 As New List(Of SelectListItem)
        For Each item In listPersonne
            listPersonne2.Add(New SelectListItem With {.Value = item.Id, .Text = item.Nom & " " & item.Prenom})
        Next
        model.IDspersonne = listPersonne2
        Return View(model)
    End Function

    <HttpPost>
    <ValidateAntiForgeryToken>
    <LocalizedAuthorize(Roles:="ADMINISTRATEUR")>
    Public Async Function Edit(model As EditUserViewModel) As Task(Of ActionResult)
        If ModelState.IsValid Then
            Dim id = model.Id
            Dim user = db.Users.Find(id)
            If IsNothing(user) Then
                Return HttpNotFound()
            End If

            Dim entity = model.getEntity(user, db)
            db.Entry(entity).State = EntityState.Modified
            Dim idManager = New IdentityManager()
            idManager.ClearUserRoles(user.Id)
            Try
                Await db.SaveChangesAsync()

                ' Changer le mot de passe eventuellement
                If Not String.IsNullOrEmpty(model.Password) Then
                    UserManager.RemovePassword(model.Id)

                    Dim result = UserManager.AddPassword(model.Id, model.Password)
                    If result.Succeeded Then
                        Return RedirectToAction("UserRoles", "Account", New With {user.Id})
                    Else
                        AddErrors(result)
                    End If
                Else
                    Return RedirectToAction("UserRoles", "Account", New With {user.Id})
                End If

            Catch ex As Exception
                Util.GetError(ex, ModelState)
            End Try
        End If
        ' If we got this far, something failed, redisplay form
        Dim listPersonne = db.Personnes.OfType(Of Personne).Where(Function(p) p.Nom <> "sa").ToList
        Dim listPersonne2 As New List(Of SelectListItem)
        For Each item In listPersonne
            listPersonne2.Add(New SelectListItem With {.Value = item.Id, .Text = item.Nom & " " & item.Prenom})

        Next
        model.IDspersonne = listPersonne2
        Return View(model)
    End Function

    <LocalizedAuthorize(Roles:="ADMINISTRATEUR")>
    Public Function Delete(Optional id As String = Nothing) As ActionResult
        Dim Db = New ApplicationDbContext()
        Dim user = Db.Users.First(Function(u) u.Id = id)
        Dim model = New EditUserViewModel(user)
        If user Is Nothing Then
            Return HttpNotFound()
        End If
        Return View(model)
    End Function


    <HttpPost, ActionName("Delete")>
    <ValidateAntiForgeryToken>
    <LocalizedAuthorize(Roles:="ADMINISTRATEUR")>
    Public Function DeleteConfirmed(id As String) As ActionResult
        Dim Db = New ApplicationDbContext()
        Dim user = Db.Users.First(Function(u) u.Id = id)
        Db.Users.Remove(user)
        Db.SaveChanges()
        Return RedirectToAction("Index")
    End Function

    'Get
    <LocalizedAuthorize(Roles:="ADMINISTRATEUR")>
    Public Function UserRoles(id As String) As ActionResult
        If IsNothing(id) Then
            Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
        End If
        Dim user = db.Users.Find(id)
        If IsNothing(user) Then
            Return HttpNotFound()
        End If
        Dim model = New SelectUserRolesViewModel(user)
        Return View(model)
    End Function

    <HttpPost>
    <ValidateAntiForgeryToken>
    <LocalizedAuthorize(Roles:="ADMINISTRATEUR")>
    Public Function UserRoles(model As SelectUserRolesViewModel) As ActionResult
        If ModelState.IsValid Then
            Dim idManager = New IdentityManager()
            Dim user = db.Users.Find(model.Id)
            idManager.ClearUserRoles(user.Id)
            For Each role As SelectRoleEditorViewModel In model.Roles
                If role.Selected Then
                    idManager.AddUserToRole(user.Id, role.RoleName)
                End If
            Next
            Return RedirectToAction("index")
        End If
        Return View()
    End Function

    '
    ' GET: /Account/Register
    <LocalizedAuthorize(Roles:="ADMINISTRATEUR")>
    Public Function Register() As ActionResult
        Dim Db = New ApplicationDbContext()
        Dim model = New RegisterViewModel()
        ' Remplir les combos						
        Dim listPersonne = Db.Personnes.OfType(Of Personne).Where(Function(p) p.Nom <> "sa").ToList
        Dim listPersonne2 As New List(Of SelectListItem)
        For Each item In listPersonne
            listPersonne2.Add(New SelectListItem With {.Value = item.Id, .Text = item.Nom & " " & item.Prenom})
        Next
        model.IDspersonne = listPersonne2
        Return View(model)
    End Function

    '
    ' POST: /Account/Register
    <HttpPost>
    <AllowAnonymous>
    <ValidateAntiForgeryToken>
    <LocalizedAuthorize(Roles:="ADMINISTRATEUR")>
    Public Async Function Register(model As RegisterViewModel) As Task(Of ActionResult)
        model.PasswordExpiredDate = DateTime.UtcNow.AddHours(1).AddDays(Util.GetPasswordValidityDays)

        If ModelState.IsValid Then

            ' Créer un identifiant local avant de connecter l'utilisateur
            Dim user = model.GetUser ' New ApplicationUser() With {.UserName = model.UserName}
            Try
                Dim result = Await UserManager.CreateAsync(user, model.Password)
                If result.Succeeded Then
                    Return RedirectToAction("UserRoles", "Account", New With {user.Id})
                Else
                    AddErrors(result)
                End If
            Catch ex As Exception
                Util.GetError(ex, ModelState)
            End Try
        End If

        ' Si nous sommes arrivés là, un échec s’est produit. Réafficher le formulaire
        Dim listPersonne = db.Personnes.OfType(Of Personne).Where(Function(p) p.Nom <> "sa").ToList
        Dim listPersonne2 As New List(Of SelectListItem)
        For Each item In listPersonne
            listPersonne2.Add(New SelectListItem With {.Value = item.Id, .Text = item.Nom & " " & item.Prenom})

        Next
        model.IDspersonne = listPersonne2

        Return View(model)
    End Function

    '
    ' POST: /Account/Disassociate
    <HttpPost>
    <ValidateAntiForgeryToken>
    <LocalizedAuthorize(Roles:="ADMINISTRATEUR")>
    Public Async Function Disassociate(loginProvider As String, providerKey As String) As Task(Of ActionResult)
        Dim message As ManageMessageId? = Nothing
        Dim result As IdentityResult = Await UserManager.RemoveLoginAsync(User.Identity.GetUserId(), New UserLoginInfo(loginProvider, providerKey))
        If result.Succeeded Then
            message = ManageMessageId.RemoveLoginSuccess
        Else
            message = ManageMessageId.UnknownError
        End If

        Return RedirectToAction("Manage", New With {
            message
        })
    End Function

    '
    ' GET: /Account/Manage
    <LocalizedAuthorize(Roles:="SA,ADMINISTRATEUR,CHEFCOLLECTEUR,MANAGER")>
    Public Function Manage(ByVal message As ManageMessageId?) As ActionResult
        ViewData("StatusMessage") =
            If(message = ManageMessageId.ChangePasswordSuccess, Resource.GestUser_PwdModify,
            If(message = ManageMessageId.ChangePasswordImpossible, Resource.ChangePasswordImpossible,
                If(message = ManageMessageId.SetPasswordSuccess, Resource.GestUser_PwdDefine,
                    If(message = ManageMessageId.RemoveLoginSuccess, Resource.GestUser_DeleteConection,
                        If(message = ManageMessageId.UnknownError, Resource.GestUser_ErrorOccured,
                        If(message = ManageMessageId.NewPasswordContainOldPassword, Resource.GestUser_NewPasswordContainOldPassword,
                        ""))))))
        ViewBag.HasLocalPassword = HasPassword()
        ViewBag.ReturnUrl = Url.Action("Manage")
        Return View()
    End Function

    'Public Function Manage(ByVal message As ManageMessageId?) As ActionResult
    '    ViewData("StatusMessage") =
    '        If(message = ManageMessageId.ChangePasswordSuccess, "Votre mot de passe a été modifié.",
    '            If(message = ManageMessageId.SetPasswordSuccess, "Votre mot de passe a été défini.",
    '                If(message = ManageMessageId.RemoveLoginSuccess, "La connexion externe a été supprimée.",
    '                    If(message = ManageMessageId.UnknownError, "Une erreur s'est produite.",
    '                    ""))))

    '    ViewBag.HasLocalPassword = HasPassword()
    '    ViewBag.ReturnUrl = Url.Action("Manage")
    '    Return View()
    'End Function

    '
    ' POST: /Account/Manage
    <HttpPost>
    <ValidateAntiForgeryToken>
    <LocalizedAuthorize(Roles:="SA,ADMINISTRATEUR,CHEFCOLLECTEUR,MANAGER")>
    Public Async Function Manage(model As ManageUserViewModel) As Task(Of ActionResult)
        Dim hasLocalLogin As Boolean = HasPassword()
        ViewBag.HasLocalPassword = hasLocalLogin
        ViewBag.ReturnUrl = Url.Action("Manage")
        If hasLocalLogin Then
            If (model.NewPassword.ToLower.Contains(model.OldPassword.ToLower)) Then
                ModelState.AddModelError("NewPassword", "Le nouveau mot de passe ne peut être identique ou être constitué à partir de l'ancien mot de passe.")
                Return RedirectToAction("Manage", New With {
                    .Message = ManageMessageId.NewPasswordContainOldPassword
                })
            End If
            If ModelState.IsValid Then
                Dim result As IdentityResult = Await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword)
                If result.Succeeded Then
                    Dim appUser = db.Users.Find(User.Identity.GetUserId)
                    appUser.PasswordExpiredDate = DateTime.UtcNow.AddDays(Util.GetPasswordValidityDays)
                    db.Entry(appUser).State = EntityState.Modified
                    Try
                        Await db.SaveChangesAsync()
                        AppSession.PasswordExpiredDate = appUser.PasswordExpiredDate
                    Catch ex As Exception
                        Util.GetError(ex, ModelState)
                    End Try
                    Return RedirectToAction("Manage", New With {
                        .Message = ManageMessageId.ChangePasswordSuccess
                    })
                Else
                    'AddErrors(result)
                    Return RedirectToAction("Manage", New With {
                        .Message = ManageMessageId.ChangePasswordImpossible
                    })
                End If
            End If
        Else
            ' L’utilisateur ne possède pas de mot de passe local. Supprimez donc toutes les erreurs de validation causées par un champ OldPassword manquant
            Dim state As ModelState = ModelState("OldPassword")
            If state IsNot Nothing Then
                state.Errors.Clear()
            End If

            If ModelState.IsValid Then
                Dim result As IdentityResult = Await UserManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword)
                If result.Succeeded Then
                    Return RedirectToAction("Manage", New With {
                        .Message = ManageMessageId.SetPasswordSuccess
                    })
                Else
                    AddErrors(result)
                End If
            End If
        End If

        ' Si nous sommes arrivés là, un échec s’est produit. Réafficher le formulaire
        Return View(model)
    End Function
    'Public Async Function Manage(model As ManageUserViewModel) As Task(Of ActionResult)
    '    Dim hasLocalLogin As Boolean = HasPassword()
    '    ViewBag.HasLocalPassword = hasLocalLogin
    '    ViewBag.ReturnUrl = Url.Action("Manage")
    '    If hasLocalLogin Then
    '        If ModelState.IsValid Then
    '            Dim appUser = db.Users.Find(User.Identity.GetUserId)

    '            Dim result As IdentityResult = Await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword)
    '            If result.Succeeded Then
    '                appUser.PasswordExpiredDate = DateTime.UtcNow.AddDays(Util.GetPasswordValidityDays)
    '                db.Entry(appUser).State = EntityState.Modified
    '                Try
    '                    Await db.SaveChangesAsync()
    '                    AppSession.PasswordExpiredDate = appUser.PasswordExpiredDate
    '                Catch ex As Exception
    '                    Util.GetError(ex, ModelState)
    '                End Try
    '                Return RedirectToAction("Manage", New With {
    '                    .Message = ManageMessageId.ChangePasswordSuccess
    '                })
    '            Else
    '                AddErrors(result)
    '            End If
    '        End If
    '    Else
    '        ' L’utilisateur ne possède pas de mot de passe local. Supprimez donc toutes les erreurs de validation causées par un champ OldPassword manquant
    '        Dim state As ModelState = ModelState("OldPassword")
    '        If state IsNot Nothing Then
    '            state.Errors.Clear()
    '        End If

    '        If ModelState.IsValid Then
    '            Dim result As IdentityResult = Await UserManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword)
    '            If result.Succeeded Then
    '                Return RedirectToAction("Manage", New With {
    '                    .Message = ManageMessageId.SetPasswordSuccess
    '                })
    '            Else
    '                AddErrors(result)
    '            End If
    '        End If
    '    End If

    '    ' Si nous sommes arrivés là, un échec s’est produit. Réafficher le formulaire
    '    Return View(model)
    'End Function

    '
    ' POST: /Account/ExternalLogin
    <HttpPost>
    <AllowAnonymous>
    <ValidateAntiForgeryToken>
    <LocalizedAuthorize(Roles:="ADMINISTRATEUR")>
    Public Function ExternalLogin(provider As String, returnUrl As String) As ActionResult
        ' Demandez une redirection vers le fournisseur de connexions externe
        Return New ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", New With {returnUrl}))
    End Function

    '
    ' GET: /Account/ExternalLoginCallback
    <AllowAnonymous>
    <LocalizedAuthorize(Roles:="ADMINISTRATEUR")>
    Public Async Function ExternalLoginCallback(returnUrl As String) As Task(Of ActionResult)
        Dim loginInfo = Await AuthenticationManager.GetExternalLoginInfoAsync()
        If loginInfo Is Nothing Then
            Return RedirectToAction("Login")
        End If

        ' Sign in the user with this external login provider if the user already has a login
        Dim user = Await UserManager.FindAsync(loginInfo.Login)
        If user IsNot Nothing Then
            Await SignInAsync(user, isPersistent:=False)
            Return RedirectToLocal(returnUrl)
        Else
            ' If the user does not have an account, then prompt the user to create an account
            ViewBag.ReturnUrl = returnUrl
            ViewBag.LoginProvider = loginInfo.Login.LoginProvider
            Return View("ExternalLoginConfirmation", New ExternalLoginConfirmationViewModel() With {.UserName = loginInfo.DefaultUserName})
        End If
        Return View("ExternalLoginFailure")
    End Function

    '
    ' POST: /Account/LinkLogin
    <HttpPost>
    <ValidateAntiForgeryToken>
    <LocalizedAuthorize(Roles:="ADMINISTRATEUR")>
    Public Function LinkLogin(provider As String) As ActionResult
        ' Request a redirect to the external login provider to link a login for the current user
        Return New ChallengeResult(provider, Url.Action("LinkLoginCallback", "Account"), User.Identity.GetUserId())
    End Function

    '
    ' GET: /Account/LinkLoginCallback
    <LocalizedAuthorize(Roles:="ADMINISTRATEUR")>
    Public Async Function LinkLoginCallback() As Task(Of ActionResult)
        Dim loginInfo = Await AuthenticationManager.GetExternalLoginInfoAsync(XsrfKey, User.Identity.GetUserId())
        If loginInfo Is Nothing Then
            Return RedirectToAction("Manage", New With {
                .Message = ManageMessageId.UnknownError
            })
        End If
        Dim result = Await UserManager.AddLoginAsync(User.Identity.GetUserId(), loginInfo.Login)
        If result.Succeeded Then
            Return RedirectToAction("Manage")
        End If
        Return RedirectToAction("Manage", New With {
            .Message = ManageMessageId.UnknownError
        })
    End Function

    '
    ' POST: /Account/ExternalLoginConfirmation
    <HttpPost>
    <AllowAnonymous>
    <ValidateAntiForgeryToken>
    <LocalizedAuthorize(Roles:="ADMINISTRATEUR")>
    Public Async Function ExternalLoginConfirmation(model As ExternalLoginConfirmationViewModel, returnUrl As String) As Task(Of ActionResult)
        If User.Identity.IsAuthenticated Then
            Return RedirectToAction("Manage")
        End If

        If ModelState.IsValid Then
            ' Obtenez des informations sur l’utilisateur auprès du fournisseur de connexions externe
            Dim info = Await AuthenticationManager.GetExternalLoginInfoAsync()
            If info Is Nothing Then
                Return View("ExternalLoginFailure")
            End If
            Dim user = New ApplicationUser() With {.UserName = model.UserName}
            Dim result = Await UserManager.CreateAsync(user)
            If result.Succeeded Then
                result = Await UserManager.AddLoginAsync(user.Id, info.Login)
                If result.Succeeded Then
                    Await SignInAsync(user, isPersistent:=False)
                    Return RedirectToLocal(returnUrl)
                End If
            End If
            AddErrors(result)
        End If

        ViewBag.ReturnUrl = returnUrl
        Return View(model)
    End Function

    '
    ' POST: /Account/LogOff
    <HttpPost>
    <ValidateAntiForgeryToken>
    Public Function LogOff() As ActionResult
        Session.Abandon()
        Session.RemoveAll()
        AuthenticationManager.SignOut()
        Return RedirectToAction("Login", "Account")
    End Function

    '
    ' GET: /Account/ExternalLoginFailure
    <AllowAnonymous>
    Public Function ExternalLoginFailure() As ActionResult
        Return View()
    End Function

    <ChildActionOnly>
    Public Function RemoveAccountList() As ActionResult
        Dim linkedAccounts = UserManager.GetLogins(User.Identity.GetUserId())
        ViewBag.ShowRemoveButton = linkedAccounts.Count > 1 Or HasPassword()
        Return DirectCast(PartialView("_RemoveAccountPartial", linkedAccounts), ActionResult)
    End Function

    Protected Overrides Sub Dispose(disposing As Boolean)
        If disposing AndAlso UserManager IsNot Nothing Then
            UserManager.Dispose()
            UserManager = Nothing
        End If
        MyBase.Dispose(disposing)
    End Sub

#Region "Assistants"
    ' Used for XSRF protection when adding external logins
    Private Const XsrfKey As String = "XsrfId"

    Private Function AuthenticationManager() As IAuthenticationManager
        Return HttpContext.GetOwinContext().Authentication
    End Function

    Private Async Function SignInAsync(user As ApplicationUser, isPersistent As Boolean) As Task
        AuthenticationManager.SignOut(Microsoft.AspNet.Identity.DefaultAuthenticationTypes.ExternalCookie)
        Dim identity = Await UserManager.CreateIdentityAsync(user, Microsoft.AspNet.Identity.DefaultAuthenticationTypes.ApplicationCookie)
        AuthenticationManager.SignIn(New AuthenticationProperties() With {.IsPersistent = isPersistent}, identity)
    End Function

    Private Sub AddErrors(result As IdentityResult)
        For Each [error] As String In result.Errors
            ModelState.AddModelError("", [error])
        Next
    End Sub

    Private Function HasPassword() As Boolean
        Dim appUser = UserManager.FindById(User.Identity.GetUserId())
        If (appUser IsNot Nothing) Then
            Return appUser.PasswordHash IsNot Nothing
        End If
        Return False
    End Function

    Private Function RedirectToLocal(returnUrl As String) As ActionResult
        If Url.IsLocalUrl(returnUrl) Then
            Return Redirect(returnUrl)
        Else
            Return RedirectToAction("Index", "Home")
        End If
    End Function

    Public Enum ManageMessageId
        ChangePasswordImpossible
        ChangePasswordSuccess
        SetPasswordSuccess
        RemoveLoginSuccess
        UnknownError
        NewPasswordContainOldPassword
    End Enum

    Private Class ChallengeResult
        Inherits HttpUnauthorizedResult
        Public Sub New(provider As String, redirectUri As String)
            Me.New(provider, redirectUri, Nothing)
        End Sub
        Public Sub New(provider As String, redirectUri As String, userId As String)
            Me.LoginProvider = provider
            Me.RedirectUri = redirectUri
            Me.UserId = userId
        End Sub

        Public Property LoginProvider As String
        Public Property RedirectUri As String

        Public Property UserId As String

        Public Overrides Sub ExecuteResult(context As ControllerContext)
            Dim properties = New AuthenticationProperties() With {.RedirectUri = RedirectUri}
            If UserId IsNot Nothing Then
                properties.Dictionary(XsrfKey) = UserId
            End If
            context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider)
        End Sub
    End Class
#End Region

End Class



'Imports System.Security.Claims
'Imports System.Threading.Tasks
'Imports Microsoft.AspNet.Identity
'Imports Microsoft.AspNet.Identity.EntityFramework
'Imports Microsoft.AspNet.Identity.Owin
'Imports Microsoft.Owin.Security
'Imports System.Web.SessionState

'<Authorize>
'Public Class AccountController
'    Inherits Controller

'    Private db As New ICDPDEntities

'    Public Sub New()
'        Me.New(New UserManager(Of ApplicationUser)(New UserStore(Of ApplicationUser)(New ApplicationDbContext())))
'    End Sub

'    Public Sub New(manager As UserManager(Of ApplicationUser))
'        UserManager = manager
'    End Sub

'    Public Property UserManager As UserManager(Of ApplicationUser)

'    '
'    ' GET: /Account/Login
'    <AllowAnonymous>
'    Public Function Login(returnUrl As String) As ActionResult
'        ViewBag.ID_ANNEE = New SelectList(db.ANNEE_BUDGETAIRE, "ID_ANNEE", "LIBELLE")
'        ViewBag.ReturnUrl = returnUrl
'        Return View()
'    End Function

'    '
'    ' POST: /Account/Login
'    <HttpPost>
'    <AllowAnonymous>
'    <ValidateAntiForgeryToken>
'    Public Async Function Login(model As LoginViewModel, returnUrl As String) As Task(Of ActionResult)
'        If ModelState.IsValid Then
'            ' Valider le mot de passe
'            Dim appUser = Await UserManager.FindAsync(model.UserName, model.Password)
'            If appUser IsNot Nothing Then
'                Session("ANNEE") = model.ID_ANNEE
'                Await SignInAsync(appUser, model.RememberMe)
'                Return RedirectToLocal(returnUrl)
'            Else
'                ModelState.AddModelError("", "Invalid username or password.")
'            End If
'        End If

'        ' Si nous sommes arrivés là, un échec s’est produit. Réafficher le formulaire
'        ViewBag.ID_ANNEE = New SelectList(db.ANNEE_BUDGETAIRE, "ID_ANNEE", "LIBELLE")
'        Return View(model)
'    End Function

'    '
'    ' GET: /Account/Register
'    <AllowAnonymous>
'    Public Function Register() As ActionResult
'        Return View()
'    End Function

'    '
'    ' POST: /Account/Register
'    <HttpPost>
'    <AllowAnonymous>
'    <ValidateAntiForgeryToken>
'    Public Async Function Register(model As RegisterViewModel) As Task(Of ActionResult)
'        If ModelState.IsValid Then
'            ' Créer un identifiant local avant de connecter l'utilisateur
'            Dim user = New ApplicationUser() With {.UserName = model.UserName}
'            Dim result = Await UserManager.CreateAsync(User, model.Password)
'            If result.Succeeded Then
'                Await SignInAsync(User, isPersistent:=False)
'                Return RedirectToAction("Index", "Home")
'            Else
'                AddErrors(result)
'            End If
'        End If

'        ' Si nous sommes arrivés là, un échec s’est produit. Réafficher le formulaire
'        Return View(model)
'    End Function

'    '
'    ' POST: /Account/Disassociate
'    <HttpPost>
'    <ValidateAntiForgeryToken>
'    Public Async Function Disassociate(loginProvider As String, providerKey As String) As Task(Of ActionResult)
'        Dim message As ManageMessageId? = Nothing
'        Dim result As IdentityResult = Await UserManager.RemoveLoginAsync(User.Identity.GetUserId(), New UserLoginInfo(loginProvider, providerKey))
'        If result.Succeeded Then
'            message = ManageMessageId.RemoveLoginSuccess
'        Else
'            message = ManageMessageId.UnknownError
'        End If

'        Return RedirectToAction("Manage", New With {
'            .Message = message
'        })
'    End Function

'    '
'    ' GET: /Account/Manage
'    Public Function Manage(ByVal message As ManageMessageId?) As ActionResult
'        ViewData("StatusMessage") =
'            If(message = ManageMessageId.ChangePasswordSuccess, "Votre mot de passe a été modifié.", _
'                If(message = ManageMessageId.SetPasswordSuccess, "Votre mot de passe a été défini.", _
'                    If(message = ManageMessageId.RemoveLoginSuccess, "La connexion externe a été supprimée.", _
'                        If(message = ManageMessageId.UnknownError, "Une erreur s'est produite.", _
'                        ""))))
'        ViewBag.HasLocalPassword = HasPassword()
'        ViewBag.ReturnUrl = Url.Action("Manage")
'        Return View()
'    End Function

'    '
'    ' POST: /Account/Manage
'    <HttpPost>
'    <ValidateAntiForgeryToken>
'    Public Async Function Manage(model As ManageUserViewModel) As Task(Of ActionResult)
'        Dim hasLocalLogin As Boolean = HasPassword()
'        ViewBag.HasLocalPassword = hasLocalLogin
'        ViewBag.ReturnUrl = Url.Action("Manage")
'        If hasLocalLogin Then
'            If ModelState.IsValid Then
'                Dim result As IdentityResult = Await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword)
'                If result.Succeeded Then
'                    Return RedirectToAction("Manage", New With {
'                        .Message = ManageMessageId.ChangePasswordSuccess
'                    })
'                Else
'                    AddErrors(result)
'                End If
'            End If
'        Else
'            ' L’utilisateur ne possède pas de mot de passe local. Supprimez donc toutes les erreurs de validation causées par un champ OldPassword manquant
'            Dim state As ModelState = ModelState("OldPassword")
'            If state IsNot Nothing Then
'                state.Errors.Clear()
'            End If

'            If ModelState.IsValid Then
'                Dim result As IdentityResult = Await UserManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword)
'                If result.Succeeded Then
'                    Return RedirectToAction("Manage", New With {
'                        .Message = ManageMessageId.SetPasswordSuccess
'                    })
'                Else
'                    AddErrors(result)
'                End If
'            End If
'        End If

'        ' Si nous sommes arrivés là, un échec s’est produit. Réafficher le formulaire
'        Return View(model)
'    End Function

'    '
'    ' POST: /Account/ExternalLogin
'    <HttpPost>
'    <AllowAnonymous>
'    <ValidateAntiForgeryToken>
'    Public Function ExternalLogin(provider As String, returnUrl As String) As ActionResult
'        ' Demandez une redirection vers le fournisseur de connexions externe
'        Return New ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", New With {.ReturnUrl = returnUrl}))
'    End Function

'    '
'    ' GET: /Account/ExternalLoginCallback
'    <AllowAnonymous>
'    Public Async Function ExternalLoginCallback(returnUrl As String) As Task(Of ActionResult)
'        Dim loginInfo = Await AuthenticationManager.GetExternalLoginInfoAsync()
'        If loginInfo Is Nothing Then
'            Return RedirectToAction("Login")
'        End If

'        ' Sign in the user with this external login provider if the user already has a login
'        Dim user = Await UserManager.FindAsync(loginInfo.Login)
'        If user IsNot Nothing Then
'            Await SignInAsync(user, isPersistent:=False)
'            Return RedirectToLocal(returnUrl)
'        Else
'            ' If the user does not have an account, then prompt the user to create an account
'            ViewBag.ReturnUrl = returnUrl
'            ViewBag.LoginProvider = loginInfo.Login.LoginProvider
'            Return View("ExternalLoginConfirmation", New ExternalLoginConfirmationViewModel() With {.UserName = loginInfo.DefaultUserName})
'        End If
'        Return View("ExternalLoginFailure")
'    End Function

'    '
'    ' POST: /Account/LinkLogin
'    <HttpPost>
'    <ValidateAntiForgeryToken>
'    Public Function LinkLogin(provider As String) As ActionResult
'        ' Request a redirect to the external login provider to link a login for the current user
'        Return New ChallengeResult(provider, Url.Action("LinkLoginCallback", "Account"), User.Identity.GetUserId())
'    End Function

'    '
'    ' GET: /Account/LinkLoginCallback
'    Public Async Function LinkLoginCallback() As Task(Of ActionResult)
'        Dim loginInfo = Await AuthenticationManager.GetExternalLoginInfoAsync(XsrfKey, User.Identity.GetUserId())
'        If loginInfo Is Nothing Then
'            Return RedirectToAction("Manage", New With {
'                .Message = ManageMessageId.UnknownError
'            })
'        End If
'        Dim result = Await UserManager.AddLoginAsync(User.Identity.GetUserId(), loginInfo.Login)
'        If result.Succeeded Then
'            Return RedirectToAction("Manage")
'        End If
'        Return RedirectToAction("Manage", New With {
'            .Message = ManageMessageId.UnknownError
'        })
'    End Function

'    '
'    ' POST: /Account/ExternalLoginConfirmation
'    <HttpPost>
'    <AllowAnonymous>
'    <ValidateAntiForgeryToken>
'    Public Async Function ExternalLoginConfirmation(model As ExternalLoginConfirmationViewModel, returnUrl As String) As Task(Of ActionResult)
'        If User.Identity.IsAuthenticated Then
'            Return RedirectToAction("Manage")
'        End If

'        If ModelState.IsValid Then
'            ' Obtenez des informations sur l’utilisateur auprès du fournisseur de connexions externe
'            Dim info = Await AuthenticationManager.GetExternalLoginInfoAsync()
'            If info Is Nothing Then
'                Return View("ExternalLoginFailure")
'            End If
'            Dim user = New ApplicationUser() With {.UserName = model.UserName}
'            Dim result = Await UserManager.CreateAsync(user)
'            If result.Succeeded Then
'                result = Await UserManager.AddLoginAsync(user.Id, info.Login)
'                If result.Succeeded Then
'                    Await SignInAsync(user, isPersistent:=False)
'                    Return RedirectToLocal(returnUrl)
'                End If
'            End If
'            AddErrors(result)
'        End If

'        ViewBag.ReturnUrl = returnUrl
'        Return View(model)
'    End Function

'    '
'    ' POST: /Account/LogOff
'    <HttpPost>
'    <ValidateAntiForgeryToken>
'    Public Function LogOff() As ActionResult
'        AuthenticationManager.SignOut()
'        Return RedirectToAction("Index", "Home")
'    End Function

'    '
'    ' GET: /Account/ExternalLoginFailure
'    <AllowAnonymous>
'    Public Function ExternalLoginFailure() As ActionResult
'        Return View()
'    End Function

'    <ChildActionOnly>
'    Public Function RemoveAccountList() As ActionResult
'        Dim linkedAccounts = UserManager.GetLogins(User.Identity.GetUserId())
'        ViewBag.ShowRemoveButton = linkedAccounts.Count > 1 Or HasPassword()
'        Return DirectCast(PartialView("_RemoveAccountPartial", linkedAccounts), ActionResult)
'    End Function

'    Protected Overrides Sub Dispose(disposing As Boolean)
'        If disposing AndAlso UserManager IsNot Nothing Then
'            UserManager.Dispose()
'            UserManager = Nothing
'        End If
'        MyBase.Dispose(disposing)
'    End Sub

'#Region "Assistants"
'    ' Used for XSRF protection when adding external logins
'    Private Const XsrfKey As String = "XsrfId"

'    Private Function AuthenticationManager() As IAuthenticationManager
'        Return HttpContext.GetOwinContext().Authentication
'    End Function

'    Private Async Function SignInAsync(user As ApplicationUser, isPersistent As Boolean) As Task
'        AuthenticationManager.SignOut(Microsoft.AspNet.Identity.DefaultAuthenticationTypes.ExternalCookie)
'        Dim identity = Await UserManager.CreateIdentityAsync(user, Microsoft.AspNet.Identity.DefaultAuthenticationTypes.ApplicationCookie)
'        AuthenticationManager.SignIn(New AuthenticationProperties() With {.IsPersistent = isPersistent}, identity)
'    End Function

'    Private Sub AddErrors(result As IdentityResult)
'        For Each [error] As String In result.Errors
'            ModelState.AddModelError("", [error])
'        Next
'    End Sub

'    Private Function HasPassword() As Boolean
'        Dim appUser = UserManager.FindById(User.Identity.GetUserId())
'        If (appUser IsNot Nothing) Then
'            Return appUser.PasswordHash IsNot Nothing
'        End If
'        Return False
'    End Function

'    Private Function RedirectToLocal(returnUrl As String) As ActionResult
'        If Url.IsLocalUrl(returnUrl) Then
'            Return Redirect(returnUrl)
'        Else
'            Return RedirectToAction("Index", "Home")
'        End If
'    End Function

'    Public Enum ManageMessageId
'        ChangePasswordSuccess
'        SetPasswordSuccess
'        RemoveLoginSuccess
'        UnknownError
'    End Enum

'    Private Class ChallengeResult
'        Inherits HttpUnauthorizedResult
'        Public Sub New(provider As String, redirectUri As String)
'            Me.New(provider, redirectUri, Nothing)
'        End Sub
'        Public Sub New(provider As String, redirectUri As String, userId As String)
'            Me.LoginProvider = provider
'            Me.RedirectUri = redirectUri
'            Me.UserId = userId
'        End Sub

'        Public Property LoginProvider As String
'        Public Property RedirectUri As String

'        Public Property UserId As String

'        Public Overrides Sub ExecuteResult(context As ControllerContext)
'            Dim properties = New AuthenticationProperties() With {.RedirectUri = RedirectUri}
'            If UserId IsNot Nothing Then
'                properties.Dictionary(XsrfKey) = UserId
'            End If
'            context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider)
'        End Sub
'    End Class
'#End Region

'End Class
