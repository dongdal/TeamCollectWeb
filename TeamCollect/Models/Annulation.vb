
Imports System
Imports System.Collections.Generic

Public Class Annulation

    Public Property Id As Long
    Public Property HistoriqueMouvementId As Long
    Public Property Motif As String
    Public Property DateAnnulation As DateTime?


    Public Property UserId As String
    Public Overridable Property User As ApplicationUser

    Public Overridable Property HistoriqueMouvement As HistoriqueMouvement
End Class

