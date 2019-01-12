using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Data.SqlClient;
using System.Data;

namespace DbManager.Common
{
    public class ConnectionProvider
    {
        public DBConnection GetDBConnection(string dbName, string dbYear, DBConnection dbConnection)
        { 
                return GetConnection(dbName, dbYear, ref dbConnection); 
        }

        #region ***** Single DES *****
        static byte[] Key = ASCIIEncoding.ASCII.GetBytes("fslsmblk");
        static byte[] IV = ASCIIEncoding.ASCII.GetBytes("sfmsbllv");

        private string DES_Decrypt(string cryptedString)
        {
            if (String.IsNullOrEmpty(cryptedString))
            {
                throw new ArgumentNullException("The string which needs to be decrypted can not be null.");
            }

            DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
            MemoryStream memoryStream = new MemoryStream(Convert.FromBase64String(cryptedString));
            CryptoStream cryptoStream = new CryptoStream(memoryStream, cryptoProvider.CreateDecryptor(Key, IV), CryptoStreamMode.Read);
            StreamReader reader = new StreamReader(cryptoStream);

            return reader.ReadToEnd();
        }
        #endregion

        private DBConnection ParseXMLFile(string bdName, string connectionStringYear)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                DBConnection dbCon = new DBConnection("","");
                //doc.Load(System.Windows.Forms.Application.StartupPath + "\\App Configuration\\CONNECTION.xml");
                //doc.Load(ConfigurationManager.ConnectionStrings["ConFilePath"].ConnectionString);
                //doc.Load(@"D:\Flora Bank\Release\Flora Bank\CONNECTION.xml");
               // doc.Load(@"D:\Flora Bank\Release\Flora Bank\CONNECTION.xml");
                doc.Load(@"D:\AMSConnection\CONNECTION.xml");
                XmlNodeList bookList = doc.GetElementsByTagName("CONNECTION");


