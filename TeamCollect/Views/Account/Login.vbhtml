@ModelType LoginViewModel

@Code
    ViewBag.Title = "Formulaire de Connexion"
    Layout = "~/Views/Shared/_LayoutLogin.vbhtml"
End Code
<br /><br /><br /><br />
<div class="row">
    <div class="col-md-12">
       <div class="col-md-6">
        </div>

        <div class="col-md-6">
            <h2> <i class="fa fa-lock" style="color:white"></i> @ViewBag.Title.</h2>
            <section id="loginForm">
                @Using Html.BeginForm("Login", "Account", New With {.ReturnUrl = ViewBag.ReturnUrl}, FormMethod.Post, New With {.class = "form-horizontal", .role = "form"})
                    @Html.AntiForgeryToken()
                    @<text>
                        <hr />
                        @Html.ValidationSummary(True)
                        <div class="form-group">
                            @Html.LabelFor(Function(m) m.UserName, New With {.class = "col-md-4 control-label", .style = "color: white"})
                            <div class="col-md-8">
                                @Html.TextBoxFor(Function(m) m.UserName, New With {.class = "form-control", .style = "background-color: #ffffff"})
                                @Html.ValidationMessageFor(Function(m) m.UserName)
                            </div>
                        </div>
                        <div class="form-group">
                            @Html.LabelFor(Function(m) m.Password, New With {.class = "col-md-4 control-label", .style = "color: white"})
                            <div class="col-md-8">
                                @Html.PasswordFor(Function(m) m.Password, New With {.class = "form-control", .style = "background-color: #ffffff"})
                                @Html.ValidationMessageFor(Function(m) m.Password)
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-md-offset-7 col-md-5">
                                <input type="submit" value="Connexion" class="btn btn-primary" />
                            </div>
                        </div>
                        <hr />
                    </text>

                End Using
            </section>
        </div>
    </div>
</div>
@Section Scripts
    @Scripts.Render("~/bundles/jqueryval")
End Section
