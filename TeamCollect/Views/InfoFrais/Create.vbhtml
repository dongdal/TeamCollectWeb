@ModelType  InfoFraisViewModel
@Imports TeamCollect.My.Resources
<div class="contentwrapper">
    <!--Content wrapper-->
    <div class="heading">
        <!--  .heading-->
        <h3 style="color:#353535">
            <i class="fa fa-archive"></i>la facturation: @Model.LibelleDelaGrille
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
                    @Using (Html.BeginForm("Create", "InfoFrais", FormMethod.Post, New With {.role = "form", .GrilleId = ViewBag.GrilleId}))
                        @Html.AntiForgeryToken()
                        @Html.ValidationSummary(True)
                        @<input type="hidden" name="GrilleId" id="GrilleId" value="@Model.GrilleId">
                        @<div class="box box-warning">
                            <div class="box-header with-border">
                                <div class="row">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>De</label>
                                            <label style="color: #fdcd23"> @Html.ValidationMessageFor(Function(model) model.BornInf) </label>
                                            <input class="form-control" data-val="true" data-val-regex="Votre Format de données n&#39;est pas correct! (00,00)" data-val-regex-pattern="^(\d+(((\,))\d+)?)$" data-val-required=":Cette Information est Obligatoire" id="BornInf" name="BornInf" type="text" value="@Model.BornInf" readonly/>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>A</label>
                                            <label style="color: #fdcd23"> @Html.ValidationMessageFor(Function(model) model.BornSup) </label>
                                            @Html.EditorFor(Function(model) model.BornSup)
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Frais</label>
                                            <label style="color: #fdcd23"> @Html.ValidationMessageFor(Function(model) model.Frais) </label>
                                            @Html.EditorFor(Function(model) model.Frais)
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Taux</label>
                                            <label style="color: #fdcd23"> @Html.ValidationMessageFor(Function(model) model.Taux) </label>
                                            @Html.EditorFor(Function(model) model.Taux)
                                        </div>
                                    </div>
                                </div>
                                <div class="box-footer" style="text-align:center">
                                    <input type="submit" value="Enregistrer" class="btn btn-primary btn-sm" />
                                    @Html.ActionLink("Retour", "Index", New With {.GrilleId = Model.GrilleId}, New With {.class = "btn btn-default btn-sm"})
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