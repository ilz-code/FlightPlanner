using System.Linq;
using FlightPlannerDB.DBContext;
using FlightPlannerDB.Models;
using FlightPlannerDB.Validations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FlightPlannerDB.Controllers
{
    [Authorize]
    [Route("admin-api")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly FlightPlannerDbContext _context;
        private static readonly object flightsLock = new object();

        public AdminController(FlightPlannerDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("flights/{id}")]
        public IActionResult GetFlight(int id)
        {
            var flight = _context.Flights.SingleOrDefault(f => f.Id == id);
            
            if (flight == null)
                return NotFound();

            return Ok(flight);
        }

        [Route("flights")]
        [HttpPut]
        public IActionResult Add(Flight flight)
        {
            lock (flightsLock)
            {
                var valid = FlightValidation.ValidateFlight(flight);
                if (!valid)
                    return BadRequest();
                var flightFound = _context.Flights.Include(f => f.From)
                    .Include(f => f.To)
                    .FirstOrDefault(f => f.From.AirportCode == flight.From.AirportCode
                                         && f.To.AirportCode == flight.To.AirportCode
                                         && f.DepartureTime == flight.DepartureTime);
                if (flightFound != null)
                    return Conflict();

                _context.Flights.Add(flight);
                _context.SaveChanges();
                return Created("", flight);
            }
        }

        [Route("flights/{id}")]
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            lock (flightsLock)
            {
                var flight = _context.Flights
                    .Include(a => a.From)
                    .Include(a => a.To)
                    .SingleOrDefault(f => f.Id == id);
                if (flight != null)
                {
                    _context.Airports.Remove(flight.From);
                    _context.Airports.Remove(flight.To);
                    _context.Flights.Remove(flight);
                    _context.SaveChanges();
                }

                return Ok();
            }
        }
    }
}
