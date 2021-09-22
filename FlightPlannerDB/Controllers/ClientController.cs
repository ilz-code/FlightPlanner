using Microsoft.AspNetCore.Mvc;
using FlightPlannerI.Models;
using FlightPlannerI.Storage;

namespace FlightPlannerI.Controllers
{
    [Route("api")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        [Route("airports")]
        [HttpGet]
        public IActionResult SearchAirports(string search)
        {
            var airport = FlightStorage.SearchAirport(search);
            return Ok(airport);
        }

        [Route("flights/search")]
        [HttpPost]
        public IActionResult SearchFlights(FlightSearch fs)
        {
            if (fs.From == fs.To)
                return BadRequest();
            var flight = FlightStorage.SearchFlight(fs);
            return Ok(flight);
        }

        [HttpGet]
        [Route("flights/{id}")]
        public IActionResult GetFlight(int id)
        {
            var flight = FlightStorage.GetById(id);
            if (flight == null)
                return NotFound();

            return Ok(flight);
        }
    }
}
