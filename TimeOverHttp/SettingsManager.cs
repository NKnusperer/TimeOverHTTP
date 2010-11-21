using System;
using System.Configuration;

namespace TimeOverHttp
{
    class SettingsManager
    {
        static Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        public static string HttpServer
        {
            get { return ConfigurationManager.AppSettings["HttpServer"]; }
            set
            {
                config.AppSettings.Settings["HttpServer"].Value = value;
                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("appSettings");
            }
        }

        public static string SyncEveryMinutes
        {
            get { return ConfigurationManager.AppSettings["SyncEveryMinutes"]; }
            set
            {
                config.AppSettings.Settings["SyncEveryMinutes"].Value = value;
                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("appSettings");
            }
        }

        public static string Autostart
        {
            get { return ConfigurationManager.AppSettings["Autostart"]; }
            set
            {
                config.AppSettings.Settings["Autostart"].Value = value;
                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("appSettings");
            }
        }

        public static string UseProxyServer
        {
            get { return ConfigurationManager.AppSettings["UseProxyServer"]; }
            set
            {
                config.AppSettings.Settings["UseProxyServer"].Value = value;
                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("appSettings");
            }
        }

        public static string ProxyServer
        {
            get { return ConfigurationManager.AppSettings["ProxyServer"]; }
            set
            {
                config.AppSettings.Settings["ProxyServer"].Value = value;
                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("appSettings");
            }
        }

        public static string ProxyPort
        {
            get { return ConfigurationManager.AppSettings["ProxyPort"]; }
            set
            {
                config.AppSettings.Settings["ProxyPort"].Value = value;
                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("appSettings");
            }
        }

        public static string ProxyUsername
        {
            get { return ConfigurationManager.AppSettings["ProxyUsername"]; }
            set
            {
                config.AppSettings.Settings["ProxyUsername"].Value = value;
                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("appSettings");
            }
        }

        public static string ProxyPasswort
        {
            get { return ConfigurationManager.AppSettings["ProxyPasswort"]; }
            set
            {
                config.AppSettings.Settings["ProxyPasswort"].Value = value;
                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("appSettings");
            }
        }

        public static string ProxyDomain
        {
            get { return ConfigurationManager.AppSettings["ProxyDomain"]; }
            set
            {
                config.AppSettings.Settings["ProxyDomain"].Value = value;
                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("appSettings");
            }
        }
    }
}
