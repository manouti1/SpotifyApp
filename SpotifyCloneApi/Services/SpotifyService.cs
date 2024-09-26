using Newtonsoft.Json.Linq;
using SpotifyApi.NetCore;
using SpotifyCloneApi.Interfaces;
using SpotifyCloneApi.Models;
using System.Net.Http.Headers;

namespace SpotifyCloneApi.Services
{
    public class SpotifyService : ISpotifyService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public SpotifyService(IHttpClientFactory httpClientFactory, IAlbumsApi albumsApi)
        {
            _httpClientFactory = httpClientFactory;

        }


        public async Task<List<AlbumItem>> GetUserAlbumsAsync(string accessToken, int limit = 20, int offset = 0)
        {
            var httpClient = _httpClientFactory.CreateClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var response = await httpClient.GetAsync($"https://api.spotify.com/v1/me/albums?limit={limit}&offset={offset}");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var content = await response.Content.ReadAsStringAsync();
            var jsonResponse = JObject.Parse(content);
            var items = jsonResponse["items"].ToObject<List<AlbumItem>>();

            return items;
        }

        public async Task<List<TrackItem>> GetUserTracksAsync(string accessToken, int limit = 20, int offset = 0)
        {
            var httpClient = _httpClientFactory.CreateClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var response = await httpClient.GetAsync($"https://api.spotify.com/v1/me/tracks?limit={limit}&offset={offset}");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var content = await response.Content.ReadAsStringAsync();
            var jsonResponse = JObject.Parse(content);
            var its = jsonResponse["items"];
            var items = jsonResponse["items"].ToObject<List<TrackItem>>();


            return items;
        }
    }
}
