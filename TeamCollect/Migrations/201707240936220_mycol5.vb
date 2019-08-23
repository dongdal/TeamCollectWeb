Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class mycol5
        Inherits DbMigration
    
        Public Overrides Sub Up()
            DropIndex("dbo.Personne", New String() { "AgenceId" })
            AlterColumn("dbo.Personne", "AgenceId", Function(c) c.Long())
            CreateIndex("dbo.Personne", "AgenceId")
        End Sub
        
        Public Overrides Sub Down()
            DropIndex("dbo.Personne", New String() { "AgenceId" })
            AlterColumn("dbo.Personne", "AgenceId", Function(c) c.Long(nullable := False))
            CreateIndex("dbo.Personne", "AgenceId")
        End Sub
    End Class
End Namespace
