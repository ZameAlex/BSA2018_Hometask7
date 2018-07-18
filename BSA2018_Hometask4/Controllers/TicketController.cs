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
    [Route("v1/api/tickets")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        readonly ITicketService service;

        public TicketsController(ITicketService ticketService)
        {
            service = ticketService;
        }
        // GET: v1/api/Tickets
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

        // GET: v1/api/Tickets/5
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

        // POST: v1/api/Tickets
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]TicketDto value)
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

        // PUT: v1/api/Tickets/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] TicketDto Ticket)
        {
            try
            {
                await service.Update(Ticket, id);
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

        // DELETE: v1/api/Tickets/5
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

        // DELETE: v1/api/Tickets
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] TicketDto Ticket)
        {
            try
            {
                await service.Delete(Ticket);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex);
            }
        }
    }
}
