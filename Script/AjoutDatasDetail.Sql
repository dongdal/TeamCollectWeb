
SELECT CollecteurId, ClientId, h.Id, TotalCollecte, Commission, h.Mois, h.Annee, 'CalculCommission', 1, h.DateCreation  from DetailsCommission, HistoriqueCalculCommission h

