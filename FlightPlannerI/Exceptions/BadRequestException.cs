using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace FlightPlannerI.Exceptions
{
    public class BadRequestException: BadHttpRequestException
    {
        public BadRequestException() : base("Bad request", 400)
        {}
    }
}
