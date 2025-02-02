using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UnityDonors.Models;
using DatabaseLayer;
using System.ComponentModel.DataAnnotations;

namespace UnityDonors.Controllers
{
    public class HomeController : Controller
    {
        Unity_DonorEntities DB = new Unity_DonorEntities();
        // GET: Home
        public ActionResult AllCampaigns()
        {
            //if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            //{
            //    return RedirectToAction("Login", "Home");
            //}
            var date = DateTime.Now.Date;
            var allcampaigns = DB.CampaignTables.Where(c => c.CampaignDate >= date).ToList();
            return View(allcampaigns);
        }


        public ActionResult MainHome()
        {
            var message = ViewData["Message"] == null? "Welcome to Unity Donors." : ViewData["Message"];
            ViewData["Message"] = message;

            var date = DateTime.Now.Date;
            var allcampaigns = DB.CampaignTables.Where(c => c.CampaignDate >= date).ToList();
            ViewBag.AllCampaigns = allcampaigns;

            var registeration = new RegisterationMV();
            ViewBag.UserTypeID = new SelectList(DB.UserTypeTables.Where(ut => ut.UserTypeID > 1).ToList(), "UserTypeID", "UserType", "0");
            ViewBag.CityID = new SelectList(DB.CityTables.ToList(), "CityID", "City", "0");
            return View(registeration);
        }

