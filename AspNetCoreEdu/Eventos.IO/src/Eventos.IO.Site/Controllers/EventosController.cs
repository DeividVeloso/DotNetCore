using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Eventos.IO.Application.ViewModels;
using Eventos.IO.Site.Data;

namespace Eventos.IO.Site.Controllers
{
    [Produces("application/json")]
    [Route("api/Eventos")]
    public class EventosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EventosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Eventos
        [HttpGet]
        public IEnumerable<EventoViewModel> GetEventoViewModel()
        {
            return _context.EventoViewModel;
        }

        // GET: api/Eventos/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEventoViewModel([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var eventoViewModel = await _context.EventoViewModel.SingleOrDefaultAsync(m => m.Id == id);

            if (eventoViewModel == null)
            {
                return NotFound();
            }

            return Ok(eventoViewModel);
        }

        // PUT: api/Eventos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEventoViewModel([FromRoute] Guid id, [FromBody] EventoViewModel eventoViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != eventoViewModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(eventoViewModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventoViewModelExists(id))
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

        // POST: api/Eventos
        [HttpPost]
        public async Task<IActionResult> PostEventoViewModel([FromBody] EventoViewModel eventoViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.EventoViewModel.Add(eventoViewModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEventoViewModel", new { id = eventoViewModel.Id }, eventoViewModel);
        }

        // DELETE: api/Eventos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEventoViewModel([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var eventoViewModel = await _context.EventoViewModel.SingleOrDefaultAsync(m => m.Id == id);
            if (eventoViewModel == null)
            {
                return NotFound();
            }

            _context.EventoViewModel.Remove(eventoViewModel);
            await _context.SaveChangesAsync();

            return Ok(eventoViewModel);
        }

        private bool EventoViewModelExists(Guid id)
        {
            return _context.EventoViewModel.Any(e => e.Id == id);
        }
    }
}