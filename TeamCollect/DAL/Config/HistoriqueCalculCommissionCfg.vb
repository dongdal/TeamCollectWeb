Imports System.Data.Entity.ModelConfiguration

Public Class HistoriqueCalculCommissionCfg
    Inherits EntityTypeConfiguration(Of HistoriqueCalculCommission)

    Public Sub New()
        Me.ToTable("HistoriqueCalculCommission")
        Me.Property(Function(p) p.Id).IsRequired()
    End Sub
End Class
