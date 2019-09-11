@ModelType TeamCollect.CategorieRemunerationViewModel
@Imports TeamCollect.My.Resources
@Code
    ViewData("Title") = Resource.CategorieRemunerationEdit
End Code


<div class="contentwrapper">
    <!--Content wrapper-->
    <div class="heading">
        <!--  .heading-->
        <h3 style="color:#353535">
            <i class="fa fa-archive"></i>@Resource.CategorieRemunerationManage
        </h3>
    </div>
    <!-- End  / heading-->
    <!-- Start .row -->
    <div class="row">
        <div class="panel panel-pattern toggle panelMove panelRefresh">
            <!-- Start .panel -->
            <div class="panel-heading">
                <h4 class="panel-title" style="color:#353535"><i class="fa fa-magic"></i> @Resource.EditForm</h4>
            </div>
            <div class="panel-body pt0 pb0">
                <div id="wizard" class="bwizard">
                    @Using (Html.BeginForm())
                        @Html.AntiForgeryToken()
                        @Html.ValidationSummary(True)
                        @Html.HiddenFor(Function(model) model.Id)
                        @Html.HiddenFor(Function(model) model.StatutExistant)
                        @Html.HiddenFor(Function(model) model.DateCreation)
                        @Html.HiddenFor(Function(model) model.UserId)
                        @<div class="box box-warning">
                            <div class="box-header with-border">
                                <div class="row">
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            @Html.LabelFor(Function(mode) Model.Libelle)
                                            @*<label>Code Secret(Adresse MAC) </label>*@
                                            <label style="color: #fdcd23"> @Html.ValidationMessageFor(Function(model) model.Libelle) </label>
                                            @Html.EditorFor(Function(model) model.Libelle)
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            @Html.LabelFor(Function(mode) Model.SalaireDeBase)
                                            @*<label>Adresse </label>*@
                                            <label style="color: #fdcd23"> @Html.ValidationMessageFor(Function(model) model.SalaireDeBase) </label>
                                            @Html.EditorFor(Function(model) model.SalaireDeBase)
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            @Html.LabelFor(Function(mode) Model.CommissionMinimale)
                                            @*<label>Quartier</label>*@
                                            <label style="color: #fdcd23"> @Html.ValidationMessageFor(Function(model) model.CommissionMinimale) </label>
                                            @Html.EditorFor(Function(model) model.CommissionMinimale)
                                        </div>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            @Html.LabelFor(Function(mode) Model.PourcentageCommission)
                                            @*<label>Nom</label>*@
                                            <label style="color: #fdcd23"> @Html.ValidationMessageFor(Function(model) model.PourcentageCommission) </label>
                                            @Html.EditorFor(Function(model) model.PourcentageCommission)
                                        </div>
                                    </div>
                                </div>

                                <div class="box-footer" style="text-align:center">
                                    <input type="submit" value="@Resource.Btn_Edit" class="btn btn-primary btn-sm" />
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
