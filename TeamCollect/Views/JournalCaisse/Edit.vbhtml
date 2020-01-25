@ModelType  JournalCaisseViewModel
@Imports TeamCollect.My.Resources

<div class="contentwrapper">
    <!--Content wrapper-->
    <div class="heading">
        <!--  .heading-->
        <h3 style="color:#353535">
            <i class="fa fa-archive"></i>Fermeture de caisse
        </h3>
    </div>
    <!-- End  / heading-->
    <!-- Start .row -->
    <div class="row">
        <div class="panel panel-pattern toggle panelMove panelRefresh">
            <!-- Start .panel -->
            <div class="panel-heading">
                <h4 class="panel-title" style="color:#353535"><i class="fa fa-magic"></i> Formulaire de fermeture...</h4>
            </div>
            <div class="panel-body pt0 pb0">
                <div id="wizard" class="bwizard">
                    @Using (Html.BeginForm("Edit", "JournalCaisse", FormMethod.Post, New With {.role = "form", .id = "__AjaxAntiForgeryForm"}))
                        @Html.AntiForgeryToken()
                        @Html.ValidationSummary(True)
                        @Html.HiddenFor(Function(model) model.Id)
                        @Html.HiddenFor(Function(model) model.UserId)
                        @Html.HiddenFor(Function(model) model.Etat)
                        @Html.HiddenFor(Function(model) model.DateCreation)
                        @Html.HiddenFor(Function(model) model.DateOuverture)
                        @Html.HiddenFor(Function(model) model.CollecteurId)
                        @Html.HiddenFor(Function(model) model.FondCaisse)
                        @Html.HiddenFor(Function(model) model.PlafondDebat)
                        @Html.HiddenFor(Function(model) model.PlafondEnCours)

                        @<div class="box box-warning">
                            <div class="box-header with-border">
                                <div class="row">
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>le Collecteur </label><br />
                                            <label style="font-size: 20px"> @Model.IDsCollecteur.First.Text.ToUpper </label>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>Montant Réel en caisse</label>
                                            <label style="color: #fdcd23"> @Html.ValidationMessageFor(Function(model) model.MontantReel) </label>
                                            @Html.EditorFor(Function(model) model.MontantReel)
                                        </div>
                                    </div>
                                </div>


                                <div class="box-footer" style="text-align:center">
                                    <input type="button" onclick="CloturerCaisse();" value="Clôturer la caisse" class="btn btn-primary btn-sm" />
                                    @Html.ActionLink("Retour", "Index", "JournalCaisse", Nothing, New With {.class = "btn btn-default btn-sm"})
                                </div>
                                <br />
                            </div>
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
        function CloturerCaisse() {
            var Id = '#Id';
            var Etat = '#Etat';
            var DateCreation = '#DateCreation';
            var DateOuverture = '#DateOuverture';
            var CollecteurId = '#CollecteurId';
            var FondCaisse = '#FondCaisse';
            var PlafondDebat = '#PlafondDebat';
            var PlafondEnCours = '#PlafondEnCours';
            var MontantReel = '#MontantReel';
            var UserId = '#UserId';
            var Etat = '#Etat';

            var form = $('#__AjaxAntiForgeryForm');
            var token = $('input[name="__RequestVerificationToken"]', form).val();

            if (typeof $(FondCaisse).val() == "undefined" || $(FondCaisse).val() == "" ||typeof $(Id).val() == "undefined" || $(Id).val() == "" || typeof $(CollecteurId).val() == "undefined" || $(CollecteurId).val() == "" || typeof $(Etat).val() == "undefined" || $(Etat).val() == "" || typeof $(DateCreation).val() == "undefined" || $(DateCreation).val() == "" || typeof $(DateOuverture).val() == "undefined" || $(DateOuverture).val() == "" || typeof $(PlafondDebat).val() == "undefined" || $(PlafondDebat).val() == "" || typeof $(PlafondEnCours).val() == "undefined" || $(PlafondEnCours).val() == "" || typeof $(MontantReel).val() == "undefined" || $(MontantReel).val() == "") {
                $.alert('"Veuillez renseigner tous les champs obligatoires."');
            } else {
            $.confirm({
                title: '@Resource.CloturerCaisse',
                content: '@Resource.CloturerCaisseBody',
                animationSpeed: 1000,
                animationBounce: 3,
                animation: 'rotatey',
                closeAnimation: 'scaley',
                theme: 'supervan',
                buttons: {
                    Confirmer: function () {
                        $.ajax({
                            url: '@Url.Action("CloturerCaisse")',
                            type: 'POST',
                            data: {
                                Id: $(Id).val(),
                                CollecteurId: $(CollecteurId).val(),
                                DateCreation: $(DateCreation).val(),
                                FondCaisse: $(FondCaisse).val(),
                                PlafondDebat: $(PlafondDebat).val(),
                                PlafondEnCours: $(PlafondEnCours).val(),
                                MontantReel: $(MontantReel).val(),
                                DateOuverture: $(DateOuverture).val(),
                                UserId: $(UserId).val(),
                                Etat: $(Etat).val()
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
                                            window.location.href = '@Url.Action("Index", "JournalCaisse")';
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