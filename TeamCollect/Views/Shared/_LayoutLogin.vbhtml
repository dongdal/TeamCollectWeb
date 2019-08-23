﻿<!DOCTYPE html>
<html class="no-js">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>@ViewBag.Title - TeamCollect.gov</title>
    @Styles.Render("~/css/CssDeBase")
    @Scripts.Render("~/bundles/jquery")
    @Scripts.Render("~/bundles/modernizr")
    @RenderSection("css", required:=False)

</head>
<body class="fixed-header fixed-right-sidebar">
    @*@Html.Partial("_Header")*@

    <div id="wrapper">
        <div id="content" class="page-content clearfix">
            @RenderBody()
        </div>

        @Html.Partial("_Footer")
        <!-- End #footer  -->
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
    @RenderSection("scripts", required:=False)
</body>
</html>
