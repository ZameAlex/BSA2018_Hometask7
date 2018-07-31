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
    [Route("v1/api/departures")]
    [ApiController]
    public class DeparturesController : ControllerBase
    {
        readonly IDepartureService service;

        public DeparturesController(IDepartureService departureService)
        {
            service = departureService;
        }
        // GET: v1/api/departures
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok(await service.Get());
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        // GET: v1/api/departures/5
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

        // POST: v1/api/departures
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]DepartureDto value)
        {
            try
            {
                await service.Create(value);
                return Ok();
            }
            catch (ValidationException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // PUT: v1/api/departures/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] DepartureDto departure)
        {
            try
            {
                await service.Update(departure, id);
                return Ok();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex);
            }
            catch (ValidationException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        //PATCH v1/api/departures/5
        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, [FromBody]DateTime value)
        {
            try
            {
                await service.Update(value, id);
                return Ok();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex);
            }
            catch (ValidationException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception ex)
            {
                return BadRequest( ex);
            }
        }

        // DELETE: v1/api/departures/5
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

        // DELETE: v1/api/departures
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DepartureDto departure)
        {
            try
            {
                await service.Delete(departure);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex);
            }
        }
    }
}
