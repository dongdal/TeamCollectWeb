@ModelType TeamCollect.Retrait
@Code
    ViewData("Title") = "Delete"
End Code

<h2>Delete</h2>

<h3>Are you sure you want to delete this?</h3>
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
    @Using (Html.BeginForm())
        @Html.AntiForgeryToken()

        @<div class="form-actions no-color">
            <input type="submit" value="Delete" class="btn btn-default" /> |
            @Html.ActionLink("Back to List", "Index")
        </div>
    End Using
</div>
