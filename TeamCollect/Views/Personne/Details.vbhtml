@ModelType Personne
@Code
    ViewData("Title") = "Details"
End Code

<h2>Details</h2>

<div>
    <h4>Personne</h4>
    <hr />
    <dl class="dl-horizontal">
        <dt>
            @Html.DisplayNameFor(Function(model) model.CodeSecret)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.CodeSecret)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.Nom)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.Nom)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.Prenom)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.Prenom)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.Sexe)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.Sexe)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.CNI)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.CNI)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.Telephone)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.Telephone)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.Adresse)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.Adresse)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.Quartier)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.Quartier)
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

        <dt>
            @Html.DisplayNameFor(Function(model) model.UserId)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.UserId)
        </dd>

    </dl>
</div>
<p>
    @Html.ActionLink("Edit", "Edit", New With { .id = Model.Id }) |
    @Html.ActionLink("Back to List", "Index")
</p>
