Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class Modifdu25052019AjoutTablesCalculCommission
        Inherits DbMigration
    
        Public Overrides Sub Up()
            CreateTable(
                "dbo.CommissionColecteur",
                Function(c) New With
                    {
                        .Id = c.Long(nullable := False, identity := True),
                        .CollecteurId = c.Long(nullable := False),
                        .HistoriqueCalculCommissionId = c.Long(nullable := False),
                        .TotalCollect = c.Double(nullable := False),
                        .Commission = c.Double(nullable := False),
                        .Mois = c.Long(nullable := False),
                        .Annee = c.Long(nullable := False),
                        .Libelle = c.String(),
                        .Etat = c.Boolean(nullable := False),
                        .DateCreation = c.DateTime(nullable := False)
                    }) _
                .PrimaryKey(Function(t) t.Id) _
                .ForeignKey("dbo.Collecteur", Function(t) t.CollecteurId) _
                .ForeignKey("dbo.HistoriqueCalculCommission", Function(t) t.HistoriqueCalculCommissionId) _
                .Index(Function(t) t.CollecteurId) _
                .Index(Function(t) t.HistoriqueCalculCommissionId)
            
            CreateTable(
                "dbo.HistoriqueCalculCommission",
                Function(c) New With
                    {
                        .Id = c.Long(nullable := False, identity := True),
                        .AgenceId = c.Long(),
                        .Mois = c.Long(nullable := False),
                        .Annee = c.Long(nullable := False),
                        .Libelle = c.String(),
                        .Etat = c.Boolean(nullable := False),
                        .DateCreation = c.DateTime(nullable := False),
                        .UserId = c.String(maxLength := 128)
                    }) _
                .PrimaryKey(Function(t) t.Id) _
                .ForeignKey("dbo.Agence", Function(t) t.AgenceId) _
                .ForeignKey("dbo.AspNetUsers", Function(t) t.UserId) _
                .Index(Function(t) t.AgenceId) _
                .Index(Function(t) t.UserId)
            
        End Sub
        
        Public Overrides Sub Down()
            DropForeignKey("dbo.CommissionColecteur", "HistoriqueCalculCommissionId", "dbo.HistoriqueCalculCommission")
            DropForeignKey("dbo.HistoriqueCalculCommission", "UserId", "dbo.AspNetUsers")
            DropForeignKey("dbo.HistoriqueCalculCommission", "AgenceId", "dbo.Agence")
            DropForeignKey("dbo.CommissionColecteur", "CollecteurId", "dbo.Collecteur")
            DropIndex("dbo.HistoriqueCalculCommission", New String() { "UserId" })
            DropIndex("dbo.HistoriqueCalculCommission", New String() { "AgenceId" })
            DropIndex("dbo.CommissionColecteur", New String() { "HistoriqueCalculCommissionId" })
            DropIndex("dbo.CommissionColecteur", New String() { "CollecteurId" })
            DropTable("dbo.HistoriqueCalculCommission")
            DropTable("dbo.CommissionColecteur")
        End Sub
    End Class
End Namespace
