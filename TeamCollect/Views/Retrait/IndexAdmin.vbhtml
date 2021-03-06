@modelType PagedList.IPagedList(Of Retrait)
@Imports PagedList.Mvc
@Imports TeamCollect.My.Resources
<!--Body content-->

<div class="contentwrapper">
    <!--Content wrapper-->
    <div class="heading">
        <!--  .heading-->
        <h3 style="color:#353535">
            <i class="fa fa-archive"></i> Retrait
        </h3>
        @*<div style="float:right; color:black">
            <a href="@Url.Action("Create")" class="btn btn-primary mr5 mb10" style="margin-top: 5px; color:#353535">
                <i class="glyphicon glyphicon-plus mr5"></i> Nouveau Retrait
            </a>
        </div>*@
    </div>
    <!-- End  / heading-->
    <!-- Start .row -->
    <br />
    <div class="row">
        @Using (Html.BeginForm("Index", "Retrait", FormMethod.Get, New With {.class = "form-inline", .role = "form"}))
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
                    <th>Client</th>
                    <th>Effectuer par</th>
                    <th>Montant</th>
                    <th>Date Retrait</th>
                    <th style="text-align:right">.....<i class="fa fa-gears"></i></th>
                </tr>
            </thead>
            <tbody>

                @For Each item In Model
                    @<tr>
                        <td>
                            @Html.DisplayFor(Function(modelItem) item.Client.Prenom) @Html.DisplayFor(Function(modelItem) item.Client.Nom)
                        </td>
                        <td>
                            @Html.DisplayFor(Function(modelItem) item.User.Personne.Nom)
                        </td>
                        <td>
                            @String.Format("{0:0,0}", item.Montant)
                        </td>

                        <td>
                            @Html.DisplayFor(Function(modelItem) item.DateRetrait)
                        </td>

                        <td style="text-align:right">
                            @if (item.Etat = True And item.DateRetrait.Value.Date = Now.Date) Then
                                @<a class="btn btn-primary btn-xs right" data-toggle="tooltip" data-placement="left" title="Annuler" href="@Url.Action("Annulation", New With {.id = item.Id})">
                                    <i class="fa fa-recycle"></i>
                                    <i class="fa fa-remove"> Extourner</i>
                                </a>
                            End If                           
                        </td>

                        @*<td>
                                @Html.DisplayFor(Function(modelItem) item.Etat)
                            </td>
                            <td>
                                @Html.DisplayFor(Function(modelItem) item.DateCloture)
                            </td>*@
                        @*<td style="text-align:right">
                                <a class="btn btn-primary btn-xs right" data-toggle="tooltip" data-placement="left" title="@Resource.Btn_Edit" href="@Url.Action("Edit", New With {.id = item.Id})">
                                    <i class="fa fa-pencil"></i>
                                    <span class="sr-only">@Resource.Btn_Edit</span>
                                </a>
                                <a class="btn btn-primary btn-xs" data-toggle="tooltip" data-placement="left" title="@Resource.Delete" href="@Url.Action("Delete", New With {.id = item.Id})">
                                    <i class="fa fa-remove"></i>
                                    <span class="sr-only">@Resource.Delete</span>
                                </a>

                            </td>*@
                    </tr>
                Next

        </table>
    </div>
    @Html.PagedListPager(Model, Function(page) Url.Action("IndexAdmin", New With {.page = page, .sortOrder = ViewBag.CurrentSort, .currentFilter = ViewBag.CurrentFilter, .tab = ViewBag.activeTab}))
    Page @IIf(Model.PageCount < Model.PageNumber, 0, Model.PageNumber) sur @Model.PageCount ( @ViewBag.EnregCount Enregistrement(s))

</div>