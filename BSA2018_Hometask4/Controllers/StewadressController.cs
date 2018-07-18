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
    [Route("v1/api/stewadress")]
    [ApiController]
    public class StewadresssController : ControllerBase
    {
        readonly IStewadressService service;

        public StewadresssController(IStewadressService stewadressService)
        {
            service = stewadressService;
        }
        // GET: v1/api/stewadress
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

        // GET: v1/api/stewadress/5
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

        // POST: v1/api/stewadress
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]StewadressDto value)
        {
            try
            {
                
                return Ok(await service.Create(value));
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

        // PUT: v1/api/stewadress/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] StewadressDto stewadress)
        {
            try
            {
                await service.Update(stewadress, id);
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

        // DELETE: v1/api/stewadress/5
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

        // DELETE: v1/api/stewadress
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] StewadressDto stewadress)
        {
            try
            {
                await service.Delete(stewadress);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex);
            }
        }
    }
}
