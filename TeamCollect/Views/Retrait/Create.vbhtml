@ModelType  RetraitViewModel
@Imports TeamCollect.My.Resources
<div class="contentwrapper">
    <!--Content wrapper-->
    <div class="heading">
        <!--  .heading-->
        <h3 style="color:#353535">
            <i class="fa fa-archive"></i>Retraits
        </h3>
    </div>
    <!-- End  / heading-->
    <!-- Start .row -->
    <div class="row">
        <div class="panel panel-pattern toggle panelMove panelRefresh">
            <!-- Start .panel -->
            <div class="panel-heading">
                <h4 class="panel-title" style="color:#353535"><i class="fa fa-magic"></i> Formulaire d'enregistrement...</h4>
            </div>
            <div class="panel-body pt0 pb0">
                <div id="wizard" class="bwizard">
                    @Using (Html.BeginForm("Create", "Retrait", FormMethod.Post, New With {.role = "form", .id = "__AjaxAntiForgeryForm"}))
                        @Html.AntiForgeryToken()
                        @Html.ValidationSummary(True)
                        @<div class="box box-warning">
                            <div class="box-header with-border">
                                <div class="row">
                                    <div class="col-md-9">
                                        <div class="form-group">
                                            <label>Client</label>

                                            @Html.DropDownListFor(Function(model) model.ClientId,
New SelectList(Model.IDsClient, "Value", "Text"), "Selectionnez le client", New With {.class = "form-control select2"})
                                            @Html.ValidationMessageFor(Function(model) model.ClientId, Nothing, New With {.style = "color: #fdcd23"})
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Montant </label>
                                            @Html.EditorFor(Function(model) model.Montant)
                                            @Html.ValidationMessageFor(Function(model) model.Montant, Nothing, New With {.style = "color: #fdcd23"})
                                        </div>
                                    </div>
                                </div>
                                <div class="row">

                                    <Label id="MessageAlerte" style="color: #fdcd23">  </Label>


                                    @*<div Class="col-md-6">
                                            <div Class="form-group">
                                                <Label> Etat </Label>
                                                <Label style="color: #fdcd23"> @Html.ValidationMessageFor(Function(model) model.Etat) </Label>
                                                @Html.EditorFor(Function(model) model.Etat)
                                            </div>
                                        </div>*@

                                </div>

                                <div class="box-footer" style="text-align:center">
                                    <input type="submit" onclick="Alert();" id="BtnSave" value="Enregistrer" class="btn btn-primary btn-sm" />
                                    @*<input type="button" onclick="DemandeDeRetrait();" value="Enregistrer" class="btn btn-primary btn-sm" />*@
                                    @Html.ActionLink("Retour", "Index", "Retrait", Nothing, New With {.class = "btn btn-default btn-sm"})
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
        //alert("Le niveau sélectionné = " + $(cboPereId).val());
        var cboPereId = '#ClientId';
        var cboFilsId = '';
        var MessageAlerte = 'MessageAlerte';
        var url = '@Url.Action("GetMessage")';
        var MsgCombo = '';
        //Dropdownlist Selectedchange event
        //Dropdownlist Selectedchange event
        $(cboPereId).change(function () {
            //alert("Le niveau sélectionné = " + $(cboPereId).val());
            document.getElementById('MessageAlerte').innerHTML = '';
            document.getElementById(MessageAlerte).style.display = '';

            //document.getElementById(CommuneDiv).style.display = (Niveau == 1) ? '' : 'none';
                $(cboFilsId).empty();
                if ($(cboPereId).val()) {

                    $.ajax({
                        type: 'POST',
                        url: url, // we are calling json method

                        dataType: 'json',
                        data: { ClientId: $(cboPereId).val() },
                        // here we are get value of selected country and passing same value as inputto json method GetStates.

                        success: function (msg) {
                            //$("#MessageAlerte").text(msg.Value);
                            document.getElementById('MessageAlerte').innerHTML = msg;
                        },
                        error: function (ex) {
                            //alert('Failed to retrieve states.' + ex);
                        }
                    });
                } else {
                            //$("#" + MessageAlerte).text(msg.Value);
                };

            return false;
        })
    </script>


    <script>
        function Alert() {
            $.confirm({
                title: 'Information',
                content: 'L\'opération est encours de traitement. Vous serez redirigé vers une autre page à la fin de son exécution.',
                theme: 'dark',
                icon: 'fa fa-info',
                buttons: {
                    buttonA: {
                        text: ' ',
                        action: function () {
                            this.buttons.buttonA.hide();
                            return false;
                        }
                    }
                }
            });
            //document.getElementById('BtnSave').setAttribute("disabled", "disabled");
            document.getElementById('BtnSave').style.display = "none";

        }
    </script>

    <script>
        function DemandeDeRetrait() {
            var ClientId = $('#ClientId').val();
            var Montant = $('#Montant').val();
            document.getElementById('Montant').value = "";

            var MessageAlert = document.getElementById('MessageAlerte').innerHTML;

            var form = $('#__AjaxAntiForgeryForm');
            var token = $('input[name="__RequestVerificationToken"]', form).val();

            var retraitJSON = {
                'ClientId': ClientId,
                'Montant': Montant,
            }

            //$.alert("Identifiant= " + Type);
            $.confirm({
                title: '@Resource.DemandeRetrait',
                content: MessageAlert,
                animationSpeed: 1000,
                animationBounce: 3,
                animation: 'rotatey',
                closeAnimation: 'scaley',
                theme: 'supervan',
                buttons: {
                    Confirmer: function () {
                        $.ajax({
                            url: '@Url.Action("DemandeDeRetrait")',
                            type: 'POST',
                            data: {
                                __RequestVerificationToken: token,
                                ClientId: ClientId,
                                Montant: Montant
                            },
                        }).done(function (data) {
                            if (data.Result == "OK") {
                                //$ctrl.closest('li').remove();
                                $.confirm({
                                    title: '@Resource.SuccessTitle',
                                    content: '@Resource.SuccessRetrait',
                                    animationSpeed: 1000,
                                    animationBounce: 3,
                                    animation: 'rotatey',
                                    closeAnimation: 'scaley',
                                    theme: 'supervan',
                                    buttons: {
                                        OK: function () {
                                            window.location.href = '@Url.Action("Index", "Retrait")';
                                            //window.location.reload();
                                        }
                                    }
                                });
                            }
                            else if (data.Result) {
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
                            content: '@Resource.CancelingRetrait',
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
    </script>
End Section
