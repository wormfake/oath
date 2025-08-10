using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;
using System.Net;
using Leaf.xNet;
using HtmlAgilityPack;
using Newtonsoft.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Data;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;
using System.IO;
using System.Diagnostics;
using Hotmail_Get_Oauth2.Properties;
using System.Text;

namespace Hotmail_Get_Oauth2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private static readonly String clientId = "9f87c23f-2ebc-4a25-bb7f-3dbdb56e5ed0";//mail birn
        private static readonly String clientIdGraph = "9e5f94bc-e8a4-4e73-b8be-63364c29d753";//thunderbin
        private static readonly String UrlImap = "http://127.0.0.1:50402";
        private static readonly String UrlGraph = "https://localhost";
        private static readonly String PathApp = AppDomain.CurrentDomain.BaseDirectory;
      
        private bool isStop = false;
        private void Form1_Load(object sender, EventArgs e)
        {
            tbListProxy.Text = "    Proxy http/sock5 ip:port\r\n    hoặc ip:port:user:pass";
            tbListMail.Text = "    Mail|Pass\r\n    hoặc Mail|Pass|Refresh_Token|Client_ID";
            tbOut.Text = "    Dữ liệu đầu ra Mail|Pass|Refresh_Token|Client_ID|Access_Token";
            tbError.Text = "    Mail bị lỗi";
        //    var request = new HttpRequest();
        //    String USER_AGENT = "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:128.0) Gecko/20100101 Thunderbird/128.2.3";
        //    request.ConnectTimeout = 60000;
        //    request.UserAgent = USER_AGENT;
        //    request["Accept"] = "*/*";
        //    request.AllowAutoRedirect = true;
        //    request.AddHeader("Accept-Language", "en-US,en;q=0.5");
        //    request.AddHeader("Accept-Encoding", "gzip, deflate, br, zstd, value");
        //    request.AddHeader("Priority", "u=0, i");
        //    request.AddHeader("Upgrade-Insecure-Requests", "1");
        //    request.AddHeader("Sec-Fetch-Dest", "document");
        //    request.AddHeader("Sec-Fetch-Mode", "navigate");
        //    request.AddHeader("Sec-Fetch-Site", "same-origin");
        //    request.AddHeader("Sec-Fetch-User", "?1");
        //    request.IgnoreProtocolErrors = true;
        //    String bodyRequest3 = $"client_id={clientId}&grant_type=authorization_code&redirect_uri=https%3A%2F%2Flocalhost&code=M.C556_BAY.2.U.79b819bc-c153-883c-d588-3cdd033fc8ba&scope=IMAP.AccessAsUser.All+POP.AccessAsUser.All+SMTP.Send+Mail.ReadWrite+Mail.ReadBasic+openid+profile+User.Read+offline_access";
        //    var accessTokenResp = request.Post(IConfig.oauthUrl, bodyRequest3, "application/x-www-form-urlencoded; charset=UTF-8");
        //    String bodyTokenResp = accessTokenResp.ToString();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            isStop = true;
        }
        private static string UrlApi = "";
        private List<string> GetProxyApi()
        {
            try
            {
                using (HttpRequest http = new HttpRequest())
                {
                    http.IgnoreProtocolErrors = true;
                    http.ConnectTimeout = 2000;
                    http.KeepAliveTimeout = 4000;
                    http.ReadWriteTimeout = 4000;
                    var string00_1 = http.Get(UrlApi);
                    string string00 = string00_1.ToString();
                    if (string00.Contains(':'))
                    {
                        string[] strings_t = Regex.Split(string00, "\r\n");
                        if (strings_t.Length > 0)
                        {
                            List<string> lkiuyp = strings_t.ToList();
                            lkiuyp.Remove("");
                            http.Dispose();
                            return lkiuyp;
                        }
                    }
                    http.Dispose();
                }
            }
            catch { }
            return null;
        }
        private Stopwatch TimerGetProxyApi = new Stopwatch();
        private int DemProxy = -1;
        private string GetListProxy()
        {
            try
            {
                lock (LockGetListProxy)
                {
                    if (TimerGetProxyApi.IsRunning && TimerGetProxyApi.Elapsed.Minutes >= 5)
                    {
                        List<string> kyu = GetProxyApi();
                        if (kyu != null && kyu.Count > 0)
                        {
                            AllProxy = kyu;
                        }
                        TimerGetProxyApi.Restart();
                    }
                    DemProxy++;
                    if (DemProxy >= AllProxy.Count)
                    {
                        DemProxy = 0;
                    }
                    return AllProxy[DemProxy];
                }
            }
            catch { }
            return null;
        }
        private string GetKeyLoop()
        {
            try
            {
                lock (Lockkeyproxy)
                {
                    DemProxy++;
                    if (DemProxy >= AllProxy.Count)
                    {
                        DemProxy = 0;
                    }
                    return AllProxy[DemProxy];
                }
            }
            catch { }
            return null;
        }
        private Thread threadRun = null;
        private object LockGetListProxy = new object();
        private object LockGetMail = new object();
        private object Lockkeyproxy = new object();
        private object LockGetProxy = new object();
        private object LockSetDataOut = new object();
        private List<string> AllProxy = new List<string>();
        private void btnStart_Click(object sender, EventArgs e)
        {
            if (TimePx > 0 && TimePx < 10)
            {
                MessageBox.Show("Time get again proxy hãy để trên 10 giây!");
                return;
            }
            string[] ListMail = tbListMail.Lines;
            if (ListMail.Length == 0 || ListMail[0].Contains("  Mail|Pass"))
            {
                ListMail = new string[] { };
            }
            List<string> AllMail = new List<string>();
            if (ListMail.Length > 0)
            {
                AllMail = ListMail.ToList();
                for (int i = 0; i < AllMail.Count; i++)
                {
                    if (!AllMail[i].Contains("@"))
                    {
                        AllMail.RemoveRange(i, 1);
                        i--;
                    }
                }
            }
            if (AllMail.Count == 0)
            {
                MessageBox.Show("Nhập mail vào trước!");
                return;
            }
            bool KeyTop = false;
            string[] ListProxy = tbListProxy.Lines;
            if (ListProxy.Length == 0 || ListProxy[0].Contains("Proxy"))
            {
                ListProxy = new string[] { };
            }
            else if (ListProxy.Length > 0 && ListProxy[0].Length > 5 && !ListProxy[0].ToLower().StartsWith("http") && !ListProxy[0].Contains(":"))
            {
                AllProxy = new List<string>();
                if (ListProxy.Length > 0)
                {
                    AllProxy = ListProxy.ToList();
                    for (int i = 0; i < AllProxy.Count; i++)
                    {
                        if (AllProxy[i].Contains(":") || AllProxy[i].ToLower().StartsWith("http") || AllProxy[i].Length <= 5)
                        {
                            AllProxy.RemoveRange(i, 1);
                            i--;
                        }
                    }
                }
                if (AllProxy.Count == 0)
                {
                    MessageBox.Show("Không có key proxy hợp lệ!");
                    return;
                }
                KeyTop = true;
            }
            else
            {
                if (ListProxy[0].ToLower().StartsWith("http"))
                {
                    UrlApi = ListProxy[0];
                    var nnh = GetProxyApi();
                    if (nnh == null || nnh.Count == 0)
                    {
                        MessageBox.Show("Không lấy được proxy từ api!");
                        return;
                    }
                    ListProxy = nnh.ToArray();
                    TimerGetProxyApi.Restart();
                }
                AllProxy = new List<string>();
                if (ListProxy.Length > 0)
                {
                    AllProxy = ListProxy.ToList();
                    for (int i = 0; i < AllProxy.Count; i++)
                    {
                        if (!AllProxy[i].Contains(":"))
                        {
                            AllProxy.RemoveRange(i, 1);
                            i--;
                        }
                    }
                }
            }
            
            DemProxy = -1;
            tbError.Lines = new string[] { };
            tbOut.Lines = new string[] { };
            btnStart.Enabled = false;
            btnStop.Enabled = true;
            int sl = Convert.ToInt32(nrudSoLuong.Value);
            bool pxHttp = !cbLoaiProxy.Checked;
            bool UsingImap = !cbImap.Checked;
            bool LayAccessToken = cbAccessToken.Checked;
            try
            {
                File.WriteAllText(PathApp + "log.txt", "");
            }
            catch { }
            threadRun = new Thread(() =>
            {
                isStop = false;
                int SoLuong = sl;
                bool UserProxy = false;
                bool UserProxyHttp = pxHttp;
                if (AllProxy.Count > 0)
                {
                    UserProxy = true;
                }
                List<string> AllDone = new List<string>();
                List<string> AllError = new List<string>();
                List<Task> tasks = new List<Task>();
                var stopw = new Stopwatch();
                Task.Run(() =>
                {
                    int SoLuongDone = 0;
                    int slCache = 0;
                    int SoLuongError = 0;
                    while (true)
                    {
                        try
                        {
                            if(TimePx > 0)
                            {
                                if (!stopw.IsRunning)
                                {
                                    stopw.Start();
                                }
                                else if (stopw.Elapsed.TotalSeconds >= TimePx)
                                {
                                    stopw.Restart();
                                    lock (LockGetListProxy)
                                    {
                                        string[] ListProxy1 = new string[] { };
                                        tbListProxy.Invoke(new Action(() =>
                                        {
                                            ListProxy1 = tbListProxy.Lines;
                                        }));
                                        if (ListProxy1.Length == 0 || ListProxy1[0].Contains("Proxy"))
                                        {
                                            ListProxy1 = new string[] { };
                                        }
                                        else
                                        {
                                            if (ListProxy[0].ToLower().StartsWith("http"))
                                            {
                                                
                                            }
                                            else
                                            {
                                                var AllProxy1 = new List<string>();
                                                if (ListProxy1.Length > 0)
                                                {
                                                    AllProxy1 = ListProxy1.ToList();
                                                    for (int i = 0; i < AllProxy1.Count; i++)
                                                    {
                                                        if (!AllProxy1[i].Contains(":"))
                                                        {
                                                            AllProxy1.RemoveRange(i, 1);
                                                            i--;
                                                        }
                                                    }
                                                    if (AllProxy1.Count > 0)
                                                    {
                                                        AllProxy = AllProxy1;
                                                    }
                                                }
                                            }
                                        }
                                        
                                    }
                                }
                            }
                            else
                            {
                                if (stopw.IsRunning)
                                {
                                    stopw.Stop();
                                    stopw.Reset();
                                }
                            }
                            for (int i = 0; i < 40; i++)
                            {
                                Task.Delay(100).Wait();
                                if (isStop)
                                {
                                    break;
                                }
                            }
                            slCache = AllDone.Count;
                            if (SoLuongDone != slCache)
                            {
                                SoLuongDone = slCache;
                                lock (LockSetDataOut)
                                {
                                    tbOut.Invoke(new Action(() =>
                                    {
                                        tbOut.Lines = AllDone.ToArray();
                                    }));
                                }
                            }
                            slCache = AllError.Count;
                            if (SoLuongError != slCache)
                            {
                                SoLuongError = slCache;
                                lock (LockSetDataOut)
                                {
                                    tbError.Invoke(new Action(() =>
                                    {
                                        tbError.Lines = AllError.ToArray();
                                    }));
                                }
                            }
                        }
                        catch { }
                        if (isStop)
                        {
                            slCache = AllDone.Count;
                            if (SoLuongDone != slCache)
                            {
                                continue;
                            }
                            slCache = AllError.Count;
                            if (SoLuongError != slCache)
                            {
                                continue;
                            }
                            if (AllMail.Count > 0)
                            {
                                AllError.AddRange(AllMail);
                                AllMail.Clear();
                                continue;
                            }
                            return;
                        }
                    }
                });
                for (int i = 0; i < SoLuong; i++)
                {
                    int ind = i;
                    Task task = Task.Run(async() =>
                    {
                        int Index = ind;
                        string Key = "";
                        Topproxy topproxy = new Topproxy();
                        if (KeyTop)
                        {
                            Key = GetKeyLoop();
                            if (!string.IsNullOrEmpty(Key))
                            {
                                QuanLyProxy.VoidLayProxy += topproxy.GetNewProxy;
                                QuanLyProxy.TimeGetProxy = 60;
                            }
                        }
                        Random rrandom = new Random();
                        string GetMail()
                        {
                            try
                            {
                                lock (LockGetMail)
                                {
                                    if (AllMail.Count == 0)
                                    {
                                        return "";
                                    }
                                    string text = AllMail[0];
                                    AllMail.RemoveRange(0, 1);
                                    return text;
                                }
                            }
                            catch { }
                            return "";
                        }
                        string ConvertToFormUrlEncoded(Dictionary<string, string> data)
                        {
                            return string.Join("&", data.Select(kvp =>
                                $"{WebUtility.UrlEncode(kvp.Key)}={WebUtility.UrlEncode(kvp.Value)}"));
                        }
                        void DoParseForm(HtmlNodeCollection inputNodes, Dictionary<string, string> submitForm)
                        {
                            if (inputNodes != null)
                            {
                                foreach (HtmlNode input in inputNodes)
                                {
                                    HtmlAttribute aname = input.Attributes["name"];
                                    HtmlAttribute avalue = input.Attributes["value"];
                                    if (aname != null && avalue != null)
                                    {
                                        submitForm.Add(aname.Value, avalue.Value);
                                    }
                                }
                            }
                        }
                        String oauth2(String email, String pass, String proxy = null, bool http = true, bool imap = true)
                        {
                            var request = new HttpRequest();
                            try
                            {
                                String USER_AGENT = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/136.0.0.0 Safari/537.36";
                                if (!imap) 
                                {
                                    USER_AGENT = "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:128.0) Gecko/20100101 Thunderbird/128.2.3";
                                }
                                request.ConnectTimeout = 60000;
                                request.UserAgent = USER_AGENT;
                                request["Accept"] = "*/*";
                                request.AllowAutoRedirect = true;
                                request.AddHeader("Accept-Language", "en-US,en;q=0.5");
                                request.AddHeader("Accept-Encoding", "gzip, deflate, br, zstd, value");
                                request.AddHeader("Priority", "u=0, i");
                                request.AddHeader("Upgrade-Insecure-Requests", "1");
                                request.AddHeader("Sec-Fetch-Dest", "document");
                                request.AddHeader("Sec-Fetch-Mode", "navigate");
                                request.AddHeader("Sec-Fetch-Site", "same-origin");
                                request.AddHeader("Sec-Fetch-User", "?1");

                                if (proxy != null && proxy.Contains(":"))
                                {
                                    string[] px = proxy.Split(':');
                                    if (http)
                                    {
                                        if (px.Length == 4)
                                        {
                                            request.Proxy = HttpProxyClient.Parse($"{px[0]}:{px[1]}");
                                            request.Proxy.Username = px[2];
                                            request.Proxy.Password = px[3];
                                        }
                                        else
                                        {
                                            request.Proxy = HttpProxyClient.Parse($"{px[0]}:{px[1]}");
                                        }
                                    }
                                    else
                                    {
                                        if (px.Length == 4)
                                        {
                                            request.Proxy = Socks5ProxyClient.Parse($"{px[0]}:{px[1]}");
                                            request.Proxy.Username = px[2];
                                            request.Proxy.Password = px[3];
                                        }
                                        else
                                        {
                                            request.Proxy = Socks5ProxyClient.Parse($"{px[0]}:{px[1]}");
                                        }
                                    }
                                }

                                HttpResponse pageLoad = request.Get("https://outlook.live.com/owa/");

                                String newUrl1 = "https://login.microsoftonline.com/common/oauth2/v2.0/authorize?response_type=code&client_id=";
                                if (imap)
                                {
                                    newUrl1 += clientId +
                                    "&redirect_uri=" + UrlImap +
                                    "&scope=https://outlook.office.com/EWS.AccessAsUser.All+https://outlook.office.com/Mail.ReadBasic+https://outlook.office.com/IMAP.AccessAsUser.All+https://outlook.office.com/POP.AccessAsUser.All+https://outlook.office.com/SMTP.Send+Mail.ReadWrite+Mail.ReadBasic+openid+email+profile+User.Read+offline_access&login_hint="
                                    + email;
                                }
                                else
                                {
                                    newUrl1 += clientIdGraph +
                                    "&redirect_uri=" + UrlGraph +
                                    "&scope=https://outlook.office.com/EWS.AccessAsUser.All+https://outlook.office.com/IMAP.AccessAsUser.All+https://outlook.office.com/POP.AccessAsUser.All+https://outlook.office.com/SMTP.Send+Mail.ReadWrite+Mail.ReadBasic+openid+profile+User.Read+email+offline_access&login_hint="
                                    + email;
                                }
                                //Chỉ dùng url 1 K dùng newUrl2 nếu lỗi.
                                String newUrl2 = "https://login.microsoftonline.com/common/oauth2/v2.0/authorize?response_type=code&client_id=";
                                if (imap)
                                {
                                    newUrl2 += clientId +
                                    "&redirect_uri=" + UrlImap +
                                    "&scope=https://outlook.office.com/EWS.AccessAsUser.All+https://outlook.office.com/Mail.ReadBasic+https://outlook.office.com/IMAP.AccessAsUser.All+https://outlook.office.com/POP.AccessAsUser.All+https://outlook.office.com/SMTP.Send+Mail.ReadWrite+Mail.ReadBasic+openid+email+profile+User.Read+offline_access&login_hint=" +
                                    email + "&sso_reload=true";
                                }
                                else
                                {
                                    newUrl2 += clientIdGraph +
                                    "&redirect_uri=" + UrlGraph +
                                    "&scope=https://outlook.office.com/EWS.AccessAsUser.All+https://outlook.office.com/IMAP.AccessAsUser.All+https://outlook.office.com/POP.AccessAsUser.All+https://outlook.office.com/SMTP.Send+Mail.ReadWrite+Mail.ReadBasic+openid+profile+User.Read+email+offline_access&login_hint="
                                    + email + "&sso_reload=true";
                                }
                                HttpResponse resp1t = request.Get(newUrl1);
                                HttpResponse resp1 = request.Get(newUrl2);
                                String respStr1 = resp1.ToString();
                                string urlResGet = resp1.Address.ToString();
                                int index = respStr1.IndexOf(IConfig.urlPostKey);
                                String urlPost = respStr1.Substring(index, 250).Split('\'')[0].Split('"')[0];

                                index = respStr1.IndexOf("PPFT");
                                String ppftTemp = respStr1.Substring(index, 400);
                                if (!ppftTemp.Contains("value="))
                                {
                                    index = respStr1.LastIndexOf("PPFT");
                                    ppftTemp = respStr1.Substring(index, 400);
                                }
                                String[] marrs = ppftTemp.Split('"');
                                String ppftValue = "";

                                foreach (String s in marrs)
                                {
                                    if (s.Length > 50)
                                    {
                                        ppftValue = s.Replace("\\", "");
                                        break;
                                    }
                                }
                                String bodyLogin = "ps=2&psRNGCDefaultType=&psRNGCEntropy=&psRNGCSLK=&canary=&ctx=&hpgrequestid=&PPFT=@PPFT&PPSX=Passp&NewUser=1&FoundMSAs=&fspost=0" +
                                    "&i21=0&CookieDisclosure=0&IsFidoSupported=1&isSignupPost=0&isRecoveryAttemptPost=0&i13=1" +
                                    "&login=@EMAIL&loginfmt=@EMAIL&type=11&LoginOptions=1&lrt=&lrtPartition=&hisRegion=&hisScaleUnit=&passwd=" + pass;
                                bodyLogin = bodyLogin.Replace("@PPFT", ppftValue);
                                bodyLogin = bodyLogin.Replace("@EMAIL", email);

                                request.AllowAutoRedirect = false;
                                request.Referer = urlResGet;
                                var resp2 = request.Post(urlPost, bodyLogin, "application/x-www-form-urlencoded; charset=UTF-8");
                                String bodyResp2 = resp2.ToString();
                                request.Referer = "";

                                if (bodyResp2.Contains("Abuse?"))
                                {
                                    throw new Exception("Your account has been locked");
                                }
                                else if (bodyResp2.Contains(IConfig.urlPostKey))
                                {
                                    string urlPost_1 = urlPost;
                                    request.Referer = urlPost_1;
                                    index = bodyResp2.IndexOf(IConfig.urlPostKey);
                                    urlPost = bodyResp2.Substring(index, 250).Split('\'')[0].Split('"')[0];
                                    index = bodyResp2.IndexOf(IConfig.ppftKey_1);
                                    ppftTemp = bodyResp2.Substring(index + 5, 300);
                                    marrs = Regex.Split(ppftTemp, "'");
                                    ppftValue = "";

                                    foreach (String s in marrs)
                                    {
                                        if (s.Length > 50)
                                        {
                                            ppftValue = s;
                                            break;
                                        }
                                    }
                                    bodyLogin = $"LoginOptions=1&type=28&ctx=&hpgrequestid=&PPFT={ppftValue}&canary=";
                                    resp2 = request.Post(urlPost, bodyLogin, "application/x-www-form-urlencoded; charset=UTF-8");
                                    bodyResp2 = resp2.ToString();
                                    request.Referer = "";
                                }
                                String location = "";
                                int Consent_ = 0;
                                GOTOConsent_:
                                if (bodyResp2.Contains("Consent/Update?"))
                                {
                                    location = "";
                                    String UrlUpdate = "";
                                    Regex extractDateRegex = new Regex("https?:\\/\\/(?:www\\.)?[-a-zA-Z0-9@:%._\\+~#=]{1,256}\\.[a-zA-Z0-9()]{1,6}\\b(?:[-a-zA-Z0-9()@:%_\\+.~#?&\\/=]*)");
                                    string[] extracted = extractDateRegex.Matches(bodyResp2).Cast<Match>().Select(m => m.Value).ToArray();
                                    if (extracted != null && extracted.Length > 0)
                                    {
                                        foreach (var item_ in extracted)
                                        {
                                            if (item_.Contains("Consent/Update?"))
                                            {
                                                UrlUpdate = item_;
                                                break;
                                            }
                                        }
                                    }
                                    if (UrlUpdate != "")
                                    {
                                        String pprid = "";
                                        String ipt = "";
                                        String uaid = "";
                                        String _client_id = "";
                                        String scope = "";
                                        String[] strings2_4 = Regex.Split(bodyResp2, "input");
                                        foreach (var item in strings2_4)
                                        {
                                            if (item.Contains("id=\"ipt\""))
                                            {
                                                ipt = Regex.Split(item, "value=\"")[1].Replace("\"", "").Replace(">", "").Replace("<", "");
                                            }
                                            else if (item.Contains("id=\"uaid\""))
                                            {
                                                uaid = Regex.Split(item, "value=\"")[1].Replace("\"", "").Replace(">", "").Replace("<", "");
                                            }
                                            else if (item.Contains("id=\"client_id\""))
                                            {
                                                _client_id = Regex.Split(item, "value=\"")[1].Replace("\"", "").Replace(">", "").Replace("<", "");
                                            }
                                            else if (item.Contains("id=\"scope\""))
                                            {
                                                string[] stringsoi = Regex.Split(item, "value=\"");
                                                scope = stringsoi[1].Split('"')[0];
                                            }
                                            else if (item.Contains("id=\"pprid\""))
                                            {
                                                pprid = Regex.Split(item, "value=\"")[1].Replace("\"", "").Replace(">", "").Replace("<", "");
                                            }
                                            if (pprid != "" && ipt != "" && uaid != "" && _client_id != "" && scope != "")
                                            {
                                                break;
                                            }
                                        }
                                        if (pprid != "" && ipt != "" && uaid != "" && _client_id != "" && scope != "")
                                        {
                                            String bodyUpdate = $"rd=none&pprid={pprid}" +
                                            $"&ipt={ipt}&uaid={uaid}&client_id={_client_id}&scope={scope.Replace(" ", "%3A")}";
                                            var resp3 = request.Post(UrlUpdate, bodyUpdate, "application/x-www-form-urlencoded; charset=UTF-8");
                                            String bodyResp3 = resp3.ToString();
                                            String canary = "";
                                            String cscope = "";
                                            String ucaction = "Yes";
                                            String[] strings_1 = Regex.Split(bodyResp3, "input");
                                            foreach (var item in strings_1)
                                            {
                                                if (item.Contains("id=\"canary\""))
                                                {
                                                    string[] stringsoi = Regex.Split(item, "value=\"");
                                                    canary = stringsoi[1].Split('"')[0];
                                                }
                                                if (canary != "")
                                                {
                                                    break;
                                                }
                                            }
                                            if (string.IsNullOrEmpty(canary) && bodyResp3.Contains("\"sCanary\":\""))
                                            {
                                                string[] strings00 = Regex.Split(bodyResp3, "\"sCanary\":\"");
                                                string[] strings1100 = Regex.Split(strings00[1], "\"");
                                                if (strings1100[0].Length > 10)
                                                {
                                                    canary = Regex.Unescape(strings1100[0]);
                                                }
                                            }
                                            if (!string.IsNullOrEmpty(canary))
                                            {
                                                String bodyUpdate_1 = $"cscope={cscope}&canary={canary}" +
                                                        $"&ucaction={ucaction}&client_id={_client_id}&scope={scope.Replace(" 0", "|0").Replace(" ", ":").Replace("|0", " 0")}";
                                                var resp4 = request.Post(UrlUpdate, bodyUpdate_1, "application/x-www-form-urlencoded; charset=UTF-8");
                                                String bodyResp4 = resp4.ToString();
                                                location = resp4.Location;
                                                if (location.Contains("login.live.com/oauth20_authorize.srf"))
                                                {
                                                    HttpResponse resp5 = request.Get(location);
                                                    bodyResp2 = resp5.ToString();
                                                    location = resp5.Location;
                                                }
                                            }
                                        }
                                    }
                                }
                                else if (bodyResp2.Contains("interrupt/passkey"))
                                {
                                    location = "";
                                    resp1t = request.Get(newUrl1);
                                    resp1 = request.Get(newUrl2);
                                    bodyResp2 = resp1.ToString();
                                    location = resp1.Location;
                                }
                                else
                                {
                                    location = resp2.Location;
                                }
                                int submit_ = 0;
                                GOTOsubmit:
                                // Chap nhan doc mail qua imap
                                if (bodyResp2.Contains("document.fmHF.submit"))
                                {
                                    HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();
                                    document.LoadHtml(bodyResp2);
                                    var formAction = document.GetElementbyId("fmHF");
                                    string actionUrl = formAction.Attributes["action"].Value;
                                    if (actionUrl.Contains("confirm?mkt="))
                                    {
                                        var confirmDocHtml = formAction.SelectNodes("//input");
                                        Dictionary<string, string> confirmForm = new Dictionary<string, string>();
                                        DoParseForm(confirmDocHtml, confirmForm);

                                        string skipformUrl = ConvertToFormUrlEncoded(confirmForm);
                                        var skipResp1 = request.Post(actionUrl, skipformUrl, "application/x-www-form-urlencoded; charset=UTF-8");

                                        string skipStr = skipResp1.ToString();
                                        document.LoadHtml(skipStr);

                                        var confirmInputNodes = document.DocumentNode.SelectNodes("//input");

                                        confirmForm.Clear();

                                        DoParseForm(confirmInputNodes, confirmForm);

                                        var confirmformUrlEncoded = ConvertToFormUrlEncoded(confirmForm);
                                        var confirmResp2 = request.Post(actionUrl, confirmformUrlEncoded, "application/x-www-form-urlencoded; charset=UTF-8");
                                    }
                                    if (actionUrl.Contains("Add?mkt="))
                                    {
                                        var skipDocHtml = formAction.SelectNodes("//input");
                                        Dictionary<string, string> skipForm = new Dictionary<string, string>();
                                        DoParseForm(skipDocHtml, skipForm);

                                        string skipformUrl = ConvertToFormUrlEncoded(skipForm);
                                        var skipResp1 = request.Post(actionUrl, skipformUrl, "application/x-www-form-urlencoded; charset=UTF-8");

                                        string skipStr = skipResp1.ToString();
                                        document.LoadHtml(skipStr);

                                        var skipInputNodes = document.DocumentNode.SelectNodes("//input");

                                        skipForm.Clear();

                                        DoParseForm(skipInputNodes, skipForm);
                                        skipForm.Remove("action");
                                        skipForm.Add("action", "Skip");
                                        skipForm[""] = "";
                                        var skipformUrlEncoded = ConvertToFormUrlEncoded(skipForm);
                                        var skipResp2 = request.Post(actionUrl, skipformUrlEncoded, "application/x-www-form-urlencoded; charset=UTF-8");
                                        if (skipResp2.Location != null && skipResp2.Location.Contains("login.live.com/oauth20_authorize.srf?"))
                                        {
                                            request.ClearAllHeaders();
                                            request.UserAgent = USER_AGENT;
                                            request["Accept"] = "*/*";
                                            request.AllowAutoRedirect = false;
                                            request.AddHeader("Accept-Language", "en-US,en;q=0.5");
                                            request.AddHeader("Accept-Encoding", "gzip, deflate, br, zstd, value");
                                            request.AddHeader("Priority", "u=0, i");
                                            request.AddHeader("Connection", "keep-alive");
                                            request.AddHeader("Cache-Control", "max-age=0");
                                            request.AddHeader("Host", "login.live.com");
                                            request.Referer = "https://account.live.com/";
                                            request.AddHeader("Upgrade-Insecure-Requests", "1");
                                            request.AddHeader("Sec-Fetch-Dest", "document");
                                            request.AddHeader("Sec-Fetch-Mode", "navigate");
                                            request.AddHeader("Sec-Fetch-Site", "same-site");
                                            request.AddHeader("Sec-Fetch-User", "?1");
                                            HttpResponse resptoken = request.Get(skipResp2.Location);
                                            if (resptoken.Location != null && resptoken.Location.Contains("/?error=access_denied"))
                                            {
                                                throw new Exception("Từ chối quyền truy cập");//Lỗi này 99% do trùng ip
                                            }
                                            else if (resptoken.Location != null && resptoken.Location.IndexOf("?code=") > 0)
                                            {
                                                location = resptoken.Location;
                                            }
                                            else
                                            {
                                                bodyResp2 = resptoken.ToString();
                                                if (bodyResp2.Contains("Consent/Update?"))
                                                {
                                                    Consent_++;
                                                    if (Consent_ >= 3)
                                                    {
                                                        throw new Exception("Error Consent Update");
                                                    }
                                                    goto GOTOConsent_;
                                                }
                                                HtmlAgilityPack.HtmlDocument documentac = new HtmlAgilityPack.HtmlDocument();
                                                documentac.LoadHtml(resptoken.ToString());
                                                formAction = documentac.GetElementbyId("fmHF");
                                                try
                                                {
                                                    actionUrl = formAction.Attributes["action"].Value;
                                                }
                                                catch
                                                {
                                                    request.AllowAutoRedirect = true;
                                                    resp1t = request.Get(newUrl1);
                                                    resp1 = request.Get(newUrl2);
                                                    request.AllowAutoRedirect = false;
                                                    bodyResp2 = resp1.ToString();
                                                    if (resp1.Location != null && resp1.Location.IndexOf("?code=") > 0)
                                                    {
                                                        location = resp1.Location;
                                                    }
                                                    else if (bodyResp2.Contains("document.fmHF.submit"))
                                                    {
                                                        submit_++;
                                                        if (submit_ >= 3)
                                                        {
                                                            throw new Exception("Error submit");
                                                        }
                                                        goto GOTOsubmit;
                                                    }
                                                }
                                            }
                                            request.ClearAllHeaders();
                                            request.UserAgent = USER_AGENT;
                                            request["Accept"] = "*/*";
                                            request.AllowAutoRedirect = false;
                                            request.AddHeader("Accept-Language", "en-US,en;q=0.5");
                                            request.AddHeader("Accept-Encoding", "gzip, deflate, br, zstd, value");
                                            request.AddHeader("Priority", "u=0, i");
                                            request.AddHeader("Upgrade-Insecure-Requests", "1");
                                            request.AddHeader("Sec-Fetch-Dest", "document");
                                            request.AddHeader("Sec-Fetch-Mode", "navigate");
                                            request.AddHeader("Sec-Fetch-Site", "same-origin");
                                            request.AddHeader("Sec-Fetch-User", "?1");
                                        }
                                    }
                                    if (actionUrl.Contains("account.live.com/proofs/Verify"))
                                    {
                                        var skipDocHtml = formAction.SelectNodes("//input");
                                        Dictionary<string, string> skipForm = new Dictionary<string, string>();
                                        DoParseForm(skipDocHtml, skipForm);

                                        string skipformUrl = ConvertToFormUrlEncoded(skipForm);
                                        var skipResp1 = request.Post(actionUrl, skipformUrl, "application/x-www-form-urlencoded; charset=UTF-8");

                                        string skipStr = skipResp1.ToString();
                                        document.LoadHtml(skipStr);
                                        string iProofOptions = "";
                                        if (skipStr.Contains("iProofOptions"))
                                        {
                                            string[] strings_ = Regex.Split(skipStr, "name=\"iProofOptions\"");
                                            if (strings_.Length > 1 && strings_[1].Contains("value=\""))
                                            {
                                                string[] strings_1 = Regex.Split(strings_[1], "value=\"");
                                                if (strings_1.Length > 1 && strings_1[1].Length > 1)
                                                {
                                                    iProofOptions = Regex.Split(strings_1[1], "\"")[0];
                                                }
                                            }
                                        }
                                        var skipInputNodes = document.DocumentNode.SelectNodes("//input");

                                        skipForm.Clear();

                                        DoParseForm(skipInputNodes, skipForm);
                                        skipForm.Remove("action");
                                        skipForm.Remove("iOttText");
                                        skipForm.Add("iProofOptions", iProofOptions);
                                        skipForm.Add("action", "Skip");
                                        skipForm.Add("iOttText", "");
                                        var skipformUrlEncoded = ConvertToFormUrlEncoded(skipForm);
                                        var skipResp2 = request.Post(actionUrl, skipformUrlEncoded, "application/x-www-form-urlencoded; charset=UTF-8");
                                        if (skipResp2.Location != null && skipResp2.Location.Contains("login.live.com/oauth20_authorize.srf?"))
                                        {
                                            request.ClearAllHeaders();
                                            request.UserAgent = USER_AGENT;
                                            request["Accept"] = "*/*";
                                            request.AllowAutoRedirect = false;
                                            request.AddHeader("Accept-Language", "en-US,en;q=0.5");
                                            request.AddHeader("Accept-Encoding", "gzip, deflate, br, zstd, value");
                                            request.AddHeader("Priority", "u=0, i");
                                            request.AddHeader("Connection", "keep-alive");
                                            request.AddHeader("Cache-Control", "max-age=0");
                                            request.AddHeader("Host", "login.live.com");
                                            request.Referer = "https://account.live.com/";
                                            request.AddHeader("Upgrade-Insecure-Requests", "1");
                                            request.AddHeader("Sec-Fetch-Dest", "document");
                                            request.AddHeader("Sec-Fetch-Mode", "navigate");
                                            request.AddHeader("Sec-Fetch-Site", "same-site");
                                            request.AddHeader("Sec-Fetch-User", "?1");
                                            HttpResponse resptoken = request.Get(skipResp2.Location);
                                            if (resptoken.Location != null && resptoken.Location.Contains("/?error=access_denied"))
                                            {
                                                throw new Exception("Từ chối quyền truy cập");//Lỗi này 99% do trùng ip
                                            }
                                            else if (resptoken.Location != null && resptoken.Location.IndexOf("?code=") > 0)
                                            {
                                                location = resptoken.Location;
                                            }
                                            else
                                            {
                                                bodyResp2 = resptoken.ToString();
                                                if (bodyResp2.Contains("Consent/Update?"))
                                                {
                                                    Consent_++;
                                                    if (Consent_ >= 3)
                                                    {
                                                        throw new Exception("Error Consent Update");
                                                    }
                                                    goto GOTOConsent_;
                                                }
                                                else if (bodyResp2.Contains("account.live.com/proofs/Verify"))
                                                {
                                                    Consent_++;
                                                    if (Consent_ >= 3)
                                                    {
                                                        throw new Exception("Error Verify Update");
                                                    }
                                                    goto GOTOConsent_;
                                                }
                                                HtmlAgilityPack.HtmlDocument documentac = new HtmlAgilityPack.HtmlDocument();
                                                documentac.LoadHtml(resptoken.ToString());
                                                formAction = documentac.GetElementbyId("fmHF");
                                                try
                                                {
                                                    actionUrl = formAction.Attributes["action"].Value;
                                                }
                                                catch
                                                {
                                                    request.AllowAutoRedirect = true;
                                                    resp1t = request.Get(newUrl1);
                                                    resp1 = request.Get(newUrl2);
                                                    request.AllowAutoRedirect = false;
                                                    bodyResp2 = resp1.ToString();
                                                    if (resp1.Location != null && resp1.Location.IndexOf("?code=") > 0)
                                                    {
                                                        location = resp1.Location;
                                                    }
                                                    else if (bodyResp2.Contains("document.fmHF.submit"))
                                                    {
                                                        submit_++;
                                                        if (submit_ >= 3)
                                                        {
                                                            throw new Exception("Error submit");
                                                        }
                                                        goto GOTOsubmit;
                                                    }
                                                }
                                            }
                                            request.ClearAllHeaders();
                                            request.UserAgent = USER_AGENT;
                                            request["Accept"] = "*/*";
                                            request.AllowAutoRedirect = false;
                                            request.AddHeader("Accept-Language", "en-US,en;q=0.5");
                                            request.AddHeader("Accept-Encoding", "gzip, deflate, br, zstd, value");
                                            request.AddHeader("Priority", "u=0, i");
                                            request.AddHeader("Upgrade-Insecure-Requests", "1");
                                            request.AddHeader("Sec-Fetch-Dest", "document");
                                            request.AddHeader("Sec-Fetch-Mode", "navigate");
                                            request.AddHeader("Sec-Fetch-Site", "same-origin");
                                            request.AddHeader("Sec-Fetch-User", "?1");
                                        }
                                    }
                                    if (location.IndexOf("?code=") == -1)
                                    {
                                        var docHtml = formAction.SelectNodes("//input");
                                        Dictionary<string, string> submitForm = new Dictionary<string, string>();
                                        DoParseForm(docHtml, submitForm);

                                        string formUrlEncoded = ConvertToFormUrlEncoded(submitForm);

                                        var acceptResp = request.Post(actionUrl, formUrlEncoded, "application/x-www-form-urlencoded; charset=UTF-8");

                                        string bodyAcceptStr = acceptResp.ToString();
                                        document.LoadHtml(bodyAcceptStr);

                                        var inputNodes = document.DocumentNode.SelectNodes("//input");

                                        submitForm.Clear();
                                        DoParseForm(docHtml, submitForm);
                                        submitForm.Add("ucaction", "Yes");

                                        formUrlEncoded = ConvertToFormUrlEncoded(submitForm);
                                        var acceptResp2 = request.Post(actionUrl, formUrlEncoded, "application/x-www-form-urlencoded; charset=UTF-8");

                                        String sublocation = acceptResp2.Location;
                                        if (sublocation.Contains("success"))
                                        {
                                            var acceptResp3 = request.Get(sublocation);
                                            sublocation = acceptResp3.Location;
                                        }
                                        if (sublocation.Contains("?code=") || sublocation.Contains("developer.microsoft.com"))
                                        {
                                            location = sublocation;
                                        }
                                    }
                                }
                                // Get Oauth2 token
                                if (location.IndexOf("?code=") > 0)
                                {
                                    String localhostCode = "";
                                    localhostCode = location.Split('=')[1];
                                    if (imap)
                                    {
                                        request.IgnoreProtocolErrors = true;
                                        String bodyRequest3 = $"client_id={clientId}&grant_type=authorization_code&redirect_uri={WebUtility.UrlEncode(UrlImap)}&code={localhostCode}&scope=openid+email+offline_access+https://outlook.office.com/EWS.AccessAsUser.All+https://outlook.office.com/IMAP.AccessAsUser.All+https://outlook.office.com/POP.AccessAsUser.All+https://outlook.office.com/SMTP.Send";
                                        var accessTokenResp = request.Post(IConfig.oauthUrl, bodyRequest3, "application/x-www-form-urlencoded; charset=UTF-8");
                                        String bodyTokenResp = accessTokenResp.ToString();
                                        dynamic results = JsonConvert.DeserializeObject<dynamic>(bodyTokenResp);
                                        var access_token = results.access_token;
                                        var fresh_token = results.refresh_token;
                                        String textOut = $"{email}|{pass}|{fresh_token}|{clientId}";
                                        if (LayAccessToken)
                                        {
                                            textOut += $"|{access_token}";
                                        }
                                        request.AllowAutoRedirect = true;
                                        HttpResponse onoutlook = request.Get("https://login.live.com/");
                                        request.Dispose();
                                        return textOut;
                                    }
                                    else
                                    {
                                        request.IgnoreProtocolErrors = true;
                                        String bodyRequest3 = $"client_id={clientIdGraph}&grant_type=authorization_code&redirect_uri={WebUtility.UrlEncode(UrlGraph)}&code={localhostCode}&scope=email+IMAP.AccessAsUser.All+POP.AccessAsUser.All+SMTP.Send+Mail.ReadWrite+Mail.ReadBasic+openid+profile+User.Read+offline_access";
                                        //request.ClearAllHeaders();
                                        //request.UserAgent = USER_AGENT;
                                        //request["Accept"] = "*/*";
                                        //request.AllowAutoRedirect = true;
                                        //request.AddHeader("Accept-Language", "en-US,en;q=0.5");
                                        //request.AddHeader("Accept-Encoding", "gzip, deflate, br, zstd, value");
                                        //request.AddHeader("Priority", "u=0, i");
                                        //request.AddHeader("Sec-Fetch-Dest", "empty");
                                        //request.AddHeader("Sec-Fetch-Mode", "cors");
                                        //request.AddHeader("Sec-Fetch-Site", "cross-site");
                                        var accessTokenResp = request.Post(IConfig.oauthUrl, bodyRequest3, "application/x-www-form-urlencoded; charset=UTF-8");
                                        String bodyTokenResp = accessTokenResp.ToString();
                                        dynamic results = JsonConvert.DeserializeObject<dynamic>(bodyTokenResp);
                                        var access_token = results.access_token;
                                        var fresh_token = results.refresh_token;
                                        String textOut = $"{email}|{pass}|{fresh_token}|{clientIdGraph}";
                                        if (LayAccessToken)
                                        {
                                            textOut += $"|{access_token}";
                                        }
                                        request.AllowAutoRedirect = true;
                                        HttpResponse onoutlook = request.Get("https://login.live.com/");
                                        request.Dispose();
                                        return textOut;
                                    }
                                }
                            }
                            catch (Exception p)
                            {
                                WriteFile writeFile_ = new WriteFile();
                                writeFile_.WriteOneByOne("Exception:" + p.Message, PathApp + "log.txt");
                            }
                            request.Dispose();
                            return "";
                        }
                        string Get_AccessToken_Hotmail(string freshToken, String clientID, String proxy = null, bool http = true, bool imap = true)
                        {
                            string accessToken = null;
                            try
                            {
                                using (HttpRequest request = new HttpRequest())
                                {
                                    if (proxy != null && proxy.Contains(":"))
                                    {
                                        string[] px = proxy.Split(':');
                                        if (http)
                                        {
                                            if (px.Length == 4)
                                            {
                                                request.Proxy = HttpProxyClient.Parse($"{px[0]}:{px[1]}");
                                                request.Proxy.Username = px[2];
                                                request.Proxy.Password = px[3];
                                            }
                                            else
                                            {
                                                request.Proxy = HttpProxyClient.Parse($"{px[0]}:{px[1]}");
                                            }
                                        }
                                        else
                                        {
                                            if (px.Length == 4)
                                            {
                                                request.Proxy = Socks5ProxyClient.Parse($"{px[0]}:{px[1]}");
                                                request.Proxy.Username = px[2];
                                                request.Proxy.Password = px[3];
                                            }
                                            else
                                            {
                                                request.Proxy = Socks5ProxyClient.Parse($"{px[0]}:{px[1]}");
                                            }
                                        }
                                    }
                                    string postData = $"client_id={clientID}" +
                                                      $"&refresh_token={freshToken}" +
                                                      $"&grant_type=refresh_token";
                                    //request.IgnoreProtocolErrors = true;
                                    string response = request.Post("https://login.microsoftonline.com/common/oauth2/v2.0/token", postData, "application/x-www-form-urlencoded").ToString();
                                    dynamic json = JsonConvert.DeserializeObject(response);
                                    accessToken = json.access_token;
                                    if (accessToken != null && accessToken.Length > 12)
                                    {
                                        return accessToken;
                                    }
                                }
                            }
                            catch
                            {
                                try
                                {
                                    using (HttpRequest request = new HttpRequest())
                                    {
                                        if (proxy != null && proxy.Contains(":"))
                                        {
                                            string[] px = proxy.Split(':');
                                            if (http)
                                            {
                                                if (px.Length == 4)
                                                {
                                                    request.Proxy = HttpProxyClient.Parse($"{px[0]}:{px[1]}");
                                                    request.Proxy.Username = px[2];
                                                    request.Proxy.Password = px[3];
                                                }
                                                else
                                                {
                                                    request.Proxy = HttpProxyClient.Parse($"{px[0]}:{px[1]}");
                                                }
                                            }
                                            else
                                            {
                                                if (px.Length == 4)
                                                {
                                                    request.Proxy = Socks5ProxyClient.Parse($"{px[0]}:{px[1]}");
                                                    request.Proxy.Username = px[2];
                                                    request.Proxy.Password = px[3];
                                                }
                                                else
                                                {
                                                    request.Proxy = Socks5ProxyClient.Parse($"{px[0]}:{px[1]}");
                                                }
                                            }
                                        }
                                        string postData = $"client_id={clientID}" +
                                                      $"&refresh_token={freshToken}" +
                                                      $"&grant_type=refresh_token";
                                        if (!UsingImap)
                                        {
                                            request.AddHeader("Origin", "https://developer.microsoft.com");
                                        }
                                        string response = request.Post("https://login.microsoftonline.com/common/oauth2/v2.0/token", postData, "application/x-www-form-urlencoded").ToString();
                                        dynamic json = JsonConvert.DeserializeObject(response);
                                        accessToken = json.access_token;
                                        if (accessToken != null && accessToken.Length > 12)
                                        {
                                            return accessToken;
                                        }
                                    }
                                }
                                catch { }
                            }
                            return "";
                        }
                        string DataOut = "";
                        string DataMail = "";
                        string Mail = "";
                        string Pass = "";
                        string ClientID = "";
                        string RFToken = "";
                        string ACToken = "";
                        string PhanSau = "";
                        string Proxy = null;
                        WriteFile writeFile = new WriteFile();
                        while (true)
                        {
                            try
                            {
                                DataOut = "";
                                DataMail = "";
                                Mail = "";
                                Pass = "";
                                ClientID = "";
                                RFToken = "";
                                ACToken = "";
                                PhanSau = "";
                                Proxy = null;
                                if (isStop)
                                {
                                    throw new Exception("Stop");
                                }
                                DataMail = GetMail();
                                if (DataMail == "")
                                {
                                    throw new Exception("Stop");
                                }
                                string[] mmMail = DataMail.Split('|');
                                if (mmMail.Length < 2)
                                {
                                    mmMail = DataMail.Split(':');
                                }
                                if (mmMail.Length <= 1 || !mmMail[0].Contains("@") || mmMail[1].Length < 6)
                                {
                                    throw new Exception("mail error");
                                }
                                Mail = mmMail[0];
                                Pass = mmMail[1];
                                if (mmMail.Length >= 4 && mmMail[3].Contains("-"))
                                {
                                    ClientID = mmMail[3];
                                }
                                else
                                {
                                    ClientID = clientId;
                                }
                                if (mmMail.Length >= 3 && mmMail[2].Length > 30)
                                {
                                    RFToken = mmMail[2];
                                }
                                if (UserProxy)
                                {
                                    if (KeyTop)
                                    {
                                        while (true)//Lấy proxy
                                        {
                                            if (isStop)
                                            {
                                                throw new Exception("Stop");
                                            }
                                            Proxy = await QuanLyProxy.GetProxy(Index);
                                            if (Proxy.Contains(":") && Proxy.Contains("."))
                                            {
                                                break;
                                            }
                                            else if (Proxy == "")
                                            {
                                                await Task.Delay(1000);
                                            }
                                            else if (Proxy == "expired")
                                            {
                                                string keypp = QuanLyProxy.GetKeyByID(Index);
                                                throw new Exception("Stop Key Hết Hạn|" + keypp);
                                            }
                                            else if (int.TryParse(Proxy, out int ttime))
                                            {
                                                for (int ttimei = 0; ttimei < ttime; ttimei++)
                                                {
                                                    if (isStop)
                                                    {
                                                        throw new Exception("Stop");
                                                    }
                                                    await Task.Delay(1000);
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        Proxy = GetListProxy();
                                    }
                                }
                                if (string.IsNullOrEmpty(RFToken) || !LayAccessToken)
                                {
                                    int bnn = 2;
                                    if (!string.IsNullOrEmpty(RFToken))
                                    {
                                        bnn = 3;
                                    }
                                    if (!string.IsNullOrEmpty(ClientID))
                                    {
                                        bnn = 4;
                                    }
                                    if (mmMail.Length > 2)
                                    {
                                        for (int ivv = bnn; ivv < mmMail.Length; ivv++)
                                        {
                                            PhanSau += $"|{mmMail[ivv]}";
                                        }
                                    }
                                    DataOut = oauth2(Mail, Pass, Proxy, UserProxyHttp, UsingImap);
                                }
                                else
                                {
                                    if (mmMail.Length > 4)
                                    {
                                        for (int ivv = 4; ivv < mmMail.Length; ivv++)
                                        {
                                            PhanSau += $"|{mmMail[ivv]}";
                                        }
                                    }
                                    ACToken = Get_AccessToken_Hotmail(RFToken, ClientID, Proxy, UserProxyHttp, UsingImap);
                                    if (!string.IsNullOrEmpty(ACToken))
                                    {
                                        DataOut = $"{Mail}|{Pass}|{RFToken}|{ClientID}|{ACToken}";
                                    }
                                    if (string.IsNullOrEmpty(DataOut))
                                    {
                                        DataOut = oauth2(Mail, Pass, Proxy, UserProxyHttp, UsingImap);
                                    }
                                }
                                throw new Exception("done");
                            }
                            catch(Exception pp)
                            {
                                if (pp.Message.IndexOf("Stop") == 0)
                                {
                                    return;
                                }
                                if (!string.IsNullOrEmpty(DataOut))
                                {
                                    if (!string.IsNullOrEmpty(PhanSau))
                                    {
                                        DataOut += PhanSau;
                                    }
                                    lock (LockSetDataOut)
                                    {
                                        writeFile.WriteOneByOne(DataOut, PathApp + "out\\ok.txt");
                                        AllDone.Add(DataOut);
                                    }
                                }
                                else
                                {
                                    lock (LockSetDataOut)
                                    {
                                        writeFile.WriteOneByOne(DataMail, PathApp + "out\\error.txt");
                                        AllError.Add(DataMail);
                                    }
                                }
                            }
                        }
                    });
                    tasks.Add(task);
                    Task.Delay(200).Wait();
                }
                Task.Delay(4000).Wait();
                Task.WhenAll(tasks.ToArray()).Wait();
                isStop = true;
                MessageBox.Show("Chạy xong.");
                btnStart.Invoke(new Action(() =>
                {
                    btnStart.Enabled = true;
                }));
                btnStop.Invoke(new Action(() =>
                {
                    btnStop.Enabled = false;
                }));
            });
            threadRun.Start();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            btnStop.Enabled = false;
            isStop = true;
            MessageBox.Show("Chờ một chút!");
        }
        private void cbLoaiProxy_CheckedChanged(object sender, EventArgs e)
        {
            if (cbLoaiProxy.Checked)
            {
                cbLoaiProxy.Text = "Proxy sock5";
            }
            else
            {
                cbLoaiProxy.Text = "Proxy http";
            }
        }

        private void cbImap_CheckedChanged(object sender, EventArgs e)
        {
            if (cbImap.Checked)
            {
                cbImap.Text = "Graph api";
            }
            else
            {
                cbImap.Text = "Imap";
            }
        }

        private void cbProxyInFile_CheckedChanged(object sender, EventArgs e)
        {
            if (cbImap.Checked)
            {
                try
                {
                    tbListProxy.Lines = File.ReadAllLines(PathApp + "proxy\\proxy.txt");
                }
                catch
                {
                    MessageBox.Show("Lỗi đọc file proxy!");
                }
            }
            else
            {
                tbListProxy.Lines = new string[] { };
            }
        }
        private static double TimePx = 0;
        private void nrudTime_ValueChanged(object sender, EventArgs e)
        {
            TimePx = Convert.ToDouble(nrudTime.Value);
        }
    }
    public class IConfig
    {
        public const string oauthUrl = "https://login.microsoftonline.com/common/oauth2/v2.0/token";
        public const string urlPostKey = "https://login.live.com/ppsecure/post.srf?";
        public const String ppftKey = "name=\"PPFT\"";
        public const String ppftKey_1 = "sFT:'";
    }
}