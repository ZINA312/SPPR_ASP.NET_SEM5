using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WEB_253503_Kudosh.API.Data;
using WEB_253503_Kudosh.API.Services.TelescopeService;
using WEB_253503_Kudosh.Domain.Entities;
using WEB_253503_Kudosh.Domain.Models;

namespace WEB_253503_Kudosh.API.Controllers
{
    [Route("api/")]
    [ApiController]
    public class TelescopeEntitiesController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ITelescopeService _telescopeService;

        public TelescopeEntitiesController(AppDbContext context, ITelescopeService service)
        {
            _context = context;
            _telescopeService = service;
        }

        // GET: api/TelescopeEntities
        [HttpGet("telescopes")]
        public async Task<ActionResult<ResponseData<List<TelescopeEntity>>>> GetTelescopes(string? category, 
                                                                                        int pageNo = 1, 
                                                                                        int pageSize = 3)
        {
            
            return Ok(await _telescopeService.GetProductListAsync(category, pageNo, pageSize));
        }
        // GET: api/TelescopeEntities/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TelescopeEntity>> GetTelescopeEntity(int id)
        {
            var telescopeEntity = await _context.Telescopes.FindAsync(id);

            if (telescopeEntity == null)
            {
                return NotFound();
            }

            return telescopeEntity;
        }

        // PUT: api/TelescopeEntities/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTelescopeEntity(int id, TelescopeEntity telescopeEntity)
        {
            if (id != telescopeEntity.Id)
            {
                return BadRequest();
            }

            _context.Entry(telescopeEntity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TelescopeEntityExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/TelescopeEntities
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TelescopeEntity>> PostTelescopeEntity(TelescopeEntity telescopeEntity)
        {
            _context.Telescopes.Add(telescopeEntity);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTelescopeEntity", new { id = telescopeEntity.Id }, telescopeEntity);
        }

        // DELETE: api/TelescopeEntities/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTelescopeEntity(int id)
        {
            var telescopeEntity = await _context.Telescopes.FindAsync(id);
            if (telescopeEntity == null)
            {
                return NotFound();
            }

            _context.Telescopes.Remove(telescopeEntity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TelescopeEntityExists(int id)
        {
            return _context.Telescopes.Any(e => e.Id == id);
        }
    }
}
