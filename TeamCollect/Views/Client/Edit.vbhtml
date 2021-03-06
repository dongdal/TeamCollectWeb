@ModelType  ClientViewModel
@Imports TeamCollect.My.Resources

<div class="contentwrapper">
    <!--Content wrapper-->
    <div class="heading">
        <!--  .heading-->
        <h3 style="color:#353535">
            <i class="fa fa-archive"></i>Les clients
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
                        @Html.HiddenFor(Function(model) model.Solde)
                        @Html.HiddenFor(Function(model) model.SoldeDisponible)
                        @Html.HiddenFor(Function(model) model.Pourcentage)
                        @Html.HiddenFor(Function(model) model.CodeSecret)
                        @Html.HiddenFor(Function(model) model.UserId)
                        @*@Html.HiddenFor(Function(model) model.AgenceId)*@
                        @<div class="box box-warning">
                             <div class="box-header with-border">
                                 <div class="row">
                                     <div class="col-md-4">
                                         <div class="form-group">
                                             <label>Numero de Compte</label>
                                             <label style="color: #fdcd23"> @Html.ValidationMessageFor(Function(model) model.NumeroCompte) </label>
                                             @Html.EditorFor(Function(model) model.NumeroCompte)
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
                                             <label>Quartier</label>
                                             <label style="color: #fdcd23"> @Html.ValidationMessageFor(Function(model) model.Quartier) </label>
                                             @Html.EditorFor(Function(model) model.Quartier)
                                         </div>
                                     </div>
                                 </div>

                                 <div class="row">
                                     <div class="col-md-4">
                                         <div class="form-group">
                                             <label>Nom</label>
                                             <label style="color: #fdcd23"> @Html.ValidationMessageFor(Function(model) model.Nom) </label>
                                             @Html.EditorFor(Function(model) model.Nom)
                                         </div>
                                     </div>
                                     <div class="col-md-4">
                                         <div class="form-group">
                                             <label>Prénom </label>
                                             <label style="color: #fdcd23"> @Html.ValidationMessageFor(Function(model) model.Prenom) </label>
                                             @Html.EditorFor(Function(model) model.Prenom)
                                         </div>
                                     </div>
                                     <div class="col-md-4">
                                         <div class="form-group">
                                             <label>Genre </label>
                                             <label style="color: #fdcd23"> @Html.ValidationMessageFor(Function(model) model.Sexe) </label>
                                             <select id="Sexe" name="Sexe" class="fancy-select1 form-control">
                                                 @If (Model.Sexe = "M") Then
                                                     @<option data-icon="fa fa-thumbs-up" value="M"> Masculin </option>
                                                     @<option data-icon="fa fa-thumbs-down" value="F"> Feminin </option>

                                                 Else
                                                     @<option data-icon="fa fa-thumbs-down" value="F"> Feminin </option>
                                                     @<option data-icon="fa fa-thumbs-up" value="M"> Masculin </option>
                                                 End If
                                             </select>
                                         </div>
                                     </div>
                                 </div>

                                 <div class="row">
                                     <div class="col-md-4">
                                         <div class="form-group">
                                             <label>Téléphone</label>
                                             <label style="color: #fdcd23"> @Html.ValidationMessageFor(Function(model) model.Telephone) </label>
                                             @Html.EditorFor(Function(model) model.Telephone)
                                         </div>
                                     </div>
                                     <div class="col-md-4">
                                         <div class="form-group">
                                             <label>Téléphone 2</label>
                                             <label style="color: #fdcd23"> @Html.ValidationMessageFor(Function(model) model.Telephone2) </label>
                                             @Html.EditorFor(Function(model) model.Telephone2)
                                         </div>
                                     </div>
                                     <div class="col-md-4">
                                         <div class="form-group">
                                             <label>CNI</label>
                                             <label style="color: #fdcd23"> @Html.ValidationMessageFor(Function(model) model.CNI) </label>
                                             @Html.EditorFor(Function(model) model.CNI)
                                         </div>
                                     </div>
                                     <div class="col-md-4">
                                         <div class="form-group">
                                             <label>Agence du client</label>
                                             <label style="color: #fdcd23"> @Html.ValidationMessageFor(Function(model) model.AgenceId) </label>
                                             @Html.DropDownListFor(Function(model) model.AgenceId,
                                                 New SelectList(Model.IDsAgence, "Value", "Text"), New With {.class = "form-control select2"})
                                         </div>
                                     </div>
                                     <div class="col-md-4">
                                         <div class="form-group">
                                             <label>Profession</label>
                                             <label style="color: #fdcd23"> @Html.ValidationMessageFor(Function(model) model.SecteurActiviteId) </label>
                                             @Html.DropDownListFor(Function(model) model.SecteurActiviteId,
 New SelectList(Model.LesSecteursActivite, "Value", "Text"), "Selectionnez une profession", New With {.class = "form-control select2"})
                                         </div>
                                     </div>
                                     <div class="col-md-4">
                                         <div class="form-group">
                                             <label>Portefeuille</label>
                                             <label style="color: #fdcd23"> @Html.ValidationMessageFor(Function(model) model.PorteFeuilleId) </label>
                                             @Html.DropDownListFor(Function(model) model.PorteFeuilleId,
 New SelectList(Model.LesPorteFeuilles, "Value", "Text"), "Selectionnez un portefeuille", New With {.class = "form-control select2"})
                                         </div>
                                     </div>
                                 </div>

                                 <div class="row">
                                     <div class="col-md-4">
                                         <div class="form-group">
                                             @Html.LabelFor(Function(mode) Model.MessageAlerte)
                                             @*<label>Téléphone</label>*@
                                             <label style="color: #fdcd23"> @Html.ValidationMessageFor(Function(model) model.MessageAlerte) </label>
                                             @Html.TextAreaFor(Function(model) model.MessageAlerte, New With {.class = "form-control", .style = "height: 140px"})
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

@Section Scripts
    @Scripts.Render("~/bundles/jqueryval")
End Section