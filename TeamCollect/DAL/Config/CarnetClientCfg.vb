Imports System.Data.Entity.ModelConfiguration

Public Class CarnetClientCfg
    Inherits EntityTypeConfiguration(Of CarnetClient)

    Public Sub New()
        Me.ToTable("CarnetClient")
        Me.Property(Function(p) p.Id).IsRequired()
        Me.Property(Function(p) p.HistoriqueMouvementId).IsOptional()
    End Sub
End Class
