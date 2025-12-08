using BCrypt.Net;
using GradskiTransport.Data;
using GradskiTransport.Shared.Models;

namespace GradskiTransport.Business
{
    public class KorisnikBusiness
    {
        private readonly KorisnikRepository _korisnikRepository;

        public KorisnikBusiness()
        {
            _korisnikRepository = new KorisnikRepository();
        }

        public async Task<Korisnik?> LoginAsync(string username, string password)
        {
            try
            {
                var korisnik = await _korisnikRepository.GetKorisnikByUsernameAsync(username);
                if (korisnik == null)
                    return null;

                // Za sada koristimo plain text lozinke (možemo kasnije dodati hash)
                if (korisnik.Password == password)
                    return korisnik;

                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<List<Korisnik>> GetAllKorisniciAsync()
        {
            try
            {
                return await _korisnikRepository.GetAllKorisniciAsync();
            }
            catch (Exception)
            {
                return new List<Korisnik>();
            }
        }

        public async Task<bool> AddKorisnikAsync(Korisnik korisnik)
        {
            try
            {
                // Validacija
                if (string.IsNullOrWhiteSpace(korisnik.Ime) || 
                    string.IsNullOrWhiteSpace(korisnik.Prezime) || 
                    string.IsNullOrWhiteSpace(korisnik.Username) || 
                    string.IsNullOrWhiteSpace(korisnik.Password))
                    return false;

                // Proveri da li korisnik već postoji
                var existingKorisnik = await _korisnikRepository.GetKorisnikByUsernameAsync(korisnik.Username);
                if (existingKorisnik != null)
                    return false;

                // Hash lozinku (za buduće implementacije)
                // korisnik.Password = BCrypt.Net.BCrypt.HashPassword(korisnik.Password);

                return await _korisnikRepository.AddKorisnikAsync(korisnik);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> UpdateKorisnikAsync(Korisnik korisnik)
        {
            try
            {
                // Validacija
                if (string.IsNullOrWhiteSpace(korisnik.Ime) || 
                    string.IsNullOrWhiteSpace(korisnik.Prezime) || 
                    string.IsNullOrWhiteSpace(korisnik.Username) || 
                    string.IsNullOrWhiteSpace(korisnik.Password))
                    return false;

                return await _korisnikRepository.UpdateKorisnikAsync(korisnik);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteKorisnikAsync(int id)
        {
            try
            {
                return await _korisnikRepository.DeleteKorisnikAsync(id);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> ChangePasswordAsync(int korisnikId, string oldPassword, string newPassword)
        {
            try
            {
                var korisnik = await _korisnikRepository.GetKorisnikByUsernameAsync(
                    (await _korisnikRepository.GetAllKorisniciAsync())
                    .FirstOrDefault(k => k.Id == korisnikId)?.Username ?? "");

                if (korisnik == null || korisnik.Password != oldPassword)
                    return false;

                korisnik.Password = newPassword;
                return await _korisnikRepository.UpdateKorisnikAsync(korisnik);
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
