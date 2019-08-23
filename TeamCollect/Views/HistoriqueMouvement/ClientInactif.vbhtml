@ModelType ClientViewModel
@Imports TeamCollect.My.Resources

  <div class="contentwrapper">
        <!--Content wrapper-->
        <div class="heading">
            <!--  .heading-->
            <h3 style="color:#353535">
                <i class="fa fa-archive"></i> Liste des Clients Inactifs
            </h3>
        </div>
        <!-- End  / heading-->
        <!-- Start .row -->
        <br />
        <div class="row">
                @Using (Html.BeginForm())
                    @<div class="col-md-12 form-group">
                        <fieldset>
                            <legend style="color:white"> Votre Filtre</legend>
                            <input type="hidden" id="AgenceId" name="AgenceId" value="@ViewBag.UserAgenceId.ToString">
                            <div class="col-md-2">
                                <div class="input-group">
                                    <label>Date de début</label>
                                    <div class="input-group input-icon">
                                        <span class="input-group-addon"><i class="fa fa-calendar s16"></i></span>
                                        <input type="text" class="form-control" value="@ViewBag.dateDebut.ToString" id="basic-datepicker" name="dateDebut" />
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="input-group">
                                    <label>Date de fin</label>
                                    <div class="input-group input-icon">
                                        <span class="input-group-addon"><i class="fa fa-calendar s16"></i></span>
                                        <input type="text" class="form-control" value="@ViewBag.dateFin.ToString" id="basic-datepicker2" name="dateFin" />
                                    </div>

                                </div>
                            </div>
                            <div class="col-md-8">
                                <br />
                                <div class="input-group">
                                    <span class="input-group-btn">
                                        <button id="lnkReport" class="btn btn-primary" type="button" style="margin-top:5px"> Visualiser <i class="fa fa-print"></i></button>
                                    </span>
                                </div>
                            </div>
                        </fieldset>
                    </div>
                    End Using
                        </div>
                      <div Class="row">
                          <div Class="col-md-9">
                              <div Class="embed-responsive embed-responsive-16by9">
                                  <iframe id = "ifrReport" Class="embed-responsive-item"></iframe>
                              </div>
                          </div>
                      </div>
                   </div>


<Script>
                    $('#lnkReport').on('click', function () {
                            var DateDebut = $('#basic-datepicker').val();
                            var DateFin = $('#basic-datepicker2').val();
                            var AgenceId = @ViewBag.UserAgenceId
                                        $('#ifrReport').attr('src', '@Url.Content("~/Report/NewReport.aspx")?DateDebut=' + DateDebut + '&DateFin=' + DateFin + '&AgenceId=' + AgenceId + '&type=LastOperation')
                                    });

</script>
