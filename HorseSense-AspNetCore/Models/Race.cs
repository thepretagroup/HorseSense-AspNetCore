using System;
using System.Collections.Generic;
using System.Text;

namespace HorseSense_AspNetCore.Models
{
    public class Race
    {
        public int RaceId { get; set; }

        public int Distance { get; set; }  // in yards
        public int Purse { get; set; }
        public string Sex { get; set; }
        public string Statebred { get; set; }
        public int ClaimingPrice { get; set; }
        public int RaceNum { get; set; }
        public int BRISPar { get; set; }
        public string Surface { get; set; }
        public string RaceType { get; set; }
        public string RaceConditions { get; set; }

        public List<Horse> Horses { get; set; }

        public Race() => Horses = new List<Horse>();

        public override string ToString()
        {
            var raceString = new StringBuilder("Race: " + String.Join(",", RaceNum, Surface, Distance));
            foreach (var horse in Horses)
            {
                raceString.AppendFormat("{0}  {1}", Environment.NewLine, horse);
            }
            return raceString.ToString();
        }
    }
}
