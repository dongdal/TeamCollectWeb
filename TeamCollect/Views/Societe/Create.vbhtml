@ModelType  SocieteViewModel
@Imports TeamCollect.My.Resources
<div class="contentwrapper">
    <!--Content wrapper-->
    <div class="heading">
        <!--  .heading-->
        <h3 style="color:#353535">
            <i class="fa fa-archive"></i>Votre Entreprise
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
                    @Using (Html.BeginForm("Create", "Societe", FormMethod.Post, New With {.role = "form"}))
                        @Html.AntiForgeryToken()
                        @Html.ValidationSummary(True)
                        @<div class="box box-warning">
                             <div class="box-header with-border">
                                 <div class="row">
                                     <div class="col-md-4">
                                         <div class="form-group">
                                             <label>Libelle</label>
                                             <label style="color: #fdcd23"> @Html.ValidationMessageFor(Function(model) model.Libelle) </label>
                                             @Html.EditorFor(Function(model) model.Libelle)
                                         </div>
                                     </div>
                                     <div class="col-md-4">
                                         <div class="form-group">
                                             <label>BP </label>
                                             <label style="color: #fdcd23"> @Html.ValidationMessageFor(Function(model) model.BP) </label>
                                             @Html.EditorFor(Function(model) model.BP)
                                         </div>
                                     </div>
                                     <div class="col-md-4">
                                         <div class="form-group">
                                             <label>Téléphone</label>
                                             <label style="color: #fdcd23"> @Html.ValidationMessageFor(Function(model) model.Telephone) </label>
                                             @Html.EditorFor(Function(model) model.Telephone)
                                         </div>
                                     </div>
                                 </div>

                                 <div class="row">
                                     <div class="col-md-4">
                                         <div class="form-group">
                                             <label>Email</label>
                                             <label style="color: #fdcd23"> @Html.ValidationMessageFor(Function(model) model.Email) </label>
                                             @Html.EditorFor(Function(model) model.Email)
                                         </div>
                                     </div>
                                     <div class="col-md-4">
                                         <div class="form-group">
                                             <label>Adresse </label>
                                             <label style="color: #fdcd23"> @Html.ValidationMessageFor(Function(model) model.Adresse) </label>
                                             @Html.EditorFor(Function(model) model.Adresse)
                                         </div>
                                     </div>
                                     <div class="col-md-4">
                                         <div class="form-group">
                                             <label>Plafond de collecte </label>
                                             <label style="color: #fdcd23"> @Html.ValidationMessageFor(Function(model) model.PlafondDeCollecte) </label>
                                             @Html.EditorFor(Function(model) model.PlafondDeCollecte)
                                         </div>
                                     </div>
                                 </div>

                                 <div class="row">
                                     <div class="col-md-4">
                                         <div class="form-group">
                                             <label>Plafond minmal de collecte</label>
                                             <label style="color: #fdcd23"> @Html.ValidationMessageFor(Function(model) model.MinCollecte) </label>
                                             @Html.EditorFor(Function(model) model.MinCollecte)
                                         </div>
                                     </div>

                                     <div class="col-md-4">
                                         <div class="form-group">
                                             <label>Plafond maximal de collecte</label>
                                             <label style="color: #fdcd23"> @Html.ValidationMessageFor(Function(model) model.MAxCollecte) </label>
                                             @Html.EditorFor(Function(model) model.MAxCollecte)
                                         </div>
                                     </div>

                                 </div>

                                 <div class="box-footer" style="text-align:center">
                                     <input type="submit" value="Enregistrer" class="btn btn-primary btn-sm" />
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