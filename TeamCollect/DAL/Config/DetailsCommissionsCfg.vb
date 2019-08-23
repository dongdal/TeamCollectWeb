Imports System.Data.Entity.ModelConfiguration

Public Class DetailsCommissionsCfg
    Inherits EntityTypeConfiguration(Of DetailsCommissions)

    Public Sub New()
        Me.ToTable("DetailsCommissions")

    End Sub
End Class
