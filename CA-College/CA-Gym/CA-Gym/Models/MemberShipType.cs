using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CA_Gym.Models
{
    public class MemberShipType
    {
        public int memTypeID { get; set; }
        public string memType { get; set; }
        public string joinDate { get; set; }
        public string renewalDate { get; set; }
        public string gymLocation { get; set; }
    }
}