@Imports Microsoft.AspNet.Identity
@ModelType ManageUserViewModel

<p class="text-info">Vous êtes connecté en tant que <strong>@User.Identity.GetUserName()</strong>.</p>

@Using Html.BeginForm("Manage", "Account", FormMethod.Post, New With {.class = "form-horizontal", .role = "form"})

    @Html.AntiForgeryToken()

    @<text>

        <h4> Formulaire de modification du mot de passe  </h4>
        <hr />
        <div class="form-group">
            @Html.LabelFor(Function(m) m.OldPassword, New With {.class = "col-md-2 control-label"})
            <div class="col-md-10">
                @Html.PasswordFor(Function(m) m.OldPassword, New With {.class = "form-control"})
                @Html.ValidationMessageFor(Function(model) model.OldPassword)
            </div>
        </div>
        <div class="form-group">
            @Html.LabelFor(Function(m) m.NewPassword, New With {.class = "col-md-2 control-label"})
            <div class="col-md-10">
                @Html.PasswordFor(Function(m) m.NewPassword, New With {.class = "form-control"})
                @Html.ValidationMessageFor(Function(model) model.NewPassword)
            </div>
        </div>
        <div class="form-group">
            @Html.LabelFor(Function(m) m.ConfirmPassword, New With {.class = "col-md-2 control-label"})
            <div class="col-md-10">
                @Html.PasswordFor(Function(m) m.ConfirmPassword, New With {.class = "form-control"})
                @Html.ValidationMessageFor(Function(model) model.ConfirmPassword)
            </div>
        </div>

        <div class="form-group">
            <div class="col-md-offset-2 col-md-10">
                <input type="submit" value="Modifier le mot de passe" class="btn btn-default" />
            </div>
        </div>
    </text>
End Using
