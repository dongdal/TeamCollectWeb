@ModelType DateTime
@Imports iMeeting.My.Resources

@Code
    Dim data As New List(Of SelectListItem)
    
    Dim heure_deb As Integer = ConfigurationManager.AppSettings("calMinTime")
    Dim heure_fin As Integer = ConfigurationManager.AppSettings("calMaxTime")
    For i = heure_deb To heure_fin
        data.Add(New SelectListItem() With {.Value = String.Format("{0:00}:00", i), .Text = String.Format("{0:00}:00", i)})
        data.Add(New SelectListItem() With {.Value = String.Format("{0:00}:30", i), .Text = String.Format("{0:00}:30", i)})
    Next
End Code

@Html.DropDownList("", New SelectList(data, "Value", "Text", String.Format("{0:00}:{1:00}", Model.Hour, Model.Minute)), New With {.class = "form-control"})
