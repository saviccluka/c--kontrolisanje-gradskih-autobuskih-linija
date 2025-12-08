using GradskiTransport.Shared.Models;

namespace GradskiTransport.Presentation.Interfaces
{
    public interface ILinijaPresenter
    {
        Task<List<Linija>> GetAllLinijeAsync();
        Task<List<Linija>> SearchLinijeAsync(string searchTerm);
        Task<List<Polazak>> GetPolasciByLinijaAsync(int linijaId);
        Task<bool> AddLinijaAsync(Linija linija);
        Task<bool> UpdateLinijaAsync(Linija linija);
        Task<bool> DeleteLinijaAsync(int linijaId);
        string GetStatusMessage();
    }
}
