using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PaymentGateWayApp.Controllers
{
    public class CurrencyController : Controller
    {
        // GET: Currency
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult getCurrencList()
        {
            return View();
        }
    }
}