Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class AjoutUserIdDansRetraitETCarnetClient12022019
        Inherits DbMigration
    
        Public Overrides Sub Up()
            AddColumn("dbo.CarnetClient", "UserId", Function(c) c.String(maxLength := 128))
            AddColumn("dbo.Retrait", "UserId", Function(c) c.String(maxLength := 128))
            CreateIndex("dbo.CarnetClient", "UserId")
            CreateIndex("dbo.Retrait", "UserId")
            AddForeignKey("dbo.CarnetClient", "UserId", "dbo.AspNetUsers", "Id")
            AddForeignKey("dbo.Retrait", "UserId", "dbo.AspNetUsers", "Id")
        End Sub
        
        Public Overrides Sub Down()
            DropForeignKey("dbo.Retrait", "UserId", "dbo.AspNetUsers")
            DropForeignKey("dbo.CarnetClient", "UserId", "dbo.AspNetUsers")
            DropIndex("dbo.Retrait", New String() { "UserId" })
            DropIndex("dbo.CarnetClient", New String() { "UserId" })
            DropColumn("dbo.Retrait", "UserId")
            DropColumn("dbo.CarnetClient", "UserId")
        End Sub
    End Class
End Namespace
