Imports System.Data.Entity.ModelConfiguration

Public Class SecteurActiviteCfg
    Inherits EntityTypeConfiguration(Of SecteurActivite)

    Public Sub New()
        Me.ToTable("SecteurActivite")
        Me.Property(Function(p) p.Libelle).IsRequired()
        'Me.Property(Function(p) p.Prix)
        'Me.Property(Function(p) p.Telephone).IsOptional()
        'Me.Property(Function(p) p.Email).IsRequired()
        'Me.Property(Function(p) p.Adresse).IsOptional()
        'Me.Property(Function(p) p.Etat).IsRequired()
        'Me.Property(Function(p) p.DateCreation).IsRequired()
    End Sub
End Class
