using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CA_Gym.Models
{
    public class Booking
    {
        public int bookingID { get; set; }
        public int classID { get; set; }
        public int memberID { get; set; }
        public string date { get; set; }
        public string time { get; set; }
    }
}