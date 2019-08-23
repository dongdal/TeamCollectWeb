@ModelType InfoCompensation
@Code
    ViewData("Title") = "Create"
End Code

<h2>Create</h2>

@Using (Html.BeginForm()) 
    @Html.AntiForgeryToken()
    
    @<div class="form-horizontal">
        <h4>InfoCompensation</h4>
        <hr />
        @Html.ValidationSummary(true)
        <div class="form-group">
            @Html.LabelFor(Function(model) model.JournalCaisseId, "JournalCaisseId", New With { .class = "control-label col-md-2" })
            <div class="col-md-10">
                @Html.DropDownList("JournalCaisseId", String.Empty)
                @Html.ValidationMessageFor(Function(model) model.JournalCaisseId)
            </div>
        </div>

        <div class="form-group">
            @Html.LabelFor(Function(model) model.Libelle, New With { .class = "control-label col-md-2" })
            <div class="col-md-10">
                @Html.EditorFor(Function(model) model.Libelle)
                @Html.ValidationMessageFor(Function(model) model.Libelle)
            </div>
        </div>

        <div class="form-group">
            @Html.LabelFor(Function(model) model.MontantVerse, New With { .class = "control-label col-md-2" })
            <div class="col-md-10">
                @Html.EditorFor(Function(model) model.MontantVerse)
                @Html.ValidationMessageFor(Function(model) model.MontantVerse)
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
                <input type="submit" value="Create" class="btn btn-default" />
            </div>
        </div>
    </div>
End Using

<div>
    @Html.ActionLink("Back to List", "Index")
</div>
