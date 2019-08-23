
Imports System
Imports System.Collections.Generic

Partial Public Class Client
    Inherits Personne

    Public Property PorteFeuilleId As Nullable(Of Long)
    Public Property NumeroCompte As String
    Public Property Pourcentage As Decimal?
    Public Property Solde As Nullable(Of Decimal)
    'Modification du 28-01-2019
    Public Property SoldeDisponible As Nullable(Of Decimal)

    Public Overridable Property PorteFeuille As PorteFeuille


    Public Overridable Property HistoriqueMouvement As ICollection(Of HistoriqueMouvement) = New HashSet(Of HistoriqueMouvement)

End Class



'Public Property PersonneId As Long
'Public Property UserId As String
'Public Overridable Property User As ApplicationDbContext
'Public Overridable Property Personne As Personne