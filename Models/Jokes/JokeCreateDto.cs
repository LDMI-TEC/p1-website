namespace poke_poke.Models.Jokes
{
    // DTO class for posting jokes with author name instead of id
    public class JokeCreateDto
    {
        public string? authorName { get; set; }
        public int categoryId { get; set;}
        public string? joke { get; set; } 
        public int? authorAge { get; set; }
    }
}