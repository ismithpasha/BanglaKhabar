using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbManager.Common;

namespace DbManager.Common
{
    public static class ConnectionManager
    {
        static string conn = string.Empty;
        static ConnectionProvider conProvider = new ConnectionProvider();
        public static List<DBConnection> DBConnectionList = new List<DBConnection>();
        public static DBConnection GetConnection(DBConnection dbConnection)
        {
            if (DBConnectionList != null)
            {
                foreach (DBConnection dbCon in DBConnectionList)
                {
                    if (dbCon != null)
                    {
                        if ((dbCon.Name == dbConnection.Name) && (dbCon.DatabaseYear == dbConnection.DatabaseYear))
                        {
                            return dbCon;
                        }
                    }
                }
            }
            DBConnection dbConn = conProvider.GetDBConnection(dbConnection.Name, dbConnection.DatabaseYear, dbConnection);
            DBConnectionList.Add(dbConn);
            return dbConn;
        }
    }
}
