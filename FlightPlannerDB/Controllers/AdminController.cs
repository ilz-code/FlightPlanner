using FlightPlannerI.Models;
using FlightPlannerI.Storage;
using FlightPlannerI.Validations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlannerI.Controllers
{
    [Authorize]
    [Route("admin-api")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        [HttpGet]
        [Route("flights/{id}")]
        public IActionResult GetFlight(int id)
        {
            var flight = FlightStorage.GetById(id);
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
            var response = FlightStorage.AddFlight(flight);
            if (response == null)
                return Conflict();
            return Created("", flight);
        }

        [Route("flights/{id}")]
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            FlightStorage.DeleteFlight(id);
            return Ok();
        }
    }
}
