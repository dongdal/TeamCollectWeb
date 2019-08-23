@ModelType ExternalLoginConfirmationViewModel
@Code
    ViewBag.Title = "S’inscrire"
End Code

<h2>@ViewBag.Title.</h2>
<h3>Associer votre compte @ViewBag.LoginProvider.</h3>

@Using Html.BeginForm("ExternalLoginConfirmation", "Account", New With { .ReturnUrl = ViewBag.ReturnUrl }, FormMethod.Post, New With {.class = "form-horizontal", .role = "form"})
    @Html.AntiForgeryToken()

    @<text>
    <h4>Formulaire d'association</h4>
    <hr />
    @Html.ValidationSummary(True)
    <p class="text-info">
        Vous avez été authentifié avec succès avec <strong>@ViewBag.LoginProvider</strong>.
            Veuillez entrer un nom d'utilisateur pour le site ci-dessous, puis cliquez sur le bouton S'enregistrer afin de compléter la
            connexion.
    </p>
    <div class="form-group">
        @Html.LabelFor(Function(m) m.UserName, New With {.class = "col-md-2 control-label"})
        <div class="col-md-10">
            @Html.TextBoxFor(Function(m) m.UserName, New With {.class = "form-control"})
            @Html.ValidationMessageFor(Function(m) m.UserName)
        </div>
    </div>
    <div class="form-group">
        <div class="col-md-offset-2 col-md-10">
            <input type="submit" class="btn btn-default" value="S'enregistrer" />
        </div>
    </div>
    </text>
End Using

@Section Scripts
    @Scripts.Render("~/bundles/jqueryval")
End Section
