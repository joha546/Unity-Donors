﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DatabaseLayer;
using System.Data.Entity;
using System.Net;
using UnityDonors.Models;

namespace UnityDonors.Controllers
{
    public class RequestTypeController : Controller
    {
        Unity_DonorEntities DB = new Unity_DonorEntities();
        

        public ActionResult AllRequestType()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            var requesttype = DB.RequestTypeTables.ToList();
            var listrequesttype = new List<RequestTypeMV>();

            foreach(var requestType in requesttype)
            {
                var addrequesttype = new RequestTypeMV();
                addrequesttype.RequestTypeID = requestType.RequestTypeID;
                addrequesttype.RequestType = requestType.RequestType;
                listrequesttype.Add(addrequesttype);
            }

            return View(listrequesttype);
        }
        [HttpGet]
        public ActionResult Create()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            var requesttype = new RequestTypeMV();
            return View(requesttype);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RequestTypeMV requestTypeMV)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            if (ModelState.IsValid)
            {
                var requestTypeTable = new RequestTypeTable();
                requestTypeTable.RequestTypeID = requestTypeMV.RequestTypeID;
                requestTypeTable.RequestType = requestTypeMV.RequestType;
                DB.RequestTypeTables.Add(requestTypeTable);
                DB.SaveChanges();
                return RedirectToAction("AllRequestType");
            }

            return View(requestTypeMV);
        }

        [HttpGet]
        public ActionResult Edit(int? id) {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }

            var requesttype = DB.RequestTypeTables.Find(id);
            if(requesttype == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var requesttypemv = new RequestTypeMV();
            requesttypemv.RequestTypeID = requesttype.RequestTypeID; 
            requesttypemv.RequestType = requesttype.RequestType;
            return View(requesttypemv);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(RequestTypeMV requestTypeMV)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            if (ModelState.IsValid)
            {
                var requestTypeTable = new RequestTypeTable();
                requestTypeTable.RequestTypeID = requestTypeMV.RequestTypeID;
                requestTypeTable.RequestType = requestTypeMV.RequestType;
                DB.RequestTypeTables.Add(requestTypeTable);
                DB.Entry(requestTypeTable).State = EntityState.Modified;
                DB.SaveChanges();
                return RedirectToAction("AllRequestType");
            }
            return View(requestTypeMV);
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
            var requesttype = DB.RequestTypeTables.Find(id);
            if (requesttype == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var requesttypemv = new RequestTypeMV();
            requesttypemv.RequestTypeID = requesttype.RequestTypeID;
            requesttypemv.RequestType = requesttypemv.RequestType;
            return View(requesttypemv);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirm(int? id)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            var requesttype = DB.RequestTypeTables.Find(id);
            DB.RequestTypeTables.Remove(requesttype);
            DB.SaveChanges();
            return RedirectToAction("AllRequestType");
        }
    }
}