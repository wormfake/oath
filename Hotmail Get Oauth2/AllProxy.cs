using Leaf.xNet;
using Newtonsoft.Json;
using System;
using System.Text.RegularExpressions;

namespace Hotmail_Get_Oauth2
{
    public class GetTMProxy
    {
        private static readonly string url = "https://tmproxy.com/api/proxy";
        public static int id_location = 1;
        public GetNewTmProxy Stats(string key)
        {
            try
            {
                using (HttpRequest httpClient = new HttpRequest())
                {
                    try
                    {
                        httpClient.ConnectTimeout = 5000;
                        httpClient.IgnoreProtocolErrors = true;
                        var typedTemp = new
                        {
                            api_key = key
                        };
                        string param = JsonConvert.SerializeObject(typedTemp);
                        string result = PostDataJson(httpClient, url + "/stats", param);
                        httpClient.Dispose();
                        GetNewTmProxy checkTM = JsonConvert.DeserializeObject<GetNewTmProxy>(result);
                        return checkTM;
                    }
                    catch (Exception p)
                    {
                        httpClient.Dispose();
                        throw new Exception(p.Message);
                    }
                }
            }
            catch { }
            return null;
        }

        public GetNewTmProxy GetCurrentProxy(string key)
        {
            try
            {
                using (HttpRequest httpClient = new HttpRequest())
                {
                    try
                    {
                        httpClient.ConnectTimeout = 5000;
                        httpClient.IgnoreProtocolErrors = true;
                        var typedTemp = new
                        {
                            api_key = key
                        };

                        string param = JsonConvert.SerializeObject(typedTemp);
                        string result = PostDataJson(httpClient, url + "/get-current-proxy", param);
                        httpClient.Dispose();
                        GetNewTmProxy checkTM = JsonConvert.DeserializeObject<GetNewTmProxy>(result);
                        return checkTM;
                    }
                    catch (Exception p)
                    {
                        httpClient.Dispose();
                        throw new Exception(p.Message);
                    }
                }
            }
            catch { }
            return null;
        }

