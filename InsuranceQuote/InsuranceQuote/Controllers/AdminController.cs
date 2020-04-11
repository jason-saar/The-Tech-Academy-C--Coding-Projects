using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InsuranceQuote.Models;
using InsuranceQuote.ViewModels;

namespace InsuranceQuote.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            using (QuotesEntities db = new QuotesEntities())
            {
                var quotes = db.Quotes.ToList();
                var quoteVms = new List<QuoteVM>();
                foreach (var quote in quotes)
                {
                    var quoteVm = new QuoteVM();
                    quoteVm.Id = quote.Id;
                    quoteVm.FirstName = quote.FirstName;
                    quoteVm.LastName = quote.LastName;
                    quoteVm.EmailAddress = quote.EmailAddress;
                    quoteVm.DateOfBirth = quote.DateOfBirth;
                    quoteVm.CarYear = quote.CarYear;
                    quoteVm.CarMake = quote.CarMake;
                    quoteVm.CarModel = quote.CarModel;
                    quoteVm.Dui = quote.Dui;
                    quoteVm.Tickets = quote.Tickets;
                    quoteVm.Coverage = quote.Coverage;
                    quoteVm.Cost = quote.Cost;
                    quoteVms.Add(quoteVm);
                }
                return View(quoteVms);
            }
        }
    }
}