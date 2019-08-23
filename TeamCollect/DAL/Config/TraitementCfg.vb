Imports System.Data.Entity.ModelConfiguration

Public Class TraitementCfg
    Inherits EntityTypeConfiguration(Of Traitement)

    Public Sub New()
        Me.ToTable("Traitement")
        Me.Property(Function(p) p.Fichier).IsOptional()
        Me.Property(Function(p) p.DateTraitement).IsOptional()
        Me.Property(Function(p) p.Etat).IsRequired()
        Me.Property(Function(p) p.DateCreation).IsRequired()
    End Sub
End Class

