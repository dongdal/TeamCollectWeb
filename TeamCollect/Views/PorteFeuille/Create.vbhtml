@ModelType  PorteFeuilleViewModel
@Imports TeamCollect.My.Resources
<div class="contentwrapper">
    <!--Content wrapper-->
    <div class="heading">
        <!--  .heading-->
        <h3 style="color:#353535">
            <i class="fa fa-archive"></i>PorteFeuille
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
                    @Using (Html.BeginForm("Create", "PorteFeuille", FormMethod.Post, New With {.role = "form"}))
                        @Html.AntiForgeryToken()
                        @Html.ValidationSummary(True)
                        @<div class="box box-warning">
                            <div class="box-header with-border">
                                <div class="row">
                                    <div class="col-md-8">
                                        <div class="form-group">
                                            <label>Le collecteur </label>
                                            <label style="color: #fdcd23"> @Html.ValidationMessageFor(Function(model) model.CollecteurId) </label>
                                            @Html.DropDownListFor(Function(model) model.CollecteurId,
                                                New SelectList(Model.IDsCollecteur, "Value", "Text"), "Selectionnez un Collecteur", New With {.class = "form-control select2"})
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>Libelle du Portefeuille</label>
                                            <label style="color: #fdcd23"> @Html.ValidationMessageFor(Function(model) model.Libelle) </label>
                                            @Html.EditorFor(Function(model) model.Libelle)
                                        </div>
                                    </div>
                                </div>


                                <div class="box-footer" style="text-align:center">
                                    <input type="submit" value="Enregistrer" class="btn btn-primary btn-sm" />
                                    @Html.ActionLink("Retour", "Index", Nothing, New With {.class = "btn btn-default btn-sm"})
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