using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisitorManagement.Domain.Exceptions
{
    public class VisitorServiceException : ApplicationException
    {
        public VisitorServiceException(string message) : base(message)
        {
        }

        public VisitorServiceException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}