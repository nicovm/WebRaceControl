﻿using RaceControl.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RaceControl.Controllers
{
    public class HomeController : Controller
    {
        private EntidadesRaceControl db = new EntidadesRaceControl();

        public ActionResult Index()
        {
            ViewBag.torneos = db.Torneo.ToList();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}