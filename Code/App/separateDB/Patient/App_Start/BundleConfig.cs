﻿using System.Web.Optimization;

namespace Patient
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/PatientStyles.css"));
                      


            bundles.Add(new ScriptBundle("~/bundles/patient_index").Include(
                    "~/Scripts/select2.js",
                    "~/Scripts/App/Model/PersonalData/PatientsModel.js",
                    "~/Scripts/App/Model/PersonalData/PatientsDieseasesModel.js",
                    "~/Scripts/App/Model/PersonalData/DieseasesModel.js",
                    "~/Scripts/App/Model/PersonalData/ExaminationModel.js",
                    "~/Scripts/App/Model/PersonalData/PatientPersonalDataViewModel.js",
                    "~/Scripts/App/Services/PatientsPersonalDataService.js",
                    "~/Scripts/App/Model/DirectorMessagesModel.js"
                    ));


            bundles.Add(new ScriptBundle("~/bundles/common").Include(
                     "~/Scripts/knockout-3.3.0.js",
                     "~/Scripts/moment.min.js",
                     "~/Scripts/App/Common.js",
                     "~/Scripts/App/Enums.js",
                     "~/Scripts/customBindingHandlers.js"));
        }
    }
}
