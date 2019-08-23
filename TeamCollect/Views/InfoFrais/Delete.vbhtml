@ModelType InfoFrais
@Code
    ViewData("Title") = "Delete"
End Code


<div class="contentwrapper">
    <!--Content wrapper-->
    <div class="heading">
        <!--  .heading-->
        <h3 style="color:#353535">
            <i class="fa fa-archive"></i>Une de vos Facturation: Confirmez-vous cette Suppression?
        </h3>

    </div>
    <!-- End  / heading-->
    <!-- Start .row -->
    <div class="row">
        <dl class="dl-horizontal">
            <dt>
                @Html.DisplayNameFor(Function(model) model.BornInf)
            </dt>

            <dd>
                @Html.DisplayFor(Function(model) model.BornInf)
            </dd>
            <dt>
                @Html.DisplayNameFor(Function(model) model.BornSup)
            </dt>

            <dd>
                @Html.DisplayFor(Function(model) model.BornSup)
            </dd>
            <dt>
                @Html.DisplayNameFor(Function(model) model.Frais)
            </dt>

            <dd>
                @Html.DisplayFor(Function(model) model.Frais)
            </dd>
            <dt>
                @Html.DisplayNameFor(Function(model) model.Taux)
            </dt>

            <dd>
                @Html.DisplayFor(Function(model) model.Taux)
            </dd>

        </dl>
        @Using (Html.BeginForm())
            @Html.AntiForgeryToken()

            @<div class="box-footer" style="text-align:center">
                <input type="submit" value="Supprimer" class="btn btn-primary btn-sm" />
                 @Html.ActionLink("Retour", "Index", New With {.GrilleId = Model.GrilleId}, New With {.class = "btn btn-default btn-sm"})
               </div>
        End Using
    </div>
</div>


