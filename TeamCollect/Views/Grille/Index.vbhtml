@modelType PagedList.IPagedList(Of Grille)
@Imports PagedList.Mvc
@Imports TeamCollect.My.Resources
<!--Body content-->

<div class="contentwrapper">
    <!--Content wrapper-->
    <div class="heading">
        <!--  .heading-->
        <h3 style="color:#353535">
            <i class="fa fa-archive"></i>Vos Grilles de facturation
        </h3>
        <div style="float:right; color:black">
            <a href="@Url.Action("Create")" class="btn btn-primary mr5 mb10" style="margin-top: 5px; color:#353535">
                <i class="glyphicon glyphicon-plus mr5"></i> Enregistrer une grille dans le système
            </a>
        </div>

    </div>
    <!-- End  / heading-->
    <!-- Start .row -->
    <br />
    <div class="row">
        @Using (Html.BeginForm("Index", "Grille", FormMethod.Get, New With {.class = "form-inline", .role = "form"}))
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
                    <th class="" style="background-color: #304a85"></th>
                    <th>Libelle</th>
                    <th style="text-align:right">.....<i class="fa fa-gears"></i></th>
                </tr>
            </thead>
            <tbody>
                @For Each item In Model
                    Dim libelle = item.Libelle
                    @<tr role="row" class="odd parent">
                         <td style="text-align:left">
                             <a class="btn btn-default btn-xs right" data-toggle="tooltip" data-placement="left" title="Paramétrage de la grille" href="@Url.Action("Index", "InfoFrais", New With {.GrilleId = item.Id, .Libelle = libelle})">
                                 <i class="fa fa-tasks"></i> Paramétrage de la grille
                                 <span class="sr-only">@Resource.Btn_Edit</span>
                             </a>
                         </td>
                        <td>@item.Libelle</td>

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
    @Html.PagedListPager(Model, Function(page) Url.Action("Index", New With {.page = page, .sortOrder = ViewBag.CurrentSort, .currentFilter = ViewBag.CurrentFilter, .tab = ViewBag.activeTab}))
    Page @IIf(Model.PageCount < Model.PageNumber, 0, Model.PageNumber) sur @Model.PageCount ( @ViewBag.EnregCount Enregistrement(s))

</div>

