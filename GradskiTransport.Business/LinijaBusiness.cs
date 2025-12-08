using GradskiTransport.Data;
using GradskiTransport.Shared.Models;

namespace GradskiTransport.Business
{
    public class LinijaBusiness
    {
        private readonly LinijaRepository _linijaRepository;

        public LinijaBusiness()
        {
            _linijaRepository = new LinijaRepository();
        }

        public async Task<List<Linija>> GetAllLinijeAsync()
        {
            try
            {
                return await _linijaRepository.GetAllLinijeAsync();
            }
            catch (Exception)
            {
                return new List<Linija>();
            }
        }

        public async Task<Linija?> GetLinijaByIdAsync(int id)
        {
            try
            {
                return await _linijaRepository.GetLinijaByIdAsync(id);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<List<Linija>> SearchLinijeAsync(string searchTerm)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(searchTerm))
                    return await GetAllLinijeAsync();

                return await _linijaRepository.GetLinijeByBrojAsync(searchTerm);
            }
            catch (Exception)
            {
                return new List<Linija>();
            }
        }

        public async Task<List<Polazak>> GetPolasciByLinijaIdAsync(int linijaId)
        {
            try
            {
                return await _linijaRepository.GetPolasciByLinijaIdAsync(linijaId);
            }
            catch (Exception)
            {
                return new List<Polazak>();
            }
        }

        public async Task<bool> AddLinijaAsync(Linija linija)
        {
            try
            {
                // Validacija
                if (string.IsNullOrWhiteSpace(linija.BrojLinije) || 
                    string.IsNullOrWhiteSpace(linija.Naziv))
                    return false;

                // Proveri da li linija sa tim brojem već postoji
                var existingLinije = await _linijaRepository.GetLinijeByBrojAsync(linija.BrojLinije);
                if (existingLinije.Any())
                    return false;

                return await _linijaRepository.AddLinijaAsync(linija);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> UpdateLinijaAsync(Linija linija)
        {
            try
            {
                // Validacija
                if (string.IsNullOrWhiteSpace(linija.BrojLinije) || 
                    string.IsNullOrWhiteSpace(linija.Naziv))
                    return false;

                return await _linijaRepository.UpdateLinijaAsync(linija);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteLinijaAsync(int id)
        {
            try
            {
                return await _linijaRepository.DeleteLinijaAsync(id);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<List<LinijaDto>> GetLinijeWithDetailsAsync()
        {
            try
            {
                var linije = await _linijaRepository.GetAllLinijeAsync();
                var result = new List<LinijaDto>();

                foreach (var linija in linije)
                {
                    var polasci = await _linijaRepository.GetPolasciByLinijaIdAsync(linija.Id);
                    
                    result.Add(new LinijaDto
                    {
                        Id = linija.Id,
                        BrojLinije = linija.BrojLinije,
                        Naziv = linija.Naziv,
                        Opis = linija.Opis,
                        Aktivna = true,
                        Polasci = polasci.Select(p => new PolazakDto
                        {
                            Id = p.Id,
                            LinijaId = p.LinijaId,
                            BrojLinije = linija.BrojLinije,
                            NazivLinije = linija.Naziv,
                            VremePolaska = p.VremePolaska,
                            TipVozila = p.TipVozila,
                            Aktivna = true
                        }).ToList()
                    });
                }

                return result;
            }
            catch (Exception)
            {
                return new List<LinijaDto>();
            }
        }
    }
}
