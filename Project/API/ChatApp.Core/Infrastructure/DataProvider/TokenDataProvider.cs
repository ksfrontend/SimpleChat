using ChatApp.Core.Infrastructure;
using ChatApp.Core.Infrastructure.DataProvider;
using ChatApp.Core.Models;
using ChatApp.Core.Models.ApiModel;
using ChatApp.Core.Models.Entity;
using ChatApp.Core.Resources;
using System.Collections.Generic;

namespace ChatApp.Core.Infrastructure.DataProvider
{
    public class TokenDataProvider : BaseDataProvider, ITokenDataProvider
    {
        /// <summary>
        /// Database connection on class intializes using PetaPoco
        /// </summary>
        public TokenDataProvider() : base(ConfigSettings.ConnectionStringName)
        { }


        public ApiResponse AuthenticateUserName(string userName)
        {
            ApiResponse response = new ApiResponse();
            var userModel = GetEntity<User>(new List<SearchValueData> { new SearchValueData("UserName", userName) });

            if (userModel != null)
            {
                response = Common.GenerateResponse(Resource.Success);
                response.Data = userModel;
            }
            else
            {
                response = Common.GenerateResponse(Resource.InvalidToken, StatusCodes.StatusCode403, false);
            }

            return response;
        }
    }
}
