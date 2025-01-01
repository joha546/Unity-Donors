﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UnityDonors.Models
{
    public class RegisterationMV
    {
        // Declaring as property.
        public SeekerMV Seeker { get; set; }
        public HospitalMV Hospital { get; set; }

        public BloodBankMV BloodBank { get; set; }
        public DonorMV Donor { get; set; }
        public UserMV User { get; set; }

    }
}