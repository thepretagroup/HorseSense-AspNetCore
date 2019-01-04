using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using HorseSense_AspNetCore.Models;

namespace HorseSense_AspNetCore.Helpers
{
    public class DrfParser
    {
        public static async Task<RaceDay> ParseFile(HorseSenseContext context, IFormFile file)
        {
            RaceDay raceDay = null;

            var lines = new List<string>();
            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                while (reader.Peek() >= 0)
                {
                    var line = await reader.ReadLineAsync();
                    var parsedRaceday = BrisnetRtfParser.ParseLine(line);

                    raceDay = ParseLine(context, parsedRaceday);
                }
            }

            return raceDay;
        }

        private static RaceDay ParseLine(HorseSenseContext context, RaceDay parsedRaceday)
        {
            var raceday = FindOrCreateRaceDay(context, parsedRaceday);
            var race = FindOrCreateRace(context, raceday, parsedRaceday);
            var horse = FindOrCreateHorse(context, race, parsedRaceday);

            context.SaveChanges();

            return raceday;
        }

        private static RaceDay FindOrCreateRaceDay(HorseSenseContext context, RaceDay parsedRaceday)
        {
            RaceDay raceDay = context.RaceDays
                .Include(rd => rd.Races)
                .ThenInclude(r => r.Horses)   //// @@@@ TODO: Move to FindOrCreateRace()
                .FirstOrDefault<RaceDay>(rd => rd.Track.Equals(parsedRaceday.Track) && rd.Date.Equals(parsedRaceday.Date));

            if (raceDay == null)
            {
                raceDay = new RaceDay
                {
                    Track = parsedRaceday.Track,
                    Date = parsedRaceday.Date
                };

                context.Add(raceDay);
            }

            return raceDay;
        }

        private static Race FindOrCreateRace(HorseSenseContext context, RaceDay raceday, RaceDay parsedRaceday)
        {
            Race race = null;
            foreach (var raceItem in raceday.Races)
            {
                if (raceItem.RaceNum == parsedRaceday.Races.First<Race>().RaceNum)
                {
                    race = raceItem;
                    return race;
                }
            }

            race = parsedRaceday.Races.First<Race>();
            raceday.Races.Add(race);
            context.Add(race);

            return race;
        }

        private static Horse FindOrCreateHorse(HorseSenseContext context, Race race, RaceDay parsedRaceday)
        {
            Horse horse = null;

            foreach (var horseItem in race.Horses)
            {
                if (horseItem.PostPos == parsedRaceday.Races.First<Race>().Horses.First<Horse>().PostPos)
                {
                    horse = horseItem;
                    return horse;
                }
            }


            horse = parsedRaceday.Races.First<Race>().Horses.First<Horse>();
            race.Horses.Add(horse);
            context.Add(horse);

            return horse;
        }
    }
}
