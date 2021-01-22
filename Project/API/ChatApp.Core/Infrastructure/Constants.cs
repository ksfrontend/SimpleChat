namespace ChatApp.Core.Infrastructure
{
    public class Constants
    {
        public const string AnonymousPermission = "AnonymousPermission";
        public const string AuthorizedPermission = "AuthorizedPermission";

        public const string Culture_EN = "en-GB";
        public const string DataTypeString = "string";
        public const string DataTypeBoolean = "bool";

        public const int CacheExpiryPeriod = 60; //In minutes

        #region Api Keys

        public const string TokenHeaderName = "Authorization";
        public const string KeyHeaderName = "AccessKey";
        public const string RequestModelName = "request";

        #endregion
    }
}
