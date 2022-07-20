using PaymentGateWayApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PaymentGateWayApp.Data
{
    public class countriesRepository
    {

        public IEnumerable<SelectListItem> GetCountriesSelectList()
        {
        
            using (var context = new CountryContext())
            {
                List<SelectListItem> countries = context.countryClasses.AsNoTracking()
                    .OrderBy(n => n.Country)
                        .Select(n =>
                        new SelectListItem
                        {
                            Value = n.Currency.ToString(), 
                            Text = n.Country
                        }).ToList();
                var countrytip = new SelectListItem()
                {
                    Value = null,
                    Text = "--- select country ---"
                };
                countries.Insert(0, countrytip);
                return new SelectList(countries, "Value", "Text");
            }
        }
        public string getCOuntry(string countryCode)
        {
            string country = string.Empty;
            using (var context = new CountryContext())
            {
                var result = context.countryClasses.Where(r => r.TwoDigitCode == countryCode).Select(r => r.Country).FirstOrDefault();
                country = result;
            }
            return country;
        }
      
    }
}