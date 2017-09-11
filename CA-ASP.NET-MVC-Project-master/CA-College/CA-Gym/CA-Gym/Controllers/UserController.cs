using CA_Gym.Models;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Configuration;
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
            return View("Register");
        }
        // Registration Action
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        //Registration Post Action
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Member member)
        {
            int count = 0;
            if (ModelState.IsValid)
            {
                        count = dao.Insert(member);
                        //Response.Write(dao.message);
                        if (count == 1)
                            ViewBag.Status = "User is created successfully.";
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
        [ValidateAntiForgeryToken]
        public ActionResult Login(Member member)
        {
            ModelState.Remove("FirstName");
            ModelState.Remove("LastName");
            ModelState.Remove("Gender");
            ModelState.Remove("Age");
            ModelState.Remove("Phone");
            ModelState.Remove("Address");

            if(ModelState.IsValid)
            {
                if(member.MemberRole == Role.Admin &&
                    member.Email == "admin@jimsgym.ie" &&
                    member.Password == "admin")
                {
                    Session["name"] = "Admin";
                    return RedirectToAction("Index", "Admin");
                }
                else if (member.MemberRole == Role.Member)
                {
                    member.FirstName = dao.CheckLogin(member);
                    if(member.FirstName != null)
                    {
                        Session["name"] = member.FirstName;
                        Session["email"] = member.Email;
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ViewBag.Status = "Error! " + dao.message;
                        return View("Status");
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
        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();
            return View(".../Home/Index");
        }
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
               
                if(dao.DeleteMember(memberID))
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