        public string GetNewProxy(string key)
        {
            try
            {
                using (HttpRequest httpClient = new HttpRequest())
                {
                    try
                    {
                        httpClient.ConnectTimeout = 5000;
                        httpClient.IgnoreProtocolErrors = true;
                        var typedTemp = new
                        {
                            api_key = key,
                            id_location = id_location,
                            id_isp = 0
                        };
                        string param = JsonConvert.SerializeObject(typedTemp);
                        string result = PostDataJson(httpClient, url + "/get-new-proxy", param);
                        httpClient.Dispose();
                        GetNewTmProxy TM = JsonConvert.DeserializeObject<GetNewTmProxy>(result);
                        if (TM.code == 0)
                        {
                            return TM.data.https;
                        }
                        else if (TM.code == 6)
                        {
                            return "expired";
                        }
                        else if (TM.code == 5)
                        {
                            try
                            {
                                var llly = Regex.Match(TM.message, @"(\d+)");
                                int hhhh = Convert.ToInt32(llly.Value) + 1;
                                return hhhh.ToString();
                            }
                            catch { }
                        }
                    }
                    catch (Exception p)
                    {
                        httpClient.Dispose();
                        throw new Exception(p.Message);
                    }
                }
            }
            catch { }
            return "6";
        }
        private string PostDataJson(HttpRequest httpClient, string url, string data = null)
        {
            string html = PostURI(new Uri(url), data, httpClient);
            return html;
        }
        private string PostURI(Uri u, string c, HttpRequest http)
        {
            var result = http.Post(u, c, "application/json");
            if (result.StatusCode == HttpStatusCode.OK)
            {
                return result.ToString();
            }
            return "";
        }
    }
    public class wwProxy
    {
        private static readonly string urlstt = "https://wwproxy.com/api/client/key/detail?user_api_key={0}&key={1}";
        private static readonly string urlgetnew = "https://wwproxy.com/api/client/proxy/available?key={0}&provinceId=-1";
        public STTWWProxy GetStatusKey(string key, string token)
        {
            try
            {
                using (HttpRequest http = new HttpRequest())
                {
                    try
                    {
                        http.ConnectTimeout = 5000;
                        http.IgnoreProtocolErrors = true;
                        string url = string.Format(urlstt, token, key);
                        var res = http.Get(url);
                        string lkoi = res.ToString();
                        http.Dispose();
                        STTWWProxy sTTWW = JsonConvert.DeserializeObject<STTWWProxy>(lkoi);
                        return sTTWW;
                    }
                    catch (Exception p)
                    {
                        http.Dispose();
                        throw new Exception(p.Message);
                    }
                }
            }
            catch
            {
                return null;
            }
        }
        public string GetNewProxy(string key)
        {
            try
            {
                using (HttpRequest http = new HttpRequest())
                {
                    http.ConnectTimeout = 5000;
                    http.IgnoreProtocolErrors = true;
                    string url = string.Format(urlgetnew, key);
                    var res = http.Get(url);
                    string lkoi = res.ToString();
                    http.Dispose();
                    STTWWProxy sTTWW = JsonConvert.DeserializeObject<STTWWProxy>(lkoi);
                    if (sTTWW != null)
                    {
                        if (sTTWW.errorCode == 0 && sTTWW.data != null && sTTWW.data.proxy != null && sTTWW.data.proxy.Contains(":"))
                        {
                            return sTTWW.data.proxy;
                        }
                        if (sTTWW.message != null)
                        {
                            if (sTTWW.message.ToLower().Contains("hết hạn"))
                            {
                                return "expired";
                            }
                            try
                            {
                                var llly = Regex.Match(sTTWW.message, @"(\d+)");
                                int hhhh = Convert.ToInt32(llly.Value) + 1;
                                return hhhh.ToString();
                            }
                            catch { }
                        }
                    }
                }
            }
            catch { }
            return "6";
        }
    }
    public class Kiotproxy
    {
        private static readonly string urlgetnew = "https://api.kiotproxy.com/api/v1/proxies/new";
        public string GetNewProxy(string key)
        {
            try
            {
                using (HttpRequest http = new HttpRequest())
                {
                    try
                    {
                        http.ConnectTimeout = 5000;
                        http.IgnoreProtocolErrors = true;
                        http["Accept"] = "*/*";
                        var res = http.Get(urlgetnew + $"?key={key}&region=random");
                        string lkoi = res.ToString();
                        http.Dispose();
                        STTKiotproxy sTTWW = JsonConvert.DeserializeObject<STTKiotproxy>(lkoi);
                        if (sTTWW != null && !sTTWW.success && sTTWW.error != null)
                        {
                            if (sTTWW.error.Contains("KEY_NOT_FOUND"))
                            {
                                return "expired";
                            }
                            else if (sTTWW.error.ToLower().Contains("expire"))
                            {
                                return "expired";
                            }
                        }
                        else if (sTTWW.data != null && sTTWW.data.http != null && sTTWW.data.http.Contains("."))
                        {
                            if (sTTWW.data.proxyUser != null && sTTWW.data.proxyUser != "")
                            {
                                return $"{sTTWW.data.host}:{sTTWW.data.httpPort}:{sTTWW.data.proxyUser}:{sTTWW.data.proxyPass}";
                            }
                            else
                            {
                                return $"{sTTWW.data.host}:{sTTWW.data.httpPort}";
                            }
                        }
                        else
                        {
                            try
                            {
                                var llly = Regex.Match(sTTWW.message, @"(\d+)");
                                int hhhh = Convert.ToInt32(llly.Value) + 1;
                                return hhhh.ToString();
                            }
                            catch { }
                        }

                    }
                    catch (Exception p)
                    {
                        http.Dispose();
                        throw new Exception(p.Message);
                    }
                }
            }
            catch { }
            return "6";
        }
    }
    public class Topproxy
    {
        private static readonly string urlgetnew = "https://proxyxoay.org/api/get.php";
        public string GetNewProxy(string key)
        {
            try
            {
                using (HttpRequest http = new HttpRequest())
                {
                    try
                    {
                        http.ConnectTimeout = 5000;
                        http.IgnoreProtocolErrors = true;
                        http["Accept"] = "*/*";
                        var res = http.Get(urlgetnew + $"?key={key}&nhamang=Random&tinhthanh=0");
                        string lkoi = res.ToString();
                        http.Dispose();
                        STTopProxy sTTWW = JsonConvert.DeserializeObject<STTopProxy>(lkoi);
                        if (sTTWW != null)
                        {
                            if (sTTWW.status == 102)
                            {
                                return "expired";
                            }
                            else if (sTTWW.status == 101)
                            {
                                try
                                {
                                    var llly = Regex.Match(sTTWW.message, @"(\d+)");
                                    int hhhh = Convert.ToInt32(llly.Value) + 1;
                                    return hhhh.ToString();
                                }
                                catch { }
                            }
                            else if (sTTWW.status == 100)
                            {
                                return sTTWW.proxyhttp;
                            }
                            else
                            {
                                try
                                {
                                    var llly = Regex.Match(sTTWW.message, @"(\d+)");
                                    int hhhh = Convert.ToInt32(llly.Value) + 1;
                                    return hhhh.ToString();
                                }
                                catch { }
                            }
                        }
                    }
                    catch (Exception p)
                    {
                        http.Dispose();
                        throw new Exception(p.Message);
                    }
                }
            }
            catch { }
            return "6";
        }
    }
    public class STTopProxy
    {
        public int status { set; get; }
        public string proxyhttp { set; get; }
        public string message { set; get; }
        public string proxysocks5 { set; get; }

