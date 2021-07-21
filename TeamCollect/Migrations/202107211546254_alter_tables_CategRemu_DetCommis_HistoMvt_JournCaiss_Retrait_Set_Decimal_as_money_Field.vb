Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class alter_tables_CategRemu_DetCommis_HistoMvt_JournCaiss_Retrait_Set_Decimal_as_money_Field
        Inherits DbMigration
    
        Public Overrides Sub Up()
            AlterColumn("dbo.HistoriqueMouvement", "Montant", Function(c) c.Decimal(storeType := "money"))
            AlterColumn("dbo.CategorieRemuneration", "SalaireDeBase", Function(c) c.Decimal(nullable := False, storeType := "money"))
            AlterColumn("dbo.JournalCaisse", "MontantTheorique", Function(c) c.Decimal(storeType := "money"))
            AlterColumn("dbo.JournalCaisse", "MontantReel", Function(c) c.Decimal(storeType := "money"))
            AlterColumn("dbo.JournalCaisse", "PlafondDeDebat", Function(c) c.Decimal(nullable := False, storeType := "money"))
            AlterColumn("dbo.JournalCaisse", "PlafondEnCours", Function(c) c.Decimal(storeType := "money"))
            AlterColumn("dbo.DetailsCommissions", "TotalCollecte", Function(c) c.Decimal(nullable := False, storeType := "money"))
            AlterColumn("dbo.DetailsCommissions", "Commission", Function(c) c.Decimal(nullable := False, storeType := "money"))
            AlterColumn("dbo.Retrait", "Montant", Function(c) c.Decimal(nullable := False, storeType := "money"))
            AlterColumn("dbo.Retrait", "SoldeApreOperation", Function(c) c.Decimal(nullable := False, storeType := "money"))
        End Sub
        
        Public Overrides Sub Down()
            AlterColumn("dbo.Retrait", "SoldeApreOperation", Function(c) c.Decimal(nullable := False, precision := 18, scale := 2))
            AlterColumn("dbo.Retrait", "Montant", Function(c) c.Decimal(nullable := False, precision := 18, scale := 2))
            AlterColumn("dbo.DetailsCommissions", "Commission", Function(c) c.Decimal(nullable := False, precision := 18, scale := 2))
            AlterColumn("dbo.DetailsCommissions", "TotalCollecte", Function(c) c.Decimal(nullable := False, precision := 18, scale := 2))
            AlterColumn("dbo.JournalCaisse", "PlafondEnCours", Function(c) c.Decimal(precision := 18, scale := 2))
            AlterColumn("dbo.JournalCaisse", "PlafondDeDebat", Function(c) c.Decimal(nullable := False, precision := 18, scale := 2))
            AlterColumn("dbo.JournalCaisse", "MontantReel", Function(c) c.Decimal(precision := 18, scale := 2))
            AlterColumn("dbo.JournalCaisse", "MontantTheorique", Function(c) c.Decimal(precision := 18, scale := 2))
            AlterColumn("dbo.CategorieRemuneration", "SalaireDeBase", Function(c) c.Decimal(nullable := False, precision := 18, scale := 2))
            AlterColumn("dbo.HistoriqueMouvement", "Montant", Function(c) c.Decimal(precision := 18, scale := 2))
        End Sub
    End Class
End Namespace
