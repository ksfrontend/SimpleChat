using ChatApp.Core.ChatHelper;
using ChatApp.Core.Infrastructure;
using ChatApp.Core.Infrastructure.Attributes;
using ChatApp.Core.Infrastructure.DataProvider;
using ChatApp.Core.Models.ApiModel;
using ChatApp.Core.Models.Entity;
using ChatApp.Core.Models.ViewModel;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace ChatApp.WebAPI.ApiControllers
{
    public class UserController : BaseController
    {
        IUserDataProvider _userDataProvider;

        /// <summary>
        /// Get list of active users.
        /// </summary>
        /// <returns>Return list of active users</returns>
        [HttpPost]
        [ApiAuthorizationFilter(Permissions = Constants.AuthorizedPermission)]
        public ApiResponse GetUserList()
        {
            _userDataProvider = new UserDataProvider();
            var response = _userDataProvider.GetUserList();
            if (response.IsSuccess)
            {
                var userList = response.Data as List<User>;
                var onlineUsers = ChatHub.GetAllActiveConnections();
                foreach (var item in userList)
                {
                    var a = onlineUsers.FirstOrDefault(x => x.UserId == item.UserId.ToString());
                    item.IsOnline = a != null;
                }
                response.Data = userList;
            }
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
        [HttpPost]
        [ApiAuthorizationFilter(Permissions = Constants.AuthorizedPermission)]
        public ApiResponse GetChatList(MessageRequestModel requestModel)
        {
            _userDataProvider = new UserDataProvider();
            return _userDataProvider.GetChatList(requestModel);
        }
    }
}