namespace Services.DTOs
{
    public class FeedbackDto
    {
        public string Commentaire { get; set; }
        public int Note { get; set; }
        public int DemandeurId { get; set; }
        public int ServiceId { get; set; }
    }
}
