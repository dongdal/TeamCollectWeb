@modelType PagedList.IPagedList(Of HistoriqueMouvement)
@Imports PagedList.Mvc
@Imports TeamCollect.My.Resources
<!--Body content-->
@If (IsNothing(Model)) Then
    @<div class="contentwrapper">
    <!--Content wrapper-->
    <div class="heading">
        <!--  .heading-->
        <h3 style="color:#353535">
            <i class="fa fa-archive"></i> Exportations des données clients 
        </h3>

    </div>
    <!-- End  / heading-->
    <!-- Start .row -->
    <br />
        <div class="row">
            <p>
                <h1 style="color: #fdcd23">
                    OUPS!!! Il ya au moins une caisse ouverte veuillez la fermer, avant d'avoir un visuel sur l'exportation des données. 
                    La cohérence des données en dépend.
                </h1>
            </p>
           
        </div>
</div>
Else
    @If (Model.Count = 0) Then
         @<div class="contentwrapper">
    <!--Content wrapper-->
    <div class="heading">
        <!--  .heading-->
        <h3 style="color:#353535">
            <i class="fa fa-archive"></i> Exportations des données clients 
        </h3>

    </div>
    <!-- End  / heading-->
    <!-- Start .row -->
    <br />
        <div class="row">
            <p>
                <h1 style="color: #fdcd23">
                   Il n'y a pas de données à exporter du système pour l'instant...
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
            <i class="fa fa-archive"></i> Exportations des données clients à la date du @Now.Date.ToString("d")
        </h3>

    </div>
    <!-- End  / heading-->
    <!-- Start .row -->
    <br />
    <div class="col-md-2">
        @Using (Html.BeginForm("Export", "HistoriqueMouvement", FormMethod.Post, New With {.class = "form-inline", .role = "form"}))
            @<input type="hidden" value="1" class=" form-control" id="TypeExport" name="TypeExport" />
            @<button class="btn btn-default btn-sm" onclick="window.location.reload()" type="submit" style="margin-top:5px"> <i class="fa fa-file-excel-o"></i>Fichier Excel global...</button>
End Using
    </div>
    @*<div class="col-md-2">
        @Using (Html.BeginForm("Export", "HistoriqueMouvement", FormMethod.Post, New With {.class = "form-inline", .role = "form"}))
            @<input type="hidden" value="2" class=" form-control" id="TypeExport" name="TypeExport" />
            @<button class="btn btn-default btn-sm" type="submit" style="margin-top:5px"> <i class="fa fa-file-text-o"></i>Fichier Texte global...</button>
        End Using
    </div>*@
  
        <div class="row">
            <table class="table table-bordered table-striped table-hover dt-responsive table-responsive" cellspacing="0" width="100%">
                <thead>
                    <tr>
                        <th class="per15">Numero du client</th>
                        <th class="per40">Nom du client</th>
                        <th class="per15">Montant total versé</th>
                        <th class="per30" style="text-align:right">
                            Exportation
                        </th>

                    </tr>
                </thead>
                <tbody>
                    @For Each item In Model
                        @<tr role="row" class="odd parent">
                            <td>@item.Client.NumeroCompte</td>
                            <td>@item.Client.Nom &nbsp; @item.Client.Prenom</td>
                            <td>@item.Montant Fcfa</td>
                             <td style="text-align:right">
                                 @If Not (IsNothing(item.Client.NumeroCompte)) Then
                                          @<a class="btn btn-primary btn-xs right" data-toggle="tooltip" data-placement="left" title="Exportez" href="@Url.Action("Export", New With {.ClientId = item.Client.Id, .TypeExport = 1})">
                                              <i class="fa fa-file-excel-o"></i> Fichier Excel du client
                                               <span class="sr-only">@Resource.Btn_Edit</span>
                                            </a>
                                     End If
                                
                                 @*<a class="btn btn-primary btn-xs right" data-toggle="tooltip" data-placement="left" title="Exportez" href="@Url.Action("Export", New With {.ClientId = item.Client.Id, .TypeExport = 2})">
                                     <i class="fa fa-file-text-o"></i> Fichier texte du client
                                     <span class="sr-only">@Resource.Btn_Edit</span>
                                 </a>*@
                             </td>
                        </tr>
                    Next
                </tbody>
            </table>
        </div>
    @Html.PagedListPager(Model, Function(page) Url.Action("Export", New With {.page = page, .ClientId = ViewBag.ClientId, .CollecteurId = ViewBag.CollecteurId, .dateDebut = ViewBag.dateDebut, .dateFin = ViewBag.dateFin, .sortOrder = ViewBag.CurrentSort, .currentFilter = ViewBag.CurrentFilter, .tab = ViewBag.activeTab}))
    Page @IIf(Model.PageCount < Model.PageNumber, 0, Model.PageNumber) sur @Model.PageCount ( @ViewBag.EnregCount Enregistrement(s))

</div>
    End If
End If