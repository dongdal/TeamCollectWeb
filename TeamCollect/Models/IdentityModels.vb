Imports Microsoft.AspNet.Identity.EntityFramework
Imports Microsoft.AspNet.Identity
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema

' You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
Public Class ApplicationUser
    Inherits IdentityUser

    Public Property CodeSecret As String


    Public Property PersonneId As Long
    Public Overridable Property Personne As Personne

End Class

Public Class IdentityManager
    Public Function RoleExists(name As String) As Boolean
        Dim rm = New RoleManager(Of IdentityRole)(New RoleStore(Of IdentityRole)(New ApplicationDbContext()))
        Return rm.RoleExists(name)
    End Function


    Public Function CreateRole(name As String) As Boolean
        Dim rm = New RoleManager(Of IdentityRole)(New RoleStore(Of IdentityRole)(New ApplicationDbContext()))
        Dim idResult = rm.Create(New IdentityRole(name))
        Return idResult.Succeeded
    End Function


    Public Function CreateUser(user As ApplicationUser, password As String) As Boolean
        Dim um = New UserManager(Of ApplicationUser)(New UserStore(Of ApplicationUser)(New ApplicationDbContext()))
        Dim idResult = um.Create(user, password)
        Return idResult.Succeeded
    End Function


    Public Function AddUserToRole(userId As String, roleName As String) As Boolean
        Dim um = New UserManager(Of ApplicationUser)(New UserStore(Of ApplicationUser)(New ApplicationDbContext()))
        Dim idResult = um.AddToRole(userId, roleName)
        Return idResult.Succeeded
    End Function


    Public Sub ClearUserRoles(userId As String)
        Dim um = New UserManager(Of ApplicationUser)(New UserStore(Of ApplicationUser)(New ApplicationDbContext()))
        Dim user = um.FindById(userId)
        Dim currentRoles = New List(Of IdentityUserRole)()
        currentRoles.AddRange(user.Roles)
        For Each role As IdentityUserRole In currentRoles
            um.RemoveFromRole(userId, role.Role.Name)
        Next
    End Sub
End Class