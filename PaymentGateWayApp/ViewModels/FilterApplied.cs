using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PaymentGateWayApp.ViewModels
{
    public class FilterApplied
    {
        //EmpCountRange, Position, Region, Country, State_, EmployeeCount, Companycount, Industry, CompanyName, Continent,
        //company_url, Subcategory, company_revenue
        public string Industries { get; set; }
        public string Position { get; set; }
        public string Titles { get; set; }
        public string CompanyName { get; set; }
        public string company_url { get; set; }
        public string SubCategory { get; set; }
        public string company_revenue { get; set; }
        public Locations location { get; set; }
        public bool isCompanyFilter { get; set; }
        public string HeaderTitle { get; set; }
    }
    public class Locations
    {
        public string Countries { get; set; }
        public string states { get; set; }
        public string Region { get; set; }
        public string Continent { get; set; }
    }
}