using Microsoft.AspNetCore.Mvc;
using SpotifyCloneApi.Interfaces;
using System.Security.Cryptography;

namespace SpotifyClone.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SpotifyController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly IConfiguration _configuration;

        public SpotifyController(ITokenService tokenService, IConfiguration configuration)
        {
            _tokenService = tokenService;
            _configuration = configuration;
        }

        [HttpGet("authenticate")]
        public IActionResult Authenticate()
        {
            var clientId = _configuration["Spotify:ClientId"];
            var redirectUri = _configuration["Spotify:RedirectUri"];
            var scope = "user-library-read";
            var state = Guid.NewGuid().ToString(); // Generate a unique state

            var authorizationUrl = $"https://accounts.spotify.com/authorize?client_id={clientId}&response_type=code&redirect_uri={redirectUri}&scope={scope}&state={state}";

            HttpContext.Session.SetString("spotify_auth_state", state); // Store state in session

            return Redirect(authorizationUrl);
        }

        [HttpGet("callback")]
        public async Task<IActionResult> Callback(string code, string state)
        {
            var expectedState = HttpContext.Session.GetString("spotify_auth_state");

            if (state != expectedState)
            {
                return BadRequest("Invalid state parameter");
            }

            // Exchange authorization code for token
            var tokenResponse = await _tokenService.GetAccessTokenAsync(code);

            if (tokenResponse == null)
            {
                return BadRequest("Error fetching access token");
            }

            return Ok(tokenResponse);
        }


        // Method to generate a secure random state
        private string GenerateState()
        {
            using (var rng = RandomNumberGenerator.Create())
            {
                var randomBytes = new byte[32];
                rng.GetBytes(randomBytes);
                return Convert.ToBase64String(randomBytes);
            }
        }
    }
}
