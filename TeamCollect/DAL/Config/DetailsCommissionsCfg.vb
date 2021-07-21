Imports System.Data.Entity.ModelConfiguration

Public Class DetailsCommissionsCfg
    Inherits EntityTypeConfiguration(Of DetailsCommissions)

    Public Sub New()
        Me.ToTable("DetailsCommissions")
        Me.Property(Function(p) p.TotalCollecte).HasColumnType("Money")
        Me.Property(Function(p) p.Commission).HasColumnType("Money")

    End Sub
End Class
