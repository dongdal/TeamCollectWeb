@ModelType  TypeCarnetViewModel
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
                <h4 class="panel-title" style="color:#353535"><i class="fa fa-magic"></i> Formulaire de modification...</h4>
            </div>
            <div class="panel-body pt0 pb0">
                <div id="wizard" class="bwizard">
                    @Using (Html.BeginForm())
                        @Html.AntiForgeryToken()
                        @Html.ValidationSummary(True)
                        @Html.HiddenFor(Function(model) model.Id)
                        @Html.HiddenFor(Function(model) model.Etat)
                        @Html.HiddenFor(Function(model) model.DateCreation)

                        @<div class="box box-warning">
                            <div class="box-header with-border">
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label>Libelle </label>
                                            <label style="color: #fdcd23"> @Html.ValidationMessageFor(Function(model) model.Libelle) </label>
                                            @Html.EditorFor(Function(model) model.Libelle)
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label>Prix</label>
                                            <label style="color: #fdcd23"> @Html.ValidationMessageFor(Function(model) model.Prix) </label>
                                            @Html.EditorFor(Function(model) model.Prix)
                                        </div>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>Nom</label>
                                            <label style="color: #fdcd23"> @Html.ValidationMessageFor(Function(model) model.Etat) </label>
                                            @Html.EditorFor(Function(model) model.Etat)
                                        </div>
                                    </div>

                                    <div class="box-footer" style="text-align:center">
                                        <input type="submit" value="Enregistrer" class="btn btn-primary btn-sm" />
                                        @Html.ActionLink("Retour", "Index", "TypeCarnet", Nothing, New With {.class = "btn btn-default btn-sm"})
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
