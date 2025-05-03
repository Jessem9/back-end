namespace Services.Models
{
    public class Feedback
    {
        public int Id { get; set; }
        public string Commentaire { get; set; }
        public int Note { get; set; }
        public int AuteurId { get; set; }
        public Demandeur Auteur { get; set; }  // Navigation property
        public int ServiceId { get; set; }
        public Service Service { get; set; }  // Navigation property
    }
}
