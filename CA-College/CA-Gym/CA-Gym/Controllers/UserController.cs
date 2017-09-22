using CA_Gym.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Configuration;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;

namespace CA_Gym.Controllers
{
    public class UserController : Controller
    {
        DAO dao = new DAO();
        string connString = WebConfigurationManager.ConnectionStrings["conStringLocal"].ConnectionString;
        
       
        // GET: User
        public ActionResult Index()
        {
            return View();
        }
        
        // Registration Action
        [HttpGet]
        public ActionResult Register()
        {
            ViewBag.GenderList = new List<string> { "M", "F" };
            return View(); 
        }

        [HttpGet]
        public ActionResult MemberType()
        {
            ViewBag.GymList = dao.GetGymLocation();
            ViewBag.TypeList = new List<string> { "Annual", "Day Pass", "Monthly"};
            return View();
        }

        [HttpPost]
        public ActionResult MemberType(FormCollection form)
        {
            string g = Request.Form["GymList"].ToString();
            string type = Request.Form["TypeList"].ToString();
            MemberShipType memType = new MemberShipType();
            int count = 0;
            if (ModelState.IsValid)
            {
                memType.MemType = type;
                memType.JoinDate = DateTime.Now.ToString();
                string rnDate = null;
                if (type == "Annual")
                {
                    rnDate = DateTime.Now.AddYears(1).ToString();
                }
                else if (type == "Monthly")
                {
                    rnDate = DateTime.Now.AddMonths(1).ToString();
                }
                else
                    rnDate = "na";

                memType.RenewalDate = rnDate;
                memType.GymLocation = g;

                count += dao.Insert(memType);
                //Response.Write(dao.message);
                if (count == 1)
                    ViewBag.Status = "User is created successfully.";
                else
                {
                    ViewBag.Status = "Error! " + dao.message;
                }
                //return View("Status");
                return RedirectToAction("Register", "User");
            }
            return View("Register", memType); 

        }

        //Registration Post Action
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Member member) 
        {
            //string t = Request.Form["MemTypeList"]!=null?Request.Form["MemTypeList"].ToString():null;
            //ViewBag.TitleList = dao.GetMemberType();
            //  int memTypeID = dao.getMemTypeIDFromDropDown(t);

            string g = Request.Form["GenderList"].ToString();
            member.Gender = g;
            int result = dao.GetMemTypeID();
            int count = 0;
            if (ModelState.IsValid)
            {
                count += dao.Insert(member, result);
                //Response.Write(dao.message);
                if (count == 1)
                    return RedirectToAction("Login", "User");
                else
                {
                    ViewBag.Status = "Error! " + dao.message;
                }
                return View("Status");
            }
            return View(member);

        }

        //Login
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Login(Member member)  //checking this out
        {
            ModelState.Remove("FirstName");
            ModelState.Remove("LastName");
            ModelState.Remove("Gender");
            ModelState.Remove("Age");
            ModelState.Remove("Phone");
            ModelState.Remove("Address");
            ModelState.Remove("ConfirmPassword");

            if (ModelState.IsValid)
            {
                Member temp = dao.getMemberObject(member.Email);
                /*
                if (!Crypto.VerifyHashedPassword(temp.MemPass, member.MemPass))
                {
                    temp = null;
                }
                else if (member.MemPass != "12345")
                {
                    temp = null;
                }
                */
                if (temp != null)
                {
                    if (temp.IsAdmin == true)
                    {
                        Session["id"] = "Admin";
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        Session["id"] = "Member"; //replaced "name" with "1".
                        Session["Name"] = temp.FirstName;
                        Session["LastName"] = temp.LastName;
                        Session["email"] = temp.Email;
                        return RedirectToAction("Index", "Home");
                    }
                }
                
                else
                {
                    ViewBag.Status = "Error! " + dao.message;
                    return View("Status");
                }
            }
            else
            {
                return View(member);
            }
        }

        //Logout
      //  [Authorize]
       // [HttpPost]
        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();
            return View("../Home/Index");
             //return RedirectToAction("Index", "Home");
        }

        //WHAT IS THE ?
        public ActionResult Edit(int? memberID, Member member)
        {
            //ModelState.Remove("Age");
            //ModelState.Remove("Phone");
            //ModelState.Remove("Password");

            try
            {
                dao.UpdateMemberDetails(member);
                return RedirectToAction("Home");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult Delete(int memberID)
        {
            try
            {

                if (dao.DeleteMember(memberID))
                {
                    ViewBag.AlertMessage = "Member Deleted Successfully";
                }
                return RedirectToAction("Home");
            }
            catch
            {
                return View();
            }
        }


    }
}