@Imports Microsoft.AspNet.Identity

@If Request.IsAuthenticated
    @Using Html.BeginForm("LogOff", "Account", FormMethod.Post, New With { .id = "logoutForm", .class = "navbar-right" })
        @Html.AntiForgeryToken()
        @<ul class="nav navbar-nav navbar-right">
            <li><a href="javascript:document.getElementById('logoutForm').submit()">Se déconnecter</a></li>
        </ul>
        
    End Using
   
Else
    @<ul class="nav navbar-nav navbar-right">
        <li>@Html.ActionLink("S’inscrire", "Register", "Account", routeValues := Nothing, htmlAttributes := New With { .id = "registerLink" })</li>
        <li>@Html.ActionLink("Se connecter", "Login", "Account", routeValues := Nothing, htmlAttributes := New With { .id = "loginLink" })</li>
    </ul>
End If

