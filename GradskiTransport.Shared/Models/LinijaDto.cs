namespace GradskiTransport.Shared.Models
{
    public class LinijaDto
    {
        public int Id { get; set; }
        public string BrojLinije { get; set; } = string.Empty;
        public string Naziv { get; set; } = string.Empty;
        public string? Opis { get; set; }
        public bool Aktivna { get; set; }
        public List<StanicaDto> Stanice { get; set; } = new List<StanicaDto>();
        public List<PolazakDto> Polasci { get; set; } = new List<PolazakDto>();
    }

    public class Linija
    {
        public int Id { get; set; }
        public string BrojLinije { get; set; } = string.Empty;
        public string Naziv { get; set; } = string.Empty;
        public string? Opis { get; set; }
    }
}
