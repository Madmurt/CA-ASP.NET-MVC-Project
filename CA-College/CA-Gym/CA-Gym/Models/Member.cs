using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CA_Gym.Models
{
    public class Member
    {
        public int memTypeID { get; set; }
        public string email { get; set; }
        public string memPass { get; set; }
        public string firstName { get; set; }
        public string MyProperty { get; set; }
    }
}