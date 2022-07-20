using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace AutomationData
{
    public class SqlHelper
    {
        private readonly string strConnectionString;

        public SqlHelper()
        {
            if (ConfigurationManager.ConnectionStrings["ExcelDataPortalEntities"] != null)
                strConnectionString = ConfigurationManager.ConnectionStrings["ExcelDataPortalEntities"].ConnectionString;
        }

        /// <summary>
        /// Execute stored procedure and returns dataset
        /// </summary>
        public DataSet GetDataUsingStoredProcedures(string strGetSpName, object[] param, object[] values)
        {

            SqlConnection con;
            SqlDataAdapter dAdapter;
            SqlCommand cmd;
            DataSet ds;
            try
            {
                ds = new DataSet();
                cmd = new SqlCommand();
                dAdapter = new SqlDataAdapter();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = strGetSpName;
                con = new SqlConnection(strConnectionString);
                cmd.Connection = con;
                cmd.CommandTimeout = 0;
                for (int i = 0; i < param.Length; i++)
                {
                    if (param[i].ToString().Length > 0)
                        cmd.Parameters.AddWithValue(param[i].ToString(), values[i]);
                }
                con.Open();
                dAdapter.SelectCommand = cmd;
                dAdapter.Fill(ds);
                con.Close();
            }
            catch
            {
                throw;
            }
            finally
            {
                con = null;
                dAdapter = null;
                cmd = null;

            }
            return ds;
        }

        /// <summary>
        /// Executes query and returns dataset
        /// </summary>
        public DataSet GetDataSetUsingQuery(string Qrystr)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter sqladapt = new SqlDataAdapter(Qrystr, strConnectionString);
            try
            {
                sqladapt.Fill(ds);
                return ds;
            }
            catch
            {
                throw;
            }
        }

        public int ExecuteQry(string strquery, int commandtimeout = -1)
        {
            SqlConnection sqlcon = new SqlConnection(strConnectionString);
            SqlCommand cmd;
            int noofrowseffected = 0;
            try
            {
                sqlcon.Open();
                cmd = sqlcon.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strquery;
                if (commandtimeout != -1)
                {
                    cmd.CommandTimeout = commandtimeout;
                }

                noofrowseffected = cmd.ExecuteNonQuery();

                cmd = null;
                sqlcon.Close();
            }
            catch
            {

                cmd = null;
                sqlcon = null;
                throw;
            }
            finally
            {
                sqlcon = null;
                cmd = null;
            }

            return noofrowseffected;
        }
    }
}
