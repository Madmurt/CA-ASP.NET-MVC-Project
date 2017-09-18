using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CA_Gym.Models
{
    public class MemberShipType
    {
        public int MemTypeID { get; set; }
        public string MemType { get; set; }
        public string JoinDate { get; set; }
        public string RenewalDate { get; set; }
        public string GymLocation { get; set; }

        public MemberShipType()
        {


        }
        public MemberShipType(int memTypeID, string memType, string joinDate, string renewalDate, string gymLocation)
        {
            MemTypeID = memTypeID;
            MemType = memType;
            JoinDate = joinDate;
            RenewalDate = renewalDate;
            GymLocation = gymLocation;
        }
    }
}