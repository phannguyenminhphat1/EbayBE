using System.Text.Json.Serialization;

public class AuthResponse<T>
{
    [JsonPropertyName("message")]
    public string Message { get; set; } = string.Empty;

    [JsonPropertyName("data")]
    public T? Data { get; set; }

}