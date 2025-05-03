using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services.Models;

namespace Services.Controllers
{
    [Route("api/Demandeur")]
    [ApiController]
    public class DemandeurController : ControllerBase
    {
        private readonly ServiceDbContext _context;

        public DemandeurController(ServiceDbContext context)
        {
            _context = context;
        }

        // GET: api/Demandeur
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Demandeur>>> GetDemandeurs()
        {
            return await _context.Demandeurs.ToListAsync();
        }

        // GET: api/Demandeur/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Demandeur>> GetDemandeur(int id)
        {
            var demandeur = await _context.Demandeurs.FindAsync(id);

            if (demandeur == null)
            {
                return NotFound();
            }

            return demandeur;
        }

        // POST: api/Demandeur
        [HttpPost]
        public async Task<ActionResult<Demandeur>> PostDemandeur(Demandeur demandeur)
        {
            // Handle the image attribute here
            if (demandeur.Image != null)
            {
                // Do any validation or processing for the image if needed
            }

            _context.Demandeurs.Add(demandeur);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDemandeur), new { id = demandeur.Id }, demandeur);
        }

        // PUT: api/Demandeur/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDemandeur(int id, Demandeur demandeur)
        {
            if (id != demandeur.Id)
            {
                return BadRequest();
            }

            // Handle the image attribute here
            if (demandeur.Image != null)
            {
                // Process the image if needed
            }

            _context.Entry(demandeur).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DemandeurExists(id))
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

        // DELETE: api/Demandeur/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDemandeur(int id)
        {
            var demandeur = await _context.Demandeurs.FindAsync(id);
            if (demandeur == null)
            {
                return NotFound();
            }

            _context.Demandeurs.Remove(demandeur);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DemandeurExists(int id)
        {
            return _context.Demandeurs.Any(e => e.Id == id);
        }
    }
}
