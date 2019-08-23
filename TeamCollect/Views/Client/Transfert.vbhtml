@modelType  TransfertViewModel
@Imports PagedList.Mvc
@Imports TeamCollect.My.Resources
<!--Body content-->

<div class="contentwrapper">
    <!--Content wrapper-->
    <div class="heading">
        <!--  .heading-->
        <h3 style="color:#353535">
            <i class="fa fa-archive"></i> Transfert de portefeuille
        </h3>
    </div>
    <!-- End  / heading-->
    <!-- Start .row -->
    <div class="row">
        <div class="panel panel-pattern toggle panelMove panelRefresh">
            <!-- Start .panel -->
            <div class="panel-heading">
                <h4 class="panel-title" style="color:#353535"><i class="fa fa-magic"></i> Formulaire  de transfert de portefeuille</h4>
            </div>
            <div class="panel-body pt0 pb0">
                <div id="wizard" class="bwizard">
                    @Using (Html.BeginForm("Transfert", "Client", FormMethod.Post, New With {.role = "form"}))
                        @Html.AntiForgeryToken()
                        @Html.ValidationSummary(True)
                        @Html.HiddenFor(Function(model) model.PorteFeuilleSourceId)
                        @Html.HiddenFor(Function(model) model.ClientId)
                        @<div class="box box-warning">
                            <div class="box-header with-border">
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label>Client: </label>
                                            @*@Html.TextBoxFor(Function(model) model.Client.Nom, New With {.readonly = "true"})*@
                                            @Html.TextBoxFor(Function(model) model.Client.Nom, New With {.class = "form-control", .tabindex = "1", .readonly = "true"})
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label>Ancien Portefeuille : </label>
                                            @*@Html.TextBoxFor(Function(model) model.Client.Nom, New With {.readonly = "true"})*@
                                            @Html.TextBoxFor(Function(model) model.PorteFeuille.Libelle, New With {.class = "form-control", .tabindex = "1", .readonly = "true"})
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label>Nouveau Portefeuille: </label>
                                            @Html.DropDownListFor(Function(model) model.PorteFeuilleId,
New SelectList(Model.IDsPorteFeuille, "Value", "Text"), "Selectionnez un portefeuille", New With {.class = "form-control select2"})
                                        </div>
                                    </div>
                                </div>

                                <div class="box-footer" style="text-align:center">
                                    <input type="submit" value="Enregistrer" class="btn btn-primary btn-sm" />
                                    @Html.ActionLink("Retour", "IndexAgence", Nothing, New With {.class = "btn btn-default btn-sm"})
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