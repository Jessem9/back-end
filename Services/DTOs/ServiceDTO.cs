namespace Services.DTOs
{
    public class ServiceDto
    {
        public string Titre { get; set; }
        public string Description { get; set; }
        public int PrestataireId { get; set; }
        public int SousCategorieId { get; set; }
        public int? ReserveParId { get; set; }  // nullable car un service peut être non réservé
        public string Image { get; set; }  // Image path or URL
    }
}
