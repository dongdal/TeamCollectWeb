Imports System.Data.Entity.ModelConfiguration

Public Class AgiosClientCfg
    Inherits EntityTypeConfiguration(Of AgiosClient)

    Public Sub New()
        Me.ToTable("AgiosClient")
        Me.Property(Function(p) p.Id).IsRequired()
    End Sub
End Class
