using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Entity;

namespace PaymentGateWayApp.Models
{
    public class CountryContext : DbContext
    {
        public DbSet<CountryClass> countryClasses { get; set; }
    }
    
}