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
    [Route("v1/api/pilots")]
    [ApiController]
    public class PilotsController : ControllerBase
    {
        readonly IPilotService service;

        public PilotsController(IPilotService pilotService)
        {
            service = pilotService;
        }
        // GET: v1/api/Pilots
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

        // GET: v1/api/pilots/5
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

        // POST: v1/api/pilots
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]PilotDto value)
        {
            try
            {
                return Ok( await service.Create(value));
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

        // PUT: v1/api/pilots/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] PilotDto pilot)
        {
            try
            {
                await service.Update(pilot, id);
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
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        //PATCH v1/api/pilots/5
        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, [FromBody]int value)
        {
            try
            {
                await service.Update(value, id);
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
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // DELETE: v1/api/pilots/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await service.Delete(id);
                return NoContent();
            }
            catch (Exception ex )
            {
                return NotFound(ex);
            }
        }

        // DELETE: v1/api/pilots
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] PilotDto pilot)
        {
            try
            {
                await service.Delete(pilot);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex);
            }
        }
    }
}
