using CA_Gym.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace CA_Gym.Controllers
{
    public class MemberTypeController : Controller
    {
        DAO dao = new DAO();
        string connString = WebConfigurationManager.ConnectionStrings["conStringLocal"].ConnectionString;

        [Authorize]
        // GET: MembershipType
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult MemberType()
        {
           // ViewBag.MemTypeList = dao.GetMemberType();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MemberType(MemberShipType user)
        {
            
            int count = 0;

            if (ModelState.IsValid)
            {
                count = dao.Insert(user);
                //Response.Write(dao.message);
                if (count == 1)
                    ViewBag.Status = "Membership type has been recorded successfully.";
                else
                {
                    ViewBag.Status = "Error! " + dao.message;
                }
              //  return View("Status");
                return RedirectToAction("Register", "User");

            }
           // return RedirectToAction("Register", "User");  
            return View(user);

        }
    }
}