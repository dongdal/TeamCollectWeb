@modelType PagedList.IPagedList(Of CompteurImpression)
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
        @Using (Html.BeginForm("Index", "CompteurImpression", FormMethod.Get, New With {.class = "form-inline", .role = "form"}))
            @<div class="col-md-12 form-group">
                <fieldset>
                    <legend style="color:white"> Votre Filtre</legend>
                    <div class="col-md-2">
                        <div class="input-group">
                            <label>Date de début</label>
                            <div class="input-group input-icon">
                                <span class="input-group-addon"><i class="fa fa-calendar s16"></i></span>
                                <input type="text" class="form-control" value="@ViewBag.dateDebut.ToString" id="basic-datepicker" name="dateDebut" />
                            </div>
                        </div>
                    </div>
                    <div class="col-md-2">
                        <div class="input-group">
                            <label>Date de fin</label>
                            <div class="input-group input-icon">
                                <span class="input-group-addon"><i class="fa fa-calendar s16"></i></span>
                                <input type="text" class="form-control" value="@ViewBag.dateFin.ToString" id="basic-datepicker2" name="dateFin" />
                            </div>

                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>Les Collectrices </label>
                            @Html.DropDownList("CollecteurId", New SelectList(ViewBag.CollectriceList, "Value", "Text"), "Selectionnez un Collecteur", New With {.class = "form-control  select2", .style = "width: 100%;"})
                        </div>
                    </div>
                    <div class="col-md-2">
                        <br />
                        <div class="input-group">
                            <span class="input-group-btn">
                                <button class="btn btn-primary" type="submit" style="margin-top:5px"> Visualiser <i class="fa fa-search"></i></button>
                            </span>
                        </div>
                    </div>
                    <div class="col-md-2">
                        <br />
                        <div class="input-group">
                            <span class="input-group-btn">
                                @Html.ActionLink("Retour à la page d'accueil", "Index", "Home", Nothing, New With {.class = "btn btn-primary ", .style = "margin-top:5px"})
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
                    <th>Numero Opération</th>
                    <th>Client</th>
                    <th>Libelle Opération</th>
                    <th>Montant Opération</th>
                    <th>Collectrice</th>
                    <th>Date de l'impression</th>
                    <th style="text-align:right">.....<i class="fa fa-gears"></i></th>
                </tr>
            </thead>
            <tbody>
                @For Each item In Model
                    @<tr role="row" class="odd parent">
                        <td>@item.HistoriqueMouvementId</td>
                        <td>@item.HistoriqueMouvement.Client.Nom &nbsp; @item.HistoriqueMouvement.Client.Prenom</td>
                        <td>@item.HistoriqueMouvement.LibelleOperation</td>
                        <td>@String.Format("{0:#,#.00#######}", item.HistoriqueMouvement.Montant) Fcfa</td>
                        <td>@item.Collectrice.Personne.Nom &nbsp; @item.Collectrice.Personne.Prenom </td>
                        <td>@item.DatePremiereImpression</td>
                    </tr>
                Next

            </tbody>
        </table>
    </div>


    @Html.PagedListPager(Model, Function(page) Url.Action("Index", New With {.page = page, .ClientId = ViewBag.ClientId, .CollecteurId = ViewBag.CollecteurId, .dateDebut = ViewBag.dateDebut, .dateFin = ViewBag.dateFin, .sortOrder = ViewBag.CurrentSort, .currentFilter = ViewBag.CurrentFilter, .tab = ViewBag.activeTab}))
    Page @IIf(Model.PageCount < Model.PageNumber, 0, Model.PageNumber) sur @Model.PageCount ( @ViewBag.EnregCount Enregistrement(s))
    <br />

</div>

End If

