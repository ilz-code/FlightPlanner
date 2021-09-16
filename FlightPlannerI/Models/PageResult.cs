using System.Collections.Generic;

namespace FlightPlannerI.Models
{
    public class PageResult
    {
        public int page { get; set; }
        public int totalItems { get; set; }
        public List<Flight> items { get; set; }
    }
}
