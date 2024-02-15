using FlightApp.Interfaces;
using FlightApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlightApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightController : ControllerBase
    {
        private readonly IFlightService _flightService;

        public FlightController(IFlightService flightService)
        {
            _flightService = flightService;
        }
        [HttpPost]
        [Route("AddFlight")]
        public ActionResult AddFlight(Flight flight)
        {
            var result =_flightService.AddFlight(flight);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest();
        }
        [HttpDelete]
        [Route("DeleteFlight/{id}")]
        public ActionResult DeleteFlight(int id)
        {
            var result = _flightService.DeleteFlight(id);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest();
        }
        [HttpGet]
        [Route("GetFlightById/{id}")]
        public ActionResult GetFlightById(int id)
        {
            var result = _flightService.GetById(id);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest();
        }
        [HttpPut]
        [Route("UpdateFlight")]
        public ActionResult UpdateFlight(Flight flight)
        {
            var result = _flightService.UpdateFlight(flight);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest();
        }
        [HttpGet]
        [Route("GetAllFlights")]
        public ActionResult GetAllFlights()
        {
            var result =_flightService.GetAll();
            if(result != null)
            {
                return Ok(result);
            }
            return BadRequest();
        }
    }
}
