using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace PaymentGateWayApp.Controllers
{
    public class ReturnController : Controller
    {
        // GET: Return
        [HttpGet]
        public ActionResult Return()
        {
            return View();
        }
      
    }
}