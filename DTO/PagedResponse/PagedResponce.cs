namespace DTO.PagedResponse;

public class PaginationQuery
{
    public int Page { get; set; } = 1;      // Номер страницы (начиная с 1)
    public int PageSize { get; set; } = 10; // Элементов на странице
}

public class PagedResponse<T>
{
    public List<T> Items { get; set; } = new();
    public int Page { get; set; }
    public int PageSize { get; set; }
    public long TotalCount { get; set; }
    public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
}