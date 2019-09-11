Imports System.Data.Entity.ModelConfiguration

Public Class HistoriqueCollecteurCategorieCfg
    Inherits EntityTypeConfiguration(Of HistoriqueCollecteurCategorie)

    Public Sub New()
        Me.ToTable("HistoriqueCollecteurCategorie")
        Me.Property(Function(p) p.Libelle).IsRequired().HasMaxLength(250)
        Me.Property(Function(p) p.CommissionMinimale).IsRequired()
        Me.Property(Function(p) p.PourcentageCommission).IsRequired()
        Me.Property(Function(p) p.SalaireDeBase).IsRequired()
        Me.Property(Function(p) p.StatutExistant).IsRequired()
        Me.Property(Function(p) p.DateCreation).IsRequired()
    End Sub
End Class
