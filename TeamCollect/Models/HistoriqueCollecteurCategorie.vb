Public Class HistoriqueCollecteurCategorie
    Public Property Id As Long
    Public Property CollecteurId As Long
    Public Property CategorieRemunerationId As Long
    Public Property Libelle As String
    Public Property SalaireDeBase As Decimal
    Public Property CommissionMinimale As Decimal
    Public Property PourcentageCommission As Double
    Public Property StatutExistant As Boolean = True
    Public Property DateCreation As DateTime = Now

    Public Overridable Property Collecteur As Collecteur
    Public Overridable Property CategorieRemuneration As CategorieRemuneration


    Public Property UserId As String
    Public Overridable Property User As ApplicationUser
End Class
