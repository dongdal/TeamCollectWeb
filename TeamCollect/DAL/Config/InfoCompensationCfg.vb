Imports System.Data.Entity.ModelConfiguration

Public Class InfoCompensationCfg
    Inherits EntityTypeConfiguration(Of InfoCompensation)

    Public Sub New()
        Me.ToTable("InfoCompensation")
        Me.Property(Function(p) p.Libelle).IsRequired()
        Me.Property(Function(p) p.MontantVerse).IsRequired()
        Me.Property(Function(p) p.Etat).IsRequired()
        Me.Property(Function(p) p.DateCreation).IsRequired()
    End Sub
End Class
