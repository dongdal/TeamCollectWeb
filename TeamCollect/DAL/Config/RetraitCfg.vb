Imports System.Data.Entity.ModelConfiguration

Public Class RetraitCfg
    Inherits EntityTypeConfiguration(Of Retrait)

    Public Sub New()
        Me.ToTable("Retrait")
        Me.Property(Function(p) p.Id).IsRequired()
    End Sub
End Class
