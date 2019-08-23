Public Class DetailsCommissions
    Public Property Id As Long
    Public Property CollecteurId As Long
    Public Overridable Property Collecteur As Collecteur
    Public Property ClientId As Long
    Public Overridable Property Client As Client
    Public Property HistoriqueCalculCommissionId As Long
    Public Overridable Property HistoriqueCalculCommission As HistoriqueCalculCommission

    Public Property TotalCollecte As Decimal = 0.0
    Public Property Commission As Decimal = 0.0
    Public Property Mois As Long
    Public Property Annee As Long
    Public Property Libelle As String
    Public Property Etat As Short
    Public Property DateCreation As DateTime

End Class
