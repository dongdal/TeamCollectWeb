Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class ModificationDu28_01_2019_A_09h45
        Inherits DbMigration
    
        Public Overrides Sub Up()
            CreateTable(
                "dbo.SecteurActivite",
                Function(c) New With
                    {
                        .Id = c.Long(nullable := False, identity := True),
                        .Libelle = c.String(nullable := False),
                        .Etat = c.Boolean(nullable := False),
                        .DateCreation = c.DateTime(nullable := False)
                    }) _
                .PrimaryKey(Function(t) t.Id)
            
            AddColumn("dbo.Personne", "Telephone2", Function(c) c.String())
            AddColumn("dbo.Personne", "SecteurActiviteId", Function(c) c.Long())
            AddColumn("dbo.Client", "SoldeDisponible", Function(c) c.Decimal(precision := 18, scale := 2))
            AddColumn("dbo.HistoriqueMouvement", "LibelleOperation", Function(c) c.String())
            AddColumn("dbo.Societe", "MinCollecte", Function(c) c.Decimal(precision := 18, scale := 2))
            AddColumn("dbo.Societe", "MAxCollecte", Function(c) c.Decimal(precision := 18, scale := 2))
            CreateIndex("dbo.Personne", "SecteurActiviteId")
            AddForeignKey("dbo.Personne", "SecteurActiviteId", "dbo.SecteurActivite", "Id")
        End Sub
        
        Public Overrides Sub Down()
            DropForeignKey("dbo.Personne", "SecteurActiviteId", "dbo.SecteurActivite")
            DropIndex("dbo.Personne", New String() { "SecteurActiviteId" })
            DropColumn("dbo.Societe", "MAxCollecte")
            DropColumn("dbo.Societe", "MinCollecte")
            DropColumn("dbo.HistoriqueMouvement", "LibelleOperation")
            DropColumn("dbo.Client", "SoldeDisponible")
            DropColumn("dbo.Personne", "SecteurActiviteId")
            DropColumn("dbo.Personne", "Telephone2")
            DropTable("dbo.SecteurActivite")
        End Sub
    End Class
End Namespace
