@ModelType Client
@Code
    ViewData("Title") = "Delete"
End Code


<div class="contentwrapper">
<!--Content wrapper-->
<div class="heading">
    <!--  .heading-->
    <h3 style="color:#353535">
        <i class="fa fa-archive"></i>Le client: Confirmez-vous cette Suppression?
    </h3>

</div>
<!-- End  / heading-->
<!-- Start .row -->
<div class="row">
        <dl class="dl-horizontal">
            <dt>
                @Html.DisplayNameFor(Function(model) model.Nom)
            </dt>

            <dd>
                @Html.DisplayFor(Function(model) model.Nom)
            </dd>

            <dt>
                @Html.DisplayNameFor(Function(model) model.Prenom)
            </dt>

            <dd>
                @Html.DisplayFor(Function(model) model.Prenom)
            </dd>

            <dt>
                @Html.DisplayNameFor(Function(model) model.Sexe)
            </dt>

            <dd>
                @Html.DisplayFor(Function(model) model.Sexe)
            </dd>

            <dt>
                @Html.DisplayNameFor(Function(model) model.CNI)
            </dt>

            <dd>
                @Html.DisplayFor(Function(model) model.CNI)
            </dd>

            <dt>
                @Html.DisplayNameFor(Function(model) model.Telephone)
            </dt>

            <dd>
                @Html.DisplayFor(Function(model) model.Telephone)
            </dd>

            <dt>
                @Html.DisplayNameFor(Function(model) model.Adresse)
            </dt>

            <dd>
                @Html.DisplayFor(Function(model) model.Adresse)
            </dd>

            <dt>
                @Html.DisplayNameFor(Function(model) model.Quartier)
            </dt>

            <dd>
                @Html.DisplayFor(Function(model) model.Quartier)
            </dd>
            <dt>
                @Html.DisplayNameFor(Function(model) model.Pourcentage)
            </dt>

            <dd>
                @Html.DisplayFor(Function(model) model.Pourcentage)
            </dd>
            <dt>
                @Html.DisplayNameFor(Function(model) model.NumeroCompte)
            </dt>

            <dd>
                @Html.DisplayFor(Function(model) model.NumeroCompte)
            </dd>


        </dl>
        @Using (Html.BeginForm())
            @Html.AntiForgeryToken()

            @<div class="box-footer" style="text-align:center">
                <input type="submit" value="Supprimer" class="btn btn-primary btn-sm" />
                @Html.ActionLink("Retour", "IndexAgence", Nothing, New With {.class = "btn btn-default btn-sm"})
            </div>
        End Using
    </div>
</div>
</div>


