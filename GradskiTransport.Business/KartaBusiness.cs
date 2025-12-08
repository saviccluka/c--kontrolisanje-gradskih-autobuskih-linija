using GradskiTransport.Data;
using GradskiTransport.Shared.Models;
using GradskiTransport.Shared.Enums;
using GradskiTransport.Shared.Constants;

namespace GradskiTransport.Business
{
    public class KartaBusiness
    {
        private readonly KartaRepository _kartaRepository;

        public KartaBusiness()
        {
            _kartaRepository = new KartaRepository();
        }

        public async Task<List<KartaDto>> GetAllKarteAsync()
        {
            try
            {
                return await _kartaRepository.GetAllKarteAsync();
            }
            catch (Exception)
            {
                return new List<KartaDto>();
            }
        }

        public async Task<KartaDto?> GetKartaByIdAsync(int id)
        {
            try
            {
                return await _kartaRepository.GetKartaByIdAsync(id);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<KartaDto?> GetKartaByBrojAsync(string brojKarte)
        {
            try
            {
                return await _kartaRepository.GetKartaByBrojAsync(brojKarte);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<List<KartaDto>> GetKarteByPutnikIdAsync(int putnikId)
        {
            try
            {
                return await _kartaRepository.GetKarteByPutnikIdAsync(putnikId);
            }
            catch (Exception)
            {
                return new List<KartaDto>();
            }
        }

        public async Task<bool> AddKartaAsync(KartaDto karta)
        {
            try
            {
                // Validacija
                if (string.IsNullOrWhiteSpace(karta.BrojKarte) || karta.PutnikId <= 0)
                    return false;

                // Generisanje broja karte ako nije unet
                if (string.IsNullOrWhiteSpace(karta.BrojKarte))
                {
                    karta.BrojKarte = await GenerateBrojKarteAsync();
                }

                // Postavljanje cene na osnovu tipa
                karta.Cena = GetCenaPoTipu(karta.Tip);

                // Postavljanje datuma važenja na osnovu tipa
                karta.DatumVazenja = GetDatumVazenja(karta.Tip, karta.DatumKupovine);

                // Postavljanje statusa na aktivna
                karta.Status = StatusKarte.Aktivna;

                return await _kartaRepository.AddKartaAsync(karta);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> UpdateKartaAsync(KartaDto karta)
        {
            try
            {
                // Validacija
                if (string.IsNullOrWhiteSpace(karta.BrojKarte) || karta.PutnikId <= 0)
                    return false;

                return await _kartaRepository.UpdateKartaAsync(karta);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteKartaAsync(int id)
        {
            try
            {
                return await _kartaRepository.DeleteKartaAsync(id);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<List<PutnikDto>> GetAllPutniciAsync()
        {
            try
            {
                return await _kartaRepository.GetAllPutniciAsync();
            }
            catch (Exception)
            {
                return new List<PutnikDto>();
            }
        }

        public async Task<Dictionary<string, int>> GetStatistikaPoTipuAsync()
        {
            try
            {
                return await _kartaRepository.GetStatistikaPoTipuAsync();
            }
            catch (Exception)
            {
                return new Dictionary<string, int>();
            }
        }

        public async Task<Dictionary<string, int>> GetStatistikaPoStatusuAsync()
        {
            try
            {
                return await _kartaRepository.GetStatistikaPoStatusuAsync();
            }
            catch (Exception)
            {
                return new Dictionary<string, int>();
            }
        }

        public async Task<decimal> GetUkupnaZaradaAsync()
        {
            try
            {
                return await _kartaRepository.GetUkupnaZaradaAsync();
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public async Task<bool> ValidirajKartuAsync(string brojKarte)
        {
            try
            {
                var karta = await _kartaRepository.GetKartaByBrojAsync(brojKarte);
                if (karta == null)
                    return false;

                // Proveri da li je karta aktivna
                if (karta.Status != StatusKarte.Aktivna)
                    return false;

                // Proveri da li je karta još uvek važeća
                if (karta.DatumVazenja < DateTime.Now)
                {
                    // Ažuriraj status na istekla
                    karta.Status = StatusKarte.Istekla;
                    await _kartaRepository.UpdateKartaAsync(karta);
                    return false;
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> IskoristiKartuAsync(string brojKarte)
        {
            try
            {
                var karta = await _kartaRepository.GetKartaByBrojAsync(brojKarte);
                if (karta == null)
                    return false;

                // Proveri da li je karta aktivna
                if (karta.Status != StatusKarte.Aktivna)
                    return false;

                // Proveri da li je karta još uvek važeća
                if (karta.DatumVazenja < DateTime.Now)
                {
                    karta.Status = StatusKarte.Istekla;
                    await _kartaRepository.UpdateKartaAsync(karta);
                    return false;
                }

                // Ako je jednokratna karta, označi je kao iskorišćenu
                if (karta.Tip == TipKarte.Jednokratna)
                {
                    karta.Status = StatusKarte.Iskoriscena;
                    await _kartaRepository.UpdateKartaAsync(karta);
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private decimal GetCenaPoTipu(TipKarte tip)
        {
            return tip switch
            {
                TipKarte.Jednokratna => ApplicationConstants.CENA_JEDNOKRATNA,
                TipKarte.Dnevna => ApplicationConstants.CENA_DNEVNA,
                TipKarte.Nedeljna => ApplicationConstants.CENA_NEDELJNA,
                TipKarte.Mesecna => ApplicationConstants.CENA_MESECNA,
                TipKarte.Godisnja => ApplicationConstants.CENA_GODISNJA,
                _ => 0
            };
        }

        private DateTime GetDatumVazenja(TipKarte tip, DateTime datumKupovine)
        {
            return tip switch
            {
                TipKarte.Jednokratna => datumKupovine.Date.AddDays(1).AddTicks(-1), // Do kraja dana
                TipKarte.Dnevna => datumKupovine.Date.AddDays(1).AddTicks(-1), // Do kraja dana
                TipKarte.Nedeljna => datumKupovine.Date.AddDays(7).AddTicks(-1), // 7 dana
                TipKarte.Mesecna => datumKupovine.Date.AddMonths(1).AddTicks(-1), // 1 mesec
                TipKarte.Godisnja => datumKupovine.Date.AddYears(1).AddTicks(-1), // 1 godina
                _ => datumKupovine
            };
        }

        private async Task<string> GenerateBrojKarteAsync()
        {
            // Generiši jedinstveni broj karte
            var random = new Random();
            var brojKarte = $"K{DateTime.Now:yyyyMMdd}{random.Next(1000, 9999)}";
            
            // Proveri da li već postoji
            var postojecaKarta = await _kartaRepository.GetKartaByBrojAsync(brojKarte);
            if (postojecaKarta != null)
            {
                // Ako postoji, generiši novi
                return await GenerateBrojKarteAsync();
            }

            return brojKarte;
        }
    }
}
