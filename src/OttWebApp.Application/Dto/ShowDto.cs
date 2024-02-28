namespace OttWebApp.Application.Dto;

public class ShowDto
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Overview { get; set; }
    public DateTimeOffset? ReleaseDate { get; set; }
    public DateTimeOffset? LastReleaseDate { get; set; }
    public int? RuntimeMinutes { get; set; }
    public string? Language { get; set; }
    public string? CountryCode { get; set; }
    public string? PosterUrl { get; set; }
    public string? BackdropUrl { get; set; }
    public double? Rating { get; set; }
    public string? RatingSource { get; set; }
    public string? Director { get; set; }
    public IEnumerable<GenreDto>? Genres { get; set; }
    public IEnumerable<BasicPersonDto>? Casts { get; set; }
}