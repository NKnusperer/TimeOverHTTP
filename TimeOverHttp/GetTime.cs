using System;
using System.Net;
using System.Collections.Generic;

namespace TimeOverHttp
{
    class GetTime
    {
        public DateTime OverHttp()
        {
            Dictionary<string, string> HeaderList = new Dictionary<string, string>();
            WebRequest WebRequestObject = HttpWebRequest.Create(SettingsManager.HttpServer);

            // use proxy server if setted up
            if (SettingsManager.UseProxyServer == "1")
            {
                WebProxy proxy = new WebProxy(SettingsManager.ProxyServer + ":" + SettingsManager.ProxyPort, true);
                NetworkCredential auth = new NetworkCredential(SettingsManager.ProxyUsername, SettingsManager.ProxyPasswort, SettingsManager.ProxyDomain);
                proxy.Credentials = auth;
                WebRequestObject.Proxy = proxy;
            }

            DateTime httpTime = new DateTime();
            try
            {
                WebResponse ResponseObject = WebRequestObject.GetResponse();
                foreach (string HeaderKey in ResponseObject.Headers)
                {
                    if (HeaderKey == "Date")
                    {
                        if (DateTime.TryParse(ResponseObject.Headers[HeaderKey], out httpTime))
                        {
                            httpTime = TimeZone.CurrentTimeZone.ToLocalTime(httpTime);
                        }
                    }
                }
                ResponseObject.Close();

                return httpTime;
            }
            catch(Exception)
            {
                return httpTime;
            }
        }
    }
}
