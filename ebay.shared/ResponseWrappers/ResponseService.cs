using System.Text.Json.Serialization;

public class ResponseService<T>
{
    [JsonPropertyName("status")]
    public int StatusCode { get; set; }

    [JsonPropertyName("message")]
    public string Message { get; set; } = string.Empty;

    [JsonPropertyName("data")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public T? Data { get; set; }

    [JsonPropertyName("errors")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Dictionary<string, string>? Errors { get; set; }


    public ResponseService(int statusCode, string message, T? data = default, Dictionary<string, string>? errors = null)
    {
        StatusCode = statusCode;
        Message = message;
        Data = data;
        Errors = errors;
    }
}

