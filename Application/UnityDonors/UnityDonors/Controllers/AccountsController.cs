﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UnityDonors.Models;
using DatabaseLayer;

namespace UnityDonors.Controllers
{
    public class AccountsController : Controller
    {
        Unity_DonorEntities DB = new Unity_DonorEntities();
        public ActionResult AllNewUserRequests()
        { 
            var users = DB.UserTables.Where(u => u.AccountStatusID == 1).ToList();

            return View(users);
        }

        public ActionResult UserDetails(int? id)
        {
            var user = DB.UserTables.Find(id);
            return View(user);
        }
        public ActionResult UserApproved(int? id)
        {
            var user = DB.UserTables.Find(id);
            user.AccountStatusID = 2;
            DB.Entry(user).State = System.Data.Entity.EntityState.Modified;
            DB.SaveChanges();
            return RedirectToAction("AllNewUserRequests");
        }

        public ActionResult UserRejected(int? id)
        {
            var user = DB.UserTables.Find(id);
            user.AccountStatusID = 3;
            DB.Entry(user).State = System.Data.Entity.EntityState.Modified;
            DB.SaveChanges();
            return RedirectToAction("AllNewUserRequests");
        }
    }
}