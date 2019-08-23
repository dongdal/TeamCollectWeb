Imports System.ComponentModel.DataAnnotations
Imports TeamCollect.My.Resources

Public Class JournalCaisseViewModel

    Public Property Id As Long

    <Required(ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="champ_Manquant")> _
    Public Property CollecteurId As Long
    Public Overridable Property IDsCollecteur As List(Of SelectListItem)
    Public Overridable Property Collecteur As Collecteur


    <RegularExpression("^(\d+(((\,))\d+)?)$", ErrorMessageResourceName:="decimalType_error", ErrorMessageResourceType:=GetType(Resource))>
    Public Property MontantTheorique As String

    <RegularExpression("^(\d+(((\,))\d+)?)$", ErrorMessageResourceName:="decimalType_error", ErrorMessageResourceType:=GetType(Resource))>
    Public Property MontantReel As String


    <RegularExpression("^(\d+(((\,))\d+)?)$", ErrorMessageResourceName:="decimalType_error", ErrorMessageResourceType:=GetType(Resource))>
    Public Property FondCaisse As String

    Public Property DateOuverture As Date
    Public Property DateCloture As Nullable(Of Date)

    Public Property UserId As String
    Public Overridable Property User As ApplicationUser

    Public Property DateCreation As Nullable(Of Date)
    Public Property Etat As Nullable(Of Integer)

    Public Property PlafondEnCours As String

    <Required(ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="champ_Manquant")> _
    <RegularExpression("^(\d+(((\,))\d+)?)$", ErrorMessageResourceName:="decimalType_error", ErrorMessageResourceType:=GetType(Resource))>
    Public Property PlafondDebat As String

    Public Sub New()
    End Sub

    Public Sub New(entity As JournalCaisse)
        With Me
            .Id = entity.Id
            .CollecteurId = entity.CollecteurId
            .MontantTheorique = entity.MontantTheorique
            .MontantReel = entity.MontantReel
            .FondCaisse = entity.FondCaisse
            .DateOuverture = entity.DateOuverture
            .DateCloture = entity.DateCloture
            .UserId = entity.UserId
            .DateCreation = entity.DateCreation
            .Etat = entity.Etat
            .PlafondDebat = entity.PlafondDeDebat
            .PlafondEnCours = entity.PlafondEnCours

        End With
    End Sub

    Public Function getEntity() As JournalCaisse
        Dim entity As New JournalCaisse

        With entity
            .Id = Me.Id
            .CollecteurId = Me.CollecteurId
            .MontantTheorique = Me.MontantTheorique
            .MontantReel = Me.MontantReel
            .FondCaisse = Me.FondCaisse
            .DateOuverture = Me.DateOuverture
            .DateCloture = Me.DateCloture
            .UserId = Me.UserId
            .DateCreation = Me.DateCreation
            .Etat = Me.Etat
            .PlafondDeDebat = Me.PlafondDebat
            .PlafondEnCours = Me.PlafondEnCours
        End With

        Return entity
    End Function


End Class
