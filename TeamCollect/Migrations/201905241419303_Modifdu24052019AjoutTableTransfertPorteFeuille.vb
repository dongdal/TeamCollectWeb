Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class Modifdu24052019AjoutTableTransfertPorteFeuille
        Inherits DbMigration
    
        Public Overrides Sub Up()
            CreateTable(
                "dbo.TransfertClientPorteFeuille",
                Function(c) New With
                    {
                        .Id = c.Long(nullable := False, identity := True),
                        .PorteFeuilleSourceId = c.Long(nullable := False),
                        .PorteFeuilleDestinationId = c.Long(nullable := False),
                        .ClientId = c.Long(nullable := False),
                        .Etat = c.Boolean(nullable := False),
                        .DateCreation = c.DateTime(nullable := False),
                        .UserId = c.String(maxLength := 128)
                    }) _
                .PrimaryKey(Function(t) t.Id) _
                .ForeignKey("dbo.Client", Function(t) t.ClientId) _
                .ForeignKey("dbo.PorteFeuille", Function(t) t.PorteFeuilleDestinationId) _
                .ForeignKey("dbo.PorteFeuille", Function(t) t.PorteFeuilleSourceId) _
                .ForeignKey("dbo.AspNetUsers", Function(t) t.UserId) _
                .Index(Function(t) t.PorteFeuilleSourceId) _
                .Index(Function(t) t.PorteFeuilleDestinationId) _
                .Index(Function(t) t.ClientId) _
                .Index(Function(t) t.UserId)
            
        End Sub
        
        Public Overrides Sub Down()
            DropForeignKey("dbo.TransfertClientPorteFeuille", "UserId", "dbo.AspNetUsers")
            DropForeignKey("dbo.TransfertClientPorteFeuille", "PorteFeuilleSourceId", "dbo.PorteFeuille")
            DropForeignKey("dbo.TransfertClientPorteFeuille", "PorteFeuilleDestinationId", "dbo.PorteFeuille")
            DropForeignKey("dbo.TransfertClientPorteFeuille", "ClientId", "dbo.Client")
            DropIndex("dbo.TransfertClientPorteFeuille", New String() { "UserId" })
            DropIndex("dbo.TransfertClientPorteFeuille", New String() { "ClientId" })
            DropIndex("dbo.TransfertClientPorteFeuille", New String() { "PorteFeuilleDestinationId" })
            DropIndex("dbo.TransfertClientPorteFeuille", New String() { "PorteFeuilleSourceId" })
            DropTable("dbo.TransfertClientPorteFeuille")
        End Sub
    End Class
End Namespace
