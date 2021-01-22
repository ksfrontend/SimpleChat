using ChatApp.Core.Infrastructure;
using ChatApp.Core.Infrastructure.Attributes;
using ChatApp.Core.Infrastructure.DataProvider;
using ChatApp.Core.Models.ApiModel;
using ChatApp.Core.Models.ViewModel;
using System.Web.Http;

namespace ChatApp.WebAPI.ApiControllers
{
    public class LoginController : BaseController
    {
        ILoginDataProvider _loginDataProvider;

        /// <summary>
        /// User authenticate based on entered credentials and generate token for further request authorization
        /// </summary>
        /// <param name="request">
        ///     UserName: UserName
        ///     Password: Password
        /// </param>
        /// <returns>OnSuccess: Returns Token & User details</returns>
        /// <returns>OnFail: Return with failure message</returns>
        [HttpPost]
        [ApiAuthorizationFilter(Permissions = Constants.AnonymousPermission)]
        public ApiResponse Authenticate(LoginModel request)
        {
            _loginDataProvider = new LoginDataProvider();

            ApiResponse response = new ApiResponse();
            return _loginDataProvider.AuthenticateUser(request);
        }
    }
}