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
            return View();
        }

        [HttpGet]
        public ActionResult AddClass()
        {
            ViewBag.TrainerList = dao.GetTrainerName();
            return View();
        }

        [HttpPost]
        public ActionResult AddClass(Class c)
        {
            //ViewBag.TitleList = dao.GetTrainerName();

            int count = 0;
            if (ModelState.IsValid)
            {
                count = dao.Insert(c);
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
    }
}