using GradskiTransport.Shared.Models;

namespace GradskiTransport.Presentation.Interfaces
{
    public interface IUserPresenter
    {
        Task<bool> ValidateUserAsync(string username, string password);
        Task<string> GetUserRoleAsync(string username);
        Task<Korisnik?> GetUserAsync(string username);
        string GetStatusMessage();
    }
}
