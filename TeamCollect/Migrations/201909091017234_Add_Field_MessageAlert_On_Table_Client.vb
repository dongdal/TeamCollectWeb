Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class Add_Field_MessageAlert_On_Table_Client
        Inherits DbMigration
    
        Public Overrides Sub Up()
            AddColumn("dbo.Client", "MessageAlerte", Function(c) c.String(maxLength := 4000))
        End Sub
        
        Public Overrides Sub Down()
            DropColumn("dbo.Client", "MessageAlerte")
        End Sub
    End Class
End Namespace
