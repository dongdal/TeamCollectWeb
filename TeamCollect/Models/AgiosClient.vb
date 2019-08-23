
Imports System
Imports System.Collections.Generic

Partial Public Class AgiosClient
    Public Property Id As Long
    Public Property ClientId As Long
    Public Property HistoriqueCalculAjoutId As Long
    Public Property TotalCollect As Double
    Public Property Frais As Double
    Public Property Mois As Long
    Public Property Annee As Long
    Public Property Libelle As String
    Public Property Etat As Boolean = False
    Public Property DateCreation As DateTime = Now


    Public Overridable Property User As Client
    Public Overridable Property HistoriqueCalculAjout As HistoriqueCalculAjout

End Class
