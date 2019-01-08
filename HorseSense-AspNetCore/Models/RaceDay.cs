using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorseSense_AspNetCore.Models
{
    public class RaceDay
    {
        public int RaceDayId { get; set; }

        public string Track { get; set; }
        public string Date { get; set; }

        public ICollection<Race> Races { get; set; }

        public RaceDay() => Races = new List<Race>();

        public override string ToString()
        {
            var RaceDayString = new StringBuilder("RaceDay: " + String.Join(",", Track, Date));
            foreach (var race in Races)
            {
                RaceDayString.AppendFormat("{0}  {1}", Environment.NewLine, race);
            }
            return RaceDayString.ToString();
        }
    }
}
