using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CA_Gym.Models
{
    public class Class
    {
        public int ClassID { get; set; }
        public int TrainerID { get; set; }
        public string Time { get; set; }
        public string ClassType { get; set; }
        public string Location { get; set; }
        public int MaxMembers { get; set; }

        public Class()
        {


        }
        public Class(int cID, int tID, string t, string cType, string l, int maxM)
        {
            ClassID = cID;
            TrainerID = tID;
            Time = t;
            ClassType = cType;
            Location = l;
            MaxMembers = maxM;
        }
    }
}