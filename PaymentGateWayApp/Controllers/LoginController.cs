using BusinessEntities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using BusinessAccessLayer;
using System.Threading;
using PaymentGateWayApp.CommonClasses;
using PaymentGateWayApp.Models;
using PaymentGateWayApp.DAL;
using AutomationData;
using PaymentGateWayApp.ViewModels;

namespace PaymentGateWayApp.Controllers
{

    public class LoginController : BaseController
    {
        // GET: Login
        [HttpGet]
        public ActionResult Index(int? ee)
        {
            try
            {
                HttpCookie read_cookie = Request.Cookies["Login"];
                DataSet ds = new DataSet();
                BusinessAccessLayer.Login ojBal = new BusinessAccessLayer.Login();
                
                if (read_cookie != null)
                {

                    LoginModel.UserLogin read_userlogin = ojBal.ValidateLoginCookie(read_cookie);
                    if (read_userlogin != null)
                    {
                        Session["User_Name"] = read_userlogin.UserName;
                        Session["Firstname"] = read_userlogin.Firstname;
                        Session["Password"] = read_userlogin.Password;
                        Session["Credit"] = read_userlogin.Credit;
                        Session["AccountType"] = read_userlogin.AccountType;

                        return View();
                    }
                    else
                    {
                        return View();
                    }
                }
                else
                {
                    ViewBag.providerVariable = ee;
                    return View();
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Error while loading Login Page" + ex.Message;
              
                return View();
            }
        }


        public ActionResult Logout()
        {
            try
            {
                //Response.Cookies.Remove("Login");
                HttpCookie remove_cookie11 = Request.Cookies["Login"];
                if (remove_cookie11 != null)
                {
                    remove_cookie11.Expires = DateTime.Now.AddDays(-1);
                    Response.Cookies.Add(remove_cookie11);
                    ViewBag.LogoutMessage = "Logged out successfully.";
                    Session.RemoveAll();
                }
                return View("Index");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Error while Sign out " + ex.Message;
             
                return View("Index");
            }
        }
        [HttpPost]
        public ActionResult Register(Models.UserDetails.UserRegistrationVM userRegistrationVM)
        {
            try
            {
                if (userRegistrationVM != null)
                {

                }
                return View();
            }
            catch (Exception ex)
            {
             
                return View();
                //throw;
            }
        }
        public ActionResult Enter_OTP()
        {
            return View();

        }
        public ActionResult Register()
        {
            try
            {
                string error = Convert.ToString(TempData["error"]);
                ViewBag.error = error;
                return View();
            }
            catch (Exception ex)
            {
               
                return View();
            }
        }
        [HttpPost]
        public ActionResult SaveRegistration(Models.UserDetails.UserLogin userLogin)
        {
            try
            {
                userLogin.UserName = userLogin.Email;
                DataSet ds = new DataSet();
                string result = "";
                userLogin.Password = Encryption.Encrypt(userLogin.Password);
                ds = DatabaseHelper.SaveRegistration(userLogin);

                //    return  this.Index(userLogin);

                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow drState in ds.Tables[0].Rows)
                        {
                            result = drState["Response"].ToString();
                        }
                    }
                }

