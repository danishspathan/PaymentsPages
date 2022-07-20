using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PaymentGateWayApp.ViewModels
{
    public class SearchFilterVM
    {
        public string Title { get; set; }
        public int Id { get; set; }
        public int srNo { get; set; }
    }
    public class Category
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public int srNo { get; set; }
    }
}