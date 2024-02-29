namespace OttWebApp.Application.Dto;

public class ShowEpisodeDto
{
    public int Number { get; set; }
    public string? Name { get; set; }
    public string? Overview { get; set; }
    public DateTimeOffset? ReleaseDate { get; set; }
    public int? RuntimeMinutes { get; set; }
    public string? BackdropUrl { get; set; }
    public int? StreamId { get; set; }
}