﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messages.Interfaces
{
    public interface IWardPatientResults
    {
        int PatientId { get; set; }
        string Comment { get; set; }
    }
}
