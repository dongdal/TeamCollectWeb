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
        <div style="float:right; color:black">
            <a href="@Url.Action("Create")" class="btn btn-primary mr5 mb10" style="margin-top: 5px; color:#353535">
                <i class="glyphicon glyphicon-plus mr5"></i> Nouveau Retrait
            </a>
        </div>

    </div>
    <!-- End  / heading-->
    <!-- Start .row -->
    <br />
    <div class="row">
        @Using (Html.BeginForm("Index", "CarnetClient", FormMethod.Get, New With {.class = "form-inline", .role = "form"}))
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
                    <th>Etat</th>
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
                            @String.Format("{0:0,0.00}", item.Montant)
                        </td>

                        <td>
                            @Html.DisplayFor(Function(modelItem) item.DateRetrait)
                        </td>

                        <td>
                            @IIf(item.Etat, "Non Extournée", "Extournée")
                        </td>
                    </tr>
                Next

        </table>
    </div>
    @Html.PagedListPager(Model, Function(page) Url.Action("Index", New With {.page = page, .sortOrder = ViewBag.CurrentSort, .currentFilter = ViewBag.CurrentFilter, .tab = ViewBag.activeTab}))
    Page @IIf(Model.PageCount < Model.PageNumber, 0, Model.PageNumber) sur @Model.PageCount ( @ViewBag.EnregCount Enregistrement(s))

</div>