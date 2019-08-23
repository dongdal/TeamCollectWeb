@Imports TeamCollect.My.Resources
@Code
    Layout = Nothing
End Code

<!DOCTYPE html>

<html>
<head>
    <meta name="viewport" content="width=device-width" />
    <meta http-equiv="refresh" content="5;url=Login/" />
    <title>TimeoutRedirect</title>

    @Styles.Render("~/Content/cssTemplate")
    @Styles.Render("~/Content/css")
    @Scripts.Render("~/bundles/jquery")
    @Scripts.Render("~/bundles/modernizr")
    <link href="@Url.Content("~/Content/font-awesome-4.2.0/css/font-awesome.min.css")" rel="stylesheet" />
</head>
<body>
    <div class="navbar navbar-default navbar-fixed-top">
        <div class="container-fluid">
            <div class="navbar-header">
                <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".navbar-collapse">
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                </button>
                <span class="navbar-brand">ICDPD</span>
            </div>
            <div class="navbar-collapse collapse">
                @*<ul class="nav navbar-nav">
                        <li>@Html.ActionLink(@Resource.About, "About", "Home")</li>
                        <li>@Html.ActionLink(@Resource.Contact, "Contact", "Home")</li>
                    </ul>
                @Html.Partial("_LoginPartial")*@
            </div>
        </div>
    </div>
    <div class="row">
        <div id="wrapper">
            <div id="login" class="animate form">
                <section class="login_content">
                    
                            <h1>Expiration Session</h1>
                            <div style="margin-top:-20px"><i class="fa fa-eye"></i>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<i class="fa fa-eye"></i></div>
                            
                            <div>
                                <div class="alert-warning">
                                    Désolé, votre session a expiré. Vous allez être redirigé vers la page de connexion dans 5 seconds...
                                    <br />
                                    Sorry, but your session has timed out. You'll be redirected to the Log On page in 5 seconds...
                                </div>
                            </div>
                    <div class="clearfix"></div>
                    <div class="separator">
                        <div class="clearfix"></div>
                        <br />
                        <div>
                            <label style="color:green"><i class="fa fa-lock" style="font-size: 70px;"></i> </label>
                        </div>
                    </div>
                </section>

            </div>
        </div>
    </div>

    <nav class="navbar navbar-inverse navbar-fixed-bottom" role="navigation">
        <div class="container-fluid col-md-12">
            <p class="navbar-text col-md-4">&copy; @DateTime.Now.Year - CND</p>
            <p class="navbar-text col-md-7 text-right">@Resource.Powered</p>
        </div>
    </nav>
</body>
</html>
