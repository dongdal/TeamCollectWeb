Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class Modif21022019AjoutHistoriqueCalcul
        Inherits DbMigration
    
        Public Overrides Sub Up()
            CreateTable(
                "dbo.HistoriqueCalculAjout",
                Function(c) New With
                    {
                        .Id = c.Long(nullable := False, identity := True),
                        .Mois = c.Long(nullable := False),
                        .Annee = c.Long(nullable := False),
                        .Libelle = c.String(),
                        .Etat = c.Boolean(nullable := False),
                        .DateCreation = c.DateTime(nullable := False),
                        .UserId = c.String(maxLength := 128)
                    }) _
                .PrimaryKey(Function(t) t.Id) _
                .ForeignKey("dbo.AspNetUsers", Function(t) t.UserId) _
                .Index(Function(t) t.UserId)
            
        End Sub
        
        Public Overrides Sub Down()
            DropForeignKey("dbo.HistoriqueCalculAjout", "UserId", "dbo.AspNetUsers")
            DropIndex("dbo.HistoriqueCalculAjout", New String() { "UserId" })
            DropTable("dbo.HistoriqueCalculAjout")
        End Sub
    End Class
End Namespace
