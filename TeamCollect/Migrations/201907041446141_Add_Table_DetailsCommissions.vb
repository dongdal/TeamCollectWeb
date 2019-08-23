Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class Add_Table_DetailsCommissions
        Inherits DbMigration
    
        Public Overrides Sub Up()
            CreateTable(
                "dbo.DetailsCommissions",
                Function(c) New With
                    {
                        .Id = c.Long(nullable := False, identity := True),
                        .CollecteurId = c.Long(nullable := False),
                        .ClientId = c.Long(nullable := False),
                        .HistoriqueCalculCommissionId = c.Long(nullable := False),
                        .TotalCollecte = c.Decimal(nullable := False, precision := 18, scale := 2),
                        .Commission = c.Decimal(nullable := False, precision := 18, scale := 2),
                        .Mois = c.Long(nullable := False),
                        .Annee = c.Long(nullable := False),
                        .Libelle = c.String(),
                        .Etat = c.Short(nullable := False),
                        .DateCreation = c.DateTime(nullable := False)
                    }) _
                .PrimaryKey(Function(t) t.Id) _
                .ForeignKey("dbo.Client", Function(t) t.ClientId) _
                .ForeignKey("dbo.Collecteur", Function(t) t.CollecteurId) _
                .ForeignKey("dbo.HistoriqueCalculCommission", Function(t) t.HistoriqueCalculCommissionId) _
                .Index(Function(t) t.CollecteurId) _
                .Index(Function(t) t.ClientId) _
                .Index(Function(t) t.HistoriqueCalculCommissionId)
            
        End Sub
        
        Public Overrides Sub Down()
            DropForeignKey("dbo.DetailsCommissions", "HistoriqueCalculCommissionId", "dbo.HistoriqueCalculCommission")
            DropForeignKey("dbo.DetailsCommissions", "CollecteurId", "dbo.Collecteur")
            DropForeignKey("dbo.DetailsCommissions", "ClientId", "dbo.Client")
            DropIndex("dbo.DetailsCommissions", New String() { "HistoriqueCalculCommissionId" })
            DropIndex("dbo.DetailsCommissions", New String() { "ClientId" })
            DropIndex("dbo.DetailsCommissions", New String() { "CollecteurId" })
            DropTable("dbo.DetailsCommissions")
        End Sub
    End Class
End Namespace
