using ChatApp.Core.Models.ApiModel;
using ChatApp.Core.Models.ViewModel;

namespace ChatApp.Core.Infrastructure.DataProvider
{
    public interface IUserDataProvider
    {
        ApiResponse GetUserList();
        ApiResponse SaveUserChat(MessageViewModel requestModel);
        ApiResponse GetChatList(MessageRequestModel requestModel);
    }
}
