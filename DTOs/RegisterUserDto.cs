namespace EventRegistrationAPI.DTOs;

public class RegisterUserDto
{
    public required string UserName { get; set; }

    public int EventId { get; set; }
}