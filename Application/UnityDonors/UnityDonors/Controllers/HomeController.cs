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
            var registeration = new RegisterationMV();
            ViewBag.UserTypeID = new SelectList(DB.UserTypeTables.ToList(), "UserTypeID", "UserType", "0");
            return View(registeration);
        }
    }
}