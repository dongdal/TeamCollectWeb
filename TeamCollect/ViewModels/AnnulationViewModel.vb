Imports System.ComponentModel.DataAnnotations
Imports TeamCollect.My.Resources

Public Class AnnulationViewModel

    Public Property Id As Long



    <Required(ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="champ_Manquant")>
    Public Property Motif As String

    Public Property DateDebut As String
    Public Property DateFin As String
    Public Property CollecteurId As Long?


    Public Sub New()
    End Sub

End Class
