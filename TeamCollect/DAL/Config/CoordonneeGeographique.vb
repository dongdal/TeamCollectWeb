Imports System.Data.Entity.ModelConfiguration

Public Class CoordonneeGeographiqueCfg
    Inherits EntityTypeConfiguration(Of CoordonneeGeographique)

    Public Sub New()
        Me.ToTable("CoordonneeGeographique")
        Me.Property(Function(p) p.Latitude).IsRequired().HasMaxLength(50)
        Me.Property(Function(p) p.Longitude).IsRequired().HasMaxLength(50)
        Me.Property(Function(p) p.Etat).IsRequired()
        Me.Property(Function(p) p.DateCreation).IsRequired()
    End Sub
End Class
