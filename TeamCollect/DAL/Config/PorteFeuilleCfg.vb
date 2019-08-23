Imports System.Data.Entity.ModelConfiguration

Public Class PorteFeuilleCfg
    Inherits EntityTypeConfiguration(Of PorteFeuille)

    Public Sub New()
        Me.ToTable("PorteFeuille")
        Me.Property(Function(p) p.Libelle).IsRequired()
        Me.Property(Function(p) p.Etat).IsRequired()
        Me.Property(Function(p) p.DateCreation).IsRequired()
    End Sub
End Class
