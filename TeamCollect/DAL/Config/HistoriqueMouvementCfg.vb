Imports System.Data.Entity.ModelConfiguration

Public Class HistoriqueMouvementCfg
    Inherits EntityTypeConfiguration(Of HistoriqueMouvement)

    Public Sub New()
        Me.ToTable("HistoriqueMouvement")
        Me.Property(Function(p) p.Montant).IsOptional()
        Me.Property(Function(p) p.DateOperation).IsOptional()
        Me.Property(Function(p) p.Pourcentage).IsOptional()
        Me.Property(Function(p) p.MontantRetenu).IsOptional()
        Me.Property(Function(p) p.PartBANK).IsOptional()
        Me.Property(Function(p) p.PartCLIENT).IsOptional()
        Me.Property(Function(p) p.Longitude).IsOptional()
        Me.Property(Function(p) p.Latitude).IsOptional()
        Me.Property(Function(p) p.EstTraiter).IsOptional()
        Me.Property(Function(p) p.Etat).IsRequired()
        Me.Property(Function(p) p.DateTraitement).IsOptional()
        Me.Property(Function(p) p.DateCreation).IsRequired()
    End Sub
End Class
