Imports System.Data.Entity.ModelConfiguration

Public Class InfoFraisCfg
    Inherits EntityTypeConfiguration(Of InfoFrais)

    Public Sub New()
        Me.ToTable("InfoFrais")
        Me.Property(Function(p) p.BornInf).IsRequired()
        Me.Property(Function(p) p.BornSup).IsRequired()
        Me.Property(Function(p) p.Frais).IsRequired()
        Me.Property(Function(p) p.Taux).IsRequired()
    End Sub
End Class
