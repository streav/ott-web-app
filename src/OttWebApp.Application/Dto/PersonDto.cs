namespace OttWebApp.Application.Dto;

public class PersonDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Biography { get; set; }
    public string? KnownFor { get; set; }
    public DateOnly? BirthDate { get; set; }
    public DateOnly? DeathDay { get; set; }
    public string? BirthPlace { get; set; }
    public string? ProfilePictureUrl { get; set; }
    public int Gender { get; set; }
    public string? ImdbId { get; set; }
    public float Popularity { get; set; }
}