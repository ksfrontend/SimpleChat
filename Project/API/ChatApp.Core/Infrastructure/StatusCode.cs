
namespace ChatApp.Core.Infrastructure
{
    public class StatusCodes
    {
        /// return false
        /// </summary>
        public static string StatusCode1 = "1";

        /// <summary>
        /// OK. Required for all requests to confirm request is from an authorized party
        /// </summary>
        public static string StatusCode200 = "200";
        /// <summary>
        /// Created. When creating a new resource instance (e.g. user) using POST succeeded.
        /// </summary>
        public static string StatusCode201 = "201";

        /// <summary>
        /// Bad Request. Problem with the request, such as a missing, invalid or type mismatched parameter
        /// </summary>
        public static string StatusCode400 = "400";

        /// <summary>
        /// Unauthorized. Invalid or missing api key.
        /// </summary>
        public static string StatusCode401 = "401";
        /// <summary>
        /// Forbidden. Disabled api key, or you do not have access to this resource
        /// </summary>
        public static string StatusCode403 = "403";

        /// <summary>
        /// Not Found. Requested resource doesn't exist. A request made over HTTP instead of HTTPS will also result in this error.
        /// </summary>
        public static string StatusCode404 = "404";

        /// <summary>
        /// Already Exists. Request could not be completed due to a conflict with the current state of the resource
        /// </summary>
        public static string StatusCode409 = "409";
        /// <summary>
        /// Server Error. An uncaught exception has occurred on the server.
        /// </summary>
        public static string StatusCode500 = "500";

    }
}