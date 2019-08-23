
Imports System
Imports System.Collections.Generic

Partial Public Class HistoriqueCalculAjout
    Public Property Id As Long
    Public Property AgenceId As Long?
    Public Property Mois As Long
    Public Property Annee As Long
    Public Property Libelle As String
    Public Property Etat As Boolean = False
    Public Property DateCreation As DateTime = Now

    Public Property UserId As String
    Public Overridable Property User As ApplicationUser

    Public Overridable Property Agence As Agence

End Class
