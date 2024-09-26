using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SpotifyCloneApi.Interfaces;

namespace SpotifyClone.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TracksController : ControllerBase
    {
        private readonly ISpotifyService _spotifyService;

        public TracksController(ISpotifyService spotifyService)
        {
            _spotifyService = spotifyService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUserTracks([FromHeader(Name = "Authorization")] string accessToken, int limit = 20, int offset = 0)
        {
            if (string.IsNullOrEmpty(accessToken))
            {
                return Unauthorized("Access token is missing or invalid.");
            }

            var tracks = await _spotifyService.GetUserTracksAsync(accessToken.Replace("Bearer ", ""), limit, offset);
            if (tracks == null)
            {
                return BadRequest("Failed to fetch tracks.");
            }


            var jsonResponse = JsonConvert.SerializeObject(tracks);
            return new ContentResult
            {
                Content = jsonResponse,
                ContentType = "application/json",
                StatusCode = 200
            };
        }
    }
}
