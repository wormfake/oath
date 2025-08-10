using MailKit;
using MailKit.Net.Imap;
using MailKit.Search;
using MailKit.Security;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MailKit.Net.Proxy;

namespace Hotmail_Get_Oauth2
{
    public class ReadCode
    {
        ImapClient client = null;
        public string StartReadCodeLogin(string mail, string pass, string proxy = null, string domain = "outlook.office365.com", string TokenAuth = null)
        {
            try
            {
                List<DataMailBoxPlus> dataMail = new List<DataMailBoxPlus>();
                for (int ie = 0; ie < 6; ie++)
                {
                    if (client != null && client.IsConnected)
                    {
                        try
                        {
                            client.Disconnect(true);
                            client.Dispose();
                        }
                        catch { }
                    }
                    for (int i = 0; i < 3; i++)
                    {
                        try
                        {
                            client = new ImapClient();
                            if (proxy != null)
                            {
                                string[] px = proxy.Split(':');
                                if (px.Length == 4)
                                {
                                    client.ProxyClient = new HttpProxyClient(px[0], Convert.ToInt32(px[1]), new System.Net.NetworkCredential(px[2], px[3]));
                                }
                                else
                                {
                                    client.ProxyClient = new HttpProxyClient(px[0], Convert.ToInt32(px[1]));
                                }
                            }
                            client.Connect(domain, 993, true);
                            if (TokenAuth == null)
                            {
                                client.Authenticate(mail, pass);
                            }
                            else
                            {
                                SaslMechanismOAuth2 auth2 = new SaslMechanismOAuth2(mail, TokenAuth);
                                client.Authenticate(auth2);
                            }
                            break;
                        }
                        catch
                        {
                            client = null;
                            Task.Delay(2000).Wait();
                        }
                    }
                    if (client == null)
                    {
                        if (TokenAuth != null)
                        {
                            return "new oauth2";
                        }
                        throw new Exception("");
                    }
                    dataMail.Clear();
                    var inbox = client.Inbox;
                    inbox.Open(FolderAccess.ReadOnly);
                    IList<UniqueId> uids = inbox.Search(SearchQuery.New);
                    var junk = client.GetFolder("Junk");
                    junk.Open(FolderAccess.ReadOnly);
                    IList<UniqueId> uids1 = junk.Search(SearchQuery.New);
                    UniqueId Trc = UniqueId.MinValue;
                    bool flag = false;
                    if (uids1.Count == 0 && uids.Count == 0)
                    {
                        Task.Delay(2000).Wait();
                        continue;
                    }
                    if (uids1.Count > 0)
                    {
                        Trc = uids1[uids1.Count - 1];
                        uids1.RemoveAt(uids1.Count - 1);
                        flag = true;
                    }
                    foreach (UniqueId lki in uids1)
                    {
                        dataMail.Add(new DataMailBoxPlus { UID = lki, MailBox = "Junk" });
                    }
                    foreach (UniqueId lki in uids)
                    {
                        dataMail.Add(new DataMailBoxPlus { UID = lki, MailBox = "INBOX" });
                    }
                    if (flag)
                    {
                        dataMail.Add(new DataMailBoxPlus { UID = Trc, MailBox = "Junk" });
                    }
                    int lenginfos = dataMail.Count;
                    int Dem = 0;
                    for (int i = dataMail.Count - 1; i >= 0; i--)
                    {
                        try
                        {
                            Dem++;
                            if (Dem > lenginfos || Dem > 10)
                            {
                                return "";
                            }
                            MimeMessage message = null;
                            if (dataMail[i].MailBox == "INBOX")
                            {
                                inbox.Open(FolderAccess.ReadOnly);
                                message = inbox.GetMessage(dataMail[i].UID);
                            }
                            else
                            {
                                junk.Open(FolderAccess.ReadOnly);
                                message = junk.GetMessage(dataMail[i].UID);
                            }
                            Task.Delay(100).Wait();
                            string string0 = "";
                            if (message.TextBody == null || message.TextBody.Length == 0)
                            {
                                string0 = message.HtmlBody;
                            }
                            else
                            {
                                string0 = message.TextBody;
                            }
                            if (message.From.ToString(false).Contains("bybit") || string0.Contains("bybit") || string0.Contains("Bybit"))
                            {
                                if (string0 != null && string0.Contains(":"))
                                {
                                    string[] strings = string0.Split('\r');
                                    foreach (var item in strings)
                                    {
                                        Regex regex = new Regex(@"\d{6}");
                                        Match match = regex.Match(item);
                                        string code = "";
                                        if (match.Success)
                                        {
                                            for (int ivn = 0; ivn < match.Groups.Count; ivn++)
                                            {
                                                code = match.Groups[ivn].Value;
                                                if (item.Contains("#" + code) || item.Contains("http"))
                                                {
                                                    code = "";
                                                    continue;
                                                }
                                                break;
                                            }
                                            if (code != "")
                                            {
                                                try
                                                {
                                                    client.Disconnect(true);
                                                    client.Dispose();
                                                }
                                                catch { }
                                                return code;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        catch { }
                    }
                    Task.Delay(2000).Wait();
                }
                try
                {
                    client.Disconnect(true);
                    client.Dispose();
                }
                catch { }
            }
            catch
            {
                try
                {
                    client.Dispose();
                }
                catch { }
                return "";
            }
            return "";
        }
    }
    public class DataMailBoxPlus
    {
        public UniqueId UID { set; get; }
        public string MailBox { set; get; } = "INBOX";
    }
}
