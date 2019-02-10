using ApiManager.Security;
using DbManager.SQLManager;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiManager.Repository
{
    public class AdminRepository
    {
        private static SqlProcedureManager objSqlProcManager;
        Encrypt objEncrypt = new Encrypt();
        string connectionString = ConfigurationManager.ConnectionStrings["BkConnection"].ConnectionString.Trim();

        #region Orders

        public DataTable GetAllOrderList(string UserId, ref string replay)
        {
            string msg_code = string.Empty;
            string msg = string.Empty;
            DataTable dTable = new DataTable();
            try
            {

                objSqlProcManager = new SqlProcedureManager(connectionString);
                dTable = objSqlProcManager.ExecuteDataTable("admin_Get_All_Orders", ref replay,
                    UserId);

            }
            catch (Exception errorException)
            {
                throw errorException;
            }
            return dTable;
        }


        #endregion Orders

    }
}
