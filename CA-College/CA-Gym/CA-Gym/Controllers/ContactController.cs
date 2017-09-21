using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CA_Gym.Controllers
{
    public class ContactController : Controller
    {
        static DataSet ds;
        static DataTable dt;

        public ActionResult Contact()
        {
            if (System.IO.File.Exists(Server.MapPath("~/App_Data/contact.xml")))
            {
                ds = new DataSet();
                ds.ReadXml(Server.MapPath("~/App_Data/contact.xml"));
                dt = ds.Tables["user_contact"];
            }
            else
            {
                ds = new DataSet("contact");
                dt = new DataTable("user_contact");
                DataColumn name_col = new DataColumn("name");
                DataColumn email_col = new DataColumn("email");
                DataColumn comments_col = new DataColumn("comments");
                dt.Columns.Add(name_col);
                dt.Columns.Add(email_col);
                dt.Columns.Add(comments_col);
                ds.Tables.Add(dt);
            }
            return View();
        }

        [HttpPost]
        public ActionResult Contact(string name, string email, string comments)
        {
            if (ModelState.IsValid)
            {
                DataRow row = dt.NewRow();
                row["name"] = name;
                row["email"] = email;
                row["comments"] = comments;
                dt.Rows.Add(row);
                ds.AcceptChanges();
                ds.WriteXml(Server.MapPath("~/App_Data/contact.xml"));
                return View("StatusC");
            }

            else return View("Index");
        }
    }
}