<AttributeUsage(AttributeTargets.Class Or AttributeTargets.Method, Inherited:=True, AllowMultiple:=True)>
Class LocalizedAuthorizeAttribute

    Inherits AuthorizeAttribute

    Protected Overrides Sub HandleUnauthorizedRequest(filterContext As AuthorizationContext)
        Dim strLanguage As String = filterContext.RouteData.Values("culture")
        If Not strLanguage Is Nothing Then
            filterContext.Result = New RedirectResult(String.Format("~/Account/Login"))
        Else
            MyBase.HandleUnauthorizedRequest(filterContext)
        End If
    End Sub
End Class
