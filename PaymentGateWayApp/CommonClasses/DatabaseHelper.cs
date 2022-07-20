using PaymentGateWayApp.Controllers;
using PaymentGateWayApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using static PaymentGateWayApp.ViewModels.Model;

namespace PaymentGateWayApp.CommonClasses
{
    public static class DatabaseHelper
    {
        //public static SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ExcelDataPortalEntities"].ToString());
        //public static SqlConnection conlive = new SqlConnection(ConfigurationManager.ConnectionStrings["ImportexcelEntities"].ToString());

        // public static string strConn = ConfigurationManager.AppSettings["ExcelDataPortalEntities"];
        public static string connStr = ConfigurationManager.ConnectionStrings["ExcelDataPortalEntities"].ToString();
        public static string strSql = "";

        public static DataSet CheckUserExists(string _UserName, string _Password)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connStr))
                {
                    using (SqlCommand sqlCmd = new SqlCommand())
                    {
                        con.Open();
                        sqlCmd.Connection = con;
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.CommandText = "usp_CheckUserExists";

                        SqlParameter param1 = new SqlParameter("@UserName", SqlDbType.VarChar);
                        param1.Value = _UserName;

                        SqlParameter param2 = new SqlParameter("@Password", SqlDbType.VarChar);
                        param2.Value = _Password;

                        sqlCmd.Parameters.Add(param1);
                        sqlCmd.Parameters.Add(param2);
                        using (DataSet ds = new DataSet())
                        {
                            using (SqlDataAdapter da = new SqlDataAdapter(sqlCmd))
                            {
                                da.Fill(ds);
                                return ds;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }

        public static DataSet executeScalar(string strSql)
        {
            using (DataSet ds = new DataSet())
            {
                using (SqlConnection con = new SqlConnection(connStr))
                {
                    using (SqlCommand sqlCmd = new SqlCommand(strSql, con))
                    {
                        try
                        {
                            if (con.State == ConnectionState.Closed)
                            {
                                con.Open();
                            }
                            //dt = sqlCmd.ExecuteScalar();
                            using (SqlDataAdapter da = new SqlDataAdapter(strSql, con))
                            {
                                da.Fill(ds);
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                            throw ex;
                        }
                        //Commented By Unsa as it is not required Using blcok will automatically close the connection
                        //finally
                        //{
                        //    if (con.State == ConnectionState.Open)
                        //    {
                        //        con.Close();
                        //    }
                        //}
                        return ds;
                    }
                }
            }
        }

        public static DataSet executeScalarLive(string strSql)
        {
            using (DataSet ds = new DataSet())
            {
                using (SqlConnection con = new SqlConnection(connStr))
                {
                    using (SqlCommand sqlCmd = new SqlCommand(strSql, con))
                    {
                        try
                        {
                            if (con.State == ConnectionState.Closed)
                            {
                                con.Open();
                            }
                            //dt = sqlCmd.ExecuteScalar();
                            using (SqlDataAdapter da = new SqlDataAdapter(strSql, con))
                            {
                                da.Fill(ds);
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                            throw ex;
                        }
                        //Commented By Unsa as it is not required Using blcok will automatically close the connection
                        //finally
                        //{
                        //    if (con.State == ConnectionState.Open)
                        //    {
                        //        con.Close();
                        //    }
                        //}
                        return ds;
                    }
                }
            }
        }
        public static DataSet FuncExecuteNonquery(string strsql)
        {
            try
            {
                DataSet dsFunExe = new DataSet();
                using (SqlConnection con = new SqlConnection(connStr))
                {
                    using (SqlCommand cmd = new SqlCommand(strsql, con))
                    {
                        con.Open();
                        cmd.CommandType = CommandType.Text;

                        using (SqlDataAdapter adp1 = new SqlDataAdapter(cmd))
                        {
                            adp1.Fill(dsFunExe);
                            return dsFunExe;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
            //finally
            //{
            //    con.Close();
            //}
        }

        public static DataTable FuncExecuteReader(string strSql)
        {
            try
            {
                using (DataTable dt = new DataTable())
                {
                    using (SqlConnection con = new SqlConnection(connStr))
                    {
                        using (SqlCommand cmd = new SqlCommand(strSql, con))
                        {
                            con.Open();
                            dt.Load(cmd.ExecuteReader());
                            con.Close();
                            return dt;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }
        public static int FuncExecuteNonQuerywithIdentity(string strSql)
        {
            try
            {
                int id = 0;
                using (DataTable dt = new DataTable())
                {
                    using (SqlConnection con = new SqlConnection(connStr))
                    {
                        con.Open();
                        using (SqlCommand cmd = new SqlCommand(strSql, con))
                        {
                            id = (int)cmd.ExecuteNonQuery();

                            con.Close();

                            return id;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }
        public static int FunExecuteNonquery(string strsql)
        {
            try
            {
                using (DataSet ds = new DataSet())
                {
                    using (SqlConnection con = new SqlConnection(connStr))
                    {
                        using (SqlCommand cmd = new SqlCommand(strsql, con))
                        {
                            con.Open();
                            cmd.CommandType = CommandType.Text;
                            int noofrowseffected = cmd.ExecuteNonQuery();
                            return noofrowseffected;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
            //finally
            //{
            //    con.Close();
            //}
        }

        public static int FunExecuteNonqueryLive(string strsql)
        {
            try
            {
                using (DataSet ds = new DataSet())
                {
                    using (SqlConnection con = new SqlConnection(connStr))
                    {
                        using (SqlCommand cmd = new SqlCommand(strsql, con))
                        {
                            con.Open();
                            cmd.CommandType = CommandType.Text;

                            int noofrowseffected = cmd.ExecuteNonQuery();
                            return noofrowseffected;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
            //finally
            //{
            //    con.Close();
            //}
        }

        public static DataSet FunGetUsersData(string EmpCountRange, string Position, string Country,
            string State, string EmpCount, string Companycount, string Industry, string CompanyName, string Continent)
        {
            try
            {
                using (DataTable dt = new DataTable())
                {
                    //cmd = new SqlCommand("sp_GetUsersData", con);
                    //cmd = new SqlCommand("usp_GetUsersDetails", con);
                    //cmd = new SqlCommand("usp_GetFilteredUsersData", con);

                    //cmd = new SqlCommand("usp_FilterData", con);
                    using (SqlConnection con = new SqlConnection(connStr))
                    {
                        using (SqlCommand cmd = new SqlCommand("usp_FilterData1", con))
                        {
                            if (Position != null && Position != "")
                            {
                                cmd.Parameters.Add(new SqlParameter("@Position", Position));
                            }
                            else
                            {
                                cmd.Parameters.Add(new SqlParameter("@Position", DBNull.Value));
                            }

                            if (Country != null && Country != "" && Country != " Select country " && Country != " Select country" && Country != "\n                                    Select Country\n                                ")
                            {
                                cmd.Parameters.Add(new SqlParameter("@Country", Country));
                            }
                            else
                            {
                                cmd.Parameters.Add(new SqlParameter("@Country", DBNull.Value));
                            }

                            if (State != null && State != "Select state" && State != "")
                            {

                                cmd.Parameters.Add(new SqlParameter("@State", State));
                            }
                            else
                            {
                                cmd.Parameters.Add(new SqlParameter("@State", DBNull.Value));
                            }

                            if (EmpCount != null && EmpCount != "")
                            {
                                cmd.Parameters.Add(new SqlParameter("@EmployeesCount", EmpCount));
                            }
                            else
                            {
                                cmd.Parameters.Add(new SqlParameter("@EmployeesCount", DBNull.Value));
                            }
                            if (Industry != null && Industry != "")
                            {
                                cmd.Parameters.Add(new SqlParameter("@Industry", Industry));
                            }
                            else
                            {
                                cmd.Parameters.Add(new SqlParameter("@Industry", DBNull.Value));
                            }

                            if (EmpCountRange != "" && EmpCountRange != null && EmpCountRange != "0")
                            {
                                cmd.Parameters.Add(new SqlParameter("@EmpCountRange", EmpCountRange));
                            }
                            else
                            {
                                cmd.Parameters.Add(new SqlParameter("@EmpCountRange", DBNull.Value));
                            }

                            if (CompanyName != null && CompanyName != "")
                            {
                                cmd.Parameters.Add(new SqlParameter("@CompanyName", CompanyName));
                            }
                            else
                            {
                                cmd.Parameters.Add(new SqlParameter("@CompanyName", DBNull.Value));
                            }

                            if (Continent != null && Continent != "" && Continent != " Select continent ")
                            {
                                cmd.Parameters.Add(new SqlParameter("@Continent", Continent));
                            }
                            else
                            {
                                cmd.Parameters.Add(new SqlParameter("@Continent", DBNull.Value));
                            }

                            cmd.CommandType = CommandType.StoredProcedure;
                            if (con.State != ConnectionState.Closed)
                            {
                                con.Open();
                            }
                            using (SqlDataAdapter adp = new SqlDataAdapter(cmd))
                            {
                                using (DataSet ds = new DataSet())
                                {
                                    adp.Fill(ds);

                                    //if (con.State == ConnectionState.Open)
                                    //{
                                    //    con.Close();
                                    //}
                                    return ds;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }

        //commented By Unsa as its not required
        //public static void CloseConnection()
        //{
        //    if (con.State == ConnectionState.Open)
        //    {
        //        con.Close();
        //    }
        //}

        //commented By Unsa as its not required
        //public static void OpenConnection()
        //{
        //    if (con.State == ConnectionState.Closed)
        //    {
        //        con.Open();
        //    }
        //}

        public static bool IsLoggedIn(string username = "", string pwd = "")
        {
            try
            {
                if (username != null && username != "" && pwd != null && pwd != "")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }

        //public static CommonClasses.Enums.Accounttype UserAuthentication(string username = "", string pwd = "")
        //{
        //    bool result = false;
        //    if (username != "" && pwd != "")
        //    {
        //        //string strSql = "select IsAdmin,IsClient from tbl_Clientdetails where UserName= '" + username + "' and Password='" + pwd + "' ";                
        //        cmd = new SqlCommand("usp_IsUserAuthenticated", con);

        //        if(username!="" && username!=null)
        //            cmd.Parameters.Add(new SqlParameter("@UserName", username));
        //        else
        //            cmd.Parameters.Add(new SqlParameter("@UserName", DBNull.Value));

        //        if(pwd!="" && pwd!=null)
        //            cmd.Parameters.Add(new SqlParameter("@Password", pwd));
        //        else
        //            cmd.Parameters.Add(new SqlParameter("@Password", DBNull.Value));

        //        cmd.CommandType = CommandType.StoredProcedure;

        //        if (con.State == ConnectionState.Closed)
        //            con.Open();

        //        ds = new DataSet();
        //        adp = new SqlDataAdapter(cmd);
        //        adp.Fill(ds);

        //        if(con.State==ConnectionState.Open)
        //            con.Close();

        //        if (ds != null && ds.Tables.Count > 0)
        //        {
        //            if (ds.Tables[0].Rows.Count > 0)
        //            {
        //                if (Convert.ToBoolean(ds.Tables[0].Rows[0]["IsAdmin"].ToString()) == true)
        //                {
        //                    result = true;
        //                }

        //                else if (Convert.ToBoolean(ds.Tables[0].Rows[0]["IsAdmin"].ToString()) == true)
        //                {

        //                }

        //                else
        //                {
        //                    result = false;
        //                }

        //            }

        //        }
        //    }

        //    return Enums.Accounttype.Admin;
        //}

        public static string CapitalizeFirst(this string s)
        {
            try
            {
                bool IsNewSentense = true;
                var result = new StringBuilder(s.Length);
                for (int i = 0; i < s.Length; i++)
                {
                    if (IsNewSentense && char.IsLetter(s[i]))
                    {
                        result.Append(char.ToUpper(s[i]));
                        IsNewSentense = false;
                    }
                    else
                        result.Append(s[i]);

                    if (s[i] == '!' || s[i] == '?' || s[i] == '.')
                    {
                        IsNewSentense = true;
                    }
                }

                return result.ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }

        public static string SuperStrip(this string InputString)
        {
            if (string.IsNullOrWhiteSpace(InputString))
                return string.Empty;

            return InputString.Replace("_", " ");
        }

        public static bool sendEmailLogInfo(string strTo, string strFrom, string strSubject, string strBody, int i)
        {

            //******************************New Mail Format*****************************************************
            bool blndone = false;
            try
            {
                MailMessage mMailMessage = new MailMessage();
                System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
                msg.From = new MailAddress(strFrom);
                string[] strToEmails = strTo.Split(';');

                for (int j = 0; j < strToEmails.Length; j++)
                {
                    if (strToEmails[j] != null && strToEmails[j] != "")
                    {
                        msg.To.Add(new MailAddress(strToEmails[j].ToString().Trim()));
                    }
                }
                if (i == 1)
                {
                    msg.CC.Add(new MailAddress("info@esgglobalservices.com"));
                    msg.Bcc.Add(new MailAddress("Michaelr@esolutionsgroup.co.uk"));
                    msg.Bcc.Add(new MailAddress("imadk@esgglobalservices.com"));
                    msg.Bcc.Add(new MailAddress("gerryl@esgglobalservices.com")); // added on 21.05.2020 as per imad request
                }


                msg.Subject = strSubject;
                msg.Body = strBody;

                msg.IsBodyHtml = true;
                msg.Priority = MailPriority.Normal;

                System.Net.Mail.SmtpClient mSmtpClient = new System.Net.Mail.SmtpClient("213.171.222.69");//213.171.196.58
                mSmtpClient.UseDefaultCredentials = false;
                //System.Net.NetworkCredential loginInfo = new System.Net.NetworkCredential("LMS@esolutionsgroup.co.uk", "Z0ngo1&2");
                System.Net.NetworkCredential loginInfo = new System.Net.NetworkCredential("esgenie@esgglobalservices.com", "132t%tsH");
                mSmtpClient.Port = 587;
                mSmtpClient.Credentials = loginInfo;
                mSmtpClient.Send(msg);

                blndone = true;
            }
            catch (Exception ex)
            {
                blndone = false;
                throw ex;
            }
            return blndone;
            //************************************************************************************
        }


        public static bool sendEmailLogInfoEmailsEtc(string strTo, string strFrom, string strSubject, string strBody, int i)
        {

            //******************************New Mail Format*****************************************************
            bool blndone = false;
            try
            {
                MailMessage mMailMessage = new MailMessage();
                System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
                msg.From = new MailAddress(strFrom);
                string[] strToEmails = strTo.Split(';');

                for (int j = 0; j < strToEmails.Length; j++)
                {
                    if (strToEmails[j] != null && strToEmails[j] != "")
                    {
                        msg.To.Add(new MailAddress(strToEmails[j].ToString().Trim()));
                    }
                }
                if (i == 1)
                {
                    msg.CC.Add(new MailAddress("info@esgglobalservices.com"));
                    msg.Bcc.Add(new MailAddress("Michaelr@esolutionsgroup.co.uk"));
                    msg.Bcc.Add(new MailAddress("imadk@esgglobalservices.com"));
                    msg.Bcc.Add(new MailAddress("gerryl@esgglobalservices.com")); // added on 21.05.2020 as per imad request
                }
                msg.Subject = strSubject;
                msg.Body = strBody;
                msg.IsBodyHtml = true;
                msg.Priority = MailPriority.Normal;

                System.Net.Mail.SmtpClient mSmtpClient = new System.Net.Mail.SmtpClient("213.171.222.69");//213.171.196.58
                mSmtpClient.UseDefaultCredentials = false;
                //System.Net.NetworkCredential loginInfo = new System.Net.NetworkCredential("LMS@esolutionsgroup.co.uk", "Z0ngo1&2");
                System.Net.NetworkCredential loginInfo = new System.Net.NetworkCredential("esgenie@emailsetc.com", "P75me&3e");

                mSmtpClient.Credentials = loginInfo;
                mSmtpClient.Send(msg);

                blndone = true;
            }
            catch (Exception ex)
            {
                blndone = false;
                throw ex;
            }
            return blndone;
            //************************************************************************************
        }

        public static DataSet FuncExecuteNonqueryConLive(string strsql)
        {
            try
            {
                using (DataSet ds = new DataSet())
                {
                    using (SqlConnection con = new SqlConnection(connStr))
                    {
                        using (SqlCommand cmd = new SqlCommand(strsql, con))
                        {
                            con.Open();
                            cmd.CommandType = CommandType.Text;

                            using (SqlDataAdapter adp = new SqlDataAdapter(cmd))
                            {
                                adp.Fill(ds);
                                return ds;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }


        public static DataSet FuncExecuteNonqueryConLiveOld(string strsql)
        {
            try
            {
                using (DataSet ds = new DataSet())
                {
                    using (SqlConnection con = new SqlConnection(connStr))
                    {
                        con.Open();
                        using (SqlCommand cmd = new SqlCommand(strsql, con))
                        {
                            cmd.CommandType = CommandType.Text;

                            using (SqlDataAdapter adp = new SqlDataAdapter(cmd))
                            {
                                adp.Fill(ds);
                                return ds;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }

        public static DataSet FunGetFilterUsersData(string EmpCountRange, string Position, string Country,
         string State, string EmpCount, string Companycount, string Industry, string CompanyName, string Continent)
        {
            try
            {
                using (DataTable dt = new DataTable())
                {
                    //cmd = new SqlCommand("sp_GetUsersData", con);
                    //cmd = new SqlCommand("usp_GetUsersDetails", con);
                    //cmd = new SqlCommand("usp_GetFilteredUsersData", con);

                    //cmd = new SqlCommand("usp_FilterData", con);
                    using (SqlConnection con = new SqlConnection(connStr))
                    {
                        using (SqlCommand cmd = new SqlCommand("usp_FilterDataNew", con))
                        {
                            if (Position != null && Position != "")
                            {
                                cmd.Parameters.Add(new SqlParameter("@Position", Position));
                            }
                            else
                            {
                                cmd.Parameters.Add(new SqlParameter("@Position", DBNull.Value));
                            }

                            if (Country != null && Country != "" && Country != " Select country " && Country != " Select country" && Country != "\n                                    Select Country\n                                ")
                            {
                                cmd.Parameters.Add(new SqlParameter("@Country", Country));
                            }
                            else
                            {
                                cmd.Parameters.Add(new SqlParameter("@Country", DBNull.Value));
                            }

                            if (State != null && State != "Select state" && State != "")
                            {

                                cmd.Parameters.Add(new SqlParameter("@State", State));
                            }
                            else
                            {
                                cmd.Parameters.Add(new SqlParameter("@State", DBNull.Value));
                            }

                            if (EmpCount != null && EmpCount != "")
                            {
                                cmd.Parameters.Add(new SqlParameter("@EmployeesCount", EmpCount));
                            }
                            else
                            {
                                cmd.Parameters.Add(new SqlParameter("@EmployeesCount", DBNull.Value));
                            }
                            if (Industry != null && Industry != "")
                            {
                                cmd.Parameters.Add(new SqlParameter("@Industry", Industry));
                            }
                            else
                            {
                                cmd.Parameters.Add(new SqlParameter("@Industry", DBNull.Value));
                            }

                            if (EmpCountRange != "" && EmpCountRange != null && EmpCountRange != "0")
                            {
                                cmd.Parameters.Add(new SqlParameter("@EmpCountRange", EmpCountRange));
                            }
                            else
                            {
                                cmd.Parameters.Add(new SqlParameter("@EmpCountRange", DBNull.Value));
                            }

                            if (CompanyName != null && CompanyName != "")
                            {
                                cmd.Parameters.Add(new SqlParameter("@CompanyName", CompanyName));
                            }
                            else
                            {
                                cmd.Parameters.Add(new SqlParameter("@CompanyName", DBNull.Value));
                            }

                            if (Continent != null && Continent != "" && Continent != " Select continent ")
                            {
                                cmd.Parameters.Add(new SqlParameter("@Continent", Continent));
                            }
                            else
                            {
                                cmd.Parameters.Add(new SqlParameter("@Continent", DBNull.Value));
                            }

                            cmd.CommandType = CommandType.StoredProcedure;
                            if (con.State != ConnectionState.Closed)
                            {
                                con.Open();
                            }
                            using (SqlDataAdapter adp = new SqlDataAdapter(cmd))
                            {
                                using (DataSet ds = new DataSet())
                                {
                                    adp.Fill(ds);
                                    return ds;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }
        public static DataSet SaveFavouriteList(string ListName, string ClientID)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connStr))
                {
                    using (SqlCommand sqlCmd = new SqlCommand())
                    {
                        con.Open();
                        sqlCmd.Connection = con;
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.CommandText = "usp_AddFavouriteList";

                        SqlParameter param1 = new SqlParameter("@ListName", SqlDbType.VarChar);
                        param1.Value = ListName;

                        SqlParameter param2 = new SqlParameter("@ClientID", SqlDbType.Int);
                        param2.Value = ClientID;

                        sqlCmd.Parameters.Add(param1);
                        sqlCmd.Parameters.Add(param2);

                        using (DataSet ds = new DataSet())
                        {
                            using (SqlDataAdapter da = new SqlDataAdapter(sqlCmd))
                            {
                                da.Fill(ds);
                                return ds;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }
        public static DataSet GetFavouriteList(string ClientID)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connStr))
                {
                    using (SqlCommand sqlCmd = new SqlCommand())
                    {
                        con.Open();
                        sqlCmd.Connection = con;
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.CommandText = "usp_GetFavouriteList";

                        SqlParameter param1 = new SqlParameter("@ClientID", SqlDbType.VarChar);
                        param1.Value = ClientID;

                        sqlCmd.Parameters.Add(param1);

                        using (DataSet ds = new DataSet())
                        {
                            using (SqlDataAdapter da = new SqlDataAdapter(sqlCmd))
                            {
                                da.Fill(ds);
                                return ds;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }
        public static DataSet GetMySearches(string ClientID)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connStr))
                {
                    using (SqlCommand sqlCmd = new SqlCommand())
                    {
                        con.Open();
                        sqlCmd.Connection = con;
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.CommandText = "PRC_MYSEARCHES";

                        SqlParameter param1 = new SqlParameter("@ClientID", SqlDbType.VarChar);
                        param1.Value = ClientID;
                        SqlParameter param2 = new SqlParameter("@Mode", SqlDbType.VarChar);
                        param2.Value = 1;
                        sqlCmd.Parameters.Add(param1);
                        sqlCmd.Parameters.Add(param2);
                        using (DataSet dsmySearch = new DataSet())
                        {
                            using (SqlDataAdapter da = new SqlDataAdapter(sqlCmd))
                            {
                                da.Fill(dsmySearch);
                                return dsmySearch;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }
        public static DataSet GetSearchInfo(string HeaderId)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connStr))
                {
                    using (SqlCommand sqlCmd = new SqlCommand())
                    {
                        con.Open();
                        sqlCmd.Connection = con;
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.CommandText = "PRC_MYSEARCHES";

                        SqlParameter param1 = new SqlParameter("@HeaderId", SqlDbType.VarChar);
                        param1.Value = HeaderId;
                        SqlParameter param2 = new SqlParameter("@Mode", SqlDbType.VarChar);
                        param2.Value = 2;
                        sqlCmd.Parameters.Add(param1);
                        sqlCmd.Parameters.Add(param2);
                        using (DataSet ds = new DataSet())
                        {
                            using (SqlDataAdapter da = new SqlDataAdapter(sqlCmd))
                            {
                                da.Fill(ds);
                                return ds;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }
        public static DataSet UpdateFavouriteList(string ListName, string ClientID, int ID)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connStr))
                {
                    using (SqlCommand sqlCmd = new SqlCommand())
                    {
                        con.Open();
                        sqlCmd.Connection = con;
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.CommandText = "usp_UpdateFavouriteList";

                        SqlParameter param1 = new SqlParameter("@ListName", SqlDbType.VarChar);
                        param1.Value = ListName;

                        SqlParameter param2 = new SqlParameter("@ClientID", SqlDbType.Int);
                        param2.Value = ClientID;

                        SqlParameter param3 = new SqlParameter("@ID", SqlDbType.Int);
                        param3.Value = ID;

                        sqlCmd.Parameters.Add(param1);
                        sqlCmd.Parameters.Add(param2);
                        sqlCmd.Parameters.Add(param3);

                        using (DataSet ds = new DataSet())
                        {
                            using (SqlDataAdapter da = new SqlDataAdapter(sqlCmd))
                            {
                                da.Fill(ds);
                                return ds;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }
        public static DataSet SaveContactList(int ListID, string Contacts)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connStr))
                {
                    using (SqlCommand sqlCmd = new SqlCommand())
                    {
                        con.Open();
                        sqlCmd.Connection = con;
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.CommandText = "usp_AddContactList";

                        SqlParameter param1 = new SqlParameter("@ListID", SqlDbType.VarChar);
                        param1.Value = ListID;

                        SqlParameter param2 = new SqlParameter("@Contacts", SqlDbType.VarChar);
                        param2.Value = Contacts;

                        sqlCmd.Parameters.Add(param1);
                        sqlCmd.Parameters.Add(param2);

                        using (DataSet ds = new DataSet())
                        {
                            using (SqlDataAdapter da = new SqlDataAdapter(sqlCmd))
                            {
                                da.Fill(ds);
                                return ds;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }
        public static DataSet SaveContactListExtension(int ListID, string Contacts)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connStr))
                {
                    using (SqlCommand sqlCmd = new SqlCommand())
                    {
                        con.Open();
                        sqlCmd.Connection = con;
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        //sqlCmd.CommandText = "usp_ExtentionAddContactList";
                        sqlCmd.CommandText = "usp_ExtentionAddContactList_Test";

                        SqlParameter param1 = new SqlParameter("@ListID", SqlDbType.VarChar);
                        param1.Value = ListID;

                        SqlParameter param2 = new SqlParameter("@Contacts", SqlDbType.VarChar);
                        param2.Value = Contacts;

                        sqlCmd.Parameters.Add(param1);
                        sqlCmd.Parameters.Add(param2);

                        using (DataSet ds = new DataSet())
                        {
                            using (SqlDataAdapter da = new SqlDataAdapter(sqlCmd))
                            {
                                da.Fill(ds);
                                return ds;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }
        public static DataSet ContactAgainstList(int ListID)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connStr))
                {
                    using (SqlCommand sqlCmd = new SqlCommand())
                    {
                        con.Open();
                        sqlCmd.Connection = con;
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.CommandText = "usp__GetContactAgainstList";

                        SqlParameter param1 = new SqlParameter("@ListID", SqlDbType.VarChar);
                        param1.Value = ListID;
                        sqlCmd.Parameters.Add(param1);

                        using (DataSet ds = new DataSet())
                        {
                            using (SqlDataAdapter da = new SqlDataAdapter(sqlCmd))
                            {
                                da.Fill(ds);
                                return ds;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }
        public static DataSet SaveRegistration(Models.UserDetails.UserLogin Register)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connStr))
                {
                    using (SqlCommand sqlCmd = new SqlCommand())
                    {
                        con.Open();
                        sqlCmd.Connection = con;
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.CommandText = "USP_RegisterNewUser";

                        SqlParameter param1 = new SqlParameter("@FirstName", SqlDbType.VarChar);
                        param1.Value = Register.Firstname;

                        SqlParameter param2 = new SqlParameter("@LastName", SqlDbType.VarChar);
                        param2.Value = Register.LastName;

                        SqlParameter param3 = new SqlParameter("@UserName", SqlDbType.VarChar);
                        param3.Value = Register.UserName;

                        SqlParameter param4 = new SqlParameter("@Email", SqlDbType.VarChar);
                        param4.Value = Register.Email;

                        SqlParameter param5 = new SqlParameter("@Password", SqlDbType.VarChar);
                        param5.Value = Register.Password;


                        //if (sqlCmd.Connection.State != ConnectionState.Open)
                        //{
                        //    sqlCmd.Connection.Open();
                        //}

                        sqlCmd.Parameters.Add(param1);
                        sqlCmd.Parameters.Add(param2);
                        sqlCmd.Parameters.Add(param3);
                        sqlCmd.Parameters.Add(param4);
                        sqlCmd.Parameters.Add(param5);
                        using (DataSet ds = new DataSet())
                        {
                            using (SqlDataAdapter da = new SqlDataAdapter(sqlCmd))
                            {
                                da.Fill(ds);
                                return ds;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }
        // free trial register free trial
        public static DataSet SaveRegistrationFreeTrial(Models.UserDetails.UserLogin Register)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connStr))
                {
                    using (SqlCommand sqlCmd = new SqlCommand())
                    {
                        con.Open();
                        sqlCmd.Connection = con;
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.CommandText = "USP_RegisterNewUserFreeTrial";

                        SqlParameter param1 = new SqlParameter("@FirstName", SqlDbType.VarChar);
                        param1.Value = Register.Firstname;

                        SqlParameter param2 = new SqlParameter("@LastName", SqlDbType.VarChar);
                        param2.Value = Register.LastName;

                        SqlParameter param3 = new SqlParameter("@UserName", SqlDbType.VarChar);
                        param3.Value = Register.UserName;

                        SqlParameter param4 = new SqlParameter("@Email", SqlDbType.VarChar);
                        param4.Value = Register.Email;

                        SqlParameter param5 = new SqlParameter("@Password", SqlDbType.VarChar);
                        param5.Value = Register.Password;


                        sqlCmd.Parameters.Add(param1);
                        sqlCmd.Parameters.Add(param2);
                        sqlCmd.Parameters.Add(param3);
                        sqlCmd.Parameters.Add(param4);
                        sqlCmd.Parameters.Add(param5);
                        using (DataSet ds = new DataSet())
                        {
                            using (SqlDataAdapter da = new SqlDataAdapter(sqlCmd))
                            {
                                da.Fill(ds);
                                return ds;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }
        //

        //
        public static DataSet UpdateCreditsOnUnlock(int Flag, int ClientID)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connStr))
                {
                    using (SqlCommand sqlCmd = new SqlCommand())
                    {
                        con.Open();
                        sqlCmd.Connection = con;
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        if (Flag == 0)
                        {
                            sqlCmd.CommandText = "usp_UpdateCreditsOnUnlock";
                            SqlParameter param1 = new SqlParameter("@ClientID", SqlDbType.VarChar);
                            param1.Value = ClientID;
                            sqlCmd.Parameters.Add(param1);
                        }
                        using (DataSet ds = new DataSet())
                        {
                            using (SqlDataAdapter da = new SqlDataAdapter(sqlCmd))
                            {
                                da.Fill(ds);
                                return ds;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //SaveResetPassword(int UserID,string Password)

        public static DataSet SaveResetPassword(int UserID, string Password)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connStr))
                {
                    using (SqlCommand sqlCmd = new SqlCommand())
                    {
                        con.Open();
                        sqlCmd.Connection = con;
                        sqlCmd.CommandType = CommandType.StoredProcedure;


                        sqlCmd.CommandText = "usp_UpdateUserPassword";
                        SqlParameter param1 = new SqlParameter("@UserID", SqlDbType.VarChar);
                        param1.Value = UserID;
                        sqlCmd.Parameters.Add(param1);

                        SqlParameter param2 = new SqlParameter("@Password", SqlDbType.VarChar);
                        param2.Value = Password;
                        sqlCmd.Parameters.Add(param2);

                        using (DataSet ds = new DataSet())
                        {
                            using (SqlDataAdapter da = new SqlDataAdapter(sqlCmd))
                            {
                                da.Fill(ds);
                                return ds;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }

        public static DataSet FilterContactData(string EmpCountRange, string Position, string Region, string Country, string State_, string EmployeeCount, string Companycount,
            string Industry, string CompanyName, string Continent, string start, string length, string company_url, string Subcategory, string company_revenue, string columnDatabase,
            string sortDirection, string clientId)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connStr))
                {
                    using (SqlCommand sqlCmd = new SqlCommand())
                    {
                        con.Open();
                        sqlCmd.Connection = con;
                        sqlCmd.CommandType = CommandType.StoredProcedure;


                        sqlCmd.CommandText = "USP_GetContactFilterData_AddIndustryId_new";
                        SqlParameter param1 = new SqlParameter("@Industry", SqlDbType.VarChar);
                        param1.Value = Industry;
                        sqlCmd.Parameters.Add(param1);

                        SqlParameter param2 = new SqlParameter("@Subcategory", SqlDbType.VarChar);
                        param2.Value = Subcategory;
                        sqlCmd.Parameters.Add(param2);

                        SqlParameter param3 = new SqlParameter("@CompanyName", SqlDbType.VarChar);
                        param3.Value = CompanyName;
                        sqlCmd.Parameters.Add(param3);

                        SqlParameter param4 = new SqlParameter("@Position", SqlDbType.VarChar);
                        param4.Value = Position;
                        sqlCmd.Parameters.Add(param4);

                        SqlParameter param5 = new SqlParameter("@company_url", SqlDbType.VarChar);
                        param5.Value = company_url;
                        sqlCmd.Parameters.Add(param5);

                        SqlParameter param6 = new SqlParameter("@company_revenue", SqlDbType.VarChar);
                        param6.Value = company_revenue;
                        sqlCmd.Parameters.Add(param6);

                        SqlParameter param7 = new SqlParameter("@Country", SqlDbType.VarChar);
                        param7.Value = Country;
                        sqlCmd.Parameters.Add(param7);

                        SqlParameter param8 = new SqlParameter("@State_", SqlDbType.VarChar);
                        param8.Value = State_;
                        sqlCmd.Parameters.Add(param8);

                        SqlParameter param9 = new SqlParameter("@columnDatabase", SqlDbType.VarChar);
                        param9.Value = columnDatabase;
                        sqlCmd.Parameters.Add(param9);

                        SqlParameter param10 = new SqlParameter("@sortDirection", SqlDbType.VarChar);
                        param10.Value = sortDirection;
                        sqlCmd.Parameters.Add(param10);

                        SqlParameter param11 = new SqlParameter("@start", SqlDbType.VarChar);
                        param11.Value = start;
                        sqlCmd.Parameters.Add(param11);

                        SqlParameter param12 = new SqlParameter("@length", SqlDbType.VarChar);
                        param12.Value = length;
                        sqlCmd.Parameters.Add(param12);

                        //Unsa - removed round trip
                        SqlParameter param13 = new SqlParameter("@clientId", SqlDbType.VarChar);
                        param13.Value = clientId;
                        sqlCmd.Parameters.Add(param13);

                        sqlCmd.CommandTimeout = 180;
                        BaseController.LogErrorWithMethodName("[DatabaseHelper]- FilterContactData", null, "N", "Industry :" + Industry + ", Subcategory : " + Subcategory + ", CompanyName : " + CompanyName + ", Position :" + Position + ", company_url : " + company_url + ", company_revenue : " + company_revenue + ", Country : " + Country + ", State_ : " + State_ + ",  columnDatabase : " + columnDatabase + ", sortDirection : " + sortDirection + ", start : " + start + ", length : " + length + " clientId : " + clientId);
                        DataSet dsFilterContactData = new DataSet();

                        //try
                        //{
                        using (SqlDataAdapter da = new SqlDataAdapter(sqlCmd))
                        {
                            da.Fill(dsFilterContactData);
                            return dsFilterContactData;
                        }
                        // }
                        //catch(Exception ex)
                        //{
                        //    BaseController.LogErrorWithMethodName("[DatabaseHelper]- FilterContactData", null, "N", "DATAADAPTER ---"+ex.Message + ", Stacktrace : " + ex.StackTrace);
                        //}
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                BaseController.LogErrorWithMethodName("[DatabaseHelper]- FilterContactData", null, "N", ex.Message + ", Stacktrace : " + ex.StackTrace);
                throw ex;
            }
        }

        public static DataSet FilterContactDataCount(string EmpCountRange, string Position, string Region, string Country, string State_, string EmployeeCount, string Companycount,
          string Industry, string CompanyName, string Continent, string company_url, string Subcategory, string company_revenue)
        {
            try
            {

                using (SqlConnection con = new SqlConnection(connStr))
                {
                    using (SqlCommand sqlCmd = new SqlCommand())
                    {
                        con.Open();
                        sqlCmd.Connection = con;
                        sqlCmd.CommandType = CommandType.StoredProcedure;


                        sqlCmd.CommandText = "USP_GetContactFilterData_AddIndustryIdCount_new";
                        SqlParameter param1 = new SqlParameter("@Industry", SqlDbType.VarChar);
                        param1.Value = Industry;
                        sqlCmd.Parameters.Add(param1);

                        SqlParameter param2 = new SqlParameter("@Subcategory", SqlDbType.VarChar);
                        param2.Value = Subcategory;
                        sqlCmd.Parameters.Add(param2);

                        SqlParameter param3 = new SqlParameter("@CompanyName", SqlDbType.VarChar);
                        param3.Value = CompanyName;
                        sqlCmd.Parameters.Add(param3);

                        SqlParameter param4 = new SqlParameter("@Position", SqlDbType.VarChar);
                        param4.Value = Position;
                        sqlCmd.Parameters.Add(param4);

                        SqlParameter param5 = new SqlParameter("@company_url", SqlDbType.VarChar);
                        param5.Value = company_url;
                        sqlCmd.Parameters.Add(param5);

                        SqlParameter param6 = new SqlParameter("@company_revenue", SqlDbType.VarChar);
                        param6.Value = company_revenue;
                        sqlCmd.Parameters.Add(param6);

                        SqlParameter param7 = new SqlParameter("@Country", SqlDbType.VarChar);
                        param7.Value = Country;
                        sqlCmd.Parameters.Add(param7);

                        SqlParameter param8 = new SqlParameter("@State_", SqlDbType.VarChar);
                        param8.Value = State_;
                        sqlCmd.Parameters.Add(param8);

                        using (DataSet ds = new DataSet())
                        {
                            using (SqlDataAdapter da = new SqlDataAdapter(sqlCmd))
                            {
                                da.Fill(ds);
                                return ds;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }





        //public static DataSet FilterContactData(string EmpCountRange, string Position, string Region, string Country, string State_, string EmployeeCount, string Companycount,
        //    string Industry, string CompanyName, string Continent, string start, string length, string company_url, string Subcategory, string company_revenue, string columnDatabase,
        //    string sortDirection)
        //{
        //    DataSet AttachmentDT = new DataSet();
        //    AttachmentDT = sub_GetDatatable("USP_GetContactFilterData '" + Industry + "','" + Subcategory + "','" + CompanyName + "','" + Position + "','" + company_url + "','" + company_revenue + "','" + Country + "','" + State_ + "','" + columnDatabase + "','" + sortDirection + "','" + start + "','" + length + "'");
        //    return AttachmentDT;
        //}


        //public static DataSet sub_GetDatatable(string strSQL)
        //{
        //    DataSet dt = new DataSet();


        //    try
        //    {
        //        SqlCommand sqlCmd = new SqlCommand();
        //        sqlCmd.Connection = conlive;
        //        SqlCommand objCommand = new SqlCommand(strSQL, conlive);
        //        SqlDataAdapter da = new SqlDataAdapter();
        //        da.SelectCommand = objCommand;
        //        objCommand.CommandTimeout = 600;
        //        da.Fill(dt);


        //    }

        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return dt;
        //}






        public static DataSet FilterDataCompany(string EmpCountRange, string Region, string Country, string State_, string EmployeeCount, string Companycount, string Industry,
                           string CompanyName, string Continent, string start, string length, string company_url, string Subcategory,
                           string company_revenue, string columnDatabase, string sortDirection)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connStr))
                {
                    using (SqlCommand sqlCmd = new SqlCommand())
                    {
                        con.Open();
                        sqlCmd.Connection = con;
                        sqlCmd.CommandType = CommandType.StoredProcedure;


                        sqlCmd.CommandText = "[USP_GetCompanyFilterData]";
                        SqlParameter param1 = new SqlParameter("@Industry", SqlDbType.VarChar);
                        param1.Value = Industry;
                        sqlCmd.Parameters.Add(param1);

                        SqlParameter param2 = new SqlParameter("@Subcategory", SqlDbType.VarChar);
                        param2.Value = Subcategory;
                        sqlCmd.Parameters.Add(param2);

                        SqlParameter param3 = new SqlParameter("@CompanyName", SqlDbType.VarChar);
                        param3.Value = CompanyName;
                        sqlCmd.Parameters.Add(param3);

                        SqlParameter param5 = new SqlParameter("@company_url", SqlDbType.VarChar);
                        param5.Value = company_url;
                        sqlCmd.Parameters.Add(param5);

                        SqlParameter param6 = new SqlParameter("@company_revenue", SqlDbType.VarChar);
                        param6.Value = company_revenue;
                        sqlCmd.Parameters.Add(param6);

                        SqlParameter param7 = new SqlParameter("@Country", SqlDbType.VarChar);
                        param7.Value = Country;
                        sqlCmd.Parameters.Add(param7);

                        SqlParameter param8 = new SqlParameter("@State_", SqlDbType.VarChar);
                        param8.Value = State_;
                        sqlCmd.Parameters.Add(param8);

                        SqlParameter param9 = new SqlParameter("@columnDatabase", SqlDbType.VarChar);
                        param9.Value = columnDatabase;
                        sqlCmd.Parameters.Add(param9);

                        SqlParameter param10 = new SqlParameter("@sortDirection", SqlDbType.VarChar);
                        param10.Value = sortDirection;
                        sqlCmd.Parameters.Add(param10);

                        SqlParameter param11 = new SqlParameter("@start", SqlDbType.VarChar);
                        param11.Value = start;
                        sqlCmd.Parameters.Add(param11);

                        SqlParameter param12 = new SqlParameter("@length", SqlDbType.VarChar);
                        param12.Value = length;
                        sqlCmd.Parameters.Add(param12);

                        using (DataSet ds = new DataSet())
                        {
                            using (SqlDataAdapter da = new SqlDataAdapter(sqlCmd))
                            {
                                da.Fill(ds);
                                return ds;
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }


        //GetUserWiseCacheAll(int clientID)
        public static DataSet GetUserWiseCacheAll(string clientID)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connStr))
                {
                    using (SqlCommand sqlCmd = new SqlCommand())
                    {
                        con.Open();
                        sqlCmd.Connection = con;
                        sqlCmd.CommandType = CommandType.StoredProcedure;

                        sqlCmd.CommandText = "usp_GetUserWiseCacheAll";
                        SqlParameter param1 = new SqlParameter("@clientID", SqlDbType.VarChar);
                        param1.Value = clientID;
                        sqlCmd.Parameters.Add(param1);
                        using (DataSet ds = new DataSet())
                        {
                            using (SqlDataAdapter da = new SqlDataAdapter(sqlCmd))
                            {
                                da.Fill(ds);
                                return ds;
                            }

                        }

                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }




        //GetUserWiseCacheAll(int clientID)
        public static DataSet GetAllSideMenuAndSubMenu()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connStr))
                {
                    using (SqlCommand sqlCmd = new SqlCommand())
                    {
                        con.Open();
                        sqlCmd.Connection = con;
                        sqlCmd.CommandType = CommandType.StoredProcedure;

                        sqlCmd.CommandText = "USP_GetAllSideMenuAndSubMenu";

                        using (DataSet ds = new DataSet())
                        {
                            using (SqlDataAdapter da = new SqlDataAdapter(sqlCmd))
                            {
                                da.Fill(ds);
                                return ds;
                            }

                        }

                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }





        public static DataSet GetSubCategoryDetails(int CategoryID)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connStr))
                {
                    using (SqlCommand sqlCmd = new SqlCommand())
                    {
                        con.Open();
                        sqlCmd.Connection = con;
                        sqlCmd.CommandType = CommandType.StoredProcedure;

                        sqlCmd.CommandText = "USP_GetSubCategoryDetails";
                        SqlParameter param1 = new SqlParameter("@CategoryID", SqlDbType.VarChar);
                        param1.Value = CategoryID;
                        sqlCmd.Parameters.Add(param1);
                        using (DataSet ds = new DataSet())
                        {
                            using (SqlDataAdapter da = new SqlDataAdapter(sqlCmd))
                            {
                                da.Fill(ds);
                                return ds;
                            }

                        }

                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        //



        public static DataSet UpdateCacheAllClientOption(string RadioStatus, string CLIENT_ID)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connStr))
                {
                    using (SqlCommand sqlCmd = new SqlCommand())
                    {
                        con.Open();
                        sqlCmd.Connection = con;
                        sqlCmd.CommandType = CommandType.StoredProcedure;

                        sqlCmd.CommandText = "USP_UpdateCacheAllClientOption";
                        SqlParameter param1 = new SqlParameter("@RadioStatus", SqlDbType.Int);
                        param1.Value = RadioStatus;
                        sqlCmd.Parameters.Add(param1);

                        SqlParameter param2 = new SqlParameter("@CLIENT_ID", SqlDbType.Int);
                        param2.Value = CLIENT_ID;
                        sqlCmd.Parameters.Add(param2);

                        using (DataSet ds = new DataSet())
                        {
                            using (SqlDataAdapter da = new SqlDataAdapter(sqlCmd))
                            {
                                da.Fill(ds);
                                return ds;
                            }

                        }

                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }




        public static DataSet GetAllCompanyName(string CompanyName)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connStr))
                {
                    using (SqlCommand sqlCmd = new SqlCommand())
                    {
                        con.Open();
                        sqlCmd.Connection = con;
                        sqlCmd.CommandType = CommandType.StoredProcedure;

                        sqlCmd.CommandText = "USP_GetAllCompanyName";
                        SqlParameter param1 = new SqlParameter("@CompanyName", SqlDbType.VarChar);
                        param1.Value = CompanyName;
                        sqlCmd.Parameters.Add(param1);
                        using (DataSet ds = new DataSet())
                        {
                            using (SqlDataAdapter da = new SqlDataAdapter(sqlCmd))
                            {
                                da.Fill(ds);
                                return ds;
                            }

                        }

                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //GetLastFailedMessageString(CLIENT_ID, CONTACT_ID)

        public static DataSet GetLastFailedMessageString(string CLIENT_ID, string CONTACT_ID)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connStr))
                {
                    using (SqlCommand sqlCmd = new SqlCommand())
                    {
                        con.Open();
                        sqlCmd.Connection = con;
                        sqlCmd.CommandType = CommandType.StoredProcedure;

                        sqlCmd.CommandText = "USP_GetLastFailedMessageString";
                        SqlParameter param1 = new SqlParameter("@CLIENT_ID", SqlDbType.Int);
                        param1.Value = CLIENT_ID;
                        sqlCmd.Parameters.Add(param1);

                        SqlParameter param2 = new SqlParameter("@CONTACT_ID", SqlDbType.Int);
                        param2.Value = CONTACT_ID;
                        sqlCmd.Parameters.Add(param2);

                        using (DataSet ds = new DataSet())
                        {
                            using (SqlDataAdapter da = new SqlDataAdapter(sqlCmd))
                            {
                                da.Fill(ds);
                                return ds;
                            }

                        }

                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        //GetCompanyDetails(RecordID)
        public static DataSet GetCompanyDetails(int RecordID)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connStr))
                {
                    using (SqlCommand sqlCmd = new SqlCommand())
                    {
                        con.Open();
                        sqlCmd.Connection = con;
                        sqlCmd.CommandType = CommandType.StoredProcedure;

                        sqlCmd.CommandText = "USP_GetCompanyDetails";
                        SqlParameter param1 = new SqlParameter("@RecordID", SqlDbType.VarChar);
                        param1.Value = RecordID;
                        sqlCmd.Parameters.Add(param1);
                        using (DataSet ds = new DataSet())
                        {
                            using (SqlDataAdapter da = new SqlDataAdapter(sqlCmd))
                            {
                                da.Fill(ds);
                                return ds;
                            }

                        }

                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public static DataSet GetContactDetails(int RecordID)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connStr))
                {
                    using (SqlCommand sqlCmd = new SqlCommand())
                    {
                        con.Open();
                        sqlCmd.Connection = con;
                        sqlCmd.CommandType = CommandType.StoredProcedure;

                        sqlCmd.CommandText = "USP_GetContactDetails";
                        SqlParameter param1 = new SqlParameter("@RecordID", SqlDbType.VarChar);
                        param1.Value = RecordID;
                        sqlCmd.Parameters.Add(param1);
                        using (DataSet ds = new DataSet())
                        {
                            using (SqlDataAdapter da = new SqlDataAdapter(sqlCmd))
                            {
                                da.Fill(ds);
                                return ds;
                            }

                        }

                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public static DataSet ValidationDeailsFetching(string CLIENT_ID, string CONTACT_ID)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connStr))
                {
                    using (SqlCommand sqlCmd = new SqlCommand())
                    {
                        con.Open();
                        sqlCmd.Connection = con;
                        sqlCmd.CommandType = CommandType.StoredProcedure;

                        sqlCmd.CommandText = "USP_ValidationDeailsFetching";
                        SqlParameter param1 = new SqlParameter("@CLIENT_ID", SqlDbType.Int);
                        param1.Value = CLIENT_ID;
                        sqlCmd.Parameters.Add(param1);

                        SqlParameter param2 = new SqlParameter("@CONTACT_ID", SqlDbType.Int);
                        param2.Value = CONTACT_ID;
                        sqlCmd.Parameters.Add(param2);

                        using (DataSet ds = new DataSet())
                        {
                            using (SqlDataAdapter da = new SqlDataAdapter(sqlCmd))
                            {
                                da.Fill(ds);
                                return ds;
                            }

                        }

                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //ValidationComponentDetailsFetching(CLIENT_ID, CONTACT_ID)



        public static DataSet ValidationComponentDetailsFetching(string CLIENT_ID, string CONTACT_ID)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connStr))
                {
                    using (SqlCommand sqlCmd = new SqlCommand())
                    {
                        con.Open();
                        sqlCmd.Connection = con;
                        sqlCmd.CommandType = CommandType.StoredProcedure;

                        sqlCmd.CommandText = "USP_ValidationComponentDetailsFetching";
                        SqlParameter param1 = new SqlParameter("@CLIENT_ID", SqlDbType.Int);
                        param1.Value = CLIENT_ID;
                        sqlCmd.Parameters.Add(param1);

                        SqlParameter param2 = new SqlParameter("@CONTACT_ID", SqlDbType.Int);
                        param2.Value = CONTACT_ID;
                        sqlCmd.Parameters.Add(param2);

                        using (DataSet ds = new DataSet())
                        {
                            using (SqlDataAdapter da = new SqlDataAdapter(sqlCmd))
                            {
                                da.Fill(ds);
                                return ds;
                            }

                        }

                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        //GetStateData(CountryName, regiontype)
        public static DataSet GetStateData(string CountryName, string regiontype)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connStr))
                {
                    using (SqlCommand sqlCmd = new SqlCommand())
                    {
                        con.Open();
                        sqlCmd.Connection = con;
                        sqlCmd.CommandType = CommandType.StoredProcedure;

                        sqlCmd.CommandText = "USP_GetStateData";
                        SqlParameter param1 = new SqlParameter("@CountryName", SqlDbType.Int);
                        param1.Value = CountryName;
                        sqlCmd.Parameters.Add(param1);

                        SqlParameter param2 = new SqlParameter("@regiontype", SqlDbType.VarChar);
                        param2.Value = regiontype;
                        sqlCmd.Parameters.Add(param2);

                        using (DataSet ds = new DataSet())
                        {
                            using (SqlDataAdapter da = new SqlDataAdapter(sqlCmd))
                            {
                                da.Fill(ds);
                                return ds;
                            }

                        }

                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }




        public static DataSet fetchSMTPdetails(string ID)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connStr))
                {
                    using (SqlCommand sqlCmd = new SqlCommand())
                    {
                        con.Open();
                        sqlCmd.Connection = con;
                        sqlCmd.CommandType = CommandType.StoredProcedure;

                        sqlCmd.CommandText = "USP_fetchSMTPdetails";
                        SqlParameter param1 = new SqlParameter("@ID", SqlDbType.Int);
                        param1.Value = ID;
                        sqlCmd.Parameters.Add(param1);



                        using (DataSet ds = new DataSet())
                        {
                            using (SqlDataAdapter da = new SqlDataAdapter(sqlCmd))
                            {
                                da.Fill(ds);
                                return ds;
                            }

                        }

                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }




        public static DataSet FetchSendgridData(string EmailId, string ClientID)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connStr))
                {
                    using (SqlCommand sqlCmd = new SqlCommand())
                    {
                        con.Open();
                        sqlCmd.Connection = con;
                        sqlCmd.CommandType = CommandType.StoredProcedure;

                        sqlCmd.CommandText = "USP_FetchSendgridData";
                        SqlParameter param1 = new SqlParameter("@EmailId", SqlDbType.VarChar);
                        param1.Value = EmailId;
                        sqlCmd.Parameters.Add(param1);

                        SqlParameter param2 = new SqlParameter("@ClientID", SqlDbType.VarChar);
                        param2.Value = ClientID;
                        sqlCmd.Parameters.Add(param2);



                        using (DataSet ds = new DataSet())
                        {
                            using (SqlDataAdapter da = new SqlDataAdapter(sqlCmd))
                            {
                                da.Fill(ds);
                                return ds;
                            }

                        }

                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        //

        public static DataSet FetchUserWiseDetails(string ClientID)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connStr))
                {
                    using (SqlCommand sqlCmd = new SqlCommand())
                    {
                        con.Open();
                        sqlCmd.Connection = con;

                        sqlCmd.CommandType = CommandType.StoredProcedure;

                        sqlCmd.CommandText = "USP_FetchUserWiseDetails";

                        SqlParameter param1 = new SqlParameter("@ClientID", SqlDbType.Int);
                        param1.Value = ClientID;
                        sqlCmd.Parameters.Add(param1);



                        using (DataSet ds = new DataSet())
                        {
                            using (SqlDataAdapter da = new SqlDataAdapter(sqlCmd))
                            {
                                da.Fill(ds);
                                return ds;
                            }

                        }

                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }





        //string str = "select UnlockedContactsString from tbl_UnlockedContacts where ClientID='"+ ClientID + "'";
        //ds = DatabaseHelper.executeScalarLive(str);

        //ds = DatabaseHelper.Unlocked(ClientID);

        public static DataSet GetUnlockedContactsString(string ClientID)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connStr))
                {
                    using (SqlCommand sqlCmd = new SqlCommand())
                    {
                        con.Open();
                        sqlCmd.Connection = con;

                        sqlCmd.CommandType = CommandType.StoredProcedure;

                        sqlCmd.CommandText = "USP_GetUnlockedContactsString";

                        SqlParameter param1 = new SqlParameter("@ClientID", SqlDbType.Int);
                        param1.Value = ClientID;
                        sqlCmd.Parameters.Add(param1);



                        using (DataSet ds = new DataSet())
                        {
                            using (SqlDataAdapter da = new SqlDataAdapter(sqlCmd))
                            {
                                da.Fill(ds);
                                return ds;
                            }

                        }

                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        //strSql += "select OTHER_DETAILS from [db_owner].[MasterContacts] where RECORD_ID  = '" + ID + "'";
        //ds = DatabaseHelper.FuncExecuteNonqueryConLive(strSql);
        //ds = DatabaseHelper.fetchotherdetails(ID);

        public static DataSet fetchotherdetails(string ID)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connStr))
                {
                    using (SqlCommand sqlCmd = new SqlCommand())
                    {
                        con.Open();
                        sqlCmd.Connection = con;

                        sqlCmd.CommandType = CommandType.StoredProcedure;

                        sqlCmd.CommandText = "USP_fetchotherdetails";

                        SqlParameter param1 = new SqlParameter("@ID", SqlDbType.Int);
                        param1.Value = ID;
                        sqlCmd.Parameters.Add(param1);

                        using (DataSet ds = new DataSet())
                        {
                            using (SqlDataAdapter da = new SqlDataAdapter(sqlCmd))
                            {
                                da.Fill(ds);
                                return ds;
                            }

                        }

                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //SaveHunterFailedDetails(ID)
        //strSqlupdt += "update [db_owner].[MasterContacts] set OTHER_DETAILS ='Email could not be found.' where RECORD_ID  = '" + ID + "'";


        public static DataSet SaveHunterFailedDetails(string ID)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connStr))
                {
                    using (SqlCommand sqlCmd = new SqlCommand())
                    {
                        sqlCmd.Connection = con;

                        sqlCmd.CommandType = CommandType.StoredProcedure;

                        sqlCmd.CommandText = "USP_SaveHunterFailedDetails";

                        SqlParameter param1 = new SqlParameter("@ID", SqlDbType.Int);
                        param1.Value = ID;
                        sqlCmd.Parameters.Add(param1);

                        using (DataSet ds = new DataSet())
                        {
                            using (SqlDataAdapter da = new SqlDataAdapter(sqlCmd))
                            {
                                da.Fill(ds);
                                return ds;
                            }

                        }

                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // SaveHunterDetails(string ID,string Email, string OtherDetails,Boolean email_smtp)
        public static DataSet SaveHunterDetails(string ID, string Email, string OtherDetails)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connStr))
                {
                    using (SqlCommand sqlCmd = new SqlCommand())
                    {
                        con.Open();
                        sqlCmd.Connection = con;

                        sqlCmd.CommandType = CommandType.StoredProcedure;

                        sqlCmd.CommandText = "USP_SaveHunterDetails";

                        SqlParameter param1 = new SqlParameter("@ID", SqlDbType.Int);
                        param1.Value = ID;
                        sqlCmd.Parameters.Add(param1);

                        SqlParameter param2 = new SqlParameter("@Email", SqlDbType.VarChar);
                        param2.Value = Email;
                        sqlCmd.Parameters.Add(param2);

                        SqlParameter param3 = new SqlParameter("@OtherDetails", SqlDbType.VarChar);
                        param3.Value = OtherDetails;
                        sqlCmd.Parameters.Add(param3);

                        using (DataSet ds = new DataSet())
                        {
                            using (SqlDataAdapter da = new SqlDataAdapter(sqlCmd))
                            {
                                da.Fill(ds);
                                return ds;
                            }

                        }

                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        //SaveResponseValidaion

        public static DataSet SaveResponseValidaion(string message, string CONTACT_ID, string ClientID)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connStr))
                {
                    using (SqlCommand sqlCmd = new SqlCommand())
                    {
                        con.Open();
                        sqlCmd.Connection = con;

                        sqlCmd.CommandType = CommandType.StoredProcedure;

                        sqlCmd.CommandText = "USP_SaveResponseValidaion";

                        SqlParameter param1 = new SqlParameter("@message", SqlDbType.VarChar);
                        param1.Value = message;
                        sqlCmd.Parameters.Add(param1);

                        SqlParameter param2 = new SqlParameter("@CONTACT_ID", SqlDbType.VarChar);
                        param2.Value = CONTACT_ID;
                        sqlCmd.Parameters.Add(param2);

                        SqlParameter param3 = new SqlParameter("@ClientID", SqlDbType.VarChar);
                        param3.Value = ClientID;
                        sqlCmd.Parameters.Add(param3);

                        using (DataSet ds = new DataSet())
                        {
                            using (SqlDataAdapter da = new SqlDataAdapter(sqlCmd))
                            {
                                da.Fill(ds);
                                return ds;
                            }

                        }

                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region Client Details ctrl
        public static DataSet ClientAccountDetails(int ClientAccountId)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connStr))
                {
                    using (SqlCommand sqlCmd = new SqlCommand())
                    {
                        con.Open();
                        sqlCmd.Connection = con;
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.CommandText = "usp_ClientAccountDetails";

                        SqlParameter param1 = new SqlParameter("@ClientAccountId", SqlDbType.VarChar);
                        param1.Value = ClientAccountId;

                        sqlCmd.Parameters.Add(param1);

                        using (DataSet ds = new DataSet())
                        {
                            using (SqlDataAdapter da = new SqlDataAdapter(sqlCmd))
                            {
                                da.Fill(ds);
                            }
                            return ds;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static DataSet CreateClientAccount(ClientDetails clientDetails, bool Isactive = false, bool Isadmin = false, bool Isclient = false)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connStr))
                {
                    using (SqlCommand sqlCmd = new SqlCommand())
                    {
                        con.Open();
                        sqlCmd.Connection = con;
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.CommandText = "USP_CreateClienAccount";

                        SqlParameter param1 = new SqlParameter("@FirstName", SqlDbType.VarChar);
                        param1.Value = clientDetails.FirstName;
                        SqlParameter param2 = new SqlParameter("@LastName", SqlDbType.VarChar);
                        param2.Value = clientDetails.LastName;
                        SqlParameter param3 = new SqlParameter("@CompanyName", SqlDbType.VarChar);
                        param3.Value = clientDetails.CompanyName;
                        SqlParameter param4 = new SqlParameter("@UserName", SqlDbType.VarChar);
                        param4.Value = clientDetails.UserName;
                        SqlParameter param5 = new SqlParameter("@Password", SqlDbType.VarChar);
                        param5.Value = clientDetails.Password;
                        //SqlParameter param6 = new SqlParameter("@IsActive", SqlDbType.VarChar);
                        //param6.Value = Isactive;
                        //SqlParameter param7 = new SqlParameter("@IsAdmin", SqlDbType.VarChar);
                        //param7.Value = Isadmin;
                        //SqlParameter param8 = new SqlParameter("@IsClient", SqlDbType.VarChar);
                        //param8.Value = Isclient;
                        //SqlParameter param9 = new SqlParameter("@AccountType", SqlDbType.VarChar);
                        //param9.Value = 3;//clientDetails.AccountType;                
                        SqlParameter param10 = new SqlParameter("@Email", SqlDbType.VarChar);
                        param10.Value = clientDetails.Email;
                        SqlParameter param11 = new SqlParameter("@Provider", SqlDbType.VarChar);
                        param11.Value = clientDetails.Provider;
                        SqlParameter param12 = new SqlParameter("@ClientStatus", SqlDbType.VarChar);
                        param12.Value = clientDetails.ClientStatus;
                        //SqlParameter param13 = new SqlParameter("@Telephone", SqlDbType.VarChar);
                        //param13.Value = clientDetails.Telephone;

                        sqlCmd.Parameters.Add(param1);
                        sqlCmd.Parameters.Add(param2);
                        sqlCmd.Parameters.Add(param3);
                        sqlCmd.Parameters.Add(param4);
                        sqlCmd.Parameters.Add(param5);
                        //sqlCmd.Parameters.Add(param6);
                        //sqlCmd.Parameters.Add(param7);
                        //sqlCmd.Parameters.Add(param8);
                        //sqlCmd.Parameters.Add(param9);
                        sqlCmd.Parameters.Add(param10);
                        sqlCmd.Parameters.Add(param11);
                        sqlCmd.Parameters.Add(param12);
                        //sqlCmd.Parameters.Add(param13);

                        using (DataSet ds = new DataSet())
                        {
                            using (SqlDataAdapter da = new SqlDataAdapter(sqlCmd))
                            {
                                da.Fill(ds);
                            }
                            return ds;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet GetEditContactDetails(int? ClientID)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connStr))
                {
                    using (SqlCommand sqlCmd = new SqlCommand())
                    {
                        con.Open();
                        sqlCmd.Connection = con;
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.CommandText = "usp_GetClientDetailsEdit";
                        SqlParameter param1 = new SqlParameter("@ClientID", SqlDbType.Int);
                        param1.Value = ClientID;
                        sqlCmd.Parameters.Add(param1);
                        using (DataSet ds = new DataSet())
                        {
                            using (SqlDataAdapter da = new SqlDataAdapter(sqlCmd))
                            {
                                da.Fill(ds);
                            }
                            return ds;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static DataSet PostClientDetailsEdit(ClientDetails details)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connStr))
                {
                    using (SqlCommand sqlCmd = new SqlCommand())
                    {
                        con.Open();
                        sqlCmd.Connection = con;
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.CommandText = "usp_PostClientDetailsEdit";

                        SqlParameter param1 = new SqlParameter("@FirstName", SqlDbType.VarChar);
                        param1.Value = details.FirstName;
                        SqlParameter param2 = new SqlParameter("@LastName", SqlDbType.VarChar);
                        param2.Value = details.LastName;
                        SqlParameter param3 = new SqlParameter("@CompanyName", SqlDbType.VarChar);
                        param3.Value = details.CompanyName;
                        SqlParameter param4 = new SqlParameter("@UserName", SqlDbType.VarChar);
                        param4.Value = details.UserName;
                        SqlParameter param5 = new SqlParameter("@Password", SqlDbType.VarChar);
                        param5.Value = details.Password;
                        SqlParameter param6 = new SqlParameter("@Email", SqlDbType.VarChar);
                        param6.Value = details.Email;
                        //SqlParameter param7 = new SqlParameter("@Telephone", SqlDbType.VarChar);
                        //param7.Value = details.Telephone;
                        SqlParameter param8 = new SqlParameter("@ClientStatus", SqlDbType.VarChar);
                        param8.Value = details.ClientStatus;
                        SqlParameter param9 = new SqlParameter("@ClientID", SqlDbType.Int);
                        param9.Value = details.ClientID;

                        sqlCmd.Parameters.Add(param1);
                        sqlCmd.Parameters.Add(param2);
                        sqlCmd.Parameters.Add(param3);
                        sqlCmd.Parameters.Add(param4);
                        sqlCmd.Parameters.Add(param5);
                        sqlCmd.Parameters.Add(param6);
                        //sqlCmd.Parameters.Add(param7);
                        sqlCmd.Parameters.Add(param8);
                        sqlCmd.Parameters.Add(param9);

                        using (DataSet ds = new DataSet())
                        {
                            using (SqlDataAdapter da = new SqlDataAdapter(sqlCmd))
                            {
                                da.Fill(ds);
                            }
                            return ds;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static DataSet DeleteClientDetails(int? ClientID)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connStr))
                {
                    using (SqlCommand sqlCmd = new SqlCommand())
                    {
                        con.Open();
                        sqlCmd.Connection = con;
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.CommandText = "usp_DeleteClientDetails";
                        SqlParameter param1 = new SqlParameter("@ClientID", SqlDbType.Int);
                        param1.Value = ClientID;
                        sqlCmd.Parameters.Add(param1);
                        using (DataSet ds = new DataSet())
                        {
                            using (SqlDataAdapter da = new SqlDataAdapter(sqlCmd))
                            {
                                da.Fill(ds);
                            }
                            return ds;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static DataSet ClientDetailsCheckCredentialsExists(string FirstName, string LastName, string UserName, string Password)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connStr))
                {
                    using (SqlCommand sqlCmd = new SqlCommand())
                    {
                        con.Open();
                        sqlCmd.Connection = con;
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.CommandText = "usp_ClientDetailsCredentialsExists";

                        SqlParameter param1 = new SqlParameter("@FirstName", SqlDbType.VarChar);
                        param1.Value = FirstName;

                        SqlParameter param2 = new SqlParameter("@LastName", SqlDbType.VarChar);
                        param2.Value = LastName;

                        SqlParameter param3 = new SqlParameter("@UserName", SqlDbType.VarChar);
                        param3.Value = UserName;

                        SqlParameter param4 = new SqlParameter("@Password", SqlDbType.VarChar);
                        param4.Value = Password;


                        sqlCmd.Parameters.Add(param1);
                        sqlCmd.Parameters.Add(param2);
                        sqlCmd.Parameters.Add(param3);
                        sqlCmd.Parameters.Add(param4);

                        using (DataSet ds = new DataSet())
                        {
                            using (SqlDataAdapter da = new SqlDataAdapter(sqlCmd))
                            {
                                da.Fill(ds);
                            }
                            return ds;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static DataSet GetLeftSidePaneData(int AccountType)//int? AccountTypeId)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connStr))
                {
                    using (SqlCommand sqlCmd = new SqlCommand())
                    {
                        con.Open();
                        sqlCmd.Connection = con;
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.CommandText = "usp_GetLeftSidePaneData";
                        SqlParameter param1 = new SqlParameter("@AccountType", SqlDbType.Int);
                        param1.Value = AccountType;
                        sqlCmd.Parameters.Add(param1);

                        using (DataSet ds = new DataSet())
                        {
                            using (SqlDataAdapter da = new SqlDataAdapter(sqlCmd))
                            {
                                da.Fill(ds);
                            }
                            return ds;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet GetExistingCompanies()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connStr))
                {
                    using (SqlCommand sqlCmd = new SqlCommand())
                    {
                        con.Open();
                        sqlCmd.Connection = con;
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.CommandText = "usp_GetClientDetailsExistingCompanies";
                        //SqlParameter param1 = new SqlParameter("@ClientID", SqlDbType.Int);
                        //param1.Value = ClientID;
                        //sqlCmd.Parameters.Add(param1);
                        using (DataSet ds = new DataSet())
                        {
                            using (SqlDataAdapter da = new SqlDataAdapter(sqlCmd))
                            {
                                da.Fill(ds);
                            }
                            return ds;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Client Data
        public static DataSet GetUploadedclientdata(string UserName1, string Password1, int AccountType)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connStr))
                {
                    using (SqlCommand sqlCmd = new SqlCommand())
                    {
                        con.Open();
                        sqlCmd.Connection = con;
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.CommandText = "usp_GetClientDataUploaded";
                        SqlParameter param1 = new SqlParameter("@UserName1", SqlDbType.VarChar);
                        param1.Value = UserName1;
                        SqlParameter param2 = new SqlParameter("@Password1", SqlDbType.VarChar);
                        param2.Value = Password1;
                        SqlParameter param3 = new SqlParameter("@AccountType", SqlDbType.Int);
                        param3.Value = AccountType;

                        sqlCmd.Parameters.Add(param1);
                        sqlCmd.Parameters.Add(param2);
                        sqlCmd.Parameters.Add(param3);

                        using (DataSet ds = new DataSet())
                        {
                            using (SqlDataAdapter da = new SqlDataAdapter(sqlCmd))
                            {
                                da.Fill(ds);
                            }
                            return ds;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet Data_Inventory(string varclientid, bool LoggedIn, string VarstrArrayMacIDAndIP1, string varstrArrayMacIDAndIP0, string VarstrLogDetails, bool Isactive)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connStr))
                {
                    using (SqlCommand sqlCmd = new SqlCommand())
                    {
                        con.Open();
                        sqlCmd.Connection = con;
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.CommandText = "usp_DataInventory";
                        SqlParameter param1 = new SqlParameter("@varclientid", SqlDbType.Int);
                        param1.Value = varclientid;
                        SqlParameter param2 = new SqlParameter("@LoggedIn", SqlDbType.Bit);
                        param2.Value = LoggedIn;
                        SqlParameter param3 = new SqlParameter("@VarstrArrayMacIDAndIP1", SqlDbType.VarChar);
                        param3.Value = VarstrArrayMacIDAndIP1;
                        SqlParameter param4 = new SqlParameter("@varstrArrayMacIDAndIP0", SqlDbType.VarChar);
                        param4.Value = varstrArrayMacIDAndIP0;
                        SqlParameter param5 = new SqlParameter("@VarstrLogDetails", SqlDbType.VarChar);
                        param5.Value = VarstrLogDetails;
                        SqlParameter param6 = new SqlParameter("@Isactive", SqlDbType.Bit);
                        param6.Value = Isactive;

                        sqlCmd.Parameters.Add(param1);
                        sqlCmd.Parameters.Add(param2);
                        sqlCmd.Parameters.Add(param3);
                        sqlCmd.Parameters.Add(param4);
                        sqlCmd.Parameters.Add(param5);
                        sqlCmd.Parameters.Add(param6);

                        using (DataSet ds = new DataSet())
                        {
                            using (SqlDataAdapter da = new SqlDataAdapter(sqlCmd))
                            {
                                da.Fill(ds);
                            }
                            return ds;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static DataSet Upload_client_data_file(int varClientID, string filename, int canview, int candownload)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connStr))
                {
                    using (SqlCommand sqlCmd = new SqlCommand())
                    {
                        con.Open();
                        sqlCmd.Connection = con;
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.CommandText = "usp_PostUploadClientDataFile";
                        SqlParameter param1 = new SqlParameter("@varClientID", SqlDbType.Int);
                        param1.Value = varClientID;
                        SqlParameter param2 = new SqlParameter("@filename", SqlDbType.VarChar);
                        param2.Value = filename;
                        SqlParameter param3 = new SqlParameter("@canview", SqlDbType.Int);
                        param3.Value = canview;
                        SqlParameter param4 = new SqlParameter("@candownload", SqlDbType.Int);
                        param4.Value = candownload;

                        sqlCmd.Parameters.Add(param1);
                        sqlCmd.Parameters.Add(param2);
                        sqlCmd.Parameters.Add(param3);
                        sqlCmd.Parameters.Add(param4);


                        using (DataSet ds = new DataSet())
                        {
                            using (SqlDataAdapter da = new SqlDataAdapter(sqlCmd))
                            {
                                da.Fill(ds);
                            }
                            return ds;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static DataSet GetUsersData(int AccounttypeId)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connStr))
                {
                    using (SqlCommand sqlCmd = new SqlCommand())
                    {
                        con.Open();
                        sqlCmd.Connection = con;
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.CommandText = "usp_GetUsersClientData";
                        SqlParameter param1 = new SqlParameter("@AccounttypeId", SqlDbType.Int);
                        param1.Value = AccounttypeId;

                        sqlCmd.Parameters.Add(param1);

                        using (DataSet ds = new DataSet())
                        {
                            using (SqlDataAdapter da = new SqlDataAdapter(sqlCmd))
                            {
                                da.Fill(ds);
                            }
                            return ds;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region List 
        public static DataSet DeleteFavouriteList(int DelID)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connStr))
                {
                    using (SqlCommand sqlCmd = new SqlCommand())
                    {
                        con.Open();
                        sqlCmd.Connection = con;
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.CommandText = "usp_DeleteFavouriteList";
                        SqlParameter param1 = new SqlParameter("@DelID", SqlDbType.Int);
                        param1.Value = DelID;
                        //SqlParameter param2 = new SqlParameter("@ClientID", SqlDbType.Int);
                        //param2.Value = ClientID;

                        sqlCmd.Parameters.Add(param1);
                        //sqlCmd.Parameters.Add(param2);

                        using (DataSet ds = new DataSet())
                        {
                            using (SqlDataAdapter da = new SqlDataAdapter(sqlCmd))
                            {
                                da.Fill(ds);
                                return ds;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet GetAllContactAgainstList(int ListID)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connStr))
                {
                    using (SqlCommand sqlCmd = new SqlCommand())
                    {
                        con.Open();
                        sqlCmd.Connection = con;
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.CommandText = "usp__GetAllContactAgainstList";
                        SqlParameter param1 = new SqlParameter("@ListID", SqlDbType.Int);
                        param1.Value = ListID;

                        sqlCmd.Parameters.Add(param1);

                        using (DataSet ds = new DataSet())
                        {
                            using (SqlDataAdapter da = new SqlDataAdapter(sqlCmd))
                            {
                                da.Fill(ds);
                            }
                            return ds;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet GetAllHunterMail(int ClientID)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connStr))
                {
                    using (SqlCommand sqlCmd = new SqlCommand())
                    {
                        con.Open();
                        sqlCmd.Connection = con;
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.CommandText = "usp_GetAllHunterMail";
                        SqlParameter param1 = new SqlParameter("@ClientID", SqlDbType.Int);
                        param1.Value = ClientID;

                        sqlCmd.Parameters.Add(param1);

                        using (DataSet ds = new DataSet())
                        {
                            using (SqlDataAdapter da = new SqlDataAdapter(sqlCmd))
                            {
                                da.Fill(ds);
                            }
                            return ds;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        public static DataSet GetContactDetailsForExtension(string location, string contactname, string company, string industry, string linkedurl, string website, string mode, string title)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connStr))
                {
                    using (SqlCommand sqlCmd = new SqlCommand())
                    {
                        con.Open();
                        sqlCmd.Connection = con;
                        sqlCmd.CommandType = CommandType.StoredProcedure;

                        sqlCmd.CommandText = "usp_ApiCallCheckExistOrNon";
                        SqlParameter param1 = new SqlParameter("@Linkedinurl", SqlDbType.VarChar);
                        param1.Value = linkedurl;
                        sqlCmd.Parameters.Add(param1);

                        SqlParameter param7 = new SqlParameter("@Contact", SqlDbType.VarChar);
                        param7.Value = contactname;
                        sqlCmd.Parameters.Add(param7);

                        SqlParameter param2 = new SqlParameter("@Company_Name", SqlDbType.VarChar);
                        param2.Value = company;
                        sqlCmd.Parameters.Add(param2);

                        SqlParameter param3 = new SqlParameter("@Industry", SqlDbType.VarChar);
                        param3.Value = industry;
                        sqlCmd.Parameters.Add(param3);

                        SqlParameter param4 = new SqlParameter("@Location", SqlDbType.VarChar);
                        param4.Value = location;
                        sqlCmd.Parameters.Add(param4);

                        SqlParameter param5 = new SqlParameter("@Website", SqlDbType.VarChar);
                        param5.Value = website;
                        sqlCmd.Parameters.Add(param5);

                        SqlParameter param6 = new SqlParameter("@Mode", SqlDbType.VarChar);
                        param6.Value = mode;
                        sqlCmd.Parameters.Add(param6);

                        SqlParameter param8 = new SqlParameter("@Title", SqlDbType.VarChar);
                        param8.Value = title;
                        sqlCmd.Parameters.Add(param8);

                        using (DataSet ds = new DataSet())
                        {
                            using (SqlDataAdapter da = new SqlDataAdapter(sqlCmd))
                            {
                                da.Fill(ds);
                                return ds;
                            }

                        }

                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet GetCompanyDataForExtension(string companyurl)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connStr))
                {
                    using (SqlCommand sqlCmd = new SqlCommand())
                    {
                        con.Open();
                        sqlCmd.Connection = con;
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.CommandText = "usp_ExtentionGetWebsiteUrl";

                        SqlParameter param1 = new SqlParameter("@Website", SqlDbType.VarChar);
                        param1.Value = companyurl;

                        sqlCmd.Parameters.Add(param1);

                        using (DataSet ds = new DataSet())
                        {
                            using (SqlDataAdapter da = new SqlDataAdapter(sqlCmd))
                            {
                                da.Fill(ds);
                                return ds;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }

        #region searchFilter
        public static int SaveSearchFilter(string clientId, FilterApplied filterObj = null)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connStr))
                {
                    using (SqlCommand sqlCmd = new SqlCommand())
                    {
                        sqlCmd.Connection = con;
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.CommandText = "Prc_savefilter";

                        SqlParameter param1 = new SqlParameter("@ClientID", clientId);
                        //param1.Value = clientId;

                        SqlParameter param2 = new SqlParameter("@Categories", SqlDbType.VarChar);
                        param2.Value = filterObj.Industries;

                        SqlParameter param3 = new SqlParameter("@title", SqlDbType.VarChar);
                        param3.Value = filterObj.HeaderTitle;

                        SqlParameter param4 = new SqlParameter("@position", SqlDbType.VarChar);
                        param4.Value = filterObj.Position;

                        SqlParameter param5 = new SqlParameter("@CompanyName", SqlDbType.VarChar);
                        param5.Value = filterObj.CompanyName;

                        SqlParameter param6 = new SqlParameter("@Countries", SqlDbType.VarChar);
                        param6.Value = filterObj.location.Countries;

                        SqlParameter param7 = new SqlParameter("@States", SqlDbType.VarChar);
                        param7.Value = filterObj.location.states;

                        SqlParameter param8 = new SqlParameter("@SubCategoryId", SqlDbType.VarChar);
                        param8.Value = filterObj.SubCategory;

                        SqlParameter paramMode = new SqlParameter("@Mode", SqlDbType.Int);
                        paramMode.Value = 1;

                        sqlCmd.Parameters.Add(param1);
                        sqlCmd.Parameters.Add(param2);
                        sqlCmd.Parameters.Add(param3);
                        sqlCmd.Parameters.Add(param4);
                        sqlCmd.Parameters.Add(param5);
                        sqlCmd.Parameters.Add(param6);
                        sqlCmd.Parameters.Add(param7);
                        sqlCmd.Parameters.Add(param8);
                        sqlCmd.Parameters.Add(paramMode);


                        con.Open();
                        return sqlCmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }

           
        }
        public static int RemoveSearchFilter(string Id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connStr))
                {
                    using (SqlCommand sqlCmd = new SqlCommand())
                    {
                        sqlCmd.Connection = con;
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.CommandText = "Prc_savefilter";

                        SqlParameter param1 = new SqlParameter("@Id", Id);
                        //param1.Value = clientId; 

                        SqlParameter param4 = new SqlParameter("@Mode", SqlDbType.Int);
                        param4.Value = 2;

                        sqlCmd.Parameters.Add(param1);
                        sqlCmd.Parameters.Add(param4);

                        con.Open();
                        return sqlCmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }

            return 0;
        }
        #endregion

        #region Related Contact
        public static DataSet GetRelatedContactDtls(string companyName, int pageNo = 1, int pageSize = 10)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connStr))
                {
                    using (SqlCommand sqlCmd = new SqlCommand())
                    {
                        con.Open();
                        sqlCmd.Connection = con;
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.CommandText = "usp_GetExtensionRelatedContactDtl";

                        SqlParameter param1 = new SqlParameter("@CompanyName", SqlDbType.VarChar);
                        param1.Value = companyName;

                        SqlParameter param2 = new SqlParameter("@PageNo", SqlDbType.Int);
                        param2.Value = pageNo;

                        SqlParameter param3 = new SqlParameter("@PageSize", SqlDbType.Int);
                        param3.Value = pageSize;

                        sqlCmd.Parameters.Add(param1);
                        sqlCmd.Parameters.Add(param2);
                        sqlCmd.Parameters.Add(param3);

                        using (DataSet ds = new DataSet())
                        {
                            using (SqlDataAdapter da = new SqlDataAdapter(sqlCmd))
                            {
                                da.Fill(ds);
                                return ds;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }
        #endregion

        public static DataSet FnSetDropdowndata()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connStr))
                {
                    using (SqlCommand sqlCmd = new SqlCommand())
                    {
                        con.Open();
                        sqlCmd.Connection = con;
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.CommandText = "FnSetDropdowndata";

                        using (DataSet ds = new DataSet())
                        {
                            using (SqlDataAdapter da = new SqlDataAdapter(sqlCmd))
                            {
                                da.Fill(ds);
                                return ds;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }

        public static DataSet FilterCompanyFilterData(string Industry, string Subcategory, string CompanyName, string company_url, string company_revenue, string Country, string State_, string start, string length, string columnDatabase, string sortDirection)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connStr))
                {
                    using (SqlCommand sqlCmd = new SqlCommand())
                    {
                        con.Open();
                        sqlCmd.Connection = con;
                        sqlCmd.CommandType = CommandType.StoredProcedure;


                        sqlCmd.CommandText = "USP_GetCompanyFilterData";
                        SqlParameter param1 = new SqlParameter("@Industry", SqlDbType.VarChar);
                        param1.Value = Industry;
                        sqlCmd.Parameters.Add(param1);

                        SqlParameter param2 = new SqlParameter("@Subcategory", SqlDbType.VarChar);
                        param2.Value = Subcategory;
                        sqlCmd.Parameters.Add(param2);

                        SqlParameter param3 = new SqlParameter("@CompanyName", SqlDbType.VarChar);
                        param3.Value = CompanyName;
                        sqlCmd.Parameters.Add(param3);

                        SqlParameter param5 = new SqlParameter("@company_url", SqlDbType.VarChar);
                        param5.Value = company_url;
                        sqlCmd.Parameters.Add(param5);

                        SqlParameter param6 = new SqlParameter("@company_revenue", SqlDbType.VarChar);
                        param6.Value = company_revenue;
                        sqlCmd.Parameters.Add(param6);

                        SqlParameter param7 = new SqlParameter("@Country", SqlDbType.VarChar);
                        param7.Value = Country;
                        sqlCmd.Parameters.Add(param7);

                        SqlParameter param8 = new SqlParameter("@State_", SqlDbType.VarChar);
                        param8.Value = State_;
                        sqlCmd.Parameters.Add(param8);

                        SqlParameter param9 = new SqlParameter("@columnDatabase", SqlDbType.VarChar);
                        param9.Value = columnDatabase;
                        sqlCmd.Parameters.Add(param9);

                        SqlParameter param10 = new SqlParameter("@sortDirection", SqlDbType.VarChar);
                        param10.Value = sortDirection;
                        sqlCmd.Parameters.Add(param10);

                        SqlParameter param11 = new SqlParameter("@start", SqlDbType.VarChar);
                        param11.Value = start;
                        sqlCmd.Parameters.Add(param11);

                        SqlParameter param12 = new SqlParameter("@length", SqlDbType.VarChar);
                        param12.Value = length;
                        sqlCmd.Parameters.Add(param12);

                        sqlCmd.CommandTimeout = 180;
                        BaseController.LogErrorWithMethodName("[DatabaseHelper] - FilterCompanyFilterData", null, "N", "Industry :" + Industry + ", Subcategory : " + Subcategory + ", CompanyName : " + CompanyName + ", company_url : " + company_url + ", company_revenue : " + company_revenue + ", Country : " + Country + ", State_ : " + State_ + ",  columnDatabase : " + columnDatabase + ", sortDirection : " + sortDirection + ", start : " + start + ", length : " + length);
                        DataSet dsFilterContactData = new DataSet();
                        using (SqlDataAdapter da = new SqlDataAdapter(sqlCmd))
                        {
                            da.Fill(dsFilterContactData);
                            return dsFilterContactData;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                BaseController.LogErrorWithMethodName("[DatabaseHelper] - FilterCompanyFilterData", null, "N", ex.Message + ", Stacktrace : " + ex.StackTrace);
                throw ex;
            }
        }
    }
}