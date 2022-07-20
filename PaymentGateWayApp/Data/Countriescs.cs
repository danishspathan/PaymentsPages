using PaymentGateWayApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PaymentGateWayApp.Data
{
    public class Countriescs
    {
        List<CountryClass> lstcountryClasses = new List<CountryClass>();
        public Countriescs()
        {

            using (var context = new CountryContext())
            {
                lstcountryClasses = context.countryClasses.ToList();
                
            }

        }
    }
}