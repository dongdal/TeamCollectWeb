Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class mycol2
        Inherits DbMigration
    
        Public Overrides Sub Up()
            AddColumn("dbo.Personne", "AgenceId", Function(c) c.Long())
            CreateIndex("dbo.Personne", "AgenceId")
            AddForeignKey("dbo.Personne", "AgenceId", "dbo.Agence", "Id")
        End Sub
        
        Public Overrides Sub Down()
            DropForeignKey("dbo.Personne", "AgenceId", "dbo.Agence")
            DropIndex("dbo.Personne", New String() { "AgenceId" })
            DropColumn("dbo.Personne", "AgenceId")
        End Sub
    End Class
End Namespace
