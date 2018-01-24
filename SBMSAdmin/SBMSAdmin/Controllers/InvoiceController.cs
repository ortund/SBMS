using SBMSAdmin.Models;
using SBMSData.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace SBMSAdmin.Controllers
{
    public class InvoiceController : Controller
    {
        // GET: Invoice
        public ActionResult Index(int page = 1)
        {
            var model = new InvoicesViewModel();
            model.Invoices = new List<InvoiceViewModel>();

            using (var db = new ApplicationDbContext())
            {
                var invoices = (page == 1) ? db.Invoices.Include(i => i.Store).Where(x => !x.Deleted).Take(20).ToList() : db.Invoices.Include(i => i.Store).Where(x => !x.Deleted).Take(20).Skip(20 * page).ToList();

                foreach (var invoice in invoices)
                {
                    model.Invoices.Add(new InvoiceViewModel
                    {
                        Amount       = invoice.Amount,
                        Date         = invoice.Date,
                        Document     = invoice.Document,
                        DocumentType = invoice.DocumentType,
                        Number       = invoice.Number,
                        Store        = invoice.Store
                    });
                }
                model.Pages       = invoices.Count / 20;
                model.CurrentPage = page;
            }
            return View();
        }
    }
}