namespace poke_poke.Models.Jokes
{
    public class Category
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        // navigation property
        public ICollection<Joke> Jokes { get; set; } = new List<Joke>();
    }
}