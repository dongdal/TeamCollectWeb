@ModelType String
@Imports ICDPD.My.Resources

@Code
    Dim data As New List(Of SelectListItem)
    
    data.Add(New SelectListItem() With {.Value = "MASCULIN", .Text = "MASCULIN"})
    data.Add(New SelectListItem() With {.Value = "FEMININ", .Text = "FEMININ"})
End Code

@Html.DropDownList("", New SelectList(data, "Value", "Text", Model), New With {.class = "form-control"})
