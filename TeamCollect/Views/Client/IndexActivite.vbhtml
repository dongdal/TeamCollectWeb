@modelType PagedList.IPagedList(Of Client)
@Imports PagedList.Mvc
@Imports TeamCollect.My.Resources
<!--Body content-->

<div class="contentwrapper">
    <!--Content wrapper-->
    <div class="heading">
        <!--  .heading-->
        <h3 style="color:#353535">
            <i class="fa fa-archive"></i>Les Clients de votre Agence
        </h3>
        @*<div style="float:right; color:black">
            <a href="@Url.Action("Create")" class="btn btn-primary mr5 mb10" style="margin-top: 5px; color:#353535">
                <i class="glyphicon glyphicon-plus mr5"></i> Enregistrer un client dans le système
            </a>
        </div>*@

    </div>
    <!-- End  / heading-->
    <!-- Start .row -->
    <br />
    <div class="row">
        @Using (Html.BeginForm("IndexActivite", "Client", FormMethod.Get, New With {.class = "form-inline", .role = "form"}))
            
            @Html.ActionLink("Visualisez la liste des clients inactifs ", "IndexActivite", Nothing, New With {.class = "btn btn-primary"})
            
            @<div style="float:right; padding-bottom:5px">
                <div id="RchId" class="form-group">
                    <div class="col-md-12 form-group">
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
                            <div class="col-md-8">
                                <br />
                                <div class="input-group">
                                    <span class="input-group-btn">
                                        <button id="lnkReport" class="btn btn-primary" type="button" style="margin-top:5px"> Visualiser <i class="fa fa-print"></i></button>
                                    </span>
                                </div>
                            </div>
                        </fieldset>
                    </div>
                </div>
            </div>
        End Using
        <table class="table table-bordered table-striped table-hover dt-responsive table-responsive" cellspacing="0" width="100%">
            <thead>
                <tr>
                    <th style="background-color: #304a85"></th>
                    <th>Numero</th>
                    <th>Nom</th>
                    <th>Prénom </th>
                    <th>Sexe</th>
                    <th>CNI.</th>
                    <th>Tel</th>
                    <th>Quartier</th>
                    <th>Adresse</th>
                    <th>Agence</th>
                    <th>Solde</th>
                    <th style="text-align:right">.....<i class="fa fa-gears"></i></th>
                </tr>
            </thead>
            <tbody>
                @For Each item In Model
                    @<tr role="row" class="odd parent">
                        <td style="text-align:left">
                            <a class="btn btn-default btn-xs right" data-toggle="tooltip" data-placement="left" title="HISTORIQUE" href="@Url.Action("Index", "HistoriqueMouvement", New With {.ClientId = item.Id, .dateDebut = Now.Date.ToString("d"), .dateFin = Now.Date.ToString("d")})">
                                <i class="fa fa-tasks"></i> Historique
                                <span class="sr-only">@Resource.Btn_Edit</span>
                            </a>
                        </td>
                        <td>@item.NumeroCompte</td>
                        <td>@item.Nom</td>
                        <td>@item.Prenom</td>
                        <td>@item.Sexe</td>
                        <td>@item.CNI</td>
                        <td>@item.Telephone</td>
                        <td>@item.Quartier</td>
                        <td>@item.Adresse</td>
                         <td>@item.Agence.Libelle</td>
                        <td>@item.Solde </td>

                        <td style="text-align:right">
                            <a class="btn btn-primary btn-xs right" data-toggle="tooltip" data-placement="left" title="@Resource.Btn_Edit" href="@Url.Action("Edit", New With {.id = item.Id})">
                                <i class="fa fa-pencil"></i>
                                <span class="sr-only">@Resource.Btn_Edit</span>
                            </a>
                            <a class="btn btn-primary btn-xs" data-toggle="tooltip" data-placement="left" title="@Resource.Delete" href="@Url.Action("Delete", New With {.id = item.Id})">
                                <i class="fa fa-remove"></i>
                                <span class="sr-only">@Resource.Delete</span>
                            </a>

                        </td>
                    </tr>
                Next

            </tbody>
        </table>
    </div>
    @Html.PagedListPager(Model, Function(page) Url.Action("IndexAgence", New With {.page = page, .sortOrder = ViewBag.CurrentSort, .currentFilter = ViewBag.CurrentFilter, .tab = ViewBag.activeTab}))
    Page @IIf(Model.PageCount < Model.PageNumber, 0, Model.PageNumber) sur @Model.PageCount ( @ViewBag.EnregCount Enregistrement(s))

</div>

