﻿using BusinessLogic.Services;
using Messages;
using Messages.Common;
using NServiceBus;
using RTG.Hubs.Services;
using RTG.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RTG.Controllers
{
    public class HomeController : Controller,
        IHandleMessages<IWardRTGExaminationRequest>
    {
        private readonly IBus _bus;
        private readonly IPatientsService _patientsService;
        private readonly IShowToUIHubService _showToUIHubService;
        private readonly IPatientsDieseasesService _patientsDieseasesService;

        public HomeController(IBus bus, IShowToUIHubService showToUIHubService,
            IPatientsService patientService, IPatientsDieseasesService patientsDieseasesService)
        {
            _patientsDieseasesService = patientsDieseasesService;
            _patientsService = patientService;
            _showToUIHubService = showToUIHubService;
            _bus = bus;
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public void Handle(IWardRTGExaminationRequest message)
        {

            var patientInfo = _patientsDieseasesService.GetPatientById(message.PatientDieseaseId);

            var currentRTGExamination = new RTGExaminationCommentViewModel
            {
                RTGExaminationComment = message.Comment
            };
            var rtgExamination = new RTGExaminationViewModel
            {
                PatientInfo = patientInfo,
                RTGComment = currentRTGExamination
            };


            _showToUIHubService.ShowRTGExamination(rtgExamination);
        }
        [HttpPost]
        public ActionResult SendResultsToWard(RTGWardResults message)
        {
            _bus.Send(message);
            return Json(new CommandResult(), JsonRequestBehavior.AllowGet);

        }
    }
}