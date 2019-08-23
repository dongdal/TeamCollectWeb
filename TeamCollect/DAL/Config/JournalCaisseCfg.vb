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
    End Sub
End Class
