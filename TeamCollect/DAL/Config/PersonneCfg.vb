Imports System.Data.Entity.ModelConfiguration

Public Class PersonneCfg
    Inherits EntityTypeConfiguration(Of Personne)

    Public Sub New()
        Me.ToTable("Personne")
        Me.Property(Function(p) p.CodeSecret).IsOptional()
        Me.Property(Function(p) p.Nom).IsOptional()
        Me.Property(Function(p) p.Prenom).IsOptional()
        Me.Property(Function(p) p.Sexe).IsOptional()
        Me.Property(Function(p) p.Telephone).IsOptional()
        Me.Property(Function(p) p.Adresse).IsOptional()
        Me.Property(Function(p) p.Quartier).IsOptional()
        Me.Property(Function(p) p.CNI).IsOptional()
        Me.Property(Function(p) p.Etat).IsRequired()
        Me.Property(Function(p) p.DateCreation).IsRequired()
    End Sub
End Class
