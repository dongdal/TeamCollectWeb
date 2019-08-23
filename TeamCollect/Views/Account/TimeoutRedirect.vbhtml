
@Code
    @<meta http-equiv="refresh" content="5;url=Login/" />
    ViewBag.Title = "Session expirée"
    Layout = "~/Views/Shared/_LayoutLogin.vbhtml"
End Code

<div class="row">
    <div id="wrapper">
        <div id="login" class="animate form">
            <section class="login_content" style="text-align:center; color: white">

                <div id="boxTimeOut" style="width:100%; text-align:center; color: white" class="animated bounceIn">
                    <div id="top_header">
                        <h3 style="text-align:center">Session Expirée / Expired Session</h3>
                        <hr />
                        <br /><br />

                        @*<h5 style="color: red;">Session Expirée</h5>*@
                        <div style="color: white; font-weight:bolder; text-align:center;">
                            Désolé, votre session a expiré. Vous allez être redirigé vers la page de connexion dans 5 seconds...
                            <br />
                            <br />
                            Sorry, but your session has timed out. You'll be redirected to the Log On page in 5 seconds...
                        </div>
                    </div>
                </div>

                <div class="clearfix"></div>
                <div class="separator">
                    <div class="clearfix"></div>
                    <br />
                    <div style="width:100%; text-align:center; color: white">
                        <label style="color:green"><i class="fa fa-lock" style="font-size: 70px;"></i> </label>
                    </div>
                </div>
            </section>

        </div>
    </div>
</div>

@Section Scripts
    @Scripts.Render("~/bundles/jqueryval")
End Section
