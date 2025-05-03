namespace Services.Models
{
    public class Service
    {
        public int Id { get; set; }
        public string Titre { get; set; }
        public string Description { get; set; }
        public int PrestataireId { get; set; }
        public Prestataire Prestataire { get; set; }  // Navigation property
        public int SousCategorieId { get; set; }
        public SousCategorie SousCategorie { get; set; }  // Navigation property
        public int? ReserveParId { get; set; }
        public Demandeur ReservePar { get; set; }  // Navigation property (nullable)
        public string Image { get; set; }  // Image path or URL
    }
}
