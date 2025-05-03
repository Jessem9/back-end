namespace Services.Models
{
    public class Prestataire
    {
        public int Id { get; set; }

        // Foreign key to Demandeur
        public int DemandeurId { get; set; }
        public Demandeur Demandeur { get; set; }

        public int ProfileProId { get; set; }

        // You can optionally duplicate image if needed, or use Demandeur.Image
        // public string Image => Demandeur?.Image;
    }
}
