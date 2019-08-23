@ModelType IEnumerable(Of Dashbord)
@Imports PagedList.Mvc
@Imports TeamCollect.My.Resources
<!--Body content-->
@Scripts.Render("~/bundles/Highcharts")
<div class="contentwrapper">
    <!--Content wrapper-->
    <div class="heading">
        <!--  .heading-->
        <h3 style="color:#353535">
            <i class="fa fa-dashboard"></i> Tableau de Bord
        </h3>
    </div>
    <!-- End  / heading-->
    <!-- / .row -->
    <div class="row">
        <!-- .row start -->
        <div class="col-md-8">
            <!-- col-md-8 start here -->
            <div class="panel panel-default chart">
                <div class="panel-heading">
                    <h4 class="panel-title">
                        <i class="s16 icomoon-icon-bars"></i>
                        <span>TOP 5 des collecteurs en terme de montant collecté</span>
                    </h4>
                </div>
                <div class="panel-body">
                        @*le premier dahsboard : sur les collecteurs*@
                        <div class="img-responsive" id="container" style="width:680px;"></div>
                            <table id="datatable" style="display:none" >
                                <thead>
                                    <tr>
                                        <th></th>
                                        <th>Montant Collecté</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    @For Each item In Model
                                        For Each item2 In item.StatCollecteurListObject
                                            @<tr>
                                            <th>@item2.Nom @item2.Prenom <br />[@item2.Agence]</th>
                                            <td>@String.Format("{0:0,0.00}", item2.Montant.Replace(",", "."))</td>
                                          </tr>
                                        Next

                                    Next
                                </tbody>
                            </table>
                </div>
            </div>
            <!-- End .panel -->
            <!-- / .row -->
        </div>
        <!-- col-md-8 end here -->
        <div class="col-md-4">
            @*-------classement des agences-----------*@
            <div class="reminder mb25">
                <h4>
                    Classement des Agences
                </h4>
                <ul>
                  @For Each itemag In Model
                      For Each itemag2 In itemag.StatAgenceListObject
                           @<li class="clearfix">
                                <div class="icon" style="font-size:14px">
                                    <span class="s32 icomoon-icon-home-4 color-gray"></span>
                                </div>
                                <span class="number" style="font-size:20px">@itemag2.Agence.ToString</span>
                                <span class="txt" style="font-size:18px;"><b> Montant Collecté: [@String.Format("{0:0,0.00}", itemag2.Montant) Fcfa]</b></span>
                           </li>
                      Next

                  Next
                   
                </ul>
            </div>

            <div class="panel panel-default chart">
                <div class="panel-heading">
                    <h4 class="panel-title">
                        <i class="s16 icomoon-icon-bars"></i>
                        <span>Nombre de collectes lié au Genre</span>
                    </h4>
                </div>
                <div class="panel-body">
                    @*le premier dahsboard : sur les collecteurs*@
                    <div class="img-responsive" id="containerGenre" style="width:310px"></div>
                    <table id="datatableGenre" style="display:none">
                        <thead>
                            <tr>
                                <th></th>
                                <th>Nombre de collecte</th>
                                <th>Nombre de collecte</th>
                            </tr>
                        </thead>
                        <tbody>
                            @For Each itemgenre In Model
                                Dim i = 0
                                For Each itemgenre2 In itemgenre.StatGenreListObject
                                    If (i = 0) Then
                                         @<tr>
                                            
                                            <th>Genre Femin</th>
                                             <td>@itemgenre2.NombreF.ToString</td>
                                         </tr>
                                    Else
                                         @<tr>
                                            <th>Genre Masculin</th>
                                             <td>@itemgenre2.NombreM.ToString</td>
                                         </tr>
                                    End If
                                    i = i + 1
                                Next

                            Next
                        </tbody>
                    </table>
                </div>
            </div>

        </div>
        <!-- col-md-4 end here -->
    </div>
    <!-- / .row -->
</div>
<!-- End contentwrapper -->
<script>
    Highcharts.chart('containerGenre', {
        data: {
            table: 'datatableGenre'
        },
        chart: {
            type: 'pie'
        },
        title: {
            text: ''
        },
        yAxis: {
            allowDecimals: false,
            title: {
                text: 'Nombre de collecte effectuée'
            }
        },
        tooltip: {
            formatter: function () {
                return '<b>' + this.series.name + '</b><br/>' +
                    this.point.y + '<br/>' + this.point.name.toLowerCase();
            }
        }
    });
</script>

<script>
    Highcharts.chart('container', {
        data: {
            table: 'datatable'
        },
        chart: {
            type: 'column'
        },
        title: {
            text: ''
        },
        yAxis: {
            allowDecimals: false,
            title: {
                text: 'Montant Collecté'
            }
        },
        tooltip: {
            formatter: function () {
                return '<b>' + this.series.name + '</b><br/>' +
                    this.point.y + '<br/>' + this.point.name.toLowerCase();
            }
        }
    });
</script>

