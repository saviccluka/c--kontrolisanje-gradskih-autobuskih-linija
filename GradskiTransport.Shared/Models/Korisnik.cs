namespace GradskiTransport.Shared.Models
{
    public class Korisnik
    {
        public int Id { get; set; }
        public string Ime { get; set; } = string.Empty;
        public string Prezime { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Uloga { get; set; } = string.Empty;

        public string ImePrezime => $"{Ime} {Prezime}";
    }
}
