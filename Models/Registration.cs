namespace EventRegistrationAPI.Models;

public class Registration
{
    public int Id { get; set; }

    public required string UserName { get; set; }

    public DateTime RegisteredAt { get; set; }

    public bool IsCancelled { get; set; }

    public int EventId { get; set; }

    public Event? Event { get; set; }
}