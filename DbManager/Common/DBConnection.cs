using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbManager.Common
{
    public class DBConnection
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string DataSource { get; set; }
        public string Database { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Status { get; set; } 
        public string ConnectionString { get; set; }
        public string DatabaseYear { get; set; }
        public DBConnection(string dbConnectionName, string databaseYear)
        {
            Name = dbConnectionName;
            DatabaseYear = databaseYear;
        }
    }
   public enum DBType
    {
        SqlClient=1,
        Oracle=2 
    }

    public enum DBName
    {
        FBSecurity = 1,
        FBReportCon = 2,//FB_Report
        FBOperationDataAccess = 3,//FloraBank_Online
        FBDataAccess = 4,//FloraBank_Online
        FBDynamicParameter = 5, //FloraBank_Web
        FBForeignExchange = 6,//ForeignExchange
        FBSYSMAN = 7//SYSMAN
    }

}
