namespace poke_poke.Models.Jokes
{
    public class JokeDto
    {
        public int Id { get; set; }
        public string? JokeText { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsApproved { get; set; }
        public int Likes { get; set; }
        public int Dislikes { get; set; }
        
        public AuthorDto? Author { get; set; }
        public CategoryDto? Category { get; set; }
    }

    public class AuthorDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Age { get; set; }
    }

    public class CategoryDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
    }

    // For API responses with just likes/dislikes
    public class JokeLikesDto
    {
        public int Likes { get; set; }
        public int Dislikes { get; set; }
    }
}