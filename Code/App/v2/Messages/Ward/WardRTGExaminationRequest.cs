﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messages
{
    public class WardRTGExaminationRequest : IWardRTGExaminationRequest
    {
        
        public string Comment { get; set; }
        public int PatientDieseaseId { get; set; }
    }
}
