Imports System.Data.Entity.ModelConfiguration

Public Class CategorieRemunerationCfg
    Inherits EntityTypeConfiguration(Of CategorieRemuneration)

    Public Sub New()
        Me.ToTable("CategorieRemuneration")
        Me.Property(Function(p) p.Libelle).IsRequired().HasMaxLength(250)
        Me.Property(Function(p) p.CommissionMinimale).IsRequired()
        Me.Property(Function(p) p.PourcentageCommission).IsRequired()
        Me.Property(Function(p) p.SalaireDeBase).IsRequired()
        Me.Property(Function(p) p.StatutExistant).IsRequired()
        Me.Property(Function(p) p.DateCreation).IsRequired()
        Me.Property(Function(p) p.SalaireDeBase).HasColumnType("Money")
    End Sub
End Class