        [JsonProperty("Nha Mang")]
        public string Nha_Mang { set; get; }
        [JsonProperty("Vi Tri")]
        public string Vi_Tri { set; get; }
        [JsonProperty("Token expiration date")]
        public string Token_expiration_date { set; get; }
    }
    public class STTWWProxy
    {
        public int errorCode { set; get; }
        public string status { set; get; }
        public string message { set; get; }
        public string currentTime { set; get; }
        public STT1WWProxy data { set; get; } = new STT1WWProxy();
    }
    public class STT1WWProxy
    {
        public object vip { set; get; }
        public object provinceId { set; get; }
        public object port { set; get; }
        public object uuid { set; get; }
        public object alias { set; get; }
        public bool expiredFlag { set; get; } = false;
        public string status { set; get; }
        public string ipAddress { set; get; }
        public string proxy { set; get; }
        public string expiredTime { set; get; }
    }
    public class STTKiotproxy
    {
        public int code { set; get; }
        public string status { set; get; }
        public string message { set; get; }
        public string error { set; get; }
        public object timestamp { set; get; }
        public bool success { set; get; } = false;
        public bool expiredFlag { set; get; } = false;
        public KiotproxyData data { set; get; } = new KiotproxyData();
    }
    public class KiotproxyData
    {
        public int ttl { set; get; }
        public int ttc { set; get; }
        public string proxyUser { set; get; }
        public string proxyPass { set; get; }
        public string host { set; get; }
        public int httpPort { set; get; }
        public string location { set; get; }
        public object nextRequestAt { set; get; }
        public object expirationAt { set; get; }
        public string http { set; get; }
        public string realIpAddress { set; get; }
    }
    public class GetNewTmProxy
    {
        public int code { set; get; }
        public string message { set; get; }
        public DataGetNewTmProxy data { set; get; }
    }
    public class DataGetNewTmProxy
    {
        public string ip_allow { set; get; }
        public string isp_name { set; get; }
        public string location_name { set; get; }
        public string socks5 { set; get; }
        public string https { set; get; }
        public int timeout { set; get; }
        public int next_request { set; get; } = 0;
        public int base_next_request { set; get; } = 0;
        public string expired_at { set; get; }
    }
    public class StatusTmProxy
    {
        public int code { set; get; }
        public string message { set; get; }
        public DataStatusTmProxy data { set; get; }
    }
    public class DataStatusTmProxy
    {
        public int id { set; get; }
        public string plan { set; get; }
        public string price_per_day { set; get; }
        public string api_key { set; get; }
        public int timeout { set; get; }
        public int base_next_request { set; get; }
        public string expired_at { set; get; }
        public string note { set; get; }
        public string max_ip_per_day { set; get; }
        public string ip_used_today { set; get; }
    }
}
