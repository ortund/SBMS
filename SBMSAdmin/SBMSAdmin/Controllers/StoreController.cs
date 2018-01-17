using SBMSAdmin.Models;
using SBMSData;
using SBMSData.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace SBMSAdmin.Controllers
{
    [Authorize]
    public class StoreController : Controller
    {
        // GET: Store
        public ActionResult Index(int page = 1)
        {
            var model = new StoresViewModel();
            model.Stores = new List<SBMSData.ViewModels.StoreViewModel>();

            using (var db = new ApplicationDbContext())
            {
                var stores = (page == 1) ? db.Stores.Include(i => i.Package).Where(x => !x.Deleted).Take(20).ToList() : db.Stores.Include(i => i.Package).Where(x => !x.Deleted).Take(20).Skip(20 * page).ToList();

                foreach (var store in stores)
                {
                    model.Stores.Add(new Models.StoreViewModel
                    {
                        // It isn't necessary to get the lsit of employees here.
                        Address       = store.Address,
                        Commission    = store.Commission,
                        ContactNumber = store.ContactNumber,
                        ContactPerson = store.ContactPerson,
                        EmailAddress  = store.EmailAddress,
                        Id            = store.Id,
                        Name          = store.Name,
                        Package       = new PackageViewModel
                        {
                            Deleted = store.Package.Deleted,
                            Id      = store.Package.Id,
                            Name    = store.Package.Name,
                            Price   = store.Package.Price
                        },
                        Telephone = store.Telephone,
                        Terminals = store.Terminals
                    });
                }
                model.Pages = stores.Count / 20;
                model.CurrentPage = page;
            }
            return View(model);
        }

        private IEnumerable<SelectListItem> GetPackages()
        {
            using (var db = new ApplicationDbContext())
            {
                var packages = db.Packages.Where(x => !x.Deleted).ToList().Select(x =>
                new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Name
                });

                return new SelectList(packages, "Value", "Text");
            }
        }

        //
        // GET: Store/Create
        public ActionResult Create()
        {
            var model = new Models.StoreViewModel
            {
                Packages = GetPackages()
            };

            return View(model);
        }

        //
        //POST: Store/Create
        [HttpPost]
        public JsonResult Create(Models.StoreViewModel model)
        {
            try
            {
                using (var db = new ApplicationDbContext())
                {
                    var store = new Store
                    {
                        // Create a new store without any employees.
                        Address       = model.Address,
                        Commission    = model.Commission,
                        ContactNumber = model.ContactNumber,
                        ContactPerson = model.ContactPerson,
                        Deleted       = false,
                        EmailAddress  = model.EmailAddress,
                        IsActive      = true,
                        Name          = model.Name,
                        Package       = db.Packages.Single(x => x.Id == model.PackageId),
                        Telephone     = model.Telephone,
                        Terminals     = model.Terminals
                    };

                    db.Stores.Add(store);
                    db.SaveChanges();

                    return Json(new { Id = store.Id, IsOkay = true }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { IsOkay = false, Error = ex.Message, Stack = ex.StackTrace }, JsonRequestBehavior.AllowGet);
            }
        }

        //
        // GET: Store/Edit
        public ActionResult Edit(int id)
        {
            var model = new Models.StoreViewModel();
            using (var db = new ApplicationDbContext())
            {
                var store           = db.Stores.Include(i => i.Package).Single(x => x.Id == id && !x.Deleted);
                model.Address       = store.Address;
                model.Commission    = store.Commission;
                model.ContactNumber = store.ContactNumber;
                model.ContactPerson = store.ContactPerson;
                model.EmailAddress  = store.EmailAddress;
                model.Id            = store.Id;
                model.Name          = store.Name;
                model.Packages      = GetPackages();
                model.PackageId     = (store.Package != null) ? store.Package.Id : 0;
                model.Telephone     = store.Telephone;
                model.Terminals     = store.Terminals;
            }

            return View(model);
        }

        //
        // POST: /Store/Edit/{id}
        [HttpPost]
        public JsonResult Edit(Models.StoreViewModel model)
        {
            try
            {
                using (var db = new ApplicationDbContext())
                {
                    var store = db.Stores.Single(x => x.Id == model.Id);

                    store.Address       = model.Address;
                    store.Commission    = model.Commission;
                    store.ContactNumber = model.ContactNumber;
                    store.ContactPerson = model.ContactPerson;
                    store.EmailAddress  = model.EmailAddress;
                    store.Package       = db.Packages.Single(x => x.Id == model.PackageId);
                    store.Telephone     = model.Telephone;
                    store.Terminals     = model.Terminals;

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
                    var store = db.Stores.Single(x => x.Id == id);
                    store.Deleted = true;

                    // Cascade delete so that employee records are also deleted.
                    foreach (var employee in db.Employees.Where(x => x.Store == store))
                    {
                        employee.Deleted = true;
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

        //
        // GET: /Store/Register
        // Basically a copy of Create()
        public ActionResult Register()
        {
            var model = new Models.StoreViewModel
            {
                Packages = GetPackages()
            };

            return View(model);
        }

        // This logic is identical to Create(Models.StoreViewModel) and will remain so until
        // it can be determined how to read the output of Create(Models.StoreViewModel) here
        // in a call to that method.
        [HttpPost]
        public JsonResult Register(Models.StoreViewModel model)
        {
            try
            {
                using (var db = new ApplicationDbContext())
                {
                    var store = new Store
                    {
                        // Create a new store without any employees.
                        Address       = model.Address,
                        Commission    = model.Commission,
                        ContactNumber = model.ContactNumber,
                        ContactPerson = model.ContactPerson,
                        Deleted       = false,
                        EmailAddress  = model.EmailAddress,
                        IsActive      = true,
                        Name          = model.Name,
                        Package       = db.Packages.Single(x => x.Id == model.PackageId),
                        Telephone     = model.Telephone,
                        Terminals     = model.Terminals
                    };

                    db.Stores.Add(store);
                    db.SaveChanges();

                    Utility.Emails.Instance.BuildAndSendStoreRegistrationEmail(store);

                    return Json(new { Id = store.Id, IsOkay = true }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { IsOkay = false, Error = ex.Message, Stack = ex.StackTrace }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}