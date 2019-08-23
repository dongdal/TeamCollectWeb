
Imports System
Imports System.Collections.Generic

Partial Public Class JournalCaisse
    Public Property Id As Long
    Public Property CollecteurId As Long
    Public Property MontantTheorique As Decimal?
    Public Property MontantReel As Decimal?
    Public Property [Date] As Date?
    Public Property FondCaisse As Decimal?
    Public Property DateOuverture As Date?
    Public Property DateCloture As Date?

    Public Property Etat As Boolean = False
    Public Property DateCreation As DateTime = Now
    Public Property PlafondDeDebat As Decimal
    Public Property PlafondEnCours As Decimal?

    Public Property UserId As String
    Public Overridable Property User As ApplicationUser

    Public Overridable Property Collecteur As Collecteur

    Public Overridable Property HistoriqueMouvement As ICollection(Of HistoriqueMouvement) = New HashSet(Of HistoriqueMouvement)
    Public Overridable Property infoCompensation As ICollection(Of InfoCompensation) = New HashSet(Of InfoCompensation)

End Class
