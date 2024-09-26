using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SpotifyCloneApi.Interfaces;

namespace SpotifyClone.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlbumsController : ControllerBase
    {
        private readonly ISpotifyService _spotifyService;

        public AlbumsController(ISpotifyService spotifyService)
        {
            _spotifyService = spotifyService;
        }

        public async Task<IActionResult> GetUserTracks([FromHeader(Name = "Authorization")] string accessToken, int limit = 20, int offset = 0)
        {
            if (string.IsNullOrEmpty(accessToken))
            {
                return Unauthorized("Access token is missing or invalid.");
            }

            var albums = await _spotifyService.GetUserAlbumsAsync(accessToken.Replace("Bearer ", ""), limit, offset);
            if (albums == null)
            {
                return BadRequest("Failed to fetch albums.");
            }


            var jsonResponse = JsonConvert.SerializeObject(albums);
            return new ContentResult
            {
                Content = jsonResponse,
                ContentType = "application/json",
                StatusCode = 200
            };
        }
    }
}
