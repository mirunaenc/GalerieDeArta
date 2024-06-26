namespace Core.Entities
{
    public class Artist
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Nationality { get; set; }

        public ICollection<Artwork> Artworks { get; set; }
    }
}
