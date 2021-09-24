using FlightPlannerDB.DBContext;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlannerDB.Controllers
{ 
    [Route("testing-api")]
    [ApiController]
    public class TestingController : ControllerBase
    {
        private readonly FlightPlannerDbContext _context;
        private static readonly object flightsLock = new object();

        public TestingController(FlightPlannerDbContext context)
        {
            _context = context; 
        }

        [Route("clear")]
        [HttpPost]
        public IActionResult Clear()
        {
            lock (flightsLock)
            {
                _context.Airports.RemoveRange(_context.Airports);
                _context.Flights.RemoveRange(_context.Flights);
                _context.SaveChanges();
                return Ok();
            }
        }
    }
}
