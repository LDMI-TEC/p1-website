using System.ComponentModel.DataAnnotations;


namespace poke_poke.Models.Horoscopes
{
    public class Horoscope
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(15)]
        public string Zodiac { get; set; }

        [Required]
        public string LoveHoroscope { get; set; }

        [Required]
        public string EconomyHoroscope { get; set; }

        [Required]
        public string AdviceHoroscope { get; set; }
    }
}