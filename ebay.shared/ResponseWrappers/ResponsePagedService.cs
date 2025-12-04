using System.Text.Json.Serialization;

public class ResponsePagedService<T>
{
    [JsonPropertyName("data")]
    public T Data { get; set; } = default!;

    [JsonPropertyName("current_page")]
    public int CurrentPage { get; set; }

    [JsonPropertyName("page_size")]
    public int PageSize { get; set; }

    [JsonPropertyName("total_page")]
    public int TotalPage { get; set; }

    [JsonPropertyName("total_item")]
    public int TotalItem { get; set; }

    public ResponsePagedService(T data, int currentPage, int pageSize, int totalPage, int totalItem)
    {
        Data = data;
        CurrentPage = currentPage;
        PageSize = pageSize;
        TotalPage = totalPage;
        TotalItem = totalItem;

    }
}