using System.Net.Http.Headers;
using System.Text.Json;
using Google.Apis.Auth;
using Microsoft.Extensions.Configuration;

public class GoogleOAuthService : IGoogleOAuthService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _config;

    public GoogleOAuthService(HttpClient httpClient, IConfiguration config)
    {
        _httpClient = httpClient;
        _config = config;
    }

    public async Task<GoogleTokenResponse> ExchangeCodeAsync(string code)
    {
        var values = new Dictionary<string, string>
        {
            { "code", code },
            { "client_id", _config["GoogleAuth:ClientId"]! },
            { "client_secret", _config["GoogleAuth:ClientSecret"]! },
            { "redirect_uri", _config["GoogleAuth:RedirectUri"]! },
            { "grant_type", "authorization_code" },
            { "access_type", "offline"}
        };

        var content = new FormUrlEncodedContent(values);

        var response = await _httpClient.PostAsync("https://oauth2.googleapis.com/token", content);

        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<GoogleTokenResponse>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;
    }

    public async Task<GoogleUserInfo> VerifyIdToken(string idToken)
    {
        var payload = await GoogleJsonWebSignature.ValidateAsync(idToken);

        return new GoogleUserInfo
        {
            Email = payload.Email,
            Name = payload.Name,
            Picture = payload.Picture
        };
    }
}
