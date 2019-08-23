Public Class AntiForgeryHandleErrorAttribute
    Inherits HandleErrorAttribute
    Public Overrides Sub OnException(context As ExceptionContext)
        If TypeOf context.Exception Is HttpAntiForgeryException Then
            Dim url = String.Empty
            If Not context.HttpContext.User.Identity.IsAuthenticated Then
                Dim requestContext = New RequestContext(context.HttpContext, context.RouteData)
                url = RouteTable.Routes.GetVirtualPath(requestContext, New RouteValueDictionary(New With { _
                     .Controller = "Account", _
                     .action = "Login" _
                })).VirtualPath
            Else
                context.HttpContext.Response.StatusCode = 200
                context.ExceptionHandled = True
                url = GetRedirectUrl(context)
            End If
            context.HttpContext.Response.Redirect(url, True)
        Else
            MyBase.OnException(context)
        End If
    End Sub

    Private Function GetRedirectUrl(context As ExceptionContext) As String
        Try
            Dim requestContext = New RequestContext(context.HttpContext, context.RouteData)
            Dim url = RouteTable.Routes.GetVirtualPath(requestContext, New RouteValueDictionary(New With { _
                 .Controller = "Account", _
                 .action = "AlreadySignIn" _
            })).VirtualPath

            Return url
        Catch generatedExceptionName As Exception
            Throw New NullReferenceException()
        End Try
    End Function
End Class