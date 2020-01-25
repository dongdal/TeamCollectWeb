@modelType PagedList.IPagedList(Of JournalCaisse)
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
                <i class="glyphicon glyphicon-plus mr5"></i> Ouvrir une caisse dans le système
            </a>
        </div>

    </div>
    <!-- End  / heading-->
    <!-- Start .row -->
    <br />
    <div class="row">
        @Using (Html.BeginForm("Index", "JournalCaisse", FormMethod.Get, New With {.class = "form-inline", .role = "form"}))
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
        <p Class="text-danger">@ViewBag.Message</p>
        <table class="table table-bordered table-striped table-hover dt-responsive table-responsive" cellspacing="0" width="100%">
            <thead>
                <tr>
                    <th>Collecteur</th>
                    <th>Fond de caisse </th>
                    <th>Date ouverture</th>
                    <th>Planfond Consommé </th>
                    <th>Montant Théo.</th>
                    <th>Montant Réel</th>
                    <th>Date fermeture</th>
                    <th>Etat</th>
                    <th>Situation de la caisse à la fermeture</th>
                    <th>Progression(%) de la compensation</th>
                </tr>
            </thead>
            <tbody>
                @Code Dim i = 0 End Code

                @For Each item In Model

                    Dim PlafondEncours As Decimal = item.PlafondEnCours
                    Dim PlafondDeDebat As Decimal = item.PlafondDeDebat
                    Dim Progression = Replace((Math.Round((PlafondEncours / IIf(PlafondDeDebat = 0, 1, PlafondDeDebat)), 3) * 100), ",", ".")

                    Dim manquant As Decimal = 0.0
                    Dim SumVersement As Decimal = 0.0
                    Dim Reste As Decimal = 0

                    @<tr role="row" class="odd parent">
                        <td>@item.Collecteur.Nom &nbsp; @item.Collecteur.Prenom </td>
                        <td>@item.FondCaisse</td>
                        <td>@item.DateOuverture.Value.ToString("d")</td>
                        <td style="background-color: white">
                            <label style="color: #304a85">@item.PlafondEnCours Fcfa</label>
                            <div class="progress progress-striped">
                                <div class="progress-bar progress-bar-warning" role="progressbar" aria-valuenow="@Progression" aria-valuemin="0" aria-valuemax="100" style="width:@Progression%;"> &nbsp;%@Progression </div>
                            </div>
                        </td>
                        <td>@item.MontantTheorique</td>
                        <td>@item.MontantReel</td>
                        @If (IsNothing(item.DateCloture)) Then
                            @<td></td>
                            @<td>
                                <button type="button" class="btn btn-success btn-xs btn-round ">.</button>
                            </td>
                            @<td style="text-align:right">
                                <a class="btn btn-danger btn-xs right" data-toggle="tooltip" data-placement="left" title="@Resource.Btn_Edit" href="@Url.Action("Edit", New With {.id = item.Id})">
                                    <i class="fa fa-lock"></i> Cloturer
                                    <span class="sr-only">@Resource.Btn_Edit</span>
                                </a>

                            </td>

                        Else

                            @<td>@item.DateCloture.Value.ToString("d")</td>
                            @<td>
                                <button type="button" class="btn btn-danger btn-xs btn-round">.</button>
                            </td>
                            If ((item.MontantTheorique - item.MontantReel) > 0) Then

                                manquant = (item.MontantTheorique - item.MontantReel)
                                For Each itemCompensation In item.infoCompensation
                                    SumVersement = (SumVersement + itemCompensation.MontantVerse)
                                Next
                                Dim Pourcentage = Replace((Math.Round((SumVersement / manquant), 3) * 100), ",", ".")
                                Reste = (manquant - SumVersement)
                                @<td style="background-color: red">
                                    Manquant : @Math.Abs(manquant) Fcfa
                                </td>
                                @<td style="background-color: white">
                                    <div class="progress progress-striped">
                                        <div class="progress-bar progress-bar-info" role="progressbar" aria-valuenow="@Pourcentage" aria-valuemin="0" aria-valuemax="100" style="width:@Pourcentage%;"> &nbsp;%@Pourcentage </div>
                                    </div>
                                </td>
                                @<td>
                                    <button type="button" class="btn btn-default btn-sm btn-round" onclick="TblDeroul('@i');"> Info</button>

                                </td>
                            Else
                                If ((item.MontantTheorique - item.MontantReel) < 0) Then

                                    manquant = item.MontantTheorique - item.MontantReel

                                    @<td style="background-color: green">
                                        Superflu : @Math.Abs(manquant) Fcfa
                                    </td>
                                Else


                                End If
                            End If


                        End If


                    </tr>
                    @<tr id="@i" class="child" style="display:none; border:1px solid black;">
                        <td class="child" colspan="9" style="width: 650px;">
                            <div class="col-md-8">
                                <legend class="scheduler-border" style="color:white"> Listing des versements pour la compensation du manquant:</legend>

                                <table class="table table-bordered table-striped table-hover dt-responsive table-responsive" cellspacing="0" width="100%">
                                    <thead>
                                        <tr>
                                            <th>Libelle</th>
                                            <th>Montant versé</th>
                                            <th>Date de Versement</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        @For Each itemCompensation In item.infoCompensation
                                            @<tr role="row" class="odd parent" style="background-color: #353535">
                                                <td>@itemCompensation.Libelle &nbsp; </td>
                                                <td>@itemCompensation.MontantVerse</td>
                                                <td>@itemCompensation.DateCreation.ToString("d")</td>
                                            </tr>
                                        Next
                                        <tr role="row" class="odd parent" style="background-color: #cccccc; font-size:16px; color:black">
                                            <td style="float:right">Reste à Payer:</td>
                                            <td> @Reste Fcfa</td>
                                            <td></td>
                                        </tr>
                                    </tbody>
                                </table>

                            </div>
                            @If (Reste > 0) Then
                                @<div class="col-md-4">

                                    @Using (Html.BeginForm("Create", "InfoCompensation", FormMethod.Post, New With {.role = "form"}))
                                        @Html.AntiForgeryToken()
                                        @Html.ValidationSummary(True)
                                        @<input type="hidden" class="form-control" id="JournalCaisseId" name="JournalCaisseId" value="@item.Id" />

                                        @<fieldset class="scheduler-border">
                                            <legend class="scheduler-border" style="color:white"> Formulaire de versement:</legend>
                                            <div class="control-group">
                                                <div class="col-md-12">
                                                    <label>Libelle du versement :</label>
                                                    <div>
                                                        <input type="text" class="form-control" id="Libelle" name="Libelle" required />
                                                        <br />
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="control-group">
                                                <div class="col-md-8">
                                                    <label> Montant du versement :</label>
                                                    <div>
                                                        <input type="number" class=" form-control" id="MontantVerse" name="MontantVerse" required />
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <br />
                                                    <button type="submit" class="btn btn-default mr5 mb10">Valider</button>
                                                </div>
                                            </div>

                                        </fieldset>
                                    End Using

                                </div>
                            Else
                                @<div class="col-md-4" style="text-align:center">
                                    <br /><br /><br />
                                    <i style="color:white; font-size:50px" class="icomoon-icon-bubble-check"></i>
                                    <button type="button" class="btn btn-default"> La Totalité du montant a été compensé...</button>

                                </div>

                            End If

                        </td>
                    </tr>
                    @Code
                        i = i + 1
                    End Code
                Next

            </tbody>
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
