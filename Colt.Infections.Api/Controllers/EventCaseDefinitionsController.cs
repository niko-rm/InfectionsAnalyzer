using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Colt.Infections.Library.Entities;

namespace Colt.Infections.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventCaseDefinitionsController : ControllerBase
    {
        private readonly InfectionDbContext _context;

        public EventCaseDefinitionsController(InfectionDbContext context)
        {
            _context = context;
        }

        // GET: api/EventCaseDefinitions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EventCaseDefinition>>> GetEventCaseDefinition()
        {
            return await _context.EventCaseDefinition.ToListAsync();
        }

        // GET: api/EventCaseDefinitions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EventCaseDefinition>> GetEventCaseDefinition(Guid id)
        {
            var eventCaseDefinition = await _context.EventCaseDefinition.FindAsync(id);

            if (eventCaseDefinition == null)
            {
                return NotFound();
            }

            return eventCaseDefinition;
        }

        // PUT: api/EventCaseDefinitions/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEventCaseDefinition(Guid id, EventCaseDefinition eventCaseDefinition)
        {
            if (id != eventCaseDefinition.UidCase)
            {
                return BadRequest();
            }

            _context.Entry(eventCaseDefinition).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventCaseDefinitionExists(id))
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

        // POST: api/EventCaseDefinitions
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<EventCaseDefinition>> PostEventCaseDefinition(EventCaseDefinition eventCaseDefinition)
        {
            _context.EventCaseDefinition.Add(eventCaseDefinition);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (EventCaseDefinitionExists(eventCaseDefinition.UidCase))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetEventCaseDefinition", new { id = eventCaseDefinition.UidCase }, eventCaseDefinition);
        }

        // DELETE: api/EventCaseDefinitions/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<EventCaseDefinition>> DeleteEventCaseDefinition(Guid id)
        {
            var eventCaseDefinition = await _context.EventCaseDefinition.FindAsync(id);
            if (eventCaseDefinition == null)
            {
                return NotFound();
            }

            _context.EventCaseDefinition.Remove(eventCaseDefinition);
            await _context.SaveChangesAsync();

            return eventCaseDefinition;
        }

        private bool EventCaseDefinitionExists(Guid id)
        {
            return _context.EventCaseDefinition.Any(e => e.UidCase == id);
        }
    }
}
