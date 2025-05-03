using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services.Models;
using Services.DTOs;

namespace Services.Controllers
{
    [Route("api/Prestataires")]
    [ApiController]
    public class PrestataireController : ControllerBase
    {
        private readonly ServiceDbContext _context;

        public PrestataireController(ServiceDbContext context)
        {
            _context = context;
        }

        // GET: api/Prestataire
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PrestataireDto>>> GetPrestataires()
        {
            var prestataires = await _context.Prestataires.ToListAsync();

            // Map entities to DTOs
            var prestataireDtos = prestataires.Select(p => new PrestataireDto
            {
                Id = p.Id,
                ProfileProId = p.ProfileProId,
                Image = p.Image
            }).ToList();

            return prestataireDtos;
        }


        // GET: api/Prestataire/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<PrestataireDto>> GetPrestataire(int id)
        {
            var prestataire = await _context.Prestataires.FindAsync(id);

            if (prestataire == null)
            {
                return NotFound();
            }

            // Map entity to DTO
            var prestataireDto = new PrestataireDto
            {
                Id = prestataire.Id,
                ProfileProId = prestataire.ProfileProId,
                Image = prestataire.Image
            };

            return prestataireDto;
        }


        // PUT: api/Prestataire/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPrestataire(int id, PrestataireDto prestataireDto)
        {
            if (id != prestataireDto.Id)
            {
                return BadRequest("ID mismatch.");
            }

            // Find the existing entity
            var prestataire = await _context.Prestataires.FindAsync(id);

            if (prestataire == null)
            {
                return NotFound();
            }

            // Update properties
            prestataire.Image = prestataireDto.Image;

            _context.Entry(prestataire).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PrestataireExists(id))
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


        [HttpPost]
        public async Task<ActionResult<Prestataire>> PostPrestataire(PrestataireDto prestataireDto)
        {
            // Mapping DTO to entity
            var prestataire = new Prestataire
            {
                ProfileProId = prestataireDto.ProfileProId,
                Image = prestataireDto.Image
            };

            // Add to the context
            _context.Prestataires.Add(prestataire);
            await _context.SaveChangesAsync();

            // Return the created entity with a location header pointing to the newly created resource
            return CreatedAtAction("GetPrestataire", new { id = prestataire.Id }, prestataire);
        }


        // DELETE: api/Prestataire/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePrestataire(int id)
        {
            var prestataire = await _context.Prestataires.FindAsync(id);
            if (prestataire == null)
            {
                return NotFound();
            }

            _context.Prestataires.Remove(prestataire);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        private bool PrestataireExists(int id)
        {
            return _context.Prestataires.Any(e => e.Id == id);
        }
    }
}
