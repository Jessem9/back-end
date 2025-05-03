using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services.DTOs;
using Services.Models;

namespace Services.Controllers
{
    [Route("api/SousCategorie")]
    [ApiController]
    public class SousCategorieController : ControllerBase
    {
        private readonly ServiceDbContext _context;

        public SousCategorieController(ServiceDbContext context)
        {
            _context = context;
        }

        // GET: api/SousCategorie
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SousCategorieDTO>>> GetSousCategories()
        {
            var sousCategories = await _context.SousCategories
                .Include(sc => sc.Categorie)  // Include related Categorie data if needed
                .Select(sc => new SousCategorieDTO
                {
                    Id = sc.Id,
                    Nom = sc.Nom,
                    CategorieId = sc.CategorieId,
                    Image = sc.Image  // Include the image field
                })
                .ToListAsync();

            return sousCategories;
        }

        // GET: api/SousCategorie/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<SousCategorieDTO>> GetSousCategorie(int id)
        {
            var sousCategorie = await _context.SousCategories
                .Include(sc => sc.Categorie)
                .Where(sc => sc.Id == id)
                .Select(sc => new SousCategorieDTO
                {
                    Id = sc.Id,
                    Nom = sc.Nom,
                    CategorieId = sc.CategorieId,
                    Image = sc.Image  // Include the image field
                })
                .FirstOrDefaultAsync();

            if (sousCategorie == null)
            {
                return NotFound();
            }

            return sousCategorie;
        }

        // PUT: api/SousCategorie/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSousCategorie(int id, SousCategorieDTO sousCategorieDto)
        {
            if (id != sousCategorieDto.Id)
            {
                return BadRequest();
            }

            var sousCategorie = await _context.SousCategories.FindAsync(id);
            if (sousCategorie == null)
            {
                return NotFound();
            }

            // Update the entity from DTO
            sousCategorie.Nom = sousCategorieDto.Nom;
            sousCategorie.CategorieId = sousCategorieDto.CategorieId;
            sousCategorie.Image = sousCategorieDto.Image;  // Handle image field

            _context.Entry(sousCategorie).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SousCategorieExists(id))
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

        // POST: api/SousCategorie
        [HttpPost]
        public async Task<ActionResult<SousCategorie>> PostSousCategorie(SousCategorieDTO sousCategorieDto)
        {
            var sousCategorie = new SousCategorie
            {
                Nom = sousCategorieDto.Nom,
                CategorieId = sousCategorieDto.CategorieId,
                Image = sousCategorieDto.Image  // Handle image field
            };

            _context.SousCategories.Add(sousCategorie);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSousCategorie", new { id = sousCategorie.Id }, sousCategorie);
        }

        // DELETE: api/SousCategorie/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSousCategorie(int id)
        {
            var sousCategorie = await _context.SousCategories.FindAsync(id);
            if (sousCategorie == null)
            {
                return NotFound();
            }

            _context.SousCategories.Remove(sousCategorie);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SousCategorieExists(int id)
        {
            return _context.SousCategories.Any(e => e.Id == id);
        }
    }
}
