@ModelType String
@Imports ICDPD.My.Resources

@Code

        Dim data As String = ""
        Select Case Model
            Case "MASCULIN"
                data = "MASCULIN"
            Case "FEMININ"
                data = "FEMININ"
    End Select

        @data

End Code