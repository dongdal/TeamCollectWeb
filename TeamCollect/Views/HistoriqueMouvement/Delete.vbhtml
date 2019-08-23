@ModelType HistoriqueMouvement
@Code
    ViewData("Title") = "Delete"
End Code

<h2>Delete</h2>

<h3>Are you sure you want to delete this?</h3>
<div>
    <h4>HistoriqueMouvement</h4>
    <hr />
    <dl class="dl-horizontal">
        <dt>
            @Html.DisplayNameFor(Function(model) model.Client.CodeSecret)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.Client.CodeSecret)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.Collecteur.CodeSecret)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.Collecteur.CodeSecret)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.JournalCaisse.UserId)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.JournalCaisse.UserId)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.Traitement.Fichier)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.Traitement.Fichier)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.User.UserName)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.User.UserName)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.Latitude)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.Latitude)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.Longitude)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.Longitude)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.Montant)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.Montant)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.DateOperation)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.DateOperation)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.Pourcentage)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.Pourcentage)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.EstTraiter)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.EstTraiter)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.DateTraitement)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.DateTraitement)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.Etat)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.Etat)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.DateCreation)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.DateCreation)
        </dd>

    </dl>
    @Using (Html.BeginForm())
        @Html.AntiForgeryToken()

        @<div class="form-actions no-color">
            <input type="submit" value="Delete" class="btn btn-default" /> |
            @Html.ActionLink("Back to List", "Index")
        </div>
    End Using
</div>
