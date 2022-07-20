using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Web;
using System.Web.Mvc;

namespace PaymentGateWayApp.Controllers
{
    public class BaseController : Controller
    {
        public Dictionary<string, object> GetErrorsFromModelState()
        {
            var errors = new Dictionary<string, object>();

            return errors;
        }

        public void UpdateErrorLog(StreamReader r)
        {
            string line;
            while ((line = r.ReadLine()) != null)
            {
                Console.WriteLine(line);
            }
            r.Close();
        }
        public static void LogError(Exception ex)
        {
            try
            {
                //string filePath = @"D:\Plesk\VHOSTS\search.datagenie.co\logs\SG Logs";
                string filePath = @"D:\Plesk\VHOSTS\dev-search.datagenie.co\tmp\SG Logs";
                DateTime currentdate = DateTime.Today.Date;
                string filename = "ErrorLog " + currentdate.ToString("MM-dd-yyyy") + ".txt";

                string _path = Path.Combine(filePath, filename);

                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }
                DirectoryInfo dir = new DirectoryInfo(filePath);
                var file = dir.GetFiles("*", SearchOption.AllDirectories);
                if (file != null)
                {
                    using (StreamWriter sw = new StreamWriter(_path, true))
                    {
                        sw.WriteLine(DateTime.Now);
                        sw.WriteLine(ex.Message);
                        sw.WriteLine(ex.Source);
                    }
                }
                else
                {
                    string[] name = ex.Message.Split(',');
                    System.IO.File.WriteAllLines(_path, name);
                }
            }
            catch (Exception ex1)
            {
                //LogEr(ex1.Message);
            }
        }
        public static void LogEr(string input)
        {
            try
            {
                string filePath = @"D:\Projects\SG\SearchDatagenie\MVCWebApplication\Models";
                DateTime currentdate = DateTime.Today.Date;
                string filename = "ErrorLog" + currentdate.ToString("MM-dd-yyyy") + ".txt";

                string _path = Path.Combine(filePath, filename);

                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }
                DirectoryInfo dir = new DirectoryInfo(filePath);
                var file = dir.GetFiles("*", SearchOption.AllDirectories);
                if (file != null)
                {
                    using (StreamWriter sw = new StreamWriter(_path, true))
                    {
                        sw.WriteLine(input);
                        sw.WriteLine(Environment.NewLine);
                    }
                }
                else
                {
                    string[] name = input.Split(',');
                    System.IO.File.WriteAllLines(_path, name);
                }
            }
            catch (Exception ex1)
            {
                //LogError(ex1);
            }
        }

        /// <summary>
        /// Gets the client ipaddress.
        /// </summary>
        public string[] GetMacIDAndIPAddress()
        {
            string[] strArrayMacIDAndIP = new string[2];
            var macAddr =
              (
                  from nic in NetworkInterface.GetAllNetworkInterfaces()
                  where nic.OperationalStatus == OperationalStatus.Up
                  select nic.GetPhysicalAddress().ToString()
              ).FirstOrDefault();
            strArrayMacIDAndIP[0] = macAddr;

            //string strMacId = "";
            string strIpAddress = "";

            strIpAddress = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (strIpAddress == null)
                strIpAddress = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];

            strArrayMacIDAndIP[1] = strIpAddress;

            return strArrayMacIDAndIP;
        }

        public static void LogErrorWithMethodName(string methodName, Exception ex, string isError = "Y", string msg = "")
        {
            try
            {
                //string filePath = @"D:\Plesk\VHOSTS\search.datagenie.co\logs\SG Logs";
                string filePath = @"D:\Plesk\VHOSTS\dev-search.datagenie.co\tmp\SG Logs";
                DateTime currentdate = DateTime.Today.Date;
                string filename = "UnsaErrorLog_ " + currentdate.ToString("MM-dd-yyyy") + ".txt";
                string _path = Path.Combine(filePath, filename);
                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }
                DirectoryInfo dir = new DirectoryInfo(filePath);
                var file = dir.GetFiles("*", SearchOption.AllDirectories);
                if (file != null)
                {
                    using (StreamWriter sw = new StreamWriter(_path, true))
                    {
                        sw.WriteLine(DateTime.Now);
                        if (isError == "Y")
                        {
                            sw.WriteLine("[" + methodName + "]  " + "Error Message : " + ex.Message + ", Source : " + ex.Source + " " + ex.StackTrace);
                        }
                        else
                        {
                            sw.WriteLine("[" + methodName + "]  " + msg);
                        }
                    }
                }
                //else
                //{
                //    string[] name = ex.Message.Split(',');
                //    System.IO.File.WriteAllLines(_path, name);
                //}
            }
            catch (Exception)
            {
                //LogEr(ex1.Message);
            }
        }
    }
}