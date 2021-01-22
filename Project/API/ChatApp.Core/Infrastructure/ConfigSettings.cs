using System;
using System.Configuration;

namespace ChatApp.Core.Infrastructure
{
    public class ConfigSettings
    {
        public static readonly string SiteName = ConfigurationManager.AppSettings["SiteName"];
        public static readonly string SiteBaseUrl = ConfigurationManager.AppSettings["SiteBaseUrl"];

        public static readonly string ConnectionStringName = ConfigurationManager.AppSettings["ConnectionStringName"];
    }
}
