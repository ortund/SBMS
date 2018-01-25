using SBMSAdmin.Models;
using SBMSData.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using SBMSData;

namespace SBMSAdmin.Controllers
{
    public class InvoiceController : Controller
    {
        // GET: Invoice
        public ActionResult Index(int page = 1)
        {
            var model = new InvoicesViewModel();
            model.Invoices = new List<SBMSData.ViewModels.InvoiceViewModel>();

            using (var db = new ApplicationDbContext())
            {
                var invoices = (page == 1) ? db.Invoices.Include(i => i.Store).Where(x => !x.Deleted).Take(20).ToList() : db.Invoices.Include(i => i.Store).Where(x => !x.Deleted).Take(20).Skip(20 * page).ToList();

                foreach (var invoice in invoices)
                {
                    model.Invoices.Add(new SBMSData.ViewModels.InvoiceViewModel
                    {
                        Amount       = invoice.Amount,
                        Date         = invoice.Date,
                        Number       = invoice.Number,
                        Store        = invoice.Store
                    });
                }
                model.Pages       = invoices.Count / 20;
                model.CurrentPage = page;
            }
            return View(model);
        }

        public IEnumerable<SelectListItem> GetStores()
        {
            using (var db = new ApplicationDbContext())
            {
                var stores = db.Stores.Where(x => !x.Deleted).ToList().Select(x =>
                new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Name
                });

                return new SelectList(stores, "Value", "Text");
            }
        }

        public ActionResult Create()
        {
            return View(new Models.InvoiceViewModel { Stores = GetStores() });
        }

        [HttpPost]
        public JsonResult Create(Models.InvoiceViewModel model)
        {
            try
            {
                using (var db = new ApplicationDbContext())
                {
                    var invoice = new Invoice
                    {
                        Amount = model.Amount,
                        Date = model.Date,
                        Deleted = false,
                        Store = db.Stores.Single(x => x.Id == model.StoreId),
                        Paid = false
                    };

                    db.Invoices.Add(invoice);
                    db.SaveChanges();

                    // Generate an invoice number.
                    var sname = model.Store.Name.Substring(0, 3);
                    var invnum = string.Format("{0:D8}", invoice.Id);

                    var invoiceNumber = $"{sname}{invnum}";

                    invoice.Number = invoiceNumber;
                }
                return Json(new { IsOkay = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { IsOkay = false, Error = ex.Message, Stack = ex.StackTrace }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult GetStorePackage(int storeId)
        {
            using (var db = new ApplicationDbContext())
            {
                var store = db.Stores.Include(i => i.Package).Single(x => x.Id == storeId);
                return Json(new { Package = $"Service Package: {store.Package.Name}", Amount = store.Package.Price });
            }
        }

        // Details opens a view which doesn't have a layout.
        // The view will essentially be a printout of an invoice that can be printed
        // directly from the browser.
        // This action will open in a new tab.
        public ActionResult Details(int id)
        {
            using (var db = new ApplicationDbContext())
            {                
                var invoice = db.Invoices.Include(i => i.Store).Single(x => x.Id == id);
                var model = new Models.InvoiceViewModel
                {
                    Id = invoice.Id,
                    Amount = invoice.Amount,
                    Date = invoice.Date,
                    Number = invoice.Number,
                    Store = invoice.Store,
                    DatePaid = invoice.DatePaid,
                    Paid = invoice.Paid,
                };

                return View(model);
            }
        }

        public ActionResult Edit(int id)
        {
            using (var db = new ApplicationDbContext())
            {
                var invoice = db.Invoices.Single(x => x.Id == id);
                var model = new Models.InvoiceViewModel
                {
                    Amount = invoice.Amount,
                    Date = invoice.Date,
                    Number = invoice.Number,
                    Store = invoice.Store,
                    Stores = GetStores(),
                    StoreId = invoice.Store.Id,
                    DatePaid = invoice.DatePaid,
                    Paid = invoice.Paid
                };

                return View(model);
            }
        }

        [HttpPost]
        public JsonResult Edit(Models.InvoiceViewModel model)
        {
            try
            {
                using (var db = new ApplicationDbContext())
                {
                    var invoice = db.Invoices.Single(x => x.Id == model.Id);
                    invoice.Amount = model.Amount;
                    invoice.Date = model.Date;
                    invoice.Number = invoice.Number;
                    invoice.Store = db.Stores.Single(x => x.Id == model.StoreId);
                    invoice.Paid = model.Paid;
                    invoice.DatePaid = model.DatePaid;

                    db.SaveChanges();
                }

                return Json(new { IsOkay = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { IsOkay = true, Error = ex.Message, Stack = ex.StackTrace }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}