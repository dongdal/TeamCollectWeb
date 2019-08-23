Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class ModifDu22012019
        Inherits DbMigration
    
        Public Overrides Sub Up()
            CreateTable(
                "dbo.BorneCommission",
                Function(c) New With
                    {
                        .Id = c.Long(nullable := False, identity := True),
                        .BornInf = c.Decimal(nullable := False, precision := 18, scale := 2),
                        .BornSup = c.Decimal(nullable := False, precision := 18, scale := 2)
                    }) _
                .PrimaryKey(Function(t) t.Id)
            
            CreateTable(
                "dbo.CarnetClient",
                Function(c) New With
                    {
                        .Id = c.Long(nullable := False, identity := True),
                        .ClientId = c.Long(nullable := False),
                        .TypeCarnetId = c.Long(nullable := False),
                        .Etat = c.Boolean(nullable := False),
                        .DateAffectation = c.DateTime()
                    }) _
                .PrimaryKey(Function(t) t.Id)
            
            CreateTable(
                "dbo.Retrait",
                Function(c) New With
                    {
                        .Id = c.Long(nullable := False, identity := True),
                        .CollecteurId = c.Long(nullable := False),
                        .Montant = c.Decimal(nullable := False, precision := 18, scale := 2),
                        .SoldeApreOperation = c.Decimal(nullable := False, precision := 18, scale := 2),
                        .DateRetrait = c.DateTime(),
                        .DateCloture = c.DateTime(),
                        .Etat = c.Boolean(nullable := False),
                        .DateCreation = c.DateTime(nullable := False)
                    }) _
                .PrimaryKey(Function(t) t.Id)
            
            CreateTable(
                "dbo.TypeCarnet",
                Function(c) New With
                    {
                        .Id = c.Long(nullable := False, identity := True),
                        .Libelle = c.String(nullable := False),
                        .Prix = c.Double(),
                        .Etat = c.Boolean(nullable := False),
                        .DateCreation = c.DateTime(nullable := False)
                    }) _
                .PrimaryKey(Function(t) t.Id)
            
        End Sub
        
        Public Overrides Sub Down()
            DropTable("dbo.TypeCarnet")
            DropTable("dbo.Retrait")
            DropTable("dbo.CarnetClient")
            DropTable("dbo.BorneCommission")
        End Sub
    End Class
End Namespace
