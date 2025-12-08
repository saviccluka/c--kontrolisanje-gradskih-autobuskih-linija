using GradskiTransport.Shared.Enums;

namespace GradskiTransport.Shared.Models
{
    public class KartaDto
    {
        public int Id { get; set; }
        public int PutnikId { get; set; }
        public string PutnikIme { get; set; } = string.Empty;
        public string PutnikPrezime { get; set; } = string.Empty;
        public string PutnikEmail { get; set; } = string.Empty;
        public string PutnikTelefon { get; set; } = string.Empty;
        public TipKarte Tip { get; set; }
        public StatusKarte Status { get; set; }
        public DateTime DatumKupovine { get; set; }
        public DateTime DatumVazenja { get; set; }
        public decimal Cena { get; set; }
        public string? BrojKarte { get; set; }
        public bool IsIstekla => DatumVazenja < DateTime.Now;
        
        // Computed properties for display
        public string TipKarte => Tip.ToString();
        public string StatusKarte => Status.ToString();
    }
}
