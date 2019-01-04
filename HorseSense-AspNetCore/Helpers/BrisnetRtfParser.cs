using HorseSense_AspNetCore.Models;
using System;

namespace HorseSense_AspNetCore.Helpers
{
    public class BrisnetRtfParser
    {
        private const int EXPECTED_ELEMENT_COUNT = 1435;
        ////////        //*** Today's Race Data ***
        ////////        public string Track { get; set; }
        ////////        Date { get; set; }
        ////////    RaceNumber             { get; set; }
        ////////PostPosition { get; set; }
        ////////        Entry { get; set; }
        ////////        Distance  { get; set; }   // (in yards)
        ////////        Surface { get; set; }
        ////////        Reserved { get; set; }
        ////////        RaceType { get; set; }
        ////////        AgeSexRestrictions { get; set; }
        ////////        RaceClassification { get; set; }
        ////////        Purse { get; set; }
        ////////        ClaimingPrice { get; set; }
        ////////        ClaimingPriceOfHorse { get; set; }
        ////////        TrackRecord { get; set; }
        ////////        RaceConditions { get; set; }


        public static RaceDay ParseLine(string line)
        {
            var elements = line.Split(',');
            Console.WriteLine("Parsed line has {0} elements", elements.Length);
            if (elements.Length < EXPECTED_ELEMENT_COUNT -1)
            {
                Console.WriteLine("!!!! BrisnetRtfParser.ParseLine:  Invalid line. Line only contains {0} elements, expected {1} !!!!", elements.Length, EXPECTED_ELEMENT_COUNT);
            }

            var raceDay = new RaceDay
            {
                Track = elements[0],
                Date = elements[1]
            };

            var race = new Race();
            {
                race.RaceNum = int.Parse(elements[2]);
                race.Distance = int.Parse(elements[5]);
                race.Surface = elements[6];
                race.RaceType = elements[8];
                race.Sex = elements[9].Substring(2, 1);
                race.Purse = int.Parse(elements[11]);
                int.TryParse(elements[12], out int claimingPrice);
                race.ClaimingPrice = claimingPrice;
                race.RaceConditions = elements[15];
            };
            raceDay.Races.Add(race);

            //////public int TrackStarts { get; set; }
            //////public int TrackWins { get; set; }
            //////public int TStarts { get; set; }
            //////public int TWins { get; set; }
            //////public int TPlaces { get; set; }
            //////public int TShows { get; set; }
            //////public int LStarts { get; set; }
            //////public int LWins { get; set; }
            //////public string TrackWkt { get; set; }
            //////public double WinPercentage { get; set; }        
            ///////public bool Scratch { get; set; }

            var horse = new Horse
            {
                PostPos = int.Parse(elements[3]),
                Trainer = elements[27],
                Jockey = elements[34],
                MorningLine = Double.Parse(elements[43]),
                Name = elements[44]
            };
            race.Horses.Add(horse);

            return raceDay;

        }
    }
}
