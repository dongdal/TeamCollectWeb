
Imports System
Imports System.Collections.Generic

Public Class Retrait

    Public Property Id As Long
    Public Property CollecteurId As Long
    Public Property ClientId As Long
    Public Property HistoriqueMouvementId As Long?
    Public Property Montant As Decimal
    Public Property SoldeApreOperation As Decimal
    Public Property DateRetrait As Date?
    Public Property DateCloture As Date?

    Public Property Etat As Boolean = True
    Public Property DateCreation As DateTime = Now

    Public Property UserId As String
    Public Overridable Property User As ApplicationUser

    Public Overridable Property Collecteur As Collecteur
    Public Overridable Property Client As Client
    Public Overridable Property HistoriqueMouvement As HistoriqueMouvement

End Class

