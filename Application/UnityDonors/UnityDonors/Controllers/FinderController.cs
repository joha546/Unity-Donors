﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UnityDonors.Controllers
{
    public class FinderController : Controller
    {
        // GET: Finder
        public ActionResult FinderDonors()
        {
            return View();
        }
    }
}