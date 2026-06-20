using Microsoft.AspNetCore.Mvc;
using EventRegistrationAPI.Models;
using EventRegistrationAPI.DTOs;
using EventRegistrationAPI.Repositories.Interfaces;

namespace EventRegistrationAPI.Controllers
{
    [ApiController]
    [Route("api/registrations")]
    public class RegistrationsController : ControllerBase
    {
        private readonly IEventRepository _eventRepository;
        private readonly IRegistrationRepository _registrationRepository;

        public RegistrationsController(IEventRepository eventRepository, IRegistrationRepository registrationRepository)
        {
            _eventRepository = eventRepository;
            _registrationRepository = registrationRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterUserDto dto)
        {
            var eventEntity = await _eventRepository.GetByIdAsync(dto.EventId);

            if (eventEntity == null)
                return NotFound(new
                {
                    message = "Event not found."
                });

            var existingRegistration = await _registrationRepository.GetUserRegistrationAsync(dto.UserName, dto.EventId);

            if (existingRegistration != null)
                return BadRequest(new
                {
                    message = "User is already registered for this event."
                });

            int activeRegistrations = eventEntity.Registrations.Count(r => !r.IsCancelled);

            if (activeRegistrations >= eventEntity.TotalSeats)
                return BadRequest(new
                {
                    message = "Event is full."
                });

            var registration = new Registration
            {
                UserName = dto.UserName,
                EventId = dto.EventId,
                RegisteredAt = DateTime.UtcNow,
                IsCancelled = false
            };

            await _registrationRepository.AddAsync(registration);

            await _registrationRepository.SaveChangesAsync();

            return Ok(new
            {
                message = "Registration successful."
            });
        }

        [HttpDelete("{registrationId}")]
        public async Task<IActionResult> CancelRegistration(int registrationId)
        {
            var registration = await _registrationRepository.GetByIdAsync(registrationId);

            if (registration == null)
                return NotFound(new
                {
                    message = "Registration not found."
                });

            if (registration.IsCancelled)
                return BadRequest(new
                {
                    message = "Registration is already cancelled."
                });

            registration.IsCancelled = true;

            await _registrationRepository.SaveChangesAsync();

            return Ok(new
            {
                message = "Registration cancelled successfully."
            });
        }
    }
}