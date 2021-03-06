@modelType PagedList.IPagedList(Of Collecteur)
@Imports PagedList.Mvc
@Imports TeamCollect.My.Resources
<!--Body content-->

<div class="contentwrapper">
    <!--Content wrapper-->
    <div class="heading">
        <!--  .heading-->
        <h3 style="color:#353535">
            <i class="fa fa-archive"></i>Les Collecteurs de votre structure
        </h3>

    </div>
    <!-- End  / heading-->
    <!-- Start .row -->
    <br />
    <div class="row">
        @Using (Html.BeginForm("Index", "Collecteur", FormMethod.Get, New With {.class = "form-inline", .role = "form"}))
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
        @Using (Html.BeginForm("Index", "Collecteur", FormMethod.Get, New With {.class = "form-inline", .role = "form"}))
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
        <table class="table table-bordered table-striped table-hover dt-responsive  table-responsive" cellspacing="0" width="100%">
            <thead>
                <tr>
                    <th style="background-color: #304a85"></th>
                    <th>@Resource.CategorieRemunerationLibelle</th>
                    <th>Nom</th>
                    <th>Prénom </th>
                    <th>Sexe</th>
                    <th>CNI.</th>
                    <th>Tel</th>
                    @*<th>Quartier</th>*@
                    <th>Adresse</th>
                    <th>Adresse Mac</th>
                    <th>Agence</th>
                    <th></th>
                </tr>
            </thead>
            <tbody>
                @For Each item In Model
                    @<tr role="row" class="odd parent">
                        <td style="text-align:left">
                            <a class="btn btn-default btn-xs right" data-toggle="tooltip" data-placement="left" title="HISTORIQUE" href="@Url.Action("Index", "HistoriqueMouvement", New With {.CollecteurId = item.Id, .dateDebut = Now.Date.ToString("d"), .dateFin = Now.Date.ToString("d")})">
                                <i class="fa fa-tasks"></i> Historique
                                <span class="sr-only">@Resource.Btn_Edit</span>
                            </a>
                        </td>
                        <td>@item.CategorieRemuneration.Libelle</td>
                        <td>@item.Nom</td>
                        <td>@item.Prenom</td>
                        <td>@item.Sexe</td>
                        <td>@item.CNI</td>
                        <td>@item.Telephone</td>
                        @*<td>@item.Quartier</td>*@
                        <td>@item.Adresse</td>
                        <td>@item.AdrMac</td>
                        <td>@item.Agence.Libelle</td>
                        <td>
                            @If User.IsInAnyRole("MANAGER") Then
                                @<a class="btn btn-primary btn-xs right" data-toggle="tooltip" data-placement="left" title="@Resource.Btn_Edit" href="@Url.Action("Edit", New With {.id = item.Id})">
                                    <i class="fa fa-pencil"></i>
                                    <span class="sr-only">@Resource.Btn_Edit</span>
                                </a>
                            End If
                        </td>
                    </tr>
                Next

            </tbody>
        </table>
    </div>
    @Html.PagedListPager(Model, Function(page) Url.Action("Index", New With {.page = page, .sortOrder = ViewBag.CurrentSort, .currentFilter = ViewBag.CurrentFilter, .tab = ViewBag.activeTab}))
    Page @IIf(Model.PageCount < Model.PageNumber, 0, Model.PageNumber) sur @Model.PageCount ( @ViewBag.EnregCount Enregistrement(s))

</div>

