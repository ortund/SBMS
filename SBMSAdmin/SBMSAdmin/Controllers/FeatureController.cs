using SBMSAdmin.Models;
using SBMSData;
using SBMSData.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SBMSAdmin.Controllers
{
    [Authorize]
    public class FeatureController : Controller
    {
        // GET: /Feature
        public ActionResult Index(int page = 1)
        {
            var model = new FeaturesViewModel();
            model.Features = new List<FeatureViewModel>();

            using (var db = new ApplicationDbContext())
            {
                var features = (page == 1) ? db.Features.Where(x => !x.Deleted).Take(20).ToList() : db.Features.Where(x => !x.Deleted).Take(20).Skip(20 * page).ToList();

                foreach (var feature in features)
                {
                    model.Features.Add(new FeatureViewModel
                    {
                        Description = feature.Description,
                        Id          = feature.Id
                    });
                }
                model.Pages       = features.Count / 20;
                model.CurrentPage = page;
            }
            return View(model);
        }

        // GET: /Feature/Create
        public ActionResult Create()
        {
            return View(new FeatureViewModel());
        }

        // POST: /Feature/Create
        [HttpPost]
        public JsonResult Create(FeatureViewModel model)
        {
            try
            {
                using (var db = new ApplicationDbContext())
                {
                    var feature = new Feature
                    {
                        Description = model.Description,
                        Deleted = false
                    };

                    db.Features.Add(feature);
                    db.SaveChanges();

                    return Json(new { IsOkay = true }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { IsOkay = false, Error = ex.Message, Stack = ex.StackTrace }, JsonRequestBehavior.AllowGet);
            }
        }

        // GET: /Feature/Edit
        public ActionResult Edit(int id)
        {
            var model = new FeatureViewModel();
            using (var db = new ApplicationDbContext())
            {
                var feature = db.Features.Single(x => x.Id == id);
                model.Description = feature.Description;
                model.Id = feature.Id;
            }

            return View(model);
        }

        // POST: /Feature/Edit
        [HttpPost]
        public JsonResult Edit(FeatureViewModel model)
        {
            try
            {
                using (var db = new ApplicationDbContext())
                {
                    var feature = db.Features.Single(x => x.Id == model.Id);
                    feature.Description = model.Description;

                    db.SaveChanges();
                }

                return Json(new { IsOkay = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { IsOkay = false, Error = ex.Message, Stack = ex.StackTrace }, JsonRequestBehavior.AllowGet);
            }
        }
        
        [HttpPost]
        public JsonResult Delete(int id)
        {
            try
            {
                using (var db = new ApplicationDbContext())
                {
                    db.Features.Single(x => x.Id == id).Deleted = true;
                    db.SaveChanges();
                }

                return Json(new { IsOkay = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { IsOkay = false, Error = ex.Message, Stack = ex.StackTrace }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}