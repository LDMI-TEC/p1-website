namespace poke_poke.Models.Jokes
{
    // DTO class for posting jokes with author name instead of id
    public class JokeCreateDto
    {
        public string? AuthorName { get; set; }
        public int CategoryId { get; set;}
        public string? Joke { get; set; } 
        public int? AuthorAge { get; set; }
    }
}