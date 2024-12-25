﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UnityDonors.Models
{
    public class RequestTypeMV
    {
        public int RequestTypeID { get; set; }
        [Required(ErrorMessage = "Rrequired")]
        [Display(Name = "Request Type")]
        public string RequestType { get; set; }
    }
}