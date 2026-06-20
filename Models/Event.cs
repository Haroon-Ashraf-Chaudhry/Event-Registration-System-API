using System.ComponentModel.DataAnnotations;

namespace EventRegistrationAPI.Models;

public class Event
{
    public int Id { get; set; }

    public required string Name { get; set; }

    public int TotalSeats { get; set; }

    public DateTime EventDate { get; set; }

    public required ICollection<Registration> Registrations { get; set; } = new List<Registration>();
}