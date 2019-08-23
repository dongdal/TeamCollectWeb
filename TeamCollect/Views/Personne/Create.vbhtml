@ModelType  PersonneViewModel
@Imports TeamCollect.My.Resources
<div class="contentwrapper">
    <!--Content wrapper-->
    <div class="heading">
        <!--  .heading-->
        <h3 style="color:#353535">
            <i class="fa fa-archive"></i>Le Personnel
        </h3>
    </div>
    <!-- End  / heading-->
    <!-- Start .row -->
    <div class="row">
        <div class="panel panel-pattern toggle panelMove panelRefresh">
            <!-- Start .panel -->
            <div class="panel-heading">
                <h4 class="panel-title" style="color:#353535"><i class="fa fa-magic"></i> Formulaire d'enregistrement...</h4>
            </div>
            <div class="panel-body pt0 pb0">
                <div id="wizard" class="bwizard">
                    @Using (Html.BeginForm("Create", "Personne", FormMethod.Post, New With {.role = "form"}))
                        @Html.AntiForgeryToken()
                        @Html.ValidationSummary(True)
                        @<div class="box box-warning">
                            <div class="box-header with-border">
                                <div class="row">
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>Adresse </label>
                                            <label style="color: #fdcd23"> @Html.ValidationMessageFor(Function(model) model.Adresse) </label>
                                            @Html.EditorFor(Function(model) model.Adresse)
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>Quartier</label>
                                            <label style="color: #fdcd23"> @Html.ValidationMessageFor(Function(model) model.Quartier) </label>
                                            @Html.EditorFor(Function(model) model.Quartier)
                                        </div>
                                    </div>

                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>Profession</label>
                                            <label style="color: #fdcd23"> @Html.ValidationMessageFor(Function(model) model.SecteurActiviteId) </label>
                                            @Html.DropDownListFor(Function(model) model.SecteurActiviteId,
New SelectList(Model.LesSecteursActivite, "Value", "Text"), "Selectionnez une Profession", New With {.class = "form-control select2"})
                                        </div>
                                    </div>

                                </div>

                                <div class="row">
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>Nom</label>
                                            <label style="color: #fdcd23"> @Html.ValidationMessageFor(Function(model) model.Nom) </label>
                                            @Html.EditorFor(Function(model) model.Nom)
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>Prénom </label>
                                            <label style="color: #fdcd23"> @Html.ValidationMessageFor(Function(model) model.Prenom) </label>
                                            @Html.EditorFor(Function(model) model.Prenom)
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>Genre </label>
                                            <label style="color: #fdcd23"> @Html.ValidationMessageFor(Function(model) model.Sexe) </label>
                                            <select id="Sexe" name="Sexe" class="fancy-select1 form-control">
                                                <option data-icon="" value=""> Choix du genre</option>
                                                <option data-icon="fa fa-male" value="M">Masculin</option>
                                                <option data-icon="fa fa-female" value="F">Féminin</option>
                                            </select>
                                        </div>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>Téléphone</label>
                                            <label style="color: #fdcd23"> @Html.ValidationMessageFor(Function(model) model.Telephone) </label>
                                            @Html.EditorFor(Function(model) model.Telephone)
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>CNI</label>
                                            <label style="color: #fdcd23"> @Html.ValidationMessageFor(Function(model) model.CNI) </label>
                                            @Html.EditorFor(Function(model) model.CNI)
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>Agence du Personnel</label>
                                            <label style="color: #fdcd23"> @Html.ValidationMessageFor(Function(model) model.AgenceId) </label>
                                            @Html.DropDownListFor(Function(model) model.AgenceId,
                         New SelectList(Model.IDsAgence, "Value", "Text"), "Selectionnez une Agence", New With {.class = "form-control select2"})
                                        </div>
                                    </div>
                                </div>

                                <div class="box-footer" style="text-align:center">
                                    <input type="submit" value="Enregistrer" class="btn btn-primary btn-sm" />
                                    @Html.ActionLink("Retour", "Index", "Personne", Nothing, New With {.class = "btn btn-default btn-sm"})
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

@Section Scripts
    @Scripts.Render("~/bundles/jqueryval")
End Section