using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CA_Gym.Models
{
    public class Booking
    {
        public int BookingID { get; set; }
        public int ClassID { get; set; }
        public int MemberID { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }

        public Booking()
        {

        }

        public Booking(int bID, int cID, int mID, string d, string t)
        {
            BookingID = bID;
            ClassID = cID;
            MemberID = mID;
            Date = d;
            Time = t;
        }
    }

}