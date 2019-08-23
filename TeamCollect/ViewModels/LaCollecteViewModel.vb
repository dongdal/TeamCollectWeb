Imports System.ComponentModel.DataAnnotations
Imports TeamCollect.My.Resources

Public Class LaCollecteViewModel

    <RegularExpression("^(\d+(((\,))\d+)?)$", ErrorMessageResourceName:="decimalType_error", ErrorMessageResourceType:=GetType(Resource))>
    <Required(ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="champ_Manquant")> _
    Public Property Montant As String

    <Required(ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="champ_Manquant")> _
    Public Property LeNumCompteClient As String


    Public Sub New()
    End Sub

    Public Function getEntity() As HistoriqueMouvement
        Dim entity As New HistoriqueMouvement
        With entity
            .Montant = Me.Montant
            'le plus important c'est avoir un type string a qui mapper les données du viewmodel c'est n'est pas une erreur
            .Latitude = Me.LeNumCompteClient
        End With

        Return entity
    End Function


End Class
