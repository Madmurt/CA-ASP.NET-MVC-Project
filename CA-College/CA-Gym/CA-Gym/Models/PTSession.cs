using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CA_Gym.Models
{
    public class PTSession
    {
        public int PtSessionID { get; set; }
        public int TrainerID { get; set; }
        public int MemberID { get; set; }
        public string SessionLength { get; set; }
        public string SessionDate { get; set; }
        public string SessionTime { get; set; }
        public string SessType { get; set; }
        public decimal Cost { get; set; }
        public string SessLocation { get; set; }

        public PTSession()
        {

        }

        public PTSession(int ptSessionID, int trainerID, int memberID, string sessionLength, string sessionDate, string sessionTime, string sessType, decimal cost, string sessLocation)
        {
            PtSessionID = ptSessionID;
            TrainerID = trainerID;
            MemberID = memberID;
            SessionLength = sessionLength;
            SessionDate = sessionDate;
            SessionTime = sessionTime;
            SessType = sessType;
            Cost = cost;
            SessLocation = sessLocation;
        }
    }
}