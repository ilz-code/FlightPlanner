using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlightPlannerI.Models;

namespace FlightPlannerI.Validations
{
    public class AirportValidation
    {
        public bool ValidateAirport(FlightSearch fs)
        {
            if (fs.From == fs.To)
                return false;
            return true;
        }
    }
}
