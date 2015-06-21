﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messages
{
    public class WardAddingExamination : IWardAddingExamination
    {
        public int PatientID { get; set; }
        public string Comment { get; set; }
        public ExaminationType Type { get; set; }
        public int PatientDieseaseId { get; set; }
    }
}
