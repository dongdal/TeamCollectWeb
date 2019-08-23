@ModelType IEnumerable(Of InfoCompensation)
@Code
ViewData("Title") = "Index"
End Code

<h2>Index</h2>

<p>
    @Html.ActionLink("Create New", "Create")
</p>
<table class="table">
    <tr>
        <th>
            @Html.DisplayNameFor(Function(model) model.JournalCaisse.UserId)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.User.UserName)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.Libelle)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.MontantVerse)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.Etat)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.DateCreation)
        </th>
        <th></th>
    </tr>

@For Each item In Model
    @<tr>
        <td>
            @Html.DisplayFor(Function(modelItem) item.JournalCaisse.UserId)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) item.User.UserName)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) item.Libelle)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) item.MontantVerse)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) item.Etat)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) item.DateCreation)
        </td>
        <td>
            @Html.ActionLink("Edit", "Edit", New With {.id = item.Id }) |
            @Html.ActionLink("Details", "Details", New With {.id = item.Id }) |
            @Html.ActionLink("Delete", "Delete", New With {.id = item.Id })
        </td>
    </tr>
Next

</table>
