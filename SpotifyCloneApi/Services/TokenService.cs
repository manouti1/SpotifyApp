using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using SpotifyCloneApi.Interfaces;
using SpotifyCloneApi.Models;

namespace SpotifyCloneApi.Services
{
    public class TokenService : ITokenService
    {
        private readonly SpotifyConfig _spotifySettings;
        private readonly HttpClient _httpClient;

        public TokenService(IOptions<SpotifyConfig> spotifySettings, HttpClient httpClient)
        {
            _spotifySettings = spotifySettings.Value;
            _httpClient = httpClient;
        }

        public async Task<SpotifyTokenResponse> GetAccessTokenAsync(string authorizationCode)
        {
            var tokenUrl = _spotifySettings.TokenUrl;
            var clientId = _spotifySettings.ClientId;
            var clientSecret = _spotifySettings.ClientSecret;
            var redirectUri = _spotifySettings.RedirectUri;

            var requestParams = new Dictionary<string, string>
            {
                { "grant_type", "authorization_code" },
                { "code", authorizationCode },
                { "redirect_uri", redirectUri },
                { "client_id", clientId },
                { "client_secret", clientSecret }
            };

            var requestContent = new FormUrlEncodedContent(requestParams);
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, tokenUrl)
            {
                Content = requestContent
            };

            try
            {
                // Sending the request and awaiting the response
                var response = await _httpClient.SendAsync(requestMessage);
                var responseContent = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"Error fetching access token. Status Code: {response.StatusCode}, Response: {responseContent}");
                }

                // Deserialize token response
                var tokenResponse = JsonConvert.DeserializeObject<SpotifyTokenResponse>(responseContent);
                if (tokenResponse == null || string.IsNullOrEmpty(tokenResponse.AccessToken))
                {
                    throw new Exception("Invalid token response received from Spotify.");
                }

                return tokenResponse;
            }
            catch (JsonSerializationException ex)
            {
                throw new Exception($"Error deserializing token response: {ex.Message}");
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"HTTP Request error: {ex.Message}");
            }
        }
    }
}
