Imports System.Threading

<SessionExpireFilterAttribute>
<LocalizedAuthorize>
Public Class BaseController
    Inherits Controller

    'Protected Overrides Function BeginExecuteCore(callback As AsyncCallback, state As Object) As IAsyncResult
    '    Dim cultureName As String = Nothing

    '    ' Attempt to read the culture cookie from Request
    '    Dim cultureCookie As HttpCookie = Request.Cookies("_culture")
    '    If cultureCookie IsNot Nothing Then
    '        cultureName = cultureCookie.Value
    '    Else
    '        ' obtain it from HTTP header AcceptLanguages
    '        cultureName = If(Request.UserLanguages IsNot Nothing AndAlso Request.UserLanguages.Length > 0, Request.UserLanguages(0), Nothing)
    '    End If
    '    ' Validate culture name
    '    cultureName = CultureHelper.GetImplementedCulture(cultureName)
    '    ' This is safe
    '    ' Modify current thread's cultures            
    '    Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo(cultureName)
    '    Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture

    '    Return MyBase.BeginExecuteCore(callback, state)
    'End Function

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
End Class