@ModelType  JournalCaisseViewModel
@Imports TeamCollect.My.Resources

<div class="contentwrapper">
    <!--Content wrapper-->
    <div class="heading">
        <!--  .heading-->
        <h3 style="color:#353535">
            <i class="fa fa-archive"></i>Fermeture de caisse
        </h3>
    </div>
    <!-- End  / heading-->
    <!-- Start .row -->
    <div class="row">
        <div class="panel panel-pattern toggle panelMove panelRefresh">
            <!-- Start .panel -->
            <div class="panel-heading">
                <h4 class="panel-title" style="color:#353535"><i class="fa fa-magic"></i> Formulaire de fermeture...</h4>
            </div>
            <div class="panel-body pt0 pb0">
                <div id="wizard" class="bwizard">
                    @Using (Html.BeginForm())
                        @Html.AntiForgeryToken()
                        @Html.ValidationSummary(True)
                        @Html.HiddenFor(Function(model) model.Id)
                        @Html.HiddenFor(Function(model) model.Etat)
                        @Html.HiddenFor(Function(model) model.DateCreation)
                        @Html.HiddenFor(Function(model) model.DateOuverture)
                        @Html.HiddenFor(Function(model) model.CollecteurId)
                        @Html.HiddenFor(Function(model) model.FondCaisse)
                        @Html.HiddenFor(Function(model) model.PlafondDebat)
                        @Html.HiddenFor(Function(model) model.PlafondEnCours)
                        
                        @<div class="box box-warning">
                            <div class="box-header with-border">
                                <div class="row">
                                    <div class="col-md-4">
                                        <div class="form-group">
                                           <label>le Collecteur </label><br />
                                           <label style="font-size: 20px"> @Model.IDsCollecteur.First.Text.ToUpper </label> 
                                         </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>Montant Réel en caisse</label>
                                            <label style="color: #fdcd23"> @Html.ValidationMessageFor(Function(model) model.MontantReel) </label>
                                            @Html.EditorFor(Function(model) model.MontantReel)
                                        </div>
                                    </div>
                                </div>


                                <div class="box-footer" style="text-align:center">
                                    <input type="submit" value="Enregistrer" class="btn btn-primary btn-sm" />
                                    @Html.ActionLink("Retour", "Index", "JournalCaisse", Nothing, New With {.class = "btn btn-default btn-sm"})
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