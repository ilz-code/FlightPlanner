using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using FlightPlannerI.Exceptions;
using FlightPlannerI.Models;
using Microsoft.AspNetCore.Http;

namespace FlightPlannerI.Storage
{
    public static class FlightStorage
    {
        private static List<Flight> _flights = new List<Flight>();
        private static int _flightNumber;

        public static Flight GetById(int id)
        {
            return _flights.SingleOrDefault(f => f.Id == id);
        }

        public static void ClearFlights()
        {
            _flights.Clear();
        }

        public static Flight AddFlight(Flight flight)
        {
            var sameFlight = _flights.Find(f => f.From.airport == flight.From.airport
                                                && f.To.airport == flight.To.airport
                                                && f.DepartureTime == flight.DepartureTime);
                if (sameFlight != null)
                    return null; 

                _flightNumber++;
                flight.Id = _flightNumber;
                _flights.Add(flight);

                return flight;
        }

        public static void DeleteFlight(int id)
        {
            var flightToDelete = _flights.Find(f => f.Id == _flightNumber);
            _flights.Remove(flightToDelete);
        }

        public static Airport SearchAirport(string code)
        {
            code = code.ToUpper().Trim();
            if (code == "RI" || code == "RIG" || code == "LATV" || code == "LATVIA" || code == "RIGA")
                code = "RIX";
            var flight = _flights.Find(f => f.From.airport == code);
            var airport = flight.From;
            return airport;
        }

        public static PageResult SearchFlight(FlightSearch fs)
        {
            var flight = _flights.Find(f => f.From.airport == fs.From
                                            && f.To.airport == fs.To
                                            && f.DepartureTime == fs.Time);
            PageResult result = new PageResult();
            result.page = 0;
            result.totalItems = 0;
            result.items.Append(flight);

            return result;
        }
    }
}
