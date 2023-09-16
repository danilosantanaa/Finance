using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Finance.Application.Helpers
{
    public class ExceptionServiceBadRequestError : Exception
    {
        public ExceptionServiceBadRequestError(string error) : base(error) { }
    }
}