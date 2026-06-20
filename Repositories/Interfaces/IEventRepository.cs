using EventRegistrationAPI.Models;

namespace EventRegistrationAPI.Repositories.Interfaces
{
    public interface IEventRepository
    {
        Task<Event?> GetByIdAsync(int id);
        Task<Event?> GetByNameAsync(string name);
        Task<List<Event>> GetAllAsync();
        Task AddAsync(Event eventEntity);
        Task SaveChangesAsync();
    }
}