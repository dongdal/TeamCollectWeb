Imports System.Data.Entity.ModelConfiguration

Public Class BorneCommissionCfg
    Inherits EntityTypeConfiguration(Of BorneCommission)

    Public Sub New()
        Me.ToTable("BorneCommission")
        Me.Property(Function(p) p.Id).IsRequired()
    End Sub
End Class
