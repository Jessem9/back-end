namespace Services.Models
{
    public class SousCategorie
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public int CategorieId { get; set; }
        public Categorie Categorie { get; set; }
        public string Image { get; set; }  // Image path or URL
    }
}
