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
    public class PackageController : Controller
    {
        // GET: Package
        public ActionResult Index(int page = 1)
        {
            var model = new PackagesViewModel();
            model.Packages = new List<SBMSData.ViewModels.PackageViewModel>();

            using (var db = new ApplicationDbContext())
            {
                var packages = (page == 1) ? db.Packages.Where(x => !x.Deleted).Take(20).ToList()
                    : db.Packages.Where(x => !x.Deleted).Take(20).Skip(20 * page).ToList();

                foreach(var package in packages)
                {
                    model.Packages.Add(new SBMSData.ViewModels.PackageViewModel
                    {
                        Id    = package.Id,
                        Name  = package.Name,
                        Price = package.Price
                    });
                }
                model.Pages       = packages.Count / 20;
                model.CurrentPage = page;
            }

            return View(model);
        }

        private IEnumerable<SelectListItem> GetFeatures()
        {
            using (var db = new ApplicationDbContext())
            {
                var features = db.Features.Where(x => !x.Deleted).ToList().Select(x =>
                new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Description
                });

                return new SelectList(features, "Value", "Text");
            }
        }

        // GET: Package/Create
        public ActionResult Create()
        {
            var model = new Models.PackageViewModel
            {
                Features = GetFeatures()
            };

            return View(model);
        }

        [HttpPost]
        public JsonResult Create(Models.PackageViewModel model)
        {
            try
            {
                using (var db = new ApplicationDbContext())
                {
                    // Create the new Package.
                    var package = new Package
                    {
                        Name = model.Name,
                        Price = model.Price
                    };

                    db.Packages.Add(package);
                    db.SaveChanges();

                    // Add features that were selected to the package.
                    foreach (var feature in model.Features)
                    {
                        if (feature.Selected)
                        {
                            int selectedFeatureId = int.TryParse(feature.Value, out selectedFeatureId) ? selectedFeatureId : 0;

                            db.PackageFeatures.Add(new PackageFeatures
                            {
                                Feature = db.Features.Single(x => x.Id == selectedFeatureId),
                                Package = package,
                                Deleted = false
                            });
                        }
                    }

                    db.SaveChanges();

                    return Json(new { Id = package.Id, IsOkay = true }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { IsOkay = false, Error = ex.Message, Stack = ex.StackTrace }, JsonRequestBehavior.AllowGet);
            }
        }

        // GET: Package/Edit
        public ActionResult Edit(int id)
        {
            var model = new Models.PackageViewModel()
            {
                Features = GetFeatures()
            };
            using (var db = new ApplicationDbContext())
            {
                var package         = db.Packages.Single(x => x.Id == id);
                var packageFeatures = db.PackageFeatures.Where(x => x.Package == package && !x.Deleted);
                foreach (var packageFeature in packageFeatures)
                {
                    model.SelectedFeatures.Add(packageFeature.Id);
                }

                model.Id    = package.Id;
                model.Name  = package.Name;
                model.Price = package.Price;
            }

            return View(model);
        }

        // POST: Package/Edit/{id}
        [HttpPost]
        public JsonResult Edit(Models.PackageViewModel model)
        {
            try
            {
                using (var db = new ApplicationDbContext())
                {
                    var package = db.Packages.Single(x => x.Id == model.Id);

                    package.Name = model.Name;
                    package.Price = model.Price;

                    // Remove all package feature links (resetting package features).
                    // These records are physically deleted from the database to avoid bloat
                    // in the database that would occur if they were kept.
                    db.PackageFeatures.RemoveRange(db.PackageFeatures.Where(x => x.Package == package));

                    // Add newly selected package feature links.
                    foreach (var feature in model.Features)
                    {
                        if (feature.Selected)
                        {
                            int selectedFeatureId = int.TryParse(feature.Value, out selectedFeatureId) ? selectedFeatureId : 0;

                            db.PackageFeatures.Add(new PackageFeatures
                            {
                                Feature = db.Features.Single(x => x.Id == selectedFeatureId),
                                Package = package,
                                Deleted = false
                            });
                        }
                    }

                    db.SaveChanges();
                }

                return Json(new { IsOkay = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { IsOkay = false, Error = ex.Message, Stack = ex.StackTrace }, JsonRequestBehavior.AllowGet);
            }
        }
        
        public JsonResult Delete(int id)
        {
            try
            {
                using (var db = new ApplicationDbContext())
                {
                    db.Packages.Single(x => x.Id == id).Deleted = true;
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