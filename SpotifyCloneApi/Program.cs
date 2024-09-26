using Microsoft.IdentityModel.Tokens;
using SpotifyApi.NetCore;
using SpotifyCloneApi.Interfaces;
using SpotifyCloneApi.Models;
using SpotifyCloneApi.Services;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy("SpotifyAccessPolicy", policy =>
                policy.RequireAuthenticatedUser()
                      .RequireClaim("scope", "user-library-read"));
        });

        // Add authentication and JWT bearer token handling
        builder.Services.AddAuthentication("Bearer")
            .AddJwtBearer("Bearer", options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = false
                };
            });


        builder.Services.Configure<SpotifyConfig>(builder.Configuration.GetSection("Spotify"));

        // Configure Services
        builder.Services.AddControllers()
        .AddJsonOptions(options =>
         {
             options.JsonSerializerOptions.PropertyNamingPolicy = null; // Use original property names
             options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.Never;
         });
        builder.Services.AddHttpClient();

        builder.Services.AddTransient<IAlbumsApi, AlbumsApi>();
        builder.Services.AddTransient<ITracksApi, TracksApi>();
        builder.Services.AddTransient<ITokenService, TokenService>();
        builder.Services.AddTransient<ISpotifyService, SpotifyService>();

        builder.Services.AddDistributedMemoryCache();
        builder.Services.AddSession(options =>
        {
            options.IdleTimeout = TimeSpan.FromMinutes(30); // Session timeout
            options.Cookie.HttpOnly = true;
            options.Cookie.IsEssential = true; // Ensure cookies are always saved
        });

        var app = builder.Build();
        app.UseHttpsRedirection();

        app.UseSession();
        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();
        app.Run();
    }
}