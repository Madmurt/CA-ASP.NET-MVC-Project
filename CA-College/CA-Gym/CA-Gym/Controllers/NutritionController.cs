using CA_Gym.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CA_Gym.Controllers
{
    public class NutritionController : Controller
    {

        static DataSet ds;
        static DataTable dt;

        //public ActionResult Nutrition()
        //{
        //    return View();
        //}

        [HttpGet]
        public ActionResult NutritionBlog()
        {
            if (System.IO.File.Exists(Server.MapPath("~/App_Data/NutritionBlog.xml")))
            {
                ds = new DataSet();
                ds.ReadXml(Server.MapPath("~/App_Data/NutritionBlog.xml"));
                dt = ds.Tables["user_nutrition"];
            }
            else
            {
                ds = new DataSet("Nutrition");
                dt = new DataTable("user_nutrition");
                DataColumn name_col = new DataColumn("name");
                DataColumn advice_col = new DataColumn("advice");
                dt.Columns.Add(name_col);
                dt.Columns.Add(advice_col);
                ds.Tables.Add(dt);
            }
            return View();
        }

        [HttpPost]
        public ActionResult NutritionBlog(Nutrition nutritionIdea)
        {
            if (ModelState.IsValid)
            {
                DataRow row = dt.NewRow();
                row["name"] = Session["Name"].ToString() + " " + Session["LastName"].ToString();
                row["advice"] = nutritionIdea.Advice;
                dt.Rows.Add(row);
                ds.AcceptChanges();
                ds.WriteXml(Server.MapPath("~/App_Data/NutritionBlog.xml"));
                return View("Advice");
            }

            else return View("Index", nutritionIdea);
        }

        [HttpGet]
        public ActionResult Nutrition()
        {
            List<Nutrition> nutritionIdeasList = new List<Nutrition>();
            if (System.IO.File.Exists(Server.MapPath("~/App_Data/NutritionBlog.xml")))
            {
                DataSet dataSet = new DataSet();
                dataSet.ReadXml(Server.MapPath("~/App_Data/NutritionBlog.xml"));
                DataTable table = dataSet.Tables[0];//dataSet.Tables["user_comments"]
                foreach (DataRow row in table.Rows)
                {
                    Nutrition idea = new Nutrition();
                    idea.Name = row["name"].ToString();
                    idea.Advice = row["advice"].ToString();
                    nutritionIdeasList.Add(idea);
                }
                ViewBag.Message = "";
            }
            else ViewBag.Message = "Blog was not found";

            return View(nutritionIdeasList);
        }
    }
}