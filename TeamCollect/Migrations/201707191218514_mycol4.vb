Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class mycol4
        Inherits DbMigration
    
        Public Overrides Sub Up()
            AddColumn("dbo.JournalCaisse", "PlafondDeDebat", Function(c) c.Decimal(nullable := False, precision := 18, scale := 2))
            AddColumn("dbo.JournalCaisse", "PlafondEnCours", Function(c) c.Decimal(precision := 18, scale := 2))
            AddColumn("dbo.Societe", "PlafondDeCollecte", Function(c) c.Decimal(precision := 18, scale := 2))
        End Sub
        
        Public Overrides Sub Down()
            DropColumn("dbo.Societe", "PlafondDeCollecte")
            DropColumn("dbo.JournalCaisse", "PlafondEnCours")
            DropColumn("dbo.JournalCaisse", "PlafondDeDebat")
        End Sub
    End Class
End Namespace
