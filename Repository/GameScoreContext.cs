using Microsoft.EntityFrameworkCore;
using poke_poke.Models;

namespace poke_poke.Repository;

public class GameScoreContext : DbContext
{
    public GameScoreContext(DbContextOptions<GameScoreContext> options) : base(options)
    {  

    }

    public DbSet<GameScore> GameScores { get; set; }

    // map the gamecore to the scores table in the db
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<GameScore>(scoreModel =>
        {
            scoreModel.ToTable("scores"); // maps to the table

            scoreModel.HasKey(x => x.Id); // primary key

            scoreModel.Property(filed => filed.Id).HasColumnName("id"); // id to id column

            scoreModel.Property(field => field.PlayerName).HasColumnName("player_name").HasMaxLength(100); // name column NVARCHAR(100) in DB
            scoreModel.Property(field => field.Score).HasColumnName("score"); // score column
            scoreModel.Property(field => field.TimeOfScore).HasColumnName("time_of_score"); // time of score column
            scoreModel.Property(field => field.Secret).HasColumnName("secret").HasMaxLength(100).IsRequired(false); // secret column
        });
    }
}
