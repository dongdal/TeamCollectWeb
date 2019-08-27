Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class Control_Sur_Le_Mot_De_Passe
        Inherits DbMigration
    
        Public Overrides Sub Up()
            AddColumn("dbo.AspNetUsers", "PasswordExpiredDate", Function(c) c.DateTime())
        End Sub
        
        Public Overrides Sub Down()
            DropColumn("dbo.AspNetUsers", "PasswordExpiredDate")
        End Sub
    End Class
End Namespace
