using System.Collections.Generic;

namespace FlightPlannerDB.Models
{
    public class PageResult
    {
        public int Page { get; set; }
        public int TotalItems => Items.Count;
        public List<Flight> Items { get; set; }
    }
}
