using Microsoft.EntityFrameworkCore;
using EventRegistrationAPI.Data;
using EventRegistrationAPI.Models;
using EventRegistrationAPI.Repositories.Interfaces;

namespace EventRegistrationAPI.Repositories
{
    public class RegistrationRepository : IRegistrationRepository
    {
        private readonly AppDbContext _context;

        public RegistrationRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Registration?> GetByIdAsync(int id)
        {
            return await _context.Registrations.FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<Registration?> GetUserRegistrationAsync(string userName, int eventId)
        {
            return await _context.Registrations.FirstOrDefaultAsync(r => r.UserName == userName && r.EventId == eventId && !r.IsCancelled);
        }

        public async Task AddAsync(Registration registration)
        {
            await _context.Registrations.AddAsync(registration);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}