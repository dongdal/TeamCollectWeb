Imports System.Data.Entity.ModelConfiguration

Public Class RetraitCfg
    Inherits EntityTypeConfiguration(Of Retrait)

    Public Sub New()
        Me.ToTable("Retrait")
        Me.Property(Function(p) p.Id).IsRequired()
        Me.Property(Function(p) p.Montant).HasColumnType("Money")
        Me.Property(Function(p) p.SoldeApreOperation).HasColumnType("Money")
    End Sub
End Class
