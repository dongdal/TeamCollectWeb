@modelType PagedList.IPagedList(Of TypeCarnet)
@Imports PagedList.Mvc
@Imports TeamCollect.My.Resources
<!--Body content-->

<div class="contentwrapper">
    <!--Content wrapper-->
    <div class="heading">
        <!--  .heading-->
        <h3 style="color:#353535">
            <i class="fa fa-archive"></i>Le Journal de caisse
        </h3>
        <div style="float:right; color:black">
            <a href="@Url.Action("Create")" class="btn btn-primary mr5 mb10" style="margin-top: 5px; color:#353535">
                <i class="glyphicon glyphicon-plus mr5"></i> Ajouter un Type de Carnet dans le système
            </a>
        </div>

    </div>
    <!-- End  / heading-->
    <!-- Start .row -->
    <br />
    <div class="row">
        @Using (Html.BeginForm("Index", "TypeCarnet", FormMethod.Get, New With {.class = "form-inline", .role = "form"}))
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
                    <th>Libelle</th>
                    <th>Prix</th>
                    @*<th>Etat</th>*@
                    <th>Date Creation </th>
                    <th style="text-align:right">.....<i class="fa fa-gears"></i></th>
                </tr>
            </thead>

            @For Each item In Model
                @<tr>
    <td>
        @Html.DisplayFor(Function(modelItem) item.Libelle)
    </td>
    <td>
        @Html.DisplayFor(Function(modelItem) item.Prix)
    </td>
    @*<td>
        @Html.DisplayFor(Function(modelItem) item.Etat)
    </td>*@
    <td>
        @Html.DisplayFor(Function(modelItem) item.DateCreation)
    </td>
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
    @*<td>
            @Html.ActionLink("Edit", "Edit", New With {.id = item.Id}) |
            @Html.ActionLink("Details", "Details", New With {.id = item.Id}) |
            @Html.ActionLink("Delete", "Delete", New With {.id = item.Id})
        </td>*@
</tr>
            Next

        </table>

    </div>
    @Html.PagedListPager(Model, Function(page) Url.Action("Index", New With {.page = page, .sortOrder = ViewBag.CurrentSort, .currentFilter = ViewBag.CurrentFilter, .tab = ViewBag.activeTab}))
    Page @IIf(Model.PageCount < Model.PageNumber, 0, Model.PageNumber) sur @Model.PageCount ( @ViewBag.EnregCount Enregistrement(s))


    <!-- /.modal -->
    <!--Style 6 Super scale modal -->
    <div class="modal fade modal-style6" id="modal-style6" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">
                        <span aria-hidden="true">&times;</span><span class="sr-only">Close</span>
                    </button>
                    <h4 class="modal-title" id="mySmallModalLabel">Modal title</h4>
                </div>
                <div class="modal-body">
                    <p>
                        Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo
                        consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id
                        est laborum.
                    </p>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    <button type="button" class="btn btn-primary">Save changes</button>
                </div>
            </div>
        </div>
    </div>
    <!-- /.modal -->





</div>

<script>
    function TblDeroul(MonId) {
            $('#' + MonId).fadeToggle(5);
    };

</script>
