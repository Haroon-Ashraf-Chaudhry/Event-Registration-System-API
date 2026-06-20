using Microsoft.EntityFrameworkCore;
using EventRegistrationAPI.Data;
using EventRegistrationAPI.Models;
using EventRegistrationAPI.Repositories.Interfaces;

namespace EventRegistrationAPI.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly AppDbContext _context;

        public EventRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Event?> GetByIdAsync(int id)
        {
            return await _context.Events.Include(e => e.Registrations).FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<Event?> GetByNameAsync(string name)
        {
            return await _context.Events.FirstOrDefaultAsync(e => e.Name == name);
        }

        public async Task<List<Event>> GetAllAsync()
        {
            return await _context.Events.Include(e => e.Registrations).OrderBy(e => e.EventDate).ToListAsync();
        }

        public async Task AddAsync(Event eventEntity)
        {
            await _context.Events.AddAsync(eventEntity);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}