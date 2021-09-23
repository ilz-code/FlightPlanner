using System.Linq;
using FlightPlannerDB.DBContext;
using FlightPlannerI.Models;
using FlightPlannerI.Storage;
using FlightPlannerI.Validations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FlightPlannerI.Controllers
{
    [Authorize]
    [Route("admin-api")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly FlightPlannerDbContext _context;

        public AdminController(FlightPlannerDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("flights/{id}")]
        public IActionResult GetFlight(int id)
        {
            var flight = _context.Flights.SingleOrDefault(f => f.Id == id);
            //var flight = FlightStorage.GetById(id);
            if (flight == null)
                return NotFound();

            return Ok(flight);
        }

        [Route("flights")]
        [HttpPut]
        public IActionResult Add(Flight flight)
        {
            var valid = FlightValidation.ValidateFlight(flight);
            if (!valid)
                return BadRequest();
            //var response = FlightStorage.AddFlight(flight);
            //if (response == null)
            //    return Conflict();
            _context.Flights.Add(flight);
            _context.SaveChanges();
            return Created("", flight);
        }

        [Route("flights/{id}")]
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            //FlightStorage.DeleteFlight(id);
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
