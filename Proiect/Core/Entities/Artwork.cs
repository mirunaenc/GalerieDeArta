

using System.Text.Json.Serialization;

namespace Core.Entities
{

    public class Artwork
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }

        public int ArtistId { get; set; }

    }
}
