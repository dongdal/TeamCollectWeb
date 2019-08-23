Imports System.ComponentModel.DataAnnotations

Public Class BorneCommissionViewModel
    Public Property Id As Long

    <Required(ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="champ_Manquant")>
    Public Property BornInf As Decimal

    <Required(ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="champ_Manquant")>
    Public Property BornSup As Decimal


    Public Sub New()
    End Sub

    Public Sub New(entity As BorneCommission
                   )
        With Me
            .Id = entity.Id
            .BornInf = entity.BornInf
            .BornSup = entity.BornSup

        End With
    End Sub

    Public Function getEntity() As BorneCommission

        Dim entity As New BorneCommission

        With entity
            .Id = Me.Id
            .BornInf = Me.BornInf
            .BornSup = Me.BornSup

        End With

        Return entity
    End Function
End Class
