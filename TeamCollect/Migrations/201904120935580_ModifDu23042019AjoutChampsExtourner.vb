Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class ModifDu23042019AjoutChampsExtourner
        Inherits DbMigration
    
        Public Overrides Sub Up()
            AddColumn("dbo.HistoriqueMouvement", "Extourner", Function(c) c.Boolean())
        End Sub
        
        Public Overrides Sub Down()
            DropColumn("dbo.HistoriqueMouvement", "Extourner")
        End Sub
    End Class
End Namespace
