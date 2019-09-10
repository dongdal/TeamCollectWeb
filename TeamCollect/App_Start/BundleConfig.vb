Imports System.Web.Optimization

Public Module BundleConfig
    ' Pour plus d’informations sur le regroupement, rendez-vous sur http://go.microsoft.com/fwlink/?LinkId=301862
    Public Sub RegisterBundles(ByVal bundles As BundleCollection)

        bundles.Add(New ScriptBundle("~/bundles/jquery").Include(
                    "~/Scripts/jquery-{version}.js"))

        bundles.Add(New ScriptBundle("~/bundles/jqueryval").Include(
                    "~/Scripts/jquery.validate*"))

        ' Utilisez la version de développement de Modernizr pour développer et apprendre. Puis, lorsque vous êtes
        ' prêt pour la production, utilisez l’outil de génération sur http://modernizr.com pour sélectionner uniquement les tests dont vous avez besoin.
        bundles.Add(New ScriptBundle("~/bundles/modernizr").Include(
                    "~/Scripts/modernizr-*"))

        bundles.Add(New ScriptBundle("~/bundles/bootstrap").Include(
                  "~/Scripts/bootstrap.js",
                  "~/Scripts/respond.js"))

        bundles.Add(New StyleBundle("~/Content/css").Include(
                  "~/Content/bootstrap.css",
                  "~/Content/site.css"))

        '...................TEMPLATE.........................................................

        bundles.Add(New StyleBundle("~/css/CssDeBase").Include(
                 "~/css/icons.css",
                 "~/css/bootstrap.css",
                 "~/css/plugins.css",
                 "~/css/main.css",
                "~/css/custom.css"))

        bundles.Add(New ScriptBundle("~/bundles/JsDeBase").Include(
                  "~/js/bootstrap/bootstrap.js",
                  "~/js/libs/modernizr.custom.js",
                  "~/js/jRespond.min.js",
                  "~/plugins/core/slimscroll/jquery.slimscroll.min.js",
                  "~/plugins/core/slimscroll/jquery.slimscroll.horizontal.min.js",
                  "~/plugins/core/fastclick/fastclick.js",
                  "~/plugins/core/velocity/jquery.velocity.min.js",
                  "~/plugins/core/quicksearch/jquery.quicksearch.js",
                  "~/plugins/ui/bootbox/bootbox.js",
                  "~/plugins/charts/sparklines/jquery.sparkline.js",
                  "~/js/jquery.supr.js",
                  "~/js/main.js"))

        bundles.Add(New ScriptBundle("~/bundles/JsBlank").Include(
                  "~/js/pages/blank.js"))

        bundles.Add(New ScriptBundle("~/bundles/Jstabs").Include(
                 "~/js/pages/tabs.js"))

        bundles.Add(New ScriptBundle("~/bundles/Jswidgets").Include(
                "~/js/pages/widgets.js"))

        bundles.Add(New ScriptBundle("~/bundles/Jsprogressbars").Include(
              "~/plugins/ui/progressbar/jquery.circliful.js",
               "~/js/pages/progressbars.js"))

        bundles.Add(New ScriptBundle("~/bundles/JsAvancedForms").Include(
                   "~/plugins/forms/fancyselect/fancySelect.js",
                 "~/plugins/forms/select2/select2.js",
                 "~/plugins/forms/maskedinput/jquery.maskedinput.js",
                "~/plugins/forms/dual-list-box/jquery.bootstrap-duallistbox.js",
               "~/plugins/forms/spinner/jquery.bootstrap-touchspin.js",
              "~/plugins/forms/bootstrap-datepicker/bootstrap-datepicker.js",
            "~/plugins/forms/bootstrap-timepicker/bootstrap-timepicker.js",
          "~/plugins/forms/bootstrap-colorpicker/bootstrap-colorpicker.js",
          "~/plugins/forms/bootstrap-tagsinput/bootstrap-tagsinput.js",
          "~/js/libs/typeahead.bundle.js",
          "~/plugins/forms/summernote/summernote.js",
         "~/plugins/forms/bootstrap-markdown/bootstrap-markdown.js",
         "~/plugins/forms/dropzone/dropzone.js",
         "~/js/pages/forms-advanced.js"))


        bundles.Add(New ScriptBundle("~/bundles/JsDataTable").Include(
                 "~/plugins/tables/datatables/jquery.dataTables.js",
               "~/tables/datatables/dataTables.tableTools.js",
            "~/plugins/tables/datatables/dataTables.bootstrap.js",
          "~/plugins/tables/datatables/dataTables.responsive.js",
           "~/js/pages/tables-data.js"))

        bundles.Add(New ScriptBundle("~/bundles/Highcharts").Include(
          "~/js/Highcharts/highcharts.js",
          "~/js/Highcharts/data.js",
          "~/js/Highcharts/exporting.js"
          ))


        '...................JQuery-Confirm-CSS.........................................................

        bundles.Add(New StyleBundle("~/css/JQueryConfirmCSS").Include(
                 "~/css/jquery-confirm-CSS/jquery-confirm.min.css"))


        '...................JQuery-Confirm-JS.........................................................
        bundles.Add(New ScriptBundle("~/bundles/JQueryConfirmJS").Include(
                  "~/js/jquery-confirm-JS/jquery-confirm.min.js"))



    End Sub
End Module

