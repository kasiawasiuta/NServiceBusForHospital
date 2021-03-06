﻿using NServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messages
{
    public interface IWardAcceptance:ICommand
    {
        int PatientID { get; set; }
        int DieseaseID { get; set; }
        DateTime IssueDate { get; set; }
        string Description { get; set; }
    }
}
