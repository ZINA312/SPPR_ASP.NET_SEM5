using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using WEB_253503_Kudosh.API.Services.TelescopeService;
using WEB_253503_Kudosh.Domain.Entities;
using WEB_253503_Kudosh.Domain.Models;
using Microsoft.AspNetCore.Authorization;

namespace WEB_253503_Kudosh.API.Controllers
{
    [Route("api/")]
    [ApiController]
    public class TelescopeEntitiesController : ControllerBase
    {
        private readonly ITelescopeService _telescopeService;

        public TelescopeEntitiesController(ITelescopeService service)
        {
            _telescopeService = service;
        }

        // GET: api/Telescopes
        [HttpGet("telescopes")]
        [Authorize]
        public async Task<ActionResult<ResponseData<List<TelescopeEntity>>>> GetTelescopes(string? category,
                                                                                        int pageNo = 1,
                                                                                        int pageSize = 3)
        {
            return Ok(await _telescopeService.GetProductListAsync(category, pageNo, pageSize));
        }

        // GET: api/TelescopeEntities/5
        [HttpGet("telescopes/{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<TelescopeEntity>> GetTelescopeEntity(int id)
        {
            var telescopeEntity = await _telescopeService.GetProductByIdAsync(id);

            if (telescopeEntity == null)
            {
                return NotFound();
            }

            return Ok(telescopeEntity);
        }

        // PUT: api/TelescopeEntities/5
        [HttpPut("telescopes/{id}")]
        [Authorize(Policy = "admin")]
        public async Task<IActionResult> PutTelescopeEntity(int id)
        {
            var form = Request.Form;
            var product = form["product"];
            var telescopeEntity = JsonSerializer.Deserialize<TelescopeEntity>(form["product"]);
            var files = form.Files;

            if (id != telescopeEntity.Id)
            {
                return BadRequest("ID не совпадает с ID телескопа.");
            }

            if (GetTelescopeEntity(id) == null)
            {
                return NotFound("Телескоп не найден.");
            }

            var result = await _telescopeService.UpdateProductAsync(id, telescopeEntity);

            return Ok(telescopeEntity);
        }

        // POST: api/TelescopeEntities
        [HttpPost ("telescopes")]
        [Authorize(Policy = "admin")]
        public async Task<ActionResult<TelescopeEntity>> PostTelescopeEntity()
        {
            var form = Request.Form;
            var product = form["product"];
            var telescopeEntity = JsonSerializer.Deserialize<TelescopeEntity>(product);
            var createdEntity = await _telescopeService.CreateProductAsync(telescopeEntity);
            return CreatedAtAction("GetTelescopeEntity", new { id = createdEntity.Data.Id }, createdEntity);
        }

        // DELETE: api/TelescopeEntities/5
        [HttpDelete("telescopes/{id}")]
        [Authorize(Policy = "admin")]
        public async Task<IActionResult> DeleteTelescopeEntity(int id)
        {
            var result = await _telescopeService.DeleteProductAsync(id);
            if (!result.Successfull)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}