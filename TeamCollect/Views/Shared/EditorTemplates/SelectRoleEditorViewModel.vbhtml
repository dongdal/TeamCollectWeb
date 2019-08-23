@ModelType SelectRoleEditorViewModel
@Html.HiddenFor(Function(Model) Model.RoleName)
<tr>
    <td style="text-align:center">
        @Html.CheckBoxFor(Function(Model) Model.Selected)
    </td>
    <td>
        @Html.DisplayFor(Function(Model) Model.RoleName)
    </td>
</tr>