Imports System.ComponentModel.DataAnnotations
Imports Microsoft.AspNet.Identity.EntityFramework
Imports TeamCollect.My.Resources

Public Class ExternalLoginConfirmationViewModel
    <Required(ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="champ_Manquant")> _
    <Display(Name:="Nom utilisateur")> _
    Public Property UserName As String
End Class

Public Class ManageUserViewModel
    <Required(ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="champ_Manquant")> _
    <DataType(DataType.Password)>
    <Display(Name:="Ancien Mot de passe ")>
    Public Property OldPassword As String

    <Required(ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="champ_Manquant")>
    <RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[#$^+=!*()@%&]).{8,}$", ErrorMessageResourceName:="PasswordStrength", ErrorMessageResourceType:=GetType(Resource))>
    <StringLength(100, ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="GestUserPwdLength", MinimumLength:=8)>
    <DataType(DataType.Password)>
   <Display(Name:="Nouveau Mot de passe")>
    Public Property NewPassword As String

    <DataType(DataType.Password)>
   <Display(Name:="Confirmez le mot de passe")>
    <Compare("NewPassword", ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="GestUserPwdConfNewMatch")>
    Public Property ConfirmPassword As String
End Class

Public Class LoginViewModel
    <Required(ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="champ_Manquant")> _
    <Display(Name:="Nom utilisateur")> _
    Public Property UserName As String

    <Required(ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="champ_Manquant")> _
    <DataType(DataType.Password)>
    <Display(Name:="Mot de passe")> _
    Public Property Password As String

    <Display(Name:="Se souvenir")> _
    Public Property RememberMe As Boolean
End Class

Public Class RegisterViewModel

    <Required(ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="champ_Manquant")> _
    <Display(Name:="Nom utilisateur")> _
    Public Property UserName As String

    <Required(ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="champ_Manquant")>
    <RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[#$^+=!*()@%&]).{8,}$", ErrorMessageResourceName:="PasswordStrength", ErrorMessageResourceType:=GetType(Resource))>
    <StringLength(100, ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="GestUserPwdLength", MinimumLength:=8)>
    <DataType(DataType.Password)>
   <Display(Name:="Mot de passe")> _
    Public Property Password As String

    <DataType(DataType.Password)>
    <Display(Name:="Confirmez le mot de passe")> _
    <Compare("Password", ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="GestUserPwdConfMatch")>
    Public Property ConfirmPassword As String

    <Required(ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="champ_Manquant")> _
    <Display(Name:="Code Secret")> _
    Public Property CodeSecret As String

    <Display(Name:="Le personnel")>
    Public Property PersonneId As Long
    Public Overridable Property Personne As Personne
    Public Overridable Property IDspersonne As ICollection(Of SelectListItem)

    <Display(Name:="Date d'expiration du mot de passe")>
    <Required(ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="champ_Manquant")>
    Public Property PasswordExpiredDate As DateTime = DateTime.UtcNow.AddHours(1).AddDays(45)

    'niveau d'accès attribut système
    'Public Property niveau As String


    Public Function GetUser() As ApplicationUser
        Dim user As New ApplicationUser With {
        .UserName = UserName,
        .CodeSecret = CodeSecret,
        .PersonneId = PersonneId,
        .PasswordExpiredDate = PasswordExpiredDate
            }
        Return user
    End Function
End Class

Public Class EditUserViewModel
    Public Sub New()

    End Sub

    Public Sub New(user As ApplicationUser)
        With user
            Id = .Id
            UserName = .UserName
            CodeSecret = .CodeSecret
            PersonneId = .PersonneId
            PasswordExpiredDate = .PasswordExpiredDate
        End With
    End Sub

    Public Function GetEntity(user As ApplicationUser, db As ApplicationDbContext) As ApplicationUser

        With user
            Id = .Id
            .UserName = Me.UserName
            .CodeSecret = Me.CodeSecret
            .PersonneId = Me.PersonneId
            .PasswordExpiredDate = Me.PasswordExpiredDate
        End With

        Return user
    End Function

    Public Property Id As String

    <StringLength(100, ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="GestUserPwdLength", MinimumLength:=6)>
    <DataType(DataType.Password)>
   <Display(Name:="GestUserPwd")> _
    Public Property Password As String

    <DataType(DataType.Password)>
    <Display(Name:="GestUserPwdConf", ResourceType:=GetType(Resource))> _
    <Compare("Password", ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="GestUserPwdConfMatch")>
    Public Property ConfirmPassword As String

    <Required(ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="champ_Manquant")> _
   <Display(Name:="Nom Utlisateur")> _
    Public Property UserName As String

    <Required(ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="champ_Manquant")> _
   <Display(Name:="Code Secret")> _
    Public Property CodeSecret As String

    <Display(Name:="Le personnel")>
    Public Property PersonneId As Long
    Public Overridable Property Personne As Personne
    Public Overridable Property IDspersonne As ICollection(Of SelectListItem)


    <Display(Name:="Date d'expiration du mot de passe")>
    <Required(ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="champ_Manquant")>
    Public Property PasswordExpiredDate As DateTime = DateTime.UtcNow.AddHours(1).AddDays(45)


End Class

Public Class SelectUserRolesViewModel
    Public Sub New()
        Me.Roles = New List(Of SelectRoleEditorViewModel)()
    End Sub


    ' Enable initialization with an instance of ApplicationUser:
    Public Sub New(user As ApplicationUser)
        Me.New()
        Me.Id = user.Id
        Me.UserName = user.UserName
        Me.CodeSecret = user.CodeSecret
        Me.PersonneId = user.PersonneId
        Me.PasswordExpiredDate = user.PasswordExpiredDate
        Dim Db = New ApplicationDbContext()

        ' Add all available roles to the list of EditorViewModels:
        'If (Not IsNothing(user.SpecialiteMedecinId)) Then

        '    Dim allRoles = (From e In Db.Roles Where e.Name = "Médecin" Select e).ToList
        '    For Each role As IdentityRole In allRoles
        '        ' An EditorViewModel will be used by Editor Template:
        '        Dim rvm = New SelectRoleEditorViewModel(role)
        '        Me.Roles.Add(rvm)
        '    Next

        'ElseIf (user.Etat = -1) Then 'Etat = -1 ==> L'utilisateur est un infirmier

        '    Dim allRoles = (From e In Db.Roles Where e.Name = "Infirmier" Select e).ToList
        '    For Each role As IdentityRole In allRoles
        '        ' An EditorViewModel will be used by Editor Template:
        '        Dim rvm = New SelectRoleEditorViewModel(role)
        '        Me.Roles.Add(rvm)
        '    Next

        'ElseIf (user.Etat = -2) Then 'Etat = -2 ==> L'utilisateur est un caissier

        '    Dim allRoles = (From e In Db.Roles Where e.Name = "Caissier" Select e).ToList
        '    For Each role As IdentityRole In allRoles
        '        ' An EditorViewModel will be used by Editor Template:
        '        Dim rvm = New SelectRoleEditorViewModel(role)
        '        Me.Roles.Add(rvm)
        '    Next

        'End If

        Dim allRoles = (From e In Db.Roles Select e).OrderBy(Function(r) r.Name).ToList

        'If (user.Personne.AgenceId.HasValue) Then
        '    allRoles = allRoles.Where(Function(u) u.Name.Contains("CHEFCOLLECTEUR") Or u.Name.Contains("COLLECTEUR")).ToList
        'Else
        '    allRoles = allRoles.Where(Function(u) u.Name.Contains("ADMINISTRATEUR") Or u.Name.Contains("MANAGER")).ToList
        'End If

        For Each role As IdentityRole In allRoles
            ' An EditorViewModel will be used by Editor Template:
            Dim rvm = New SelectRoleEditorViewModel(role)
            Me.Roles.Add(rvm)
        Next


        ' Set the Selected property to true for those roles for 
        ' which the current user is a member:
        For Each userRole As IdentityUserRole In user.Roles
            Dim LeRoleCourant = (From rol In Db.Roles Where rol.Id = userRole.RoleId Select rol).FirstOrDefault()
            Dim checkUserRole = Me.Roles.Find(Function(r) r.RoleName = LeRoleCourant.Name)
            checkUserRole.Selected = True
        Next
        'For Each userRole As IdentityUserRole In user.Roles
        '    Dim checkUserRole = Me.Roles.Find(Function(r) r.RoleName = userRole.Role.Name)
        '    checkUserRole.Selected = True
        'Next


    End Sub

    Public Property Id As String
    Public Property Password As String
    Public Property ConfirmPassword As String
    Public Property PersonneId As Long
    Property UserName As String
    Property CodeSecret As String
    Property PasswordExpiredDate As DateTime

    Public Property Roles() As List(Of SelectRoleEditorViewModel)
        Get
            Return m_Roles
        End Get
        Set(value As List(Of SelectRoleEditorViewModel))
            m_Roles = value
        End Set
    End Property
    Private m_Roles As List(Of SelectRoleEditorViewModel)


End Class


Public Class SelectRoleEditorViewModel
    Public Sub New()
    End Sub
    Public Sub New(role As IdentityRole)
        Me.RoleName = role.Name
    End Sub

    Public Property Selected() As Boolean
        Get
            Return m_Selected
        End Get
        Set(value As Boolean)
            m_Selected = value
        End Set
    End Property
    Private m_Selected As Boolean

    <Required(ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="champ_Manquant")> _
    Public Property RoleName() As String
        Get
            Return m_RoleName
        End Get
        Set(value As String)
            m_RoleName = value
        End Set
    End Property
    Private m_RoleName As String
End Class
