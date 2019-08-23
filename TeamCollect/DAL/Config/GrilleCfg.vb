Imports System.Data.Entity.ModelConfiguration

Public Class GrilleCfg
    Inherits EntityTypeConfiguration(Of Grille)

    Public Sub New()
        Me.ToTable("Grille")
        Me.Property(Function(p) p.Libelle).IsRequired()
        Me.Property(Function(p) p.Etat).IsRequired()
        Me.Property(Function(p) p.DateCreation).IsRequired()
    End Sub
End Class
