using DatabaseLayer;
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
    public class BloodGroupController : Controller
    {
        Unity_DonorEntities DB = new Unity_DonorEntities();


        public ActionResult AllBloodGroups()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            var bloodgroups = DB.BloodGroupsTables.ToList();
            var listbloodgroups = new List<BloodGroupsMV>();

            foreach (var bloodgroup in bloodgroups)
            {
                var addbloodgroup = new BloodGroupsMV();
                addbloodgroup.BloodGroupID = bloodgroup.BloodGroupID;
                addbloodgroup.BloodGroup = bloodgroup.BloodGroup;
                listbloodgroups.Add(addbloodgroup);
            }

            return View(listbloodgroups);
        }
        [HttpGet]
        public ActionResult Create()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            var bloodgroup = new BloodGroupsMV();
            return View(bloodgroup);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BloodGroupsMV bloodGroupsMV)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            if (ModelState.IsValid)
            {
                var checkbloodgroup = DB.BloodGroupsTables.Where(b => b.BloodGroup == bloodGroupsMV.BloodGroup).FirstOrDefault();
                if (checkbloodgroup == null)
                {
                    var bloodGroupsTable = new BloodGroupsTable();
                    bloodGroupsTable.BloodGroupID = bloodGroupsMV.BloodGroupID;
                    bloodGroupsTable.BloodGroup = bloodGroupsMV.BloodGroup;
                    DB.BloodGroupsTables.Add(bloodGroupsTable);
                    DB.SaveChanges();
                    return RedirectToAction("AllBloodGroups");
                }
                else
                {
                    ModelState.AddModelError("BLoodGroup", "Already Exist!");
                }
                
            }

            return View(bloodGroupsMV);
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            var bloodgroup = DB.BloodGroupsTables.Find(id);
            if (bloodgroup == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var bloodgroupseMV = new BloodGroupsMV();
            bloodgroupseMV.BloodGroupID = bloodgroup.BloodGroupID;
            bloodgroupseMV.BloodGroup = bloodgroup.BloodGroup;
            return View(bloodgroupseMV);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BloodGroupsMV bloodgroupsMV)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            if (ModelState.IsValid)
            {
                var checkbloodgroup = DB.BloodGroupsTables.Where(b => b.BloodGroup == bloodgroupsMV.BloodGroup && b.BloodGroupID != bloodgroupsMV.BloodGroupID ).FirstOrDefault();
                if (checkbloodgroup == null)
                {
                    var bloodgroupsTable = new BloodGroupsTable();
                    bloodgroupsTable.BloodGroupID = bloodgroupsMV.BloodGroupID;
                    bloodgroupsTable.BloodGroup = bloodgroupsMV.BloodGroup; 
                    // DB.RequestTypeTables.Add(requestTypeTable);
                    DB.Entry(bloodgroupsTable).State = EntityState.Modified;
                    DB.SaveChanges();
                    return RedirectToAction("AllBloodGroups");
                }
                else
                {
                    ModelState.AddModelError("BLoodGroup", "Already Exist!");
                }
            }
            return View(bloodgroupsMV);
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
            var bloodgroup = DB.BloodGroupsTables.Find(id);
            if (bloodgroup == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var bloodGroupsMV = new BloodGroupsMV();
            bloodGroupsMV.BloodGroupID = bloodgroup.BloodGroupID;
            bloodGroupsMV.BloodGroup = bloodgroup.BloodGroup;
            return View(bloodGroupsMV);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirm(int? id)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            var bloodgroup = DB.BloodGroupsTables.Find(id);
            DB.BloodGroupsTables.Remove(bloodgroup);
            DB.SaveChanges();
            return RedirectToAction("AllBloodGroups");
        }
    }
}