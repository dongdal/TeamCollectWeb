@ModelType EnumCurrentState
@Imports iMeeting.My.Resources

@Code
    Dim data As New List(Of SelectListItem)
    
    data.Add(New SelectListItem() With {.Value = EnumCurrentState.Initial.ToString, .Text = "Initial"})
    data.Add(New SelectListItem() With {.Value = EnumCurrentState.EnCours.ToString, .Text = "En Cours"})
    data.Add(New SelectListItem() With {.Value = EnumCurrentState.EnRetard.ToString, .Text = "En Retard"})
    data.Add(New SelectListItem() With {.Value = EnumCurrentState.Terminee.ToString, .Text = "Terminé"})
    data.Add(New SelectListItem() With {.Value = EnumCurrentState.Canceled.ToString, .Text = "Annulé"})
    data.Add(New SelectListItem() With {.Value = EnumCurrentState.CanceledPending.ToString, .Text = "Annulation en cours"})
    data.Add(New SelectListItem() With {.Value = EnumCurrentState.Reported.ToString, .Text = "Reporté"})
    data.Add(New SelectListItem() With {.Value = EnumCurrentState.ReportedPending.ToString, .Text = "Report en cours"})
End Code

@Html.DropDownList("", New SelectList(data, "Value", "Text", Model), New With {.class = "form-control"})
