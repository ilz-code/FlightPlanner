using System;
using FlightPlannerI.Models;

namespace FlightPlannerI.Validations
{
    public class FlightValidation
    {
        public static bool ValidateFlight(Flight flight)
        {
            if (flight.From == null
                || flight.To == null
                || flight.Carrier == null || flight.Carrier == ""
                || flight.DepartureTime == null
                || flight.ArrivalTime == null)

                return false;

            else if (String.IsNullOrEmpty(flight.From.Country)
                     || String.IsNullOrEmpty(flight.To.Country)
                     || String.IsNullOrEmpty(flight.From.City)
                     || String.IsNullOrEmpty(flight.To.City)
                     || String.IsNullOrEmpty(flight.From.airport)
                     || String.IsNullOrEmpty(flight.To.airport))
                return false;

            else if (flight.From.airport.Trim().ToLower() == flight.To.airport.Trim().ToLower())
                return false;

            else if (Convert.ToDateTime(flight.DepartureTime) >= Convert.ToDateTime(flight.ArrivalTime))
                return false;
            else
                return true;
        }
    }
}
