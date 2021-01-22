using ChatApp.Core.Models.ApiModel;

namespace ChatApp.Core.Infrastructure.DataProvider
{
    public interface ITokenDataProvider
    {
        ApiResponse AuthenticateUserName(string userName);
    }
}
