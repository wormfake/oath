using System.Net;

namespace Hotmail_Get_Oauth2
{
    public static class Google
    {
        public static bool Online(string proxy = null)
        {
            DateTime dateTime = Get(proxy);
            if (dateTime.Year >= 2050)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public static DateTime Get(string proxy = null)
        {
            HttpClientHandler handler = new HttpClientHandler();
            if(proxy != null)
            {
                handler.Proxy = new WebProxy($"{proxy}", true);
                handler.UseProxy = true;
                handler.Proxy.Credentials = CredentialCache.DefaultNetworkCredentials;
                handler.Credentials = CredentialCache.DefaultNetworkCredentials;
                handler.UseDefaultCredentials = true;
            }
            handler.AllowAutoRedirect = true;
            HttpClient http = new HttpClient();
            try
            {
                http.Timeout = new TimeSpan(0, 0, 10);
                http.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/123.0.0.0 Safari/537.36");
                var rd = http.GetAsync("https://www.google.com.vn/").GetAwaiter().GetResult();
                if (rd.Headers != null && rd.Headers.Date != null)
                {
                    TimeSpan offset = TimeSpan.FromHours(7);
                    DateTime dateTime = rd.Headers.Date.Value.ToOffset(offset).DateTime;
                    rd.Dispose();
                    http.Dispose();
                    return dateTime;
                }
                rd.Dispose();
                http.Dispose();
            }
            catch { }
            http.Dispose();
            return new DateTime(2050, 5, 10, 1, 1, 1);
        }
    }
}
