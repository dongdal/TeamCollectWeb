@ModelType string
@Imports iMeeting.My.Resources

@Code
    Dim data As String = ""
    Select Case Model
        Case "N"
            data = Resource.posi_nord
        Case "NO"
            data = Resource.posi_no
        Case "NE"
            data = Resource.posi_ne
        Case "C"
            data = Resource.posi_centre
        Case "S"
            data = Resource.posi_sud
        Case "SO"
            data = Resource.posi_so
        Case "SE"
            data = Resource.posi_se
        Case "E"
            data = Resource.posi_est
        Case "O"
            data = Resource.posi_ouest
    End Select
    
    ' Afficher la donnée
    @data
End Code
