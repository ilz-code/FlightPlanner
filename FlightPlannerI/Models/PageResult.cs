using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightPlannerI.Models
{
    public class PageResult
    {
        public int page { get; set; }
        public int totalItems { get; set; }
        public Flight[] items { get; set; }
    }
}
