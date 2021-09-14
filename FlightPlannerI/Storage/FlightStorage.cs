using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlightPlannerI.Models;

namespace FlightPlannerI.Storage
{
    public static class FlightStorage
    {
        private static List<Flight> _flights = new List<Flight>();

        public static Flight GetById(int id)
        {
            return _flights.SingleOrDefault(f => f.Id == id);
        }

        public static void ClearFlights()
        {
            _flights.Clear();
        }
    }
}
