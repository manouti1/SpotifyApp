namespace SpotifyCloneApi.Models
{
    public class AlbumItem
    {
        public string AddedAt { get; set; }
        public Album Album { get; set; }
    }

    public class Album
    {
        public string AlbumType { get; set; }
        public int TotalTracks { get; set; }
        public List<string> AvailableMarkets { get; set; }
        public string Id { get; set; }
        public List<Image> Images { get; set; }
        public string Name { get; set; }
        public string ReleaseDate { get; set; }
        public List<Artist> Artists { get; set; }
        // Add other properties as needed
    }

    public class Image
    {
        public string Url { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
    }

    public class Artist
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Uri { get; set; }
    }

}
