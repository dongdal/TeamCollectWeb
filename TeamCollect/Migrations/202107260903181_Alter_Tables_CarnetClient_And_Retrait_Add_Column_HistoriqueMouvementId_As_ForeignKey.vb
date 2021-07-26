Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class Alter_Tables_CarnetClient_And_Retrait_Add_Column_HistoriqueMouvementId_As_ForeignKey
        Inherits DbMigration
    
        Public Overrides Sub Up()
            AddColumn("dbo.CarnetClient", "HistoriqueMouvementId", Function(c) c.Long())
            AddColumn("dbo.Retrait", "HistoriqueMouvementId", Function(c) c.Long())
            CreateIndex("dbo.CarnetClient", "HistoriqueMouvementId")
            CreateIndex("dbo.Retrait", "HistoriqueMouvementId")
            AddForeignKey("dbo.CarnetClient", "HistoriqueMouvementId", "dbo.HistoriqueMouvement", "Id")
            AddForeignKey("dbo.Retrait", "HistoriqueMouvementId", "dbo.HistoriqueMouvement", "Id")
        End Sub
        
        Public Overrides Sub Down()
            DropForeignKey("dbo.Retrait", "HistoriqueMouvementId", "dbo.HistoriqueMouvement")
            DropForeignKey("dbo.CarnetClient", "HistoriqueMouvementId", "dbo.HistoriqueMouvement")
            DropIndex("dbo.Retrait", New String() { "HistoriqueMouvementId" })
            DropIndex("dbo.CarnetClient", New String() { "HistoriqueMouvementId" })
            DropColumn("dbo.Retrait", "HistoriqueMouvementId")
            DropColumn("dbo.CarnetClient", "HistoriqueMouvementId")
        End Sub
    End Class
End Namespace
