@ModelType [Enum]

@Code
    Dim enumtype = Model.GetType()
End Code

@For Each value In [Enum].GetValues(enumtype)
    Dim field = enumtype.GetField(value.ToString())
    Dim display = CType(field.GetCustomAttributes(GetType(DisplayAttribute), False), DisplayAttribute()).FirstOrDefault()
    Dim label As String
    If display IsNot Nothing Then
        label = display.GetName
    Else
        label = value.ToString
    End If
    

    Dim id = TagBuilder.CreateSanitizedId(String.Format(
        "{0}_{1}_{2}", ViewData.TemplateInfo.HtmlFieldPrefix, Model.GetType(), value))
@<span>
    @Html.RadioButton(String.Empty, value, value.Equals(Model), New With {.id = id})
    @Html.Label(label, New With {.for = id})
</span>
Next
