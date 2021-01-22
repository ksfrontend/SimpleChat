using Newtonsoft.Json;
using System.Net;
using System.Web.Http.Controllers;
using System.Net.Http;
using System.Web.Http;
using System.Web;
using ChatApp.Core.Models.ApiModel;

namespace ChatApp.Core.Infrastructure
{
    public class Common
    {
        #region Serialize/Deserialize Object

        public static string SerializeObject<T>(T objectData)
        {
            string defaultJson = JsonConvert.SerializeObject(objectData);
            return defaultJson;
        }

        public static T DeserializeObject<T>(string json)
        {
            T obj = default(T);
            obj = JsonConvert.DeserializeObject<T>(json, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            return obj;
        }

        #endregion

        #region Generate Response

        public static ApiResponse GenerateResponse(string message = "", string statusCode = "200", bool isSuccess = true)
        {
            ApiResponse response = new ApiResponse();
            response.Message = message;
            response.Code = statusCode;
            response.IsSuccess = isSuccess;
            return response;
        }

        public static void SendApiResponse(HttpActionContext actionContext, ApiResponse response, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        {
            var resp = actionContext.Request.CreateResponse(
                statusCode,
                response,
                actionContext.ControllerContext.Configuration.Formatters.JsonFormatter
            );

            if (statusCode == HttpStatusCode.Unauthorized)
            {
                HttpContext.Current.Response.Headers.Add("WWW-Authenticate", "Token");
            }

            actionContext.Response = resp;
        }

        public static void BadRequest(ApiResponse response, HttpActionContext actionContext)
        {
            actionContext.Response = actionContext.Request.CreateResponse(
                HttpStatusCode.NotAcceptable,
                response,
                actionContext.ControllerContext.Configuration.Formatters.JsonFormatter
                );
        }

        public static void ThrowErrorMessage(string Message, HttpStatusCode code = HttpStatusCode.NotFound)
        {
            var resp = new HttpResponseMessage(code)
            {
                Content = new StringContent(Message),
                ReasonPhrase = Message,
            };
            throw new HttpResponseException(resp);
        }

        #endregion
    }
}
