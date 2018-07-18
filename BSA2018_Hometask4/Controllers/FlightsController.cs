using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSA2018_Hometask4.BLL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BSA2018_Hometask4.Shared.DTO;
using FluentValidation;
using BSA2018_Hometask4.Shared.Exceptions;

namespace BSA2018_Hometask4.Controllers
{
    [Route("v1/api/flights")]
    [ApiController]
    public class FlightsController : ControllerBase
    {
        readonly IFlightService service;

        public FlightsController(IFlightService flightService)
        {
            service = flightService;
        }
        // GET: v1/api/flights
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok(await service.Get());
            }
            catch (Exception ex)
            {
                return NotFound(ex);
            }
        }

        // GET: v1/api/flights/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                return Ok(await service.Get(id));
            }
            catch (Exception ex)
            {
                return NotFound(ex);
            }
        }

        // POST: v1/api/flights
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]FlightDto value)
        {
            try
            {
                
                return Ok(await service.Create(value));
            }
            catch (ValidationException e)
            {
                return BadRequest(e.Message);
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // PUT: v1/api/flights/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] FlightDto flight)
        {
            try
            {
                await service.Update(flight, id);
                return Ok("success");
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex);
            }
            catch (ValidationException e)
            {
                return BadRequest(e.Message);
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }

        //PATCH v1/api/flights/5
        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, [FromBody](DateTime,DateTime) value)
        {
            try
            {
                await service.Update(value.Item1, value.Item2, id);
                return Ok("success");
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex);
            }
            catch (ValidationException e)
            {
                return BadRequest(e.Message);
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // DELETE: v1/api/flights/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await service.Delete(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex);
            }
        }

        // DELETE: v1/api/flights
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] FlightDto flight)
        {
            try
            {
                await service.Delete(flight);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex);
            }
        }
    }
}
