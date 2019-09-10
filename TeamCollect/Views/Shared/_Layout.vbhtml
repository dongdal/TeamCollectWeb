<!DOCTYPE html>
<html class="no-js">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    @If AppSession.PasswordExpiredDate < DateTime.UtcNow Then
        Response.Redirect(Url.Action("Manage", "Account", New With {.PasswordExpired = True}))
    End If
    <title>@ViewBag.Title - TeamCollect.gov</title>
    @Styles.Render("~/css/CssDeBase")
    @Styles.Render("~/css/JQueryConfirmCSS")
    @Scripts.Render("~/bundles/jquery")
    @Scripts.Render("~/bundles/modernizr")
    @RenderSection("css", required:=False)

</head>



<body class="fixed-header fixed-right-sidebar">
    @Html.Partial("_Header")

    <div id="wrapper">
        <!--Sidebar content-->
        @Html.Partial("_MenuDeGauche")
        <!-- End #sidebar -->
        <!-- Start #right-sidebar -->
        @*@Html.Partial("_MenuDeDroite")*@
        <!-- End #right-sidebar -->
        <div id="content" class="page-content clearfix">
            @RenderBody()
        </div>

        @Html.Partial("_Footer")
        <!-- End #footer  -->
    </div>

    <!-- Back to top -->
    <div id="back-to-top">
        <a href="#">Back to Top</a>
    </div>

    <!-- Load pace first -->
    @*<script src="~/plugins/core/pace/pace.min.js"></script>*@
    <!-- Important javascript libs(put in all pages) -->
    <script src="~/js/jquery-2.1.1.min.js"></script>
    <script>
            window.jQuery || document.write('<script src="@Styles.Url("~/js/libs/jquery-2.1.1.min.js")">\x3C/script>')
    </script>
    <script src="~/js/ui/1.10.4/jquery-ui.js"></script>
    <script>
            window.jQuery || document.write('<script src="@Styles.Url("~/js/libs/jquery-ui-1.10.4.min.js")">\x3C/script>')
    </script>
    <script src="~/js/jquery-migrate-1.2.1.min.js"></script>
    <script>
            window.jQuery || document.write('<script src="@Styles.Url("~/js/libs/jquery-migrate-1.2.1.min.js")">\x3C/script>')
    </script>

    @Scripts.Render("~/bundles/JsDeBase")
    @Scripts.Render("~/bundles/JsAvancedForms")
    @Scripts.Render("~/bundles/JsDataTable")
    @Scripts.Render("~/bundles/Jstabs")
    @Scripts.Render("~/bundles/Jswidgets")
    @Scripts.Render("~/bundles/Jsprogressbars")
    @Scripts.Render("~/bundles/JQueryConfirmJS")
    @RenderSection("scripts", required:=False)
</body>
</html>
