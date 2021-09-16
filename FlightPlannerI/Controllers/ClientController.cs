using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlightPlannerI.Models;
using FlightPlannerI.Storage;
using FlightPlannerI.Validations;

namespace FlightPlannerI.Controllers
{
    [Route("api")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        [Route("airports")]
        [HttpGet]
        public IActionResult SearchAirports(string code)
        {
            FlightStorage.SearchAirport(code);
            return Ok();
        }

        [Route("flights/search")]
        [HttpPost]
        public IActionResult SearchFlights(FlightSearch fs)
        {
            if(fs.From == fs.To)
                return BadRequest();
            var flight = FlightStorage.SearchFlight(fs);
            if (flight == null)
                return null;
            return Ok();
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
