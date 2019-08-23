@ModelType TeamCollect.SecteurActivite
@Code
    ViewData("Title") = "Delete"
End Code

<div class="contentwrapper">
    <!--Content wrapper-->
    <div class="heading">
        <!--  .heading-->
        <h3 style="color:#353535">
            <i class="fa fa-archive"></i>Voulez-vous vraiment supprimer cet élément?
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
            <br />

            <dt>
                @Html.DisplayNameFor(Function(model) model.Etat)
            </dt>

            <dd>
                @Html.DisplayFor(Function(model) model.Etat)
            </dd>
            <br />

            <dt>
                @Html.DisplayNameFor(Function(model) model.DateCreation)
            </dt>

            <dd>
                @Html.DisplayFor(Function(model) model.DateCreation)
            </dd>
            <br />


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