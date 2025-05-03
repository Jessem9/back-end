using Microsoft.EntityFrameworkCore;

namespace Services.Models
{
    public class ServiceDbContext : DbContext
    {
        public ServiceDbContext(DbContextOptions<ServiceDbContext> options)
        : base(options) { }

        public DbSet<Admin> Admins { get; set; }
        public DbSet<Demandeur> Demandeurs { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<ProfilPro> ProfilPros { get; set; }
        public DbSet<Prestataire> Prestataires { get; set; }
        public DbSet<Categorie> Categories { get; set; }
        public DbSet<SousCategorie> SousCategories { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
    }
}
