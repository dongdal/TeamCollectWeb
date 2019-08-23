Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class ModifDu22042019AjoutTableAnnulation
        Inherits DbMigration
    
        Public Overrides Sub Up()
            CreateTable(
                "dbo.Annulation",
                Function(c) New With
                    {
                        .Id = c.Long(nullable := False, identity := True),
                        .HistoriqueMouvementId = c.Long(nullable := False),
                        .Motif = c.String(nullable := False),
                        .DateAnnulation = c.DateTime(),
                        .UserId = c.String(maxLength := 128)
                    }) _
                .PrimaryKey(Function(t) t.Id) _
                .ForeignKey("dbo.HistoriqueMouvement", Function(t) t.HistoriqueMouvementId) _
                .ForeignKey("dbo.AspNetUsers", Function(t) t.UserId) _
                .Index(Function(t) t.HistoriqueMouvementId) _
                .Index(Function(t) t.UserId)
            
            AddColumn("dbo.HistoriqueCalculAjout", "AgenceId", Function(c) c.Long())
            CreateIndex("dbo.HistoriqueCalculAjout", "AgenceId")
            AddForeignKey("dbo.HistoriqueCalculAjout", "AgenceId", "dbo.Agence", "Id")
        End Sub
        
        Public Overrides Sub Down()
            DropForeignKey("dbo.HistoriqueCalculAjout", "AgenceId", "dbo.Agence")
            DropForeignKey("dbo.Annulation", "UserId", "dbo.AspNetUsers")
            DropForeignKey("dbo.Annulation", "HistoriqueMouvementId", "dbo.HistoriqueMouvement")
            DropIndex("dbo.HistoriqueCalculAjout", New String() { "AgenceId" })
            DropIndex("dbo.Annulation", New String() { "UserId" })
            DropIndex("dbo.Annulation", New String() { "HistoriqueMouvementId" })
            DropColumn("dbo.HistoriqueCalculAjout", "AgenceId")
            DropTable("dbo.Annulation")
        End Sub
    End Class
End Namespace
