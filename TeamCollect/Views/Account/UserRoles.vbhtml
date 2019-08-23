@ModelType SelectUserRolesViewModel
@Imports TeamCollect.My.Resources


<div class="contentwrapper">
    <!--Content wrapper-->
    <div class="heading">
        <!--  .heading-->
        <h3 style="color:#353535">
            <i class="fa fa-archive"></i>Les Profils
        </h3>
    </div>
    <!-- End  / heading-->
    <!-- Start .row -->
    <div class="row">
        <div class="panel panel-pattern toggle panelMove panelRefresh">
            <!-- Start .panel -->
            <div class="panel-heading">
                <h4 class="panel-title" style="color:#353535"><i class="fa fa-magic"></i> Votre selection...</h4>
            </div>
            <div class="panel-body pt0 pb0">
                <h2> <i style="color:white" class=" fa fa-cogs"></i> Selectionnez des Roles pour l'utilisateur <i style="color: #fdcd23" class=" fa fa-arrow-right"></i> <span style="color: #fdcd23"> @Html.DisplayFor(Function(m) m.UserName) </span> </h2>

                @Using (Html.BeginForm("UserRoles", "Account", FormMethod.Post, New With {.encType = "multipart/form-data", .name = "myform"}))
                    @Html.AntiForgeryToken()

                    @<div class="form-horizontal">
                        @Html.ValidationSummary(True)
                        <div class="form-group">
                            <div class="col-md-10">

                                @Html.HiddenFor(Function(m) m.UserName)
                            </div>
                        </div>
                        <table style=" font-size:large">
                            <thead>
                                <tr>
                                    <th style="text-align:right; width:50px"></th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr>
                                    <td> @Html.EditorFor(Function(Model) Model.Roles) </td>
                                </tr>
                            </tbody>
                        </table>
                        <br />
                        <div class="form-group">
                            <div class="col-md-offset-2 col-md-10">
                                <input type="submit" value="Save" class="btn btn-primary" />
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



