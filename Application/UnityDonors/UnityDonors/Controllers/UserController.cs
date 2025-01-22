using DatabaseLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UnityDonors.Controllers
{
    public class UserController : Controller
    {
        Unity_DonorEntities DB = new Unity_DonorEntities();
        // GET: User
        public ActionResult UserProfile(int? id)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            var user = DB.UserTables.Find(id);
            return View(user);
        }
    }
}