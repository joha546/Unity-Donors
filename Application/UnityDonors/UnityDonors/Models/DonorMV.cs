﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UnityDonors.Models
{
    public class DonorMV
    {
        public int DonorID { get; set; }
        public string FullName { get; set; }
        public int BloodGroupID { get; set; }
        public string BloodGroup {  get; set; }
        public System.DateTime LastDonationID { get; set; }
        public string ContactNo { get; set; }
        public string CNIC { get; set; }
        public string Location { get; set; }
        public int CityID { get; set; }
        public string City { get; set; }
        public int UserID { get; set; }
        public string UserName { get; set; }
    }
}