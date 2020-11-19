namespace Sigma
{

    /// <summary>
    /// All defaults labels/strings to be used by the USML application.
    /// </summary>
    /// 
    [Documented(false)]
    public interface USMLDefaults {

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        
        public static readonly string USMLCONFIG_FILE = "usml-config.yml";
        public static readonly string LIBRARIES_FOLDER = "libraries";

        public static readonly string CONFIG_DRIVERCLASS_KEY = "driverClass";
        public static readonly string CONFIG_NAME_KEY = "name";
        public static readonly string CONFIG_VERSION_KEY = "version";
        public static readonly string CONFIG_DESCRIPTION_KEY = "description";

#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member


        public static bool CONSOLE_HIDE_EXCEPTION = false;

    }

}