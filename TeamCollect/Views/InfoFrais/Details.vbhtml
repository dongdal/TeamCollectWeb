@ModelType InfoFrais
@Code
    ViewData("Title") = "Details"
End Code

<h2>Details</h2>

<div>
    <h4>InfoFrais</h4>
    <hr />
    <dl class="dl-horizontal">
        <dt>
            @Html.DisplayNameFor(Function(model) model.Grille.Libelle)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.Grille.Libelle)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.BornInf)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.BornInf)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.BornSup)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.BornSup)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.Frais)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.Frais)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.Taux)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.Taux)
        </dd>

    </dl>
</div>
<p>
    @Html.ActionLink("Edit", "Edit", New With { .id = Model.Id }) |
    @Html.ActionLink("Back to List", "Index")
</p>
