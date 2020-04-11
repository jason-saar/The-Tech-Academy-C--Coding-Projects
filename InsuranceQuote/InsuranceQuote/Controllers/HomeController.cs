using InsuranceQuote.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InsuranceQuote.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Inquiry(string firstName, string lastName, string emailAddress, DateTime dateOfBirth, short year, string make, string model, byte tickets, string coverage, bool dui = false)
        {
            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) || string.IsNullOrEmpty(emailAddress) || dateOfBirth == DateTime.MinValue || string.IsNullOrEmpty(year.ToString()) || string.IsNullOrEmpty(make) || string.IsNullOrEmpty(model) || string.IsNullOrEmpty(tickets.ToString()) || string.IsNullOrEmpty(coverage))
            {
                return View("~/Views/Shared/Error.cshtml");
            }
            else
            {
                var quote = new Quote();
                decimal price = 50;
                if ((DateTime.Now.Year - dateOfBirth.Year) < 25)    //Is Age < 25? + $25/mo
                {
                    price += 25;
                    if ((DateTime.Now.Year - dateOfBirth.Year) < 18) //Is Age < 18? + $75/mo for total of $100/mo
                    {
                        price += 75;
                    }
                } else if ((DateTime.Now.Year - dateOfBirth.Year) > 100)    //Is Age > 100? + 25/mo
                {
                    price += 25;
                }
                if (year < 2000)
                {
                    price += 25;
                } else if (year > 2015)
                {
                    price += 25;
                }
                if (make == "Porsche")
                {
                    price += 25;
                }
                if (model == "911 Carrera")
                {
                    price += 25;
                }
                if (tickets > 0)
                {
                    price += 10 * tickets;
                }
                if (dui)
                {
                    price = Decimal.Multiply(price, (decimal)1.25);
                }
                if (coverage == "Full Coverage")
                {
                    price = Decimal.Multiply(price, (decimal)1.5);
                }
                price = Math.Round(price, 2);

                using (QuotesEntities db = new QuotesEntities())
                {
                    quote.FirstName = firstName;
                    quote.LastName = lastName;
                    quote.EmailAddress = emailAddress;
                    quote.DateOfBirth = dateOfBirth;
                    quote.CarYear = year;
                    quote.CarMake = make;
                    quote.CarModel = model;
                    quote.Dui = dui;
                    quote.Tickets = tickets;
                    quote.Coverage = coverage;
                    quote.Cost = price;

                    db.Quotes.Add(quote);
                    db.SaveChanges();
                }
                ViewData.Model = quote;
                return View();
            }
        }
    }
}