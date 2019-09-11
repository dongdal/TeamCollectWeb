Imports System.ComponentModel.DataAnnotations
Imports TeamCollect.My.Resources

Public Class CategorieRemunerationViewModel
    Public Property Id As Long

    <Required(ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="champ_Manquant")>
    <StringLength(250, ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="StringLength")>
    <Display(Name:="Libelle", ResourceType:=GetType(Resource))>
    Public Property Libelle As String

    <Required(ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="champ_Manquant")>
    <Display(Name:="SalaireDeBase", ResourceType:=GetType(Resource))>
    Public Property SalaireDeBase As String

    <Required(ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="champ_Manquant")>
    <RegularExpression("^(\d+(((\,))\d+)?)$", ErrorMessageResourceName:="decimalType_error", ErrorMessageResourceType:=GetType(Resource))>
    <Display(Name:="CommissionMinimale", ResourceType:=GetType(Resource))>
    Public Property CommissionMinimale As String

    <Required(ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="champ_Manquant")>
    <RegularExpression("^(\d+(((\,))\d+)?)$", ErrorMessageResourceName:="decimalType_error", ErrorMessageResourceType:=GetType(Resource))>
    <Display(Name:="PourcentageCommission", ResourceType:=GetType(Resource))>
    <Range(1, 100, ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="DecimalMaxValuePercent")>
    Public Property PourcentageCommission As String

    <Required(ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="champ_Manquant")>
    <Display(Name:="StatutExistant", ResourceType:=GetType(Resource))>
    Public Property StatutExistant As Boolean = True

    <Required(ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="champ_Manquant")>
    <Display(Name:="DateCreation", ResourceType:=GetType(Resource))>
    Public Property DateCreation As DateTime = Now

    <Display(Name:="DateCreation", ResourceType:=GetType(Resource))>
    Public Property UserId As String
    Public Overridable Property LesUtilisateurs As List(Of SelectListItem)
    Public Overridable Property User As ApplicationUser


    Public Sub New()
    End Sub

    Public Sub New(entity As CategorieRemuneration)
        With Me
            .Id = entity.Id
            .Libelle = entity.Libelle
            .SalaireDeBase = entity.SalaireDeBase
            .CommissionMinimale = entity.CommissionMinimale
            .PourcentageCommission = entity.PourcentageCommission
            .StatutExistant = entity.StatutExistant
            .DateCreation = entity.DateCreation
            .UserId = entity.UserId
            .User = entity.User
        End With
    End Sub

    Public Function GetEntity() As CategorieRemuneration
        Dim entity As New CategorieRemuneration

        With entity
            .Id = Id
            .Libelle = Libelle
            Decimal.TryParse(SalaireDeBase, .SalaireDeBase)
            Decimal.TryParse(CommissionMinimale, .CommissionMinimale)
            Double.TryParse(PourcentageCommission, .PourcentageCommission)
            .StatutExistant = StatutExistant
            .DateCreation = DateCreation
            .UserId = UserId
        End With

        Return entity
    End Function


End Class
