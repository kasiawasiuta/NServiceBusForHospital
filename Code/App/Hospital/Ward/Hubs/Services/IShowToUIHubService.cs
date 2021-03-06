﻿using Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ward.ViewModels;

namespace Ward.Hubs.Services
{
    public interface IShowToUIHubService
    {
        void ShowWardAcceptance(WardPatientDeclarationViewModel message);
        void ShowRTGWardResults(IRTGWardResults message);
        void ShowUSGWardResults(IUSGWardResults message);
        void ShowLabWardResults(ILabWardResults message);
    }
}
