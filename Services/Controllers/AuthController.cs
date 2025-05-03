using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Services.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Services.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ServiceDbContext _context;
        private readonly JWTSettings _jwtSettings;

        public AuthController(ServiceDbContext context, IOptions<JWTSettings> jwtSettings)
        {
            _context = context;
            _jwtSettings = jwtSettings.Value;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel model)
        {
            // Check Admin
            var admin = _context.Admins
                .FirstOrDefault(a => a.Email == model.Email && a.MotDePasse == model.Password);

            if (admin != null)
            {
                var token = GenerateJwtToken(admin.Email, "Admin");
                return Ok(new
                {
                    token,
                    role = "Admin",
                    userId = admin.Id,
                    email = admin.Email
                });
            }

            // Check Demandeur
            var demandeur = _context.Demandeurs
                .FirstOrDefault(d => d.Email == model.Email && d.MotDePasse == model.Password);

            if (demandeur != null)
            {
                // Check if this demandeur is also a Prestataire
                var prestataire = _context.Prestataires
                    .FirstOrDefault(p => p.DemandeurId == demandeur.Id);

                string role = prestataire != null ? "Prestataire" : "Demandeur";
                var token = GenerateJwtToken(demandeur.Email, role);

                return Ok(new
                {
                    token,
                    role,
                    userId = demandeur.Id,
                    email = demandeur.Email
                });
            }

            // If none found
            return Unauthorized(new { message = "Invalid credentials" });
        }

        private string GenerateJwtToken(string email, string role)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.Role, role)
            };

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(_jwtSettings.ExpireMinutes),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
