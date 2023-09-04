﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisitorManagement.Domain.Exceptions
{
    public class VisitorNotFoundException : ApplicationException
    {
        public VisitorNotFoundException(string message) : base(message)
        {
        }

        public VisitorNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
