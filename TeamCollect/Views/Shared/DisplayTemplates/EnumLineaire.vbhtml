@ModelType [Enum]

@Code
    Dim enumtype = Model.GetType()
    Dim label As String
    Dim field = enumtype.GetField(Model.ToString())
    Dim display = CType(field.GetCustomAttributes(GetType(DisplayAttribute), False), DisplayAttribute()).FirstOrDefault()
    If display IsNot Nothing Then
        label = display.GetName
    Else
        label = Model.ToString
    End If
    @label 
End Code
