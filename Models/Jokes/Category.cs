namespace poke_poke.Models.Jokes
{
    public class Category
    {
        public int id { get; set; }
        public string? name { get; set; }

        // navigation property
        public ICollection<Joke> jokes { get; set; } = new List<Joke>();
    }
}