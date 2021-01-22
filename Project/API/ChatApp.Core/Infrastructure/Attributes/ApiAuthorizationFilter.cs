using ChatApp.Core.Models.ApiModel;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace ChatApp.Core.Infrastructure.Attributes
{
    /// <summary>
    /// ActionFilter for authorized every API request based on permission. Currently, we're only autorized AuthorizedPermission.
    /// </summary>
    public class ApiAuthorizationFilter : ActionFilterAttribute
    {
        public string Permissions { get; set; }
        public string[] PermissionList { get { return string.IsNullOrEmpty(Permissions) ? null : Permissions.Split(','); } set { Permissions = (value != null) ? string.Join(",", value) : ""; } }

        /// <summary>
        /// Called before the action method is invoked.
        /// </summary>
        /// <param name="actionContext"></param>
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (CheckAllowedActions())
            {
                base.OnActionExecuting(actionContext);
                return;
            }

            //Check for the User is authorized or not based on the JWT Header Token.
            if (!ApiHelper.IsAuthorizedUser())
            {
                Common.SendApiResponse(actionContext,
                    new ApiResponse
                    {
                        IsSuccess = false,
                        Message = string.Format("'{0}' Header is not passed or invalid.", Constants.KeyHeaderName)
                    }, HttpStatusCode.Unauthorized);
            }
            else
            {
                base.OnActionExecuting(actionContext);
                return;
            }
        }

        /// <summary>
        /// Allow actions based on the Permissions.
        /// </summary>
        /// <returns></returns>
        private bool CheckAllowedActions()
        {
            string[] strPermissions = string.IsNullOrEmpty(Permissions) ? new string[] { } : Permissions.Split(',');

            if (strPermissions.Contains(Constants.AnonymousPermission))
                return true;

            if (!strPermissions.Any())
                return true;

            return false;
        }
    }
}
