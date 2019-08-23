@ModelType Grille
@Code
    ViewData("Title") = "Delete"
End Code


<div class="contentwrapper">
    <!--Content wrapper-->
    <div class="heading">
        <!--  .heading-->
        <h3 style="color:#353535">
            <i class="fa fa-archive"></i>Une de vos Grilles: Confirmez-vous cette Suppression?
        </h3>

    </div>
    <!-- End  / heading-->
    <!-- Start .row -->
    <div class="row">
        <dl class="dl-horizontal">
            <dt>
                @Html.DisplayNameFor(Function(model) model.Libelle)
            </dt>

            <dd>
                @Html.DisplayFor(Function(model) model.Libelle)
            </dd>

        </dl>
        @Using (Html.BeginForm())
            @Html.AntiForgeryToken()

            @<div class="box-footer" style="text-align:center">
                <input type="submit" value="Supprimer" class="btn btn-primary btn-sm" />
                @Html.ActionLink("Retour", "Index", Nothing, New With {.class = "btn btn-default btn-sm"})
            </div>
        End Using
    </div>
</div>


