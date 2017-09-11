using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CA_Gym.Models;

namespace CA_Gym.Controllers
{
    public class TrainersController : Controller
    {
        DAO dao = new DAO();
        // GET: Trainers
        public ActionResult Trainers()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AddTrainer()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddTrainer(Trainer trainer)
        {
            int count = 0;
            if (ModelState.IsValid)
            {
                count = dao.Insert(trainer);
                //Response.Write(dao.message);
                if (count == 1)
                    ViewBag.Status = "User is created successfully.";
                else
                {
                    ViewBag.Status = "Error! " + dao.message;
                }
                return View("Status");
            }
            return View(trainer);
        }
    }
}