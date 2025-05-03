using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services.Models;

namespace Services.Controllers
{
    [Route("api/Categorie")]
    [ApiController]
    public class CategorieController : ControllerBase
    {
        private readonly ServiceDbContext _context;

        public CategorieController(ServiceDbContext context)
        {
            _context = context;
        }

        // GET: api/Categorie
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Categorie>>> GetCategories()
        {
            var categories = await _context.Categories
                .Select(c => new Categorie
                {
                    Id = c.Id,
                    Nom = c.Nom,
                    Image = c.Image  // Mapping image
                })
                .ToListAsync();

            return categories;
        }

        // GET: api/Categorie/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Categorie>> GetCategorie(int id)
        {
            var categorie = await _context.Categories
                .Where(c => c.Id == id)
                .Select(c => new Categorie
                {
                    Id = c.Id,
                    Nom = c.Nom,
                    Image = c.Image  // Mapping image
                })
                .FirstOrDefaultAsync();

            if (categorie == null)
            {
                return NotFound();
            }

            return categorie;
        }

        // PUT: api/Categorie/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategorie(int id, Categorie Categorie)
        {
            if (id != Categorie.Id)
            {
                return BadRequest();
            }

            var categorie = await _context.Categories.FindAsync(id);
            if (categorie == null)
            {
                return NotFound();
            }

            // Update the entity with DTO data
            categorie.Nom = Categorie.Nom;
            categorie.Image = Categorie.Image;  // Update image attribute

            _context.Entry(categorie).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategorieExists(id))
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

        // POST: api/Categorie
        [HttpPost]
        public async Task<ActionResult<Categorie>> PostCategorie(Categorie Categorie)
        {
            var categorie = new Categorie
            {
                Nom = Categorie.Nom,
                Image = Categorie.Image  // Set the image attribute
            };

            _context.Categories.Add(categorie);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCategorie", new { id = categorie.Id }, categorie);
        }

        // DELETE: api/Categorie/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategorie(int id)
        {
            var categorie = await _context.Categories.FindAsync(id);
            if (categorie == null)
            {
                return NotFound();
            }

            _context.Categories.Remove(categorie);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CategorieExists(int id)
        {
            return _context.Categories.Any(e => e.Id == id);
        }
    }
}
