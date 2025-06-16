namespace poke_poke.services
{
    public class DataOfTheDay
    {
        private static DataOfTheDay instance;
        private static readonly object lockObject = new object();
        private string WordOfTheDay;
        private Dictionary<String, String> HoroscopeOfTheDay;

        private DataOfTheDay()
        {
            this.WordOfTheDay = "";
            this.HoroscopeOfTheDay = new Dictionary<String, String>();
        }

        public static DataOfTheDay GetInstance()
        {
            if (instance == null)
            {
                lock (lockObject)
                {
                    if (instance == null)
                    {
                        instance = new DataOfTheDay();
                    }
                }
            }
            return instance;
        }

        public string GetWordOfTheDay() => WordOfTheDay;
        private void setWordOfTheDay(string word) => WordOfTheDay = word;

        public Dictionary<string, string> GetHoroScope() => HoroscopeOfTheDay;
        private void setHoroscope(Dictionary<string, string> horoscope) => HoroscopeOfTheDay = horoscope;
    }
}