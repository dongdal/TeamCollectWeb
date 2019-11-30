@ModelType StatViewModel
@Imports TeamCollect.My.Resources

  <div class="contentwrapper">
        <!--Content wrapper-->
        <div class="heading">
            <!--  .heading-->
            <h3 style="color:#353535">
                <i class="fa fa-archive"></i> Fiche Commissions par Collectrice avec rémunération
            </h3>
        </div>
        <!-- End  / heading-->
        <!-- Start .row -->
        <br />
        <div class="row">
            @Using (Html.BeginForm())
                @<div class="col-md-12 form-group">
                    <fieldset>
                        <legend style="color:white"> Votre Filtre</legend>

                        <div class="col-md-4">
                            <div class="input-group col-md-12">
                                <label>Mois</label>
                                <div class="input-group input-icon col-md-12">

                                    @Html.DropDownListFor(Function(model) model.Mois,
New SelectList(Model.ListeMois, "Value", "Text"), "Selectionnez un mois", New With {.class = "form-control select2 col-md-2", .required = "required"})
                                    @Html.ValidationMessageFor(Function(model) model.Mois)
                                </div>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="input-group  col-md-12">
                                <label>Année</label>
                                <div class="input-group input-icon  col-md-12">
                                    @Html.DropDownListFor(Function(model) model.Annee,
New SelectList(Model.ListeAnnee, "Value", "Text"), "Selectionnez une année", New With {.class = "form-control select2", .required = "required"})
                                    @Html.ValidationMessageFor(Function(model) model.Annee)
                                </div>

                            </div>
                        </div>
                        <div class="col-md-2">
                            <br />
                            <div class="input-group">
                                <span class="input-group-btn">
                                    <button id="lnkReport" class="btn btn-primary" type="button" style="margin-top:5px"> Visualiser <i class="fa fa-print"></i></button>
                                </span>
                            </div>
                        </div>
                    </fieldset>
                </div>
            End Using
        </div>
        <div class="row">
            <div class="col-md-9">
                <div class="embed-responsive embed-responsive-16by9">
                    <iframe id="ifrReport" class="embed-responsive-item"></iframe>
                </div>
            </div>
        </div>
   </div>


<script>
    $('#lnkReport').on('click', function () {
        var AgenceId = @ViewBag.UserAgenceId.ToString; //$('#AgenceId').val();
        var Mois = $('#Mois').val();
        var Annee = $('#Annee').val();
        if (Annee == null || Annee == "" || Mois == null || Mois == "") {
            alert("Merci de bien vouloir sélectionner un mois et une année.");
        } else {
            $('#ifrReport').attr('src', '@Url.Content("~/Report/NewReport.aspx")?AgenceId=' + AgenceId + '&Mois=' + Mois + '&Annee=' + Annee + '&type=CommissionCollectriceAvecGrilleRemuneration')
        }
        //$('#ifrReport').attr('src', '@Url.Content("~/Report/NewReport.aspx")?AgenceId=' + AgenceId +  '&type=FicheCommissionsCollecteurs')FicheCommissionsCollecteurs
        });

</script>