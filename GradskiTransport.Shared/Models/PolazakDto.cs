namespace GradskiTransport.Shared.Models
{
    public class PolazakDto
    {
        public int Id { get; set; }
        public int LinijaId { get; set; }
        public string BrojLinije { get; set; } = string.Empty;
        public string NazivLinije { get; set; } = string.Empty;
        public TimeSpan VremePolaska { get; set; }
        public string? TipVozila { get; set; }
        public bool Aktivna { get; set; }
    }

    public class Polazak
    {
        public int Id { get; set; }
        public int LinijaId { get; set; }
        public TimeSpan VremePolaska { get; set; }
        public string TipVozila { get; set; } = string.Empty;
    }
}
