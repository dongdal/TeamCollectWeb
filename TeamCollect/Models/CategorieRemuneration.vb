Public Class CategorieRemuneration
    Public Property Id As Long
    Public Property Libelle As String
    Public Property SalaireDeBase As Decimal
    Public Property CommissionMinimale As Decimal
    Public Property PourcentageCommission As Double
    Public Property StatutExistant As Boolean = True
    Public Property DateCreation As DateTime = Now


    Public Property UserId As String
    Public Overridable Property User As ApplicationUser

    Public Overridable Property Collecteur As ICollection(Of Collecteur) = New HashSet(Of Collecteur)
    Public Overridable Property CollecteurCategorie As ICollection(Of HistoriqueCollecteurCategorie) = New HashSet(Of HistoriqueCollecteurCategorie)
End Class
