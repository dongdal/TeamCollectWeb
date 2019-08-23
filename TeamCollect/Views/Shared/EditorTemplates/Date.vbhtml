@ModelType Date?
@*Using Date Template*@
@Code
    
    Dim id = TagBuilder.CreateSanitizedId(String.Format(
       "{0}_{1}_{2}", ViewData.TemplateInfo.HtmlFieldPrefix, "dt", 1))
    
@<div class="input-group date">
    @If Model.HasValue Then
        @Html.TextBox("", String.Format("{0:d}", Model.Value.ToShortDateString()),
            New With {.class = "form-control datefield", .type = "text", .id = id})
    Else
        @Html.TextBox("", "", New With {.class = "form-control datefield", .type = "date", .id = id})
    End If

    <label for="@id" class="input-group-addon"><i class="fa fa-calendar"></i></label>
</div>
End code