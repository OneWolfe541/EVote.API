using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EVote.API.Models.Stats
{
    public class VoterCountsModel
    {
        public int TotalVoters { get; set; }
        public int ActiveVoters { get; set; }

        public double ActivePercent 
        { 
            get
            {
                return (((float)ActiveVoters / (float)TotalVoters) * 100);
            }
        }
    }
}
