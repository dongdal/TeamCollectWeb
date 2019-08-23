@ModelType PagedList.IPagedList(Of ApplicationUser)
@Imports PagedList.Mvc
@Imports TeamCollect.My.Resources


<div class="contentwrapper">
    <!--Content wrapper-->
    <div class="heading">
        <!--  .heading-->
        <h3 style="color:#353535">
            <i class="fa fa-archive"></i>Les Comptes Utilisateurs
        </h3>
        <div style="float:right">
            <a href="@Url.Action("Register")" class="btn btn-primary mr5 mb10" style="margin-top:5px;">
                <i class="glyphicon glyphicon-plus mr5"></i> Enregistrer un compte dans le système
            </a>
        </div>

    </div>
    <!-- End  / heading-->
    <!-- Start .row -->
    <br />
    <div class="row">
        @Using (Html.BeginForm("Index", "Account", FormMethod.Get, New With {.class = "form-inline", .role = "form"}))
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
                    <th>Login</th>
                    <th>Profil(s) de l'utilisateur</th>
                    <th>Nom</th>
                    <th>Prenom</th>
                    <th style="text-align:right">.....<i class="fa fa-gears"></i></th>
                </tr>
            </thead>
            <tbody>
                @For Each item In Model
                    @<tr role="row" class="odd parent">
                        <td class="sorting_1">@item.UserName</td>
                        <td class="sorting_1">
                            @For Each item2 In item.Roles
                                @<label>
                                    <i style="color:white" class="s16 icomoon-icon-arrow-right-3"></i> @item2.Role.Name.ToUpper
                                </label>
                            Next
                        </td>
                        <td class="sorting_1">@item.Personne.Nom</td>
                        <td class="sorting_1">@item.Personne.Prenom</td>
                        <td style="text-align:right">
                            <a class="btn btn-primary btn-xs right" data-toggle="tooltip" data-placement="left" title="@Resource.Btn_Edit" href="@Url.Action("Edit", New With {.id = item.Id})">
                                <i class="fa fa-pencil"></i> Modifier
                                <span class="sr-only">@Resource.Btn_Edit</span>
                            </a>
                            <a class="btn btn-success btn-xs" data-toggle="tooltip" data-placement="left" title="Profil" href="@Url.Action("UserRoles", New With {.id = item.Id}) ">
                                <i class="fa fa-list-alt"></i> Profil
                                <span class="sr-only">Profil</span>
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
