﻿using BusinessLogic.CommandHandlers;
using BusinessLogic.Models.Commands;
using BusinessLogic.Services;
using Messages;
using NServiceBus;
using NServiceBus.Saga;
using System.Linq;
using Ward.Hubs.Services;
using Ward.Models;
using Ward.ViewModels;

namespace Ward
{
    public class WardSaga : Saga<WardSagaData>,
        IAmStartedByMessages<IWardAcceptance>,
        IHandleMessages<ILabWardResults>,
        IHandleMessages<IRTGWardResults>,
        IHandleMessages<IUSGWardResults>,
        IHandleMessages<IWardAddingExamination>
    {
        private readonly IPatientsService _patientsService;
        private readonly IPatientsDieseasesService _patientsDieseasesService;
        private readonly IShowToUIHubService _showToUIHubService;
        private readonly IAddExaminationToPatientCommandHandler _addExaminationToPatientCommandHandler;
        private readonly IExaminationsService _examinationsService;

        public WardSaga(IShowToUIHubService showToUIHubService,
            IPatientsService patientService,
            IPatientsDieseasesService patientsDieseasesService,
            IAddExaminationToPatientCommandHandler addExaminationToPatientCommandHandler,
            IExaminationsService examinationsService)
        {
            _patientsService = patientService;
            _showToUIHubService = showToUIHubService;
            _patientsDieseasesService = patientsDieseasesService;
            _addExaminationToPatientCommandHandler = addExaminationToPatientCommandHandler;
            _examinationsService = examinationsService;
        }

        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<WardSagaData> mapper)
        {
            mapper.ConfigureMapping<IWardAddingExamination>(s => s.PatientDieseaseId)
                    .ToSaga(m => m.PatientDieseaseId);
            mapper.ConfigureMapping<IWardAcceptance>(s => s.PatientDieseaseId) 
                    .ToSaga(m => m.PatientDieseaseId);
            mapper.ConfigureMapping<ILabWardResults>(s => s.PatientDieseaseId)
                   .ToSaga(m => m.PatientDieseaseId);
            mapper.ConfigureMapping<IUSGWardResults>(s => s.PatientDieseaseId)
                   .ToSaga(m => m.PatientDieseaseId);
            mapper.ConfigureMapping<IRTGWardResults>(s => s.PatientDieseaseId)
                   .ToSaga(m => m.PatientDieseaseId);
            mapper.ConfigureMapping<IWardAddingExamination>(s => s.PatientDieseaseId)
                   .ToSaga(m => m.PatientDieseaseId);
        }

        public void Handle(IWardAcceptance message)
        {
            var patientInfo = _patientsDieseasesService.GetPatientById(message.PatientDieseaseId);
            base.Data.PatientDieseaseId = message.PatientDieseaseId;

            var currentDiesease = new WardPatientCurrentDieseaseViewModel 
            { 
                DieseaseDescription = message.Description, 
                PatientDieseaseId = message.PatientDieseaseId 
            };

            var patientDeclaration = new WardPatientDeclarationViewModel
            {
                PatientInfo = patientInfo,
                CurrentDiesease = currentDiesease
            };

            _showToUIHubService.ShowWardAcceptance(patientDeclaration);
        }

        public void Handle(IWardAddingExamination message)
        {
            Data.Examinations.Add(new Examination(message.Type));
            int examinationId = -1;
                 var addExaminationCommand = _addExaminationToPatientCommandHandler.Add(new AddExaminationToPatientCommand
                 {
                     PatientDieseaseId = message.PatientDieseaseId,
                     ExaminationType = message.Type,
                     LogType = Messages.Models.LogTypeEnum.LogType.Request,
                     Comment = message.Comment
                 }, ref examinationId);
        

           if (addExaminationCommand.IsSuccess)
           {
               switch (message.Type)
               {
                   case ExaminationTypeEnum.ExaminationType.BLOOD:
                       Bus.Send(new WardBloodExaminationRequest
                       {
                           PatientDieseaseId = message.PatientDieseaseId,
                           ExaminationId = examinationId
                       });
                       break;
                   case ExaminationTypeEnum.ExaminationType.RTG:
                       Bus.Send(new WardRTGExaminationRequest
                       {
                           PatientDieseaseId = message.PatientDieseaseId,
                           ExaminationId = examinationId
                       });
                       break;
                   case ExaminationTypeEnum.ExaminationType.USG:
                       Bus.Send(new WardUSGExaminationRequest
                       {
                           PatientDieseaseId = message.PatientDieseaseId,
                           ExaminationId = examinationId
                       });
                       break;
               }
           }
        }

        public void Handle(IRTGWardResults message)
        {
            AddLogToUIAndTryFinish(message.ExaminationId, message.PatientDieseaseId, ExaminationTypeEnum.ExaminationType.RTG);
        }

        public void Handle(IUSGWardResults message)
        {
            AddLogToUIAndTryFinish(message.ExaminationId, message.PatientDieseaseId, ExaminationTypeEnum.ExaminationType.USG);
        }

        public void Handle(ILabWardResults message)
        {
            AddLogToUIAndTryFinish(message.ExaminationId, message.PatientDieseaseId, ExaminationTypeEnum.ExaminationType.LAB);
        }

        private void AddLogToUIAndTryFinish(int examinationId, int patientDieseaseId, ExaminationTypeEnum.ExaminationType examinationType)
        {
            base.Data.PatientDieseaseId = patientDieseaseId;
            var examination = _examinationsService.GetById(examinationId);
        
            var log = new PatientLogViewModel
            {
                Comment = examination.Comment,
                PatientDieseaseId = examination.PatientDieseaseId,
                ExaminationType = examinationType,
                LogType = Messages.Models.LogTypeEnum.LogType.Response,
                When = examination.When
            };
            _showToUIHubService.ShowPatientLog(log);

            if (log.ExaminationType != ExaminationTypeEnum.ExaminationType.LAB)
                ConcludeExaminationAndTryFinish(log.ExaminationType);
            else
                ConcludeExaminationAndTryFinish(ExaminationTypeEnum.ExaminationType.BLOOD);
        }

        private void CheckIfTreatmentComplete()
        {
            if (!Data.Examinations.Where(x => !x.IsReceived).Any())
            {
                ReplyToOriginator(new Results_PatientRecive
                {
                    PatientDieseaseId = Data.PatientDieseaseId,
                });
                MarkAsComplete();
            }
        }

        private void ConcludeExamination(ExaminationTypeEnum.ExaminationType type)
        {
            var examination = Data.Examinations.Where(x => x.Type == type && !x.IsReceived).FirstOrDefault();
            if (examination != null)
            {
                examination.IsReceived = true;
            }
        }

        private void ConcludeExaminationAndTryFinish(ExaminationTypeEnum.ExaminationType type)
        {
            ConcludeExamination(type);
            CheckIfTreatmentComplete();

        }
    }
}