@ModelType ChoixParamAjoutViewModel
@Imports TeamCollect.My.Resources

<div class="contentwrapper">
    <!--Content wrapper-->
    <div class="heading">
        <!--  .heading-->
        <h3 style="color:#353535">
            <i class="fa fa-archive"></i>Calcul des Agios
        </h3>
    </div>
    <!-- End  / heading-->
    <!-- Start .row -->
    <div class="row">
        <div class="panel panel-pattern toggle panelMove panelRefresh">
            <!-- Start .panel -->
            <div class="panel-heading">
                <h4 class="panel-title" style="color:#353535"><i class="fa fa-magic"></i> Formulaire de calcul des Agios mensuel...</h4>
            </div>
            <div class="panel-body pt0 pb0">
                <div id="wizard" class="bwizard">
                    @Using (Html.BeginForm("CreateByProc", "CalculAjout", FormMethod.Post, New With {.role = "form"}))
                        @Html.AntiForgeryToken()
                        @Html.ValidationSummary(True)
                        @<div class="box box-warning">
                            <div class="box-header with-border">
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label>Mois </label>

                                            @Html.DropDownListFor(Function(model) model.Mois,
New SelectList(Model.ListeMois, "Value", "Text"), "Selectionnez un mois", New With {.class = "form-control select2"})
                                            @Html.ValidationMessageFor(Function(model) model.Mois)
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label>Année </label>
                                            @Html.DropDownListFor(Function(model) model.Annee,
New SelectList(Model.ListeAnnee, "Value", "Text"), "Selectionnez une année", New With {.class = "form-control select2"})
                                            @Html.ValidationMessageFor(Function(model) model.Annee)
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-6">
                                           
                                        </div>
                                        <div class="box-footer" style="text-align:center">
                                            <input type="submit" value="Enregistrer" class="btn btn-primary btn-sm" />
                                            @Html.ActionLink("Retour", "Index", "CalculAjout", Nothing, New With {.class = "btn btn-default btn-sm"})
                                        </div>
                                    </div>
                                    
                                    <br />
                                </div>
                            </div>

                        </div>
                    End Using
                </div>
            </div>
        </div>
    </div>

    @Section Scripts
        @Scripts.Render("~/bundles/jqueryval")
    End Section
