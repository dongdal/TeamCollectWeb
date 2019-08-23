@ModelType EnumReservation
@Imports iMeeting.My.Resources

@Code
    Dim data As New List(Of SelectListItem)
    
    data.Add(New SelectListItem() With {.Value = EnumReservation.Initial.ToString, .Text = "Non validé"})
    data.Add(New SelectListItem() With {.Value = EnumReservation.Confirmed.ToString, .Text = "Validé"})
End Code

@Html.DropDownList("", New SelectList(data, "Value", "Text", Model), New With {.class = "form-control"})
