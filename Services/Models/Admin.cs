namespace Services.Models
{
    public class Admin
    {   public int Id { get; set; }
        public string Nom { get; set; }
        public string Email { get; set; }
        public required string MotDePasse { get; set; }
    }
}
