@ModelType HistoriqueMouvement
@Code
    ViewData("Title") = "Edit"
End Code

<h2>Edit</h2>

@Using (Html.BeginForm())
    @Html.AntiForgeryToken()
    
    @<div class="form-horizontal">
        <h4>HistoriqueMouvement</h4>
        <hr />
        @Html.ValidationSummary(true)
        @Html.HiddenFor(Function(model) model.Id)

        <div class="form-group">
            @Html.LabelFor(Function(model) model.JournalCaisseId, "JournalCaisseId", New With { .class = "control-label col-md-2" })
            <div class="col-md-10">
                @Html.DropDownList("JournalCaisseId", String.Empty)
                @Html.ValidationMessageFor(Function(model) model.JournalCaisseId)
            </div>
        </div>

        <div class="form-group">
            @Html.LabelFor(Function(model) model.CollecteurId, "CollecteurId", New With { .class = "control-label col-md-2" })
            <div class="col-md-10">
                @Html.DropDownList("CollecteurId", String.Empty)
                @Html.ValidationMessageFor(Function(model) model.CollecteurId)
            </div>
        </div>

        <div class="form-group">
            @Html.LabelFor(Function(model) model.ClientId, "ClientId", New With { .class = "control-label col-md-2" })
            <div class="col-md-10">
                @Html.DropDownList("ClientId", String.Empty)
                @Html.ValidationMessageFor(Function(model) model.ClientId)
            </div>
        </div>

        <div class="form-group">
            @Html.LabelFor(Function(model) model.TraitementId, "TraitementId", New With { .class = "control-label col-md-2" })
            <div class="col-md-10">
                @Html.DropDownList("TraitementId", String.Empty)
                @Html.ValidationMessageFor(Function(model) model.TraitementId)
            </div>
        </div>

        <div class="form-group">
            @Html.LabelFor(Function(model) model.Latitude, New With { .class = "control-label col-md-2" })
            <div class="col-md-10">
                @Html.EditorFor(Function(model) model.Latitude)
                @Html.ValidationMessageFor(Function(model) model.Latitude)
            </div>
        </div>

        <div class="form-group">
            @Html.LabelFor(Function(model) model.Longitude, New With { .class = "control-label col-md-2" })
            <div class="col-md-10">
                @Html.EditorFor(Function(model) model.Longitude)
                @Html.ValidationMessageFor(Function(model) model.Longitude)
            </div>
        </div>

        <div class="form-group">
            @Html.LabelFor(Function(model) model.Montant, New With { .class = "control-label col-md-2" })
            <div class="col-md-10">
                @Html.EditorFor(Function(model) model.Montant)
                @Html.ValidationMessageFor(Function(model) model.Montant)
            </div>
        </div>

        <div class="form-group">
            @Html.LabelFor(Function(model) model.DateOperation, New With { .class = "control-label col-md-2" })
            <div class="col-md-10">
                @Html.EditorFor(Function(model) model.DateOperation)
                @Html.ValidationMessageFor(Function(model) model.DateOperation)
            </div>
        </div>

        <div class="form-group">
            @Html.LabelFor(Function(model) model.Pourcentage, New With { .class = "control-label col-md-2" })
            <div class="col-md-10">
                @Html.EditorFor(Function(model) model.Pourcentage)
                @Html.ValidationMessageFor(Function(model) model.Pourcentage)
            </div>
        </div>

        <div class="form-group">
            @Html.LabelFor(Function(model) model.EstTraiter, New With { .class = "control-label col-md-2" })
            <div class="col-md-10">
                @Html.EditorFor(Function(model) model.EstTraiter)
                @Html.ValidationMessageFor(Function(model) model.EstTraiter)
            </div>
        </div>

        <div class="form-group">
            @Html.LabelFor(Function(model) model.DateTraitement, New With { .class = "control-label col-md-2" })
            <div class="col-md-10">
                @Html.EditorFor(Function(model) model.DateTraitement)
                @Html.ValidationMessageFor(Function(model) model.DateTraitement)
            </div>
        </div>

        <div class="form-group">
            @Html.LabelFor(Function(model) model.Etat, New With { .class = "control-label col-md-2" })
            <div class="col-md-10">
                @Html.EditorFor(Function(model) model.Etat)
                @Html.ValidationMessageFor(Function(model) model.Etat)
            </div>
        </div>

        <div class="form-group">
            @Html.LabelFor(Function(model) model.DateCreation, New With { .class = "control-label col-md-2" })
            <div class="col-md-10">
                @Html.EditorFor(Function(model) model.DateCreation)
                @Html.ValidationMessageFor(Function(model) model.DateCreation)
            </div>
        </div>

        <div class="form-group">
            @Html.LabelFor(Function(model) model.UserId, "UserId", New With { .class = "control-label col-md-2" })
            <div class="col-md-10">
                @Html.DropDownList("UserId", String.Empty)
                @Html.ValidationMessageFor(Function(model) model.UserId)
            </div>
        </div>

        <div class="form-group">
            <div class="col-md-offset-2 col-md-10">
                <input type="submit" value="Save" class="btn btn-default" />
            </div>
        </div>
    </div>
End Using

<div>
    @Html.ActionLink("Back to List", "Index")
</div>

@Section Scripts 
    @Scripts.Render("~/bundles/jqueryval")
End Section
