Imports System.ComponentModel.DataAnnotations
Imports TeamCollect.My.Resources

Public Class TransfertViewModel

    'Public Property Id As Long

    <Required(ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="champ_Manquant")>
    Public Property PorteFeuilleId As Long
    Public Overridable Property IDsPorteFeuille As List(Of SelectListItem)
    Public Overridable Property PorteFeuille As PorteFeuille

    'Public Property Etat As Boolean = True
    ' Public Property DateCreation As DateTime = Now

    Public Property ClientId As Long
    Public Property PorteFeuilleSourceId As Long
    Public Overridable Property Client As Client





    Public Sub New()
    End Sub


End Class
