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
            ViewBag.MemberList = dao.GetMemberName();
            ViewBag.ClassList = dao.GetClassType();

            return View();
        }

        [HttpPost]
        public ActionResult AddBooking(Booking b)
        {
            /*string t = Request.Form["MemberList"].ToString();
            int memberID = dao.getMemberIDFromDropDown(t);

            string c = Request.Form["ClassList"].ToString();
            int classID = dao.getClassIDFromDropDown(c);
            string classTime = dao.getClassTimeFromDropDown(c);

            int count = 0;
            if (ModelState.IsValid)
            {
                count = dao.Insert(b, memberID, classID, classTime);
                //Response.Write(dao.message);
                if (count == 1)
                    ViewBag.Status = "Class is created successfully.";
                else
                {
                    ViewBag.Status = "Error! " + dao.message;
                }
                return View("Status");

            }*/
            return View(b);
        }
    }
}