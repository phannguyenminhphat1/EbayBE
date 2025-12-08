using System.Text.Json.Serialization;

public class TokenResponse<UserType>
{
    [JsonPropertyName("access_token")]
    public string AccessToken { get; } = string.Empty;



    [JsonPropertyName("refresh_token")]
    public string RefreshToken { get; } = string.Empty;

    [JsonPropertyName("user")]
    public UserType User { get; }

    public TokenResponse(string accessToken, string refreshToken, UserType user)
    {
        AccessToken = accessToken;
        RefreshToken = refreshToken;
        User = user;
    }
}