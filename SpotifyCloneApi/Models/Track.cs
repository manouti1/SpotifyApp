using Newtonsoft.Json;

namespace SpotifyCloneApi.Models
{

    public class ExternalIds
    {
        [JsonProperty("isrc")]
        public string Isrc;
    }

    public class ExternalUrls
    {
        [JsonProperty("spotify")]
        public string Spotify;
    }

    public class TrackItem
    {
        [JsonProperty("added_at")]
        public DateTime? AddedAt;

        [JsonProperty("track")]
        public TrackData Track;
    }

    public class TrackData
    {
        [JsonProperty("album")]
        public Album Album;

        [JsonProperty("artists")]
        public List<Artist> Artists;

        [JsonProperty("available_markets")]
        public List<string> AvailableMarkets;

        [JsonProperty("disc_number")]
        public int? DiscNumber;

        [JsonProperty("duration_ms")]
        public int? DurationMs;

        [JsonProperty("explicit")]
        public bool? Explicit;

        [JsonProperty("external_ids")]
        public ExternalIds ExternalIds;

        [JsonProperty("external_urls")]
        public ExternalUrls ExternalUrls;

        [JsonProperty("href")]
        public string Href;

        [JsonProperty("id")]
        public string Id;

        [JsonProperty("is_local")]
        public bool? IsLocal;

        [JsonProperty("is_playable")]
        public bool? IsPlayable;

        [JsonProperty("name")]
        public string Name;

        [JsonProperty("popularity")]
        public int? Popularity;

        [JsonProperty("preview_url")]
        public string PreviewUrl;

        [JsonProperty("track_number")]
        public int? TrackNumber;

        [JsonProperty("type")]
        public string Type;

        [JsonProperty("uri")]
        public string Uri;
    }
}
