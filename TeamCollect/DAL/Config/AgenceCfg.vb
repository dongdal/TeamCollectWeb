Imports System.Data.Entity.ModelConfiguration

Public Class AgenceCfg
    Inherits EntityTypeConfiguration(Of Agence)

    Public Sub New()
        Me.ToTable("Agence")
        Me.Property(Function(p) p.Libelle).IsRequired()
        Me.Property(Function(p) p.BP).IsOptional()
        Me.Property(Function(p) p.Telephone).IsOptional()
        Me.Property(Function(p) p.Email).IsRequired()
        Me.Property(Function(p) p.Adresse).IsOptional()
        Me.Property(Function(p) p.Etat).IsRequired()
        Me.Property(Function(p) p.DateCreation).IsRequired()
    End Sub
End Class