                if (result == "Data saved successfully")
                {
                    userLogin.Password = Encryption.Decrypt(userLogin.Password);
                    return this.Index(userLogin);
                }
                else
                {
                    TempData["error"] = "User already exist";
                    Thread.Sleep(200);
                    // return this.Register();
                    return Redirect("/Login/Register");
                    //ViewBag.error = "User already exist";
                    //return  View();
                }
            }
            catch (Exception ex)
            {
            
                return Redirect("/Login/Register");
            }
        }
     
        

        public ActionResult GetResetPassword(int ID)
        {
            try
            {
                ViewBag.ID = ID;
                return View();
            }
            catch (Exception ex)
            {
        
                return View();
            }
        }
        public ActionResult SaveResetPassword(int UserID, string Password)
        {
            try
            {
                DataSet ds = DatabaseHelper.SaveResetPassword(UserID, Encryption.Encrypt(Password));
                //    return  this.Index(userLogin);
                string Response = "";
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {

                        foreach (DataRow drState in ds.Tables[0].Rows)
                        {
                            Response = Convert.ToString(drState["response"]);
                        }
                    }
                }
                return Json(Response);
            }
            catch (Exception ex)
            {
              
                string Response = "";
                return Json(Response);
            }
        }

        public ActionResult ResetPasswordForUserDeprecated(string Email)
        {
            try
            {

                string str = "select ClientID from tbl_ClientDetails where Email = '" + Email + "'";
                //string ClientID = Convert.ToString(httpCookie.Values["ClientID"]);
                DataSet ds = DatabaseHelper.executeScalar(str);
                int result = 0;
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow drState in ds.Tables[0].Rows)
                        {
                            result = Convert.ToInt32(drState["ClientID"]);
                        }
                    }
                }
                if (result > 0)
                {
                    bool isEmailSent = true;
                    string Subject = "";
                    string PortalName = "";
                    string ContactUsLink = "";
                    string ProviderEmail = "";
                    string Website = "";
                    //  string ContactNumber = "";
                    string FromEmailAddress = "";

                    PortalName = "DataGenie";
                    Subject = "Reset Password";
                    ContactUsLink = "https://www.datagenie.co";
                    ProviderEmail = "enquiries@datagenie.co";
                    Website = "www.datagenie.co";
                    //  ContactNumber = "London: +44 (0) 207 609 2800<br>New York: +1 (0) 646 757 1645<br>";
                    FromEmailAddress = "esgenie@esgglobalservices.com";
                    string strHtml = "";
                    strHtml += "<br>" + "Hello " + Email + "<br>";
                    strHtml += "<br>Please visit on the below link to reset your password.<br>";
                    strHtml += "<br><b><a href='http://search.datagenie.co/login/GetResetPassword?ID=" + result + "' target='_blank'>Click here to reset your password</a></b><br>";
                    strHtml += "<br>If you have any questions, or if you think someone unauthorised may have attempted to login to your account on DataGenie, then please contact us immediately on (" + ContactUsLink + " ) or email us at " + ProviderEmail + ".<br>";
                    strHtml += "<br><br>";
                    strHtml += "Regards <br>";
                    strHtml += "Support <br>";
                    strHtml += PortalName + "<br>";
                    //        strHtml += ContactNumber;
                    strHtml += ProviderEmail + "<br>";
                    strHtml += Website + "<br>";
                    string strLogDetails = strHtml;
                    isEmailSent = DatabaseHelper.sendEmailLogInfo("" + Email + "", "" + FromEmailAddress + "", "" + Subject + "", strLogDetails, 0);
                    if (isEmailSent == true)
                    {
                        return Json(1);
                    }
                    else
                    {
                        return Json(2);
                    }
                }
                else
                {
                    return Json(3);
                }
            }
            catch (Exception ex)
            {
               
                return Json(3);
            }
        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult IndexDeprecated(Models.UserDetails.UserLogin userLogin)
        {
            try
            {
                DataSet ds = new DataSet();
                if (userLogin != null)
                {
                    ds = CommonClasses.DatabaseHelper.CheckUserExists(userLogin.UserName, userLogin.Password);  //checks entered credentials are correct or not
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {

                        HttpCookie cookie = new HttpCookie("Login_OTP");
                        cookie.Values["ClientID"] = ds.Tables[0].Rows[0]["ClientID"].ToString();
                        Session["ClientID"] = ds.Tables[0].Rows[0]["ClientID"].ToString();
                        Session["AccountType"] = Convert.ToInt32(ds.Tables[0].Rows[0]["AccountType"].ToString());
                        Session["Credit"] = Convert.ToInt32(ds.Tables[0].Rows[0]["Credit"]);
                        cookie.Expires = DateTime.Now.AddHours(10);
                        Response.Cookies.Add(cookie);

                        //Code added here
                        #region New_Code_28June2021
                        bool IsTrustedDeviceCookieFound = false;
                        HttpCookie trustedDeviceCookieReq = Request.Cookies["TrustedDeviceCookie"];
                        if (trustedDeviceCookieReq != null && trustedDeviceCookieReq.Values["TrustedDevice"] != "")
                        {
                            String trustedDeviceCookieReqValue = trustedDeviceCookieReq.Values["TrustedDevice"].ToString();
                            String trustedDeviceCookieDBValue = ds.Tables[0].Rows[0]["TrustedDeviceNumber"].ToString();
                            if (String.Compare(trustedDeviceCookieReqValue, trustedDeviceCookieDBValue, false) == 0)
                            {
                                IsTrustedDeviceCookieFound = true;
                            }
                        }
                        if (IsTrustedDeviceCookieFound)
                        {
                            //Here are repeat code
                            //string strSQL1 = "";
                            //strSQL1 = "SELECT ClientID,ISNULL(Credit,0) Credit, UserName,Firstname,PASSWORD,ISACTIVE,IsAdmin,IsClient,AccountType,CompanyName,otp,ISNULL(ShowAnalytics,0) ShowAnalytics,Provider from tbl_ClientDetails where ClientID  = '" + Session["ClientID"].ToString() + "'";
                            //DataSet ds1 = DatabaseHelper.executeScalar(strSQL1);

                            //Get Client Details
                            LoginDAL ojDal = new LoginDAL();
                            DataSet ds1 = ojDal.GetClientDetails(Convert.ToInt32(Session["ClientID"]));

                            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                            {
                                Models.UserDetails.UserLogin userLogin1 = new Models.UserDetails.UserLogin();
                                HttpCookie cookie1 = new HttpCookie("Login");
                                userLogin1.UserName = ds1.Tables[0].Rows[0]["UserName"].ToString();
                                userLogin1.Password = ds1.Tables[0].Rows[0]["Password"].ToString();
                                userLogin1.Credit = Convert.ToInt32(ds1.Tables[0].Rows[0]["Credit"]);
                                cookie1.Values["UserName"] = userLogin1.UserName;
                                cookie1.Values["Password"] = userLogin1.Password;
                                cookie1.Values["Credit"] = userLogin1.Credit.ToString();

                                userLogin1.AccountType = Convert.ToInt32(ds1.Tables[0].Rows[0]["AccountType"].ToString());
                                userLogin1.Firstname = ds1.Tables[0].Rows[0]["Firstname"].ToString();
                                cookie1.Values["Firstname"] = userLogin1.Firstname;

                                cookie1.Values["AccountType"] = userLogin1.AccountType.ToString();
                                cookie1.Values["CompanyName"] = ds1.Tables[0].Rows[0]["CompanyName"].ToString();

                                Session["AccountType"] = userLogin1.AccountType.ToString();

                                Session["Password"] = userLogin1.Password;
                                Session["User_Name"] = userLogin1.UserName;
                                Session["Firstname"] = userLogin1.Firstname;
                                Session["Credit"] = userLogin1.Credit;
                                //new added
                                cookie1.Values["ClientID"] = ds1.Tables[0].Rows[0]["ClientID"].ToString();
                                Session["ClientID"] = ds1.Tables[0].Rows[0]["ClientID"].ToString();
                                cookie1.Values["Provider"] = ds1.Tables[0].Rows[0]["Provider"].ToString();
                                Session["Provider"] = ds1.Tables[0].Rows[0]["Provider"].ToString();

                                bool ShowAnalyticsFlag = Convert.ToBoolean(ds1.Tables[0].Rows[0]["ShowAnalytics"].ToString());
                                if (ShowAnalyticsFlag == true)
                                {
                                    cookie1.Values["ShowAnalytics"] = "1";
                                }
                                else
                                {
                                    cookie1.Values["ShowAnalytics"] = "0";
                                }
                                cookie1.Expires = DateTime.Now.AddHours(10);
                                Response.Cookies.Add(cookie1);
                                TempData["SuccessMessage"] = "Logged in successfully.";
                                if (userLogin1.AccountType == (int)Enums.Accounttype.SuperAdmin)
                                {
                                    return RedirectToAction("Details", "FilterData");
                                }
                                else if (userLogin1.AccountType == (int)Enums.Accounttype.Admin)
                                {
                                    return RedirectToAction("Details", "FilterData");
                                }
                                else
                                {
                                    return RedirectToAction("Data_Inventory", "ClientData");
                                }
                            }
                            else
                            {
                                return View("Index");
                            }
                        }
                        #endregion New_Code_28June2021
                        //***************************************************************
                        //string[] strArrayMacIDAndIP = new string[2];
                        //var macAddr =
                        //  (
                        //      from nic in NetworkInterface.GetAllNetworkInterfaces()
                        //      where nic.OperationalStatus == OperationalStatus.Up
                        //      select nic.GetPhysicalAddress().ToString()
                        //  ).FirstOrDefault();
                        //strArrayMacIDAndIP[0] = macAddr;

                        ////string strMacId = "";
                        //string strIpAddress = "";

                        //strIpAddress = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                        //if (strIpAddress == null)
                        //    strIpAddress = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];

                        //strArrayMacIDAndIP[1] = strIpAddress;

                        //***************************************************************** 

                        //Get IP Address
                        string[] strArrayMacIDAndIP = GetMacIDAndIPAddress();

                        string browserDetails = string.Empty;
                        System.Web.HttpBrowserCapabilities browser = System.Web.HttpContext.Current.Request.Browser;
                        browserDetails =
                        "<li><ul>Browser: " + browser.Browser + "</ul></li>  <li><ul>Browser version: " + browser.Version + "</ul></li>";


                        ////code for otp sending on email for login////// 

                        //string num = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                        //int len = num.Length;
                        //string otp = string.Empty;
                        //int otpdigit = 6;
                        //string finaldigit;
                        //int getindex;
                        //for (int i = 0; i < otpdigit; i++)
                        //{
                        //    do
                        //    {
                        //        getindex = new Random().Next(0, len);
                        //        finaldigit = num.ToCharArray()[getindex].ToString();
                        //    } while (otp.IndexOf(finaldigit) != -1);
                        //    otp += finaldigit;
                        //}

                        //GetOTP
                     AutomationData.Common objCommon = new AutomationData.Common();
                        string otp = objCommon.GenerateOTP();

                        string firstname = ds.Tables[0].Rows[0]["FirstName"].ToString();
                        string strSqlEmail = "";
                        string clientEmail = "";
                        string Provider = "";
                        System.Data.DataTable dtemail;
                        dtemail = new DataTable();
                        strSqlEmail = "Select Email,Provider from  tbl_ClientDetails where ClientID  = '" + Session["ClientID"] + "'";
                        dtemail = CommonClasses.DatabaseHelper.FuncExecuteReader(strSqlEmail);
                        clientEmail = dtemail.Rows[0]["Email"].ToString();
                        Provider = dtemail.Rows[0]["Provider"].ToString();
                        Session["Provider"] = dtemail.Rows[0]["Provider"].ToString();
                        string Subject = "";
                        string PortalName = "";
                        string ContactUsLink = "";
                        string ProviderEmail = "";
                        string Website = "";
                        //  string ContactNumber = "";
                        string FromEmailAddress = "";

                        Subject = "Your DataGenie authentication code";
                        PortalName = "DataGenie";
                        ContactUsLink = "https://www.datagenie.co";
                        ProviderEmail = "enquiries@datagenie.co";
                        Website = "www.datagenie.co";
                        //  ContactNumber = "London: +44 (0) 207 609 2800<br>New York: +1 (0) 646 757 1645<br>";
                        FromEmailAddress = "esgenie@esgglobalservices.com";


                        string strHtml = "";
                        strHtml += "<br>" + "Hello " + firstname + "<br>";
                        strHtml += "<br>Please find below your two-factor authentication code from DataGenie’s data portal: SearchGenie.<br>";
                        strHtml += "<br><b>Authentication code: " + otp + "</b><br>";
                        strHtml += "<br>Please note that this code is valid only for 5 minutes from when it was generated.<br>";
                        strHtml += "<br>";
                        strHtml += "For your information, the below are the details of the person and device attempting to login:";
                        strHtml += " <li><ul> Username: " + userLogin.UserName + "</ul></li>";
                        strHtml += " <li><ul> IP address: " + strArrayMacIDAndIP[1].ToString() + "</ul></li>";
                        strHtml += " <li><ul> MAC address of the device: " + strArrayMacIDAndIP[0].ToString() + ".</ul></li>";
                        strHtml += browserDetails;
                        strHtml += "<br>If you have any questions, or if you think someone unauthorised may have attempted to login to your account on DataGenie, then please contact us immediately on (" + ContactUsLink + " ) or email us at " + ProviderEmail + ".<br>";
                        strHtml += "<br><br>";

                        strHtml += "Regards <br>";
                        strHtml += "Support <br>";
                        strHtml += PortalName + "<br>";
                        //        strHtml += ContactNumber;
                        strHtml += ProviderEmail + "<br>";
                        strHtml += Website + "<br>";


                        string strLogDetails = strHtml;
                        //  bool isEmailSent = true;

                        bool isEmailSent = true;



                        // added new
                        string strSqlFlag = "";
                        System.Data.DataTable dtFlag;
                        dtFlag = new DataTable();
                        strSqlFlag = "Select TestModeFlag from TestModeTable where 1=1";
                        dtFlag = CommonClasses.DatabaseHelper.FuncExecuteReader(strSqlFlag);
                        bool TestModeFlag = Convert.ToBoolean(dtFlag.Rows[0]["TestModeFlag"].ToString());
                        //added new

                        if (Convert.ToInt32(Session["AccountType"]) == (int)Enums.Accounttype.Client && TestModeFlag == true)
                        {
                            isEmailSent = DatabaseHelper.sendEmailLogInfo("shishirm@esgglobalservices.com", "esgenie@esgglobalservices.com", "ESGenie two-factor authentication code-Test Mode Turned On", strLogDetails, 0);
                            //isEmailSent = DatabaseHelper.sendEmailLogInfo("gerryl@esgglobalservices.com", "esgenie@esgglobalservices.com", "ESGenie two-factor authentication code-Test Mode Turned On", strLogDetails, 0);
                        }
                        else
                        {
                            //isEmailSent = DatabaseHelper.sendEmailLogInfo("" + clientEmail + "", "" + FromEmailAddress + "", "" + Subject + "", strLogDetails, 0);
                        }

                        if (isEmailSent)
                        {
                            string strSqlupdt = string.Empty;
                            strSqlupdt = "";
                            strSqlupdt += "update TBL_CLIENTDETAILS set otp ='" + otp + "' where ClientID  = '" + Session["ClientID"] + "'";
                            int rowseffected = CommonClasses.DatabaseHelper.FunExecuteNonquery(strSqlupdt);
                            if (rowseffected > 0)
                            {
                                return RedirectToAction("Enter_OTP", "Login");
                            }
                            else
                            {
                                ViewBag.ErrorMessage = "Error while logging in";
                                return View();

                            }
                        }
                        else
                        {
                            ViewBag.ErrorMessage = "Error while sending email ";
                            return View();
                        }


                        /*      if (userLogin.AccountType == (int)Enums.Accounttype.SuperAdmin)
                              {
                                  return RedirectToAction("Dashboard", "Home");
                              }
                              else if (userLogin.AccountType == (int)Enums.Accounttype.Admin)
                              {
                                  return RedirectToAction("Dashboard", "Home");
                              }
                              else
                              {
                                  return RedirectToAction("Data_Inventory", "ClientData");
                              }
                              */
                        #region
                        //if (userLogin.IsAdmin==true)
                        //{
                        //    return RedirectToAction("Dashboard", "Home");
                        //}
                        //else
                        //{
                        //    return RedirectToAction("DataInventory", "ClientData");
                        //}
                        #endregion
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "User name or password incorrect.";
                        return View();
                    }
                }
                else
                {
                    ViewBag.ErrorMessage = "Please enter login credentials.";
                    return View();
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Error while logging " + ex.Message;
            
                return View();
            }
        }
        public ActionResult Verify_OTPDeprecated(Models.UserDetails.OTPDetails otpdetails)
        {
            try
            {
                string form_otp = otpdetails.otp;
                if (!Regex.Match(form_otp, "^[A-Z0-9]{6}$").Success)
                {
                    ViewBag.ErrorMessage = "Invalid authentication code. Please enter the correct code.";
                    return View("Enter_OTP");
                }
                HttpCookie read_cookie = Request.Cookies["Login_OTP"];
                if (read_cookie != null && read_cookie.Values["ClientID"] != "")
                {
                    string ClientID = read_cookie.Values["ClientID"];
                    string strSQL1 = "";
                    strSQL1 = "SELECT ClientID,ISNULL(Credit,0) Credit, UserName,Firstname,PASSWORD,ISACTIVE,IsAdmin,IsClient,AccountType,CompanyName,otp,ISNULL(ShowAnalytics,0) ShowAnalytics,Provider from tbl_ClientDetails where ClientID  = '" + ClientID + "'";
                    DataSet ds = DatabaseHelper.executeScalar(strSQL1);
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        string otp = ds.Tables[0].Rows[0]["otp"].ToString();
                        if (otp == form_otp)
                        {
                            LoginModel.UserLogin userLogin = new LoginModel.UserLogin();
                            HttpCookie cookie = new HttpCookie("Login");
                            userLogin.UserName = ds.Tables[0].Rows[0]["UserName"].ToString();
                            userLogin.Password = ds.Tables[0].Rows[0]["Password"].ToString();
                            userLogin.Credit = Convert.ToInt32(ds.Tables[0].Rows[0]["Credit"]);
                            cookie.Values["UserName"] = userLogin.UserName;
                            cookie.Values["Password"] = userLogin.Password;
                            cookie.Values["Credit"] = userLogin.Credit.ToString();

                            userLogin.AccountType = Convert.ToInt32(ds.Tables[0].Rows[0]["AccountType"].ToString());
                            userLogin.Firstname = ds.Tables[0].Rows[0]["Firstname"].ToString();
                            cookie.Values["Firstname"] = userLogin.Firstname;

                            cookie.Values["AccountType"] = userLogin.AccountType.ToString();
                            cookie.Values["CompanyName"] = ds.Tables[0].Rows[0]["CompanyName"].ToString();

                            Session["AccountType"] = userLogin.AccountType.ToString();

                            Session["Password"] = userLogin.Password;
                            Session["User_Name"] = userLogin.UserName;
                            Session["Firstname"] = userLogin.Firstname;
                            Session["Credit"] = userLogin.Credit;
                            //new added
                            cookie.Values["ClientID"] = ds.Tables[0].Rows[0]["ClientID"].ToString();
                            Session["ClientID"] = ds.Tables[0].Rows[0]["ClientID"].ToString();
                            cookie.Values["Provider"] = ds.Tables[0].Rows[0]["Provider"].ToString();
                            Session["Provider"] = ds.Tables[0].Rows[0]["Provider"].ToString();

                            bool ShowAnalyticsFlag = Convert.ToBoolean(ds.Tables[0].Rows[0]["ShowAnalytics"].ToString());
                            if (ShowAnalyticsFlag == true)
                            {
                                cookie.Values["ShowAnalytics"] = "1";
                            }
                            else
                            {
                                cookie.Values["ShowAnalytics"] = "0";
                            }
                            cookie.Expires = DateTime.Now.AddHours(10);
                            Response.Cookies.Add(cookie);

                            #region TrustedDeviceCode
                            if (otpdetails.TrustedDevice == true)
                            {
                                Random generator = new Random();
                                String r = generator.Next(0, 1000000).ToString("D6");

                                HttpCookie trustedDeviceCookie = new HttpCookie("TrustedDeviceCookie");
                                trustedDeviceCookie.Values["TrustedDevice"] = r;
                                trustedDeviceCookie.Expires = DateTime.Now.AddDays(30);
                                Response.Cookies.Add(trustedDeviceCookie);

                                string strSqlupdt = string.Empty;
                                strSqlupdt = "update tbl_Clientdetails set TrustedDeviceNumber ='" + r.ToString() + "' where ClientID  = '" + Session["ClientID"] + "'";
                                int rowseffected = CommonClasses.DatabaseHelper.FunExecuteNonquery(strSqlupdt);
                            }

                            #endregion TrustedDeviceCode

                            TempData["SuccessMessage"] = "Logged in successfully.";
                            //   int AccountType = Convert.ToInt32(Session["AccountType"]);
                            //int AccountType = Convert.ToInt32(read_cookie.Values["AccountType"]);
                            if (userLogin.AccountType == (int)Enums.Accounttype.SuperAdmin)
                            {
                                return RedirectToAction("Details", "FilterData");
                            }
                            else if (userLogin.AccountType == (int)Enums.Accounttype.Admin)
                            {
                                return RedirectToAction("Details", "FilterData");
                            }
                            else
                            {
                                return RedirectToAction("Data_Inventory", "ClientData");
                            }
                        }
                        else
                        {
                            ViewBag.ErrorMessage = "Invalid authentication code. Please enter the correct code.";
                            return View("Enter_OTP");
                        }
                    }
                    else
                    {
                        return View("Index");
                    }
                }
                else
                {
                    return View("Index");
                }
            }
            catch (Exception ex)
            {
                LogError(ex);
                return View("Index");
            }
        }


        [HttpPost]
        public ActionResult Index(Models.UserDetails.UserLogin userLogin)
        {
            try
            {
                DataSet ds = new DataSet();
                if (userLogin != null)
                {
                 AutomationData.Login obj = new AutomationData.Login();

                    ds = obj.CheckUserExists(userLogin.UserName, Encryption.Encrypt(userLogin.Password));  //checks entered credentials are correct or not

                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                     AutomationData.Login ojDal = new AutomationData.Login();
                        HttpCookie cookie = new HttpCookie("Login_OTP");
                        cookie.Values["ClientID"] = ds.Tables[0].Rows[0]["ClientID"].ToString();
                        Session["ClientID"] = ds.Tables[0].Rows[0]["ClientID"].ToString();
                        Session["AccountType"] = Convert.ToInt32(ds.Tables[0].Rows[0]["AccountType"].ToString());
                        Session["Credit"] = Convert.ToInt32(ds.Tables[0].Rows[0]["Credit"]);
                        cookie.Expires = DateTime.Now.AddHours(10);
                        Response.Cookies.Add(cookie);

                        //Code added here
                        //#region New_Code_28June2021
                        //bool IsTrustedDeviceCookieFound = false;
                        //HttpCookie trustedDeviceCookieReq = Request.Cookies["TrustedDeviceCookie"];
                        //if (trustedDeviceCookieReq != null && trustedDeviceCookieReq.Values["TrustedDevice"] != "")
                        //{
                        //    String trustedDeviceCookieReqValue = trustedDeviceCookieReq.Values["TrustedDevice"].ToString();
                        //    String trustedDeviceCookieDBValue = ds.Tables[0].Rows[0]["TrustedDeviceNumber"].ToString();
                        //    if (String.Compare(trustedDeviceCookieReqValue, trustedDeviceCookieDBValue, false) == 0)
                        //    {
                        //        IsTrustedDeviceCookieFound = true;
                        //    }
                        //}
                        //if (IsTrustedDeviceCookieFound)
                        {
                            //Get Client Details
                            DataSet ds1 = ojDal.GetClientDetails(Convert.ToInt32(Session["ClientID"]));

                            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                            {
                                Model.UserLogin userLogin1 = new Model.UserLogin();
                                HttpCookie cookie1 = new HttpCookie("Login");
                                userLogin1.UserName = ds1.Tables[0].Rows[0]["UserName"].ToString();
                                userLogin1.Password = ds1.Tables[0].Rows[0]["Password"].ToString();
                                userLogin1.Credit = Convert.ToInt32(ds1.Tables[0].Rows[0]["Credit"]);
                                cookie1.Values["UserName"] = userLogin1.UserName;
                                cookie1.Values["Password"] = userLogin1.Password;
                                cookie1.Values["Credit"] = userLogin1.Credit.ToString();

                                userLogin1.AccountType = Convert.ToInt32(ds1.Tables[0].Rows[0]["AccountType"].ToString());
                                userLogin1.Firstname = ds1.Tables[0].Rows[0]["Firstname"].ToString();
                                cookie1.Values["Firstname"] = userLogin1.Firstname;

                                cookie1.Values["AccountType"] = userLogin1.AccountType.ToString();
                                cookie1.Values["CompanyName"] = ds1.Tables[0].Rows[0]["CompanyName"].ToString();

                                Session["AccountType"] = userLogin1.AccountType.ToString();

                                Session["Password"] = userLogin1.Password;
                                Session["User_Name"] = userLogin1.UserName;
                                Session["Firstname"] = userLogin1.Firstname;
                                Session["Credit"] = userLogin1.Credit;
                                //new added
                                cookie1.Values["ClientID"] = ds1.Tables[0].Rows[0]["ClientID"].ToString();
                                Session["ClientID"] = ds1.Tables[0].Rows[0]["ClientID"].ToString();
                                cookie1.Values["Provider"] = ds1.Tables[0].Rows[0]["Provider"].ToString();
                                Session["Provider"] = ds1.Tables[0].Rows[0]["Provider"].ToString();

                                return RedirectToAction("Demo", "Demo");
                                //bool ShowAnalyticsFlag = Convert.ToBoolean(ds1.Tables[0].Rows[0]["ShowAnalytics"].ToString());
                                //if (ShowAnalyticsFlag == true)
                                //{
                                //    cookie1.Values["ShowAnalytics"] = "1";
                                //}
                                //else
                                //{
                                //    cookie1.Values["ShowAnalytics"] = "0";
                                //}
                                //cookie1.Expires = DateTime.Now.AddHours(10);
                                //Response.Cookies.Add(cookie1);
                                //TempData["SuccessMessage"] = "Logged in successfully.";
                                //if (userLogin1.AccountType == (int)Enums.Accounttype.SuperAdmin)
                                //{
                                //    return RedirectToAction("Details", "FilterData");
                                //}
                                //else if (userLogin1.AccountType == (int)Enums.Accounttype.Admin)
                                //{
                                //    return RedirectToAction("Details", "FilterData");
                                //}
                                //else
                                //{
                                //    return RedirectToAction("Data_Inventory", "ClientData");
                                //}
                            }
                            else
                            {
                                return View("Index");
                            }
                        }
                        //#endregion New_Code_28June2021
                        //Get IP Address
                        string[] strArrayMacIDAndIP = GetMacIDAndIPAddress();

                        //GetOTP
                     AutomationData.Common objCommon = new AutomationData.Common();
                        string otp = objCommon.GenerateOTP();

                        //Get Browser details
                        System.Web.HttpBrowserCapabilities browser = System.Web.HttpContext.Current.Request.Browser;

                        TokenModel objToken = new TokenModel();
                        objToken.EmailID = userLogin.UserName;
                        objToken.OTP = otp;
                        objToken.FirstName = ds.Tables[0].Rows[0]["FirstName"].ToString();
                        objToken.UserEmail = userLogin.UserName;
                        objToken.IPAddress = strArrayMacIDAndIP[1].ToString();
                        objToken.MacAddress = strArrayMacIDAndIP[0].ToString();
                        objToken.BrowserName = browser.Browser;
                        objToken.BrowserVersion = browser.Version;

                        Session["Provider"] = ds.Tables[0].Rows[0]["Provider"].ToString();

                        bool isEmailSent = true;

                        // added new
                        string strSqlFlag = "";
                        System.Data.DataTable dtFlag;
                        dtFlag = new DataTable();
                        strSqlFlag = "Select TestModeFlag from TestModeTable where 1=1";
                        dtFlag = CommonClasses.DatabaseHelper.FuncExecuteReader(strSqlFlag);
                        bool TestModeFlag = Convert.ToBoolean(dtFlag.Rows[0]["TestModeFlag"].ToString());
                        //added new

                        if (Convert.ToInt32(Session["AccountType"]) == (int)Enums.Accounttype.Client && TestModeFlag == true)
                        {
                            //  isEmailSent = DatabaseHelper.sendEmailLogInfo("shishirm@esgglobalservices.com", "esgenie@esgglobalservices.com", "ESGenie two-factor authentication code-Test Mode Turned On", strLogDetails, 0);
                            //isEmailSent = DatabaseHelper.sendEmailLogInfo("gerryl@esgglobalservices.com", "esgenie@esgglobalservices.com", "ESGenie two-factor authentication code-Test Mode Turned On", strLogDetails, 0);

                            objToken.EmailID = ConfigurationManager.AppSettings["TestModeEmailID"].ToString();
                            objCommon.SendEmailFromTemplate(objToken, CommunicationTemplate.AuthenticationCode);
                        }
                        else
                        {
                            objCommon.SendEmailFromTemplate(objToken, CommunicationTemplate.AuthenticationCode);
                        }

                        if (isEmailSent)
                        {
                            int rowseffected = ojDal.UpdateClientOtp(otp, Session["ClientID"].ToString());

                            if (rowseffected > 0)
                            {
                                return RedirectToAction("Enter_OTP", "Login");
                            }
                            else
                            {
                                ViewBag.ErrorMessage = "Error while logging in";
                                return View();

                            }
                        }
                        else
                        {
                            ViewBag.ErrorMessage = "Error while sending email ";
                            return View();
                        }

                    }
                    else
                    {
                        ViewBag.ErrorMessage = "User name or password incorrect.";
                        return View();
                    }
                }
                else
                {
                    ViewBag.ErrorMessage = "Please enter login credentials.";
                    return View();
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Error while logging " + ex.Message;
           
                return View();
            }
        }

        public ActionResult ResetPasswordForUser(string Email)
        {
            try
            {
             AutomationData.Login objDal = new AutomationData.Login();

                int clientID = objDal.GetClientIDFromEmail(Email);

                if (clientID > 0)
                {
                    TokenModel objToken = new TokenModel();
                    objToken.EmailID = Email;
                    objToken.UserEmail = Email;
                    objToken.ClientID = clientID;

                    bool isEmailSent = true;

                 AutomationData.Common objCommon = new AutomationData.Common();
                    isEmailSent = objCommon.SendEmailFromTemplate(objToken, CommunicationTemplate.ResetPassword);

                    if (isEmailSent == true)
                    {
                        return Json(1);
                    }
                    else
                    {
                        return Json(2);
                    }
                }
                else
                {
                    return Json(3);
                }
            }
            catch (Exception ex)
            {
             
                return Json(3);
            }
        }

        public ActionResult Verify_OTP(Models.UserDetails.OTPDetails otpdetails)
        {
            try
            {
                string form_otp = otpdetails.otp;
                if (!Regex.Match(form_otp, "^[A-Z0-9]{6}$").Success)
                {
                    ViewBag.ErrorMessage = "Invalid authentication code. Please enter the correct code.";
                    return View("Enter_OTP");
                }
                HttpCookie read_cookie = Request.Cookies["Login_OTP"];
                if (read_cookie != null && read_cookie.Values["ClientID"] != "")
                {
                    string ClientID = read_cookie.Values["ClientID"];

                 AutomationData.Login ojDal = new AutomationData.Login();
                    DataSet ds = ojDal.GetClientDetails(Convert.ToInt32(Session["ClientID"]));

                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        string otp = ds.Tables[0].Rows[0]["otp"].ToString();
                        if (otp == form_otp)
                        {
                            LoginModel.UserLogin userLogin = new LoginModel.UserLogin();
                            HttpCookie cookie = new HttpCookie("Login");
                            userLogin.UserName = ds.Tables[0].Rows[0]["UserName"].ToString();
                            userLogin.Password = ds.Tables[0].Rows[0]["Password"].ToString();
                            userLogin.Credit = Convert.ToInt32(ds.Tables[0].Rows[0]["Credit"]);
                            cookie.Values["UserName"] = userLogin.UserName;
                            cookie.Values["Password"] = userLogin.Password;
                            cookie.Values["Credit"] = userLogin.Credit.ToString();

                            userLogin.AccountType = Convert.ToInt32(ds.Tables[0].Rows[0]["AccountType"].ToString());
                            userLogin.Firstname = ds.Tables[0].Rows[0]["Firstname"].ToString();
                            cookie.Values["Firstname"] = userLogin.Firstname;

                            cookie.Values["AccountType"] = userLogin.AccountType.ToString();
                            cookie.Values["CompanyName"] = ds.Tables[0].Rows[0]["CompanyName"].ToString();

                            Session["AccountType"] = userLogin.AccountType.ToString();

                            Session["Password"] = userLogin.Password;
                            Session["User_Name"] = userLogin.UserName;
                            Session["Firstname"] = userLogin.Firstname;
                            Session["Credit"] = userLogin.Credit;
                            //new added
                            cookie.Values["ClientID"] = ds.Tables[0].Rows[0]["ClientID"].ToString();
                            Session["ClientID"] = ds.Tables[0].Rows[0]["ClientID"].ToString();
                            cookie.Values["Provider"] = ds.Tables[0].Rows[0]["Provider"].ToString();
                            Session["Provider"] = ds.Tables[0].Rows[0]["Provider"].ToString();

                            bool ShowAnalyticsFlag = Convert.ToBoolean(ds.Tables[0].Rows[0]["ShowAnalytics"].ToString());
                            if (ShowAnalyticsFlag == true)
                            {
                                cookie.Values["ShowAnalytics"] = "1";
                            }
                            else
                            {
                                cookie.Values["ShowAnalytics"] = "0";
                            }
                            cookie.Expires = DateTime.Now.AddHours(10);
                            Response.Cookies.Add(cookie);

                            #region TrustedDeviceCode
                            if (otpdetails.TrustedDevice == true)
                            {
                                Random generator = new Random();
                                String r = generator.Next(0, 1000000).ToString("D6");

                                HttpCookie trustedDeviceCookie = new HttpCookie("TrustedDeviceCookie");
                                trustedDeviceCookie.Values["TrustedDevice"] = r;
                                trustedDeviceCookie.Expires = DateTime.Now.AddDays(30);
                                Response.Cookies.Add(trustedDeviceCookie);

                                string strSqlupdt = string.Empty;
                                strSqlupdt = "update tbl_Clientdetails set TrustedDeviceNumber ='" + r.ToString() + "' where ClientID  = '" + Session["ClientID"] + "'";
                                SqlHelper sqlHelper = new SqlHelper();
                                int rowseffected = sqlHelper.ExecuteQry(strSqlupdt);
                            }

                            #endregion TrustedDeviceCode

                            TempData["SuccessMessage"] = "Logged in successfully.";

                            if (userLogin.AccountType == (int)Enums.Accounttype.SuperAdmin)
                            {
                                return RedirectToAction("Details", "FilterData");
                            }
                            else if (userLogin.AccountType == (int)Enums.Accounttype.Admin)
                            {
                                return RedirectToAction("Details", "FilterData");
                            }
                            else
                            {
                                return RedirectToAction("Data_Inventory", "ClientData");
                            }
                        }
                        else
                        {
                            ViewBag.ErrorMessage = "Invalid authentication code. Please enter the correct code.";
                            return View("Enter_OTP");
                        }
                    }
                    else
                    {
                        return View("Index");
                    }
                }
                else
                {
                    return View("Index");
                }
            }
            catch (Exception ex)
            {
         
                return View("Index");
            }
        }

        //[HttpPost]
        //public ActionResult SaveRegistrationFreeTrial(Models.UserDetails.UserLogin userLogin)
        //{
        //    try
        //    {
        //        userLogin.UserName = userLogin.Email;
        //        DataSet ds = new DataSet();
        //        string result = "";
        //        userLogin.Password = Encryption.Encrypt(userLogin.Password);
        //        ds = DatabaseHelper.SaveRegistrationFreeTrial(userLogin);
        //        if (ds.Tables.Count > 0)
        //        {
        //            if (ds.Tables[0].Rows.Count > 0)
        //            {
        //                foreach (DataRow drState in ds.Tables[0].Rows)
        //                {
        //                    result = drState["Response"].ToString();
        //                }
        //            }
        //        }

        //        if (result == "Data saved successfully")
        //        {
        //            userLogin.Password = Encryption.Decrypt(userLogin.Password);
        //            // this.EmailFreeTrial(userLogin);
        //            return this.EmailFreeTrial(userLogin);
        //        }
        //        else
        //        {
        //            TempData["error"] = "User already exist";
        //            Thread.Sleep(200);
        //            // return this.Register();
        //            return Redirect("/Login/Register");
        //            //ViewBag.error = "User already exist";
        //            //return  View();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
               
        //        return Redirect("/Login/Register");
        //    }
        //}

        //[HttpPost]
        //public ActionResult UpdateFreeTrialInDays(Models.UserDetails.UserLogin userLogin)
        //{
        //    try
        //    {
        //        userLogin.UserName = userLogin.Email;
        //        DataSet ds = new DataSet();
        //        string result = "";
        //        userLogin.Password = Encryption.Encrypt(userLogin.Password);
        //        ds = DatabaseHelper.SaveRegistrationFreeTrial(userLogin);
        //        if (ds.Tables.Count > 0)
        //        {
        //            if (ds.Tables[0].Rows.Count > 0)
        //            {
        //                foreach (DataRow drState in ds.Tables[0].Rows)
        //                {
        //                    result = drState["Response"].ToString();
        //                }
        //            }
        //        }

        //        if (result == "Data saved successfully")
        //        {
        //            userLogin.Password = Encryption.Decrypt(userLogin.Password);
        //            return this.Index(userLogin);
        //        }
        //        else
        //        {
        //            TempData["error"] = "User already exist";
        //            Thread.Sleep(200);
        //            // return this.Register();
        //            return Redirect("/Login/Register");
        //            //ViewBag.error = "User already exist";
        //            //return  View();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
            
        //        return Redirect("/Login/Register");
        //    }
        //}

        //public ActionResult EmailFreeTrial(Models.UserDetails.UserLogin userLogin)
        //{

        // AutomationData.Login objDal = new AutomationData.Login();

        //    //int clientID = objDal.GetClientIDFromEmail(userLogin.Email);

        //    //if (clientID > 0)
        //    //{
        //    //    TokenModel objToken = new TokenModel();
        //    //   objToken.EmailID = userLogin.Email;
        //    //    objToken.UserEmail = userLogin.Email;
        //    //    objToken.ClientID = Convert.ToInt32(userLogin.ClientID);

        //    TokenModel objToken = new TokenModel();
        //    objToken.FirstName = userLogin.Firstname;
        //    objToken.LastName = userLogin.LastName;
        //    objToken.UserEmail = userLogin.UserName;
        //    objToken.EmailID = userLogin.Email;
        //    objToken.Password = userLogin.Password;
        //    bool isEmailSent = true;

        // AutomationData.Common objCommon = new AutomationData.Common();
        //    isEmailSent = objCommon.SendEmailFromTemplate(objToken, CommunicationTemplate.FreeTrialEmail);

        //    if (isEmailSent == true)
        //    {
        //        return Json(1);
        //    }
        //    else
        //    {
        //        return Json(2);
        //    }

        //}
    }

}