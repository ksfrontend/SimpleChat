using ChatApp.Core.Models;
using ChatApp.Core.Models.ApiModel;
using ChatApp.Core.Models.Entity;
using ChatApp.Core.Models.ViewModel;
using ChatApp.Core.Resources;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ChatApp.Core.Infrastructure.DataProvider
{
    public class UserDataProvider : BaseDataProvider, IUserDataProvider
    {
        public UserDataProvider() : base(ConfigSettings.ConnectionStringName)
        {
        }

        /// <summary>
        /// Get List of active users.
        /// </summary>
        /// <returns></returns>
        public ApiResponse GetUserList()
        {
            ApiResponse response = new ApiResponse();
            var userList = GetEntityList<User>(new List<SearchValueData>
            {
                new SearchValueData { Name = "IsActive", Value = "1"}
            });

            userList.ForEach(q => q.Password = string.Empty);

            response = Common.GenerateResponse(Resource.Success, StatusCodes.StatusCode200, true);
            response.Data = userList;
            return response;
        }

        /// <summary>
        /// Save user chat details
        /// </summary>
        /// <param name="requestModel">
        ///     SenderId: LoggedIn UserId
        ///     ReceiverId: Selected User's UserId from chat box.
        ///     TextMessage: Entered text messsage
        /// </param>
        /// <returns>Return success/fail response of status</returns>
        public ApiResponse SaveUserChat(MessageViewModel requestModel)
        {
            ApiResponse response = new ApiResponse();

            var dbMessage = new Message();
            dbMessage.SenderId = requestModel.SenderId;
            dbMessage.ReceiverId = requestModel.ReceiverId;
            dbMessage.TextMessage = requestModel.TextMessage;
            dbMessage.IsActive = true;
            dbMessage.CreatedDate = DateTime.Now;

            SaveEntity(dbMessage);

            response = Common.GenerateResponse(Resource.Success, StatusCodes.StatusCode200, true);
            response.Data = dbMessage;
            return response;
        }

        /// <summary>
        /// Get chat conversation list based on the sender and receiver id.
        /// </summary>
        /// <param name="requestModel">
        ///     SenderId: Current loggedin UserId
        ///     ReceiverId: Selected User's UserId from chat box.
        /// </param>
        /// <returns>Return list of conversations</returns>
        public ApiResponse GetChatList(MessageRequestModel requestModel)
        {
            ApiResponse response = new ApiResponse();
            var chatList = GetEntityList<Message>("GetChatList", new List<SearchValueData>
            {
                new SearchValueData { Name = "SenderId", Value = requestModel.SenderId.ToString() },
                new SearchValueData { Name = "ReceiverId", Value = requestModel.ReceiverId.ToString() }
            });

            response = Common.GenerateResponse(Resource.Success, StatusCodes.StatusCode200, true);
            response.Data = chatList.OrderBy(q => q.CreatedDate);
            return response;
        }
    }
}