        public ActionResult Login()
        {
            var usermv = new UserMV();
            return View(usermv);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserMV userMV)
        {
            if (ModelState.IsValid)
            {
                var user = DB.UserTables.Where(u => u.Password == userMV.Password && u.UserName == userMV.UserName).FirstOrDefault();
                if (user != null)
                {
                    if (user.AccountStatusID == 1)
                    {
                        ModelState.AddModelError(string.Empty, "Please Wait, Your account is under review..");
                    }
                    else if (user.AccountStatusID == 3)
                    {
                        ModelState.AddModelError(string.Empty, "Your Account is Rejected. For more details, contact us.");
                    }
                    else if (user.AccountStatusID == 5)
                    {
                        ModelState.AddModelError(string.Empty, "Your Account is Suspended. For more details, contact us.");
                    }
                    else if (user.AccountStatusID == 2) // Approved
                    {
                        Session["UserID"] = user.UserID;
                        Session["UserName"] = user.UserName;
                        Session["Password"] = user.Password;
                        Session["EmailAddress"] = user.EmailAddress;
                        Session["AccountStatusID"] = user.AccountStatusID;
                        Session["AccountStatus"] = user.AccountStatusTable.AccountStatus;
                        Session["UserTypeID"] = user.UserTypeID;
                        Session["UserType"] = user.UserTypeTable.UserType;
                        Session["Description"] = user.Description;

                        if(user.UserTypeID == 1)  // Admin
                        {
                            return RedirectToAction("AllNewUserRequests", "Accounts");
                        }

                        // Corrected UserTypeID checks and removed duplicates
                        else if (user.UserTypeID == 2) // Donor Sessions
                        {
                            var donor = DB.DonorTables.Where(u => u.UserID == user.UserID).FirstOrDefault();
                            if (donor != null)
                            {
                                Session["DonorID"] = donor.DonorID;
                                Session["FullName"] = donor.FullName;
                                Session["GenderID"] = donor.GenderID;
                                Session["BloodGroupID"] = donor.BloodGroupID;
                                Session["BloodGroup"] = donor.BloodGroupsTable.BloodGroup;
                                Session["LastDonationID"] = donor.LastDonationID;
                                Session["ContactNo"] = donor.ContactNo;
                                Session["CNIC"] = donor.CNIC;
                                Session["Location"] = donor.Location;
                                Session["CityID"] = donor.CityID;
                                Session["City"] = donor.CityTable.City;

                                return RedirectToAction("DonorRequests", "Finder");
                            }
                            else
                            {
                                ModelState.AddModelError(string.Empty, "Account Not Registered!!!!");
                            }
                        }
                        else if (user.UserTypeID == 4) // Seeker Sessions
                        {
                            var seeker = DB.SeekerTables.Where(u => u.UserID == user.UserID).FirstOrDefault();
                            if (seeker != null)
                            {
                                Session["SeekerID"] = seeker.SeekerID;
                                Session["FullName"] = seeker.FullName;
                                Session["Age"] = seeker.Age;
                                Session["CityID"] = seeker.CityID;
                                Session["CityName"] = seeker.CityTable.City;
                                Session["BloodGroupID"] = seeker.BloodGroupID;
                                Session["Blood"] = seeker.BloodGroupsTable.BloodGroup;
                                Session["Contact"] = seeker.Contact;
                                Session["CNIC"] = seeker.CNIC;
                                Session["GenderID"] = seeker.GenderID;
                                Session["Gender"] = seeker.GenderTable.Gender;
                                Session["RegistrationDate"] = seeker.RegistrationDate;
                                Session["Address"] = seeker.Address;

                                return RedirectToAction("ShowAllResults", "Finder");
                                
                            }
                            else
                            {
                                ModelState.AddModelError(string.Empty, "Account Not Registered!!!!");
                            }
                        }
                        else if (user.UserTypeID == 5) // Hospital Sessions
                        {
                            var hospital = DB.HospitalTables.Where(u => u.UserID == user.UserID).FirstOrDefault();
                            if (hospital != null)
                            {
                                Session["HospitalID"] = hospital.HospitalID;
                                Session["FullName"] = hospital.FullName;
                                Session["Address"] = hospital.Address;
                                Session["PhoneNo"] = hospital.PhoneNo;
                                Session["Website"] = hospital.Website;
                                Session["Email"] = hospital.Email;
                                Session["Location"] = hospital.Location;
                                Session["CityID"] = hospital.CityID;
                                Session["City"] = hospital.CityTable.City;

                                return RedirectToAction("ShowAllResults", "Finder");
                                
                            }
                            else
                            {
                                ModelState.AddModelError(string.Empty, "Account Not Registered!!!!");
                            }
                        }
                        else if (user.UserTypeID == 6) // BloodBank Sessions
                        {
                            var bloodbank = DB.BloodBankTables.Where(u => u.UserID == user.UserID).FirstOrDefault();
                            if (bloodbank == null)
                            {
                                ModelState.AddModelError(string.Empty, "Blood bank account not associated with this user.");
                                //Session["BloodBankID"] = bloodbank.BloodBankID;
                                //Session["BloodBankName"] = bloodbank.BloodBankName;
                                //Session["Address"] = bloodbank.Address;
                                //Session["PhoneNo"] = bloodbank.PhoneNo;
                                //Session["Website"] = bloodbank.Website;
                                //Session["Email"] = bloodbank.Email;
                                //Session["Location"] = bloodbank.Location;
                                //Session["CityID"] = bloodbank.CityID;
                                //Session["City"] = bloodbank.CityTable.City;

                                //return RedirectToAction("BloodBank", "Dashboard");
                            }
                            else
                            {
                                Session["BloodBankID"] = bloodbank.BloodBankID;
                                Session["BloodBankName"] = bloodbank.BloodBankName;
                                Session["Address"] = bloodbank.Address;
                                Session["PhoneNo"] = bloodbank.PhoneNo;
                                Session["Website"] = bloodbank.Website;
                                Session["Email"] = bloodbank.Email;
                                Session["Location"] = bloodbank.Location;
                                Session["CityID"] = bloodbank.CityID;
                                Session["City"] = bloodbank.CityTable.City;

                                
                                return RedirectToAction("BloodBankStock", "BloodBank");
                            }
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "Invalid User Type!!!!");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Account Not Registered!!!!");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Username or Password is incorrect");
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Please Provide the required information.");
            }
            ClearSession();
            return View(userMV);
             
        }

        private void ClearSession()
        {
            Session["UserID"] = string.Empty;
            Session["UserName"] = string.Empty;  // used to maintain the state of user data throughout the application
            Session["Password"] = string.Empty;
            Session["EmailAddress"] = string.Empty;
            Session["AccountStatusID"] = string.Empty;
            Session["AccountStatus"] = string.Empty;
            Session["UserTypeID"] = string.Empty;
            Session["UserType"] = string.Empty;
            Session["Description"] = string.Empty;
        }

        public ActionResult Logout()
        {
            ClearSession();

            return RedirectToAction("MainHome");
        }

        public ActionResult AboutUs()
        {
            return View();
        }
    }
}