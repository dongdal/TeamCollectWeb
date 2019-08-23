Imports System.ComponentModel.DataAnnotations
Imports TeamCollect.My.Resources

Public Class InfoCompensationViewModel

    Public Property Id As Long
    Public Property JournalCaisseId As Long

    <Required(ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="champ_Manquant")> _
    Public Property Libelle As String

    <Required(ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="champ_Manquant")> _
    <RegularExpression("^(\d+(((\,))\d+)?)$", ErrorMessageResourceName:="decimalType_error", ErrorMessageResourceType:=GetType(Resource))>
    Public Property MontantVerse As Decimal

    Public Property Etat As Boolean = False
    Public Property DateCreation As DateTime = Now


    Public Sub New()
    End Sub

    Public Sub New(entity As InfoCompensation)
        With Me
            .Id = entity.Id
            .JournalCaisseId = entity.JournalCaisseId
            .Libelle = entity.Libelle
            .MontantVerse = entity.MontantVerse
        End With
    End Sub

    Public Function getEntity() As InfoCompensation
        Dim entity As New InfoCompensation

        With entity
            .Id = Me.Id
            .JournalCaisseId = Me.JournalCaisseId
            .Libelle = Me.Libelle
            .MontantVerse = Me.MontantVerse
        End With

        Return entity
    End Function


End Class
