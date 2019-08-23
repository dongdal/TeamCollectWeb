Imports System.ComponentModel.DataAnnotations
Imports TeamCollect.My.Resources

Public Class InfoFraisViewModel

    Public Property Id As Long
    Public Property GrilleId As Long
    Public Property LibelleDelaGrille As String
    Public Overridable Property Grille As Grille

    <Required(ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="champ_Manquant")> _
    <RegularExpression("^(\d+(((\,))\d+)?)$", ErrorMessageResourceName:="decimalType_error", ErrorMessageResourceType:=GetType(Resource))>
    Public Property BornInf As String

    <Required(ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="champ_Manquant")> _
   <RegularExpression("^(\d+(((\,))\d+)?)$", ErrorMessageResourceName:="decimalType_error", ErrorMessageResourceType:=GetType(Resource))>
    Public Property BornSup As String

    <Required(ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="champ_Manquant")> _
   <RegularExpression("^(\d+(((\,))\d+)?)$", ErrorMessageResourceName:="decimalType_error", ErrorMessageResourceType:=GetType(Resource))>
    Public Property Frais As String = 0

    <Required(ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="champ_Manquant")> _
   <RegularExpression("^(\d+(((\,))\d+)?)$", ErrorMessageResourceName:="decimalType_error", ErrorMessageResourceType:=GetType(Resource))>
    Public Property Taux As String = 0

    Public Sub New()
    End Sub

    Public Sub New(entity As InfoFrais)
        With Me
            .Id = entity.Id
            .GrilleId = entity.GrilleId
            .BornInf = entity.BornInf
            .BornSup = entity.BornSup
            .Frais = entity.Frais
            .Taux = entity.Taux
        End With
    End Sub

    Public Function getEntity() As InfoFrais
        Dim entity As New InfoFrais

        With entity
            .Id = Me.Id
            .GrilleId = Me.GrilleId
            .BornInf = Me.BornInf
            .BornSup = Me.BornSup
            .Frais = Me.Frais
            .Taux = Me.Taux
        End With

        Return entity
    End Function


End Class
