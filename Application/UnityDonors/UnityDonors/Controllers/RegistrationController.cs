using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UnityDonors.Models;

namespace UnityDonors.Controllers
{
    public class RegistrationController : Controller
    {
        public ActionResult UserRegistration()
        {
            var registration = new RegisterationMV();
            return View(registration);
        }
    }
}