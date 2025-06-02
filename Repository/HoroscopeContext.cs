using Microsoft.EntityFrameworkCore;
using poke_poke.Models.Horoscopes;

namespace poke_poke.Repository
{
    public class HoroscopeContext : DbContext
    {
        public HoroscopeContext(DbContextOptions<HoroscopeContext> options) : base(options)
        {

        }

        public DbSet<Horoscope> horoscopes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Horoscope>(horoscopeModel =>
            {
                horoscopeModel.ToTable("horoscope");

                horoscopeModel.HasKey(x => x.Id);

                horoscopeModel.Property(x => x.Id)
                    .HasColumnName("id")
                    .ValueGeneratedOnAdd();

                horoscopeModel.Property(x => x.Zodiac)
                    .HasColumnName("zodiac")
                    .HasMaxLength(15);
                    
                horoscopeModel.Property(x => x.LoveHoroscope).HasColumnName("love_horoscope");
                horoscopeModel.Property(x => x.EconomyHoroscope).HasColumnName("economy_horoscope");
                horoscopeModel.Property(x => x.AdviceHoroscope).HasColumnName("advice_horoscope");
            });
        }
    }
}