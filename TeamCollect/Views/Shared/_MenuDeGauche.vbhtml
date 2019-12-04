@Imports TeamCollect.My.Resources
@*@If Request.IsAuthenticated = True Then*@
<div id="sidebarbg" class="hidden-lg hidden-md hidden-sm hidden-xs"></div>
<!--Sidebar content-->
<div id="sidebar" class="page-sidebar hidden-lg hidden-md hidden-sm hidden-xs">
    <!-- End search -->
    <!-- Start .sidebar-inner -->
    <div class="sidebar-inner">
        <!-- Start .sidebar-scrollarea -->
        <div class="sidebar-scrollarea">

            <div class="sidenav">
                <div class="sidebar-widget mb0">
                    <h6 class="title mb0">VOTRE GESTION</h6>
                </div>
                <!-- End .sidenav-widget -->
                <div class="mainnav">
                    <ul>
                        @If User.IsInAnyRole("SA,ADMINISTRATEUR,MANAGER") Then
                            @If User.IsInAnyRole("SA,ADMINISTRATEUR") Then
                                @<li>
                                    <a href="#"><i class="s16  icomoon-icon-home-2"></i><span class="txt">Votre Structure </span></a>
                                    <ul class="sub">

                                        <li>
                                            <a href="@Url.Action("Index", "Societe")" title="Votre Société"><i class="s16 icomoon-icon-arrow-right-3"></i><span class="txt">Votre Socièté</span></a>
                                        </li>
                                        <li>
                                            <a href="@Url.Action("Index", "Agence")" title="Vos Agence"><i class="s16 icomoon-icon-arrow-right-3"></i><span class="txt">Vos Agences</span></a>
                                        </li>
                                        <li>
                                            <a href="@Url.Action("Index", "Grille")" title="Grille de facturation"><i class="s16 icomoon-icon-arrow-right-3"></i><span class="txt">Vos Grilles de Fact...</span></a>
                                        </li>
                                        <li>
                                            <a href="@Url.Action("Index", "CategorieRemunerations")" title="Grille de rémuneration"><i class="s16 icomoon-icon-arrow-right-3"></i><span class="txt">Grille de rémuneration</span></a>
                                        </li>
                                        <li>
                                            <a href="@Url.Action("Index", "BorneCommission")" title="Borne des Commission"><i class="s16 icomoon-icon-arrow-right-3"></i><span class="txt">Borne de Commission</span></a>
                                        </li>
                                        <li>
                                            <a href="@Url.Action("Index", "TypeCarnet")" title="Type de Carnet"><i class="s16 icomoon-icon-arrow-right-3"></i><span class="txt">Type de Carnet</span></a>
                                        </li>
                                        <li>
                                            <a href="@Url.Action("Index", "SecteurActivite")" title="Les Professions"><i class="s16 icomoon-icon-arrow-right-3"></i><span class="txt">Les Professions</span></a>
                                        </li>
                                    </ul>

                                </li>
                                @<li>
                                    <a href="@Url.Action("IndexAdmin", "Retrait")"><i class="s16  icomoon-icon-credit"></i><span class="txt">Retrait</span> </a>
                                </li>
                                @<li>
                                    <a href="@Url.Action("Index", "Personne")"><i class="s16  icomoon-icon-user-4"></i><span class="txt">Gestion du Personnel</span> </a>
                                </li>
                                @<li>
                                    <a href="@Url.Action("Index", "Account")"><i class="s16  icomoon-icon-lock"></i><span class="txt">Gestion des profils</span> </a>
                                </li>
                            End If

                            @If User.IsInAnyRole("SA,ADMINISTRATEUR,MANAGER") Then
                                @<li>
                                    <a href="@Url.Action("Index", "Client")"><i class="s16  icomoon-icon-users"></i><span class="txt">Tous les Clients</span> </a>
                                </li>

                                @<li>
                                    <a href="@Url.Action("Index", "Collecteur")"><i class="s16  icomoon-icon-users-2"></i><span class="txt">Tous les Collecteurs</span> </a>
                                </li>
                            End If


                            @<li>
                                <a href="#"><i class="s16  icomoon-icon-print-2"></i><span class="txt">Gestion des Rapports </span></a>
                                <ul class="sub">
                                    <li>
                                        <a href="@Url.Action("ListeCollecteurGlobal", "Collecteur")"><i class="s16 icomoon-icon-arrow-right-3"></i><span class="txt">Liste des Collecteurs </span></a>
                                    </li>
                                    <li>
                                        <a href="@Url.Action("ListeClientGlobal", "Client")"><i class="s16 icomoon-icon-arrow-right-3"></i><span class="txt">Liste des Clients </span></a>
                                    </li>
                                    <li>
                                        <a href="@Url.Action("ListeClientParCollectrice", "Client")" title="Liste Clients enrôlés par une collectrice à une période donnée"> <i class="s16 icomoon-icon-arrow-right-3"></i><span class="txt">Liste Clients / Période</span></a>
                                    </li>
                                    <li>
                                        <a href="@Url.Action("HistoriqueClientGlobal", "HistoriqueMouvement")"><i class="s16 icomoon-icon-arrow-right-3"></i><span class="txt">Historique Client </span></a>
                                    </li>
                                    <li>
                                        <a href="@Url.Action("RecetteClientGlobal", "HistoriqueMouvement")"><i class="s16 icomoon-icon-arrow-right-3"></i><span class="txt">Recette Client </span></a>
                                    </li>
                                    <li>
                                        <a href="@Url.Action("HistoriqueCollecteurGlobal", "HistoriqueMouvement")"><i class="s16 icomoon-icon-arrow-right-3"></i><span class="txt">Synthèse Collecteur</span></a>
                                    </li>
                                    <li>
                                        <a href="@Url.Action("HistoriqueColParClientGlobal", "HistoriqueMouvement")"><i class="s16 icomoon-icon-arrow-right-3"></i><span class="txt">Collecteur Détails</span></a>
                                    </li>
                                    <li>
                                        <a href="@Url.Action("RecetteCollecteurGlobal", "HistoriqueMouvement")"><i class="s16 icomoon-icon-arrow-right-3"></i><span class="txt">Recette Collecteur </span></a>
                                    </li>
                                    <li>
                                        <a href="@Url.Action("HistoriqueBank", "HistoriqueMouvement")"><i class="s16 icomoon-icon-arrow-right-3"></i><span class="txt">Recette de la Banque </span></a>
                                    </li>
                                    <li>
                                        <a href="@Url.Action("CommissionCollectriceAvecGrilleRemunerationGlobal", "HistoriqueMouvement")"><i class="s16 icomoon-icon-arrow-right-3"></i><span class="txt">Fiche rémunération</span></a>
                                    </li>

                                </ul>

                            </li>

                        End If

                        @If User.IsInRole("CHEFCOLLECTEUR") Then
                            @<li>
                                <a href="@Url.Action("IndexCommission", "CalculAjout")"><i class="s16  icomoon-icon-lock"></i><span class="txt">Calcul des Commissions</span> </a>
                            </li>
                            @<li>
                                <a href="@Url.Action("Index", "CalculAjout")"><i class="s16  icomoon-icon-lock"></i><span class="txt">Calcul des Agios</span> </a>
                            </li>
                            @<li>
                                <a href="@Url.Action("Index", "Retrait")"><i class="s16  icomoon-icon-credit"></i><span class="txt">Retrait</span> </a>
                            </li>
                            @<li>
                                <a href="@Url.Action("Index", "CarnetClient")"><i class="s16  icomoon-icon-users"></i><span class="txt">Gestion des Carnets</span> </a>
                            </li>
                            @<li>
                                <a href="@Url.Action("IndexAgence", "Client")"><i class="s16  icomoon-icon-users"></i><span class="txt">Vos Clients</span> </a>
                            </li>

                            @<li>
                                <a href="@Url.Action("Import", "Client")"><i class="s16  icomoon-icon-users"></i><span class="txt">Importer un Fichier Client</span> </a>
                            </li>
                            @<li>
                                <a href="@Url.Action("IndexAgence", "Collecteur")"><i class="s16  icomoon-icon-users-2"></i><span class="txt">Vos Collecteurs</span> </a>
                            </li>
                            @<li>
                                <a href="@Url.Action("Index", "PorteFeuille")" title=""><i class="s16 icomoon-icon-archive"></i><span class="txt">Vos Portefeuilles</span></a>
                            </li>
                            @<li>
                                <a href="@Url.Action("Index", "JournalCaisse")"><i class="s16 icomoon-icon-calculate"></i><span class="txt">Ouverture de caisse</span> </a>
                            </li>

                            @<li>
                                <a href="#"><i class="s16  icomoon-icon-print-2"></i><span class="txt">Gestion des Rapports </span></a>
                                <ul class="sub">
                                    <li>
                                        <a href="@Url.Action("ListeClient", "Client")"> <i class="s16 icomoon-icon-arrow-right-3"></i><span class="txt">Liste des Clients</span></a>
                                    </li>
                                    <li>
                                        <a href="@Url.Action("ListeClientParCollectrice", "Client")" title="Liste Clients enrôlés par une collectrice à une période donnée"> <i class="s16 icomoon-icon-arrow-right-3"></i><span class="txt">Liste Clients / Période</span></a>
                                    </li>
                                    <li>
                                        <a href="@Url.Action("ListeCollecteur", "Collecteur")"><i class="s16 icomoon-icon-arrow-right-3"></i><span class="txt">Liste des Collecteurs </span></a>
                                    </li>
                                    <li>
                                        <a href="@Url.Action("HistoriqueClient", "HistoriqueMouvement")"><i class="s16 icomoon-icon-arrow-right-3"></i><span class="txt">Historique Client </span></a>
                                    </li>
                                    <li>
                                        <a href="@Url.Action("FicheCollecteJournaliere", "HistoriqueMouvement")"><i class="s16 icomoon-icon-arrow-right-3"></i><span class="txt">Fiche Collecte </span></a>
                                    </li>
                                    <li>
                                        <a href="@Url.Action("FicheCollecteJournaliereParPeriode", "HistoriqueMouvement")"><i class="s16 icomoon-icon-arrow-right-3"></i><span class="txt">Collectes Périodiques </span></a>
                                    </li>
                                    <li>
                                        <a href="@Url.Action("AgiosParClient", "HistoriqueMouvement")"><i class="s16 icomoon-icon-arrow-right-3"></i><span class="txt">Agios Par Client </span></a>
                                    </li>
                                    <li>
                                        <a href="@Url.Action("FicheCommissionsCollecteurs", "HistoriqueMouvement")"><i class="s16 icomoon-icon-arrow-right-3"></i><span class="txt">Fiche de commisisons</span></a>
                                    </li>
                                    <li>
                                        <a href="@Url.Action("CommissionCollectriceAvecGrilleRemuneration", "HistoriqueMouvement")"><i class="s16 icomoon-icon-arrow-right-3"></i><span class="txt">Fiche rémunération</span></a>
                                    </li>
                                    <li>
                                        <a href="@Url.Action("FicheCommissionsParPorteFeuille", "HistoriqueMouvement")" title="Commissions par portefeuille"><i class="s16 icomoon-icon-arrow-right-3"></i><span class="txt">Commis... par portefeuille</span></a>
                                    </li>
                                    <li>
                                        <a href="@Url.Action("FicheCommissionsParPorteFeuilleSimplifiee", "HistoriqueMouvement")" title="Commissions par portefeuille simplifiée"><i class="s16 icomoon-icon-arrow-right-3"></i><span class="txt">Commissions simplifiées</span></a>
                                    </li>
                                    <li>
                                        <a href="@Url.Action("FicheOperationsParPeriode", "HistoriqueMouvement")" title="Historique des opérations"><i class="s16 icomoon-icon-arrow-right-3"></i><span class="txt">Historique opérations</span></a>
                                    </li>
                                    <li>
                                        <a href="@Url.Action("HistoriqueCollecteur", "HistoriqueMouvement")"><i class="s16 icomoon-icon-arrow-right-3"></i><span class="txt">Synthèse Collecteur</span></a>
                                    </li>

                                </ul>

                            </li>

                        End If


                    </ul>
                </div>
                <div Class="sidebar-widget mb0">
                    <h6 Class="title mb0">......</h6>
                </div>
            </div>
            <!-- End sidenav -->



        </div>
        <!-- End .sidebar-scrollarea -->
    </div>
    <!-- End .sidebar-inner -->
</div>

@*End If*@
