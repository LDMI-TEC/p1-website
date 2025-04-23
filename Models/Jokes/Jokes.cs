namespace poke_poke.Models.Jokes
{
    public class Jokes
    {
        public int id { get; set; }
        public int authorId { get; set; } 
        public int categoryId { get; set; }
        public string? joke { get; set; }
        public DateTime createAT { get; set; }
        public bool isApproved { get; set; }
        public int likes { get; set; }
        public int dislikes { get; set; }
    }
}