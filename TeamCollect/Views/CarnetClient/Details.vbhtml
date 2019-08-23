@ModelType TeamCollect.CarnetClient
@Code
    ViewData("Title") = "Details"
End Code

<h2>Details</h2>

<div>
    <h4>CarnetClient</h4>
    <hr />
    <dl class="dl-horizontal">
        <dt>
            @Html.DisplayNameFor(Function(model) model.ClientId)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.ClientId)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.TypeCarnetId)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.TypeCarnetId)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.Etat)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.Etat)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.DateAffectation)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.DateAffectation)
        </dd>

    </dl>
</div>
<p>
    @Html.ActionLink("Edit", "Edit", New With { .id = Model.Id }) |
    @Html.ActionLink("Back to List", "Index")
</p>
