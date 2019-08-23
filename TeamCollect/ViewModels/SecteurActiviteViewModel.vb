Imports System.ComponentModel.DataAnnotations

Public Class SecteurActiviteViewModel

    Public Property Id As Long

    <Required(ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="champ_Manquant")>
    Public Property Libelle As String

    Public Property Etat As Boolean = False
    Public Property DateCreation As DateTime = Now

    Public Sub New()
    End Sub

    Public Sub New(entity As SecteurActivite)
        With Me
            .Id = entity.Id
            .Libelle = entity.Libelle
            .DateCreation = entity.DateCreation
            .Etat = entity.Etat

        End With
    End Sub

    Public Function getEntity() As SecteurActivite
        Dim entity As New SecteurActivite

        With entity
            .Id = Me.Id
            .Libelle = Me.Libelle
            .DateCreation = Me.DateCreation
            .Etat = Me.Etat

        End With

        Return entity
    End Function
End Class
