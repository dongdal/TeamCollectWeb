Public Class CompteurImpression
    Public Property Id As Long
    Public Property HistoriqueMouvementId As Long
    Public Property HistoriqueMouvement As HistoriqueMouvement
    Public Property CollectriceId As String
    Public Property Collectrice As ApplicationUser
    Public Property NombreImpression As Long = 0
    Public Property DatePremiereImpression As DateTime = Now
End Class
