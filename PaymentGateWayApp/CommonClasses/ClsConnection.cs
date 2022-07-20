using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PaymentGateWayApp.CommonClasses
{
    public class ClsConnection
    {

        //public static SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ExcelDataPortalEntities"].ToString());
        //public static SqlConnection conlive = new SqlConnection(ConfigurationManager.ConnectionStrings["ImportexcelEntities"].ToString());

        //Unsa - Added code optimization
        static string conStr = ConfigurationManager.ConnectionStrings["ExcelDataPortalEntities"].ToString();

        public static DataSet FuncExecuteNonquery(string strsql)
        {
            using (DataSet ds = new DataSet())
            {
                using (SqlConnection con = new SqlConnection(conStr))
                {
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
        public static DataSet FuncExecuteNonqueryConLive(string strsql)
        {
            using (DataSet ds = new DataSet())
            {
                using (SqlConnection con = new SqlConnection(conStr))
                {
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

        public static DataSet FunGetUsersData(int EmpCountRange, string Position, string Region, string EmpCount, string Companycount, string Industry)
        {
            try
            {
                using (DataTable dt = new DataTable())
                {
                    using (SqlConnection con = new SqlConnection(conStr))
                    {

                        using (SqlCommand cmd = new SqlCommand("sp_GetUsersData", con))
                        {
                            if (Position != null && Position != "")
                            {
                                cmd.Parameters.Add(new SqlParameter("@Position", Position));
                            }
                            else
                            {
                                cmd.Parameters.Add(new SqlParameter("@Position", DBNull.Value));
                            }
                            if (Region != null && Region != "")
                            {
                                cmd.Parameters.Add(new SqlParameter("@Region", Region));
                            }
                            else
                            {
                                cmd.Parameters.Add(new SqlParameter("@Region", DBNull.Value));
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

                            cmd.Parameters.Add(new SqlParameter("@EmpCountRange", EmpCountRange));

                            // cmd.Parameters.Add(new SqlParameter("@CompanyCount", Companycount));
                            cmd.CommandType = CommandType.StoredProcedure;

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
    }
}