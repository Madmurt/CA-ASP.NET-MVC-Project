using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CA_Gym.Models
{
    public class PTSession
    {
        public int ptSessionID { get; set; }
        public int trainerID { get; set; }
        public int memberID { get; set; }
        public string sessionLength { get; set; }
        public string sessionDate { get; set; }
        public string sessionTime { get; set; }
        public string sessType { get; set; }
        public decimal cost { get; set; }
        public string sessLocation { get; set; }
    }
}