using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbManager.Common;
using System.Data.Common;

namespace DbManager.SQLManager
{
    public class SqlQueryManager
    {
        private string connectionString = string.Empty;
        private static SqlQueryManager objSqlQueryManager = null;
        public SqlQueryManager(string dbConnectionName, string ReportYear)
        {
            connectionString = ConnectionManager.GetConnection(new DBConnection(dbConnectionName, ReportYear)).ConnectionString;
        }
        public static SqlQueryManager Instance()
        {
            //if (objSqlQueryManager == null)
            //objSqlQueryManager = new LoggerRepository();
            return objSqlQueryManager;
        }
        public bool ReturnData(string sqlstring, ref DataTable dataTable, ref DBResponse dbResponse)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(sqlstring, conn))
                    {
                        cmd.CommandType = CommandType.Text;
                        SqlDataAdapter sqlDa = new SqlDataAdapter(cmd);
                        sqlDa.Fill(dataTable);
                        dbResponse = new DBResponse { Code = 0, Message = "Success", Details = "Ok" };
                        return true;
                    }
                }
            }
            catch (Exception errorExcep)
            {
                dbResponse = new DBResponse { Code = 1, Message = "Error", Details = errorExcep.Message };
            }
            return false;
        }


        public DataSet ReturnDataSet(string sqlstring)
        {
            DataSet ds = new DataSet();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(sqlstring, conn))
                    {
                        cmd.CommandTimeout = 10000;
                        cmd.CommandType = CommandType.Text;
                        SqlDataAdapter sqlDa = new SqlDataAdapter(cmd);
                        sqlDa.Fill(ds);
                        sqlDa.Dispose();

                        cmd.Dispose();
                        conn.Dispose();

                        return ds;
                    }
                }
            }
            catch (Exception errorExcep)
            {
                return null;
            }
            finally
            {
                ds.Dispose();
            }
            return null;
        }

        public IDataReader ReturnDataReader(string sqlstring)
        {
            IDataReader idr;
            SqlConnection conn = new SqlConnection(connectionString);
            try
            {
                SqlConnection.ClearAllPools();
                SqlCommand cmd = new SqlCommand(sqlstring, conn);
                if (cmd.Connection.State == ConnectionState.Broken || cmd.Connection.State == ConnectionState.Closed)
                {
                    cmd.Connection.Open();
                } 
                cmd.CommandType = CommandType.Text;
                idr = cmd.ExecuteReader();
                //idr.Dispose();
            }
            catch (Exception errorExcep)
            {
                return null;
            }
            finally
            {
                //idr.Dispose();
                //conn.Close();
                //conn.Dispose();
            }
            return idr;
        }

    }
}
