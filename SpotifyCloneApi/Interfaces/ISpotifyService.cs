using Newtonsoft.Json.Linq;
using SpotifyApi.NetCore;
using SpotifyCloneApi.Models;

namespace SpotifyCloneApi.Interfaces
{
    public interface ISpotifyService
    {
        Task<List<AlbumItem>> GetUserAlbumsAsync(string accessToken, int limit = 20, int offset = 0);
        Task<List<TrackItem>> GetUserTracksAsync(string accessToken, int limit = 20, int offset = 0);
    }
}
