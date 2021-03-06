﻿using BusinessLogic.Models;
using DataAccess;
using DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _patientRepository;

        public PatientService(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }

        public PatientModel GetById(int id)
        {
            var patient = _patientRepository.GetById(id);
            return new PatientModel
            {
                Name = patient.Name,
                Age = patient.Age
            };
        }

        public Patient GetByName(string name)
        {
            return _patientRepository.GetByName(name);
        }

        public PatientModel GetModelByName(string name)
        {
            var patient = _patientRepository.GetByName(name);
            return new PatientModel
            {
                Id = patient.Id,
                Name = patient.Name
            };
        }

        public IEnumerable<PatientModel> TestMethod1_1(int id)
        {
            var patients = _patientRepository.TestMethod1(id);
            return patients.Select(x => new PatientModel
            {
                Name = x.Name
            }).ToList();
        }
    }
}
