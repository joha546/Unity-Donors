using DatabaseLayer;
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
            ViewBag.GenderID = new SelectList(DB.GenderTables.ToList(), "GenderID", "Gender", registerationMV.GenderID);
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
            if (ModelState.IsValid)
            {
                var checktitle = DB.BloodBankTables.Where(h => h.BloodBankName == registerationMV.BloodBank.BloodBankName.Trim() && h.PhoneNo == registerationMV.BloodBank.PhoneNo).FirstOrDefault();
                if (checktitle == null)
                {
                    using (var transaction = DB.Database.BeginTransaction())
                    {
                        try
                        {
                            var user = new UserTable
                            {
                                UserName = registerationMV.User.UserName,
                                Password = registerationMV.User.Password,
                                EmailAddress = registerationMV.User.EmailAddress,
                                AccountStatusID = 1,
                                UserTypeID = registerationMV.UserTypeID,
                                Description = registerationMV.User.Description
                            };

                            DB.UserTables.Add(user);
                            DB.SaveChanges();

                            var bloodBank = new BloodBankTable
                            {
                                BloodBankName = registerationMV.BloodBank.BloodBankName,
                                Address = registerationMV.BloodBank.Location,
                                Location = registerationMV.BloodBank.Location,
                                PhoneNo = registerationMV.BloodBank.PhoneNo,
                                Website = registerationMV.BloodBank.Website,
                                CityID = registerationMV.CityID,
                                UserID = user.UserID,
                                Email = registerationMV.BloodBank.Email
                            };

                            DB.BloodBankTables.Add(bloodBank);
                            DB.SaveChanges();
                            transaction.Commit();
                            ViewData["Message"] = "Thanks for Registration, Your Query will be reviewed shortly!";
                            return RedirectToAction("MainHome", "Home");
                        }
                        catch (Exception ex)
                        {
                            ModelState.AddModelError(string.Empty, $"Error: {ex.Message}");
                            transaction.Rollback();
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Blood Bank Already Existed.");
                }
            }

            ViewBag.CityID = new SelectList(DB.CityTables.ToList(), "CityID", "City", registerationMV.CityID);
            return View(registerationMV);
        }


        public ActionResult SeekerUser()
        {
            ViewBag.CityID = new SelectList(DB.CityTables.ToList(), "CityID", "City", "0");
            ViewBag.BloodGroupID = new SelectList(DB.BloodGroupsTables.ToList(), "BloodGroupID", "BloodGroup", "0");
            ViewBag.GenderID = new SelectList(DB.GenderTables.ToList(), "GenderID", "Gender", "0");
            return View(registerationmv);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SeekerUser(RegisterationMV registerationMV)
        {
            if (ModelState.IsValid)
            {
                var checktitle = DB.SeekerTables
                    .Where(h => h.FullName == registerationMV.Seeker.FullName.Trim() && h.CNIC == registerationMV.Seeker.CNIC)
                    .FirstOrDefault();
                if (checktitle == null)
                {
                    using (var transaction = DB.Database.BeginTransaction())
                    {
                        try
                        {
                            var user = new UserTable
                            {
                                UserName = registerationMV.User.UserName,
                                Password = registerationMV.User.Password,
                                EmailAddress = registerationMV.User.EmailAddress,
                                AccountStatusID = 1,
                                UserTypeID = registerationMV.UserTypeID,
                                Description = registerationMV.User.Description
                            };

                            DB.UserTables.Add(user);
                            DB.SaveChanges();

                            var seeker = new SeekerTable
                            {
                                FullName = registerationMV.Seeker.FullName,
                                Age = registerationMV.Seeker.Age,
                                BloodGroupID = registerationMV.BloodGroupID,
                                Address = registerationMV.Seeker.Address,
                                Contact = registerationMV.Seeker.Contact,
                                RegistrationDate = DateTime.Now,
                                CNIC = registerationMV.Seeker.CNIC,
                                GenderID = registerationMV.GenderID,
                                CityID = registerationMV.CityID,
                                UserID = user.UserID
                            };

                            DB.SeekerTables.Add(seeker);
                            DB.SaveChanges();

                            transaction.Commit();
                            ViewData["Message"] = "Thanks for Registration, Your Query will be reviewed shortly!";
                            return RedirectToAction("MainHome", "Home");
                        }
                        catch (Exception ex)
                        {
                            ModelState.AddModelError(string.Empty, $"Error: {ex.Message}");
                            Console.WriteLine($"Exception: {ex.Message}"); // Log for debugging
                            transaction.Rollback();
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Seeker Already Existed.");
                }
            }
            else
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                foreach (var error in errors)
                {
                    Console.WriteLine($"ModelState Error: {error.ErrorMessage}"); // Log validation errors
                }
            }

            ViewBag.BloodGroupID = new SelectList(DB.BloodGroupsTables.ToList(), "BloodGroupID", "BloodGroup", registerationMV.BloodGroupID);
            ViewBag.GenderID = new SelectList(DB.GenderTables.ToList(), "GenderID", "Gender", registerationMV.GenderID);
            ViewBag.CityID = new SelectList(DB.CityTables.ToList(), "CityID", "City", registerationMV.CityID);
            return View(registerationMV);

        }
    }
}