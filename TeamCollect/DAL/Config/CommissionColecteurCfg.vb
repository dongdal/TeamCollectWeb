Imports System.Data.Entity.ModelConfiguration

Public Class CommissionColecteurCfg
    Inherits EntityTypeConfiguration(Of CommissionColecteur)

    Public Sub New()
        Me.ToTable("CommissionColecteur")
        Me.Property(Function(p) p.Id).IsRequired()
    End Sub
End Class
