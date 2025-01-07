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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SelectUser(RegisterationMV registerationMV)
        {
            if (registerationMV.UserTypeID == 2)
            {
                return RedirectToAction("DonorUser");
            }
            else if(registerationMV.UserTypeID == 6)
            {
                return RedirectToAction("SeekerUser");
            }
            else if (registerationMV.UserTypeID == 1 || registerationMV.UserTypeID == 4)
            {
                return RedirectToAction("HospitalUser");
            }

            else if (registerationMV.UserTypeID == 5)
            {
                return RedirectToAction("BloodBankUser");
            }
            else
            {
                return RedirectToAction("MainHome", "Home");
            }
            var registration = new RegisterationMV();
            return View(registration);
        }

        public ActionResult HospitalUser()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult HospitalUser(HospitalMV hospitalMV)
        {
            return View();
        }

        public ActionResult DonorUser()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DonorUser(DonorMV donorMV)
        {
            return View();
        }

        public ActionResult BloodBankUser()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BloodBankUser(BloodBankMV bloodbankMV)
        {
            return View();
        }

        public ActionResult SeekerUser()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SeekerUser(SeekerMV seekerMV)
        {
            return View();
        }
    }
}