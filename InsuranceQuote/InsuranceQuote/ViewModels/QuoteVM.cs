using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InsuranceQuote.ViewModels
{
    public class QuoteVM
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public Nullable<System.DateTime> DateOfBirth { get; set; }
        public Nullable<short> CarYear { get; set; }
        public string CarMake { get; set; }
        public string CarModel { get; set; }
        public Nullable<bool> Dui { get; set; }
        public Nullable<byte> Tickets { get; set; }
        public string Coverage { get; set; }
        public Nullable<decimal> Cost { get; set; }
    }
}