using ChatApp.Core.Infrastructure;
using ChatApp.Core.Infrastructure.Attributes;
using ChatApp.Core.Models.ApiModel;
using System.Web.Http;

namespace ChatApp.WebAPI.ApiControllers
{
    public class BaseController : ApiController
    {

        //Test Get API call
        [HttpGet]
        public ServiceResponse Ping()
        {
            return new ServiceResponse
            {
                IsSuccess = true,
                Message = "Hey! It's working. You're running ChatAPP project."
            };
        }
    }
}