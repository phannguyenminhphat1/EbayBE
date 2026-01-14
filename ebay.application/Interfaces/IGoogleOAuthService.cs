public interface IGoogleOAuthService
{
    Task<GoogleTokenResponse> ExchangeCodeAsync(string code);

    Task<GoogleUserInfo> VerifyIdToken(string idToken);
}
