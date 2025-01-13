﻿using DatabaseLayer;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using UnityDonors.Models;

namespace UnityDonors.Controllers
{
    public class UserTypeController : Controller
    {
        Unity_DonorEntities DB = new Unity_DonorEntities();


        public ActionResult AllUserTypes()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            var usertypes = DB.UserTypeTables.ToList();
            var listusertypes = new List<UserTypeMV>();

            foreach (var usertype in usertypes)
            {
                var addusertype = new UserTypeMV();
                addusertype.UserTypeID = usertype.UserTypeID;
                addusertype.UserType = usertype.UserType;
                listusertypes.Add(addusertype);

            }

            return View(listusertypes);
        }

        public ActionResult Create()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            var usertype = new UserTypeMV();
            return View(usertype);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserTypeMV userTypeMV)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            if (ModelState.IsValid)
            {
                var userTypeTable = new UserTypeTable();
                userTypeTable.UserTypeID = userTypeMV.UserTypeID;
                userTypeTable.UserType = userTypeMV.UserType;
                DB.UserTypeTables.Add(userTypeTable);
                DB.SaveChanges();
                return RedirectToAction("AllUserTypes");
            }

            return View(userTypeMV);
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            var usertype = DB.UserTypeTables.Find(id);
            if (usertype == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var usertypemv = new UserTypeMV();
            usertypemv.UserTypeID = usertype.UserTypeID;
            usertypemv.UserType =  usertypemv.UserType;
            return View(usertypemv);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserTypeMV userTypeMV)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            if (ModelState.IsValid)
            {
                var userTypeTable = new UserTypeTable();
                userTypeTable.UserTypeID = userTypeMV.UserTypeID;
                userTypeTable.UserType = userTypeMV.UserType;
                // DB.RequestTypeTables.Add(userTypeTable);
                DB.Entry(userTypeTable).State = EntityState.Modified;
                DB.SaveChanges();
                return RedirectToAction("AllUserTypes");
            }
            return View(userTypeMV);
        }

        public ActionResult Delete(int? id)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            if (id == null)
            {
                return HttpNotFound();
            }
            var usertype = DB.UserTypeTables.Find(id);
            if (usertype == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var usertypemv = new UserTypeMV();
            usertypemv.UserTypeID = usertype.UserTypeID;
            usertypemv.UserType = usertype.UserType;
            return View(usertypemv);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirm(int? id)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            var usertype = DB.UserTypeTables.Find(id);
            DB.UserTypeTables.Remove(usertype);
            DB.SaveChanges();
            return RedirectToAction("AllUserTypes");
        }
    }
}