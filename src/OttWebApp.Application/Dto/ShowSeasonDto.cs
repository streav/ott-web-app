namespace OttWebApp.Application.Dto;

public class ShowSeasonDto
{
    public int Number { get; set; }
    public string? Name { get; set; }
    public string? Overview { get; set; }
    public DateTimeOffset? ReleaseDate { get; set; }
    public string? PosterUrl { get; set; }
    public string? BackdropUrl { get; set; }
}