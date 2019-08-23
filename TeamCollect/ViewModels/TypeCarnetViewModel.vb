Imports System.ComponentModel.DataAnnotations

Public Class TypeCarnetViewModel

    Public Property Id As Long

    <Required(ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="champ_Manquant")>
    Public Property Libelle As String

    <Required(ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="champ_Manquant")>
    Public Property Prix As Double? = 0

    Public Property Etat As Boolean = False
    Public Property DateCreation As DateTime = Now



    Public Sub New()
    End Sub

    Public Sub New(entity As TypeCarnet
                   )
        With Me
            .Id = entity.Id
            .Libelle = entity.Libelle
            .Prix = entity.Prix
            .Etat = entity.Etat
            .DateCreation = entity.DateCreation
        End With
    End Sub

    Public Function getEntity() As TypeCarnet

        Dim entity As New TypeCarnet

        With entity
            .Id = Me.Id
            .Libelle = Me.Libelle
            .Prix = Me.Prix
            .Etat = Me.Etat
            .DateCreation = Me.DateCreation
        End With

        Return entity
    End Function
End Class
