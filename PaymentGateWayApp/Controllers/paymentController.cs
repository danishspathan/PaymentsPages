using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PaymentGateWayApp.Controllers
{
    public class paymentController : Controller
    {
        // GET: payment
        public ActionResult Index()
        {
            ViewBag.source = Session["paymentUrl"];
            return View();
        }
    }
}