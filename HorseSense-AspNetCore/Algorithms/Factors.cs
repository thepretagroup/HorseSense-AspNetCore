using HorseSense_AspNetCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HorseSense_AspNetCore.Algorithms
{
    public class Factor
    {
    }

    public class HorseFactor
    {
        private Horse Horse { get; set; }

        public bool RailDebut()
        {
            return (Horse.PostPos == 1 /* && Horse.Maiden */);
        }

    }

    public class RaceFactor
    {
        private Race Race { get; set; }

        public bool Sharp { get; set; }
        public double MidRaceSpeed { get; set; }
        public string OkFresh { get; set; }

        public void CalculateStaticFactors()
        {
            // MidRaceSpeed = Race.Distance * other stuff;
            // OkFresh = Days since last 
        }

        public bool IsTurf()
        {
            return string.Compare(Race.Surface, "T", StringComparison.OrdinalIgnoreCase) == 0;
        }

    }

    public class RaceDayFactor
    {
        // ex: Trifecta picks
    }

}
