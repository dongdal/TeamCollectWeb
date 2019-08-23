Imports System.ComponentModel.DataAnnotations
Imports TeamCollect.My.Resources

Public Class SocieteViewModel

    Public Property Id As Long

    <Required(ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="champ_Manquant")> _
    Public Property Libelle As String

    Public Property BP As String

    <Required(ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="champ_Manquant")> _
    Public Property Email As String

    Public Property Telephone As String
    Public Property Adresse As String
    Public Property DateCreation As Nullable(Of Date)
    Public Property Etat As Nullable(Of Integer)

    <Required(ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="champ_Manquant")>
    <RegularExpression("^[0-9]+$|^[0-9]+[0-9]*$", ErrorMessageResourceName:="NumericType_error", ErrorMessageResourceType:=GetType(Resource))>
    Public Property PlafondDeCollecte As String

    <Required(ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="champ_Manquant")>
    <RegularExpression("^[0-9]+$|^[0-9]+[0-9]*$", ErrorMessageResourceName:="NumericType_error", ErrorMessageResourceType:=GetType(Resource))>
    Public Property MinCollecte As String = "0"

    <Required(ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="champ_Manquant")>
    <RegularExpression("^[0-9]+$|^[0-9]+[0-9]*$", ErrorMessageResourceName:="NumericType_error", ErrorMessageResourceType:=GetType(Resource))>
    Public Property MAxCollecte As String = "0"





    Public Sub New()
    End Sub

    Public Sub New(entity As Societe)
        With Me
            .Id = entity.Id
            .Libelle = entity.Libelle
            .BP = entity.BP
            .Telephone = entity.Telephone
            .Email = entity.Email
            .Adresse = entity.Adresse
            .DateCreation = entity.DateCreation
            .Etat = entity.Etat
            .PlafondDeCollecte = entity.PlafondDeCollecte
            .MinCollecte = entity.MinCollecte
            .MAxCollecte = entity.MAxCollecte

        End With
    End Sub

    Public Function getEntity() As Societe
        Dim entity As New Societe

        With entity
            .Id = Me.Id
            .Libelle = Me.Libelle
            .BP = Me.BP
            .Telephone = Me.Telephone
            .Email = Me.Email
            .Adresse = Me.Adresse
            .DateCreation = Me.DateCreation
            .Etat = Me.Etat
            .PlafondDeCollecte = Me.PlafondDeCollecte
            .MAxCollecte = Me.MAxCollecte
            .MinCollecte = Me.MinCollecte

        End With

        Return entity
    End Function


End Class
