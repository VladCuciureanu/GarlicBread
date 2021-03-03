using System;

namespace GarlicBread
{
    public static class Constants
    {
        public const string ENVIRONMENT_VARIABLE_PREFIX = "GARLICBREAD_";
        public const string ENVIRONMENT_VARNAME = ENVIRONMENT_VARIABLE_PREFIX + "ENVIRONMENT";
        public const string DEFAULT_RUNTIME_ENVIRONMENT = "Development";

        public const string CONFIGURATION_FILENAME = "garlicbread.appsettings.json";

        public const string DEFAULT_GUILD_MESSAGE_PREFIX = "gb ";

        public static Guid SessionId = Guid.NewGuid();
    }
}