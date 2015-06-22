﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messages.Director
{
    public class DirectorPatientMessage : IDirectorPatientMessage
    {
        public int MessageId { get; set; }
        public string Content { get; set; }
    }
}
