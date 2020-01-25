@Imports Microsoft.AspNet.Identity
@ModelType ManageUserViewModel
@Code
    ViewBag.Title = "Gérer le compte"
    Layout = "~/Views/Shared/_LayoutManage.vbhtml"
    Dim PasswordExpiredText As String = ""
End Code

<h2>@ViewBag.Title.</h2>

        @If AppSession.PasswordExpiredDate < DateTime.UtcNow Then
            PasswordExpiredText = "Votre mot de passe est arrivé à expiration. Merci de le changer. / Your password has expired. Thank you for changing it."
        End If

<p class="text-success" style="font-size: 20px;" >@ViewBag.StatusMessage</p>
<p class="text-warning" style="font-size: 16px;" >@PasswordExpiredText </p>

<div class="row">
    <div class="col-md-12">
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

        @*<section id="externalLogins">
            @Html.Action("RemoveAccountList")
            @Html.Partial("_ExternalLoginsListPartial", New With {.Action = "LinkLogin", .ReturnUrl = ViewBag.ReturnUrl})
        </section>*@
    </div>
</div>
@Section Scripts
    @Scripts.Render("~/bundles/jqueryval")
End Section
