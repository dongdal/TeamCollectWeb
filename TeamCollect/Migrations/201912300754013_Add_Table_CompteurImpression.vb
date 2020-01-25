Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class Add_Table_CompteurImpression
        Inherits DbMigration
    
        Public Overrides Sub Up()
            CreateTable(
                "dbo.CompteurImpression",
                Function(c) New With
                    {
                        .Id = c.Long(nullable := False, identity := True),
                        .HistoriqueMouvementId = c.Long(nullable := False),
                        .CollectriceId = c.String(nullable := False, maxLength := 128),
                        .NombreImpression = c.Long(nullable := False),
                        .DatePremiereImpression = c.DateTime(nullable := False)
                    }) _
                .PrimaryKey(Function(t) t.Id) _
                .ForeignKey("dbo.AspNetUsers", Function(t) t.CollectriceId) _
                .ForeignKey("dbo.HistoriqueMouvement", Function(t) t.HistoriqueMouvementId) _
                .Index(Function(t) t.HistoriqueMouvementId) _
                .Index(Function(t) t.CollectriceId)
            
        End Sub
        
        Public Overrides Sub Down()
            DropForeignKey("dbo.CompteurImpression", "HistoriqueMouvementId", "dbo.HistoriqueMouvement")
            DropForeignKey("dbo.CompteurImpression", "CollectriceId", "dbo.AspNetUsers")
            DropIndex("dbo.CompteurImpression", New String() { "CollectriceId" })
            DropIndex("dbo.CompteurImpression", New String() { "HistoriqueMouvementId" })
            DropTable("dbo.CompteurImpression")
        End Sub
    End Class
End Namespace
