Imports System.Data.Entity
Imports Microsoft.AspNet.Identity.EntityFramework
Imports System.Data.Entity.ModelConfiguration.Conventions

Public Class ApplicationDbContext

    Inherits IdentityDbContext(Of ApplicationUser)

    Public Sub New()
        MyBase.New("DefaultConnection")
    End Sub

    Protected Overrides Sub OnModelCreating(modelBuilder As DbModelBuilder)
        MyBase.OnModelCreating(modelBuilder)
        modelBuilder.Conventions.Remove(Of OneToManyCascadeDeleteConvention)()
        modelBuilder.Configurations.Add(New ClientCfg())
        modelBuilder.Configurations.Add(New CollecteurCfg())
        modelBuilder.Configurations.Add(New CompteurImpressionCfg())
        modelBuilder.Configurations.Add(New CoordonneeGeographiqueCfg())
        modelBuilder.Configurations.Add(New HistoriqueMouvementCfg())
        modelBuilder.Configurations.Add(New JournalCaisseCfg())
        modelBuilder.Configurations.Add(New TraitementCfg())
        modelBuilder.Configurations.Add(New PersonneCfg())
        modelBuilder.Configurations.Add(New InfoFraisCfg())
        modelBuilder.Configurations.Add(New InfoCompensationCfg())
        modelBuilder.Configurations.Add(New SocieteCfg())
        modelBuilder.Configurations.Add(New AgenceCfg())
        modelBuilder.Configurations.Add(New PorteFeuilleCfg())
        modelBuilder.Configurations.Add(New GrilleCfg())
        modelBuilder.Configurations.Add(New DetailsCommissionsCfg())
        'modif 22-01-2019
        modelBuilder.Configurations.Add(New BorneCommissionCfg())
        modelBuilder.Configurations.Add(New CarnetClientCfg())
        modelBuilder.Configurations.Add(New RetraitCfg())
        modelBuilder.Configurations.Add(New TypeCarnetCfg())
        modelBuilder.Configurations.Add(New SecteurActiviteCfg())
        'modif du 21-02-2019
        modelBuilder.Configurations.Add(New HistoriqueCalculAjoutCfg())
        'modif du 11-04-2019
        modelBuilder.Configurations.Add(New AnnulationCfg())
        'modif du 16-04-2019 pour Agios ClientAgios
        modelBuilder.Configurations.Add(New AgiosClientCfg())
        'modif du 24-05-2019 pour ajout table transfert
        modelBuilder.Configurations.Add(New TransfertClientPorteFeuilleCfg())

        'modif du 24-05-2019 pour ajout table transfert
        modelBuilder.Configurations.Add(New CommissionColecteurCfg())
        modelBuilder.Configurations.Add(New HistoriqueCalculCommissionCfg())
        modelBuilder.Configurations.Add(New CategorieRemunerationCfg())
        modelBuilder.Configurations.Add(New HistoriqueCollecteurCategorieCfg())

    End Sub

    Public Property Clients() As DbSet(Of Client)
    Public Property Collecteurs() As DbSet(Of Collecteur)
    Public Property CompteurImpressions() As DbSet(Of CompteurImpression)
    Public Property CoordonneeGeographiques() As DbSet(Of CoordonneeGeographique)
    Public Property HistoriqueMouvements() As DbSet(Of HistoriqueMouvement)
    Public Property JournalCaisses() As DbSet(Of JournalCaisse)
    Public Property Traitements() As DbSet(Of Traitement)
    Public Property Personnes() As DbSet(Of Personne)
    Public Property InfoFrais() As DbSet(Of InfoFrais)
    Public Property InfoCompensation() As DbSet(Of InfoCompensation)
    Public Property Societes() As DbSet(Of Societe)
    Public Property Agences() As DbSet(Of Agence)
    Public Property PorteFeuilles() As DbSet(Of PorteFeuille)
    Public Property Grilles() As DbSet(Of Grille)

    'modif 22-01-2019
    Public Property BorneCommissions() As DbSet(Of BorneCommission)
    Public Property CarnetClients() As DbSet(Of CarnetClient)
    Public Property Retraits() As DbSet(Of Retrait)
    Public Property TypeCarnets() As DbSet(Of TypeCarnet)
    Public Property SecteurActivites() As DbSet(Of SecteurActivite)
    'modif du 21-02-2019
    Public Property HistoriqueCalculAjout() As DbSet(Of HistoriqueCalculAjout)

    'modif du 11-04-2019
    Public Property Annulation() As DbSet(Of Annulation)
    'modif du 16-04-2019 pour Agios ClientAgios
    Public Property AgiosClient() As DbSet(Of AgiosClient)

    'modif du 25-05-2019 pour ajout table TransfertClientPorteFeuille
    Public Property TransfertClientPorteFeuille() As DbSet(Of TransfertClientPorteFeuille)

    'modif du 25-05-2019 pour ajout table TransfertClientPorteFeuille
    Public Property HistoriqueCalculCommission() As DbSet(Of HistoriqueCalculCommission)
    Public Property CommissionColecteur() As DbSet(Of CommissionColecteur)
    Public Property DetailsCommissions() As DbSet(Of DetailsCommissions)
    Public Property CategorieRemunerations As DbSet(Of CategorieRemuneration)
    Public Property HistoriqueCollecteurCategories As DbSet(Of HistoriqueCollecteurCategorie)
End Class
