Imports System.ComponentModel.DataAnnotations
Imports TeamCollect.My.Resources

Public Class ImportClientViewModel

    'Public Property Id As Long

    <Required(ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="champ_Manquant")> _
    Public Property CollecteurId As Long
    Public Overridable Property IDsCollecteur As List(Of SelectListItem)
    Public Overridable Property Collecteur As Collecteur

    '<Required(ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="champ_Manquant")> _
    'Public Property Libelle As String

    'Public Property BP As String

    '<Required(ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="champ_Manquant")> _
    'Public Property Email As String

    'Public Property Telephone As String
    'Public Property Adresse As String
    'Public Property DateCreation As Nullable(Of Date)
    'Public Property Etat As Nullable(Of Integer)

    '<Required(ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="champ_Manquant")> _
    '<RegularExpression("^(\d+(((\,))\d+)?)$", ErrorMessageResourceName:="decimalType_error", ErrorMessageResourceType:=GetType(Resource))>
    'Public Property PlafondDeCollecte As String




    Public Sub New()
    End Sub

    'Public Sub New(entity As Societe)
    '    With Me
    '        .Id = entity.Id
    '        .Libelle = entity.Libelle
    '        .BP = entity.BP
    '        .Telephone = entity.Telephone
    '        .Email = entity.Email
    '        .Adresse = entity.Adresse
    '        .DateCreation = entity.DateCreation
    '        .Etat = entity.Etat
    '        .PlafondDeCollecte = entity.PlafondDeCollecte

    '    End With
    'End Sub

    'Public Function getEntity() As Societe
    '    Dim entity As New Societe

    '    With entity
    '        .Id = Me.Id
    '        .Libelle = Me.Libelle
    '        .BP = Me.BP
    '        .Telephone = Me.Telephone
    '        .Email = Me.Email
    '        .Adresse = Me.Adresse
    '        .DateCreation = Me.DateCreation
    '        .Etat = Me.Etat
    '        .PlafondDeCollecte = Me.PlafondDeCollecte

    '    End With

    '    Return entity
    'End Function


End Class
