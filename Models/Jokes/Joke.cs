namespace poke_poke.Models.Jokes
{
    public class Joke
    {
        public int id { get; set; }
        public int authorId { get; set; } 
        public int categoryId { get; set; }
        public string? joke { get; set; }
        public DateTime createAt { get; set; }
        public bool isApproved { get; set; }
        public int likes { get; set; }
        public int dislikes { get; set; }

        // navigation properties
        public Author? author { get; set; }
        public Category? category{ get; set; }
    }
}