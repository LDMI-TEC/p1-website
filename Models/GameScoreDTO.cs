namespace poke_poke.Models;

public class GameScoreDTO
{
    public int Id { get; set;}
    public string? PlayerName { get; set;}
    public int Score { get; set;}
    public DateTime TimeOfScore { get; set;} = DateTime.Now;

}