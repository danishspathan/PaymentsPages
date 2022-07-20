using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using BusinessEntities;
using AutomationData;

namespace BusinessAccessLayer
{
    public class Login
    {
        /// <summary>
        /// All the logic for login validate
        /// </summary>
        public LoginModel.UserLogin ValidateLoginCookie(HttpCookie cookie)
        {
            try
            {
                HttpCookie read_cookie = cookie;
                DataSet ds = new DataSet();
                LoginModel.UserLogin read_userlogin = new LoginModel.UserLogin();
                
                if (read_cookie != null)
                {

                    if (IsUserExits(read_cookie.Values["UserName"], read_cookie.Values["Password"]))
                    {
                        read_userlogin.UserName = read_cookie.Values["UserName"];
                        read_userlogin.Firstname = read_cookie.Values["Firstname"];
                        read_userlogin.Password = read_cookie.Values["Password"];
                        read_userlogin.Credit = Convert.ToInt32(read_cookie.Values["Credit"]);
                        read_userlogin.AccountType = Convert.ToInt32(read_cookie.Values["AccountType"]);

                    }
                }
                return read_userlogin;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// check if User Exits
        /// </summary>
        /// <param name="_UserName"></param>
        /// <param name="_Password"></param>
        /// <returns></returns>
        public bool IsUserExits(string _UserName, string _Password)
        {
            try
            {
                DataSet ds = new DataSet();
                LoginModel.UserLogin read_userlogin = new LoginModel.UserLogin();
                AutomationData.Login ojDal = new  AutomationData.Login();

                ds = ojDal.CheckUserExists(_UserName, _Password);  //checks entered credentials are correct or not
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// get User Exits Data Set
        /// </summary>
        /// <param name="_UserName"></param>
        /// <param name="_Password"></param>
        /// <returns></returns>
        public DataSet UserExitsDataSet(string _UserName, string _Password)
        {
            try
            {
                DataSet ds = new DataSet();
                LoginModel.UserLogin read_userlogin = new LoginModel.UserLogin();
                 AutomationData.Login ojDal = new  AutomationData.Login();

                ds = ojDal.CheckUserExists(_UserName, _Password);  //checks entered credentials are correct or not
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    return ds;
                }
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
