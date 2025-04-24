namespace poke_poke.Models.Jokes
{
    public class Author
    {
        public int id { get; set; }
        public string? name { get; set; }
        public int age { get; set; }

        // navigation property
        public ICollection<Joke> jokes { get; set; } = new List<Joke>();
    }
}