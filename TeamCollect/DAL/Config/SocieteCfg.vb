Imports System.Data.Entity.ModelConfiguration

Public Class SocieteCfg
    Inherits EntityTypeConfiguration(Of Societe)

    Public Sub New()
        Me.ToTable("Societe")
        Me.Property(Function(p) p.Libelle).IsRequired()
        Me.Property(Function(p) p.BP).IsOptional()
        Me.Property(Function(p) p.Telephone).IsOptional()
        Me.Property(Function(p) p.Email).IsRequired()
        Me.Property(Function(p) p.Adresse).IsOptional()
        Me.Property(Function(p) p.Etat).IsRequired()
        Me.Property(Function(p) p.DateCreation).IsRequired()
    End Sub
End Class
