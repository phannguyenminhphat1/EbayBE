using System.Text.Json.Serialization;

public class TokenResponse
{
    [JsonPropertyName("access_token")]
    public string AccessToken { get; } = string.Empty;


    [JsonPropertyName("refresh_token")]
    public string RefreshToken { get; } = string.Empty;

    public TokenResponse(string accessToken, string refreshToken)
    {
        AccessToken = accessToken;
        RefreshToken = refreshToken;
    }
}