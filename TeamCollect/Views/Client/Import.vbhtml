@modelType  ImportClientViewModel
@Imports PagedList.Mvc
@Imports TeamCollect.My.Resources
<!--Body content-->

<div class="contentwrapper">
    <!--Content wrapper-->
    <div class="heading">
        <!--  .heading-->
        <h3 style="color:#353535">
            <i class="fa fa-archive"></i> Importations des fichiers clients
        </h3>

    </div>
    <!-- End  / heading-->
    <!-- Start .row -->
    <br />
    <div class="col-md-12">
        <fieldset>
            <legend style="color:white"> Selectionnez un Fichier de données dans l'explorateur </legend>
            @If Not (IsNothing(ViewBag.Error)) Then
                     @<p>
                          <h3 style="color: #fdcd23">
                              @Html.ViewBag.Msg
                              OUPS!!! Une erreur lors de la lecture du fichier veuillez verifier que le fichier respecte le format de données
                          </h3>
                     </p>
            End If
         </fieldset>
                @Using (Html.BeginForm("Import", "Client", FormMethod.Post, New With {.class = "form-inline", .role = "form", .enctype = "multipart/form-data"}))
                    @Html.AntiForgeryToken()
                    @Html.ValidationSummary(True)
                    @<div class="col-md-4">
                         <label>Le collecteur </label>
                         <label style="color: #fdcd23"> @Html.ValidationMessageFor(Function(model) model.CollecteurId) </label>
                         @Html.DropDownListFor(Function(model) model.CollecteurId,
                                                New SelectList(Model.IDsCollecteur, "Value", "Text"), "Selectionnez un Collecteur", New With {.class = "form-control select2"})
                    </div>
                    @<div class="col-md-4">
                        <input type="file" class=" form-control" required id="Fichier" name="Fichier" />
                    </div>
                    @<input type="hidden" value="true" class=" form-control" id="VientDeForm" name="VientDeForm" />
                    @<button class="btn btn-primary btn-sm" type="submit" style="margin-top:5px"> <i class="fa fa-file-excel-o"></i>Importer le Fichier de données</button>
                End Using
</div>
  
    
</div>