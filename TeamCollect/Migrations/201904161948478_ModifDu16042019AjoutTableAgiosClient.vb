Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class ModifDu16042019AjoutTableAgiosClient
        Inherits DbMigration
    
        Public Overrides Sub Up()
            CreateTable(
                "dbo.AgiosClient",
                Function(c) New With
                    {
                        .Id = c.Long(nullable := False, identity := True),
                        .ClientId = c.Long(nullable := False),
                        .HistoriqueCalculAjoutId = c.Long(nullable := False),
                        .TotalCollect = c.Double(nullable := False),
                        .Frais = c.Double(nullable := False),
                        .Mois = c.Long(nullable := False),
                        .Annee = c.Long(nullable := False),
                        .Libelle = c.String(),
                        .Etat = c.Boolean(nullable := False),
                        .DateCreation = c.DateTime(nullable := False)
                    }) _
                .PrimaryKey(Function(t) t.Id) _
                .ForeignKey("dbo.HistoriqueCalculAjout", Function(t) t.HistoriqueCalculAjoutId) _
                .ForeignKey("dbo.Client", Function(t) t.ClientId) _
                .Index(Function(t) t.ClientId) _
                .Index(Function(t) t.HistoriqueCalculAjoutId)
            
        End Sub
        
        Public Overrides Sub Down()
            DropForeignKey("dbo.AgiosClient", "ClientId", "dbo.Client")
            DropForeignKey("dbo.AgiosClient", "HistoriqueCalculAjoutId", "dbo.HistoriqueCalculAjout")
            DropIndex("dbo.AgiosClient", New String() { "HistoriqueCalculAjoutId" })
            DropIndex("dbo.AgiosClient", New String() { "ClientId" })
            DropTable("dbo.AgiosClient")
        End Sub
    End Class
End Namespace
