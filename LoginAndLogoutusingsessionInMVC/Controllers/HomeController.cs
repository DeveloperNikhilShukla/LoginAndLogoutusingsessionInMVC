using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LoginAndLogoutusingsessionInMVC.Models;
using LoginAndLogoutusingsessionInMVC.Controllers;
using System.Data;
using OnlineMedicalShopingApplication.Controllers;

namespace LoginAndLogoutusingsessionInMVC.Controllers
{
    public class HomeController : Controller
    {
        private Logic con = new Logic();
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(User _User)
        {
            DataSet dt = con.GetRecordWithDataset("exec sp_LoginUser 'nikhilshuklaiarmy@gmail.com','NIKhil1@'");

            
            if(Convert.ToInt32(dt.Tables[0].Rows[0]["message"]) == 0) // 0 means user not found
            {
                return View();
            }

            else
            {
                Session["Id"] = (dt.Tables[0].Rows[0]["id"]);
                Session["Email"]= (dt.Tables[0].Rows[0]["Email"]);
                return View("About");
            }
           


            return View();
        }
        
        public ActionResult Logout()
        {
            Session.Abandon();
            return View("index");
        }
        
        
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}