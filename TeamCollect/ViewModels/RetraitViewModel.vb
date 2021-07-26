Imports System.ComponentModel.DataAnnotations

Public Class RetraitViewModel
    Public Property Id As Long
    '<Required(ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="champ_Manquant")>
    Public Property CollecteurId As Long
    Public Overridable Property IDsCollecteur As List(Of SelectListItem)
    Public Overridable Property Collecteur As Collecteur

    '<Required(ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="champ_Manquant")>
    Public Property ClientId As Long
    Public Overridable Property IDsClient As List(Of SelectListItem)
    Public Overridable Property Client As Client


    '<Required(ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="champ_Manquant")>
    Public Property HistoriqueMouvementId As Long?
    Public Overridable Property LesHistoriqueMouvements As List(Of SelectListItem)
    Public Overridable Property HistoriqueMouvement As HistoriqueMouvement

    '<Required(ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="champ_Manquant")>
    '<RegularExpression("^(\d+(((\,))\d+)?)$", ErrorMessageResourceName:="decimalType_error", ErrorMessageResourceType:=GetType(Resource))>
    'Public Property Montant As Decimal

    <Required(ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="champ_Manquant")>
    <RegularExpression("^(\d+(((\,))\d+)?)$", ErrorMessageResourceName:="decimalType_error", ErrorMessageResourceType:=GetType(Resource))>
    <Range(500, Decimal.MaxValue, ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="DecimalMaxValue")>
    Public Property Montant As Decimal



    Public Property SoldeApreOperation As Decimal
    Public Property DateRetrait As Date? = Now
    Public Property DateCloture As Date? = Now

    Public Property Etat As Boolean = True
    Public Property DateCreation As DateTime = Now


    Public Sub New()

    End Sub

    Public Sub New(retraitJSON As Controllers.RetraitJSON)
        With Me
            .Id = .Id
            .CollecteurId = .CollecteurId
            .ClientId = retraitJSON.ClientId
            Decimal.TryParse(retraitJSON.Montant, .Montant)
            .Etat = .Etat
            .SoldeApreOperation = .SoldeApreOperation
            .DateRetrait = .DateRetrait
            .DateCloture = .DateCloture
            .DateCreation = .DateCreation
        End With
    End Sub

    Public Sub New(entity As Retrait)
        With Me
            .Id = entity.Id
            .CollecteurId = entity.CollecteurId
            .ClientId = entity.ClientId
            .HistoriqueMouvementId = entity.HistoriqueMouvementId
            .Montant = entity.Montant
            .Etat = entity.Etat
            .SoldeApreOperation = entity.SoldeApreOperation
            .DateRetrait = entity.DateRetrait
            .DateCloture = entity.DateCloture
            .DateCreation = entity.DateCreation
        End With
    End Sub

    Public Function GetEntity() As Retrait
        Dim entity As New Retrait

        With entity
            .Id = Me.Id
            .CollecteurId = Me.CollecteurId
            .ClientId = Me.ClientId
            .HistoriqueMouvementId = Me.HistoriqueMouvementId
            .Montant = Me.Montant
            .Etat = Me.Etat
            .SoldeApreOperation = Me.SoldeApreOperation
            .DateRetrait = Me.DateRetrait
            .DateCloture = Me.DateCloture
            .DateCreation = Me.DateCreation
        End With

        Return entity
    End Function
End Class
