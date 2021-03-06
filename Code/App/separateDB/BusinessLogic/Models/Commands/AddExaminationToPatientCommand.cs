﻿using Messages;
using Messages.Models;
using System;

namespace BusinessLogic.Models.Commands
{
    public class AddExaminationToPatientCommand
    {
        public int PatientDieseaseId { get; set; }
        public string Comment { get; set; }
        public ExaminationTypeEnum.ExaminationType ExaminationType { get; set; }
        public LogTypeEnum.LogType LogType { get; set; }
        public DateTime WhenExamined { get; set; }
    }
}
