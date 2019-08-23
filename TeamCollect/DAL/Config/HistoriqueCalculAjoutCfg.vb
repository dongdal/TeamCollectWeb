Imports System.Data.Entity.ModelConfiguration

Public Class HistoriqueCalculAjoutCfg
    Inherits EntityTypeConfiguration(Of HistoriqueCalculAjout)

    Public Sub New()
        Me.ToTable("HistoriqueCalculAjout")
        Me.Property(Function(p) p.Id).IsRequired()
    End Sub
End Class
