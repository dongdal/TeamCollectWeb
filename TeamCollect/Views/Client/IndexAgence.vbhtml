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
        <div style="float:right; color:black">
            <a href="@Url.Action("Create")" class="btn btn-primary mr5 mb10" style="margin-top: 5px; color:#353535">
                <i class="glyphicon glyphicon-plus mr5"></i> Enregistrer un client dans le système
            </a>
        </div>

    </div>
    <!-- End  / heading-->
    <!-- Start .row -->
    <br />
    <div class="row">
        @Using (Html.BeginForm("IndexAgence", "Client", FormMethod.Get, New With {.class = "form-inline", .role = "form"}))

            @<div style="float:right; padding-bottom:5px">
                <div id="RchId" class="form-group">
                    <div class="input-group">
                        @Html.TextBox("SearchString", CStr(ViewBag.CurrentFilter), New With {.class = "form-control", .placeholder = Resource.Find, .style = "max-width:100%"})
                        <span class="input-group-btn">
                            <button class="btn btn-primary" type="submit"><i class="fa fa-search"></i></button>
                        </span>
                    </div>
                </div>
            </div>
        End Using
        <table class="table table-bordered table-striped table-hover dt-responsive table-responsive" cellspacing="0" width="100%">
            <thead>
                <tr>
                    <th style="background-color: #304a85"></th>
                    <th>
                        @Html.ActionLink("Code", "IndexAgence", New With {.sortOrder = ViewBag.CodeParm, .currentFilter = ViewBag.CurrentFilter, .tab = ViewBag.activeTab})
                    </th>
                    <th>
                        @Html.ActionLink("Nom", "IndexAgence", New With {.sortOrder = ViewBag.NomParm, .currentFilter = ViewBag.CurrentFilter, .tab = ViewBag.activeTab})
                    </th>
                    <th>
                        @Html.ActionLink("Prénom", "IndexAgence", New With {.sortOrder = ViewBag.PrenomParm, .currentFilter = ViewBag.CurrentFilter, .tab = ViewBag.activeTab})
                    </th>
                    <th>
                        @Html.ActionLink("Sexe", "IndexAgence", New With {.sortOrder = ViewBag.SexeParm, .currentFilter = ViewBag.CurrentFilter, .tab = ViewBag.activeTab})
                    </th>
                    <th>
                        @Html.ActionLink("CNI", "IndexAgence", New With {.sortOrder = ViewBag.CNIParm, .currentFilter = ViewBag.CurrentFilter, .tab = ViewBag.activeTab})
                    </th>
                    <th>
                        @Html.ActionLink("Tel", "IndexAgence", New With {.sortOrder = ViewBag.TelParm, .currentFilter = ViewBag.CurrentFilter, .tab = ViewBag.activeTab})
                    </th>
                    <th>
                        @Html.ActionLink("Quartier", "IndexAgence", New With {.sortOrder = ViewBag.QuartierParm, .currentFilter = ViewBag.CurrentFilter, .tab = ViewBag.activeTab})
                    </th>
                    <th>
                        @Html.ActionLink("Solde", "IndexAgence", New With {.sortOrder = ViewBag.SoldeParm, .currentFilter = ViewBag.CurrentFilter, .tab = ViewBag.activeTab})
                    </th>

                    <th>
                        @Html.ActionLink("Solde Dispo", "IndexAgence", New With {.sortOrder = ViewBag.SoldeParm, .currentFilter = ViewBag.CurrentFilter, .tab = ViewBag.activeTab})
                    </th>
                    <th style="text-align:right">.....<i class="fa fa-gears"></i></th>
                    <th style="text-align:right">.....<i class="fa fa-gears"></i></th>
                </tr>
            </thead>
            <tbody>
                @For Each item In Model
                    @<tr role="row" class="odd parent">
                        <td style="text-align:left">
                            <a class="btn btn-default btn-xs right" data-toggle="tooltip" data-placement="left" title="HISTORIQUE" href="@Url.Action("Index", "HistoriqueMouvement",
New With {.ClientId = item.Id, .dateDebut = Now.Date.ToString(AppSession.DateFormat), .dateFin = Now.Date.ToString(AppSession.DateFormat)})">
                                <i class="fa fa-tasks"></i> Historique
                                <span class="sr-only">@Resource.Btn_Edit</span>
                            </a>
                        </td>
                        <td>@item.CodeSecret</td>
                        <td>@item.Nom</td>
                        <td>@item.Prenom</td>
                        <td>@item.Sexe</td>
                        <td>@item.CNI</td>
                        <td>@item.Telephone</td>
                        <td>@item.Quartier</td>
                        <td>@item.Solde </td>
                        <td>@item.SoldeDisponible </td>
                        @If (item.Etat = True) Then
                            @<td style="background-color: green">

                                @Using (Html.BeginForm("EditActivite", "Client", FormMethod.Get, New With {.class = "form-inline", .role = "form"}))

                                    @<input type="hidden" id="Etat" name="Etat" value="1">
                                    @<input type="hidden" id="ClientId" name="ClientId" value="@item.Id">
                                    @<button class="btn btn-default" type="submit"> Désactiver</button>
                                End Using
                            </td>
                        Else
                            @<td style="background-color: red">
                                @Using (Html.BeginForm("EditActivite", "Client", FormMethod.Get, New With {.class = "form-inline", .role = "form", .Etat = 1, .ClientId = item.Id}))

                                    @<input type="hidden" id="Etat" name="Etat" value="0">
                                    @<input type="hidden" id="ClientId" name="ClientId" value="@item.Id">
                                    @<button class="btn btn-default" type="submit"> Activer</button>
                                End Using
                            </td>

                        End If

                        <td style="background-color: blue">

                            @Using (Html.BeginForm("Transfert", "Client", FormMethod.Get, New With {.class = "form-inline", .role = "form"}))

                                @<input type="hidden" id="ClientId" name="ClientId" value="@item.Id">
                                @<button class="btn btn-default" type="submit"> Transférer</button>
                            End Using
                        </td>
                        <td style="text-align:right">
                            <a class="btn btn-primary btn-xs right" data-toggle="tooltip" data-placement="left" title="@Resource.Btn_Edit" href="@Url.Action("Edit", New With {.id = item.Id})">
                                <i class="fa fa-pencil"></i>
                                <span class="sr-only">@Resource.Btn_Edit</span>
                            </a>
                            @*<a class="btn btn-primary btn-xs" data-toggle="tooltip" data-placement="left" title="@Resource.Delete" href="@Url.Action("Delete", New With {.id = item.Id})">
                                    <i class="fa fa-remove"></i>
                                    <span class="sr-only">@Resource.Delete</span>
                                </a>*@

                        </td>
                    </tr>
                Next

            </tbody>
        </table>
    </div>
    @Html.PagedListPager(Model, Function(page) Url.Action("IndexAgence", New With {.page = page, .sortOrder = ViewBag.CurrentSort, .currentFilter = ViewBag.CurrentFilter, .tab = ViewBag.activeTab}))
    Page @IIf(Model.PageCount < Model.PageNumber, 0, Model.PageNumber) sur @Model.PageCount ( @ViewBag.EnregCount Enregistrement(s))

</div>

