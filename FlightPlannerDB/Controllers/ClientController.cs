using System.Collections.Generic;
using System.Linq;
using FlightPlannerDB.DBContext;
using FlightPlannerDB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FlightPlannerDB.Controllers
{
    [Route("api")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly FlightPlannerDbContext _context;

        public ClientController(FlightPlannerDbContext context)
        {
            _context = context;
        }

        [Route("airports")]
        [HttpGet]
        public IActionResult SearchAirports(string search)
        {
            search = search.ToUpper().Trim();
            var airport = _context.Airports.FirstOrDefault(a => 
                a.City.Substring(0, search.Length) == search ||
                a.Country.Substring(0, search.Length) == search ||
                a.AirportCode.Substring(0, search.Length) == search);
                Airport[] airports = new Airport[1];
                airports[0] = airport;
            return Ok(airports);
        }

        [Route("flights/search")]
        [HttpPost]
        public IActionResult SearchFlights(FlightSearch fs)
        {
            if (fs.From == fs.To)
                return BadRequest();
            
            PageResult result = new PageResult();
            result.Items = new List<Flight>();

            var flight = _context.Flights.Include(f => f.From).Include(f => f.To).FirstOrDefault(f =>
                f.From.AirportCode == fs.From
                && f.To.AirportCode == fs.To);
                
            if (flight != null)
                result.Items.Add(flight);
            
            result.Page = result.TotalItems;

            return Ok(result);
        }

        [HttpGet]
        [Route("flights/{id}")]
        public IActionResult GetFlight(int id)
        {
            var flight = _context.Flights.
                Include(f => f.From).
                Include(f => f.To).
                FirstOrDefault(f => f.Id == id); 
            if (flight == null)
                return NotFound();

            return Ok(flight);
        }
    }
}
