namespace Services.Models
{
    public class Prestataire
    {
        public int Id { get; set; }
        public int ProfileProId { get; set; }
        public ProfilPro ProfilePro { get; set; }  // Navigation property

        public string Image { get; set; }  // Image path or URL
    }
}
