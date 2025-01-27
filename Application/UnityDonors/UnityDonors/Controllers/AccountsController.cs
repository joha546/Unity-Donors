using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UnityDonors.Models;
using DatabaseLayer;
using System.Drawing;

namespace UnityDonors.Controllers
{
    public class AccountsController : Controller
    {
        Unity_DonorEntities DB = new Unity_DonorEntities();
        public ActionResult AllNewUserRequests()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            var users = DB.UserTables.Where(u => u.AccountStatusID == 1).ToList();

            return View(users);
        }

        public ActionResult UserDetails(int? id)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            var user = DB.UserTables.Find(id);
            return View(user);
        }
        public ActionResult UserApproved(int? id)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            var user = DB.UserTables.Find(id);
            user.AccountStatusID = 2;
            DB.Entry(user).State = System.Data.Entity.EntityState.Modified;
            DB.SaveChanges();
            return RedirectToAction("AllNewUserRequests");
        }

        public ActionResult UserRejected(int? id)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            var user = DB.UserTables.Find(id);
            user.AccountStatusID = 3;
            DB.Entry(user).State = System.Data.Entity.EntityState.Modified;
            DB.SaveChanges();
            return RedirectToAction("AllNewUserRequests");
        }

        public ActionResult AddNewDonorByBloodBank()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }

            var collectBloodMV = new CollectBloodMV();
            ViewBag.CityID = new SelectList(DB.CityTables.ToList(), "CityID", "City", "0");
            ViewBag.BloodGroupID = new SelectList(DB.BloodGroupsTables.ToList(), "BloodGroupID", "BloodGroup", "0");
            ViewBag.GenderID = new SelectList(DB.GenderTables.ToList(), "GenderID", "Gender", "0");
            return View(collectBloodMV);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddNewDonorByBloodBank(CollectBloodMV collectBloodMV)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }

            int bloodbankID = 0;
            string bloodbankid = Convert.ToString(Session["BloodBankID"]);
            int.TryParse(bloodbankid, out bloodbankID);

            var currentdate = DateTime.Now.Date;
            var currentcampaign = DB.CampaignTables.Where(c => c.CampaignDate == currentdate && c.BloodBankID == bloodbankID).FirstOrDefault();

            if (ModelState.IsValid)
            {
                using (var transaction = DB.Database.BeginTransaction())
                {
                    try
                    {
                        var checkdonor = DB.DonorTables.Where(d => d.CNIC.Trim().Replace("-", "") == collectBloodMV.DonorDetails.CNIC.Trim().Replace("-", "")).FirstOrDefault();
                        if (checkdonor == null)
                        {
                            var user = new UserTable();
                            user.UserName = collectBloodMV.DonorDetails.FullName.Trim();
                            user.Password = "12345";
                            user.EmailAddress = collectBloodMV.DonorDetails.EmailAddress;
                            user.AccountStatusID = 2;
                            user.UserTypeID = 2;
                            user.Description = "Added by BloodBank";

                            DB.UserTables.Add(user);
                            DB.SaveChanges();

                            var donor = new DonorTable();

                            donor.FullName = collectBloodMV.DonorDetails.FullName.Trim();
                            donor.BloodGroupID = collectBloodMV.BloodGroupID;
                            donor.Location = collectBloodMV.DonorDetails.Location;
                            donor.ContactNo = collectBloodMV.DonorDetails.ContactNo;
                            donor.LastDonationID = DateTime.Now;
                            donor.CNIC = collectBloodMV.DonorDetails.CNIC;
                            donor.GenderID = collectBloodMV.GenderID;
                            donor.CityID = collectBloodMV.CityID;
                            donor.UserID = user.UserID;

                            DB.DonorTables.Add(donor);
                            DB.SaveChanges();

                            checkdonor = DB.DonorTables.Where(d => d.CNIC.Trim().Replace("-", "") == collectBloodMV.DonorDetails.CNIC.Trim().Replace("-", "")).FirstOrDefault();
                        }

                        if ((DateTime.Now - checkdonor.LastDonationID).TotalDays < 120)
                        {
                            ModelState.AddModelError(string.Empty, "Donor has already donated blood within 120 days.");
                            transaction.Rollback();
                        }
                        else
                        {
                            var checkbloodgroupstock = DB.BloodBankStockTables.Where(s => s.BloodBankID == bloodbankID
                            && s.BloodGroupID == collectBloodMV.BloodGroupID).FirstOrDefault();

                            if (checkbloodgroupstock == null)
                            {
                                var bloodbankStock = new BloodBankStockTable();
                                bloodbankStock.BloodBankID = bloodbankID;
                                bloodbankStock.BloodGroupID = collectBloodMV.BloodGroupID;
                                bloodbankStock.Quantity = 0;
                                bloodbankStock.Status = true;
                                bloodbankStock.Description = "";

                                DB.BloodBankStockTables.Add(bloodbankStock);
                                DB.SaveChanges();

                                checkbloodgroupstock = DB.BloodBankStockTables.Where(s => s.BloodBankID == bloodbankID
                                && s.BloodGroupID == collectBloodMV.BloodGroupID).FirstOrDefault();
                            }
                            checkbloodgroupstock.Quantity += collectBloodMV.Quantity;
                            DB.Entry(checkbloodgroupstock).State = System.Data.Entity.EntityState.Modified;
                            DB.SaveChanges();

                            var colllectblooddetail = new BloodBankStockDetailTable();
                            colllectblooddetail.BloodBankStockID = checkbloodgroupstock.BloodBankStockID;
                            colllectblooddetail.BloodGroupID = collectBloodMV.BloodGroupID;
                            colllectblooddetail.CampaignID = currentcampaign.CampaignID;
                            colllectblooddetail.Quantity = collectBloodMV.Quantity + 1;
                            colllectblooddetail.DonorID = checkdonor.DonorID;
                            colllectblooddetail.DonateDateTime = DateTime.Now;

                            DB.BloodBankStockDetailTables.Add(colllectblooddetail);
                            DB.SaveChanges();

                            // update donor's last donation date to keep update.
                            checkdonor.LastDonationID = DateTime.Now;
                            DB.Entry(checkdonor).State = System.Data.Entity.EntityState.Modified;
                            DB.SaveChanges();
                            transaction.Commit();

                            return RedirectToAction("BloodBankStock", "BloodBank");
                        }
                    }
                    catch
                    {
                        ModelState.AddModelError(string.Empty, "Please Provide Correct Information!");
                        transaction.Rollback();
                    }

                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Please Provide Donor Full Details!");
            }
            ViewBag.CityID = new SelectList(DB.CityTables.ToList(), "CityID", "City", collectBloodMV.CityID);
            ViewBag.BloodGroupID = new SelectList(DB.BloodGroupsTables.ToList(), "BloodGroupID", "BloodGroup", collectBloodMV.BloodGroupID);
            ViewBag.GenderID = new SelectList(DB.GenderTables.ToList(), "GenderID", "Gender", collectBloodMV.GenderID);
            return View(collectBloodMV);
        }
    }
}