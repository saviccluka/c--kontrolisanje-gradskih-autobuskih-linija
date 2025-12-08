using GradskiTransport.Business;
using GradskiTransport.Presentation.Interfaces;
using GradskiTransport.Shared.Models;

namespace GradskiTransport.Presentation.Presenters
{
    public class LinijaPresenter : ILinijaPresenter
    {
        private readonly LinijaBusiness _linijaBusiness;
        private string _statusMessage = "";

        public LinijaPresenter()
        {
            _linijaBusiness = new LinijaBusiness();
        }

        public async Task<List<Linija>> GetAllLinijeAsync()
        {
            try
            {
                var linije = await _linijaBusiness.GetAllLinijeAsync();
                _statusMessage = $"Učitano {linije.Count} linija";
                return linije;
            }
            catch (Exception ex)
            {
                _statusMessage = $"Greška pri učitavanju podataka: {ex.Message}";
                return new List<Linija>();
            }
        }

        public async Task<List<Linija>> SearchLinijeAsync(string searchTerm)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(searchTerm))
                {
                    return await GetAllLinijeAsync();
                }

                var searchResults = await _linijaBusiness.SearchLinijeAsync(searchTerm);
                _statusMessage = $"Pronađeno {searchResults.Count} linija";
                return searchResults;
            }
            catch (Exception ex)
            {
                _statusMessage = $"Greška pri pretrazi: {ex.Message}";
                return new List<Linija>();
            }
        }

        public async Task<List<Polazak>> GetPolasciByLinijaAsync(int linijaId)
        {
            try
            {
                var polasci = await _linijaBusiness.GetPolasciByLinijaIdAsync(linijaId);
                _statusMessage = $"Učitano {polasci.Count} polazaka";
                return polasci;
            }
            catch (Exception ex)
            {
                _statusMessage = $"Greška pri učitavanju polazaka: {ex.Message}";
                return new List<Polazak>();
            }
        }

        public async Task<bool> AddLinijaAsync(Linija linija)
        {
            try
            {
                bool result = await _linijaBusiness.AddLinijaAsync(linija);
                _statusMessage = result ? "Linija je uspešno dodana!" : "Greška pri dodavanju linije!";
                return result;
            }
            catch (Exception ex)
            {
                _statusMessage = $"Greška pri dodavanju linije: {ex.Message}";
                return false;
            }
        }

        public async Task<bool> UpdateLinijaAsync(Linija linija)
        {
            try
            {
                bool result = await _linijaBusiness.UpdateLinijaAsync(linija);
                _statusMessage = result ? "Linija je uspešno ažurirana!" : "Greška pri ažuriranju linije!";
                return result;
            }
            catch (Exception ex)
            {
                _statusMessage = $"Greška pri ažuriranju linije: {ex.Message}";
                return false;
            }
        }

        public async Task<bool> DeleteLinijaAsync(int linijaId)
        {
            try
            {
                bool result = await _linijaBusiness.DeleteLinijaAsync(linijaId);
                _statusMessage = result ? "Linija je uspešno obrisana!" : "Greška pri brisanju linije!";
                return result;
            }
            catch (Exception ex)
            {
                _statusMessage = $"Greška pri brisanju linije: {ex.Message}";
                return false;
            }
        }

        public string GetStatusMessage()
        {
            return _statusMessage;
        }
    }
}
