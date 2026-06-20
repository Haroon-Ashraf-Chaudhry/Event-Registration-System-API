using EventRegistrationAPI.Models;

namespace EventRegistrationAPI.Repositories.Interfaces
{
    public interface IRegistrationRepository
    {
        Task<Registration?> GetByIdAsync(int id);

        Task<Registration?> GetUserRegistrationAsync(string userName, int eventId);

        Task AddAsync(Registration registration);

        Task SaveChangesAsync();
    }
}