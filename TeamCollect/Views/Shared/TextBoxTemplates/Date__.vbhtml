@ModelType DateTime?
@Code

    @<div class="input-group">
        @If Model.HasValue Then
            @Html.TextBox("", String.Format("{0:d}", Model.Value.ToShortDateString()),
            New With {.class = "form-control datefield", .type = "Date"})
        Else
            @Html.TextBox("", "", New With {.class = "form-control datefield", .type = "Date"})
        End If

    </div>
End code
