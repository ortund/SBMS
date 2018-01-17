using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SBMSAdmin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SBMSAdmin.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            // Determine if the user has access to view this content.
            // If yes, continue, else redirect to login.
            var usermanager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            var isGlobal = usermanager.IsInRole(HttpContext.User.Identity.GetUserId(), "global");

            if (isGlobal)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
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