@ModelType TeamCollect.CategorieRemunerationViewModel
@Imports TeamCollect.My.Resources
@Code
    ViewData("Title") = Resource.CategorieRemunerationDelete
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
                <h4 class="panel-title" style="color:#353535"><i class="fa fa-magic"></i> @Resource.ConfirmDelete</h4>
            </div>
            <div class="panel-body pt0 pb0">
                <div id="wizard" class="bwizard">
                    @Using (Html.BeginForm())
                        @Html.AntiForgeryToken()
                        @Html.ValidationSummary(True)
                        @Html.HiddenFor(Function(model) model.Id)
                        @Html.HiddenFor(Function(model) model.StatutExistant)
                        @Html.HiddenFor(Function(model) model.DateCreation)
                        @<div class="box box-warning">
                             <div class="box-header with-border">
                                 <div class="row">
                                     <div class="col-md-4">
                                         <div class="form-group">
                                             @Html.LabelFor(Function(mode) Model.Libelle)
                                             @Html.TextBoxFor(Function(model) model.Libelle, New With {.readonly = "True", .class = "form-control"})
                                         </div>
                                     </div>
                                     <div class="col-md-4">
                                         <div class="form-group">
                                             @Html.LabelFor(Function(mode) Model.SalaireDeBase)
                                             @Html.TextBoxFor(Function(model) model.SalaireDeBase, New With {.readonly = "True", .class = "form-control"})
                                         </div>
                                     </div>
                                     <div class="col-md-4">
                                         <div class="form-group">
                                             @Html.LabelFor(Function(mode) Model.CommissionMinimale)
                                             @Html.TextBoxFor(Function(model) model.CommissionMinimale, New With {.readonly = "True", .class = "form-control"})
                                         </div>
                                     </div>
                                 </div>

                                 <div class="row">
                                     <div class="col-md-4">
                                         <div class="form-group">
                                             @Html.LabelFor(Function(mode) Model.PourcentageCommission)
                                             @Html.TextBoxFor(Function(model) model.PourcentageCommission, New With {.readonly = "True", .class = "form-control"})
                                         </div>
                                     </div>
                                     <div class="col-md-4">
                                         <div class="form-group">
                                             @Html.LabelFor(Function(mode) Model.DateCreation)
                                             @Html.TextBoxFor(Function(model) model.DateCreation, New With {.readonly = "True", .class = "form-control"})
                                         </div>
                                     </div>
                                     <div class="col-md-4">
                                         <div class="form-group">
                                             @Html.LabelFor(Function(mode) Model.UserId)
                                             @Html.TextBoxFor(Function(model) model.User.UserName, New With {.readonly = "True", .class = "form-control"})
                                         </div>
                                     </div>
                                 </div>

                                 <div class="box-footer" style="text-align:center">
                                     <input type="submit" value="@Resource.BtnDelete" class="btn btn-primary btn-sm" />
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
