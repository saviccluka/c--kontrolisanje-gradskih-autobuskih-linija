using GradskiTransport.Business;
using GradskiTransport.Shared.Models;
using GradskiTransport.Shared.Enums;

namespace GradskiTransport.Presentation.Presenters
{
    public class KartaPresenter
    {
        private readonly KartaBusiness _kartaBusiness;
        private string _statusMessage = "";

        public KartaPresenter()
        {
            _kartaBusiness = new KartaBusiness();
        }

        public async Task<List<KartaDto>> GetAllKarteAsync()
        {
            try
            {
                var karte = await _kartaBusiness.GetAllKarteAsync();
                _statusMessage = $"Učitano {karte.Count} karata";
                return karte;
            }
            catch (Exception ex)
            {
                _statusMessage = $"Greška pri učitavanju karata: {ex.Message}";
                return new List<KartaDto>();
            }
        }

        public async Task<KartaDto?> GetKartaByIdAsync(int id)
        {
            try
            {
                var karta = await _kartaBusiness.GetKartaByIdAsync(id);
                _statusMessage = karta != null ? "Karta pronađena" : "Karta nije pronađena";
                return karta;
            }
            catch (Exception ex)
            {
                _statusMessage = $"Greška pri dobijanju karte: {ex.Message}";
                return null;
            }
        }

        public async Task<KartaDto?> GetKartaByBrojAsync(string brojKarte)
        {
            try
            {
                var karta = await _kartaBusiness.GetKartaByBrojAsync(brojKarte);
                _statusMessage = karta != null ? "Karta pronađena" : "Karta nije pronađena";
                return karta;
            }
            catch (Exception ex)
            {
                _statusMessage = $"Greška pri dobijanju karte: {ex.Message}";
                return null;
            }
        }

        public async Task<List<KartaDto>> GetKarteByPutnikIdAsync(int putnikId)
        {
            try
            {
                var karte = await _kartaBusiness.GetKarteByPutnikIdAsync(putnikId);
                _statusMessage = $"Učitano {karte.Count} karata za putnika";
                return karte;
            }
            catch (Exception ex)
            {
                _statusMessage = $"Greška pri učitavanju karata putnika: {ex.Message}";
                return new List<KartaDto>();
            }
        }

        public async Task<bool> AddKartaAsync(KartaDto karta)
        {
            try
            {
                bool result = await _kartaBusiness.AddKartaAsync(karta);
                _statusMessage = result ? "Karta je uspešno dodana!" : "Greška pri dodavanju karte!";
                return result;
            }
            catch (Exception ex)
            {
                _statusMessage = $"Greška pri dodavanju karte: {ex.Message}";
                return false;
            }
        }

        public async Task<bool> UpdateKartaAsync(KartaDto karta)
        {
            try
            {
                bool result = await _kartaBusiness.UpdateKartaAsync(karta);
                _statusMessage = result ? "Karta je uspešno ažurirana!" : "Greška pri ažuriranju karte!";
                return result;
            }
            catch (Exception ex)
            {
                _statusMessage = $"Greška pri ažuriranju karte: {ex.Message}";
                return false;
            }
        }

        public async Task<bool> DeleteKartaAsync(int id)
        {
            try
            {
                bool result = await _kartaBusiness.DeleteKartaAsync(id);
                _statusMessage = result ? "Karta je uspešno obrisana!" : "Greška pri brisanju karte!";
                return result;
            }
            catch (Exception ex)
            {
                _statusMessage = $"Greška pri brisanju karte: {ex.Message}";
                return false;
            }
        }

        public async Task<List<PutnikDto>> GetAllPutniciAsync()
        {
            try
            {
                var putnici = await _kartaBusiness.GetAllPutniciAsync();
                _statusMessage = $"Učitano {putnici.Count} putnika";
                return putnici;
            }
            catch (Exception ex)
            {
                _statusMessage = $"Greška pri učitavanju putnika: {ex.Message}";
                return new List<PutnikDto>();
            }
        }

        public async Task<Dictionary<string, int>> GetStatistikaPoTipuAsync()
        {
            try
            {
                var statistika = await _kartaBusiness.GetStatistikaPoTipuAsync();
                _statusMessage = "Statistika po tipu učitana";
                return statistika;
            }
            catch (Exception ex)
            {
                _statusMessage = $"Greška pri učitavanju statistike po tipu: {ex.Message}";
                return new Dictionary<string, int>();
            }
        }

        public async Task<Dictionary<string, int>> GetStatistikaPoStatusuAsync()
        {
            try
            {
                var statistika = await _kartaBusiness.GetStatistikaPoStatusuAsync();
                _statusMessage = "Statistika po statusu učitana";
                return statistika;
            }
            catch (Exception ex)
            {
                _statusMessage = $"Greška pri učitavanju statistike po statusu: {ex.Message}";
                return new Dictionary<string, int>();
            }
        }

        public async Task<decimal> GetUkupnaZaradaAsync()
        {
            try
            {
                var zarada = await _kartaBusiness.GetUkupnaZaradaAsync();
                _statusMessage = $"Ukupna zarada: {zarada:C}";
                return zarada;
            }
            catch (Exception ex)
            {
                _statusMessage = $"Greška pri učitavanju ukupne zarade: {ex.Message}";
                return 0;
            }
        }

        public async Task<bool> ValidirajKartuAsync(string brojKarte)
        {
            try
            {
                bool result = await _kartaBusiness.ValidirajKartuAsync(brojKarte);
                _statusMessage = result ? "Karta je validna" : "Karta nije validna";
                return result;
            }
            catch (Exception ex)
            {
                _statusMessage = $"Greška pri validaciji karte: {ex.Message}";
                return false;
            }
        }

        public async Task<bool> IskoristiKartuAsync(string brojKarte)
        {
            try
            {
                bool result = await _kartaBusiness.IskoristiKartuAsync(brojKarte);
                _statusMessage = result ? "Karta je uspešno iskorišćena" : "Karta nije mogla biti iskorišćena";
                return result;
            }
            catch (Exception ex)
            {
                _statusMessage = $"Greška pri iskorišćavanju karte: {ex.Message}";
                return false;
            }
        }

        public string GetStatusMessage()
        {
            return _statusMessage;
        }

        public List<string> GetTipoviKarata()
        {
            return Enum.GetNames<TipKarte>().ToList();
        }

        public List<string> GetStatusiKarata()
        {
            return Enum.GetNames<StatusKarte>().ToList();
        }
    }
}
