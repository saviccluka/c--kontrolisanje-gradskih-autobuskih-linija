namespace GradskiTransport.Shared.Models
{
    public class StanicaDto
    {
        public int Id { get; set; }
        public string Naziv { get; set; } = string.Empty;
        public string? Adresa { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public bool Aktivna { get; set; }
        public int RedniBroj { get; set; } // Redosled na liniji
    }
}
