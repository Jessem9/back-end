using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services.DTOs;
using Services.Models;

namespace Services.Controllers
{
    [Route("api/Feedback")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private readonly ServiceDbContext _context;

        public FeedbackController(ServiceDbContext context)
        {
            _context = context;
        }

        // GET: api/Feedback
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FeedbackDto>>> GetFeedbacks()
        {
            var feedbacks = await _context.Feedbacks
                .Include(f => f.Service)
                .Select(f => new FeedbackDto
                {
                    Commentaire = f.Commentaire,
                    Note = f.Note,
                    DemandeurId = f.AuteurId,
                    ServiceId = f.ServiceId
                })
                .ToListAsync();

            return feedbacks;
        }

        // POST: api/Feedback
        [HttpPost]
        public async Task<ActionResult<Feedback>> PostFeedback(FeedbackDto feedbackDto)
        {
            // Vérifier si le service existe
            var service = await _context.Services.FindAsync(feedbackDto.ServiceId);

            if (service == null)
            {
                return BadRequest("Service non trouvé.");
            }

            var feedback = new Feedback
            {
                Commentaire = feedbackDto.Commentaire,
                Note = feedbackDto.Note,
                AuteurId = feedbackDto.DemandeurId,
                ServiceId = feedbackDto.ServiceId
            };

            _context.Feedbacks.Add(feedback);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetFeedbacks), new { id = feedback.Id }, feedback);
        }

        // DELETE: api/Feedback/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFeedback(int id)
        {
            var feedback = await _context.Feedbacks.FindAsync(id);
            if (feedback == null)
            {
                return NotFound();
            }

            _context.Feedbacks.Remove(feedback);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
