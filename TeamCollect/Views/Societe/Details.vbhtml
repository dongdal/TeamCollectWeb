@ModelType Societe
@Code
    ViewData("Title") = "Details"
End Code

<h2>Details</h2>

<div>
    <h4>Societe</h4>
    <hr />
    <dl class="dl-horizontal">
        <dt>
            @Html.DisplayNameFor(Function(model) model.Libelle)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.Libelle)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.BP)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.BP)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.Telephone)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.Telephone)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.Email)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.Email)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.Adresse)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.Adresse)
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
