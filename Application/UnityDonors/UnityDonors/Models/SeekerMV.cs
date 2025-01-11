using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UnityDonors.Models
{
    public class SeekerMV
    {
        public int SeekerID { get; set; }
        public string FullName { get; set; }
        public int Age { get; set; }
        public int CityID { get; set; }
        public string CityName { get; set; }
        public int BloodGroupID { get; set; }
        public string Blood { get; set; }
        public string Contact { get; set; }
        public string CNIC { get; set; }
        public int GenderID { get; set; }
        public string Gender { get; set; }
        public System.DateTime RegistrationDate { get; set; }
        public string Address { get; set; }
        public int UserID { get; set; }
        public string UserName { get; set; }
    }
}