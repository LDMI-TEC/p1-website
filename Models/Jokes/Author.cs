namespace poke_poke.Models.Jokes
{
    public class Author
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Age { get; set; }

        // navigation property
        public ICollection<Joke> Jokes { get; set; } = new List<Joke>();
    }
}