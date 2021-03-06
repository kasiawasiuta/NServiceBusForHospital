﻿using Autofac;
using BusinessLogic.CommandHandlers;
using BusinessLogic.Services;
using DataAccess;
using DataAccess.Repositories;

namespace HospitalDependencyResolver
{
    public class BusinessLogicModel : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //builder.RegisterType<PatientsService>().As<IPatientsService>().InstancePerRequest();
            //builder.RegisterType<DieseasesService>().As<IDieseasesService>().InstancePerRequest();
            //builder.RegisterType<PatientsDieseasesService>().As<IPatientsDieseasesService>().InstancePerRequest();
            //builder.RegisterType<AccountService>().As<IAccountService>().InstancePerRequest();

            //builder.RegisterType<AddPatientCommandHandler>().As<IAddPatientCommandHandler>().InstancePerRequest();
            //builder.RegisterType<AddDieseaseToPatientCommandHandler>().As<IAddDieseaseToPatientCommandHandler>().InstancePerRequest();

            var context = new HospitalKSREntities();
            
            var patientsRepository = new PatientsRepository(context);
            var dieseasesRepository = new DieseasesRepository(context);
            var patientsDieseasesRepository = new PatientsDieseasesRepository(context);
            var examinationsRepository = new ExaminationsRepository(context);

            var unitOfWork = new UnitOfWork(context);

            builder.RegisterInstance<IPatientsRepository>(patientsRepository);
            builder.RegisterInstance<IDieseasesRepository>(dieseasesRepository);
            builder.RegisterInstance<IPatientsDieseasesRepository>(patientsDieseasesRepository);
            builder.RegisterInstance<IExaminationsRepository>(examinationsRepository);

            builder.RegisterInstance<IUnitOfWork>(unitOfWork);

            var patientService = new PatientsService(patientsRepository);
            var addPatientCommandHandler = new AddPatientCommandHandler(patientsRepository,unitOfWork);
            builder.RegisterInstance<IPatientsService>(patientService);
            builder.RegisterInstance<IAddPatientCommandHandler>(addPatientCommandHandler);
            builder.RegisterInstance<IDieseasesService>(new DieseasesService(dieseasesRepository));
            builder.RegisterInstance<IPatientsDieseasesService>(new PatientsDieseasesService(patientsDieseasesRepository));
            builder.RegisterInstance<IAccountService>(new AccountService(patientService, addPatientCommandHandler));
            builder.RegisterInstance<IExaminationsService>(new ExaminationsService(examinationsRepository));

            builder.RegisterInstance<IAddDieseaseToPatientCommandHandler>(new AddDieseaseToPatientCommandHandler(patientsDieseasesRepository, unitOfWork));
            builder.RegisterInstance<IAddExaminationToPatientCommandHandler>(new AddExaminationToPatientCommandHandler(examinationsRepository, unitOfWork));
           

            base.Load(builder);
        }
    }
}
