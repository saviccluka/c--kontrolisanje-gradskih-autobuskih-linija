using GradskiTransport.Business;
using GradskiTransport.Presentation.Interfaces;
using GradskiTransport.Shared.Models;

namespace GradskiTransport.Presentation.Presenters
{
    public class UserPresenter : IUserPresenter
    {
        private readonly KorisnikBusiness _korisnikBusiness;
        private string _statusMessage = "";

        public UserPresenter()
        {
            _korisnikBusiness = new KorisnikBusiness();
        }

        public async Task<bool> ValidateUserAsync(string username, string password)
        {
            try
            {
                var user = await _korisnikBusiness.LoginAsync(username, password);
                bool result = user != null;
                _statusMessage = result ? "Uspešno prijavljivanje!" : "Neispravno korisničko ime ili lozinka!";
                return result;
            }
            catch (Exception ex)
            {
                _statusMessage = $"Greška pri prijavljivanju: {ex.Message}";
                return false;
            }
        }

        public async Task<string> GetUserRoleAsync(string username)
        {
            try
            {
                var users = await _korisnikBusiness.GetAllKorisniciAsync();
                var user = users.FirstOrDefault(u => u.Username == username);
                return user?.Uloga ?? "korisnik";
            }
            catch (Exception ex)
            {
                _statusMessage = $"Greška pri dobijanju uloge korisnika: {ex.Message}";
                return "korisnik";
            }
        }

        public async Task<Korisnik?> GetUserAsync(string username)
        {
            try
            {
                var users = await _korisnikBusiness.GetAllKorisniciAsync();
                var user = users.FirstOrDefault(u => u.Username == username);
                _statusMessage = user != null ? "Korisnik pronađen" : "Korisnik nije pronađen";
                return user;
            }
            catch (Exception ex)
            {
                _statusMessage = $"Greška pri dobijanju korisnika: {ex.Message}";
                return null;
            }
        }

        public string GetStatusMessage()
        {
            return _statusMessage;
        }
    }
}
