
<AttributeUsage(AttributeTargets.[Class] Or AttributeTargets.Method, Inherited:=True, AllowMultiple:=True)> _
Public Class SessionExpireFilterAttribute
    Inherits ActionFilterAttribute
    Public Overrides Sub OnActionExecuting(filterContext As ActionExecutingContext)
        Dim ctx As HttpContext = HttpContext.Current

        ' If the browser session or authentication session has expired...
        If ctx.Session("UserName") Is Nothing OrElse Not filterContext.HttpContext.Request.IsAuthenticated Then
            If filterContext.HttpContext.Request.IsAjaxRequest() Then
                ' For AJAX requests, we're overriding the returned JSON result with a simple string,
                ' indicating to the calling JavaScript code that a redirect should be performed.
                filterContext.Result = New JsonResult() With {
                     .Data = "_Logon_"
                }
            Else
                ' For round-trip posts, we're forcing a redirect to Home/TimeoutRedirect/, which
                ' simply displays a temporary 5 second notification that they have timed out, and
                ' will, in turn, redirect to the logon page.
                filterContext.Result = New RedirectToRouteResult(New RouteValueDictionary() From {
                    {"Controller", "Account"},
                    {"Action", "TimeoutRedirect"}
                })
            End If
        End If

        MyBase.OnActionExecuting(filterContext)
    End Sub
End Class
