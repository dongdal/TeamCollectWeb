
Imports System
Imports System.Collections.Generic

Partial Public Class CommissionColecteur
    Public Property Id As Long
    Public Property CollecteurId As Long
    Public Property HistoriqueCalculCommissionId As Long
    Public Property TotalCollect As Double
    Public Property Commission As Double
    Public Property Mois As Long
    Public Property Annee As Long
    Public Property Libelle As String
    Public Property Etat As Boolean = False
    Public Property DateCreation As DateTime = Now


    Public Overridable Property Collecteur As Collecteur
    Public Overridable Property HistoriqueCalculCommission As HistoriqueCalculCommission

End Class
