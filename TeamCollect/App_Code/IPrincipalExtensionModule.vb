Imports System.Security.Principal
Imports System.Runtime.CompilerServices

Public Module IPrincipalExtensionModule
    <Extension()> _
    Public Function IsInAnyRole(principal As IPrincipal, ParamArray roles As String()) As Boolean
        For Each role In roles
            If principal.IsInRole(role) Then
                Return True
            End If
        Next

        Return False
    End Function

    <Extension()> _
    Public Function IsInAnyRole(principal As IPrincipal, str_roles As String) As Boolean
        Dim roles = str_roles.split(",")
        For Each role In roles
            If principal.IsInRole(role.Trim) Then
                Return True
            End If
        Next

        Return False
    End Function
End Module
