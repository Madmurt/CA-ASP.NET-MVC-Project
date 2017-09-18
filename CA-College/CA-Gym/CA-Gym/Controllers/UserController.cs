using CA_Gym.Models;
using System;
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
            return View();
        }
        
        // Registration Action
        [HttpGet]
        public ActionResult Register()
        {
            //return View(); 
           return RedirectToAction("MemberType", "MemberType");
        }
        [HttpPost]
        public ActionResult RegisterMemType(MemberShipType memType)
        {
            
            int count = 0;
            if (ModelState.IsValid)
            {
                count += dao.Insert(memType);
                //Response.Write(dao.message);
                if (count == 1)
                    ViewBag.Status = "User is created successfully.";
                else
                {
                    ViewBag.Status = "Error! " + dao.message;
                }
                return View("Status");
            }
            //return View(); 
            return RedirectToAction("Register", "User");
        }

        //Registration Post Action
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Member member) 
        {
            //string t = Request.Form["MemTypeList"]!=null?Request.Form["MemTypeList"].ToString():null;
            //ViewBag.TitleList = dao.GetMemberType();
            //  int memTypeID = dao.getMemTypeIDFromDropDown(t);
            
            int result = dao.GetMemTypeID();
            int count = 0;
            if (ModelState.IsValid)
            {
                count += dao.Insert(member, result);
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
        //[ValidateAntiForgeryToken]
        public ActionResult Login(Member member)  //checking this out
        {
            ModelState.Remove("FirstName");
            ModelState.Remove("LastName");
            ModelState.Remove("Gender");
            ModelState.Remove("Age");
            ModelState.Remove("Phone");
            ModelState.Remove("Address");

            if (ModelState.IsValid)
            {
                Member temp = dao.getMemberObject(member.Email, member.MemPass);
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