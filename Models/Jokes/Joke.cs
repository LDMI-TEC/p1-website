namespace poke_poke.Models.Jokes
{
    public class Joke
    {
        public int Id { get; set; }
        public int AuthorId { get; set; }
        public int CategoryId { get; set; }
        public string? JokeText { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsApproved { get; set; }
        public int Likes { get; set; }
        public int Dislikes { get; set; }

        // navigation properties
        public Author? Author { get; set; }
        public Category? Category{ get; set; }
    }
}