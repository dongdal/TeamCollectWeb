@ModelType EditUserViewModel
@Imports TeamCollect.My.Resources

<div class="contentwrapper">
    <!--Content wrapper-->
    <div class="heading">
        <!--  .heading-->
        <h3 style="color:#353535">
            <i class="fa fa-archive"></i>Les Comptes Utilisateurs
        </h3>
    </div>
    <!-- End  / heading-->
    <!-- Start .row -->
    <div class="row">
        <div class="panel panel-pattern toggle panelMove panelRefresh">
            <!-- Start .panel -->
            <div class="panel-heading">
                <h4 class="panel-title" style="color:#353535"><i class="fa fa-magic"></i> Formulaire de modification...</h4>
            </div>
            <div class="panel-body pt0 pb0">
                <div id="wizard" class="bwizard">
                    @Using (Html.BeginForm("Edit", "Account", FormMethod.Post, New With {.role = "form"}))
                        @Html.AntiForgeryToken()
                        @Html.ValidationSummary(True)
                        @Html.HiddenFor(Function(model) model.Id)
                        @Html.HiddenFor(Function(model) model.PasswordExpiredDate)
                        @<div class="box box-warning">
                            <div class="box-header with-border">
                                <div class="row">
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>le Personnel</label>
                                            <label style="color: #fdcd23"> @Html.ValidationMessageFor(Function(model) model.PersonneId) </label>
                                            @Html.DropDownListFor(Function(model) model.PersonneId,
                                                New SelectList(Model.IDspersonne, "Value", "Text"), New With {.class = "form-control select2"})

                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>Nom utilisateur</label>
                                            <label style="color: #fdcd23"> @Html.ValidationMessageFor(Function(model) model.UserName) </label>
                                            @Html.TextBoxFor(Function(m) m.UserName, New With {.class = "form-control"})
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>Mot de Passe</label>
                                            <label style="color: #fdcd23"> @Html.ValidationMessageFor(Function(model) model.Password) </label>
                                            @Html.PasswordFor(Function(m) m.Password, New With {.class = "form-control"})
                                        </div>
                                    </div>

                                </div>
                                <div class="row">
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>Confirmez le Mot de Passe</label>
                                            <label style="color: #fdcd23"> @Html.ValidationMessageFor(Function(model) model.ConfirmPassword) </label>
                                            @Html.PasswordFor(Function(m) m.ConfirmPassword, New With {.class = "form-control"})
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>Code secret</label>
                                            <label style="color: #fdcd23"> @Html.ValidationMessageFor(Function(model) model.CodeSecret) </label>
                                            @Html.EditorFor(Function(m) m.CodeSecret, New With {.class = "form-control"})
                                        </div>
                                    </div>
                                </div>

                                <hr style="border: 1px solid #c4c4c4; " />

                                <div class="box-footer" style="text-align:center">
                                    <input type="submit" value="Enregistrer" class="btn btn-primary btn-sm" />
                                    @Html.ActionLink("Retour", "Index", "Account", Nothing, New With {.class = "btn btn-default btn-sm"})
                                </div>
                                <br />
                            </div>
                        </div>
                    End Using
                </div>
            </div>
        </div>
    </div>
</div>

@section Scripts
    @Scripts.Render("~/bundles/jqueryval")
End Section
