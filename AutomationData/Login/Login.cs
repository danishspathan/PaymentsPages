using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationData
{
    public class Login
    {
        //public static SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ExcelDataPortalEntities"].ToString());
        public static string connstr = ConfigurationManager.ConnectionStrings["ExcelDataPortalEntities"].ToString();
        /// <summary>
        /// Check if User Exists
        /// </summary>
        /// <param name="_UserName"></param>
        /// <param name="_Password"></param>
        /// <returns></returns>
        public DataSet CheckUserExists(string _UserName, string _Password)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connstr))
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

        public DataSet GetClientDetails(int ClientID)
        {
            string strSQL = "SELECT ClientID,ISNULL(Credit,0) Credit, UserName,Firstname,PASSWORD,ISACTIVE,IsAdmin,IsClient,AccountType,CompanyName,otp,ISNULL(ShowAnalytics,0) ShowAnalytics,Provider from tbl_ClientDetails where ClientID  = '" + ClientID + "'";

            SqlHelper sqlHelper = new SqlHelper();
            DataSet ds = sqlHelper.GetDataSetUsingQuery(strSQL);

            return ds;
        }

        public int UpdateClientOtp(string OTP, string ClientID)
        {
            string strSQL = "update TBL_CLIENTDETAILS set otp ='" + OTP + "' where ClientID  = '" + ClientID + "'";

            SqlHelper sqlHelper = new SqlHelper();
            int rowseffected = sqlHelper.ExecuteQry(strSQL);

            return rowseffected;
        }

        public int GetClientIDFromEmail(string EmailID)
        {
            string str = "select ClientID from tbl_ClientDetails where Email = '" + EmailID + "'";
            SqlHelper sqlHelper = new SqlHelper();
            DataSet ds = sqlHelper.GetDataSetUsingQuery(str);
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

            return result;
        }
    }
}
