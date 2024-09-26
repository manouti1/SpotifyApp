using SpotifyCloneApi.Models;

namespace SpotifyCloneApi.Interfaces
{
    public interface ITokenService
    {
        Task<SpotifyTokenResponse> GetAccessTokenAsync(string code);
    }
}
