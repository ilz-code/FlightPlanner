using System;
using FlightPlannerDB.Models;

namespace FlightPlannerDB.Validations
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
                     || String.IsNullOrEmpty(flight.From.AirportCode)
                     || String.IsNullOrEmpty(flight.To.AirportCode))
                return false;

            else if (flight.From.AirportCode.Trim().ToLower() == flight.To.AirportCode.Trim().ToLower())
                return false;

            else if (Convert.ToDateTime(flight.DepartureTime) >= Convert.ToDateTime(flight.ArrivalTime))
                return false;
            else
                return true;
        }
    }
}
