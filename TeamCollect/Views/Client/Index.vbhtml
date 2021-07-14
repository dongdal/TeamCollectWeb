﻿@modelType PagedList.IPagedList(Of Client)
@Imports PagedList.Mvc
@Imports TeamCollect.My.Resources
<!--Body content-->

<div class="contentwrapper">
    <!--Content wrapper-->
    <div class="heading">
        <!--  .heading-->
        <h3 style="color:#353535">
            <i class="fa fa-archive"></i>Les Clients de la Structure
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
        @Using (Html.BeginForm("Index", "Client", FormMethod.Get, New With {.class = "form-inline", .role = "form"}))
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
        @Using (Html.BeginForm("Index", "Client", FormMethod.Get, New With {.class = "form-inline", .role = "form"}))
            @<div style="float:left; padding-bottom:5px">
                <div id="RchId" class="form-group">
                    <div class="input-group">
                        <div class="form-group">
                            @Html.DropDownList("AgenceId", New SelectList(ViewBag.lesagences, "Value", "Text"), "[Selectionnez une Agence]--", New With {.class = "form-control select2", .style = "width: 400px;"})
                           </div>
                        <span class="input-group-btn">
                            <button class="btn btn-primary" type="submit">Votre Filtre</button>
                        </span>
                    </div>
                </div>
            </div>
        End Using
        <table class="table table-bordered table-striped table-hover dt-responsive table-responsive" cellspacing="0" width="100%">
            <thead>
                <tr>
                    <th style="background-color: #304a85"></th>
                    <th>Code</th>
                    <th>Nom</th>
                    <th>Prénom </th>
                    <th>Sexe</th>
                    <th>CNI.</th>
                    <th>Tel</th>
                    <th>Quartier</th>
                    <th>Agence</th>
                    <th>Solde</th>
                    <th>Solde Dispo</th>
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
                         <td>@item.Agence.Libelle</td>
                        <td>@item.Solde </td>
                        <td>@item.SoldeDisponible </td>
                    </tr>
                Next

            </tbody>
        </table>
    </div>
    @Html.PagedListPager(Model, Function(page) Url.Action("Index", New With {.page = page, .sortOrder = ViewBag.CurrentSort, .currentFilter = ViewBag.CurrentFilter, .tab = ViewBag.activeTab}))
    Page @IIf(Model.PageCount < Model.PageNumber, 0, Model.PageNumber) sur @Model.PageCount ( @ViewBag.EnregCount Enregistrement(s))

</div>

