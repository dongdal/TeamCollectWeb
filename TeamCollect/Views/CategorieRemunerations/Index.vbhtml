@modelType PagedList.IPagedList(Of CategorieRemuneration)
@Imports PagedList.Mvc
@Imports TeamCollect.My.Resources
@Code
    ViewData("Title") = Resource.CategorieRemunerationList
End Code

<div class="contentwrapper">
    <!--Content wrapper-->
    <div class="heading">
        <!--  .heading-->
        <h3 style="color:#353535">
            <i class="fa fa-archive"></i> Retrait
        </h3>
        <div style="float:right; color:black">
            <a href="@Url.Action("Create")" class="btn btn-primary mr5 mb10" style="margin-top: 5px; color:#353535">
                <i class="glyphicon glyphicon-plus mr5"></i> @Resource.CategorieRemunerationCreate
            </a>
        </div>

    </div>
    <!-- End  / heading-->
    <!-- Start .row -->
    <br />
    <div class="row">
        @Using (Html.BeginForm("Index", "CategorieRemunerations", FormMethod.Get, New With {.class = "form-inline", .role = "form"}))
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
        <table class="table table-bordered table-striped table-hover dt-responsive table-responsive" cellspacing="0">
            <thead>
                <tr>
                    <th>@Resource.Libelle</th>
                    <th>@Resource.SalaireDeBase</th>
                    <th>@Resource.CommissionMinimale</th>
                    <th>@Resource.PourcentageCommission</th>
                    <th>@Resource.DateCreation</th>
                    <th style="text-align:right">.....<i class="fa fa-gears"></i></th>
                </tr>
            </thead>
            <tbody>

                @For Each item In Model
                    @<tr>
                        <td>
                            @Html.DisplayFor(Function(modelItem) item.Libelle)
                        </td>
                        <td>
                            @String.Format("{0:0,0.00}", item.SalaireDeBase)
                        </td>
                        <td>
                            @String.Format("{0:0,0.00}", item.CommissionMinimale)
                        </td>
                        <td>
                            @String.Format("{0:0,0.00}", item.PourcentageCommission)
                        </td>

                        <td>
                            @Html.DisplayFor(Function(modelItem) item.DateCreation)
                        </td>

                        <td style="text-align:right">
                            <a class="btn btn-warning btn-xs right" data-toggle="tooltip" data-placement="left" title="@Resource.Btn_Edit" href="@Url.Action("Edit", New With {.id = item.Id})">
                                <i class="fa fa-pencil"></i>
                                <span class="sr-only">@Resource.Btn_Edit</span>
                            </a>

                            <a class="btn btn-info btn-xs right" data-toggle="tooltip" data-placement="left" title="@Resource.BtnDetails" href="@Url.Action("Details", New With {.id = item.Id})">
                                <i class="fa fa-list"></i>
                                <span class="sr-only">@Resource.BtnDetails</span>
                            </a>

                            <a class="btn btn-danger btn-xs right" data-toggle="tooltip" data-placement="left" title="@Resource.BtnDelete" href="@Url.Action("Delete", New With {.id = item.Id})">
                                <i class="fa fa-trash"></i>
                                <span class="sr-only">@Resource.BtnDelete</span>
                            </a>
                        </td>

                    </tr>
                Next

        </table>
    </div>
    @Html.PagedListPager(Model, Function(page) Url.Action("Index", New With {.page = page, .sortOrder = ViewBag.CurrentSort, .currentFilter = ViewBag.CurrentFilter, .tab = ViewBag.activeTab}))
    Page @IIf(Model.PageCount < Model.PageNumber, 0, Model.PageNumber) sur @Model.PageCount ( @ViewBag.EnregCount Enregistrement(s))

</div>
