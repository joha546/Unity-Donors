﻿using DatabaseLayer;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UnityDonors.Models;

namespace UnityDonors.Controllers
{
    public class UserController : Controller
    {
        Unity_DonorEntities DB = new Unity_DonorEntities();
        // GET: User
        public ActionResult UserProfile(int? id)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            var user = DB.UserTables.Find(id);
            return View(user);
        }

        public ActionResult EditUserProfile(int? id)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }

            var userprofile = new RegisterationMV();
            var user = DB.UserTables.Find(id);

            userprofile.UserTypeID = user.UserTypeID;

            // for gener users.
            userprofile.User.UserID = user.UserID;
            userprofile.User.UserName = user.UserName;
            userprofile.User.EmailAddress = user.EmailAddress;
            userprofile.User.AccountStatusID = user.AccountStatusID;
            userprofile.User.UserTypeID = user.UserTypeID;
            userprofile.User.Description = user.Description;

            if (user.SeekerTables.Count > 0) 
            {
                var seeker = user.SeekerTables.FirstOrDefault();

                userprofile.Seeker.SeekerID = seeker.SeekerID;
                userprofile.Seeker.FullName = seeker.FullName;
                userprofile.Seeker.Age = seeker.Age;
                userprofile.Seeker.CityID = seeker.CityID;
                userprofile.Seeker.BloodGroupID = seeker.BloodGroupID;
                userprofile.Seeker.Contact = seeker.Contact;
                userprofile.Seeker.CNIC = seeker.CNIC;
                userprofile.Seeker.GenderID = seeker.GenderID;
                userprofile.Seeker.RegistrationDate = seeker.RegistrationDate;
                userprofile.Seeker.Address = seeker.Address;
                userprofile.Seeker.UserID = seeker.UserID;

                userprofile.ContactNo = seeker.Contact;
                userprofile.CityID = seeker.CityID;
                userprofile.BloodGroupID = seeker.BloodGroupID;
                userprofile.GenderID = seeker.GenderID;


            }

            else if (user.HospitalTables.Count > 0)
            {
                var hospital = user.HospitalTables.FirstOrDefault();

                userprofile.Hospital.HospitalID = hospital.HospitalID;
                userprofile.Hospital.FullName = hospital.FullName;
                userprofile.Hospital.Address = hospital.Address;
                userprofile.Hospital.PhoneNo = hospital.PhoneNo;
                userprofile.Hospital.Website = hospital.Website;
                userprofile.Hospital.Email = hospital.Email;
                userprofile.Hospital.Location = hospital.Location;
                userprofile.Hospital.CityID = hospital.CityID;
                userprofile.Hospital.UserID = hospital.UserID;

                userprofile.ContactNo = hospital.PhoneNo;
                userprofile.CityID = hospital.CityID;
            }

            else if (user.BloodBankTables.Count > 0)
            {
                var bloodbank = user.BloodBankTables.FirstOrDefault();

                userprofile.BloodBank.BloodBankID = bloodbank.BloodBankID;
                userprofile.BloodBank.BloodBankName = bloodbank.BloodBankName;
                userprofile.BloodBank.Address = bloodbank.Address;
                userprofile.BloodBank.PhoneNo = bloodbank.PhoneNo;
                userprofile.BloodBank.Location = bloodbank.Location;
                userprofile.BloodBank.Website = bloodbank.Website;
                userprofile.BloodBank.Email = bloodbank.Email;
                userprofile.BloodBank.CityID = bloodbank.CityID;

                userprofile.ContactNo = bloodbank.PhoneNo;
                userprofile.CityID = bloodbank.CityID;
            }

            else if (user.DonorTables.Count > 0)
            {
                var donor = user.DonorTables.FirstOrDefault();

                userprofile.Donor.DonorID = donor.DonorID;
                userprofile.Donor.FullName = donor.FullName;
                userprofile.Donor.GenderID = donor.GenderID;
                userprofile.Donor.BloodGroupID = donor.BloodGroupID;
                userprofile.Donor.LastDonationID = donor.LastDonationID;
                userprofile.Donor.ContactNo = donor.ContactNo;
                userprofile.Donor.CNIC = donor.CNIC;
                userprofile.Donor.Location = donor.Location;
                userprofile.Donor.CityID = donor.CityID;
                userprofile.Donor.UserID = donor.UserID;

                userprofile.ContactNo = donor.ContactNo;
                userprofile.CityID = donor.CityID;
                userprofile.BloodGroupID = donor.BloodGroupID;
                userprofile.GenderID = donor.GenderID;
            }
            ViewBag.CityID = new SelectList(DB.CityTables.ToList(), "CityID", "City", userprofile.CityID);
            ViewBag.BloodGroupID = new SelectList(DB.BloodGroupsTables.ToList(), "BloodGroupID", "BloodGroup",userprofile.BloodGroupID );
            ViewBag.GenderID = new SelectList(DB.GenderTables.ToList(), "GenderID", "Gender", userprofile.GenderID);
            return View(userprofile);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditUserProfile(RegisterationMV userprofile)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }

            // updation.
            if (ModelState.IsValid)
            {
                var checkuseremail = DB.UserTables.Where(u => u.EmailAddress == userprofile.User.EmailAddress && u.UserID != userprofile.User.UserID).FirstOrDefault();

                if(checkuseremail == null)
                {
                    try
                    {
                        var user = DB.UserTables.Find(userprofile.User.UserID);

                        user.EmailAddress = userprofile.User.EmailAddress;
                        DB.Entry(user).State = System.Data.Entity.EntityState.Modified;
                        DB.SaveChanges();

                        if (userprofile.Donor.DonorID > 0)
                        {
                            var donor = DB.DonorTables.Find(userprofile.Donor.DonorID);

                            donor.FullName = userprofile.Donor.FullName;
                            donor.BloodGroupID = userprofile.BloodGroupID;
                            donor.GenderID = userprofile.GenderID;
                            donor.ContactNo = userprofile.Donor.ContactNo;
                            donor.CNIC = userprofile.Donor.CNIC;
                            donor.CityID = userprofile.CityID;
                            donor.Location = userprofile.Donor.Location;

                            DB.Entry(donor).State = System.Data.Entity.EntityState.Modified;
                            DB.SaveChanges();
                        }
                        else if (userprofile.Seeker.SeekerID > 0)
                        {
                            var seeker = DB.SeekerTables.Find(userprofile.Seeker.SeekerID);

                            seeker.FullName = userprofile.Seeker.FullName;
                            seeker.BloodGroupID = userprofile.BloodGroupID;
                            seeker.GenderID = userprofile.GenderID;
                            seeker.Age = userprofile.Seeker.Age;
                            seeker.Contact = userprofile.Seeker.Contact;
                            seeker.CNIC = userprofile.Seeker.CNIC;
                            seeker.CityID = userprofile.Seeker.CityID;
                            seeker.Address = userprofile.Seeker.Address;

                            DB.Entry(seeker).State = System.Data.Entity.EntityState.Modified;
                            DB.SaveChanges();
                        }
                        else if (userprofile.BloodBank.BloodBankID > 0)
                        {
                            var bloodbank = DB.BloodBankTables.Find(userprofile.BloodBank.BloodBankID);

                            bloodbank.BloodBankName = userprofile.BloodBank.BloodBankName;
                            bloodbank.PhoneNo = userprofile.BloodBank.PhoneNo;
                            bloodbank.Email = userprofile.BloodBank.Email;
                            bloodbank.Website = userprofile.BloodBank.Website;
                            bloodbank.CityID = userprofile.CityID;
                            bloodbank.Address = userprofile.BloodBank.Address;

                            DB.Entry(bloodbank).State = System.Data.Entity.EntityState.Modified;
                            DB.SaveChanges();
                        }
                        else if (userprofile.Hospital.HospitalID > 0)
                        {
                            var hospital = DB.HospitalTables.Find(userprofile.Hospital.HospitalID);

                            hospital.FullName = userprofile.Hospital.FullName;
                            hospital.PhoneNo = userprofile.Hospital.PhoneNo;
                            hospital.Email = userprofile.Hospital.Email;
                            hospital.Website = userprofile.Hospital.Website;
                            hospital.CityID = userprofile.CityID;
                            hospital.Address = userprofile.Hospital.Address;

                            DB.Entry(hospital).State = System.Data.Entity.EntityState.Modified;
                            DB.SaveChanges();
                        }

                        return RedirectToAction("UserProfile", "User", new {id = userprofile.User.UserID});
                    }
                    catch
                    {
                        ModelState.AddModelError(string.Empty, "Some Data is Incorrect! Please Provide Correct Detail.");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "User Email is Already Exists..");
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Some Data is Incorrect! Please Provide Correct Detail.");
            }


            // var user = DB.UserTables.Find(id);
            ViewBag.CityID = new SelectList(DB.CityTables.ToList(), "CityID", "City", userprofile.CityID);
            ViewBag.BloodGroupID = new SelectList(DB.BloodGroupsTables.ToList(), "BloodGroupID", "BloodGroup", userprofile.BloodGroupID);
            ViewBag.GenderID = new SelectList(DB.GenderTables.ToList(), "GenderID", "Gender", userprofile.GenderID);
            return View(userprofile);
        }
    }
}