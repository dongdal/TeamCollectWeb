Imports System.Data.Entity.ModelConfiguration

Public Class JournalCaisseCfg
    Inherits EntityTypeConfiguration(Of JournalCaisse)

    Public Sub New()
        Me.ToTable("JournalCaisse")
        Me.Property(Function(p) p.FondCaisse).IsOptional()
        Me.Property(Function(p) p.DateOuverture).IsOptional()
        Me.Property(Function(p) p.DateCloture).IsOptional()
        Me.Property(Function(p) p.Date).IsOptional()
        Me.Property(Function(p) p.Etat).IsRequired()
        Me.Property(Function(p) p.DateCreation).IsRequired()
        Me.Property(Function(p) p.MontantTheorique).HasColumnType("Money")
        Me.Property(Function(p) p.MontantReel).HasColumnType("Money")
        Me.Property(Function(p) p.PlafondEnCours).HasColumnType("Money")
        Me.Property(Function(p) p.PlafondDeDebat).HasColumnType("Money")
    End Sub
End Class
