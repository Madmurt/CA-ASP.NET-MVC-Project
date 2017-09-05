using CA_Gym.Models;
using System;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace CA_Gym.Controllers
{
    public class UserController : Controller
    {
        // Registration Action
        [HttpGet]
        public ActionResult Registration()
        {
            return View();
        }

        //Registration Post Action
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registration([Bind(Exclude = "IsEmailVerified, ActivationCode")] User user)
        {
            bool status = false;
            string message = "";

            //Model Validation
            if (ModelState.IsValid)
            {
                //Is Email already exist
                var isExist = IsEmailAddressExist(user.EmailAddress);

                if (isExist)
                {
                    ModelState.AddModelError("EmailExist", "Email already exist");
                    return View(user);
                }

                //Generate activation code
                user.ActivationCode = Guid.NewGuid();

                //Password hashing. We create a Crypto class and a Hash Method. Here, we call the Methodhashing
                user.Password = Crypto.Hash(user.Password);
                user.ConfirmPassword = Crypto.Hash(user.ConfirmPassword); //This is necessary as db validates again on save changes

                user.IsEmailVerified = false;

                //Saving to Database

                using (GymDb db = new GymDb())
                {

                    db.Users.Add(user);
                    db.SaveChanges();

                    //Send Email to user using this Method.
                    SendVerificationLinkEmail(user.EmailAddress, user.ActivationCode.ToString());

                    message = "Registration successfully done. Activation link has been sent to: " + user.EmailAddress;
                    status = true;
                }
            }
            else
            {
                message = "Invalid Request";
            }
            ViewBag.Message = message;
            ViewBag.Status = status;
            return View(user);
        }
        [HttpGet]
        public ActionResult VerifyAccount(string id)
        {
            bool status = false;

            using (GymDb db = new GymDb())
            {
                db.Configuration.ValidateOnSaveEnabled = false; //This line of code helps avoid "Confirm Password" arising when we say save changes.

                var v = db.Users.Where(a => a.ActivationCode == new Guid(id)).FirstOrDefault();
                if (v != null)
                {
                    v.IsEmailVerified = true;
                    db.SaveChanges();
                    status = true;
                }
                else
                {
                    ViewBag.Message = "Invalid Request";
                }
                ViewBag.status = true;
            }
            return View();
        }

        //Login
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserLogin login, string ReturnUrl)
        {
            string message = "";
            using (GymDb db = new GymDb())
            {
                var v = db.Users.Where(a => a.EmailAddress == login.EmailAddress).FirstOrDefault();
                if (v != null)
                {
                    if (string.Compare(Crypto.Hash(login.Password), v.Password) == 0)
                    {
                        int timeOut = login.RememberMe ? 525600 : 20; //525600 minutes = 1 year
                        var ticket = new System.Web.Security.FormsAuthenticationTicket(login.EmailAddress, login.RememberMe, timeOut);
                        string encrypted = FormsAuthentication.Encrypt(ticket);
                        var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrypted);
                        cookie.Expires = DateTime.Now.AddMinutes(timeOut);
                        cookie.HttpOnly = true;
                        Response.Cookies.Add(cookie);

                        if (Url.IsLocalUrl(ReturnUrl))
                        {
                            return Redirect(ReturnUrl);
                        }
                        else
                        {
                            return RedirectToAction("Index", "Home");
                        }


                    }
                    else
                    {
                        message = "Invalid Credentials";
                    }
                }
                else
                {
                    message = "Invalid Credentials";
                }
            }
            ViewBag.Message = message;
            return View();
        }
        //Logout
        [Authorize]
        [HttpPost]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "User");
        }

        //Method for Email verification
        [NonAction]
        public bool IsEmailAddressExist(string emailAddress)
        {
            using (GymDb db = new GymDb())
            {
                var verify = db.Users.Where(a => a.EmailAddress == emailAddress).FirstOrDefault();
                return verify != null;
            }
        }
        //Method for sending verification link and activation code
        [NonAction]
        public void SendVerificationLinkEmail(string emailAddress, string activationCode)
        {
            var verifyUrl = "/User/VerifyAccount/" + activationCode;
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl);

            var fromEmail = new MailAddress("mvcassignment@mail.com", "Mvc Assignment"); //using System.Net.Mail
            var toMail = new MailAddress(emailAddress);
            var fromEmailPassword = "@castlehouse";
            string subject = "Your account is successfully created";
            string body = "<br/><br/>We are delighted to let you know that your MVC Gym account has " +
                           "been successfully created. Please click on the link below to verify your account." +
                           "<br/><br/><a href='" + link + "'>" + link + "</a>";

            var smtp = new SmtpClient
            {
                Host = "smtp.mail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromEmail.Address, fromEmailPassword)
            };
            using (var message = new MailMessage(fromEmail, toMail)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            })
                smtp.Send(message);

        }


    }
}