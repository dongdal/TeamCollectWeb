@ModelType string
@Imports iMeeting.My.Resources

@Code
    Dim data As New List(Of SelectListItem)
    
    data.Add(New SelectListItem() With {.Value = "N", .Text = Resource.posi_nord})
    data.Add(New SelectListItem() With {.Value = "NO", .Text = Resource.posi_no})
    data.Add(New SelectListItem() With {.Value = "NE", .Text = Resource.posi_ne})
    data.Add(New SelectListItem() With {.Value = "C", .Text = Resource.posi_centre})
    data.Add(New SelectListItem() With {.Value = "S", .Text = Resource.posi_sud})
    data.Add(New SelectListItem() With {.Value = "SO", .Text = Resource.posi_so})
    data.Add(New SelectListItem() With {.Value = "SE", .Text = Resource.posi_se})
    data.Add(New SelectListItem() With {.Value = "E", .Text = Resource.posi_est})
    data.Add(New SelectListItem() With {.Value = "O", .Text = Resource.posi_ouest})
End Code

@Html.DropDownList("", New SelectList(data, "Value", "Text", Model), Resource.posi_select, New With {.class = "form-control"})
