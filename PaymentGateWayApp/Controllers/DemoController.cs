using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using PaymentGateWayApp.Models;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.IO;
using System.Configuration;
using PaymentGateWayApp.Data;
using Logger;
using PaymentGateWayApp.ApiCalls;
using PaymentGateWayApp.PaymentEnum;
using BusinessAccessLayer;
using static PaymentGateWayApp.Models.UserDetails;

namespace PaymentGateWayApp.Controllers
{
    
    public class DemoController : Controller
    {

        private ILog _ILog;

        public DemoController()
        {
            _ILog = Log.GetInstance;
        }
        // Logging all Exception
        protected override void OnException(ExceptionContext filterContext)
        {
            _ILog.LogException(filterContext.Exception.ToString());
            filterContext.ExceptionHandled = true;
            this.View("Error").ExecuteResult(this.ControllerContext);
        }
        public ActionResult Index()
        {
            return View();
        }

        //Get Method for the zoho repllica form
        [HttpGet]
        //[Route("Demo/Demo/{planName:string}")]
        public ActionResult Demo(string planName)
        {


            DemoModel model = new DemoModel();
            countriesRepository countries = new countriesRepository();
            model.Countries = countries.GetCountriesSelectList();

            var planTip = new SelectListItem();
            List<SelectListItem> plans = new List<SelectListItem>();
            string planDetails = ReturnPlanDetails(planName, out plans, out planTip);
            ViewBag.planDetailsYearly = planDetails.Split('|')[0];
            ViewBag.planDetailsMonthly = planDetails.Split('|')[1];
            ViewBag.Country = "default";
            //if (planName != null)
            //{
            //    if (planName.Contains("Y"))
            //    {

            //        plans = planNamesYearly.Select(p => new SelectListItem { Value = p, Text = p.Replace("Yearly", "") }).ToList();

            //    }
            //    else
            //    {
            //        plans = planNames.Select(p => new SelectListItem { Value = p, Text = p }).ToList();


            //    }

            //    switch (planNo)
            //    {
            //        case 1:
            //            planTip.Value = enumPlan.ToString();
            //            planTip.Text = "Enterprise";
            //            ViewBag.planDetails = "36000 Contacts per month Additional credits $0.10";
            //            plans.Insert(0, planTip);
            //            return View(model);

            //        case 2:
            //            planTip.Value = enumPlan.ToString();
            //            planTip.Text = "Corporate";
            //            ViewBag.planDetails = "18000 Contacts per month Additional credits $0.13";
            //            break;
            //        case 3:
            //            planTip.Value = enumPlan.ToString();
            //            planTip.Text = "Professional";
            //            ViewBag.planDetails = "6000 Contacts per annum Additional credits $0.20";

            //            break;
            //        case 4:
            //            planTip.Value = enumPlan.ToString();
            //            planTip.Text = "Enterprise";
            //            ViewBag.planDetails = "3000 Contacts per month Additional credits $0.10";
            //            break;
            //        case 5:
            //            planTip.Value = enumPlan.ToString();
            //            planTip.Text = "Corporate";
            //            ViewBag.planDetails = "1500 Contacts per month Additional credits $0.13";
            //            break;
            //        case 6:
            //            planTip.Value = enumPlan.ToString();
            //            planTip.Text = "Professional";
            //            ViewBag.planDetails = "500 Contacts per month Additional credits $0.20";

            //            break;
            //        default:
            //            planTip.Value = "Professional";
            //            planTip.Text = "Professional";
            //            ViewBag.planDetails = "500 Contacts per month Additional credits $0.20";
            //            break;
            //    }

            //}
            //else
            //{
            //    plans = planNames.Select(p => new SelectListItem { Value = p, Text = p }).ToList();
            //    planTip.Value = "Professional";
            //    planTip.Text = "Professional";
            //    ViewBag.planDetails = "3000 Contacts per month Additional credits $0.10";

            //}
            plans.Insert(0, planTip);
            model.selectPlan = plans;
            return View(model);
        }
        //Posting the parameters to hosted Pages
        [HttpPost]
        public ActionResult Demo(DemoModel model)
        {


            if (ModelState.IsValid)
            {

                UserRepository userRepository = new UserRepository();
                #region creating Json object out of the posted variables
                string firstName = model.FirstName;
                string lastName = model.LastName;

                string companyName = model.Company;
                string strEmail = model.Email;
                string currency = model.currency;
                string vatTreatment = model.currency == "GBP" ? "uk" : "non_eu";
                if(model.planRadio=="Annual")
                {
                    model.Plans = model.Plans.Replace("Y", "") + "Y";
                }

                string password = Encryption.Encrypt(model.Password);
            
                bool ifUserexits = userRepository.checkIfUserxists(model.Email);

                if (!ifUserexits)
                {
                    //string price = ReturnPrice(model.Plans, currency);
                    var objplan = new
                    {
                        plan_code = model.Plans,
                        //price = price,
                        quantity = "1"

                    };
                    var billingAddress = new
                    {
                        country = model.hiddenCountry,

                    };

                    var CustomerData = new
                    {
                        display_name = firstName + " " + lastName,
                        //salutation = "Mr.",
                        first_name = firstName,
                        last_name = lastName,

                        email = strEmail,
                        company_name = companyName,
                        //vat_treatment = vatTreatment,


                        billing_address = billingAddress,
                        currency_code = currency

                    };
                    var finalData = new
                    {
                        customer = CustomerData,
                        plan = objplan,
                        redirect_url = "https://dev-search.datagenie.co/",


                    };

                 

                    string jsonFinal = JsonConvert.SerializeObject(finalData);
                    string zohoRefreshToken = ConfigurationManager.AppSettings["zohoRefreshToken"];
                    string ZohoAccessToken = ApiClass.RefreshToken(zohoRefreshToken).GetAwaiter().GetResult();
                    List<string> result = ApiClass.Geturl(jsonFinal, ZohoAccessToken).GetAwaiter().GetResult();

                    if (result.Count == 2)
                    {

                        userRepository.insertHostedPagesID(strEmail, password, result[1]);
                    }
                    ViewBag.source = result[0];
                    countriesRepository countries = new countriesRepository();
                    model.Countries = countries.GetCountriesSelectList();
                    var planTip = new SelectListItem();
                    List<SelectListItem> plans = new List<SelectListItem>();
                    ViewBag.planDetails = ReturnPlanDetails(model.Plans, out plans, out planTip);
                    plans.Insert(0, planTip);
                    model.selectPlan = plans;
                    return View(model);

                }

                else
                {
                    
                    countriesRepository countries = new countriesRepository();
                    model.Countries = countries.GetCountriesSelectList();
                    var planTip = new SelectListItem();
                    List<SelectListItem> plans = new List<SelectListItem>();
                    ViewBag.planDetails = ReturnPlanDetails(model.Plans, out plans, out planTip);
                    plans.Insert(0, planTip);
                    model.selectPlan = plans;
                    ViewBag.planError = "User already exists";
                    ViewBag.Country = model.hiddenCountry;
                    return View(model);
                   
                }

                #endregion
            }
            return View(model);
        }
        [HttpGet]
        public ActionResult getThankyoupage()
        {
            return View();
        }
        public ActionResult paymentPage()
        {
            return View();
        }
        [HttpGet]
        public ActionResult getCountriesAndCurrencies()
        {
            List<CountryClass> lstcountryClasses = new List<CountryClass>();
            using (var context = new CountryContext())
            {
                lstcountryClasses = context.countryClasses.ToList();

            }

            return View(lstcountryClasses);
        }
        [NonAction]
        public string ReturnPrice(string plan, string currentCountryCurrency)
        {
            string exchangeRate = ApiClass.GetExchangeRate(currentCountryCurrency).GetAwaiter().GetResult();
            string totalPrice = string.Empty;
            string refreshToken = ConfigurationManager.AppSettings["zohoPlanRefreshToken"];
            string accessToken = ApiClass.RefreshToken(refreshToken).GetAwaiter().GetResult();
            string price = ApiClass.GetPlanPrice(accessToken, plan).GetAwaiter().GetResult();
            double decPrice = double.Parse(price);

            if (exchangeRate == "1")
            {
                totalPrice = price;
            }
            else
            {
                decPrice = decPrice * double.Parse(exchangeRate);
                if (currentCountryCurrency == "GBP")
                {
                    decPrice = decPrice * 1.20;
                    totalPrice = Convert.ToString(decPrice);
                }
                else
                {
                    totalPrice = Convert.ToString(decPrice);
                }
            }
            return totalPrice;
        }
        public string ReturnPlanDetails(string planName, out List<SelectListItem> plans, out SelectListItem planTip)
        {
            string strViewbag = string.Empty;
            planEnum enumPlan = new planEnum();

            Enum.TryParse(planName, out enumPlan);
            int planNo = ((int)enumPlan);

            List<string> planNames = new List<string> { "Professional", "Corporate", "Enterprise" };
            List<string> planNamesYearly = new List<string> { "ProfessionalY", "CorporateY", "EnterpriseY" };
            planTip = new SelectListItem();
            if (planName != null)
            {
                if (planName.Contains("Y"))
                {

                    plans = planNamesYearly.Select(p => new SelectListItem { Value = p, Text = p.Replace("Y", "") }).ToList();

                }
                else
                {
                    plans = planNames.Select(p => new SelectListItem { Value = p, Text = p }).ToList();


                }


                switch (planNo)
                {
                    case 1:
                        planTip.Value = enumPlan.ToString();
                        planTip.Text = "Enterprise";
                        strViewbag = "36000 Contacts per month Additional credits $0.10"+ "|3000 Contacts per month Additional credits $0.10";
                        var result = plans.Where(r => r.Text == "Enterprise").FirstOrDefault();
                        if(result!=null) plans.Remove(result);  
                        break;


                    case 2:
                        planTip.Value = enumPlan.ToString();
                        planTip.Text = "Corporate";
                        strViewbag = "18000 Contacts per month Additional credits $0.13"+ "|1500 Contacts per month Additional credits $0.13";
                        var result1 = plans.Where(r => r.Text == "Corporate").FirstOrDefault();
                        if (result1 != null) plans.Remove(result1);
                        break;
                    case 3:
                        planTip.Value = enumPlan.ToString();
                        planTip.Text = "Professional";
                        strViewbag = "6000 Contacts per annum Additional credits $0.20"+ "|500 Contacts per month Additional credits $0.20";
                        var result2 = plans.Where(r => r.Text == "Professional").FirstOrDefault();
                        if (result2 != null) plans.Remove(result2);

                        break;
                    case 4:
                        planTip.Value = enumPlan.ToString();
                        planTip.Text = "Enterprise";
                        strViewbag = "36000 Contacts per month Additional credits $0.10" + "|3000 Contacts per month Additional credits $0.10";
                        var result3= plans.Where(r => r.Text == "Enterprise").FirstOrDefault();
                        if (result3 != null) plans.Remove(result3);
                        break;
                    case 5:
                        planTip.Value = enumPlan.ToString();
                        planTip.Text = "Corporate";
                        strViewbag = "18000 Contacts per month Additional credits $0.13" + "|1500 Contacts per month Additional credits $0.13";
                        var result4 = plans.Where(r => r.Text == "Enterprise").FirstOrDefault();
                        if (result4 != null) plans.Remove(result4);
                        break;
                    case 6:
                        planTip.Value = enumPlan.ToString();
                        planTip.Text = "Professional";
                        strViewbag = "6000 Contacts per annum Additional credits $0.20" + "|500 Contacts per month Additional credits $0.20";
                        var result5 = plans.Where(r => r.Text == "Professional").FirstOrDefault();
                        if (result5 != null) plans.Remove(result5);

                        break;
                    default:
                        planTip.Value = "Professional";
                        planTip.Text = "Professional";
                        strViewbag = "6000 Contacts per annum Additional credits $0.20" + "|500 Contacts per month Additional credits $0.20";
                        var result6 = plans.Where(r => r.Text == "Professional").FirstOrDefault();
                        if (result6 != null) plans.Remove(result6);
                        break;
                }

            }
            else
            {
                plans = planNames.Select(p => new SelectListItem { Value = p, Text = p }).ToList();
                planTip.Value = "Professional";
                planTip.Text = "Professional";
                var result7 = plans.Where(r => r.Text == "Professional").FirstOrDefault();
                if (result7 != null) plans.Remove(result7);
                strViewbag = "6000 Contacts per annum Additional credits $0.20" + "|500 Contacts per month Additional credits $0.20";

            }


            return strViewbag;
        }


    }
}