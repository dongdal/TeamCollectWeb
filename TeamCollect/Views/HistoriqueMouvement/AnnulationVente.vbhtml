@ModelType  AnnulationViewModel
@Imports TeamCollect.My.Resources
<div class="contentwrapper">
    <!--Content wrapper-->
    <div class="heading">
        <!--  .heading-->
        <h3 style="color:#353535">
            <i class="fa fa-archive"></i>Annuler une vente de carnet
        </h3>
    </div>
    <!-- End  / heading-->
    <!-- Start .row -->
    <div class="row">
        <div class="panel panel-pattern toggle panelMove panelRefresh">
            <!-- Start .panel -->
            <div class="panel-heading">
                <h4 class="panel-title" style="color:#353535"><i class="fa fa-magic"></i> Formulaire d'Annulation d'une vente de carnet...</h4>
            </div>
            <div class="panel-body pt0 pb0">
                <div id="wizard" class="bwizard">
                    @Using (Html.BeginForm("AnnulationVente", "HistoriqueMouvement", FormMethod.Post, New With {.role = "form", .id = "__AjaxAntiForgeryForm"}))
                        @Html.AntiForgeryToken()
                        @Html.ValidationSummary(True)
                        @Html.HiddenFor(Function(model) model.Id)
                        @Html.HiddenFor(Function(model) model.CollecteurId)
                        @Html.HiddenFor(Function(model) model.DateDebut)
                        @Html.HiddenFor(Function(model) model.DateFin)
                        @<div class="box box-warning">
                            <div class="box-header with-border">
                                <div class="row">

                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label>Saisir le Motif de l'annulation </label>
                                            <label style="color: #fdcd23"> @Html.ValidationMessageFor(Function(model) model.Motif) </label>
                                            @Html.EditorFor(Function(model) model.Motif)
                                            @Html.ValidationMessageFor(Function(model) model.Motif)
                                        </div>
                                    </div>
                                </div>
                                <div class="row">

                                </div>
                                <div class="box-footer" style="text-align:center">
                                    <input type="submit" onclick="Alert();" id="BtnSubmit" value="Enregistrer" class="btn btn-primary btn-sm" />
                                    <a class="btn btn-default btn-sm" data-toggle="tooltip" data-placement="left" title="Retour" href="@Url.Action("Index", "HistoriqueMouvement", New With {.ClientId = Model.Id, .dateDebut = Now.Date.ToString("d"), .dateFin = Now.Date.ToString("d")})">
                                        <i class=""></i> Retour
                                    </a>
                                </div>

                            </div>
                            <br />
                        </div>

                    End Using
                </div>
            </div>
        </div>
    </div>
</div>
@Section Scripts
    @Scripts.Render("~/bundles/jqueryval")



    <script>
        function AnnulerVente() {
            var Id = '#Id';
            var CollecteurId = '#CollecteurId';
            var DateDebut = '#DateDebut';
            var DateFin = '#DateFin';
            var Motif = '#Motif';
            //var MessageAlert = document.getElementById('MessageAlerte').innerHTML;

            var form = $('#__AjaxAntiForgeryForm');
            var token = $('input[name="__RequestVerificationToken"]', form).val();

            //var retraitJSON = {
            //    'ClientId': $(ClientId).val(),
            //    'Montant': $(Montant).val(),
            //}

            //$.alert("Identifiant= " + Type);
            if (typeof $(Id).val() == "undefined" || $(Id).val() == "" || typeof $(CollecteurId).val() == "undefined" || $(CollecteurId).val() == "" || typeof $(DateDebut).val() == "undefined" || $(DateDebut).val() == "" || typeof $(DateFin).val() == "undefined" || $(DateFin).val() == "" || typeof $(Motif).val() == "undefined" || $(Motif).val() == "") {
                alert('"Veuillez renseigner tous les champs obligatoires."');
            } else {
            $.confirm({
                title: '@Resource.AnnulerOperationTitle',
                content: '@Resource.AnnulerOperationBody',
                animationSpeed: 1000,
                animationBounce: 3,
                animation: 'rotatey',
                closeAnimation: 'scaley',
                theme: 'supervan',
                buttons: {
                    Confirmer: function () {
                        $.ajax({
                            url: '@Url.Action("AnnulationRetrait")',
                            type: 'POST',
                            data: {
                                __RequestVerificationToken: token,
                                Id: $(Id).val(),
                                CollecteurId: $(CollecteurId).val(),
                                DateDebut: $(DateDebut).val(),
                                DateFin: $(DateFin).val(),
                                Motif: $(Motif).val()
                            },
                        }).done(function (data) {
                            if (data.Result == "OK") {
                                //$ctrl.closest('li').remove();
                                $.confirm({
                                    title: '@Resource.SuccessTitle',
                                    content: '@Resource.SuccessOperation',
                                    animationSpeed: 1000,
                                    animationBounce: 3,
                                    animation: 'rotatey',
                                    closeAnimation: 'scaley',
                                    theme: 'supervan',
                                    buttons: {
                                        OK: function () {
                                            window.location.href = '@Url.Action("IndexAgence", "Collecteur")';
                                            //window.location.reload();
                                        }
                                    }
                                });
                            }
                            else {
                                //alert(data.Result.Message);
                                $.confirm({
                                    title: '@Resource.ErreurTitle',
                                    content: data.Result,
                                    animationSpeed: 1000,
                                    animationBounce: 3,
                                    animation: 'rotatey',
                                    closeAnimation: 'scaley',
                                    theme: 'supervan',
                                    buttons: {
                                        OK: function () {
                                            @*window.location.href = '@Url.Action("Index", "HistoriqueMouvement")';*@
                                            //window.location.reload();
                                        }
                                    }
                                });
                            }
                        }).fail(function () {
                            @*//$.alert('@Resource.ErrorProcess');*@
                            $.confirm({
                                title: '@Resource.ErreurTitle',
                                content: '@Resource.ErrorProcess',
                                animationSpeed: 1000,
                                animationBounce: 3,
                                animation: 'rotatey',
                                closeAnimation: 'scaley',
                                theme: 'supervan',
                                buttons: {
                                    OK: function () {
                                    }
                                }
                            });
                        })
                    },
                    Annuler: function () {
                        $.confirm({
                            title: '@Resource.CancelingProcess',
                            content: '@Resource.CancelingOperation',
                            animationSpeed: 1000,
                            animationBounce: 3,
                            animation: 'rotatey',
                            closeAnimation: 'scaley',
                            theme: 'supervan',
                            buttons: {
                                OK: function () {
                                }
                            }
                        });
                    }
                }
            });
            }
        }
    </script>

End Section
