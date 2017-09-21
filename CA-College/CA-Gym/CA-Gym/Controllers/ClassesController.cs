using CA_Gym.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CA_Gym.Controllers
{
    public class ClassesController : Controller
    {
        DAO dao = new DAO();
        // GET: Classes
        public ActionResult Classes()
        {
            List<Class> classList = dao.ShowAllClasses();
            return View(classList);
        }

        [HttpGet]
        public ActionResult AddClass()
        {
            string response = null;
            ViewBag.TrainerList = dao.GetTrainerName();
            //ViewBag.TrainerID = dao.getTrainerIDFromDropDown();
            //response = dao.getTrainerIDFromDropDown();
            //Response.Write(response.Count());
            return View();
        }

        [HttpPost]
        public ActionResult AddClass(Class c)
        {
            string t = Request.Form["TrainerList"].ToString();
            ViewBag.TitleList = dao.GetTrainerName();
            int trainerID = dao.getTrainerIDFromDropDown(t);
            int count = 0;
            if (ModelState.IsValid)
            {
                count = dao.Insert(c, trainerID);
                //Response.Write(dao.message);
                if (count == 1)
                    ViewBag.Status = "Class is created successfully.";
                else
                {
                    ViewBag.Status = "Error! " + dao.message;
                }
                return View("Status");

            }
            return View(c);
        }

        [HttpGet]
        public ActionResult AddBooking()
        {
            ViewBag.ClassList = dao.GetClassType();

            return View();
        }

        [HttpPost]
        public ActionResult AddBooking(Booking b)
        {
            int memberID = dao.getMemberIDFromSession(Session["email"].ToString());

            string c = Request.Form["ClassList"].ToString();
            int classID = dao.getClassIDFromDropDown(c);
            string classTime = dao.getClassTimeFromDropDown(c);

            int count = 0;
            if (ModelState.IsValid)
            {
                count = dao.Insert(b, memberID, classID, classTime);
                //Response.Write(dao.message);
                if (count == 1)
                    ViewBag.Status = "Booking has been made successfully.";
                else
                {
                    ViewBag.Status = "Error! " + dao.message;
                }
                return View("StatusBook");

            }
            return View(b);
        }

        [HttpGet]
        public ActionResult AddPTSession()
        {
            ViewBag.TrainerList = dao.GetTrainerName();
            ViewBag.LocationList = dao.GetGymLocation();
            ViewBag.HourList = new List<int> { 1, 2, 3, 4};
            ViewBag.TypeList = new List<string> {"Strength","Cardio","Yoga","Pilates","Mobility", "Specialised"};

            return View();
        }

        [HttpPost]
        public ActionResult AddPTSession(PTSession pt)
        {
            string t = Request.Form["TrainerList"].ToString();
            string g = Request.Form["LocationList"].ToString();
            string type = Request.Form["TypeList"].ToString();
            string hour = Request.Form["HourList"].ToString();
            int c = int.Parse(hour) * 20;

            int count = 0;

            pt.TrainerID = dao.getTrainerIDFromDropDown(t);
            pt.MemberID = dao.getMemberIDFromSession(Session["email"].ToString());
            pt.SessLocation = g;
            pt.SessionLength = hour + " hour(s)";
            pt.SessType = type;
            pt.Cost = c;

            if (ModelState.IsValid)
            {
                count = dao.Insert(pt);
                //Response.Write(dao.message);
                if (count == 1)
                    ViewBag.Status = "PT Session has been successfully booked.";
                else
                {
                    ViewBag.Status = "Error! " + dao.message;
                }
                return View("StatusPT");

            }
            return View(pt);
        }
    }
}