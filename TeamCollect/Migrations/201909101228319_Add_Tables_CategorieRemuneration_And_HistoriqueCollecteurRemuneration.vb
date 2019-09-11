Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class Add_Tables_CategorieRemuneration_And_HistoriqueCollecteurRemuneration
        Inherits DbMigration
    
        Public Overrides Sub Up()
            CreateTable(
                "dbo.CategorieRemuneration",
                Function(c) New With
                    {
                        .Id = c.Long(nullable := False, identity := True),
                        .Libelle = c.String(nullable := False, maxLength := 250),
                        .SalaireDeBase = c.Decimal(nullable := False, precision := 18, scale := 2),
                        .CommissionMinimale = c.Decimal(nullable := False, precision := 18, scale := 2),
                        .PourcentageCommission = c.Double(nullable := False),
                        .StatutExistant = c.Boolean(nullable := False),
                        .DateCreation = c.DateTime(nullable := False),
                        .UserId = c.String(maxLength := 128)
                    }) _
                .PrimaryKey(Function(t) t.Id) _
                .ForeignKey("dbo.AspNetUsers", Function(t) t.UserId) _
                .Index(Function(t) t.UserId)
            
            CreateTable(
                "dbo.HistoriqueCollecteurCategorie",
                Function(c) New With
                    {
                        .Id = c.Long(nullable := False, identity := True),
                        .CollecteurId = c.Long(nullable := False),
                        .CategorieRemunerationId = c.Long(nullable := False),
                        .Libelle = c.String(nullable := False, maxLength := 250),
                        .SalaireDeBase = c.Decimal(nullable := False, precision := 18, scale := 2),
                        .CommissionMinimale = c.Decimal(nullable := False, precision := 18, scale := 2),
                        .PourcentageCommission = c.Double(nullable := False),
                        .StatutExistant = c.Boolean(nullable := False),
                        .DateCreation = c.DateTime(nullable := False),
                        .UserId = c.String(maxLength := 128)
                    }) _
                .PrimaryKey(Function(t) t.Id) _
                .ForeignKey("dbo.CategorieRemuneration", Function(t) t.CategorieRemunerationId) _
                .ForeignKey("dbo.Collecteur", Function(t) t.CollecteurId) _
                .ForeignKey("dbo.AspNetUsers", Function(t) t.UserId) _
                .Index(Function(t) t.CollecteurId) _
                .Index(Function(t) t.CategorieRemunerationId) _
                .Index(Function(t) t.UserId)
            
            AddColumn("dbo.Collecteur", "CategorieRemunerationId", Function(c) c.Long())
            CreateIndex("dbo.Collecteur", "CategorieRemunerationId")
            AddForeignKey("dbo.Collecteur", "CategorieRemunerationId", "dbo.CategorieRemuneration", "Id")
        End Sub
        
        Public Overrides Sub Down()
            DropForeignKey("dbo.Collecteur", "CategorieRemunerationId", "dbo.CategorieRemuneration")
            DropForeignKey("dbo.CategorieRemuneration", "UserId", "dbo.AspNetUsers")
            DropForeignKey("dbo.HistoriqueCollecteurCategorie", "UserId", "dbo.AspNetUsers")
            DropForeignKey("dbo.HistoriqueCollecteurCategorie", "CollecteurId", "dbo.Collecteur")
            DropForeignKey("dbo.HistoriqueCollecteurCategorie", "CategorieRemunerationId", "dbo.CategorieRemuneration")
            DropIndex("dbo.Collecteur", New String() { "CategorieRemunerationId" })
            DropIndex("dbo.HistoriqueCollecteurCategorie", New String() { "UserId" })
            DropIndex("dbo.HistoriqueCollecteurCategorie", New String() { "CategorieRemunerationId" })
            DropIndex("dbo.HistoriqueCollecteurCategorie", New String() { "CollecteurId" })
            DropIndex("dbo.CategorieRemuneration", New String() { "UserId" })
            DropColumn("dbo.Collecteur", "CategorieRemunerationId")
            DropTable("dbo.HistoriqueCollecteurCategorie")
            DropTable("dbo.CategorieRemuneration")
        End Sub
    End Class
End Namespace
