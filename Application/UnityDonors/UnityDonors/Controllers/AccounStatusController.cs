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
    public class AccounStatusController : Controller
    {
        Unity_DonorEntities DB = new Unity_DonorEntities();


        public ActionResult AllAccountStatus()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            var accountstatuses = DB.AccountStatusTables.ToList();
            var listaccountstatuses = new List<AccountStatusMV>();

            foreach (var accountStatus in accountstatuses)
            {
                var accountStatusmv = new AccountStatusMV();
                accountStatusmv.AccountStatusID = accountStatus.AccountStatusID;
                accountStatusmv.AccountStatus = accountStatus.AccountStatus;
                listaccountstatuses.Add(accountStatusmv);
            }

            return View(listaccountstatuses);
        }
        [HttpGet]
        public ActionResult Create()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            var accountStatus = new AccountStatusMV();
            return View(accountStatus);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AccountStatusMV accountStatusMV)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            if (ModelState.IsValid)
            {
                var checkaccountstatus = DB.AccountStatusTables.Where(b => b.AccountStatus == accountStatusMV.AccountStatus).FirstOrDefault();
                if (checkaccountstatus == null)
                {
                    var accountstatusesTable = new AccountStatusTable();
                    accountstatusesTable.AccountStatusID = accountStatusMV.AccountStatusID;
                    accountstatusesTable.AccountStatus = accountStatusMV.AccountStatus;
                    DB.AccountStatusTables.Add(accountstatusesTable);
                    DB.SaveChanges();
                    return RedirectToAction("AllAccountStatus");
                }
                else
                {
                    ModelState.AddModelError("AccountStatus", "Already Exist!");
                }
            }

            return View(accountStatusMV);
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            var accountStatus = DB.AccountStatusTables.Find(id);
            if (accountStatus == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var accountstatusesemv = new AccountStatusMV();
            accountstatusesemv.AccountStatusID = accountStatus.AccountStatusID;
            accountstatusesemv.AccountStatus = accountStatus.AccountStatus;
            return View(accountstatusesemv);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AccountStatusMV accountStatusMV)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            if (ModelState.IsValid)
            {
                var checkaccountstatus = DB.AccountStatusTables.Where(b => b.AccountStatus == accountStatusMV.AccountStatus && b.AccountStatusID != accountStatusMV.AccountStatusID).FirstOrDefault();
                if (checkaccountstatus == null)
                {
                    var accountStatusTable = new AccountStatusTable();
                    accountStatusTable.AccountStatusID = accountStatusMV.AccountStatusID;
                    accountStatusTable.AccountStatus = accountStatusMV.AccountStatus;
                    // DB.RequestTypeTables.Add(requestTypeTable);
                    DB.Entry(accountStatusTable).State = EntityState.Modified;
                    DB.SaveChanges();
                    return RedirectToAction("AllAccountStatus");
                }
                else
                {
                    ModelState.AddModelError("AccountStatus", "Already Exist!");
                }
            }
            return View(accountStatusMV);
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
            var accountStatus = DB.AccountStatusTables.Find(id);
            if (accountStatus == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var accountStatusMV = new AccountStatusMV();
            accountStatusMV.AccountStatusID = accountStatus.AccountStatusID;
            accountStatusMV.AccountStatus = accountStatus.AccountStatus;
            return View(accountStatusMV);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirm(int? id)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            var accountStatus = DB.AccountStatusTables.Find(id);
            DB.AccountStatusTables.Remove(accountStatus);
            DB.SaveChanges();
            return RedirectToAction("AllAccountStatus");
        }
    }
}