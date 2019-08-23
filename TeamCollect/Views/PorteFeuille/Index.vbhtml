@ModelType PagedList.IPagedList(Of PorteFeuille)
@Imports PagedList.Mvc
@Imports TeamCollect.My.Resources


<div class="contentwrapper">
    <!--Content wrapper-->
    <div class="heading">
        <!--  .heading-->
        <h3 style="color:#353535">
            <i class="fa fa-archive"></i>Les Portefeuilles
        </h3>
        <div style="float:right">
            <a href="@Url.Action("Create")" class="btn btn-primary mr5 mb10" style="margin-top:5px;">
                <i class="glyphicon glyphicon-plus mr5"></i> Enregistrer un portefeuille dans le système
            </a>
        </div>

    </div>
    <!-- End  / heading-->
    <!-- Start .row -->
    <br />
    <div class="row">
        @Using (Html.BeginForm("Index", "PorteFeuille", FormMethod.Get, New With {.class = "form-inline", .role = "form"}))
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
                    <th>Le Libelle</th>
                    <th>Les Clients</th>
                    <th>Le collecteur</th>
                    <th style="text-align:right">.....<i class="fa fa-gears"></i></th>
                </tr>
            </thead>
            <tbody>
                @For Each item In Model
                    Dim searchString = ViewBag.CurrentFilter
                    @<tr role="row" class="odd parent">
                        <td class="sorting_1">@item.Libelle</td>
                        <td class="sorting_1">
                            @For Each item2 In item.Client
                                Dim NomPrenom = ""
                                If (String.IsNullOrEmpty(item2.Prenom)) Then
                                    NomPrenom = item2.Nom
                                Else
                                    NomPrenom = item2.Nom & " " & item2.Prenom
                                End If
                                @<label>
                                    <i style="color:white" Class="s16 icomoon-icon-arrow-right-3"></i> @NomPrenom

                                    @*@If Not String.IsNullOrEmpty(searchString) Then
                                            If (NomPrenom.ToUpper.Contains(searchString.ToUpper)) Then
                                                @<i style="color:white" Class="s16 icomoon-icon-arrow-right-3"></i> @NomPrenom
                                            Else
                                            End If
                                        Else
                                        End If*@
                                </label>
                                @<br />
                            Next
                        </td>
                        <td class="sorting_1">@item.Collecteur.Nom @item.Collecteur.Prenom</td>
                        <td style="text-align:right">
                            <a class="btn btn-primary btn-xs right" data-toggle="tooltip" data-placement="left" title="@Resource.Btn_Edit" href="@Url.Action("Edit", New With {.id = item.Id})">
                                <i class="fa fa-pencil"></i> Modifier
                                <span class="sr-only">@Resource.Btn_Edit</span>
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
