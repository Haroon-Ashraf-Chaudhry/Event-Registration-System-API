namespace EventRegistrationAPI.DTOs;

public class CreateEventDto
{
    public required string Name { get; set; }

    public int TotalSeats { get; set; }

    public DateTime EventDate { get; set; }
}