Imports System.Data.Entity.ModelConfiguration

Public Class CollecteurCfg
    Inherits EntityTypeConfiguration(Of Collecteur)

    Public Sub New()
        Me.ToTable("Collecteur")
        Me.Property(Function(p) p.Pourcentage).IsOptional()
        Me.Property(Function(p) p.AdrMac).IsOptional()
        Me.Property(Function(p) p.Etat).IsRequired()
        Me.Property(Function(p) p.DateCreation).IsRequired()
    End Sub
End Class
