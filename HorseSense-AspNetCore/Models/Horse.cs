using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HorseSense_AspNetCore.Models
{
    public class Horse
    {
        public int HorseId { get; set; }
        public string Trainer { get; set; }
        public string Jockey { get; set; }
        public double MorningLine { get; set; }
        public string Name { get; set; }
        public int PostPos { get; set; }
        public int TrackStarts { get; set; }
        public int TrackWins { get; set; }
        public int TStarts { get; set; }
        public int TWins { get; set; }
        public int TPlaces { get; set; }
        public int TShows { get; set; }
        public int LStarts { get; set; }
        public int LWins { get; set; }
        public string TrackWkt { get; set; }
        public double WinPercentage { get; set; }
        public bool Scratch { get; set; }

        public override string ToString()
        {
            return "Horse: " + String.Join(",", PostPos, Name, Jockey, Trainer);
        }
    }
}
