using ChatApp.Core.Models;
using ChatApp.Core.Models.ApiModel;
using ChatApp.Core.Models.Entity;
using ChatApp.Core.Models.ViewModel;
using ChatApp.Core.Resources;
using System.Collections.Generic;

namespace ChatApp.Core.Infrastructure.DataProvider
{
    public class LoginDataProvider : BaseDataProvider, ILoginDataProvider
    {
        /// <summary>
        /// Database connection on class intializes using PetaPoco
        /// </summary>
        public LoginDataProvider() : base(ConfigSettings.ConnectionStringName)
        {
        }

        /// <summary>
        /// User authenticate based on entered credentials and generate token for further request authorization
        /// </summary>
        public ApiResponse AuthenticateUser(LoginModel requestModel)
        {
            ApiResponse response = new ApiResponse();

            var userModel = GetEntity<User>(new List<SearchValueData>
            {
                new SearchValueData("UserName", requestModel.UserName),
                new SearchValueData("Password", requestModel.Password),
            });

            if (userModel != null)
            {
                TokenModel tokenModel = new TokenModel();
                tokenModel.UserId = userModel.UserId;
                tokenModel.UserName = userModel.UserName;
                tokenModel.Token = ApiHelper.GenerateJwtToken(tokenModel.UserName);

                response = Common.GenerateResponse(Resource.Success);
                response.Data = tokenModel;
            }
            else
            {
                response = Common.GenerateResponse(Resource.UserNotFound, StatusCodes.StatusCode1, false);
            }

            return response;
        }
    }
}
