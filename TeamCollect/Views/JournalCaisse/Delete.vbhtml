@ModelType JournalCaisse
@Code
    ViewData("Title") = "Delete"
End Code

<h2>Delete</h2>

<h3>Are you sure you want to delete this?</h3>
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
    @Using (Html.BeginForm())
        @Html.AntiForgeryToken()

        @<div class="form-actions no-color">
            <input type="submit" value="Delete" class="btn btn-default" /> |
            @Html.ActionLink("Back to List", "Index")
        </div>
    End Using
</div>
