using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services.Models;

namespace Services.Controllers
{
    [Route("api/ProfilPro")]
    [ApiController]
    public class ProfilProController : ControllerBase
    {
        private readonly ServiceDbContext _context;

        public ProfilProController(ServiceDbContext context)
        {
            _context = context;
        }

        // GET: api/ProfilPro
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProfilPro>>> GetProfilPros()
        {
            return await _context.ProfilPros.ToListAsync();
        }

        // GET: api/ProfilPro/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ProfilPro>> GetProfilPro(int id)
        {
            var profilPro = await _context.ProfilPros.FindAsync(id);

            if (profilPro == null)
            {
                return NotFound();
            }

            return profilPro;
        }

        // PUT: api/ProfilPro/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProfilPro(int id, ProfilPro profilPro)
        {
            if (id != profilPro.Id)
            {
                return BadRequest();
            }

            _context.Entry(profilPro).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProfilProExists(id))
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

        // POST: api/ProfilPro
        [HttpPost]
        public async Task<ActionResult<ProfilPro>> PostProfilPro(ProfilPro profilPro)
        {
            _context.ProfilPros.Add(profilPro);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProfilPro", new { id = profilPro.Id }, profilPro);
        }

        // DELETE: api/ProfilPro/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProfilPro(int id)
        {
            var profilPro = await _context.ProfilPros.FindAsync(id);
            if (profilPro == null)
            {
                return NotFound();
            }

            _context.ProfilPros.Remove(profilPro);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProfilProExists(int id)
        {
            return _context.ProfilPros.Any(e => e.Id == id);
        }
    }
}
