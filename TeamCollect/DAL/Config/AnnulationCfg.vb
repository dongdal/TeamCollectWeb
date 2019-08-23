Imports System.Data.Entity.ModelConfiguration

Public Class AnnulationCfg
    Inherits EntityTypeConfiguration(Of Annulation)

    Public Sub New()
        Me.ToTable("Annulation")
        Me.Property(Function(p) p.Motif).IsRequired()
        'Me.Property(Function(p) p.BP).IsOptional()
        'Me.Property(Function(p) p.Telephone).IsOptional()
        'Me.Property(Function(p) p.Email).IsRequired()
        'Me.Property(Function(p) p.Adresse).IsOptional()
        'Me.Property(Function(p) p.Etat).IsRequired()
        'Me.Property(Function(p) p.DateCreation).IsRequired()
    End Sub
End Class
