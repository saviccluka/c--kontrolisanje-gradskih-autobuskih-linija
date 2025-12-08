namespace GradskiTransport.Shared.Models
{
    public class PutnikDto
    {
        public int Id { get; set; }
        public string Ime { get; set; } = string.Empty;
        public string Prezime { get; set; } = string.Empty;
        public string? JMBG { get; set; }
        public string? Email { get; set; }
        public string? Telefon { get; set; }
        public DateTime DatumRegistracije { get; set; }
        public List<KartaDto> Karte { get; set; } = new List<KartaDto>();
    }
}
