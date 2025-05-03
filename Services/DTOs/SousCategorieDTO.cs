namespace Services.DTOs
{
    public class SousCategorieDTO
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public int CategorieId { get; set; }  // Only the ID of the related Categorie
        public string Image { get; set; }  // Image path or URL
    }
}
