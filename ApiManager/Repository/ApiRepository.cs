using ApiManager.Models;
using ApiManager.Models.Orders;
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
    public class ApiRepository
    {
        private static SqlProcedureManager objSqlProcManager;
        Encrypt objEncrypt = new Encrypt();
        string connectionString = ConfigurationManager.ConnectionStrings["BkConnection"].ConnectionString.Trim();
        //  SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["OnlineBookingConnection"].ConnectionString);

        #region Security
        public string[] UserSignUp(UserInfo userInfo, ref string replay)
        {
            string[] res = new string[4];

            string msg_code = string.Empty;
            string msg = string.Empty;

            try
            {
                string encryptedPassword = objEncrypt.EncryptString(userInfo.Password, "");

                DateTime myDate = Convert.ToDateTime(userInfo.BirthDate);
                objSqlProcManager = new SqlProcedureManager(connectionString);
                string spname = "sp_User_Sign_Up_Update";

                var result = objSqlProcManager.ExecuteNonQueryParam(spname, ref replay,
                    0,
                    userInfo.UserId,
                    userInfo.FirstName,
                    userInfo.LastName,
                    userInfo.NickName,
                    myDate.ToString("yyyy-MM-dd"),
                    userInfo.Gender,
                    userInfo.Email,
                    encryptedPassword,
                    userInfo.Phone,
                    userInfo.UserType,
                    userInfo.UserStatus,
                    msg_code,
                    msg
                );

                res[0] = result[12].Value.ToString();
                res[1] = result[13].Value.ToString();
                //  string decryptedPassword = objEncrypt.DecryptString(encryptedPassword, "");


                return res;
            }
            catch (Exception errorException)
            {
                throw errorException;
            }
            return res;
        }

        public string[] UserLogIn(LoginInfo loginInfo, ref string replay)
        {
            string[] res = new string[4];

            string msg_code = string.Empty;
            string msg = string.Empty;

            try
            {
                string encryptedPassword = objEncrypt.EncryptString(loginInfo.Password, "");

                objSqlProcManager = new SqlProcedureManager(connectionString);
                string spname = "sp_User_Login";

                var result = objSqlProcManager.ExecuteNonQueryParam(spname, ref replay,
                    loginInfo.Email,
                    encryptedPassword,
                    loginInfo.UserType,
                    msg_code,
                    msg
                );

                res[0] = result[3].Value.ToString();
                res[1] = result[4].Value.ToString();
                string decryptedPassword = objEncrypt.DecryptString(encryptedPassword, "");

                return res;
            }
            catch (Exception errorException)
            {
                throw errorException;
            }
            return res;
        }

        public DataTable GetUserInfo(string email, ref string replay)
        {
            string msg_code = string.Empty;
            string msg = string.Empty;
            DataTable dTable = new DataTable();
            try
            {

                objSqlProcManager = new SqlProcedureManager(connectionString);
                dTable = objSqlProcManager.ExecuteDataTable("sp_Get_User_Info", ref replay,
                    email,
                    msg_code,
                    msg);


            }
            catch (Exception errorException)
            {
                throw errorException;
            }
            return dTable;
        }

        #endregion Security

        #region UserAccount
        public string[] SaveUserAddress(UserAddress userAddress, ref string replay)
        {
            string[] res = new string[4];

            string msg_code = string.Empty;
            string msg = string.Empty;

            try
            {
                objSqlProcManager = new SqlProcedureManager(connectionString);
                string spname = "sp_User_Address_Insert_Update";

                var result = objSqlProcManager.ExecuteNonQuery(spname, ref replay,
                    0,
                    userAddress.UserId,
                    userAddress.AddressTitle,
                    userAddress.StreetAddress,
                    userAddress.City,
                    userAddress.PostCode,
                    userAddress.Country,
                    userAddress.Latitude,
                    userAddress.Longitude,

                    "Y"
                );

                if(result>0)
                {
                    res[0] = "Y";
                    res[1] = "Address saved successfully.";
                }
                else
                {
                    res[0] = "N";
                    res[1] = "Failed to save address.";
                }
                

                return res;
            }
            catch (Exception errorException)
            {
                throw errorException;
            }
            return res;
        }
        
        public string[] UpdateUserAddress(UserAddress userAddress, ref string replay)
        {
            string[] res = new string[4];

            string msg_code = string.Empty;
            string msg = string.Empty;

            try
            {

                objSqlProcManager = new SqlProcedureManager(connectionString);
                string spname = "sp_User_Address_Insert_Update";

              
                var result = objSqlProcManager.ExecuteNonQuery(spname, ref replay,
                    userAddress.AddressId,
                   userAddress.UserId,
                   userAddress.AddressTitle,
                   userAddress.StreetAddress,
                   userAddress.City,
                   userAddress.PostCode,
                   userAddress.Country,
                    userAddress.Latitude,
                    userAddress.Longitude,
                   "Y"
               );

                if (result > 0)
                {
                    res[0] = "Y";
                    res[1] = "Address successfully updated.";
                }
                else
                {
                    res[0] = "N";
                    res[1] = "Failed to update address.";
                }

                return res;
            }
            catch (Exception errorException)
            {
                throw errorException;
            }
            return res;
        }
        public DataTable GetUserAddress(string userid, ref string replay)
        {
            string msg_code = string.Empty;
            string msg = string.Empty;
            DataTable dTable = new DataTable();
            try
            {

                objSqlProcManager = new SqlProcedureManager(connectionString);
                dTable = objSqlProcManager.ExecuteDataTable("sp_Get_User_Addreess", ref replay,
                    userid);

            }
            catch (Exception errorException)
            {
                throw errorException;
            }
            return dTable;
        }

        public DataTable GetAllUsers(ref string replay)
        {
            string msg_code = string.Empty;
            string msg = string.Empty;
            DataTable dTable = new DataTable();
            try
            {

                objSqlProcManager = new SqlProcedureManager(connectionString);
                dTable = objSqlProcManager.ExecuteDataTable("sp_Get_All_Users", ref replay
                   );


            }
            catch (Exception errorException)
            {
                throw errorException;
            }
            return dTable;
        }

        public DataTable GetMenuItems(ref string replay)
        {
            string msg_code = string.Empty;
            string msg = string.Empty;
            DataTable dTable = new DataTable();
            try
            {

                objSqlProcManager = new SqlProcedureManager(connectionString);
                dTable = objSqlProcManager.ExecuteDataTable("sp_Get_All_Users", ref replay
                   );


            }
            catch (Exception errorException)
            {
                throw errorException;
            }
            return dTable;
        }


        #endregion User Account

        #region Order

        public DataTable GetTagList(string dayName, ref string replay)
        {
            string msg_code = string.Empty;
            string msg = string.Empty; 
            DataTable dTable = new DataTable();
            try
            {

                objSqlProcManager = new SqlProcedureManager(connectionString);
                dTable = objSqlProcManager.ExecuteDataTable("sp_Get_TagList", ref replay,
                    dayName);

            }
            catch (Exception errorException)
            {
                throw errorException;
            }
            return dTable;
        }

        public DataTable GetMenuDetails(string dayName, string tagName, ref string replay)
        {
            string msg_code = string.Empty;
            string msg = string.Empty;
            DataTable dTable = new DataTable();
            try
            {

                objSqlProcManager = new SqlProcedureManager(connectionString);
                dTable = objSqlProcManager.ExecuteDataTable("sp_Get_MenuItems_By_Day_Tag", ref replay,
                    dayName,
                    tagName
                   );

            }
            catch (Exception errorException)
            {
                throw errorException;
            }
            return dTable;
        }

        public string[] PlaceCustomerOrder(PlaceCustomerOrder objOrder, ref string replay) 
        {
            string[] res = new string[4];
             
            string msg_code = string.Empty;
            string msg = string.Empty;

            try
            {
                objSqlProcManager = new SqlProcedureManager(connectionString);
                string spname = "sp_Create_Order";

                var result = objSqlProcManager.ExecuteNonQuery(spname, ref replay,
                        objOrder.MenuId,
                        objOrder.CustomerId,
                        objOrder.DeleveryDate,
                        objOrder.DeleveryDayName,
                        objOrder.Quantity,
                        objOrder.AddressId,
                        objOrder.DeleveryAddress,
                        objOrder.ReceiverName,
                        objOrder.UserRemarks
                );

                if (result > 0)
                {
                    res[0] = "Y";
                    res[1] = "Order placed successfully.";
                }
                else
                {
                    res[0] = "N";
                    res[1] = "Failed to placed order.";
                }


                return res;
            }
            catch (Exception errorException)
            {
                throw errorException;
            }
            return res;
        }


        #endregion Order

        #region Messages

        public string[] SaveMessage(UserToVendorMessage newMessage, ref string replay)
        {
            string[] res = new string[4];

            string msg_code = string.Empty;
            string msg = string.Empty;

            try
            {
                objSqlProcManager = new SqlProcedureManager(connectionString);
                string spname = "sp_Save_a_Message";

                var result = objSqlProcManager.ExecuteNonQuery(spname, ref replay,
                    newMessage.SenderId,
                    newMessage.ReceiverId,
                    newMessage.MessageDetails,
                    "",
                    "Y"
                );

                if (result > 0)
                {
                    res[0] = "Y";
                    res[1] = "Message sent.";
                }
                else
                {
                    res[0] = "N";
                    res[1] = "Failed to send message.";
                }

                //  string decryptedPassword = objEncrypt.DecryptString(encryptedPassword, "");

                return res;
            }
            catch (Exception errorException)
            {
                throw errorException;
            }
            return res;
        }

        public DataTable GetMyMessages(ChatParameters objChatParameters, ref string replay)
        {
            string msg_code = string.Empty;
            string msg = string.Empty;
            DataTable dTable = new DataTable();
            try
            {

                objSqlProcManager = new SqlProcedureManager(connectionString);
                dTable = objSqlProcManager.ExecuteDataTable("sp_Get_My_Messages", ref replay,
                    objChatParameters.SenderId,
                    objChatParameters.ReceiverId

                   );

            }
            catch (Exception errorException)
            {
                throw errorException;
            }
            return dTable;
        }

        #endregion Messages

    }
}
