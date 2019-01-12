using ApiManager.ApiModels;
using ApiManager.Models;
using ApiManager.Repository;
using BanglaKhabarWebApp.Models;
using BanglaKhabarWebApp.Models.RequestModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BanglaKhabarWebApp.Controllers
{
    [RoutePrefix("api/Message")]
    public class MessageController : ApiController
    {
       
        private ApiRepository apiRepository = new ApiRepository();

        [HttpPost]
        [Route("SendMessage")]
        public ResponseMessage SendMessage(UserToVendorMessage newMessage)
        {
            string reply = string.Empty;
            ResponseMessage rM = new ResponseMessage();
            try
            {
                var result = apiRepository.SaveMessage(newMessage, ref reply);
                if (result[0] == "Y")
                {
                    rM.MessageCode = "Y";
                    rM.Message = result[1];
                    rM.SystemMessage = reply;
                    rM.Content = "Message sent successfully.";
                }
                else
                {
                    rM.MessageCode = "N";
                    rM.Message = result[1];
                    rM.SystemMessage = reply;
                    rM.Content = result[1];
                }
            }
            catch (Exception ex)
            {
                rM.MessageCode = "N";
                rM.Message = "System Error";
                rM.SystemMessage = ex.Message;
                rM.Content = "Failed to send message due to server problem.";
                return rM;
            }


            return rM;
        }


        [HttpPost]
        [Route("GetMyMessages")]
        public ResponseMessage GetMyMessages(ChatParameters objChatParameters)
        {

            string reply = string.Empty;
            ResponseMessage rM = new ResponseMessage();
            List<ChatInfo> messageList = new List<ChatInfo>();

            try
            {
                var dt = apiRepository.GetMyMessages(objChatParameters, ref reply);
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        ChatInfo message = new ChatInfo();

                        message.MessageId = dr["MessageId"].ToString().Trim();
                        message.SenderId = dr["SenderId"].ToString().Trim();
                        message.ReceiverId = dr["ReceiverId"].ToString().Trim();
                        message.MessageDetails = dr["MessageDetails"].ToString().Trim();
                        message.MessageTime = dr["MessageTime"].ToString().Trim();

                        messageList.Add(message);
                    }
                    rM.MessageCode = "Y";
                    rM.Message = "";
                    rM.SystemMessage = reply;
                    rM.Content = messageList;
                }
                else
                {
                    rM.MessageCode = "N";
                    rM.Message = "No message found.";
                    rM.SystemMessage = reply;
                    rM.Content = messageList;
                }
                return rM;
            }
            catch (Exception ex)
            {
                rM.MessageCode = "N";
                rM.Message = "System Error";
                rM.SystemMessage = ex.Message;
                rM.Content = messageList;
                return rM;
            }


            return rM;
        }

       

    }
}
