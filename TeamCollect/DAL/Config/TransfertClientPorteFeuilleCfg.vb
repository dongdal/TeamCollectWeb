Imports System.Data.Entity.ModelConfiguration

Public Class TransfertClientPorteFeuilleCfg
    Inherits EntityTypeConfiguration(Of TransfertClientPorteFeuille)

    Public Sub New()
        Me.ToTable("TransfertClientPorteFeuille")
    End Sub
End Class
