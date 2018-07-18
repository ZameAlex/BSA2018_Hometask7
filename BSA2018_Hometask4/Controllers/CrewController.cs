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
using System.Net.Http;
using BSA2018_Hometask7.Shared.DTO;
using System.IO;
using BSA2018_Hometask7.Shared.DTO.API;

namespace BSA2018_Hometask4.Controllers
{
    [Route("v1/api/crew")]
    [ApiController]
    public class CrewsController : ControllerBase
    {
        readonly ICrewService service;

        public CrewsController(ICrewService crewService)
        {
            service = crewService;
        }
        // GET: v1/api/crew
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok(await service.Get());
            }
            catch (Exception ex )
            {
                return NotFound(ex);
            }
        }

        // GET: v1/api/crew/5
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

        // POST: v1/api/crew
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]CrewDto value)
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

        // PUT: v1/api/crew/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] CrewDto crew)
        {
            try
            {
                await service.Update(crew, id);
                return Ok();
            }
            catch(NotFoundException ex)
            {
                return NotFound( ex);
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

        // DELETE: v1/api/crew/5
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

        // DELETE: v1/api/crew
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] CrewDto crew)
        {
            try
            {
                await service.Delete(crew);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex);
            }
        }

        //GET : v1/api/crew/load
        [HttpGet("load")]
        public async Task<IActionResult> Load()
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var x = await client.GetStringAsync("http://5b128555d50a5c0014ef1204.mockapi.io/crew");

                    Newtonsoft.Json.JsonSerializerSettings settings = new Newtonsoft.Json.JsonSerializerSettings();
                    settings.DateFormatString = "YYYY-MM-DDTHH:mm:ss.FFFZ";
                    settings.Formatting = Newtonsoft.Json.Formatting.Indented;
                    var list = Newtonsoft.Json.JsonConvert.DeserializeObject<List<APICrewDto>>(x, settings).Where(s=>s.Id<=10).ToList();
                    await Task.WhenAll(service.WriteToCsv(list), service.WriteToDB(list));
                }
                catch(Exception e)
                {
                    return BadRequest(e);
                }
                return Ok();
            }
        }
    }
}
