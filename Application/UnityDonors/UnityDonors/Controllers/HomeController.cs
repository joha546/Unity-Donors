using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UnityDonors.Models;
using DatabaseLayer;

namespace UnityDonors.Controllers
{
    public class HomeController : Controller
    {
        Unity_DonorEntities DB = new Unity_DonorEntities();
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult MainHome()
        {
            var message = ViewData["Message"] == null? "Welcome to Unity Donors." : ViewData["Message"];
            ViewData["Message"] = message;
            var registeration = new RegisterationMV();
            ViewBag.UserTypeID = new SelectList(DB.UserTypeTables.Where(ut => ut.UserTypeID > 1).ToList(), "UserTypeID", "UserType", "0");
            ViewBag.CityID = new SelectList(DB.CityTables.ToList(), "CityID", "City", "0");
            return View(registeration);
        }

        public ActionResult Login()
        {
            var usermv = new UserMV();
            return View(usermv);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserMV userMV)
        {
            return View(userMV);
        }
    }
}