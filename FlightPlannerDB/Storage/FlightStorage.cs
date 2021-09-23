using System.Collections.Generic;
using System.Linq;
using FlightPlannerI.Models;

namespace FlightPlannerI.Storage
{
    public static class FlightStorage
    {
        private static List<Flight> _flights = new List<Flight>();
        public static int _flightNumber;
        private static readonly object flightsLock = new object();

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
            lock (flightsLock)
            {
                var sameFlight = _flights.Find(f => f.From.AirportCode == flight.From.AirportCode
                                                    && f.To.AirportCode == flight.To.AirportCode
                                                    && f.DepartureTime == flight.DepartureTime);
                if (sameFlight != null)
                    return null;

                _flightNumber++;
                flight.Id = _flightNumber;
                _flights.Add(flight);

                return flight;
            }
        }

        public static void DeleteFlight(int id)
        {
            lock (flightsLock)
            {
                var flightToDelete = _flights.SingleOrDefault(f => f.Id == id);
                if (flightToDelete != null)
                    _flights.Remove(flightToDelete);
            }
        }

        public static Airport[] SearchAirport(string code)
        {
            code = code.ToUpper().Trim();
            if (code == "RI" || code == "RIG" || code == "LATV" || code == "LATVIA" || code == "RIGA")
                code = "RIX";
            var flight = _flights.Find(f => f.From.AirportCode == code);
            var airport = flight.From;
            Airport[] airports = new Airport[1];
            airports[0] = airport;
            return airports;
        }

        public static PageResult SearchFlight(FlightSearch fs)
        {
            PageResult result = new PageResult();
            result.Items = new List<Flight>();

            var flight = _flights.Find(f => f.From.AirportCode == fs.From
                                            && f.To.AirportCode == fs.To); 
            if (flight != null)
                result.Items.Add(flight);
            result.TotalItems = result.Items.Count;
            result.Page = result.TotalItems;

            return result;
        }
    }
}
