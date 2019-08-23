@modelType PagedList.IPagedList(Of HistoriqueMouvement)
@Imports PagedList.Mvc
@Imports TeamCollect.My.Resources
<!--Body content-->
@If (ViewBag.nbreDouverture >= 1) Then
    @<div class="contentwrapper">
    <!--Content wrapper-->
    <div class="heading">
        <!--  .heading-->
        <h3 style="color:#353535">
            <i class="fa fa-archive"></i> Importations des données clients 
        </h3>

    </div>
    <!-- End  / heading-->
    <!-- Start .row -->
    <br />
        <div class="row">
            <p>
                <h1 style="color: #fdcd23">
                    OUPS!!! Il ya au moins une caisse ouverte veuillez la fermer, avant d'avoir un visuel sur l'importation des données. 
                    La cohérence des données en dépend.
                </h1>
            </p>
           
        </div>
</div>
Else
@<div class="contentwrapper">
    <!--Content wrapper-->
    <div class="heading">
        <!--  .heading-->
        <h3 style="color:#353535">
            <i class="fa fa-archive"></i> Importations des données clients à la date du @Now.Date.ToString("d")
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
                            OUPS!!! Une erreur lors de la lecture du fichier veuillez verifier que le fichier respecte le format de données
                        </h3>
                     </p>
            End If
         </fieldset>
                @Using (Html.BeginForm("Import", "HistoriqueMouvement", FormMethod.Post, New With {.class = "form-inline", .role = "form", .enctype = "multipart/form-data"}))
                    @<div class="col-md-4">
                        <input type="file" class=" form-control" required id="Fichier" name="Fichier" />
                    </div>
                    @<input type="hidden" value="true" class=" form-control" id="VientDeForm" name="VientDeForm" />
                    @<button class="btn btn-primary btn-sm" type="submit" style="margin-top:5px"> <i class="fa fa-file-excel-o"></i>Importer le Fichier de données</button>
                End Using
</div>
  
    
</div>

End If