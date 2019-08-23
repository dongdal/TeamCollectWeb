@modelType PagedList.IPagedList(Of InfoFrais)
@Imports PagedList.Mvc
@Imports TeamCollect.My.Resources
<!--Body content-->
@If (String.IsNullOrEmpty(ViewBag.GrilleId.ToString)) Then
    @<div class="contentwrapper">
        <br />
        <div class="row">
            <p>
                <h1 style="color: #fdcd23">
                    OUPS!!! le chemin par lequel vous accedez à cette information n'est pas correct. Veuillez consulter le guide utilisateur
                </h1>
            </p>

        </div>
    </div>
Else

    @<div class="contentwrapper">
        <!--Content wrapper-->
        <div class="heading">
            <!--  .heading-->
            <h3 style="color:#353535">
                <i class="fa fa-archive"></i> La facturation: @ViewBag.Libelle
            </h3>
            <div style="float:right; color:black">
                <a href="@Url.Action("Create", New With {.GrilleId = ViewBag.GrilleId})" class="btn btn-primary mr5 mb10" style="margin-top: 5px; color:#353535">
                    <i class="glyphicon glyphicon-plus mr5"></i> Enregistrer une plage dans le système
                </a>
            </div>
        </div>
        <!-- End  / heading-->
        <!-- Start .row -->
        <br />
        <div class="row">
            <table class="table table-bordered table-striped table-hover dt-responsive table-responsive" cellspacing="0" width="100%">
                <thead>
                    <tr>
                        <th>De</th>
                        <th>à</th>
                        <th>Frais</th>
                        <th>Taux</th>
                        <th style="text-align:right">.....<i class="fa fa-gears"></i></th>
                    </tr>
                </thead>
                <tbody>
                    
                    @For Each item In Model
                            Dim currentIntem = New InfoFrais()
                            currentIntem = Model.LastOrDefault
                        @<tr role="row" class="odd parent">
                            <td>@item.BornInf</td>
                            <td>@item.BornSup</td>
                            <td>@item.Frais Fcfa</td>
                            <td>@item.Taux %</td>
                                @If (item.Id = currentIntem.Id) Then
                                    @<td style="text-align:right">
                                         <a class="btn btn-primary btn-xs" data-toggle="tooltip" data-placement="left" title="@Resource.Delete" href="@Url.Action("Delete", New With {.id = item.Id})">
                                               <i class="fa fa-remove"></i>
                                               <span class="sr-only">@Resource.Delete</span>
                                          </a>

                                    </td>
                                End If
                             
                        </tr>
                        
                    Next
                </tbody>
            </table>
        </div>
        @Html.PagedListPager(Model, Function(page) Url.Action("Index", New With {.page = page, .GrilleId = ViewBag.GrilleId, .tab = ViewBag.activeTab}))
        Page @IIf(Model.PageCount < Model.PageNumber, 0, Model.PageNumber) sur @Model.PageCount ( @ViewBag.EnregCount Enregistrement(s))

    </div>

End If

