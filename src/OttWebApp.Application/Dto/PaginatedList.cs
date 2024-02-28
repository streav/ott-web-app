namespace OttWebApp.Application.Dto;

public class PaginatedListDto<T> where T : class
{
    public int Page { get; set; }
    public int TotalPages { get; set; }
    public int Total { get; set; }
    public IEnumerable<T>? Data { get; set; }
}