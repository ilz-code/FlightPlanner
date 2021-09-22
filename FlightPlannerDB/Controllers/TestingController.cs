using Microsoft.AspNetCore.Mvc;
using FlightPlannerI.Storage;

namespace FlightPlannerI.Controllers
{
    [Route("testing-api")]
    [ApiController]
    public class TestingController : ControllerBase
    {
        [Route("clear")]
        [HttpPost]
        public IActionResult Clear()
        {
            FlightStorage.ClearFlights();
            FlightStorage._flightNumber = 0;
            return Ok();
        }
    }
}
