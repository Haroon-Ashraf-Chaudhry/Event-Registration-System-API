using Microsoft.AspNetCore.Mvc;
using EventRegistrationAPI.Models;
using EventRegistrationAPI.DTOs;
using EventRegistrationAPI.Repositories.Interfaces;

namespace EventRegistrationAPI.Controllers
{
    [ApiController]
    [Route("api/events")]
    public class EventsController : ControllerBase
    {
        private readonly IEventRepository _eventRepository;

        public EventsController(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateEvent(CreateEventDto dto)
        {
            if (dto.TotalSeats <= 0)
                return BadRequest(new
                {
                    message = "Seats must be greater than zero."
                });

            if (dto.EventDate <= DateTime.UtcNow)
                return BadRequest(new
                {
                    message = "Event date must be in the future."
                });

            var existingEvent = await _eventRepository.GetByNameAsync(dto.Name);

            if (existingEvent != null)
                return BadRequest(new
                {
                    message = "Event name already exists."
                });

            var eventEntity = new Event
            {
                Name = dto.Name,
                TotalSeats = dto.TotalSeats,
                EventDate = dto.EventDate,
                Registrations = new List<Registration>()
            };

            await _eventRepository.AddAsync(eventEntity);
            await _eventRepository.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEvents), new { id = eventEntity.Id }, eventEntity);
        }

        [HttpGet]
        public async Task<IActionResult> GetEvents(bool upcomingOnly = false)
        {
            var events = await _eventRepository.GetAllAsync();

            if (upcomingOnly)
            {
                events = events.Where(e => e.EventDate > DateTime.UtcNow).ToList();
            }

            var result = events.Select(e => new
            {
                e.Id,
                e.Name,
                e.EventDate,
                e.TotalSeats,

                TotalRegistrations = e.Registrations.Count(r => !r.IsCancelled),

                AvailableSeats = e.TotalSeats - e.Registrations.Count(r => !r.IsCancelled)
            });

            return Ok(result);
        }
    }
}