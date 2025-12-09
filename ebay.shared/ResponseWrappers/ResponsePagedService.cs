using System.Text.Json.Serialization;

public class ResponsePagedService<T>
{
    [JsonPropertyName("data")]
    public T Data { get; set; } = default!;

    [JsonPropertyName("pagination")]
    public Pagination Pagination { get; set; } = default!;

    public ResponsePagedService(T data, Pagination pagination)
    {
        Data = data;
        Pagination = pagination;
    }
}

public class Pagination
{
    [JsonPropertyName("current_page")]
    public int CurrentPage { get; set; }

    [JsonPropertyName("page_size")]
    public int PageSize { get; set; }

    [JsonPropertyName("total_page")]
    public int TotalPage { get; set; }

    [JsonPropertyName("total_item")]
    public int TotalItem { get; set; }
    public Pagination(int currentPage, int pageSize, int totalItem, int totalPage)
    {
        CurrentPage = currentPage;
        PageSize = pageSize;
        TotalPage = totalPage;
        TotalItem = totalItem;
    }
}