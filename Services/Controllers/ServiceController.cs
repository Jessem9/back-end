using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services.DTOs;
using Services.Models;

namespace Services.Controllers
{
    [Route("api/Service")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        private readonly ServiceDbContext _context;

        public ServiceController(ServiceDbContext context)
        {
            _context = context;
        }

        // GET: api/Service
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ServiceDto>>> GetServices()
        {
            var services = await _context.Services
                .Select(s => new ServiceDto
                {
                    Id = s.Id,
                    Titre = s.Titre,
                    Description = s.Description,
                    PrestataireId = s.PrestataireId,
                    SousCategorieId = s.SousCategorieId,
                    ReserveParId = s.ReserveParId,
                    Image = s.Image,
                })
                .ToListAsync();

            return services;
        }

        // GET: api/Service/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceDto>> GetService(int id)
        {
            var service = await _context.Services.FindAsync(id);

            if (service == null)
            {
                return NotFound();
            }

            var serviceDto = new ServiceDto
            {
                Id = service.Id,
                Titre = service.Titre,
                Description = service.Description,
                PrestataireId = service.PrestataireId,
                SousCategorieId = service.SousCategorieId,
                ReserveParId = service.ReserveParId,
                Image = service.Image,
            };

            return serviceDto;
        }

        // POST: api/Service
        [HttpPost]
        public async Task<ActionResult<Service>> PostService(ServiceDto serviceDto)
        {
            var service = new Service
            {
                Id = serviceDto.Id,
                Titre = serviceDto.Titre,
                Description = serviceDto.Description,
                PrestataireId = serviceDto.PrestataireId,
                SousCategorieId = serviceDto.SousCategorieId,
                ReserveParId = serviceDto.ReserveParId,
                Image = serviceDto.Image,
            };

            _context.Services.Add(service);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetService), new { id = service.Id }, service);
        }

        // PUT: api/Service/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutService(int id, ServiceDto serviceDto)
        {
            var service = await _context.Services.FindAsync(id);

            if (service == null)
            {
                return NotFound();
            }
            service.Id = serviceDto.Id;  
            service.Titre = serviceDto.Titre;
            service.Description = serviceDto.Description;
            service.PrestataireId = serviceDto.PrestataireId;
            service.SousCategorieId = serviceDto.SousCategorieId;
            service.ReserveParId = serviceDto.ReserveParId;
            service.Image = serviceDto.Image;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Service/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteService(int id)
        {
            var service = await _context.Services.FindAsync(id);
            if (service == null)
            {
                return NotFound();
            }

            _context.Services.Remove(service);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ServiceExists(int id)
        {
            return _context.Services.Any(e => e.Id == id);
        }
    }
}
