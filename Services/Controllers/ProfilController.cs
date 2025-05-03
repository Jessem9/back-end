using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services.Models;

namespace Services.Controllers
{
    [Route("api/Profil")]
    [ApiController]
    public class ProfilController : ControllerBase
    {
        private readonly ServiceDbContext _context;

        public ProfilController(ServiceDbContext context)
        {
            _context = context;
        }

        // GET: api/Profil
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Profile>>> GetProfils()
        {
            return await _context.Profiles.ToListAsync();
        }

        // GET: api/Profil/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Profile>> GetProfil(int id)
        {
            var profil = await _context.Profiles.FindAsync(id);

            if (profil == null)
            {
                return NotFound();
            }

            return profil;
        }

        // PUT: api/Profil/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProfil(int id, Profile profil)
        {
            if (id != profil.Id)
            {
                return BadRequest();
            }

            _context.Entry(profil).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProfileExists(id))
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

        // POST: api/Profil
        [HttpPost]
        public async Task<ActionResult<Profile>> PostProfil(Profile profile)
        {
            _context.Profiles.Add(profile);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProfile", new { id = profile.Id }, profile);
        }

        // DELETE: api/Profil/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProfile(int id)
        {
            var profile = await _context.Profiles.FindAsync(id);
            if (profile == null)
            {
                return NotFound();
            }

            _context.Profiles.Remove(profile);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProfileExists(int id)
        {
            return _context.Profiles.Any(e => e.Id == id);
        }
    }
}
