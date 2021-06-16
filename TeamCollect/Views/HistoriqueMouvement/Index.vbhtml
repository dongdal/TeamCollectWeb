@modelType PagedList.IPagedList(Of HistoriqueMouvement)
@Imports PagedList.Mvc
@Imports TeamCollect.My.Resources
<!--Body content-->
@If (ViewBag.dateDebut = "" Or ViewBag.dateFin = "") Then

    @<div class="contentwrapper">
        <!--Content wrapper-->
        <!-- End  / heading-->
        <!-- Start .row -->
        <br />
        <div class="row">
            <p>
                <h1 style="color: #fdcd23">
                    OUPS!!! le chemin par lequel vous accedez à cette information n'est pas correct. Veuillez consulter le guide utilisateur
                </h1>
            </p>

        </div>
    </div>
Else

    @<div class="contentwrapper">
        <!--Content wrapper-->
        <div class="heading">
            <!--  .heading-->
            <h3 style="color:#353535">
                <i class="fa fa-archive"></i> Historique associé à la sélection
            </h3>

        </div>
        <!-- End  / heading-->
        <!-- Start .row -->
        <br />
        <div class="row">
            @Using (Html.BeginForm("Index", "HistoriqueMouvement", FormMethod.Get, New With {.class = "form-inline", .role = "form"}))
                @<div class="col-md-12 form-group">
                    <fieldset>
                        <legend style="color:white"> Votre Filtre</legend>
                        <div class="col-md-2">
                            <div class="input-group">
                                <label>Date de début</label>
                                <div class="input-group input-icon">
                                    <span class="input-group-addon"><i class="fa fa-calendar s16"></i></span>
                                    <input type="text" class="form-control" value="@ViewBag.dateDebut.ToString" id="basic-datepicker" name="dateDebut" required />
                                </div>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="input-group">
                                <label>Date de fin</label>
                                <div class="input-group input-icon">
                                    <span class="input-group-addon"><i class="fa fa-calendar s16"></i></span>
                                    <input type="text" class="form-control" value="@ViewBag.dateFin.ToString" id="basic-datepicker2" name="dateFin" required />
                                </div>

                            </div>
                        </div>
                        <div class="col-md-8">
                            <br />
                            <div class="input-group">
                                <span class="input-group-btn">
                                    @If Not (IsNothing(Request.Params("ClientId"))) Then
                                        @<input type="hidden" value="@Request.Params("ClientId").ToString" class=" form-control" id="ClientId" name="ClientId" />
                                    End If
                                    @If Not (IsNothing(Request.Params("CollecteurId"))) Then
                                        @<input type="hidden" value="@Request.Params("CollecteurId").ToString" class="form-control" id="CollecteurId" name="CollecteurId" />
                                    End If
                                    <button class="btn btn-primary" type="submit" style="margin-top:5px"> Visualiser <i class="fa fa-search"></i></button>
                                </span>
                            </div>
                        </div>
                    </fieldset>
                </div>
            End Using
        </div>
        <div class="row">
            <table class="table table-bordered table-striped table-hover dt-responsive table-responsive" cellspacing="0" width="100%">
                <thead>
                    <tr>
                        <th>Opération N°</th>
                        <th>Nom du client</th>
                        <th>Lib Opération</th>
                        <th>Montant</th>
                        <th>Date de l'opération</th>
                        <th>Collecteur</th>
                        <th style="text-align:right">.....<i class="fa fa-gears"></i></th>
                    </tr>
                </thead>
                <tbody>
                    @For Each item In Model
                        @<tr role="row" class="odd parent">
                            <td>@item.Id</td>
                            <td>@item.Client.Nom &nbsp; @item.Client.Prenom</td>
                            <td>@item.LibelleOperation</td>
                            <td>@String.Format("{0:0,0.00}", item.Montant) Fcfa</td>
                            <td>@item.DateOperation</td>
                            @*<td>@ConfigurationManager.AppSettings("CollecteurSystemeId")</td>*@
                            <td>@item.Collecteur.Nom &nbsp; @item.Collecteur.Prenom </td>
                            <td style="text-align:right">
                                @if (item.Extourner = True Or item.JournalCaisse.Etat = 1) Then
                                    'ElseIf (item.DateOperation.Value.Date = Now.Date) Then
                                Else
                                    If (item.LibelleOperation.Contains("CASH-IN") And item.JournalCaisse.Etat = 0) Then
                                        @<a class="btn btn-primary btn-xs right" data-toggle="tooltip" data-placement="left" title="Annuler une collecte" href="@Url.Action("Annulation", New With {.id = item.Id, .CollecteurId = item.CollecteurId, .dateDebut = ViewBag.dateDebut.ToString, .dateFin = ViewBag.dateFin.ToString})">
                                            <i class="fa fa-remove"> Extourner</i>
                                            <span class="sr-only">Annuler</span>
                                        </a>
                                    End If
                                    If (item.LibelleOperation.Contains("RETRAIT") And item.JournalCaisse.Etat = 0) Then
                                        @<a class="btn btn-primary btn-xs right" data-toggle="tooltip" data-placement="left" title="Annuler un retrait" href="@Url.Action("AnnulationRetrait", New With {.id = item.Id, .CollecteurId = item.CollecteurId, .dateDebut = ViewBag.dateDebut.ToString, .dateFin = ViewBag.dateFin.ToString})">
                                            <i class="fa fa-remove"> Extourner</i>
                                            <span class="sr-only">Annuler</span>
                                        </a>
                                    End If
                                    If (item.LibelleOperation.Contains("VENTE") And item.JournalCaisse.Etat = 0) Then
                                        @<a class="btn btn-primary btn-xs right" data-toggle="tooltip" data-placement="left" title="Annuler une vente de carnet" href="@Url.Action("AnnulationVente", New With {.id = item.Id, .CollecteurId = item.CollecteurId, .dateDebut = ViewBag.dateDebut.ToString, .dateFin = ViewBag.dateFin.ToString})">
                                            <i class="fa fa-remove"> Extourner</i>
                                            <span class="sr-only">Annuler</span>
                                        </a>
                                    End If

                                End If


                            </td>
                        </tr>
                    Next
                    <tr role="row" class="odd parent" style="background-color:#eeeeee">
                        <td></td>
                        <td style="color: #304a85; font-size:large">TOTAL</td>
                        <td></td>
                        <td style="background-color: #304a85; font-size: large">@String.Format("{0:0,0.00}", ViewBag.masom.ToString) Fcfa</td>

                        <td style="color: #304a85; font-size:large"></td>
                        <td style="background-color: #304a85; font-size: large"></td>
                    </tr>
                </tbody>
            </table>
        </div>


        @If User.IsInAnyRole("ADMINISTRATEUR,MANAGER") Then
            @<div>
                @Html.ActionLink("Retour à la liste des clients", "IndexAgence", "Client", Nothing, New With {.class = "btn btn-default btn-sm"})
                @Html.ActionLink("Retour à la liste de collecteurs", "Index", "Collecteur", Nothing, New With {.class = "btn btn-default btn-sm"})
            </div>
        End If

        @Html.PagedListPager(Model, Function(page) Url.Action("Index", New With {.page = page, .ClientId = ViewBag.ClientId, .CollecteurId = ViewBag.CollecteurId, .dateDebut = ViewBag.dateDebut, .dateFin = ViewBag.dateFin, .sortOrder = ViewBag.CurrentSort, .currentFilter = ViewBag.CurrentFilter, .tab = ViewBag.activeTab}))
        Page @IIf(Model.PageCount < Model.PageNumber, 0, Model.PageNumber) sur @Model.PageCount ( @ViewBag.EnregCount Enregistrement(s))

    </div>

End If