                int i = 0;
                foreach (XmlNode node in bookList)
                {
                    XmlElement bookElement = (XmlElement)node;

                    if (bdName== bookElement.GetElementsByTagName("NAME")[i].InnerText)
                    {
                        dbCon.Id = i;
                        dbCon.Name = bookElement.GetElementsByTagName("NAME")[i].InnerText;
                        dbCon.Type = bookElement.GetElementsByTagName("CONNECTIONTYPE")[i].InnerText;
                        dbCon.DataSource = bookElement.GetElementsByTagName("DATASOURCE")[i].InnerText;
                        dbCon.Database = bookElement.GetElementsByTagName("DATABASE")[i].InnerText;
                        dbCon.UserName = bookElement.GetElementsByTagName("USERNAME")[i].InnerText;
                        dbCon.Password = bookElement.GetElementsByTagName("PASSWORD")[i].InnerText;
                        dbCon.Status = bookElement.GetElementsByTagName("STATUS")[i].InnerText;
                        dbCon.DatabaseYear = bookElement.GetElementsByTagName("NAME")[i].InnerText;

                        if (dbCon.Type == DBType.SqlClient.ToString())
                        {
                            dbCon.ConnectionString = "Data Source=" + dbCon.DataSource + "; Initial Catalog=" + dbCon.Database + ";Persist Security Info=True;User ID=" + dbCon.UserName + ";Password=" + dbCon.Password;
                        }
                        else if (dbCon.Type == DBType.Oracle.ToString())
                        {
                            dbCon.ConnectionString = "Data Source=" + dbCon.DataSource + ";Persist Security Info=True;user id=" + dbCon.UserName + ";password=" + dbCon.Password + ";";
                        }

                        return dbCon;

                    } 
                   
                }

            }
            catch (Exception errorException)
            {
                throw errorException;
            }
            return null;
        }

        public DBConnection GetConnection(string bdName, string connectionStringYear, ref DBConnection dbConnection)
        {
             
            if (dbConnection.ConnectionString == null)
            {
              return  dbConnection = ParseXMLFile( bdName,  connectionStringYear);
            } 
            UpdateUser(ref dbConnection);  

            string sqlstring = "select database_name,server_name,db_user,db_pwd from wv_parameter_report_server where database_name='"+ bdName + "' and period_code='" + connectionStringYear + "'";
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(dbConnection.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(sqlstring, conn))
                    {
                        cmd.CommandType = CommandType.Text;
                        SqlDataAdapter sqlDa = new SqlDataAdapter(cmd);
                        sqlDa.Fill(dt);

                        DBConnection dbCon = new DBConnection(bdName, connectionStringYear);

                        if (dt != null)
                        {
                            dbCon.Name = bdName;
                            dbCon.Type = "SqlClient";// dt.Rows[0]["database_name"].ToString();
                            dbCon.DataSource = dt.Rows[0]["server_name"].ToString();
                            dbCon.Database = dt.Rows[0]["database_name"].ToString();
                            dbCon.UserName = dt.Rows[0]["db_user"].ToString();
                            dbCon.Password = dt.Rows[0]["db_pwd"].ToString();
                            dbCon.Status = "1";
                            dbCon.DatabaseYear = connectionStringYear;

                            //if (SystemSecurity.IsEncrypted(dbCon.Password))
                            //{
                            //    dbCon.Password = SystemSecurity.DecryptValue(dbCon.Password);
                            //}
                            //if (SystemSecurity.IsEncrypted(dbCon.UserName))
                            //{
                            //    dbCon.UserName = SystemSecurity.DecryptValue(dbCon.UserName);
                            //}

                            if (dbCon.Type == DBType.SqlClient.ToString())
                            {
                                dbCon.ConnectionString = "Data Source=" + dbCon.DataSource + "; Initial Catalog=" + dbCon.Database + ";Persist Security Info=True;User ID=" + dbCon.UserName + ";Password=" + dbCon.Password;
                            }
                            else if (dbCon.Type == DBType.Oracle.ToString())
                            {
                                dbCon.ConnectionString = "Data Source=" + dbCon.DataSource + ";Persist Security Info=True;user id=" + dbCon.UserName + ";password=" + dbCon.Password + ";";
                            }

                            return dbCon;

                        }
                    }
                }
            }
            catch (Exception errorExcep)
            {

            }
            return null;
        }

        public void UpdateUser(ref DBConnection dbConnection)
        {
            if (dbConnection.ConnectionString == null)
            {
                dbConnection = ParseXMLFile("","");
            }
            string sqlstring = "select netappuser, netappass, netwebuser, netwebpass, centralip from wv_net_system36";
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(dbConnection.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(sqlstring, conn))
                    {
                        cmd.CommandType = CommandType.Text;
                        SqlDataAdapter sqlDa = new SqlDataAdapter(cmd);
                        sqlDa.Fill(dt);

                         
                        if (dt != null)
                        {

                            if (dt.Rows.Count > 0)
                            {

                                 
                                dbConnection.UserName = dt.Rows[0]["netappuser"].ToString();
                                dbConnection.Password = dt.Rows[0]["netappass"].ToString();


                                //if (SystemSecurity.IsEncrypted(dbConnection.Password))
                                //{
                                //    dbConnection.Password = SystemSecurity.DecryptValue(dbConnection.Password);
                                //}
                                //if (SystemSecurity.IsEncrypted(dbConnection.UserName))
                                //{
                                //    dbConnection.UserName = SystemSecurity.DecryptValue(dbConnection.UserName);
                                //}
                                if (dbConnection.Type == DBType.SqlClient.ToString())
                                {
                                    dbConnection.ConnectionString = "Data Source=" + dbConnection.DataSource + "; Initial Catalog=" + dbConnection.Database + ";Persist Security Info=True;User ID=" + dbConnection.UserName + ";Password=" + dbConnection.Password;
                                }
                                else if (dbConnection.Type == DBType.Oracle.ToString())
                                {
                                    dbConnection.ConnectionString = "Data Source=" + dbConnection.DataSource + ";Persist Security Info=True;user id=" + dbConnection.UserName + ";password=" + dbConnection.Password + ";";
                                }
                            } 

                        }
                    }
                }
            }
            catch (Exception errorExcep)
            {

            }
            
        }
    }

}
