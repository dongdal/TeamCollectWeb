@ModelType TeamCollect.Retrait
@Code
    ViewData("Title") = "Details"
End Code

<h2>Details</h2>

<div>
    <h4>Retrait</h4>
    <hr />
    <dl class="dl-horizontal">
        <dt>
            @Html.DisplayNameFor(Function(model) model.CollecteurId)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.CollecteurId)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.Montant)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.Montant)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.SoldeApreOperation)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.SoldeApreOperation)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.DateRetrait)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.DateRetrait)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.DateCloture)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.DateCloture)
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
