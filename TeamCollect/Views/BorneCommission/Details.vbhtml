@ModelType TeamCollect.BorneCommission
@Code
    ViewData("Title") = "Details"
End Code

<h2>Details</h2>

<div>
    <h4>BorneCommission</h4>
    <hr />
    <dl class="dl-horizontal">
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

    </dl>
</div>
<p>
    @Html.ActionLink("Edit", "Edit", New With { .id = Model.Id }) |
    @Html.ActionLink("Back to List", "Index")
</p>
