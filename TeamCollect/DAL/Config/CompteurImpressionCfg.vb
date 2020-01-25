Imports System.Data.Entity.ModelConfiguration

Public Class CompteurImpressionCfg
    Inherits EntityTypeConfiguration(Of CompteurImpression)

    Public Sub New()
        Me.ToTable("CompteurImpression")
        Me.Property(Function(p) p.Id).IsRequired()
        Me.Property(Function(p) p.HistoriqueMouvementId).IsRequired()
        Me.Property(Function(p) p.CollectriceId).IsRequired()
        Me.Property(Function(p) p.NombreImpression).IsRequired()
        Me.Property(Function(p) p.DatePremiereImpression).IsRequired()
    End Sub
End Class
