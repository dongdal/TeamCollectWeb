Imports System.Data.Entity.ModelConfiguration

Public Class ClientCfg
    Inherits EntityTypeConfiguration(Of Client)

    Public Sub New()
        Me.ToTable("Client")
        Me.Property(Function(p) p.NumeroCompte).IsOptional().HasMaxLength(30)
        Me.Property(Function(p) p.Pourcentage).IsOptional()
        Me.Property(Function(p) p.Solde).IsOptional()
        Me.Property(Function(p) p.Etat).IsRequired()
        Me.Property(Function(p) p.DateCreation).IsRequired()
    End Sub
End Class
