namespace Services.DTOs
{
    public class PrestataireDto
    {
        public int Id { get; set; }
        public int ProfileProId { get; set; }

        public int DemandeurId { get; set; }
        public string Email { get; set; }       // from Demandeur
        public string Image { get; set; }       // from Demandeur
    }
}
