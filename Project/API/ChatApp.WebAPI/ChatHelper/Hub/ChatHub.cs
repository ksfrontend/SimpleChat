using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;
using System.Xml;
using Microsoft.AspNet.SignalR.Hubs;
using ChatApp.Core.Models.ViewModel;
using ChatApp.Core.Infrastructure.DataProvider;
using ChatApp.Core.Models.Entity;

namespace ChatApp.Core.ChatHelper
{
    [HubName("chatHub")]
    public class ChatHub : Hub
    {
        #region 

        static HashSet<OnlineUsersList> CurrentConnections = new HashSet<OnlineUsersList>();
        IUserDataProvider _userDataProvider = new UserDataProvider();

        #endregion
        public override System.Threading.Tasks.Task OnConnected()
        {
            string clientId = Context.ConnectionId;
            string data = clientId;
            string count = "";
            try
            {
                count = "0";
                CurrentConnections.Add(new OnlineUsersList { ConnectionId = Context.ConnectionId, UserId = Context.QueryString["UserId"] });
            }
            catch (Exception d)
            {
                count = d.Message;
            }
            Clients.Caller.receiveMessage("ChatHub", data, count);

            return base.OnConnected();
        }

        [HubMethodName("hubconnect")]
        public void Get_Connect(String fromid, String toUSerid)
        {
            string count = "";
            string msg = "";
            string list = "";
            try
            {
                count = "0";
                msg = "saved";
                list = toUSerid;
            }
            catch (Exception d)
            {
                msg = "DB Error " + d.Message;
            }
            var id = Context.ConnectionId;
            string[] Exceptional = new string[1];
            Exceptional[0] = id;
            // Clients.Caller.receiveMessage("RU", msg, list);
            //Clients.AllExcept(Exceptional).receiveMessage("NewConnection", fromid + " " + id, count);
        }

        [HubMethodName("privatemessage")]
        public void Send_PrivateMessage(MessageViewModel model)
        {
            var id = Context.ConnectionId;
            Clients.Caller.receiveMessage(model.SenderId.ToString(), model.TextMessage, model.ReceiverId.ToString());
            var chatsaveresponse = _userDataProvider.SaveUserChat(model);
            if (chatsaveresponse.IsSuccess)
            {
                var message = (Message)chatsaveresponse.Data;
                var usersConnectionIds = CurrentConnections.Where(x => x.UserId == model.ReceiverId.ToString() || x.UserId == model.SenderId.ToString()).Select(m => m.ConnectionId).ToList();
                if (usersConnectionIds.Count > 0)
                {
                    Clients.Clients(usersConnectionIds).receiveMessage(message);
                }
            }
        }

        //return list of all active connections
        public static List<OnlineUsersList> GetAllActiveConnections()
        {
            return CurrentConnections.ToList();
        }

        public override System.Threading.Tasks.Task OnDisconnected(bool stopCalled)
        {
            string count = "";
            string msg = "";
            string clientId = Context.ConnectionId;
            try
            {
                CurrentConnections.RemoveWhere(m => m.ConnectionId == clientId);
                //foreach (var item in CurrentConnections)
                //{
                //    if (item.ConnectionId == Context.ConnectionId)
                //    {
                //        CurrentConnections.Remove(item);
                //    }
                //}
            }
            catch (Exception d)
            {
                msg = "DB Error " + d.Message;
            }
            string[] Exceptional = new string[1];
            Exceptional[0] = clientId;
            //Clients.AllExcept(Exceptional).receiveMessage("NewConnection", clientId + " leave", count);
            return base.OnDisconnected(stopCalled);
        }
    }

    public class OnlineUsersList
    {
        public string UserId { get; set; }
        public string ConnectionId { get; set; }
    }

}
