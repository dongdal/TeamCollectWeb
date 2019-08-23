@ModelType JournalCaisse
@Code
    ViewData("Title") = "Details"
End Code

<h2>Details</h2>

<div>
    <h4>JournalCaisse</h4>
    <hr />
    <dl class="dl-horizontal">
        <dt>
            @Html.DisplayNameFor(Function(model) model.Collecteur.CodeSecret)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.Collecteur.CodeSecret)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.User.UserName)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.User.UserName)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.MontantTheorique)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.MontantTheorique)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.MontantReel)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.MontantReel)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.Date)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.Date)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.FondCaisse)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.FondCaisse)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.DateOuverture)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.DateOuverture)
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
