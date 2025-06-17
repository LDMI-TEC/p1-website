using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using Microsoft.EntityFrameworkCore;
using poke_poke.Repository;

namespace poke_poke.services
{
    public class DataOfTheDay
    {
        private static DataOfTheDay instance;
        private static readonly object lockObject = new object();
        private string WordOfTheDay;
        private Dictionary<string, List<string>> HoroscopeOfTheDay;
        private System.Timers.Timer dailyTimer;
        private DateTime lastFetchData;
        private readonly DbContextOptions<HoroscopeContext> _contextOptions;

        private DataOfTheDay(DbContextOptions<HoroscopeContext> contextOptions)
        {
            this.WordOfTheDay = "";
            this.HoroscopeOfTheDay = new Dictionary<string, List<string>>();
            this.lastFetchData = DateTime.MinValue;
            _contextOptions = contextOptions;

            // add the initial fetch and timer setup
            FetchDailyData();
            SetUpDailyTimer();
        }

        private void SetUpDailyTimer()
        {
            var now = DateTime.Now;
            var nextMidNight = now.Date.AddDays(1); // tomorrow at 00:00
            var timeUntilMidnight = nextMidNight - now;

            // create timer that fires at midnight
            dailyTimer = new System.Timers.Timer(timeUntilMidnight.TotalMilliseconds);
            dailyTimer.Elapsed += OnMidnightReset;
            dailyTimer.AutoReset = false;
            dailyTimer.Start();
        }

        public async void OnMidnightReset(Object sender, ElapsedEventArgs e)
        {
            // fetch new data at midnight
            await FetchDailyData();

            // schedule for next midnight
            dailyTimer.Interval = TimeSpan.FromDays(1).TotalMilliseconds;
            dailyTimer.AutoReset = true;
            dailyTimer.Start();
        }

        private async Task FetchDailyData()
        {
            var today = DateTime.Today;

            //only fetch if we have not fetched already today
            if (lastFetchData.Date != today)
            {
                FetchWordOfTheDay();
                await FetchHoroscopeData();

                lastFetchData = today;
            }
        }

        private void FetchWordOfTheDay()
        {
            //TODO: replace with call to word api or database
        }

        private async Task FetchHoroscopeData()
        {
            HoroscopeOfTheDay.Clear();

            var zodiacSigns = new[] { "Vædderen", "Tyren", "Tvillingerne", "Krebsen", "Løven", "Jomfruen",
                             "Vægten", "Skorpionen", "Skytten", "Stenbukken", "Vandmanden", "Fiskene" };

            // Create a new context instance for this operation
            using var context = new HoroscopeContext(_contextOptions);

            foreach (var zodiac in zodiacSigns)
            {
                try
                {
                    var horoscopes = await context.horoscopes
                        .Where(h => h.Zodiac.ToLower() == zodiac.ToLower())
                        .ToListAsync();

                    if (horoscopes.Any())
                    {
                        var random = new Random();
                        var randomHoroscope = horoscopes[random.Next(horoscopes.Count)];

                        // create the list with (love, economy, advice)
                        var horoscopeList = new List<string>
                        {
                            randomHoroscope.LoveHoroscope,
                            randomHoroscope.EconomyHoroscope,
                            randomHoroscope.AdviceHoroscope
                        };

                        HoroscopeOfTheDay[zodiac] = horoscopeList;
                    }
                    else
                    {
                        // No horoscopes found for this zodiac
                        HoroscopeOfTheDay[zodiac] = new List<string>
                        {
                            "No love horoscope available",
                            "No economy horoscope available",
                            "No advice horoscope available"
                        };
                    }
                }
                catch (Exception e)
                {
                    // log error
                    System.Console.WriteLine($"Error fetching horoscope for {zodiac}: {e.Message}");
                    HoroscopeOfTheDay[zodiac] = new List<string>
                    {
                        "Error loading love horoscope", 
                        "Error loading economy horoscope", 
                        "Error loading advice horoscope"
                    };
                }
            }
        }

        public static DataOfTheDay GetInstance(DbContextOptions<HoroscopeContext> contextOptions = null)
        {
            if (instance == null)
            {
                lock (lockObject)
                {
                    if (instance == null)
                    {
                        if (contextOptions  == null)
                        {
                            throw new ArgumentException("HoroscopeContext must be provided for first initialization");
                        }
                        instance = new DataOfTheDay(contextOptions);
                    }
                }
            }
            return instance;
        }

        public string GetWordOfTheDay() => WordOfTheDay;
        private void SetWordOfTheDay(string word) => WordOfTheDay = word;

        public Dictionary<string, List<string>> GetHoroScope() => HoroscopeOfTheDay;
        private void SetHoroscope(Dictionary<string, List<string>> horoscope) => HoroscopeOfTheDay = horoscope;
    }
}