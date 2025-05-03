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

        // GET: api/Prestataires
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PrestataireDto>>> GetPrestataires()
        {
            var prestataires = await _context.Prestataires
                .Include(p => p.Demandeur)
                .ToListAsync();

            var prestataireDtos = prestataires.Select(p => new PrestataireDto
            {
                Id = p.Id,
                ProfileProId = p.ProfileProId,
                DemandeurId = p.DemandeurId,
                Email = p.Demandeur.Email,
                Image = p.Demandeur.Image
            }).ToList();

            return prestataireDtos;
        }

        // GET: api/Prestataires/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<PrestataireDto>> GetPrestataire(int id)
        {
            var prestataire = await _context.Prestataires
                .Include(p => p.Demandeur)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (prestataire == null)
                return NotFound();

            var prestataireDto = new PrestataireDto
            {
                Id = prestataire.Id,
                ProfileProId = prestataire.ProfileProId,
                DemandeurId = prestataire.DemandeurId,
                Email = prestataire.Demandeur.Email,
                Image = prestataire.Demandeur.Image
            };

            return prestataireDto;
        }

        // PUT: api/Prestataires/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPrestataire(int id, PrestataireDto dto)
        {
            if (id != dto.Id)
                return BadRequest("ID mismatch.");

            var prestataire = await _context.Prestataires
                .Include(p => p.Demandeur)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (prestataire == null)
                return NotFound();

            // Update only the allowed fields
            prestataire.ProfileProId = dto.ProfileProId;
            prestataire.Demandeur.Image = dto.Image;

            _context.Entry(prestataire).State = EntityState.Modified;
            _context.Entry(prestataire.Demandeur).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PrestataireExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // POST: api/Prestataires
        [HttpPost]
        public async Task<ActionResult<Prestataire>> PostPrestataire(PrestataireDto dto)
        {
            // Ensure Demandeur exists
            var demandeur = await _context.Demandeurs.FindAsync(dto.DemandeurId);
            if (demandeur == null)
                return BadRequest("Demandeur not found.");

            var prestataire = new Prestataire
            {
                ProfileProId = dto.ProfileProId,
                DemandeurId = dto.DemandeurId
            };

            _context.Prestataires.Add(prestataire);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPrestataire), new { id = prestataire.Id }, prestataire);
        }

        // DELETE: api/Prestataires/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePrestataire(int id)
        {
            var prestataire = await _context.Prestataires.FindAsync(id);
            if (prestataire == null)
                return NotFound();

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
