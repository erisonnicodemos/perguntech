namespace Perguntech.Services;
public class PaginatedResult<T>
{
    public IEnumerable<T> Items { get; set; }
    public long TotalItems { get; set; }
    public int CurrentPage { get; set; }
    public long PageSize { get; set; }
    public int TotalPages => (int)Math.Ceiling(TotalItems / (double)PageSize);
}
