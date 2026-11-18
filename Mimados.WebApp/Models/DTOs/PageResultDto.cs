namespace Mimados.WebApp.Models;

public class PageResultDto<T>
{
    public int TotalCount { get; set; }
    public List<T> Page { get; set; } = new();
}
