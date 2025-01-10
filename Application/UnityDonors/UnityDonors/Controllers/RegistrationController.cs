﻿using DatabaseLayer;
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
        Unity_DonorEntities DB = new Unity_DonorEntities();

        static RegisterationMV registerationmv;

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SelectUser(RegisterationMV registerationMV)
        {
            registerationmv = registerationMV;
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
           
        }

        public ActionResult HospitalUser()
        {
            ViewBag.CityID = new SelectList(DB.CityTables.ToList(), "CityID", "City", registerationmv.CityID);
            return View(registerationmv);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult HospitalUser(RegisterationMV registerationMV)
        {
            if (ModelState.IsValid)
            {
                var checktitle = DB.HospitalTables.Where(h => h.FullName == registerationMV.Hospital.FullName.Trim()).FirstOrDefault();
                if (checktitle == null)
                {
                    using (var transaction = DB.Database.BeginTransaction())
                    {
                        try
                        {
                            var user = new UserTable();
                            user.UserName = registerationMV.User.UserName;
                            user.Password = registerationMV.User.Password;
                            user.EmailAddress = registerationMV.User.EmailAddress;
                            user.AccountStatusID = 1;
                            user.UserTypeID = registerationMV.UserTypeID;
                            user.Description = registerationMV.User.Description;

                            DB.UserTables.Add(user);
                            DB.SaveChanges();

                            var hospital = new HospitalTable();

                            hospital.FullName = registerationMV.Hospital.FullName;
                            hospital.Address = registerationMV.Hospital.Address;
                            hospital.PhoneNo = registerationMV.Hospital.PhoneNo;
                            hospital.Website = registerationMV.Hospital.Website;
                            hospital.Email = registerationMV.Hospital.Email;
                            hospital.Location = registerationMV.Hospital.Address;
                            hospital.CityID = registerationMV.CityID;
                            hospital.UserID = user.UserID;

                            DB.HospitalTables.Add(hospital);
                            DB.SaveChanges();
                            transaction.Commit();
                            ViewData["Message"] = "Thanks for Registration, Your Query will be reviewed shortly!";
                            return RedirectToAction("MainHome", "Home");
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
                    ModelState.AddModelError(string.Empty, "Hospital Already Existed.");
                }

            }
            ViewBag.CityID = new SelectList(DB.CityTables.ToList(), "CityID", "City", registerationMV.CityID);
            return View(registerationMV);
        }

        public ActionResult DonorUser()
        {
            // ViewBag.UserTypeID = new SelectList(DB.UserTypeTables.Where(ut => ut.UserTypeID > 1).ToList(), "UserTypeID", "UserType", registerationmv.UserTypeID);
            ViewBag.CityID = new SelectList(DB.CityTables.ToList(), "CityID", "City", registerationmv.CityID);
            ViewBag.BloodGroupID = new SelectList(DB.BloodGroupsTables.ToList(), "BloodGroupID", "BloodGroup", "0");
            ViewBag.GenderID = new SelectList(DB.GenderTables.ToList(), "GenderID", "Gender", "0");
            return View(registerationmv);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DonorUser(RegisterationMV registerationMV)
        {
            if (ModelState.IsValid)
            {
                var checktitle = DB.DonorTables.Where(h => h.FullName == registerationMV.Donor.FullName.Trim() && h.CNIC == registerationMV.Donor.CNIC).FirstOrDefault();
                if (checktitle == null)
                {
                    using (var transaction = DB.Database.BeginTransaction())
                    {
                        try
                        {
                            var user = new UserTable();
                            user.UserName = registerationMV.User.UserName;
                            user.Password = registerationMV.User.Password;
                            user.EmailAddress = registerationMV.User.EmailAddress;
                            user.AccountStatusID = 1;
                            user.UserTypeID = registerationMV.UserTypeID;
                            user.Description = registerationMV.User.Description;

                            DB.UserTables.Add(user);
                            DB.SaveChanges();

                            var donor = new DonorTable();

                            donor.FullName = registerationMV.Donor.FullName;
                            donor.BloodGroupID = registerationMV.BloodGroupID;
                            donor.Location = registerationMV.Donor.Location;
                            donor.ContactNo = registerationMV.Donor.ContactNo;
                            donor.LastDonationID = registerationMV.Donor.LastDonationID;    
                            donor.CNIC = registerationMV.Donor.CNIC;
                            donor.GenderID = registerationMV.GenderID;
                            donor.CityID = registerationMV.CityID;
                            donor.UserID = user.UserID;

                            DB.DonorTables.Add(donor);
                            DB.SaveChanges();
                            transaction.Commit();
                            ViewData["Message"] = "Thanks for Registration, Your Query will be reviewed shortly!";
                            return RedirectToAction("MainHome", "Home");
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
                    ModelState.AddModelError(string.Empty, "Donor Already Existed.");
                }

            }
            ViewBag.BloodGroupID = new SelectList(DB.BloodGroupsTables.ToList(), "BloodGroupID", "BloodGroup", registerationMV.BloodGroupID);

            ViewBag.CityID = new SelectList(DB.CityTables.ToList(), "CityID", "City", registerationMV.CityID);
            return View(registerationMV);
        }

        public ActionResult BloodBankUser()
        {
            ViewBag.CityID = new SelectList(DB.CityTables.ToList(), "CityID", "City", registerationmv.CityID);
            return View(registerationmv);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BloodBankUser(RegisterationMV registerationMV)
        {
            ViewBag.CityID = new SelectList(DB.CityTables.ToList(), "CityID", "City", registerationmv.CityID);
            return View();
        }

        public ActionResult SeekerUser()
        {
            ViewBag.CityID = new SelectList(DB.CityTables.ToList(), "CityID", "City", registerationmv.CityID);
            return View(registerationmv);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SeekerUser(RegisterationMV registerationMV)
        {
            ViewBag.CityID = new SelectList(DB.CityTables.ToList(), "CityID", "City", registerationmv.CityID);
            return View();
        }
    }
}