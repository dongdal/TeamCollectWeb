@ModelType TeamCollect.SecteurActivite
@Code
    ViewData("Title") = "Details"
End Code

<h2>Details</h2>

<div>
    <h4>SecteurActivite</h4>
    <hr />
    <dl class="dl-horizontal">
        <dt>
            @Html.DisplayNameFor(Function(model) model.Libelle)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.Libelle)
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
</div>
<p>
    @Html.ActionLink("Edit", "Edit", New With { .id = Model.Id }) |
    @Html.ActionLink("Back to List", "Index")
</p>
