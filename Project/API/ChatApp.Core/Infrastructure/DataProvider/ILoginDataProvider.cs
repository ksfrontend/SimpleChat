using ChatApp.Core.Models.ApiModel;
using ChatApp.Core.Models.ViewModel;

namespace ChatApp.Core.Infrastructure.DataProvider
{
    public interface ILoginDataProvider
    {
        ApiResponse AuthenticateUser(LoginModel requestModel);
    }
}
